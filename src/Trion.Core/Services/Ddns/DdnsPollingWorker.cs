using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Services;
using Trion.Core.Logging;

namespace Trion.Core.Services.Ddns;

public sealed class DdnsPollingWorker : BackgroundService
{
    private readonly IDdnsUpdater          _updater;
    private readonly IOptions<DdnsOptions> _options;
    private readonly ILogger               _log;

    public DdnsPollingWorker(
        IDdnsUpdater          updater,
        IOptions<DdnsOptions> options,
        TrionLogger           trionLogger)
    {
        _updater = updater;
        _options = options;
        _log     = trionLogger.CreateLogger(nameof(DdnsPollingWorker));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_options.Value.Provider == DdnsProvider.None)
        {
            _log.LogDebug("DDNS provider is None — polling worker is idle.");
            return;
        }

        _log.LogInformation(
            "DDNS polling worker started (provider: {Provider}, interval: {Interval}).",
            _options.Value.Provider, _options.Value.PollInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _updater.UpdateNowAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "DDNS polling worker encountered an unexpected error.");
            }

            try
            {
                await Task.Delay(_options.Value.PollInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }
}
