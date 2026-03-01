using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Logging;
using Trion.Core.Monitoring;

namespace Trion.Platform.Windows;

[SupportedOSPlatform("windows")]
public sealed class WindowsMetricsProvider : IMachineMetricsProvider, IDisposable
{
    private readonly ILogger              _log;
    private readonly WindowsDiskReader    _disk;
    private readonly WindowsNetworkReader _net;

    private PerformanceCounter? _cpuCounter;
    private long _totalRamBytes;

    public WindowsMetricsProvider(
        TrionLogger          trionLogger,
        WindowsDiskReader    disk,
        WindowsNetworkReader net)
    {
        _log  = trionLogger.CreateLogger(nameof(WindowsMetricsProvider));
        _disk = disk;
        _net  = net;

        InitCpuCounter();
        InitTotalRam();
    }

    public MachineMetrics GetSnapshot()
    {
        var cpu = GetCpuPercent();
        var usedRam = GetUsedRamBytes();
        var (diskRead, diskWrite) = _disk.GetDiskThroughput();
        var (netRx, netTx) = _net.GetNetworkThroughput();

        return new MachineMetrics(
            CpuPercent:           cpu,
            RamTotalBytes:        _totalRamBytes,
            RamUsedBytes:         usedRam,
            DiskReadBytesPerSec:  (long)diskRead,
            DiskWriteBytesPerSec: (long)diskWrite,
            NetworkRxBytesPerSec: (long)netRx,
            NetworkTxBytesPerSec: (long)netTx,
            Timestamp:            DateTimeOffset.UtcNow);
    }

    private void InitCpuCounter()
    {
        try
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _cpuCounter.NextValue(); // First call always returns 0 — discard
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "CPU PerformanceCounter unavailable.");
            _cpuCounter = null;
        }
    }

    private void InitTotalRam()
    {
        try
        {
            if (TryGetMemoryStatus(out var mem))
                _totalRamBytes = (long)mem.ullTotalPhys;
            else
                _totalRamBytes = 0;
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "GlobalMemoryStatusEx unavailable.");
            _totalRamBytes = 0;
        }
    }

    private double GetCpuPercent()
    {
        if (_cpuCounter is null) return 0.0;
        try { return Math.Round(_cpuCounter.NextValue(), 1); }
        catch { return 0.0; }
    }

    private long GetUsedRamBytes()
    {
        try
        {
            if (!TryGetMemoryStatus(out var mem)) return 0;
            return (long)(mem.ullTotalPhys - mem.ullAvailPhys);
        }
        catch { return 0; }
    }

    // ── GlobalMemoryStatusEx P/Invoke ─────────────────────────────────────

    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORYSTATUSEX
    {
        public uint  dwLength;
        public uint  dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    private static bool TryGetMemoryStatus(out MEMORYSTATUSEX mem)
    {
        mem = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>() };
        return GlobalMemoryStatusEx(ref mem);
    }

    public void Dispose() => _cpuCounter?.Dispose();
}
