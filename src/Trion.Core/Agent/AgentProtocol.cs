using Trion.Core.Abstractions.Services;

namespace Trion.Core.Agent;

// ── Base command wrapper ──────────────────────────────────────────────────────

public sealed record AgentCommand(
    string CommandType,
    string Payload,       // JSON-serialized command body
    string Hmac,          // HMAC-SHA256 of (CommandType + Payload)
    string? PnToken = null);

public sealed record AgentResponse(
    bool Success,
    string? Payload,      // JSON-serialized response body (null on failure)
    string? Error = null);

// ── Command bodies ────────────────────────────────────────────────────────────

public sealed record LaunchProcessCommand(
    string ExecutablePath,
    string[] Arguments,
    string? WorkingDirectory);

public sealed record LaunchProcessResponse(
    bool Success,
    int? Pid,
    DateTimeOffset? StartTime,
    string? ErrorMessage);

public sealed record KillProcessCommand(int Pid, DateTimeOffset ExpectedStartTime);

public sealed record KillProcessResponse(bool Success, string? ErrorMessage);

public sealed record ServiceControlCommand(string ServiceName, string Action); // start|stop|restart|status

public sealed record ServiceControlResponse(
    bool Success,
    string PreviousState,
    string CurrentState,
    string? ErrorMessage);

public sealed record IsProcessAliveCommand(int Pid, DateTimeOffset ExpectedStartTime);

public sealed record IsProcessAliveResponse(bool Alive);

public sealed record PingCommand();

public sealed record PingResponse(DateTimeOffset ServerTime);

// ── Command type constants ────────────────────────────────────────────────────

public static class AgentCommandType
{
    public const string LaunchProcess    = "launch_process";
    public const string KillProcess      = "kill_process";
    public const string ServiceControl   = "service_control";
    public const string IsProcessAlive   = "is_process_alive";
    public const string Ping             = "ping";
}
