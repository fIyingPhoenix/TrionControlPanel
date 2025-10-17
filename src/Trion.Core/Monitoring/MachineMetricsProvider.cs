using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

#if WINDOWS
using System.Diagnostics.PerformanceCounter;
#endif

namespace Trion.Core.Monitoring;

public sealed class MachineMetricsProvider : IMachineMetricsProvider
{
    private readonly object _lock = new();
#if WINDOWS
    private PerformanceCounter? _cpu;
#endif
    private (long read, long write) _prevDisk;
    private (long recv, long sent) _prevNet;

    public MachineMetrics GetSnapshot()
    {
        lock (_lock)
        {
            var now = DateTimeOffset.UtcNow;
            var cpu = GetCpuPercent();
            var (total, used) = GetRamBytes();
            var (diskRead, diskWrite) = GetDiskBytesDelta(now);
            var (netRecv, netSent) = GetNetworkBytesDelta(now);
            return new MachineMetrics(cpu, used / 1024 / 1024, total / 1024 / 1024,
                                      diskRead, diskWrite, netRecv, netSent, now);
        }
    }

    private double GetCpuPercent()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
#if WINDOWS
            _cpu ??= new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return Math.Clamp(_cpu.NextValue(), 0, 100);
#else
            return 0;
#endif
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var line = File.ReadLines("/proc/stat").First(l => l.StartsWith("cpu "));
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var user = double.Parse(parts[1]);
            var nice = double.Parse(parts[2]);
            var sys = double.Parse(parts[3]);
            var idle = double.Parse(parts[4]);
            var total = user + nice + sys + idle;
            return total == 0 ? 0 : Math.Clamp((1.0 - idle / total) * 100.0, 0, 100);
        }
        return 0;
    }

    private (long total, long used) GetRamBytes()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
#if WINDOWS
            using var mem = new PerformanceCounter("Memory", "Available Bytes");
            var avail = (long)mem.RawValue;
            var total = GetTotalRamWindows();
            return (total, total - avail);
#else
            return (0, 0);
#endif
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var total = long.Parse(File.ReadLines("/proc/meminfo")
                                   .First(l => l.StartsWith("MemTotal"))
                                   .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]) * 1024;
            var avail = long.Parse(File.ReadLines("/proc/meminfo")
                                   .First(l => l.StartsWith("MemAvailable"))
                                   .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]) * 1024;
            return (total, total - avail);
        }
        return (0, 0);
    }

    private (long read, long write) GetDiskBytesDelta(DateTimeOffset now)
    {
        (long read, long write) raw = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? GetWindowsDiskRaw()
            : GetLinuxDiskRaw();
        long deltaRead = raw.read - _prevDisk.read;
        long deltaWrite = raw.write - _prevDisk.write;
        _prevDisk = raw;
        return (deltaRead, deltaWrite);
    }

    private (long recv, long sent) GetNetworkBytesDelta(DateTimeOffset now)
    {
        (long recv, long sent) raw = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? (0, 0)   // replace with perf-counter if wanted
            : (0, 0);
        long deltaRecv = raw.recv - _prevNet.recv;
        long deltaSent = raw.sent - _prevNet.sent;
        _prevNet = raw;
        return (deltaRecv, deltaSent);
    }

    /* ---------- helpers ---------- */
    private static (long read, long write) GetWindowsDiskRaw()
    {
#if WINDOWS
        using var r = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
        using var w = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
        return ((long)r.RawValue, (long)w.RawValue);
#else
        return (0L, 0L);
#endif
    }

    private static (long read, long write) GetLinuxDiskRaw() => (0L, 0L);

#if WINDOWS
    private static long GetTotalRamWindows()
    {
        using var mc = new System.Management.ManagementObjectSearcher(
            "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        return (long)mc.Get().Cast<System.Management.ManagementObject>().First()["TotalPhysicalMemory"];
    }
#endif
}