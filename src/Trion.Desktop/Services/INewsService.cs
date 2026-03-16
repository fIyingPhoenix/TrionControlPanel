using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface INewsService
{
    Task<IReadOnlyList<NewsItem>> GetLatestAsync(int count = 5, CancellationToken ct = default);
}
