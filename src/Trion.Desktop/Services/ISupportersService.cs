using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface ISupportersService
{
    Task<IReadOnlyList<SupporterEntry>> GetSupportersAsync(CancellationToken ct = default);
}
