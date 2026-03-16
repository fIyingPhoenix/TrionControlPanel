using Trion.Desktop.Infrastructure.Constants;

namespace Trion.Desktop.Models;

/// <summary>
/// Runtime URL registry — all API URLs are derived from <see cref="ExternalLinks.ApiDefaultHost"/>.
///
/// URL paths and external addresses live in:
///   <see cref="ApiEndpoints"/>   — Trion / Phoenix API routes
///   <see cref="ExternalLinks"/>  — Static websites, emulator repos, DDNS sign-up pages
///
/// API keys are sent via X-API-Key header — never in URLs.
/// </summary>
public static class AppLinks
{
    // ── API base — fixed to ExternalLinks.ApiDefaultHost ─────────────────────

    internal static readonly string ApiBaseUrl = ExternalLinks.ApiDefaultHost;

    // ── Derived API URLs (resolved against ApiBaseUrl) ────────────────────────

    public static string UpdatesUrl         => ApiEndpoints.UpdatesUrl(ApiBaseUrl);
    public static string DownloadsUrl       => ApiEndpoints.DownloadsUrl(ApiBaseUrl);
    public static string GetExternalIpv4Url => ApiEndpoints.GetExternalIpv4Url(ApiBaseUrl);

    // ── API request builders ──────────────────────────────────────────────────

    public static string GetFileVersionUrl =>
        ApiEndpoints.GetFileVersionUrl(ApiBaseUrl);

    public static string InstallSppUrl(string emulator) =>
        ApiEndpoints.InstallSppUrl(ApiBaseUrl, emulator);

    public static string DownloadFileUrl(string emulator) =>
        ApiEndpoints.DownloadFileUrl(ApiBaseUrl, emulator);

    /// <summary>
    /// POST /Trion/DownloadFile — used for repair (individual file download).
    /// Send emulator + filePath in the JSON body, not the query string.
    /// </summary>
    public static string DownloadSingleFileUrl =>
        ApiEndpoints.DownloadSingleFileUrl(ApiBaseUrl);

    public static string RepairSppUrl =>
        ApiEndpoints.RepairSppUrl(ApiBaseUrl);

    /// <summary>
    /// URL to download the portable MySQL package through the Trion API.
    /// Falls back to <see cref="ExternalLinks.MySqlCdnDownload"/> if this fails.
    /// Send X-API-Key header to unlock the supporter-tier download.
    /// </summary>
    public static string MySqlApiDownloadUrl =>
        ApiEndpoints.MySqlDownloadUrl(ApiBaseUrl);

    // ── Static external URLs (convenience aliases to ExternalLinks) ───────────

    public const string DonationUrl = ExternalLinks.Donation;
    public const string DiscordUrl  = ExternalLinks.Discord;
    public const string SupportUrl  = ExternalLinks.Support;
    public const string PublicIpUrl = ExternalLinks.PublicIpFallback;
}
