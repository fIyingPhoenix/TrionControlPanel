using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class ApiKeyService : IApiKeyService
{
    private readonly ISettingsService _settings;
    private readonly HttpClient       _http = new() { Timeout = TimeSpan.FromSeconds(15) };

    public string? GuestKey   { get; private set; }
    public string? UserApiKey { get; set; }
    public string  ActiveKey  => UserApiKey ?? GuestKey ?? string.Empty;

    public ApiKeyService(ISettingsService settings)
    {
        _settings = settings;
    }

    public async Task InitialiseAsync(CancellationToken ct = default)
    {
        var cfg = _settings.Current;

        // Return early if we already have a cached guest key
        if (!string.IsNullOrEmpty(cfg.GuestKey))
        {
            GuestKey = cfg.GuestKey;
            return;
        }

        // First run — generate a stable install UUID and register with the API
        if (string.IsNullOrEmpty(cfg.InstallUuid))
        {
            cfg.InstallUuid = Guid.NewGuid().ToString();
            _settings.Save();
        }

        try
        {
            var url     = $"{AppLinks.ApiBaseUrl}{ApiEndpoints.GuestKey}";
            var payload = new { uuid = cfg.InstallUuid, app_version = GetAppVersion() };
            var resp    = await _http.PostAsJsonAsync(url, payload, ct);

            if (resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadFromJsonAsync<GuestKeyResponse>(cancellationToken: ct);
                if (body is not null && !string.IsNullOrEmpty(body.Key))
                {
                    GuestKey     = body.Key;
                    cfg.GuestKey = body.Key;
                    _settings.Save();
                }
            }
        }
        catch
        {
            // Guest key is optional — app still works without one
        }
    }

    private static string GetAppVersion()
        => System.Reflection.Assembly.GetExecutingAssembly()
               .GetName().Version?.ToString(3) ?? "unknown";

    private sealed record GuestKeyResponse(
        [property: JsonPropertyName("guest_key")] string Key);
}
