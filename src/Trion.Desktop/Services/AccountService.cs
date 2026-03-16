using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class AccountService : IAccountService
{
    private readonly ISettingsService _settings;
    private readonly IApiKeyService   _apiKeys;
    private readonly HttpClient       _http = new() { Timeout = TimeSpan.FromSeconds(15) };

    public string?         Username   { get; private set; }
    public string?         Email      { get; private set; }
    public bool            IsGuest    { get; private set; }
    public AccountProfile? Profile    { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrEmpty(_settings.Current.AccountToken);

    public event Action? SessionChanged;

    public AccountService(ISettingsService settings, IApiKeyService apiKeys)
    {
        _settings = settings;
        _apiKeys  = apiKeys;

        var cfg = _settings.Current;

        // Restore full session from persisted settings
        if (!string.IsNullOrEmpty(cfg.AccountToken))
        {
            Username = cfg.AccountUsername;
            Email    = cfg.AccountEmail;
            Profile  = BuildProfileFromSettings(cfg);

            // Ensure the user key is active immediately — before ApiKeyService.InitialiseAsync fires
            if (!string.IsNullOrEmpty(cfg.AccountApiKey))
                _apiKeys.UserApiKey = cfg.AccountApiKey;
        }
        // Restore guest mode
        else if (cfg.LoginSkipped)
        {
            IsGuest = true;
        }
    }

    public async Task<LoginResult> LoginAsync(string username, string password, bool rememberMe = false, CancellationToken ct = default)
    {
        try
        {
            var url     = ApiEndpoints.AccountLoginUrl(AppLinks.ApiBaseUrl);
            var content = JsonContent.Create(new { username, password });
            var resp    = await _http.PostAsync(url, content, ct);

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                return new LoginResult(false, $"Login failed ({(int)resp.StatusCode}).");
            }

            var loginResp = await resp.Content.ReadFromJsonAsync<LoginResponse>(ct);
            if (loginResp is null || string.IsNullOrEmpty(loginResp.Token))
                return new LoginResult(false, "Invalid response from server.");

            // Persist all fields
            var cfg = _settings.Current;
            cfg.AccountToken     = loginResp.Token;
            cfg.AccountId        = loginResp.Id;
            cfg.AccountUsername  = loginResp.Username ?? username;
            cfg.AccountEmail     = loginResp.Email    ?? "";
            cfg.AccountRole      = loginResp.Role      ?? "user";
            cfg.AccountApiKey    = loginResp.ApiKey    ?? "";
            cfg.AccountApiTier   = loginResp.ApiTier   ?? "free";
            cfg.AccountIsActive  = loginResp.IsActive;
            cfg.AccountIsBanned  = loginResp.IsBanned;
            cfg.AccountLastLogin = loginResp.LastLogin ?? "";
            cfg.AccountCreatedAt = loginResp.CreatedAt ?? "";
            cfg.LoginSkipped     = false;
            cfg.RememberMe       = rememberMe;
            _settings.Save();

            Username = cfg.AccountUsername;
            Email    = cfg.AccountEmail;
            IsGuest  = false;
            Profile  = BuildProfileFromSettings(cfg);

            // Promote to supporter-tier API key for all future requests
            _apiKeys.UserApiKey = cfg.AccountApiKey;

            SessionChanged?.Invoke();
            return new LoginResult(true);
        }
        catch (HttpRequestException)
        {
            return new LoginResult(false, "Cannot reach the Trion server. Check your internet connection.");
        }
        catch (TaskCanceledException)
        {
            return new LoginResult(false, "Request timed out.");
        }
    }

    public void Logout()
    {
        var cfg = _settings.Current;
        cfg.AccountToken     = "";
        cfg.AccountId        = 0;
        cfg.AccountUsername  = "";
        cfg.AccountEmail     = "";
        cfg.AccountRole      = "user";
        cfg.AccountApiKey    = "";
        cfg.AccountApiTier   = "free";
        cfg.AccountIsActive  = false;
        cfg.AccountIsBanned  = false;
        cfg.AccountLastLogin = "";
        cfg.AccountCreatedAt = "";
        cfg.LoginSkipped     = true;   // return to guest mode rather than fully signed out
        _settings.Save();

        Username = null;
        Email    = null;
        IsGuest  = true;
        Profile  = null;

        // Fall back to guest key
        _apiKeys.UserApiKey = null;

        SessionChanged?.Invoke();
    }

    public void ContinueAsGuest()
    {
        var cfg = _settings.Current;
        cfg.LoginSkipped = true;
        _settings.Save();

        IsGuest = true;
        SessionChanged?.Invoke();
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static AccountProfile BuildProfileFromSettings(AppSettings cfg) => new()
    {
        Id        = cfg.AccountId,
        Username  = cfg.AccountUsername,
        Email     = cfg.AccountEmail,
        Role      = cfg.AccountRole,
        IsActive  = cfg.AccountIsActive,
        IsBanned  = cfg.AccountIsBanned,
        LastLogin = cfg.AccountLastLogin,
        CreatedAt = cfg.AccountCreatedAt,
        ApiKey    = cfg.AccountApiKey,
        ApiTier   = cfg.AccountApiTier,
    };

    // ── Server response DTO ───────────────────────────────────────────────────

    private sealed record LoginResponse(
        [property: JsonPropertyName("token")]       string  Token,
        [property: JsonPropertyName("id")]          int     Id,
        [property: JsonPropertyName("username")]    string? Username,
        [property: JsonPropertyName("email")]       string? Email,
        [property: JsonPropertyName("role")]        string? Role,
        [property: JsonPropertyName("is_active")]   bool    IsActive,
        [property: JsonPropertyName("is_banned")]   bool    IsBanned,
        [property: JsonPropertyName("last_login")]  string? LastLogin,
        [property: JsonPropertyName("created_at")]  string? CreatedAt,
        [property: JsonPropertyName("api_key")]     string? ApiKey,
        [property: JsonPropertyName("api_tier")]    string? ApiTier
    );
}
