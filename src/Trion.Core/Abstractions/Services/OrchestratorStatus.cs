namespace Trion.Core.Abstractions.Services;

public enum ProcessState { Stopped, Running, Starting, Stopping, Crashed, Restarting }

public sealed record OrchestratorStatus(
    string ProfileId,
    ProcessState State,
    int? Pid,
    DateTimeOffset? StartedAt,
    int RestartCount,
    string? LastError = null);
