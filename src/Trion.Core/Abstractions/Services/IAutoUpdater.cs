namespace Trion.Core.Abstractions.Services;

public interface IAutoUpdater
{
    Task<UpdateCheckResult> CheckForUpdateAsync(CancellationToken ct = default);
    Task ApplyUpdateAsync(UpdateCheckResult update, CancellationToken ct = default);
}
