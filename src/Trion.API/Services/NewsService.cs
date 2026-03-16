using Trion.API.Data;
using Trion.API.Models;

namespace Trion.API.Services;

public interface INewsService
{
    Task<IReadOnlyList<NewsItem>> GetNewsAsync(int limit);
}

public sealed class NewsService(TrionDbAccess db) : INewsService
{
    public async Task<IReadOnlyList<NewsItem>> GetNewsAsync(int limit)
        => await db.QueryAsync<NewsItem>(TrionSql.GetNews, new { Limit = Math.Clamp(limit, 1, 50) });
}
