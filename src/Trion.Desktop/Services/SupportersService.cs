using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class SupportersService : ISupportersService
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(10) };

    public async Task<IReadOnlyList<SupporterEntry>> GetSupportersAsync(CancellationToken ct = default)
    {
        try
        {
            var url  = ApiEndpoints.SupportersUrl(AppLinks.ApiBaseUrl);
            var resp = await _http.GetFromJsonAsync<SupporterListResponse>(url, ct);
            return resp?.Supporters ?? [];
        }
        catch
        {
            return [];
        }
    }

    // ── API response wrapper ──────────────────────────────────────────────────

    private sealed record SupporterListResponse(
        [property: JsonPropertyName("total")]      int                  Total,
        [property: JsonPropertyName("supporters")] List<SupporterEntry> Supporters);
}
