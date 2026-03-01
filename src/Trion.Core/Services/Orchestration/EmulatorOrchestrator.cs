using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;
using Trion.Core.Logging;

namespace Trion.Core.Services.Orchestration;

/// <summary>
/// Singleton service that owns process lifecycle for all registered emulator profiles.
/// Also runs as a hosted background service for periodic health checks.
/// </summary>
public sealed class EmulatorOrchestrator : BackgroundService, IEmulatorOrchestrator
{
    private static readonly TimeSpan HealthCheckInterval = TimeSpan.FromSeconds(10);

    private readonly IAgentClient _agent;
    private readonly ILogger      _log;

    private readonly ConcurrentDictionary<string, ProfileEntry> _entries = new();

    public EmulatorOrchestrator(IAgentClient agent, TrionLogger trionLogger)
    {
        _agent = agent;
        _log   = trionLogger.CreateLogger(nameof(EmulatorOrchestrator));
    }

    // ── IEmulatorOrchestrator ──────────────────────────────────────────────────

    public async Task StartAsync(EmulatorProfile profile, CancellationToken ct = default)
    {
        var entry = _entries.GetOrAdd(profile.Id, _ => new ProfileEntry(profile));

        await entry.Lock.WaitAsync(ct);
        try
        {
            if (entry.State is ProcessState.Running or ProcessState.Starting)
            {
                _log.LogWarning("Profile {Id} is already {State}; ignoring Start.", profile.Id, entry.State);
                return;
            }

            entry.Profile      = profile;   // refresh in case definition changed
            entry.State        = ProcessState.Starting;
            entry.LastError    = null;
            entry.RestartCount = 0;

            var result = await _agent.LaunchProcessAsync(
                new LaunchRequest(profile.ExecutablePath, [], profile.WorkingDirectory), ct: ct);

            if (result.Success)
            {
                entry.State     = ProcessState.Running;
                entry.Pid       = result.Pid;
                entry.StartedAt = result.StartTime;
                _log.LogInformation("Started profile {Id} (PID {Pid}).", profile.Id, result.Pid);
            }
            else
            {
                entry.State     = ProcessState.Crashed;
                entry.LastError = result.ErrorMessage;
                _log.LogError("Failed to start profile {Id}: {Error}", profile.Id, result.ErrorMessage);
            }
        }
        finally
        {
            entry.Lock.Release();
        }
    }

    public async Task StopAsync(string profileId, CancellationToken ct = default)
    {
        if (!_entries.TryGetValue(profileId, out var entry))
        {
            _log.LogWarning("StopAsync: profile {Id} not found.", profileId);
            return;
        }

        await entry.Lock.WaitAsync(ct);
        try
        {
            if (entry.State is ProcessState.Stopped or ProcessState.Stopping)
                return;

            entry.State = ProcessState.Stopping;

            if (entry.Pid is { } pid && entry.StartedAt is { } startedAt)
            {
                var killed = await _agent.KillProcessAsync(pid, startedAt, ct);
                _log.LogInformation(
                    "Kill PID {Pid} for profile {Id}: {Result}.",
                    pid, profileId, killed ? "ok" : "failed/already exited");
            }

            entry.State     = ProcessState.Stopped;
            entry.Pid       = null;
            entry.StartedAt = null;
        }
        finally
        {
            entry.Lock.Release();
        }
    }

    public async Task RestartAsync(string profileId, CancellationToken ct = default)
    {
        if (!_entries.TryGetValue(profileId, out var entry))
        {
            _log.LogWarning("RestartAsync: profile {Id} not found.", profileId);
            return;
        }

        await StopAsync(profileId, ct);
        await StartAsync(entry.Profile, ct);
    }

    public Task<OrchestratorStatus> GetStatusAsync(string profileId)
    {
        return Task.FromResult(
            _entries.TryGetValue(profileId, out var entry)
                ? entry.ToStatus()
                : new OrchestratorStatus(profileId, ProcessState.Stopped, null, null, 0));
    }

    public IReadOnlyList<OrchestratorStatus> GetAllStatuses() =>
        _entries.Values.Select(e => e.ToStatus()).ToList();

    // ── Background health-check loop ───────────────────────────────────────────

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _log.LogInformation("Emulator orchestrator health-check loop started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(HealthCheckInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            // Snapshot to avoid issues if entries are added concurrently
            foreach (var entry in _entries.Values.ToList())
            {
                if (entry.State != ProcessState.Running)
                    continue;

                bool alive;
                try
                {
                    alive = entry.Pid is { } pid && entry.StartedAt is { } start
                         && await _agent.IsProcessAliveAsync(pid, start, stoppingToken);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "Health check for profile {Id} failed.", entry.Profile.Id);
                    continue;
                }

                if (alive)
                    continue;

                // Process has gone away — decide whether to restart or mark crashed
                bool shouldRestart = false;

                await entry.Lock.WaitAsync(stoppingToken);
                try
                {
                    if (entry.State != ProcessState.Running)
                        continue;   // already being acted upon

                    _log.LogWarning("Profile {Id} process is no longer alive.", entry.Profile.Id);

                    if (entry.Profile.AutoRestart &&
                        entry.RestartCount < entry.Profile.MaxRestartAttempts)
                    {
                        entry.RestartCount++;
                        entry.State   = ProcessState.Restarting;
                        shouldRestart = true;
                        _log.LogInformation(
                            "Scheduling auto-restart for profile {Id} (attempt {N}/{Max}).",
                            entry.Profile.Id, entry.RestartCount, entry.Profile.MaxRestartAttempts);
                    }
                    else
                    {
                        entry.State     = ProcessState.Crashed;
                        entry.Pid       = null;
                        entry.StartedAt = null;
                        entry.LastError = "Process exited unexpectedly.";
                    }
                }
                finally
                {
                    entry.Lock.Release();
                }

                if (shouldRestart)
                {
                    try
                    {
                        // Reset to Stopped so StartAsync can proceed through its guard
                        entry.State = ProcessState.Stopped;
                        await StartAsync(entry.Profile, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _log.LogError(ex, "Auto-restart of profile {Id} failed.", entry.Profile.Id);
                        entry.State     = ProcessState.Crashed;
                        entry.LastError = ex.Message;
                    }
                }
            }
        }

        _log.LogInformation("Emulator orchestrator health-check loop stopped.");
    }

    // ── Private state holder ───────────────────────────────────────────────────

    private sealed class ProfileEntry(EmulatorProfile profile)
    {
        public EmulatorProfile Profile      { get; set; } = profile;
        public ProcessState    State        { get; set; } = ProcessState.Stopped;
        public int?            Pid          { get; set; }
        public DateTimeOffset? StartedAt    { get; set; }
        public int             RestartCount { get; set; }
        public string?         LastError    { get; set; }

        public readonly SemaphoreSlim Lock = new(1, 1);

        public OrchestratorStatus ToStatus() =>
            new(Profile.Id, State, Pid, StartedAt, RestartCount, LastError);
    }
}
