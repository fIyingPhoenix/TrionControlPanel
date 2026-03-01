// Legacy fallback implementation — superseded by Trion.Platform.Linux.LinuxMetricsProvider
// and Trion.Platform.Windows.WindowsMetricsProvider registered via AddPlatformServices().
// Kept here only so existing tests that new-up this class directly still compile.
// Do not register this class in DI — use the platform-specific implementations.

using Trion.Core.Abstractions.Monitoring;

namespace Trion.Core.Monitoring;

[Obsolete("Use LinuxMetricsProvider or WindowsMetricsProvider via AddPlatformServices(). " +
          "This class will be removed in a future milestone.")]
public sealed class MachineMetricsProvider : IMachineMetricsProvider
{
    public MachineMetrics GetSnapshot() => new(
        CpuPercent:           0.0,
        RamTotalBytes:        0,
        RamUsedBytes:         0,
        DiskReadBytesPerSec:  0,
        DiskWriteBytesPerSec: 0,
        NetworkRxBytesPerSec: 0,
        NetworkTxBytesPerSec: 0);
}
