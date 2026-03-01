using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Monitoring;
using Trion.Core.Logging;

namespace Trion.Core.Monitoring;

/// <summary>
/// Background service that tracks a set of processes and writes
/// <see cref="ProcessMetrics"/> arrays into <see cref="MetricsChannelAccessor"/>.
/// PIDs are tracked at runtime via <see cref="Track"/>/<see cref="Untrack"/>.
/// No event Action callbacks — channel-only writes.
/// </summary>
public sealed class ProcessMonitor : BackgroundService, IProcessMonitor
{
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

    private readonly MetricsChannelAccessor              _accessor;
    private readonly IOptionsMonitor<ProcessMonitorOptions> _opts;
    private readonly ILogger                             _log;

    // Runtime-tracked PIDs — replaces the config-level ExplicitPids array
    private readonly ConcurrentDictionary<int, byte> _trackedPids = new();

    // Previous snapshot per PID
    private readonly ConcurrentDictionary<int, ProcessMetricsSnapshot> _previous = new();

    /// <summary>
    /// Raised when a tracked process exits. Low-frequency event — OK to use here.
    /// </summary>
    public event Action<int>? OnProcessExited;

    public ProcessMonitor(
        MetricsChannelAccessor                accessor,
        IOptionsMonitor<ProcessMonitorOptions> opts,
        TrionLogger                           trionLogger)
    {
        _accessor = accessor;
        _opts     = opts;
        _log      = trionLogger.CreateLogger(nameof(ProcessMonitor));
    }

    /// <summary>Adds a PID to the live tracking set.</summary>
    public void Track(int pid)   => _trackedPids.TryAdd(pid, 0);

    /// <summary>Removes a PID from the live tracking set.</summary>
    public void Untrack(int pid)
    {
        _trackedPids.TryRemove(pid, out _);
        _previous.TryRemove(pid, out _);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var opts = _opts.CurrentValue;

        if (opts.ProcessNameFilter.Length == 0 && _trackedPids.IsEmpty)
            _log.LogWarning(
                "ProcessMonitor: ProcessNameFilter is empty and no PIDs tracked — " +
                "no processes will be monitored until Track() is called.");

        _log.LogInformation("ProcessMonitor started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var metrics = Poll(stoppingToken);
                _accessor.ProcessWriter.TryWrite(metrics);
                _accessor.SetLastProcess(metrics);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "ProcessMonitor poll failed — retrying in {Delay}s.", RetryDelay.TotalSeconds);
                await Task.Delay(RetryDelay, stoppingToken);
                continue;
            }

            await Task.Delay(_opts.CurrentValue.RefreshInterval, stoppingToken);
        }

        _log.LogInformation("ProcessMonitor stopped.");
    }

    private ProcessMetrics[] Poll(CancellationToken ct)
    {
        var now  = DateTimeOffset.UtcNow;
        var opts = _opts.CurrentValue;

        // Discover processes matching name filter
        var discovered = DiscoverByFilter(opts.ProcessNameFilter);

        // Merge with runtime-tracked PIDs
        foreach (var pid in _trackedPids.Keys)
        {
            var p = SafeGetProcess(pid);
            if (p is not null && discovered.All(x => x.Id != pid))
                discovered.Add(p);
        }

        var alivePids = new HashSet<int>(discovered.Select(p => p.Id));

        // Remove dead entries and raise exit events
        foreach (var pid in _previous.Keys.ToArray())
        {
            if (!alivePids.Contains(pid))
            {
                _previous.TryRemove(pid, out _);
                _trackedPids.TryRemove(pid, out _);
                RaiseExited(pid);
            }
        }

        var results = new List<ProcessMetrics>(discovered.Count);

        foreach (var p in discovered)
        {
            if (ct.IsCancellationRequested) break;
            try
            {
                if (_previous.TryGetValue(p.Id, out var prev))
                {
                    // PID reuse guard — if start time changed, treat as new process
                    DateTime startTime;
                    try { startTime = p.StartTime; }
                    catch { startTime = prev.StartTime; }

                    if (startTime != prev.StartTime)
                    {
                        _previous.TryRemove(p.Id, out _);
                        SeedProcess(p);
                        continue;
                    }

                    var deltaWall = (now - prev.Time).TotalMilliseconds;
                    var deltaCpu  = p.TotalProcessorTime.TotalMilliseconds - prev.PrevCpuMs;

                    var cpu = deltaWall > 0
                        ? Math.Clamp(deltaCpu / Environment.ProcessorCount / deltaWall * 100.0, 0.0, 100.0)
                        : 0.0;

                    var (diskRead, diskWrite) = opts.EnableDisk
                        ? ReadProcIo(p.Id)
                        : (prev.DiskReadBytes, prev.DiskWriteBytes);

                    results.Add(new ProcessMetrics(
                        Pid:            p.Id,
                        Name:           p.ProcessName,
                        CpuPercent:     Math.Round(cpu, 1),
                        RamMb:          p.WorkingSet64 / 1024 / 1024,
                        DiskReadBytes:  diskRead,
                        DiskWriteBytes: diskWrite,
                        NetRecvBytes:   prev.NetRecvBytes,
                        NetSentBytes:   prev.NetSentBytes,
                        Timestamp:      now));

                    prev.PrevCpuMs      = p.TotalProcessorTime.TotalMilliseconds;
                    prev.RamBytes       = p.WorkingSet64;
                    prev.DiskReadBytes  = diskRead;
                    prev.DiskWriteBytes = diskWrite;
                    prev.Time           = now;
                }
                else
                {
                    SeedProcess(p);
                }
            }
            catch { /* process exited between discovery and metrics read */ }
        }

        return [.. results];
    }

    private static List<Process> DiscoverByFilter(string[] nameFilter)
    {
        if (nameFilter.Length == 0) return [];

        return Process.GetProcesses()
            .Where(p => nameFilter.Any(f =>
                p.ProcessName.Contains(f, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private static Process? SafeGetProcess(int pid)
    {
        try { return Process.GetProcessById(pid); }
        catch { return null; }
    }

    private void SeedProcess(Process p)
    {
        DateTime startTime;
        try { startTime = p.StartTime; }
        catch { startTime = DateTime.MinValue; }

        _previous[p.Id] = new ProcessMetricsSnapshot
        {
            Pid       = p.Id,
            Name      = p.ProcessName,
            PrevCpuMs = p.TotalProcessorTime.TotalMilliseconds,
            StartTime = startTime,
            RamBytes  = p.WorkingSet64,
            Time      = DateTimeOffset.UtcNow
        };
    }

    private static (long Read, long Write) ReadProcIo(int pid)
    {
        // Linux: /proc/{pid}/io  (no-op on Windows — returns zeros)
        try
        {
            var path = $"/proc/{pid}/io";
            if (!File.Exists(path)) return (0, 0);

            long read = 0, write = 0;
            foreach (var line in File.ReadLines(path))
            {
                if (line.StartsWith("read_bytes:", StringComparison.Ordinal))
                    read = long.Parse(line.AsSpan("read_bytes:".Length).Trim());
                else if (line.StartsWith("write_bytes:", StringComparison.Ordinal))
                    write = long.Parse(line.AsSpan("write_bytes:".Length).Trim());
            }
            return (read, write);
        }
        catch { return (0, 0); }
    }

    private void RaiseExited(int pid)
    {
        try { OnProcessExited?.Invoke(pid); }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "OnProcessExited handler threw for PID {Pid}.", pid);
        }
    }
}
