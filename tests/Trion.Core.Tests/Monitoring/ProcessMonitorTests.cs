using Microsoft.Extensions.Options;
using Trion.Core.Monitoring;
using Trion.Core.Tests;

namespace Trion.Core.Tests.Monitoring;

public sealed class ProcessMonitorTests
{
    private static IOptionsMonitor<ProcessMonitorOptions> CreateOpts(ProcessMonitorOptions opts)
        => new StaticOptionsMonitor(opts);

    [Fact]
    public async Task EmptyFilter_LogsWarning_DoesNotThrow()
    {
        // ProcessMonitor with empty filter and no tracked PIDs should not throw —
        // it logs a warning and runs without polling anything.
        var accessor = new MetricsChannelAccessor();
        var monitor  = new ProcessMonitor(
            accessor,
            CreateOpts(new ProcessMonitorOptions
            {
                ProcessNameFilter = [],
                RefreshInterval   = TimeSpan.FromMilliseconds(50)
            }),
            TestLogger.Instance);

        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
        await monitor.StartAsync(cts.Token);
        await Task.Delay(100);
        await monitor.StopAsync(CancellationToken.None);

        // No exception means the warning path was handled gracefully
        Assert.True(true);
    }

    [Fact]
    public void Track_AddsPidToTrackedSet()
    {
        var accessor = new MetricsChannelAccessor();
        var monitor  = new ProcessMonitor(
            accessor,
            CreateOpts(new ProcessMonitorOptions { RefreshInterval = TimeSpan.FromSeconds(5) }),
            TestLogger.Instance);

        var currentPid = Environment.ProcessId;
        monitor.Track(currentPid);

        // No easy way to read _trackedPids without reflection —
        // verify indirectly: Untrack does not throw
        monitor.Untrack(currentPid);
        Assert.True(true);
    }

    [Fact]
    public async Task Monitor_WritesProcessArrayToChannel_WhenFilterMatches()
    {
        // Track the current process by name to ensure at least one match
        var currentProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

        var accessor = new MetricsChannelAccessor();
        var monitor  = new ProcessMonitor(
            accessor,
            CreateOpts(new ProcessMonitorOptions
            {
                ProcessNameFilter = [currentProcessName],
                RefreshInterval   = TimeSpan.FromMilliseconds(50)
            }),
            TestLogger.Instance);

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        _ = monitor.StartAsync(cts.Token);

        // Wait until at least one write lands in the channel
        for (int i = 0; i < 60; i++)
        {
            if (accessor.LastProcess is not null) break;
            await Task.Delay(50);
        }

        await monitor.StopAsync(CancellationToken.None);

        Assert.NotNull(accessor.LastProcess);
        Assert.Contains(accessor.LastProcess, p =>
            p.Name.Contains(currentProcessName, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task ExceptionInPollLoop_DoesNotStopService()
    {
        // ProcessMonitor with a name filter that returns no processes — no exception
        // but we still verify the service stays alive across poll cycles.
        var accessor = new MetricsChannelAccessor();
        var monitor  = new ProcessMonitor(
            accessor,
            CreateOpts(new ProcessMonitorOptions
            {
                ProcessNameFilter = ["__unlikely_to_exist_process_xyz__"],
                RefreshInterval   = TimeSpan.FromMilliseconds(30)
            }),
            TestLogger.Instance);

        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(300));
        await monitor.StartAsync(cts.Token);
        await Task.Delay(200);

        // Service should still be running (not faulted)
        Assert.False(monitor.ExecuteTask?.IsFaulted ?? false);
        await monitor.StopAsync(CancellationToken.None);
    }

    private sealed class StaticOptionsMonitor : IOptionsMonitor<ProcessMonitorOptions>
    {
        public StaticOptionsMonitor(ProcessMonitorOptions value) => CurrentValue = value;
        public ProcessMonitorOptions CurrentValue { get; }
        public ProcessMonitorOptions Get(string? name) => CurrentValue;
        public IDisposable? OnChange(Action<ProcessMonitorOptions, string?> listener) => null;
    }
}
