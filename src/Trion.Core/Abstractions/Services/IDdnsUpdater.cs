namespace Trion.Core.Abstractions.Services;

public interface IDdnsUpdater
{
    Task UpdateNowAsync(CancellationToken ct = default);
    Task<DdnsUpdateResult?> GetLastResultAsync();
}
