using System.Diagnostics;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;

namespace Trion.Platform.Windows;

[SupportedOSPlatform("windows")]
public sealed class WindowsNetworkReader : IDisposable
{
    private readonly ILogger<WindowsNetworkReader> _logger;

    // Cached per-interface counter pairs — created once, reused on every poll
    private readonly List<(PerformanceCounter Rx, PerformanceCounter Tx)> _counters = [];
    private bool _initialized;

    public WindowsNetworkReader(ILogger<WindowsNetworkReader> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns aggregated network receive/send throughput in bytes per second.
    /// </summary>
    public (float RxBytesPerSec, float TxBytesPerSec) GetNetworkThroughput()
    {
        EnsureInitialized();

        float totalRx = 0f, totalTx = 0f;
        foreach (var (rx, tx) in _counters)
        {
            try
            {
                totalRx += rx.NextValue();
                totalTx += tx.NextValue();
            }
            catch { /* skip a failed interface */ }
        }
        return (totalRx, totalTx);
    }

    private void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;

        try
        {
            var category  = new PerformanceCounterCategory("Network Interface");
            var instances = category.GetInstanceNames();

            foreach (var instance in instances)
            {
                var rx = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
                var tx = new PerformanceCounter("Network Interface", "Bytes Sent/sec",     instance);
                // Prime the counters
                rx.NextValue();
                tx.NextValue();
                _counters.Add((rx, tx));
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Network Interface PerformanceCounters unavailable.");
        }
    }

    public void Dispose()
    {
        foreach (var (rx, tx) in _counters)
        {
            rx.Dispose();
            tx.Dispose();
        }
        _counters.Clear();
    }
}
