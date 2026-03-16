namespace Trion.Desktop.Infrastructure.Constants;

/// <summary>
/// URL builders for every supported Dynamic DNS provider.
/// Edit this file whenever a provider changes its update endpoint.
/// All raw strings are URL-encoded internally — pass plain values.
/// </summary>
internal static class DdnsProviderUrls
{
    // ── Cloudflare (REST API — not GET-based) ─────────────────────────────────
    // username = Zone ID  |  password = API Token (Bearer auth)

    private const string CloudflareApi = "https://api.cloudflare.com/client/v4";

    public static string CloudflareListRecords(string zoneId, string domain) =>
        $"{CloudflareApi}/zones/{zoneId}/dns_records?type=A&name={Uri.EscapeDataString(domain)}";

    public static string CloudflareUpdateRecord(string zoneId, string recordId) =>
        $"{CloudflareApi}/zones/{zoneId}/dns_records/{recordId}";

    // ── GET-based providers ───────────────────────────────────────────────────

    /// <summary>Afraid.org (FreeDNS) — username field carries the per-domain update hash.</summary>
    public static string Afraid(string updateHash) =>
        $"https://sync.afraid.org/u/{Uri.EscapeDataString(updateHash)}/";

    /// <summary>All-Inkl — German hosting provider (hostname inferred server-side).</summary>
    public static string AllInkl(string username, string password, string ip) =>
        $"https://{E(username)}:{E(password)}@dyndns.kasserver.com/?myip={E(ip)}";

    /// <summary>DuckDNS — token-based, free service. Pass subdomain without ".duckdns.org".</summary>
    public static string DuckDns(string domain, string token, string ip) =>
        $"https://www.duckdns.org/update?domains={E(domain)}&token={E(token)}&ip={E(ip)}";

    /// <summary>DynDNS — original dynamic DNS service.</summary>
    public static string DynDns(string username, string password, string domain, string ip) =>
        $"https://{E(username)}:{E(password)}@update.dyndns.it/nic/update?hostname={E(domain)}&myip={E(ip)}";

    /// <summary>Dynu — DynDNS-compatible protocol.</summary>
    public static string Dynu(string username, string password, string domain, string ip) =>
        $"https://{E(username)}:{E(password)}@api.dynu.com/nic/update?hostname={E(domain)}&myip={E(ip)}";

    /// <summary>Enom — domain registrar custom API. Zone = main domain, HostName = subdomain.</summary>
    public static string Enom(string domain, string zone, string password, string ip) =>
        $"https://dynamic.name-services.com/interface.asp?command=SetDnsHost" +
        $"&HostName={E(domain)}&Zone={E(zone)}&DomainPassword={E(password)}&Address={E(ip)}";

    /// <summary>Freemyip — token-based, free service. Token passed as "username".</summary>
    public static string Freemyip(string domain, string token, string ip) =>
        $"https://freemyip.com/update?domain={E(domain)}&token={E(token)}&myip={E(ip)}";

    /// <summary>NoIP — popular paid/free service, HTTP Basic Auth.</summary>
    public static string NoIp(string username, string password, string domain, string ip) =>
        $"https://{E(username)}:{E(password)}@dynupdate.no-ip.com/nic/update?hostname={E(domain)}&myip={E(ip)}";

    /// <summary>OVH — French hosting provider, standard DynDNS protocol.</summary>
    public static string Ovh(string username, string password, string domain, string ip) =>
        $"https://{E(username)}:{E(password)}@www.ovh.com/nic/update?system=dyndns&hostname={E(domain)}&myip={E(ip)}";

    /// <summary>STRATO — German hosting provider, standard DynDNS protocol.</summary>
    public static string Strato(string username, string password, string domain, string ip) =>
        $"https://{E(username)}:{E(password)}@dyndns.strato.com/nic/update?hostname={E(domain)}&myip={E(ip)}";

    // ── Helper ────────────────────────────────────────────────────────────────

    private static string E(string value) => Uri.EscapeDataString(value);
}
