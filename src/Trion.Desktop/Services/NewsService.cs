using System.Net.Http;
using System.Net.Http.Json;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class NewsService : INewsService
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(10) };

    public async Task<IReadOnlyList<NewsItem>> GetLatestAsync(int count = 5, CancellationToken ct = default)
    {
        try
        {
            var url   = $"{AppLinks.ApiBaseUrl}{ApiEndpoints.News}?limit={count}";
            var items = await _http.GetFromJsonAsync<List<NewsItem>>(url, ct);
            return items ?? [];
        }
        catch
        {
            return [];
        }
    }
}
