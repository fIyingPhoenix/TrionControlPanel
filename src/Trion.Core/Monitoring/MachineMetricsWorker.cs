using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Logging;

namespace Trion.Core.Monitoring;

/// <summary>
/// Background service that polls <see cref="IMachineMetricsProvider"/> on a fixed interval
/// and writes snapshots into <see cref="MetricsChannelAccessor"/>.
/// No events. No Action callbacks. Channel-only writes.
/// </summary>
public sealed class MachineMetricsWorker : BackgroundService
{
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

    private readonly IMachineMetricsProvider     _provider;
    private readonly MetricsChannelAccessor      _accessor;
    private readonly IOptionsMonitor<ProcessMonitorOptions> _opts;
    private readonly ILogger                     _log;

    public MachineMetricsWorker(
        IMachineMetricsProvider               provider,
        MetricsChannelAccessor                accessor,
        IOptionsMonitor<ProcessMonitorOptions> opts,
        TrionLogger                           trionLogger)
    {
        _provider = provider;
        _accessor = accessor;
        _opts     = opts;
        _log      = trionLogger.CreateLogger(nameof(MachineMetricsWorker));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _log.LogInformation("MachineMetricsWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var snapshot = _provider.GetSnapshot() with { Timestamp = DateTimeOffset.UtcNow };
                _accessor.MachineWriter.TryWrite(snapshot);
                _accessor.SetLastMachine(snapshot);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "MachineMetricsWorker poll failed — retrying in {Delay}s.", RetryDelay.TotalSeconds);
                await Task.Delay(RetryDelay, stoppingToken);
                continue;
            }

            await Task.Delay(_opts.CurrentValue.RefreshInterval, stoppingToken);
        }

        _log.LogInformation("MachineMetricsWorker stopped.");
    }
}
