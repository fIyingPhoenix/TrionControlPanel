using Trion.Desktop.Infrastructure.Constants;

namespace Trion.Desktop.Models;

/// <summary>
/// All application settings persisted to Settings.db (SQLite).
/// Loaded into memory on startup; flushed back on exit and on explicit user save.
/// Property names match the legacy JSON schema so old Settings.json files remain compatible.
/// </summary>
public class AppSettings
{
    // ── Database server ────────────────────────────────────────────────────────

    public string WorldDatabase      { get; set; } = "wotlk_world";
    public string AuthDatabase       { get; set; } = "wotlk_auth";
    public string CharactersDatabase { get; set; } = "wotlk_characters";
    public string HotfixDatabase     { get; set; } = "";
    public string DBExeLoc           { get; set; } = "";
    public string DBWorkingDir       { get; set; } = "";
    public string DBLocation         { get; set; } = "";
    public string DBServerHost       { get; set; } = "localhost";
    public string DBServerUser       { get; set; } = "phoenix";
    public string DBServerPassword   { get; set; } = "phoenix";
    public string DBServerPort       { get; set; } = "3306";
    public string DBExeName          { get; set; } = "mysqld";
    public bool   DBInstalled        { get; set; }
    /// <summary>
    /// Which auth DB schema to use.
    /// 0 = TrinityCore 3.3.5a (sha_pass_hash, SHA1)
    /// 1 = AzerothCore         (binary salt + verifier, LegacySHA1 SRP6)   [default]
    /// 2 = CMaNGOS / VMaNGOS   (hex v/s columns, CMaNGOS SRP6)
    /// 3 = TrinityCore 4.x / Latest (battlenet_accounts + account, BNet SHA256 hash)
    /// </summary>
    public int    SelectedDatabases  { get; set; } = 1;   // default: AzerothCore

    // ── Installed versions (per expansion) ───────────────────────────────────

    public string CustomInstalledVersion  { get; set; } = "";
    public string ClassicInstalledVersion { get; set; } = "";
    public string TBCInstalledVersion     { get; set; } = "";
    public string WotLKInstalledVersion   { get; set; } = "";
    public string CataInstalledVersion    { get; set; } = "";
    public string MopInstalledVersion     { get; set; } = "";

    // ── Custom Core ───────────────────────────────────────────────────────────

    public string CustomWorkingDirectory { get; set; } = "";
    public string CustomWorldExeLoc      { get; set; } = "";
    public string CustomLogonExeLoc      { get; set; } = "";
    public string CustomLogonExeName     { get; set; } = "worldserver";
    public string CustomWorldExeName     { get; set; } = "authserver";
    public string CustomLogonName        { get; set; } = "Custom Core";
    public string CustomWorldName        { get; set; } = "Custom Core";
    public bool   CustomNames            { get; set; }
    public bool   LaunchCustomCore       { get; set; }
    public bool   CustomInstalled        { get; set; }

    // ── Classic ───────────────────────────────────────────────────────────────

    public string ClassicWorkingDirectory { get; set; } = "";
    public string ClassicWorldExeLoc      { get; set; } = "";
    public string ClassicLogonExeLoc      { get; set; } = "";
    public string ClassicLogonExeName     { get; set; } = "";
    public string ClassicWorldExeName     { get; set; } = "";
    public string ClassicWorldName        { get; set; } = "";
    public string ClassicLogonName        { get; set; } = "";
    public bool   LaunchClassicCore       { get; set; }
    public bool   ClassicInstalled        { get; set; }

    // ── TBC ───────────────────────────────────────────────────────────────────

    public string TBCWorkingDirectory { get; set; } = "";
    public string TBCWorldExeLoc      { get; set; } = "";
    public string TBCLogonExeLoc      { get; set; } = "";
    public string TBCLogonExeName     { get; set; } = "";
    public string TBCWorldExeName     { get; set; } = "";
    public string TBCWorldName        { get; set; } = "";
    public string TBCLogonName        { get; set; } = "";
    public bool   LaunchTBCCore       { get; set; }
    public bool   TBCInstalled        { get; set; }

    // ── WotLK ─────────────────────────────────────────────────────────────────

    public string WotLKWorkingDirectory { get; set; } = "";
    public string WotLKWorldExeLoc      { get; set; } = "";
    public string WotLKLogonExeLoc      { get; set; } = "";
    public string WotLKLogonExeName     { get; set; } = "";
    public string WotLKWorldExeName     { get; set; } = "";
    public string WotLKWorldName        { get; set; } = "";
    public string WotLKLogonName        { get; set; } = "";
    public bool   LaunchWotLKCore       { get; set; }
    public bool   WotLKInstalled        { get; set; }

    // ── Cataclysm ─────────────────────────────────────────────────────────────

    public string CataWorkingDirectory { get; set; } = "";
    public string CataWorldExeLoc      { get; set; } = "";
    public string CataLogonExeLoc      { get; set; } = "";
    public string CataLogonExeName     { get; set; } = "";
    public string CataWorldExeName     { get; set; } = "";
    public string CataWorldName        { get; set; } = "";
    public string CataLogonName        { get; set; } = "";
    public bool   LaunchCataCore       { get; set; }
    public bool   CataInstalled        { get; set; }

    // ── MoP ───────────────────────────────────────────────────────────────────

    public string MopWorkingDirectory { get; set; } = "";
    public string MopWorldExeLoc      { get; set; } = "";
    public string MopLogonExeLoc      { get; set; } = "";
    public string MopLogonExeName     { get; set; } = "";
    public string MopWorldExeName     { get; set; } = "";
    public string MoPWorldName        { get; set; } = "";
    public string MoPLogonName        { get; set; } = "";
    public bool   LaunchMoPCore       { get; set; }
    public bool   MOPInstalled        { get; set; }

    // ── Active selection ──────────────────────────────────────────────────────

    /// <summary>Which expansion tab is active in Single Player. 0=Custom 1=Classic 2=TBC 3=WotLK 4=Cata 5=MoP</summary>
    public int SelectedCore { get; set; }
    /// <summary>Which SPP emulator build is selected. Maps to the SPP enum.</summary>
    public int SelectedSPP  { get; set; } = 3;   // 3 = WotLK

    // ── DDNS ──────────────────────────────────────────────────────────────────

    public string DDNSDomain       { get; set; } = "";
    public string DDNSUsername     { get; set; } = "";
    public string DDNSPassword     { get; set; } = "";
    public int    DDNSInterval     { get; set; } = 300;  // seconds
    public string IPAddress        { get; set; } = "";
    public bool   DDNSRunOnStartup { get; set; }
    /// <summary>DDNS provider. 0=None 1=NoIP 2=DuckDNS 3=Cloudflare</summary>
    public int    DDNSService      { get; set; }
    /// <summary>DDNS provider name as displayed in the WPF UI (e.g. "DuckDNS", "Cloudflare").</summary>
    public string DDNSServiceName  { get; set; } = "DuckDNS";

    // ── Trion preferences ─────────────────────────────────────────────────────

    /// <summary>How often the system metrics sampler fires, in seconds (1–10). Default 2.</summary>
    public int    MetricsIntervalSeconds  { get; set; } = 2;
    /// <summary>Active theme: e.g. "TrionBlue Dark", "Green Light".</summary>
    public string Theme              { get; set; } = "TrionBlue Dark";
    /// <summary>Locale code: "en-US", "pl-PL".</summary>
    public string Language           { get; set; } = "en-US";
    // ── Account / Session ─────────────────────────────────────────────────────

    public int    AccountId        { get; set; }
    public string AccountUsername  { get; set; } = "";
    public string AccountEmail     { get; set; } = "";
    public string AccountToken     { get; set; } = "";
    public string AccountRole      { get; set; } = "user";
    public string AccountApiKey    { get; set; } = "";
    public string AccountApiTier   { get; set; } = "free";
    public bool   AccountIsActive  { get; set; }
    public bool   AccountIsBanned  { get; set; }
    public string AccountLastLogin { get; set; } = "";
    public string AccountCreatedAt { get; set; } = "";
    /// <summary>True when the user explicitly chose "Continue as Guest" at the login prompt.</summary>
    public bool   LoginSkipped     { get; set; }
    /// <summary>When false the account token is wiped on exit so the user must sign in again next launch.</summary>
    public bool   RememberMe       { get; set; } = false;

    // ── Guest key system ──────────────────────────────────────────────────────

    /// <summary>Stable per-installation UUID used to register a guest key. Generated once, never regenerated.</summary>
    public string InstallUuid { get; set; } = "";
    /// <summary>Cached guest API key returned by POST /Trion/guest-key. Empty until first successful registration.</summary>
    public string GuestKey    { get; set; } = "";
    public bool   AutoUpdateCore     { get; set; } = true;
    public bool   AutoUpdateTrion    { get; set; } = true;
    public bool   AutoUpdateDatabase { get; set; } = true;
    public bool   NotificationSound  { get; set; }
    public bool   ConsoleHide        { get; set; } = true;
    public bool   StayInTray         { get; set; }
    public bool   RunWithWindows     { get; set; }
    public bool   RunServerWithWindows  { get; set; }
    public bool   ServerCrashDetection  { get; set; }
    public bool   FirstRun           { get; set; } = true;
    public bool   CreateBnetAccount  { get; set; }

    // ── Window state (WPF only) ───────────────────────────────────────────────

    public double WindowLeft   { get; set; } = double.NaN;
    public double WindowTop    { get; set; } = double.NaN;
    public double WindowWidth  { get; set; } = 1180;
    public double WindowHeight { get; set; } = 740;
}
