using Trion.Core.Monitoring;

namespace Trion.Core.Abstractions.Monitoring;

public interface IMetricsChannelReader : IAsyncDisposable
{
    ValueTask<MachineMetrics> ReadMachineAsync(CancellationToken ct = default);
    ValueTask<ProcessMetrics[]> ReadProcessAsync(CancellationToken ct = default);
}
