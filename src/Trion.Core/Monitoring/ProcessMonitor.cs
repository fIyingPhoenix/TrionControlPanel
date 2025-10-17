using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

#if WINDOWS
using System.Diagnostics.PerformanceCounter;
#endif

namespace Trion.Core.Monitoring;

public sealed class ProcessMonitor : BackgroundService
{
    private readonly ProcessMonitorOptions _opts;
    private readonly IMachineMetricsProvider _machine;
    private readonly ConcurrentDictionary<int, ProcessMetricsSnapshot> _previous = new();

    public event Action<IReadOnlyList<ProcessMetrics>>? OnSnapshot;
    public event Action<MachineMetrics>? OnMachineSnapshot;

    public ProcessMonitor(IOptions<ProcessMonitorOptions> opts,
                          IMachineMetricsProvider? machine = null)
    {
        _opts = opts.Value;
        _machine = machine ?? new MachineMetricsProvider();
    }

    /* ------------ public API ------------ */
    public void TrackProcess(int pid) => SeedPid(pid);

    /* ------------ background loop ------------ */
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var sw = Stopwatch.StartNew();
            var procs = Poll();
            var machine = _machine.GetSnapshot();
            OnSnapshot?.Invoke(procs);
            OnMachineSnapshot?.Invoke(machine);
            var delay = _opts.RefreshInterval - sw.Elapsed;
            if (delay > TimeSpan.Zero)
                await Task.Delay(delay, stoppingToken);
        }
    }

    /* ------------ single poll ------------ */
    private IReadOnlyList<ProcessMetrics> Poll()
    {
        var now = DateTimeOffset.UtcNow;

        /* 1. explicit PIDs (TrackProcess) */
        foreach (var pid in _opts.ExplicitPids)
            SeedPid(pid);

        /* 2. name-filter discovery */
        var alive = DiscoverAliveProcesses(now);

        /* 3. remove dead */
        foreach (var dead in _previous.Keys.Except(alive.Select(p => p.Id)
                                                        .Union(_opts.ExplicitPids)))
            _previous.TryRemove(dead, out _);

        /* 4. build metrics */
        var list = new List<ProcessMetrics>(alive.Count + _opts.ExplicitPids.Length);
        foreach (var p in alive.Union(_opts.ExplicitPids
                                        .Select(pid => SafeGetProcess(pid))
                                        .Where(p => p != null))!)
        {
            try
            {
                if (_previous.TryGetValue(p!.Id, out var prev))
                {
                    var deltaMs = (now - prev.Time).TotalMilliseconds;
                    var cpu = deltaMs > 0
                        ? (p.TotalProcessorTime - TimeSpan.FromMilliseconds(prev.CpuPercent)).TotalMilliseconds
                          / Environment.ProcessorCount / deltaMs * 100d
                        : 0d;
                    if (cpu < 0) cpu = 0; if (cpu > 100) cpu = 100;

                    var ram = p.WorkingSet64;
                    var (read, write) = _opts.EnableDisk ? GetDiskBytes(p.Id) : (prev.DiskReadBytes, prev.DiskWriteBytes);
                    var (recv, sent) = _opts.EnableNetwork ? GetNetworkBytes(p.Id) : (prev.NetRecvBytes, prev.NetSentBytes);

                    list.Add(new ProcessMetrics(p.Id, p.ProcessName, cpu, ram / 1024 / 1024,
                                                read, write, recv, sent, now));

                    prev.CpuPercent = p.TotalProcessorTime.TotalMilliseconds;
                    prev.RamBytes = ram;
                    prev.DiskReadBytes = read;
                    prev.DiskWriteBytes = write;
                    prev.NetRecvBytes = recv;
                    prev.NetSentBytes = sent;
                    prev.Time = now;
                }
                else
                {
                    SeedProcess(p);
                }
            }
            catch { /* gone */ }
        }
        return list;
    }

    /* ------------ helpers ------------ */
    private IReadOnlyList<Process> DiscoverAliveProcesses(DateTimeOffset now)
    {
        if (!_opts.ProcessNameFilter.Any())
            return Process.GetProcesses();

        return Process.GetProcesses()
            .Where(pr => _opts.ProcessNameFilter.Any(f =>
                pr.ProcessName.Contains(f, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private static Process? SafeGetProcess(int pid)
    {
        try { return Process.GetProcessById(pid); }
        catch { return null; }
    }

    private void SeedPid(int pid)
    {
        if (_previous.ContainsKey(pid)) return;
        var p = SafeGetProcess(pid);
        if (p != null) SeedProcess(p);
    }

    private void SeedProcess(Process p)
    {
        _previous[p.Id] = new ProcessMetricsSnapshot
        {
            Pid = p.Id,
            Name = p.ProcessName,
            CpuPercent = p.TotalProcessorTime.TotalMilliseconds,
            RamBytes = p.WorkingSet64,
            DiskReadBytes = 0,
            DiskWriteBytes = 0,
            NetRecvBytes = 0,
            NetSentBytes = 0,
            Time = DateTimeOffset.UtcNow
        };
    }

    private static (long read, long write) GetDiskBytes(int pid)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return WindowsIO.GetDiskBytes(pid);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return LinuxIO.GetDiskBytes(pid);
        return (0, 0);
    }

    private static (long recv, long sent) GetNetworkBytes(int pid)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return WindowsIO.GetNetworkBytes(pid);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return LinuxIO.GetNetworkBytes(pid);
        return (0, 0);
    }
}

/* ------------- platform IO stubs ------------- */
file static class WindowsIO
{
    public static (long read, long write) GetDiskBytes(int pid)
    {
#if WINDOWS
        var name = GetInstanceName(pid);
        using var r = new PerformanceCounter("Process", "IO Read Bytes/sec", name);
        using var w = new PerformanceCounter("Process", "IO Write Bytes/sec", name);
        return ((long)r.RawValue, (long)w.RawValue);
#else
        return (0, 0);
#endif
    }

    public static (long recv, long sent) GetNetworkBytes(int pid) => (0, 0); // stub

#if WINDOWS
    private static string GetInstanceName(int pid)
    {
        var cat = new PerformanceCounterCategory("Process");
        foreach (var inst in cat.GetInstanceNames())
        {
            using var cnt = new PerformanceCounter("Process", "ID Process", inst, true);
            if ((int)cnt.RawValue == pid) return inst;
        }
        return string.Empty;
    }
#endif
}

file static class LinuxIO
{
    public static (long read, long write) GetDiskBytes(int pid)
    {
        try
        {
            var io = File.ReadAllText($"/proc/{pid}/io");
            long read = 0, write = 0;
            foreach (var line in io.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.StartsWith("read_bytes:")) read = long.Parse(line.Split()[1]);
                if (line.StartsWith("write_bytes:")) write = long.Parse(line.Split()[1]);
            }
            return (read, write);
        }
        catch { return (0, 0); }
    }

    public static (long recv, long sent) GetNetworkBytes(int pid) => (0, 0); // stub
}