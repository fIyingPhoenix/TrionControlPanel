using System.Diagnostics;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Trion.Core.Logging;

namespace Trion.Platform.Windows;

[SupportedOSPlatform("windows")]
public sealed class WindowsDiskReader : IDisposable
{
    private readonly ILogger _log;
    private PerformanceCounter? _readCounter;
    private PerformanceCounter? _writeCounter;

    public WindowsDiskReader(TrionLogger trionLogger)
    {
        _log = trionLogger.CreateLogger(nameof(WindowsDiskReader));
        try
        {
            _readCounter  = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec",  "_Total");
            _writeCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
            // Prime both counters (first call returns 0)
            _readCounter.NextValue();
            _writeCounter.NextValue();
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "Disk PerformanceCounters unavailable.");
            _readCounter  = null;
            _writeCounter = null;
        }
    }

    /// <summary>
    /// Returns current disk read/write throughput in bytes per second.
    /// Windows PerformanceCounter returns a rate directly — no delta required.
    /// </summary>
    public (float ReadBytesPerSec, float WriteBytesPerSec) GetDiskThroughput()
    {
        try
        {
            var r = _readCounter?.NextValue()  ?? 0f;
            var w = _writeCounter?.NextValue() ?? 0f;
            return (r, w);
        }
        catch { return (0f, 0f); }
    }

    public void Dispose()
    {
        _readCounter?.Dispose();
        _writeCounter?.Dispose();
    }
}
