namespace Trion.Desktop.Services;

/// <summary>Outcome of a single DDNS update attempt.</summary>
public record DdnsResult(bool Success, string Message);

public interface IDdnsUpdateService
{
    /// <summary>
    /// Sends an IP-address update to the chosen DDNS provider.
    /// For Cloudflare: <paramref name="username"/> = Zone ID,
    /// <paramref name="password"/> = API Token (Bearer).
    /// </summary>
    Task<DdnsResult> UpdateAsync(
        string            service,
        string            domain,
        string            username,
        string            password,
        string            ip,
        CancellationToken ct = default);
}
