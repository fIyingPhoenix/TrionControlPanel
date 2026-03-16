namespace Trion.Desktop.Services;

public interface IApiKeyService
{
    string? GuestKey   { get; }
    string? UserApiKey { get; set; }

    /// <summary>
    /// Best available key: user API key if logged in, guest key otherwise.
    /// Empty string when neither is available.
    /// </summary>
    string ActiveKey { get; }

    /// <summary>
    /// Loads the cached guest key or registers a new installation with the API.
    /// Safe to call on every startup.
    /// </summary>
    Task InitialiseAsync(CancellationToken ct = default);
}
