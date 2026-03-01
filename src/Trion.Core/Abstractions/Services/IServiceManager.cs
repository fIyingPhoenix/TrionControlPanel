namespace Trion.Core.Abstractions.Services;

public interface IServiceManager
{
    Task<ServiceStatus> GetStatusAsync(string serviceName, CancellationToken ct = default);
    Task StartAsync(string serviceName, CancellationToken ct = default);
    Task StopAsync(string serviceName, CancellationToken ct = default);
    Task RestartAsync(string serviceName, CancellationToken ct = default);
}
