namespace Trion.Desktop.Models;

/// <summary>
/// Snapshot of system resource usage captured at a single point in time.
/// </summary>
public sealed class SystemMetrics
{
    // ── CPU ───────────────────────────────────────────────────────────────────

    /// <summary>CPU utilisation across all logical processors, 0–100 %.</summary>
    public float CpuPercent { get; init; }

    // ── RAM ───────────────────────────────────────────────────────────────────

    public long  RamTotalMb { get; init; }
    public long  RamUsedMb  { get; init; }
    /// <summary>RAM utilisation, 0–100 %.</summary>
    public float RamPercent { get; init; }

    // ── Disk (aggregate of all physical disks) ────────────────────────────────

    /// <summary>Disk read throughput in bytes per second.</summary>
    public float DiskReadBps  { get; init; }
    /// <summary>Disk write throughput in bytes per second.</summary>
    public float DiskWriteBps { get; init; }

    // ── Network (sum of all active adapters) ──────────────────────────────────

    /// <summary>Inbound network throughput in bytes per second.</summary>
    public float NetRecvBps { get; init; }
    /// <summary>Outbound network throughput in bytes per second.</summary>
    public float NetSendBps { get; init; }
}
