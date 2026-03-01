using System.Runtime.Versioning;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Monitoring;

namespace Trion.Platform.Linux;

[SupportedOSPlatform("linux")]
public sealed class LinuxMetricsProvider : IMachineMetricsProvider
{
    private readonly LinuxDiskReader    _disk;
    private readonly LinuxNetworkReader _net;
    private readonly LinuxRamReader     _ram;
    private readonly string             _procStatPath;

    // Previous CPU snapshot for delta calculation
    private (long user, long nice, long sys, long idle, long iowait,
             long irq, long softirq, long steal, long guest, long guestNice)? _prevCpu;

    public LinuxMetricsProvider(
        LinuxDiskReader    disk,
        LinuxNetworkReader net,
        LinuxRamReader     ram,
        string             procStatPath = "/proc/stat")
    {
        _disk         = disk;
        _net          = net;
        _ram          = ram;
        _procStatPath = procStatPath;
    }

    public MachineMetrics GetSnapshot()
    {
        var cpu  = GetCpuPercent();
        var (totalRam, usedRam) = _ram.GetRamBytes();
        var (diskRead, diskWrite) = _disk.GetDiskBytesDelta();
        var (netRx, netTx) = _net.GetNetworkBytesDelta();

        return new MachineMetrics(
            CpuPercent:           cpu,
            RamTotalBytes:        totalRam,
            RamUsedBytes:         usedRam,
            DiskReadBytesPerSec:  diskRead,
            DiskWriteBytesPerSec: diskWrite,
            NetworkRxBytesPerSec: netRx,
            NetworkTxBytesPerSec: netTx,
            Timestamp:            DateTimeOffset.UtcNow);
    }

    private double GetCpuPercent()
    {
        // /proc/stat first line: cpu user nice sys idle iowait irq softirq steal guest guest_nice
        string? cpuLine = null;
        using (var reader = new StreamReader(_procStatPath))
        {
            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                if (line.StartsWith("cpu ", StringComparison.Ordinal))
                {
                    cpuLine = line;
                    break;
                }
            }
        }

        if (cpuLine is null) return 0.0;

        var parts = cpuLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        // parts[0] = "cpu", parts[1..10] = 10 numeric fields
        if (parts.Length < 11) return 0.0;

        var cur = (
            user:      long.Parse(parts[1]),
            nice:      long.Parse(parts[2]),
            sys:       long.Parse(parts[3]),
            idle:      long.Parse(parts[4]),
            iowait:    long.Parse(parts[5]),
            irq:       long.Parse(parts[6]),
            softirq:   long.Parse(parts[7]),
            steal:     long.Parse(parts[8]),
            guest:     long.Parse(parts[9]),
            guestNice: long.Parse(parts[10])
        );

        if (_prevCpu is null)
        {
            _prevCpu = cur;
            return 0.0;
        }

        var prev = _prevCpu.Value;

        var curIdle   = cur.idle  + cur.iowait;
        var prevIdle  = prev.idle + prev.iowait;

        var curTotal  = cur.user + cur.nice + cur.sys + cur.idle + cur.iowait
                      + cur.irq + cur.softirq + cur.steal + cur.guest + cur.guestNice;
        var prevTotal = prev.user + prev.nice + prev.sys + prev.idle + prev.iowait
                      + prev.irq + prev.softirq + prev.steal + prev.guest + prev.guestNice;

        var deltaTotal  = curTotal  - prevTotal;
        var deltaIdle   = curIdle   - prevIdle;
        var deltaActive = deltaTotal - deltaIdle;

        _prevCpu = cur;

        if (deltaTotal == 0) return 0.0;
        return Math.Round((double)deltaActive / deltaTotal * 100.0, 1);
    }
}
