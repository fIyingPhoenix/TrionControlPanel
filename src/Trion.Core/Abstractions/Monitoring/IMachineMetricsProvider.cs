using Trion.Core.Monitoring;

namespace Trion.Core.Abstractions.Monitoring;

public interface IMachineMetricsProvider
{
    MachineMetrics GetSnapshot();
}
