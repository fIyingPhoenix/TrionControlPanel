namespace Trion.Core.Monitoring;

public sealed class ProcessMonitorOptions
{
    /// <summary>How often the monitor polls for new metrics.</summary>
    public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Only track processes whose name contains one of these strings (case-insensitive).
    /// If empty, no automatic discovery occurs — use <see cref="ProcessMonitor.Track"/> instead.
    /// </summary>
    public string[] ProcessNameFilter { get; set; } = [];

    /// <summary>Whether to collect per-process disk I/O (reads /proc/{pid}/io on Linux).</summary>
    public bool EnableDisk { get; set; } = true;

    // ExplicitPids removed — use ProcessMonitor.Track(pid) / Untrack(pid) at runtime.
    // Config-level PIDs were unreliable because PIDs are ephemeral and not stable across restarts.
}
