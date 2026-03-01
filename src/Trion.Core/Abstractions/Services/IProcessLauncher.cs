namespace Trion.Core.Abstractions.Services;

public interface IProcessLauncher
{
    Task<LaunchResult> LaunchAsync(LaunchRequest request, CancellationToken ct = default);
}
