namespace Trion.Core.Abstractions.Services;

public enum ServiceState
{
    Unknown,
    Running,
    Stopped,
    Starting,
    Stopping,
    Failed
}

public sealed record ServiceStatus(string ServiceName, ServiceState State, string? ErrorMessage = null);
