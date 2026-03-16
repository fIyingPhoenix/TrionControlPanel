using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Background service that samples CPU, RAM, disk, and network metrics every two seconds
/// using Windows <see cref="PerformanceCounter"/>s on a dedicated background thread.
/// </summary>
public sealed class SystemMetricsService : ISystemMetricsService, IHostedService, IDisposable
{
    // ── Public API ────────────────────────────────────────────────────────────

    public SystemMetrics? Last { get; private set; }
    public event Action<SystemMetrics>? Updated;

    // ── Internals ─────────────────────────────────────────────────────────────

    private CancellationTokenSource? _cts;
    private Thread?                  _thread;
    private bool                     _countersAvailable;

    // CPU
    private PerformanceCounter? _cpuCounter;

    // RAM
    private PerformanceCounter? _availBytesCounter;
    private readonly long       _totalRamBytes;

    // Disk
    private PerformanceCounter? _diskReadCounter;
    private PerformanceCounter? _diskWriteCounter;

    // Network — one counter pair per real adapter; values are summed
    private PerformanceCounter[] _netRecvCounters = [];
    private PerformanceCounter[] _netSendCounters = [];

    private readonly ISettingsService _settings;

    // ── Constructor ───────────────────────────────────────────────────────────

    public SystemMetricsService(ISettingsService settings)
    {
        _settings = settings;

        // TotalAvailableMemoryBytes maps to GlobalMemoryStatusEx.ullTotalPhys on Windows.
        // Use as a stable fallback; real value confirmed against PerformanceCounter "Available Bytes".
        _totalRamBytes = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
        if (_totalRamBytes <= 0) _totalRamBytes = 4L * 1024 * 1024 * 1024; // 4 GB safe fallback
    }

    // ── IHostedService ────────────────────────────────────────────────────────

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts    = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _thread = new Thread(SampleLoop)
        {
            IsBackground = true,
            Name         = "Trion.MetricsSampler",
            Priority     = ThreadPriority.BelowNormal,
        };
        _thread.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        return Task.CompletedTask;
    }

    // ── Sampling loop ─────────────────────────────────────────────────────────

    private void SampleLoop()
    {
        try
        {
            _countersAvailable = TryInitCounters();

            if (!_countersAvailable)
            {
                Debug.WriteLine("[MetricsSampler] PerformanceCounters unavailable — metrics disabled.");
                return;
            }

            // Warm-up: first NextValue() always returns 0 for rate counters.
            // Discard it, wait 1 s, then start the real loop.
            WarmUp();
            Thread.Sleep(1_000);

            while (!_cts!.Token.IsCancellationRequested)
            {
                var snapshot = BuildSnapshot();
                Last = snapshot;
                Updated?.Invoke(snapshot);

                // Interval is read from settings on every iteration so changes apply immediately.
                int intervalMs = Math.Clamp(_settings.Current.MetricsIntervalSeconds, 1, 10) * 1_000;
                var deadline   = DateTime.UtcNow.AddMilliseconds(intervalMs);
                while (DateTime.UtcNow < deadline && !_cts.Token.IsCancellationRequested)
                    Thread.Sleep(200);
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MetricsSampler] Fatal: {ex.Message}");
        }
    }

    // ── Counter initialisation ────────────────────────────────────────────────

    private bool TryInitCounters()
    {
        try
        {
            // CPU — "Processor Information" gives hyperthreading-aware values.
            // Fall back to "Processor" on systems that don't have the category.
            if (PerformanceCounterCategory.Exists("Processor Information"))
                _cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            else
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // RAM
            _availBytesCounter = new PerformanceCounter("Memory", "Available Bytes");

            // Disk — _Total aggregates all physical disks
            _diskReadCounter  = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec",  "_Total");
            _diskWriteCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

            // Network — build a counter pair per real adapter, then sum
            InitNetworkCounters();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MetricsSampler] Counter init failed: {ex.Message}");
            return false;
        }
    }

    private void InitNetworkCounters()
    {
        try
        {
            var cat       = new PerformanceCounterCategory("Network Interface");
            var adapters  = cat.GetInstanceNames()
                               .Where(IsRealAdapter)
                               .ToArray();

            _netRecvCounters = adapters
                .Select(n => new PerformanceCounter("Network Interface", "Bytes Received/sec", n))
                .ToArray();

            _netSendCounters = adapters
                .Select(n => new PerformanceCounter("Network Interface", "Bytes Sent/sec", n))
                .ToArray();
        }
        catch
        {
            _netRecvCounters = [];
            _netSendCounters = [];
        }
    }

    /// <summary>Returns true for real Ethernet/Wi-Fi adapters, filters out virtual/tunnel interfaces.</summary>
    private static bool IsRealAdapter(string name)
    {
        var n = name.ToLowerInvariant();
        return !n.Contains("loopback")
            && !n.Contains("teredo")
            && !n.Contains("isatap")
            && !n.Contains("6to4")
            && !n.Contains("pseudo");
    }

    // ── Warm-up & sampling ────────────────────────────────────────────────────

    private void WarmUp()
    {
        _cpuCounter!.NextValue();
        _availBytesCounter!.NextValue();
        _diskReadCounter!.NextValue();
        _diskWriteCounter!.NextValue();
        foreach (var c in _netRecvCounters) c.NextValue();
        foreach (var c in _netSendCounters) c.NextValue();
    }

    private SystemMetrics BuildSnapshot()
    {
        float cpu       = Math.Clamp(_cpuCounter!.NextValue(), 0f, 100f);
        float availBytes = _availBytesCounter!.NextValue();
        float diskRead  = _diskReadCounter!.NextValue();
        float diskWrite = _diskWriteCounter!.NextValue();

        float netRecv = _netRecvCounters.Length > 0
            ? _netRecvCounters.Sum(c => c.NextValue()) : 0f;
        float netSend = _netSendCounters.Length > 0
            ? _netSendCounters.Sum(c => c.NextValue()) : 0f;

        long totalMb = _totalRamBytes / (1024L * 1024L);
        long availMb = (long)(availBytes  / (1024L * 1024L));
        long usedMb  = Math.Max(0, totalMb - availMb);
        float ramPct = totalMb > 0 ? (float)usedMb / totalMb * 100f : 0f;

        return new SystemMetrics
        {
            CpuPercent   = cpu,
            RamTotalMb   = totalMb,
            RamUsedMb    = usedMb,
            RamPercent   = ramPct,
            DiskReadBps  = Math.Max(0f, diskRead),
            DiskWriteBps = Math.Max(0f, diskWrite),
            NetRecvBps   = Math.Max(0f, netRecv),
            NetSendBps   = Math.Max(0f, netSend),
        };
    }

    // ── Formatting helpers (used by consumers) ────────────────────────────────

    public static string FormatBps(float bps)
    {
        if (bps >= 1_073_741_824f) return $"{bps / 1_073_741_824f:F2} GB/s";
        if (bps >= 1_048_576f)     return $"{bps / 1_048_576f:F1} MB/s";
        if (bps >= 1_024f)         return $"{bps / 1_024f:F1} KB/s";
        return $"{bps:F0} B/s";
    }

    public static string FormatMb(long mb)
    {
        if (mb >= 1024) return $"{mb / 1024.0:F1} GB";
        return $"{mb} MB";
    }

    // ── Dispose ───────────────────────────────────────────────────────────────

    public void Dispose()
    {
        _cts?.Cancel();
        _cpuCounter?.Dispose();
        _availBytesCounter?.Dispose();
        _diskReadCounter?.Dispose();
        _diskWriteCounter?.Dispose();
        foreach (var c in _netRecvCounters) c.Dispose();
        foreach (var c in _netSendCounters) c.Dispose();
    }
}
