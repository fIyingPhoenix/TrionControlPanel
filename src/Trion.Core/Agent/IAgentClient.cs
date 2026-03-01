using Trion.Core.Abstractions.Services;

namespace Trion.Core.Agent;

public interface IAgentClient
{
    Task<LaunchResult> LaunchProcessAsync(
        LaunchRequest request,
        string? pnToken = null,
        CancellationToken ct = default);

    Task<bool> KillProcessAsync(int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default);

    Task<ServiceStatus> GetServiceStatusAsync(string serviceName, CancellationToken ct = default);
    Task<bool> StartServiceAsync(string serviceName, CancellationToken ct = default);
    Task<bool> StopServiceAsync(string serviceName, CancellationToken ct = default);
    Task<bool> RestartServiceAsync(string serviceName, CancellationToken ct = default);

    Task<bool> IsProcessAliveAsync(int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default);
    Task<bool> PingAsync(CancellationToken ct = default);
}
