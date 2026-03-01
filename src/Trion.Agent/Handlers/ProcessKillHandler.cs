using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Trion.Core.Agent;

namespace Trion.Agent.Handlers;

/// <summary>
/// Handles <c>kill_process</c> and <c>is_process_alive</c> agent commands.
/// PID-reuse guard: the caller must supply the expected process start time;
/// if the actual start time differs the operation is rejected.
/// </summary>
public sealed class ProcessKillHandler
{
    private readonly ILogger<ProcessKillHandler> _logger;

    public ProcessKillHandler(ILogger<ProcessKillHandler> logger)
    {
        _logger = logger;
    }

    // ── Kill ─────────────────────────────────────────────────────────────────

    public Task<KillProcessResponse> HandleAsync(KillProcessCommand cmd, CancellationToken ct = default)
    {
        Process? process;
        try
        {
            process = Process.GetProcessById(cmd.Pid);
        }
        catch (ArgumentException)
        {
            return Task.FromResult(new KillProcessResponse(
                Success: false, ErrorMessage: $"No process with PID {cmd.Pid}"));
        }

        using (process)
        {
            // PID reuse guard: verify start time matches
            DateTimeOffset actualStart;
            try
            {
                actualStart = new DateTimeOffset(process.StartTime.ToUniversalTime(), TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Cannot read StartTime for PID {Pid}", cmd.Pid);
                return Task.FromResult(new KillProcessResponse(
                    Success: false, ErrorMessage: "Cannot verify process start time"));
            }

            // Allow up to 1 second tolerance for timestamp rounding across serialisation
            var diff = Math.Abs((actualStart - cmd.ExpectedStartTime).TotalSeconds);
            if (diff > 1.0)
            {
                _logger.LogWarning(
                    "PID {Pid} start-time mismatch: expected {Expected}, actual {Actual}",
                    cmd.Pid, cmd.ExpectedStartTime, actualStart);
                return Task.FromResult(new KillProcessResponse(
                    Success: false, ErrorMessage: "PID start time mismatch — possible PID reuse"));
            }

            try
            {
                process.Kill(entireProcessTree: true);
                _logger.LogInformation("Killed PID {Pid}", cmd.Pid);
                return Task.FromResult(new KillProcessResponse(Success: true, ErrorMessage: null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to kill PID {Pid}", cmd.Pid);
                return Task.FromResult(new KillProcessResponse(Success: false, ErrorMessage: ex.Message));
            }
        }
    }

    // ── Is-alive probe ────────────────────────────────────────────────────────

    public Task<IsProcessAliveResponse> IsAliveAsync(IsProcessAliveCommand cmd, CancellationToken ct = default)
    {
        Process? process;
        try
        {
            process = Process.GetProcessById(cmd.Pid);
        }
        catch (ArgumentException)
        {
            return Task.FromResult(new IsProcessAliveResponse(Alive: false));
        }

        using (process)
        {
            if (process.HasExited)
                return Task.FromResult(new IsProcessAliveResponse(Alive: false));

            DateTimeOffset actualStart;
            try
            {
                actualStart = new DateTimeOffset(process.StartTime.ToUniversalTime(), TimeSpan.Zero);
            }
            catch
            {
                return Task.FromResult(new IsProcessAliveResponse(Alive: false));
            }

            var diff = Math.Abs((actualStart - cmd.ExpectedStartTime).TotalSeconds);
            return Task.FromResult(new IsProcessAliveResponse(Alive: diff <= 1.0));
        }
    }
}
