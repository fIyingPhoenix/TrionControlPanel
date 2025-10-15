namespace Trion.Core.Monitoring;

public interface IMachineMetricsProvider
{
    MachineMetrics GetSnapshot();
}