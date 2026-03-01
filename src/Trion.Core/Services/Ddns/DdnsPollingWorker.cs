using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Services;

namespace Trion.Core.Services.Ddns;

public sealed class DdnsPollingWorker : BackgroundService
{
    private readonly IDdnsUpdater          _updater;
    private readonly IOptions<DdnsOptions> _options;
    private readonly ILogger<DdnsPollingWorker> _logger;

    public DdnsPollingWorker(
        IDdnsUpdater              updater,
        IOptions<DdnsOptions>     options,
        ILogger<DdnsPollingWorker> logger)
    {
        _updater = updater;
        _options = options;
        _logger  = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_options.Value.Provider == DdnsProvider.None)
        {
            _logger.LogDebug("DDNS provider is None — polling worker is idle.");
            return;
        }

        _logger.LogInformation(
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
                _logger.LogError(ex, "DDNS polling worker encountered an unexpected error.");
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
