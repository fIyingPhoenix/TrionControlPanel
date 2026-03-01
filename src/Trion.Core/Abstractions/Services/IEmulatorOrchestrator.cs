namespace Trion.Core.Abstractions.Services;

public interface IEmulatorOrchestrator
{
    Task StartAsync(EmulatorProfile profile, CancellationToken ct = default);
    Task StopAsync(string profileId, CancellationToken ct = default);
    Task RestartAsync(string profileId, CancellationToken ct = default);
    Task<OrchestratorStatus> GetStatusAsync(string profileId);
    IReadOnlyList<OrchestratorStatus> GetAllStatuses();
}
