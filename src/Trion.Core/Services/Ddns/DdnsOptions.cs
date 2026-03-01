namespace Trion.Core.Services.Ddns;

public enum DdnsProvider { None, NoIp, DuckDns, Cloudflare }

public sealed class DdnsOptions
{
    public DdnsProvider Provider    { get; set; } = DdnsProvider.None;
    public string       Hostname    { get; set; } = string.Empty;
    public TimeSpan     PollInterval { get; set; } = TimeSpan.FromMinutes(15);

    // No-IP: Basic auth
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // DuckDNS: token
    public string Token { get; set; } = string.Empty;

    // Cloudflare: Bearer API token + zone/record IDs
    public string ApiToken { get; set; } = string.Empty;
    public string ZoneId   { get; set; } = string.Empty;
    public string RecordId { get; set; } = string.Empty;
}
