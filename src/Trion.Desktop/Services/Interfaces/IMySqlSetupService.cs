namespace Trion.Desktop.Services;

/// <summary>Live snapshot of a MySQL package download in progress.</summary>
public record DownloadProgress(
    long      DownloadedBytes,
    long      TotalBytes,          // -1 when Content-Length header is absent
    double    SpeedBytesPerSec,
    TimeSpan? Eta,                 // null when total is unknown or speed is zero
    int       Percent);            // 0-100; 0 when total is unknown

public interface IMySqlSetupService
{
    // ── Installation state ────────────────────────────────────────────────────

    /// <summary>True when mysqld.exe and the data directory both exist in {AppDir}/database/.</summary>
    bool IsInstalled { get; }

    // ── Setup operation state ─────────────────────────────────────────────────

    /// <summary>True while RunStartupCheckAsync is executing (download / extract / init).</summary>
    bool IsRunning { get; }

    /// <summary>The most recent progress message (empty string before first run).</summary>
    string LastMessage { get; }

    /// <summary>
    /// The most recent download snapshot while a download is active; null otherwise.
    /// Allows late subscribers to recover current state.
    /// </summary>
    DownloadProgress? CurrentDownload { get; }

    // ── Process state ─────────────────────────────────────────────────────────

    /// <summary>True when the mysqld process is alive.</summary>
    bool IsProcessRunning { get; }

    /// <summary>The OS process ID of the running mysqld, or null if not running.</summary>
    int? ProcessId { get; }

    /// <summary>Wall-clock time when StartAsync launched mysqld, or null.</summary>
    DateTime? StartTime { get; }

    /// <summary>How long the mysqld process has been running. Zero when not running.</summary>
    TimeSpan Uptime { get; }

    // ── Events ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Raised on every progress step and once more when the setup operation finishes
    /// (IsRunning will be false on that final raise).
    /// </summary>
    event Action<string>? ProgressChanged;

    /// <summary>
    /// Raised during the download phase with live speed / ETA / size data.
    /// Raised with <c>null</c> when the download completes or is aborted,
    /// signalling subscribers to hide download-specific UI.
    /// </summary>
    event Action<DownloadProgress?>? DownloadProgressChanged;

    /// <summary>
    /// Raised when mysqld starts (<c>true</c>) or stops / crashes (<c>false</c>).
    /// </summary>
    event Action<bool>? StatusChanged;

    // ── Setup ─────────────────────────────────────────────────────────────────

    /// <summary>
    /// Called once at startup.
    /// • If MySQL is already installed: regenerates my.ini, then starts mysqld.
    /// • If not installed: downloads the portable ZIP, extracts it, initialises the
    ///   data directory, creates the 'phoenix' DB user, then starts mysqld.
    /// </summary>
    Task RunStartupCheckAsync(CancellationToken ct = default);

    /// <summary>
    /// Writes (or overwrites) my.ini next to the exe with settings tuned to the
    /// current machine's RAM and CPU core count.
    /// </summary>
    void GenerateMyIni();

    // ── Lifecycle ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Regenerates my.ini and starts the mysqld process.
    /// No-op if mysqld is already running.
    /// Pass <c>null</c> (default) to use the <c>ConsoleHide</c> value from application settings.
    /// </summary>
    Task StartAsync(bool? hideConsole = null, CancellationToken ct = default);

    /// <summary>
    /// Gracefully stops mysqld via Ctrl+C signal (same as the old Desktop app);
    /// force-kills after 15 s if mysqld does not respond.
    /// No-op if mysqld is not running.
    /// </summary>
    Task StopAsync(CancellationToken ct = default);

    /// <summary>
    /// Regenerates my.ini for the current hardware and restarts mysqld.
    /// Useful when the machine's RAM/CPU changes or the config is corrupted.
    /// </summary>
    Task RepairAsync(CancellationToken ct = default);

    /// <summary>
    /// Stops mysqld, deletes the entire database/ folder and my.ini,
    /// and clears the installed flag in settings.
    /// </summary>
    Task UninstallAsync(CancellationToken ct = default);

    /// <summary>
    /// Runs UninstallAsync then a full RunStartupCheckAsync (fresh download + init).
    /// </summary>
    Task ReinstallAsync(CancellationToken ct = default);
}
