using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Trion.Core.Logging;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Handles detection, portable installation, lifecycle management, and hardware-tuned
/// configuration of MySQL 8.  All paths are relative to the application's base directory
/// so the whole folder is portable.
/// </summary>
public sealed class MySqlSetupService : IMySqlSetupService, IDisposable
{
    // ── MySQL version + port ───────────────────────────────────────────────────

    private static readonly string MySqlVersion = ExternalLinks.MySqlVersion;
    private const int MySqlPort = 3306;

    // ── Paths (all under AppDir so the installation is portable) ──────────────

    private static readonly string AppDir    = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string DbDir     = Path.Combine(AppDir, "database");
    private static readonly string MyIniPath = Path.Combine(AppDir, "my.ini");
    private static readonly string LogDir    = Path.Combine(AppDir, "logs", "database");
    private static readonly string MysqldExe = Path.Combine(DbDir, "bin", "mysqld.exe");
    private static readonly string MysqlExe  = Path.Combine(DbDir, "bin", "mysql.exe");
    private static readonly string DataDir   = Path.Combine(DbDir, "data");

    // ── HTTP client ────────────────────────────────────────────────────────────

    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromMinutes(15) };

    static MySqlSetupService()
    {
        Http.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) TrionControlPanel/1.0");
    }

    // ── Win32 P/Invoke — graceful Ctrl+C shutdown (same as old Desktop app) ───

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    private static extern bool FreeConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AttachConsole(uint dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate? handler, bool add);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

    private delegate bool ConsoleCtrlDelegate(uint ctrlType);
    private const uint CtrlCEvent = 0;

    // ── Dependencies ──────────────────────────────────────────────────────────

    private readonly ISettingsService                    _settings;
    private readonly INotificationService                _notifications;
    private readonly ILogger<MySqlSetupService>          _logger;
    private readonly IApiKeyService                      _apiKeys;

    // ── Setup operation state ─────────────────────────────────────────────────

    public event Action<string>?            ProgressChanged;
    public event Action<DownloadProgress?>? DownloadProgressChanged;

    public bool              IsRunning       { get; private set; }
    public string            LastMessage     { get; private set; } = string.Empty;
    public DownloadProgress? CurrentDownload { get; private set; }

    // ── Process lifecycle state ────────────────────────────────────────────────

    private volatile int               _processId;
    private          DateTime          _processStartTime;
    private          CancellationTokenSource? _monitorCts;
    private readonly SemaphoreSlim     _startStopLock = new(1, 1);

    private          int               _crashCount;
    private const    int               MaxCrashRestarts = 5;

    public event Action<bool>? StatusChanged;

    public bool IsProcessRunning
    {
        get
        {
            var pid = _processId;
            if (pid <= 0) return false;
            try   { return !Process.GetProcessById(pid).HasExited; }
            catch (ArgumentException) { return false; }
        }
    }

    public int?      ProcessId => _processId > 0 ? _processId         : null;
    public DateTime? StartTime => _processId > 0 ? _processStartTime  : null;
    public TimeSpan  Uptime    => _processId > 0
        ? DateTime.Now - _processStartTime
        : TimeSpan.Zero;

    // ── Constructor ───────────────────────────────────────────────────────────

    public MySqlSetupService(ISettingsService settings, INotificationService notifications,
                             ILogger<MySqlSetupService> logger, IApiKeyService apiKeys)
    {
        _settings      = settings;
        _notifications = notifications;
        _logger        = logger;
        _apiKeys       = apiKeys;
    }

    // ── Installation flags ────────────────────────────────────────────────────

    /// <summary>True when the binary was extracted (mysqld.exe exists).</summary>
    private bool IsBinaryExtracted => File.Exists(MysqldExe);

    /// <summary>True when the data directory was initialised.</summary>
    private bool IsDataInitialized => Directory.Exists(DataDir);

    /// <summary>Fully installed = binary extracted + data directory initialised.</summary>
    public bool IsInstalled => IsBinaryExtracted && IsDataInitialized;

    // ── Startup check ─────────────────────────────────────────────────────────

    public async Task RunStartupCheckAsync(CancellationToken ct = default)
    {
        IsRunning = true;
        try
        {
            if (IsInstalled)
            {
                Report("MySQL detected. Refreshing hardware-tuned configuration...");
                GenerateMyIni();
                Notify("MySQL configuration updated for current hardware.");
                Report("MySQL configuration updated. Starting MySQL...");
                await StartAsync(ct: ct);
                return;
            }

            // ── Download + extract (skip if binary already extracted from a previous
            //    attempt that crashed before initialization completed) ──────────────
            if (IsBinaryExtracted)
            {
                Report("MySQL binaries found — resuming initialization from previous attempt...");
            }
            else
            {
                Notify($"MySQL not found — starting download of portable MySQL {MySqlVersion}...");
                Report($"MySQL not found — downloading portable MySQL {MySqlVersion}...");
                var zipPath = await DownloadMySqlAsync(ct);
                Notify("MySQL download complete. Extracting files...");

                Report("Extracting MySQL files...");
                ExtractMySql(zipPath);
                File.Delete(zipPath);
            }

            Report("Generating hardware-tuned my.ini...");
            Directory.CreateDirectory(LogDir);
            GenerateMyIni();

            Report("Initializing MySQL data directory...");
            await InitializeDataDirectoryAsync(ct);

            // Diagnostic snapshot before starting the daemon
            _logger.LogDebug("my.ini path : {Path}  (exists={Exists})", MyIniPath, File.Exists(MyIniPath));
            _logger.LogDebug("mysqld.exe  : {Path}  (exists={Exists})", MysqldExe, File.Exists(MysqldExe));
            _logger.LogDebug("data dir    : {Path}  (exists={Exists})", DataDir, Directory.Exists(DataDir));
            int dataFileCount = Directory.Exists(DataDir)
                ? Directory.GetFiles(DataDir, "*", SearchOption.AllDirectories).Length : 0;
            _logger.LogDebug("data dir files: {Count}", dataFileCount);

            Report("Starting MySQL for first-time user setup...");
            using var mysqld = StartMysqldProcess(out var stderrCapture);

            Report("Waiting for MySQL to accept connections...");
            await WaitForMysqlAsync(mysqld, stderrCapture, ct);

            Report("Creating databases and 'phoenix' user...");
            await CreatePhoenixUserAsync(ct);

            // Graceful shutdown of the init instance
            await Task.Delay(500, ct);
            try { mysqld.Kill(entireProcessTree: true); } catch { /* already exited */ }
            try { await mysqld.WaitForExitAsync(ct).WaitAsync(TimeSpan.FromSeconds(10), ct); } catch { }

            _settings.Current.DBInstalled  = true;
            _settings.Current.DBLocation   = DbDir;
            _settings.Current.DBWorkingDir = Path.Combine(DbDir, "bin");
            _settings.Current.DBExeLoc     = MysqldExe;
            _settings.Current.DBExeName    = "mysqld";
            _settings.Save();

            Notify("MySQL installed and configured successfully.");
            Report("MySQL installed. Starting MySQL...");
            await StartAsync(ct: ct);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("MySQL setup was cancelled by user or application shutdown.");
            Notify("MySQL setup was cancelled.");
            Report("MySQL setup was cancelled.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, nameof(RunStartupCheckAsync));
            Notify($"MySQL setup failed: {ex.Message}");
            Report($"MySQL setup failed: {ex.Message}");
            throw;
        }
        finally
        {
            IsRunning = false;
            ProgressChanged?.Invoke(LastMessage);
        }
    }

    // ── Process lifecycle ─────────────────────────────────────────────────────

    public async Task StartAsync(bool? hideConsole = null, CancellationToken ct = default)
    {
        if (!IsInstalled)
        {
            Report("MySQL is not installed. Run startup check first.");
            return;
        }

        // null = read from settings (default behaviour)
        bool hide = hideConsole ?? _settings.Current.ConsoleHide;

        await _startStopLock.WaitAsync(ct);
        try
        {
            if (IsProcessRunning)
            {
                Report("MySQL is already running.");
                return;
            }

            GenerateMyIni();

            // --no-monitor: disables MySQL 8's built-in monitor/child-process pattern.
            //   Without it, mysqld spawns a second mysqld as the real server while
            //   the first acts as a watchdog — giving two PIDs, only one of which
            //   does any actual work.  Since we handle crash detection ourselves via
            //   MonitorLoopAsync, the built-in monitor is redundant.
            //
            // --console: only passed when a visible window is requested; the WPF app
            //   has no console handle, so passing it while hidden causes an immediate exit.
            string args = hide
                ? $"--defaults-file=\"{MyIniPath}\" --no-monitor"
                : $"--defaults-file=\"{MyIniPath}\" --no-monitor --console";

            var psi = new ProcessStartInfo
            {
                FileName        = MysqldExe,
                Arguments       = args,
                UseShellExecute = false,
                CreateNoWindow  = hide,
            };

            var proc = Process.Start(psi)
                ?? throw new InvalidOperationException("Failed to start mysqld.");

            _processId        = proc.Id;
            _processStartTime = DateTime.Now;

            Report($"MySQL started (PID: {proc.Id}).");
            Notify($"MySQL server started (PID: {proc.Id}).");
            StatusChanged?.Invoke(true);

            BeginMonitoring();
        }
        finally
        {
            _startStopLock.Release();
        }
    }

    public async Task StopAsync(CancellationToken ct = default)
    {
        await _startStopLock.WaitAsync(ct);
        try
        {
            var pid = _processId;
            if (pid <= 0)
            {
                Report("MySQL is not running.");
                return;
            }

            Report("Stopping MySQL (sending Ctrl+C)...");

            try
            {
                var proc = Process.GetProcessById(pid);
                await SendCtrlC(proc);

                if (!proc.WaitForExit(15_000))
                {
                    Report("MySQL did not respond to Ctrl+C — force killing...");
                    proc.Kill(entireProcessTree: true);
                    try { await proc.WaitForExitAsync(ct).WaitAsync(TimeSpan.FromSeconds(5), ct); }
                    catch { /* timeout on kill is acceptable */ }
                }
            }
            catch (ArgumentException) { /* process already exited — that's fine */ }

            ClearProcessState();
            _crashCount = 0;

            Report("MySQL stopped.");
            Notify("MySQL server stopped.");
            StatusChanged?.Invoke(false);
        }
        finally
        {
            _startStopLock.Release();
        }
    }

    // ── Monitor loop — detects unexpected mysqld exit ─────────────────────────

    private void BeginMonitoring()
    {
        _monitorCts?.Cancel();
        _monitorCts?.Dispose();
        _monitorCts = new CancellationTokenSource();
        var token = _monitorCts.Token;
        _ = Task.Run(() => MonitorLoopAsync(token), token);
    }

    private async Task MonitorLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try { await Task.Delay(3_000, ct); }
            catch (OperationCanceledException) { return; }

            var pid = _processId;
            if (pid <= 0) return;

            bool alive;
            try   { alive = !Process.GetProcessById(pid).HasExited; }
            catch (ArgumentException) { alive = false; }

            if (!alive)
            {
                ClearProcessState();
                _logger.LogWarning("mysqld (PID {Pid}) exited unexpectedly — possible crash.", pid);
                StatusChanged?.Invoke(false);

                if (_settings.Current.ServerCrashDetection)
                {
                    if (_crashCount >= MaxCrashRestarts)
                    {
                        _crashCount = 0;
                        string giveUp = $"MySQL crashed {MaxCrashRestarts} times — auto-restart disabled.";
                        _logger.LogWarning("{Message}", giveUp);
                        Report("MySQL stopped unexpectedly.");
                        Notify(giveUp);
                        return;
                    }

                    _crashCount++;
                    string msg = $"MySQL crashed — restart attempt {_crashCount}/{MaxCrashRestarts}...";
                    _logger.LogInformation("{Message}", msg);
                    Report(msg);
                    Notify($"MySQL stopped unexpectedly. Restarting ({_crashCount}/{MaxCrashRestarts})...");

                    // Brief pause before restart — gives the OS time to release ports
                    await Task.Delay(2_000, CancellationToken.None);

                    try   { await StartAsync(); }
                    catch (Exception ex) { _logger.LogException(ex, "MySqlSetupService crash restart"); }
                }
                else
                {
                    Report("MySQL process exited unexpectedly.");
                    Notify("MySQL server stopped unexpectedly.");
                }

                return;
            }
        }
    }

    private void ClearProcessState()
    {
        _processId        = 0;
        _processStartTime = default;
        _monitorCts?.Cancel();
        _monitorCts?.Dispose();
        _monitorCts = null;
    }

    // ── Graceful Ctrl+C (identical logic to old Desktop AppExecuteManager) ────

    private static async Task SendCtrlC(Process process)
    {
        // 1. Attach our process to the target's console group
        AttachConsole((uint)process.Id);

        // 2. Suppress Ctrl+C in our own process so we don't terminate ourselves
        SetConsoleCtrlHandler(null, add: true);

        // 3. Broadcast Ctrl+C to every process sharing this console
        GenerateConsoleCtrlEvent(CtrlCEvent, 0);

        // 4. Give the signal time to propagate
        await Task.Delay(1_000);

        // 5. Detach and restore our Ctrl+C handler
        FreeConsole();
        SetConsoleCtrlHandler(null, add: false);
    }

    // ── Repair / Uninstall / Reinstall ────────────────────────────────────────

    public async Task RepairAsync(CancellationToken ct = default)
    {
        if (!IsInstalled)
        {
            Report("MySQL is not installed — nothing to repair.");
            return;
        }

        Report("Stopping MySQL for repair...");
        await StopAsync(ct);

        Report("Regenerating hardware-tuned my.ini...");
        GenerateMyIni();
        Notify("MySQL configuration repaired.");

        Report("Restarting MySQL...");
        await StartAsync(ct: ct);
    }

    public async Task UninstallAsync(CancellationToken ct = default)
    {
        if (IsProcessRunning)
        {
            Report("Stopping MySQL before uninstall...");
            await StopAsync(ct);
        }

        Report("Removing MySQL files...");
        if (Directory.Exists(DbDir))
            await Task.Run(() => Directory.Delete(DbDir, recursive: true), ct);

        if (File.Exists(MyIniPath))
            File.Delete(MyIniPath);

        _settings.Current.DBInstalled  = false;
        _settings.Current.DBLocation   = string.Empty;
        _settings.Current.DBWorkingDir = string.Empty;
        _settings.Current.DBExeLoc     = string.Empty;
        _settings.Save();

        Report("MySQL uninstalled.");
        Notify("MySQL has been uninstalled.");
    }

    public async Task ReinstallAsync(CancellationToken ct = default)
    {
        Report("Preparing for reinstall — uninstalling current installation...");
        await UninstallAsync(ct);

        Report("Starting fresh MySQL installation...");
        await RunStartupCheckAsync(ct);
    }

    // ── Dispose ───────────────────────────────────────────────────────────────

    public void Dispose()
    {
        _monitorCts?.Cancel();
        _monitorCts?.Dispose();
        _startStopLock.Dispose();
    }

    // ── Progress helpers ──────────────────────────────────────────────────────

    private void Report(string message)
    {
        LastMessage = message;
        ProgressChanged?.Invoke(message);
        _logger.LogInformation("{Message}", message);
    }

    private void Notify(string message) => _notifications.Add($"[MySQL] {message}");

    // ── my.ini generation ─────────────────────────────────────────────────────

    public void GenerateMyIni()
    {
        // ── Hardware probing ──────────────────────────────────────────────────
        long totalRamMb = GetTotalPhysicalRamMb();
        int  cpuCores   = Environment.ProcessorCount;

        // ── InnoDB buffer pool: 40 % of RAM, 256 MB – 32 GB ─────────────────
        long bufferPoolMb = Math.Clamp(totalRamMb * 40 / 100, 256, 32_768);

        int poolInstances = Math.Clamp((int)(bufferPoolMb / 1024), 1,
                                        Math.Min(cpuCores, 64));
        if (poolInstances < 1) poolInstances = 1;

        long redoLogMb   = Math.Clamp(bufferPoolMb / 4, 256, 8_192);
        long logBufferMb = Math.Clamp(bufferPoolMb / 20, 16, 256);
        long tmpTableMb  = Math.Clamp(totalRamMb * 5 / 100, 64, 2_048);

        int maxConnections  = Math.Clamp(cpuCores * 100,  200, 5_000);
        int threadCacheSize = Math.Clamp(cpuCores * 50,   200, 2_000);
        int readIoThreads   = Math.Clamp(cpuCores / 2,    4,   64);
        int writeIoThreads  = Math.Clamp(cpuCores / 4,    2,   32);
        int pageCleaners    = Math.Clamp(poolInstances,   1,   64);
        int tableOpenCache  = Math.Clamp(cpuCores * 200,  2_000, 65_536);
        int tableDefCache   = Math.Clamp(tableOpenCache / 2, 1_000, 20_000);
        int cacheInstances  = Math.Clamp(cpuCores,         1,   16);
        int lruScanDepth    = Math.Clamp(cpuCores * 256,   512, 4_096);
        int ioCapacity      = Math.Clamp(cpuCores * 200, 1_000, 6_000);
        int ioCapacityMax   = ioCapacity * 2;

        string dbDirFwd  = DbDir.Replace('\\', '/');
        string logDirFwd = LogDir.Replace('\\', '/');

        var ini = new StringBuilder();
        ini.AppendLine($"# MySQL {MySqlVersion} — generated by Trion Control Panel");
        ini.AppendLine($"# System: {totalRamMb:N0} MB RAM  |  {cpuCores} CPU threads");
        ini.AppendLine($"# InnoDB buffer pool: {bufferPoolMb:N0} MB (40 % of RAM)");
        ini.AppendLine();
        ini.AppendLine("[client]");
        ini.AppendLine($"port={MySqlPort}");
        ini.AppendLine("default-character-set=utf8mb4");
        ini.AppendLine();
        ini.AppendLine("[mysqld]");
        ini.AppendLine();
        ini.AppendLine("# ── Paths ────────────────────────────────────────────────────────");
        ini.AppendLine($"""basedir="{dbDirFwd}" """.TrimEnd());
        ini.AppendLine($"""datadir="{dbDirFwd}/data" """.TrimEnd());
        ini.AppendLine();
        ini.AppendLine("# ── Network ──────────────────────────────────────────────────────");
        ini.AppendLine($"port={MySqlPort}");
        ini.AppendLine("bind-address=0.0.0.0");
        ini.AppendLine("max_allowed_packet=512M");
        ini.AppendLine("back_log=1024");
        ini.AppendLine("net_buffer_length=512K");
        ini.AppendLine();
        ini.AppendLine("# ── Character set ────────────────────────────────────────────────");
        ini.AppendLine("character-set-server=utf8mb4");
        ini.AppendLine("collation-server=utf8mb4_0900_ai_ci");
        ini.AppendLine();
        ini.AppendLine("# ── Security ────────────────────────────────────────────────────");
        ini.AppendLine("secure-file-priv=\"\"");
        ini.AppendLine();
        ini.AppendLine("# ── Connections ─────────────────────────────────────────────────");
        ini.AppendLine($"max_connections={maxConnections}");
        ini.AppendLine($"thread_cache_size={threadCacheSize}");
        ini.AppendLine();
        ini.AppendLine($"# ── InnoDB memory ({bufferPoolMb:N0} MB = 40% of {totalRamMb:N0} MB) ──────────────────");
        ini.AppendLine($"innodb_buffer_pool_size={FormatMb(bufferPoolMb)}");
        ini.AppendLine($"innodb_buffer_pool_instances={poolInstances}");
        ini.AppendLine($"innodb_log_buffer_size={FormatMb(logBufferMb)}");
        ini.AppendLine($"innodb_redo_log_capacity={FormatMb(redoLogMb)}");
        ini.AppendLine();
        ini.AppendLine("# ── InnoDB IO ────────────────────────────────────────────────────");
        ini.AppendLine($"innodb_read_io_threads={readIoThreads}");
        ini.AppendLine($"innodb_write_io_threads={writeIoThreads}");
        ini.AppendLine($"innodb_page_cleaners={pageCleaners}");
        ini.AppendLine($"innodb_io_capacity={ioCapacity}");
        ini.AppendLine($"innodb_io_capacity_max={ioCapacityMax}");
        ini.AppendLine("innodb_flush_neighbors=0");
        ini.AppendLine("innodb_flush_log_at_trx_commit=2");
        ini.AppendLine($"innodb_lru_scan_depth={lruScanDepth}");
        ini.AppendLine("innodb_adaptive_hash_index=ON");
        ini.AppendLine();
        ini.AppendLine("# ── Table caches ─────────────────────────────────────────────────");
        ini.AppendLine($"table_open_cache={tableOpenCache}");
        ini.AppendLine($"table_definition_cache={tableDefCache}");
        ini.AppendLine($"table_open_cache_instances={cacheInstances}");
        ini.AppendLine();
        ini.AppendLine("# ── Temporary tables ─────────────────────────────────────────────");
        ini.AppendLine($"tmp_table_size={FormatMb(tmpTableMb)}");
        ini.AppendLine($"max_heap_table_size={FormatMb(tmpTableMb)}");
        ini.AppendLine();
        ini.AppendLine("# ── Per-session buffers (small — multiplied by max_connections) ──");
        ini.AppendLine("join_buffer_size=512K");
        ini.AppendLine("sort_buffer_size=512K");
        ini.AppendLine("read_buffer_size=256K");
        ini.AppendLine("read_rnd_buffer_size=512K");
        ini.AppendLine();
        ini.AppendLine("# ── Storage engine ───────────────────────────────────────────────");
        ini.AppendLine("default-storage-engine=INNODB");
        ini.AppendLine();
        ini.AppendLine("# ── SQL behaviour ────────────────────────────────────────────────");
        ini.AppendLine("sql_mode=STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION");
        ini.AppendLine();
        ini.AppendLine("# ── Logging ──────────────────────────────────────────────────────");
        ini.AppendLine($"log_error=\"{logDirFwd}/mysql_error.log\"");
        ini.AppendLine("slow_query_log=1");
        ini.AppendLine($"slow_query_log_file=\"{logDirFwd}/mysql_slow.log\"");
        ini.AppendLine("long_query_time=2");
        ini.AppendLine("log_queries_not_using_indexes=0");
        ini.AppendLine("general_log=0");
        ini.AppendLine($"general_log_file=\"{logDirFwd}/mysql_general.log\"");
        ini.AppendLine();
        ini.AppendLine("# ── Monitoring ───────────────────────────────────────────────────");
        ini.AppendLine("performance_schema=ON");
        ini.AppendLine();
        ini.AppendLine("[mysqld_safe]");
        ini.AppendLine("open-files-limit=65535");

        // Must use UTF-8 WITHOUT BOM — MySQL's config parser aborts on the 3-byte
        // BOM that Encoding.UTF8 emits, reporting "option without preceding group".
        File.WriteAllText(MyIniPath, ini.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }

    // ── Download ──────────────────────────────────────────────────────────────

    private async Task<string> DownloadMySqlAsync(CancellationToken ct)
    {
        var zipPath = Path.Combine(Path.GetTempPath(), $"mysql-{MySqlVersion}-winx64.zip");

        if (File.Exists(zipPath))
        {
            if (IsValidZip(zipPath))
            {
                Report("Found cached MySQL download, resuming extract...");
                return zipPath;
            }

            Report("Cached download is corrupt — deleting and re-downloading...");
            File.Delete(zipPath);
        }

        // Primary: Trion API (sends X-API-Key header for supporter tier)
        string apiUrl = AppLinks.MySqlApiDownloadUrl;

        Report($"Downloading MySQL {MySqlVersion} via Trion API...");
        bool apiOk = await TryDownloadFromUrlAsync(apiUrl, zipPath, addApiKey: true, ct);

        if (!apiOk)
        {
            // Fallback: direct MySQL CDN — up to 3 attempts in case of mid-transfer stall
            const int maxAttempts = 3;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                if (attempt == 1)
                    Report("Trion API unavailable — falling back to MySQL CDN...");
                else
                    Report($"MySQL CDN download stalled — retry {attempt}/{maxAttempts}...");

                bool cdnOk = await TryDownloadFromUrlAsync(ExternalLinks.MySqlCdnDownload, zipPath, addApiKey: false, ct);
                if (cdnOk) break;

                if (attempt == maxAttempts)
                    throw new InvalidOperationException(
                        $"MySQL download failed after {maxAttempts} attempts. Check your connection and try again.");
            }
        }

        return zipPath;
    }

    private async Task<bool> TryDownloadFromUrlAsync(string url, string destPath, bool addApiKey, CancellationToken ct)
    {
        try
        {
            await DownloadFromUrlAsync(url, destPath, addApiKey, ct);
            return true;
        }
        catch
        {
            if (File.Exists(destPath)) File.Delete(destPath);
            return false;
        }
    }

    private async Task DownloadFromUrlAsync(string url, string destPath, bool addApiKey, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        if (addApiKey)
        {
            var key = _apiKeys.ActiveKey;
            if (!string.IsNullOrEmpty(key))
                req.Headers.TryAddWithoutValidation("X-API-Key", key);
        }
        using var response = await Http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
        response.EnsureSuccessStatusCode();

        long totalBytes = response.Content.Headers.ContentLength ?? -1L;

        var initial = new DownloadProgress(0, totalBytes, 0, null, 0);
        CurrentDownload = initial;
        DownloadProgressChanged?.Invoke(initial);

        await using var networkStream = await response.Content.ReadAsStreamAsync(ct);
        await using var fileStream    = new FileStream(destPath, FileMode.Create,
                                                       FileAccess.Write, FileShare.None,
                                                       bufferSize: 81_920, useAsync: true);
        var  buffer     = new byte[81_920];
        long downloaded = 0L;
        int  read;

        var    sw            = Stopwatch.StartNew();
        long   windowBytes   = 0L;
        long   windowStartMs = 0L;
        double speedBps      = 0;

        // Stall detection: reset to 30 s after every successful chunk
        using var stallCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        stallCts.CancelAfter(TimeSpan.FromSeconds(30));

        while ((read = await networkStream.ReadAsync(buffer, stallCts.Token)) > 0)
        {
            stallCts.CancelAfter(TimeSpan.FromSeconds(30)); // reset stall timer
            await fileStream.WriteAsync(buffer.AsMemory(0, read), ct);
            downloaded  += read;
            windowBytes += read;

            long elapsedMs = sw.ElapsedMilliseconds;
            long windowMs  = elapsedMs - windowStartMs;

            if (windowMs >= 500)
            {
                speedBps      = windowBytes * 1000.0 / windowMs;
                windowBytes   = 0;
                windowStartMs = elapsedMs;

                int       pct = totalBytes > 0 ? (int)(downloaded * 100 / totalBytes) : 0;
                TimeSpan? eta = totalBytes > 0 && speedBps > 0
                    ? TimeSpan.FromSeconds((totalBytes - downloaded) / speedBps)
                    : null;

                var progress = new DownloadProgress(downloaded, totalBytes, speedBps, eta, pct);
                CurrentDownload = progress;
                DownloadProgressChanged?.Invoke(progress);
                Report(BuildDownloadMessage(downloaded, totalBytes, speedBps, eta, pct));
            }
        }

        CurrentDownload = null;
        DownloadProgressChanged?.Invoke(null);
    }

    private string BuildDownloadMessage(
        long downloaded, long total, double speedBps, TimeSpan? eta, int pct)
    {
        string size  = total > 0
            ? $"{FormatBytes(downloaded)} / {FormatBytes(total)} ({pct}%)"
            : FormatBytes(downloaded);
        string speed = $"{FormatBytes((long)speedBps)}/s";
        string time  = eta.HasValue ? $"ETA {eta.Value:mm\\:ss}" : "ETA —";
        return $"Downloading MySQL {MySqlVersion}...  {size}  •  {speed}  •  {time}";
    }

    private static string FormatBytes(long bytes) => bytes switch
    {
        >= 1_073_741_824 => $"{bytes / 1_073_741_824.0:F1} GB",
        >= 1_048_576     => $"{bytes / 1_048_576.0:F1} MB",
        >= 1_024         => $"{bytes / 1_024.0:F0} KB",
        _                => $"{bytes} B",
    };

    // ── Extract ───────────────────────────────────────────────────────────────

    private void ExtractMySql(string zipPath)
    {
        var tempDir = Path.Combine(Path.GetTempPath(), $"mysql_extract_{Guid.NewGuid():N}");

        try
        {
            Report("Extracting archive (this may take a moment)...");
            try
            {
                ZipFile.ExtractToDirectory(zipPath, tempDir, overwriteFiles: true);
            }
            catch (InvalidDataException)
            {
                // ZIP is corrupt — delete it so the next run re-downloads a fresh copy
                try { File.Delete(zipPath); } catch { /* best effort */ }
                throw new InvalidDataException(
                    "The downloaded MySQL archive is corrupt and has been deleted. " +
                    "Please restart the application to re-download.");
            }

            var extractedRoot = Directory.GetDirectories(tempDir).FirstOrDefault()
                ?? throw new DirectoryNotFoundException(
                    "Unexpected MySQL ZIP layout — cannot find root folder.");

            Report("Copying files to database/ folder...");
            Directory.CreateDirectory(DbDir);

            foreach (var dir in Directory.GetDirectories(extractedRoot, "*",
                                                          SearchOption.AllDirectories))
                Directory.CreateDirectory(dir.Replace(extractedRoot, DbDir));

            foreach (var file in Directory.GetFiles(extractedRoot, "*",
                                                     SearchOption.AllDirectories))
                File.Move(file, file.Replace(extractedRoot, DbDir), overwrite: true);
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, recursive: true);
        }
    }

    // ── Initialize data directory ─────────────────────────────────────────────

    private static async Task InitializeDataDirectoryAsync(CancellationToken ct)
    {
        var psi = new ProcessStartInfo
        {
            FileName              = MysqldExe,
            Arguments             = $"--defaults-file=\"{MyIniPath}\" --initialize-insecure",
            RedirectStandardError = true,
            UseShellExecute       = false,
            CreateNoWindow        = true,
        };

        using var proc = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to launch mysqld for initialization.");

        // Read stderr concurrently — prevents pipe-buffer deadlock
        var stderrTask = proc.StandardError.ReadToEndAsync(ct);
        await proc.WaitForExitAsync(ct);
        var stderr = await stderrTask;

        // MySQL exits with code 0 even on config errors (e.g. BOM in my.ini).
        // Always validate that the data directory was actually created.
        if (proc.ExitCode != 0 || !Directory.Exists(DataDir))
        {
            string detail = string.IsNullOrWhiteSpace(stderr)
                ? "(no stderr output — possible config file or DLL issue)"
                : stderr.Trim();
            throw new InvalidOperationException(
                $"mysqld --initialize-insecure failed (exit {proc.ExitCode}, " +
                $"data dir created: {Directory.Exists(DataDir)}):\n{detail}");
        }
    }

    // ── Start mysqld process (internal — for first-run user setup only) ────────

    /// <summary>
    /// Starts mysqld and wires up stderr capture via <c>BeginErrorReadLine</c>.
    /// The returned <paramref name="stderrCapture"/> accumulates lines written to
    /// stderr so we can include them in error messages without a pipe-buffer deadlock
    /// (BeginErrorReadLine drains the pipe asynchronously regardless of read rate).
    /// </summary>
    private static Process StartMysqldProcess(out StringBuilder stderrCapture)
    {
        var capture = new StringBuilder();
        stderrCapture = capture;

        var psi = new ProcessStartInfo
        {
            FileName              = MysqldExe,
            Arguments             = $"--defaults-file=\"{MyIniPath}\" --no-monitor",
            UseShellExecute       = false,
            CreateNoWindow        = true,
            RedirectStandardError = true,  // drained via BeginErrorReadLine — no deadlock
        };

        var proc = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to start mysqld process.");

        proc.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is not null && capture.Length < 16_384)  // cap at 16 KB
                capture.AppendLine(e.Data);
        };
        proc.BeginErrorReadLine();

        return proc;
    }

    // ── Wait for MySQL to accept TCP connections ───────────────────────────────

    private static async Task WaitForMysqlAsync(Process proc, StringBuilder stderrCapture,
                                                CancellationToken ct)
    {
        var deadline = DateTime.UtcNow.AddSeconds(90);

        while (DateTime.UtcNow < deadline)
        {
            ct.ThrowIfCancellationRequested();

            // Fail fast — no point waiting 90 s if mysqld already crashed
            if (proc.HasExited)
            {
                // Give BeginErrorReadLine's async callback a moment to flush
                await Task.Delay(200, CancellationToken.None);

                string stderr = stderrCapture.ToString().Trim();
                string logTail = ReadMySqlErrorLogTail(30);

                throw new InvalidOperationException(
                    $"mysqld exited unexpectedly (exit code {proc.ExitCode}).\n" +
                    $"Captured stderr:\n{(string.IsNullOrWhiteSpace(stderr) ? "(empty)" : stderr)}\n" +
                    $"MySQL error log (last 30 lines):\n{logTail}");
            }

            try
            {
                using var tcp = new TcpClient();
                await tcp.ConnectAsync("127.0.0.1", MySqlPort, ct);
                await Task.Delay(1_000, ct);
                return;
            }
            catch (SocketException) { /* not ready yet */ }

            await Task.Delay(500, ct);
        }

        string tailOnTimeout = ReadMySqlErrorLogTail(20);
        throw new TimeoutException(
            $"MySQL did not accept connections within 90 seconds.\n" +
            $"MySQL error log (last 20 lines):\n{tailOnTimeout}");
    }

    /// <summary>
    /// Reads the last <paramref name="lines"/> lines of the MySQL error log.
    /// Returns a short message if the log cannot be read.
    /// </summary>
    private static string ReadMySqlErrorLogTail(int lines)
    {
        try
        {
            string logPath = Path.Combine(LogDir, "mysql_error.log");
            if (!File.Exists(logPath)) return "(error log not found)";

            // Read without locking the file so mysqld can still write to it
            using var fs     = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fs);
            var all = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
            return string.Join('\n', all.TakeLast(lines));
        }
        catch (Exception ex)
        {
            return $"(could not read error log: {ex.Message})";
        }
    }

    // ── First-run MySQL initialization ────────────────────────────────────────

    /// <summary>
    /// Runs <see cref="MySqlInitSql.FirstRunSetup"/> against the freshly-initialized
    /// MySQL instance (root has no password after <c>--initialize-insecure</c>).
    ///
    /// This single call:
    ///   • Secures the root account
    ///   • Creates the 'phoenix' application user
    ///   • Creates all expansion databases (Classic → MoP)
    ///   • Grants phoenix full ownership of every database
    /// </summary>
    private static async Task CreatePhoenixUserAsync(CancellationToken ct)
    {
        var psi = new ProcessStartInfo
        {
            FileName               = MysqlExe,
            Arguments              = $"--user=root --host=127.0.0.1 --port={MySqlPort} --protocol=TCP",
            RedirectStandardInput  = true,
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute        = false,
            CreateNoWindow         = true,
        };

        using var proc = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to launch mysql.exe for initialization.");

        await proc.StandardInput.WriteLineAsync(MySqlInitSql.FirstRunSetup);
        proc.StandardInput.Close();

        // Drain both streams concurrently — reading after WaitForExitAsync can deadlock
        // if the process fills the pipe buffer before it exits.
        var stdoutTask = proc.StandardOutput.ReadToEndAsync(ct);
        var stderrTask = proc.StandardError.ReadToEndAsync(ct);
        await proc.WaitForExitAsync(ct);
        await stdoutTask;
        var stderr = await stderrTask;

        if (proc.ExitCode != 0)
            throw new InvalidOperationException(
                $"MySQL initialization failed (exit {proc.ExitCode}): {stderr}");
    }

    // ── Hardware helpers ──────────────────────────────────────────────────────

    private static long GetTotalPhysicalRamMb()
    {
        try
        {
            var info = GC.GetGCMemoryInfo();
            long mb = info.TotalAvailableMemoryBytes / (1024L * 1024L);
            return mb > 0 ? mb : 4_096;
        }
        catch
        {
            return 4_096;
        }
    }

    private static string FormatMb(long mb) =>
        mb >= 1024 && mb % 1024 == 0 ? $"{mb / 1024}G" : $"{mb}M";

    /// <summary>
    /// Checks that the file is at least 1 MB and starts with the ZIP magic bytes (PK\x03\x04).
    /// Guards against cached partial downloads or HTML error pages saved as .zip.
    /// </summary>
    private static bool IsValidZip(string path)
    {
        const long minSize = 1 * 1024 * 1024; // 1 MB

        try
        {
            var info = new FileInfo(path);
            if (!info.Exists || info.Length < minSize) return false;

            Span<byte> magic = stackalloc byte[4];
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return fs.Read(magic) == 4
                && magic[0] == 0x50   // 'P'
                && magic[1] == 0x4B   // 'K'
                && magic[2] == 0x03
                && magic[3] == 0x04;
        }
        catch
        {
            return false;
        }
    }
}
