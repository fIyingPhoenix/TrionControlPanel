using System.Threading.Channels;
using Trion.Core.Abstractions.Monitoring;

namespace Trion.Core.Monitoring;

/// <summary>
/// Per-consumer reader over the shared metric channels.
/// Dispose to release the linked cancellation source.
/// </summary>
internal sealed class MetricsChannelReader : IMetricsChannelReader
{
    private readonly ChannelReader<MachineMetrics>   _machineReader;
    private readonly ChannelReader<ProcessMetrics[]> _processReader;
    private readonly CancellationTokenSource         _cts = new();

    internal MetricsChannelReader(
        ChannelReader<MachineMetrics>   machineReader,
        ChannelReader<ProcessMetrics[]> processReader)
    {
        _machineReader = machineReader;
        _processReader = processReader;
    }

    public ValueTask<MachineMetrics> ReadMachineAsync(CancellationToken ct = default)
    {
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
        return _machineReader.ReadAsync(linked.Token);
    }

    public ValueTask<ProcessMetrics[]> ReadProcessAsync(CancellationToken ct = default)
    {
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
        return _processReader.ReadAsync(linked.Token);
    }

    public ValueTask DisposeAsync()
    {
        _cts.Cancel();
        _cts.Dispose();
        return ValueTask.CompletedTask;
    }
}
