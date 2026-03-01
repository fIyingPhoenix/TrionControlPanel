using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Monitoring;

namespace Trion.Core.Tests.Monitoring;

public sealed class MachineMetricsWorkerTests
{
    private static readonly ProcessMonitorOptions DefaultOpts = new()
    {
        RefreshInterval = TimeSpan.FromMilliseconds(50)
    };

    private static IOptionsMonitor<ProcessMonitorOptions> CreateOpts(ProcessMonitorOptions? opts = null)
        => new StaticOptionsMonitor(opts ?? DefaultOpts);

    [Fact]
    public async Task Worker_WritesMetricsToChannel()
    {
        var provider = new FakeMetricsProvider();
        var accessor = new MetricsChannelAccessor();
        var worker   = new MachineMetricsWorker(
            provider, accessor, CreateOpts(),
            NullLogger<MachineMetricsWorker>.Instance);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        _ = worker.StartAsync(cts.Token);

        // Wait until at least one metric is available
        for (int i = 0; i < 40; i++)
        {
            if (accessor.LastMachine is not null) break;
            await Task.Delay(50);
        }

        await worker.StopAsync(CancellationToken.None);
        Assert.NotNull(accessor.LastMachine);
    }

    [Fact]
    public async Task Worker_RetriesAfterProviderException_DoesNotCrash()
    {
        var provider = new ThrowingMetricsProvider(throwCount: 3);
        var accessor = new MetricsChannelAccessor();
        var worker   = new MachineMetricsWorker(
            provider, accessor, CreateOpts(new ProcessMonitorOptions
            {
                RefreshInterval = TimeSpan.FromMilliseconds(20)
            }),
            NullLogger<MachineMetricsWorker>.Instance);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        _ = worker.StartAsync(cts.Token);

        // After 3 throws the provider returns real data; wait for it
        for (int i = 0; i < 80; i++)
        {
            if (accessor.LastMachine is not null) break;
            await Task.Delay(50);
        }

        await worker.StopAsync(CancellationToken.None);
        // Worker survived the exceptions and eventually wrote a metric
        Assert.NotNull(accessor.LastMachine);
    }

    // ── Fakes ────────────────────────────────────────────────────────────────

    private sealed class FakeMetricsProvider : IMachineMetricsProvider
    {
        public MachineMetrics GetSnapshot() => new(
            CpuPercent: 10.0, RamTotalBytes: 8_000_000_000, RamUsedBytes: 2_000_000_000,
            DiskReadBytesPerSec: 0, DiskWriteBytesPerSec: 0,
            NetworkRxBytesPerSec: 0, NetworkTxBytesPerSec: 0);
    }

    private sealed class ThrowingMetricsProvider : IMachineMetricsProvider
    {
        private int _remaining;
        public ThrowingMetricsProvider(int throwCount) => _remaining = throwCount;

        public MachineMetrics GetSnapshot()
        {
            if (_remaining > 0)
            {
                _remaining--;
                throw new InvalidOperationException("Simulated provider failure.");
            }
            return new(10.0, 8_000_000_000, 2_000_000_000, 0, 0, 0, 0);
        }
    }

    private sealed class StaticOptionsMonitor : IOptionsMonitor<ProcessMonitorOptions>
    {
        public StaticOptionsMonitor(ProcessMonitorOptions value) => CurrentValue = value;
        public ProcessMonitorOptions CurrentValue { get; }
        public ProcessMonitorOptions Get(string? name) => CurrentValue;
        public IDisposable? OnChange(Action<ProcessMonitorOptions, string?> listener) => null;
    }
}
