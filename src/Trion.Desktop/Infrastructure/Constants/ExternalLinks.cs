namespace Trion.Desktop.Infrastructure.Constants;

/// <summary>
/// All hard-coded external URLs that are not derived from the configurable API base.
/// Edit here when external services change their addresses.
/// </summary>
internal static class ExternalLinks
{
    // ── Trion / Flying-Phoenix ────────────────────────────────────────────────

    /// <summary>Default API host. Loaded into AppLinks.ApiBaseUrl on startup.</summary>
    public const string ApiDefaultHost = "https://flying-phoenix.dev";

    public const string WebSite  = "https://flying-phoenix.dev/";
    public const string Support  = "https://flying-phoenix.dev/support.php";
    public const string Discord  = "https://discord.gg/By4nkETRXS";
    public const string Donation = "https://github.com/sponsors/TrionControlPanel";

    /// <summary>Dev / local fallback when the main API host is unreachable.</summary>
    public const string ApiBackupHost = "http://localhost:5000";

    // ── Third-party services ──────────────────────────────────────────────────

    /// <summary>Plain-text public IPv4 lookup — returns the raw IP string.</summary>
    public const string PublicIpFallback = "https://api.ipify.org";

    // ── MySQL portable package ────────────────────────────────────────────────
    // Update both MySqlVersion and MySqlCdnDownload together whenever upgrading MySQL.

    /// <summary>MySQL version served by the Trion API and used for the local cache filename.</summary>
    public const string MySqlVersion = "8.4.8";

    /// <summary>
    /// Direct CDN fallback — only used when the Trion API download fails.
    /// Primary download goes through <see cref="ApiEndpoints.MySqlDownloadUrl"/>.
    /// </summary>
    public const string MySqlCdnDownload =
        "https://cdn.mysql.com//Downloads/MySQL-8.4/mysql-8.4.8-winx64.zip";

    // ── Emulator repositories ─────────────────────────────────────────────────

    public static class Emulators
    {
        public const string AzerothCore = "https://github.com/AzerothCore/";
        public const string CMaNGOS     = "https://github.com/cmangos/";
        public const string CypherCore  = "https://github.com/CypherCore/";
        public const string TrinityCore = "https://github.com/TrinityCore/";
        public const string SkyFire     = "https://codeberg.org/ProjectSkyfire/";
        public const string VMaNGOS     = "https://github.com/vmangos/";
    }

    // ── DDNS provider sign-up / management websites ───────────────────────────

    public static class DdnsWebsites
    {
        public const string Afraid    = "https://freedns.afraid.org/";
        public const string AllInkl   = "https://all-inkl.com/";
        public const string Cloudflare = "https://www.cloudflare.com/";
        public const string DuckDns   = "https://www.duckdns.org/";
        public const string DynDns    = "https://account.dyn.com/";
        public const string Dynu      = "https://www.dynu.com/";
        public const string Enom      = "https://www.enom.com/";
        public const string Freemyip  = "https://freemyip.com/";
        public const string NoIp      = "https://www.noip.com/sign-up";
        public const string Ovh       = "https://www.ovhcloud.com/";
        public const string Strato    = "https://www.strato.de/";

        /// <summary>Returns the sign-up/management URL for the given provider name.</summary>
        public static string For(string service) => service switch
        {
            "Afraid"    => Afraid,
            "AllInkl"   => AllInkl,
            "Cloudflare" => Cloudflare,
            "DuckDNS"   => DuckDns,
            "DynDNS"    => DynDns,
            "Dynu"      => Dynu,
            "Enom"      => Enom,
            "Freemyip"  => Freemyip,
            "NoIP"      => NoIp,
            "OVH"       => Ovh,
            "STRATO"    => Strato,
            _           => WebSite,
        };
    }
}
