using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface ISystemMetricsService
{
    /// <summary>Most recent snapshot, or null before the first sample is ready.</summary>
    SystemMetrics? Last { get; }

    /// <summary>Raised on the background thread whenever a new snapshot is available (~2 s interval).</summary>
    event Action<SystemMetrics>? Updated;
}
