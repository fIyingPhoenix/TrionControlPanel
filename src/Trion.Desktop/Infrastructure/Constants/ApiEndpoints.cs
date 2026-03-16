namespace Trion.Desktop.Infrastructure.Constants;

/// <summary>
/// All Trion / Flying-Phoenix API route paths and URL builders.
/// Paths are relative to <see cref="Models.AppLinks.ApiBaseUrl"/>.
/// Edit here when backend routes change — nothing else needs updating.
///
/// API keys are sent via the X-API-Key request header, NOT in query strings.
/// </summary>
internal static class ApiEndpoints
{
    // ── Route paths (relative) ────────────────────────────────────────────────

    public const string GuestKey      = "/Trion/guest-key";
    public const string GetExternalIp = "/Trion/GetExternalIPv4";
    public const string GetFileVersion = "/Trion/GetFileVersion";
    public const string InstallSpp    = "/Trion/InstallSPP";
    public const string DownloadFile  = "/Trion/DownloadFile";
    public const string RepairSpp     = "/Trion/RepairSPP";
    public const string Updates       = "/Trion/updates";
    public const string Downloads     = "/Trion/downloads";
    public const string AccountLogin  = "/Trion/account/login";
    public const string News          = "/Trion/news";
    public const string Supporters    = "/Trion/supporters";

    // ── URL builders (accept the resolved base URL) ───────────────────────────
    // Keys are NO LONGER embedded in URLs — send X-API-Key header instead.

    public static string GetExternalIpv4Url(string baseUrl) =>
        $"{baseUrl}{GetExternalIp}";

    public static string GetFileVersionUrl(string baseUrl) =>
        $"{baseUrl}{GetFileVersion}";

    public static string InstallSppUrl(string baseUrl, string emulator) =>
        $"{baseUrl}{InstallSpp}?Emulator={Uri.EscapeDataString(emulator)}";

    public static string DownloadFileUrl(string baseUrl, string emulator) =>
        $"{baseUrl}{DownloadFile}?emulator={Uri.EscapeDataString(emulator)}";

    /// <summary>
    /// POST /Trion/DownloadFile — individual file download for repair.
    /// Emulator and filePath go in the JSON body, not the query string.
    /// </summary>
    public static string DownloadSingleFileUrl(string baseUrl) =>
        $"{baseUrl}{DownloadFile}";

    public static string RepairSppUrl(string baseUrl) =>
        $"{baseUrl}{RepairSpp}";

    public static string UpdatesUrl(string baseUrl)   => $"{baseUrl}{Updates}";
    public static string DownloadsUrl(string baseUrl) => $"{baseUrl}{Downloads}";

    /// <summary>MySQL package download URL. Send X-API-Key header to unlock supporter tier.</summary>
    public static string MySqlDownloadUrl(string baseUrl) =>
        $"{baseUrl}{DownloadFile}?emulator=mysql";

    public static string AccountLoginUrl(string baseUrl) => $"{baseUrl}{AccountLogin}";
    public static string SupportersUrl(string baseUrl)   => $"{baseUrl}{Supporters}";
}
