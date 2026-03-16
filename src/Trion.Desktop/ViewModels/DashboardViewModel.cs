using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Timers;
using Trion.Desktop.Models;
using Trion.Desktop.Services;
using Timer = System.Timers.Timer;

namespace Trion.Desktop.ViewModels;

public partial class DashboardViewModel : ObservableObject, IDisposable
{
    private readonly ISettingsService      _settings;
    private readonly IMySqlSetupService    _mysql;
    private readonly ISystemMetricsService _metrics;
    private readonly ILocalizationService  _loc;
    private readonly INewsService          _news;
    private readonly Timer                 _serverTimer;
    private readonly Timer                 _uptimeTimer;

    public ILocalizationService Loc => _loc;

    // ── MySQL setup banner ────────────────────────────────────────────────────

    [ObservableProperty] private bool   _isSetupRunning;
    [ObservableProperty] private string _setupStatusMessage = string.Empty;

    // ── Server status ────────────────────────────────────────────────────────

    [ObservableProperty] private bool _isDatabaseRunning;
    [ObservableProperty] private bool _isWorldRunning;
    [ObservableProperty] private bool _isLogonRunning;

    [ObservableProperty] private string _databaseUptime = "00:00:00";
    [ObservableProperty] private string _worldUptime    = "00:00:00";
    [ObservableProperty] private string _logonUptime    = "00:00:00";

    [ObservableProperty] private int _databasePid;
    [ObservableProperty] private int _worldPid;
    [ObservableProperty] private int _logonPid;

    // ── Machine metrics ───────────────────────────────────────────────────────

    [ObservableProperty] private double _cpuPercent;
    [ObservableProperty] private double _ramPercent;
    [ObservableProperty] private string _ramUsed   = "—";
    [ObservableProperty] private string _ramTotal  = "—";
    [ObservableProperty] private string _diskRead  = "—";
    [ObservableProperty] private string _diskWrite = "—";
    [ObservableProperty] private string _networkRx = "—";
    [ObservableProperty] private string _networkTx = "—";

    // Sparkline history (newest value appended; oldest dropped when over capacity)
    private const int HistoryCapacity = 60;   // 60 × 2 s = 2 min window

    private readonly List<double> _diskReadBuf  = new(HistoryCapacity + 1);
    private readonly List<double> _diskWriteBuf = new(HistoryCapacity + 1);
    private readonly List<double> _netRecvBuf   = new(HistoryCapacity + 1);
    private readonly List<double> _netSendBuf   = new(HistoryCapacity + 1);

    [ObservableProperty] private double[] _diskReadHistory  = [];
    [ObservableProperty] private double[] _diskWriteHistory = [];
    [ObservableProperty] private double[] _netRecvHistory   = [];
    [ObservableProperty] private double[] _netSendHistory   = [];

    // ── News ─────────────────────────────────────────────────────────────────

    [ObservableProperty] private bool _isNewsLoading = true;
    [ObservableProperty] private bool _isNewsEmpty;
    public ObservableCollection<NewsItem> NewsItems { get; } = [];

    // ── Server process tracking ───────────────────────────────────────────────

    private DateTime? _dbStartTime;
    private DateTime? _worldStartTime;
    private DateTime? _logonStartTime;

    // ── Construction ──────────────────────────────────────────────────────────

    public DashboardViewModel(ISettingsService settings, IMySqlSetupService mysql,
                              ISystemMetricsService metrics, ILocalizationService loc,
                              INewsService news)
    {
        _settings = settings;
        _mysql    = mysql;
        _metrics  = metrics;
        _loc      = loc;
        _news     = news;

        // MySQL setup banner
        IsSetupRunning     = _mysql.IsRunning;
        SetupStatusMessage = _mysql.LastMessage;
        _mysql.ProgressChanged += OnMySqlProgress;
        _mysql.StatusChanged   += OnMySqlStatusChanged;

        // Machine metrics — handle late subscription
        _metrics.Updated += OnMetricsUpdated;
        if (_metrics.Last is { } last)
            ApplyMetrics(last);

        // Server status polled on a 3-second timer (process checks are cheap)
        _serverTimer = new Timer(3_000) { AutoReset = true };
        _serverTimer.Elapsed += (_, _) => RefreshServerStatus();
        _serverTimer.Start();

        // Uptime strings updated every second (just string formatting, no process scan)
        _uptimeTimer = new Timer(1_000) { AutoReset = true };
        _uptimeTimer.Elapsed += (_, _) => TickUptime();
        _uptimeTimer.Start();

        RefreshServerStatus();
        _ = LoadNewsAsync();
    }

    // ── News loading ──────────────────────────────────────────────────────────

    private async Task LoadNewsAsync()
    {
        IsNewsLoading = true;
        var items = await _news.GetLatestAsync(6);
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            NewsItems.Clear();
            foreach (var item in items)
                NewsItems.Add(item);
            IsNewsLoading = false;
            IsNewsEmpty   = NewsItems.Count == 0;
        });
    }

    // ── MySQL banner / status ─────────────────────────────────────────────────

    private void OnMySqlProgress(string message)
    {
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            SetupStatusMessage = message;
            IsSetupRunning     = _mysql.IsRunning;
        });
    }

    private void OnMySqlStatusChanged(bool running)
    {
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            IsDatabaseRunning = running;
            DatabasePid       = _mysql.ProcessId ?? 0;
            _dbStartTime      = _mysql.StartTime;
            DatabaseUptime    = FormatUptime(_dbStartTime);
        });
    }

    // ── Machine metrics ───────────────────────────────────────────────────────

    private void OnMetricsUpdated(SystemMetrics m)
        => App.Current?.Dispatcher.InvokeAsync(() => ApplyMetrics(m));

    private void ApplyMetrics(SystemMetrics m)
    {
        CpuPercent = Math.Round(m.CpuPercent, 1);
        RamPercent = Math.Round(m.RamPercent, 1);
        RamUsed    = SystemMetricsService.FormatMb(m.RamUsedMb);
        RamTotal   = SystemMetricsService.FormatMb(m.RamTotalMb);
        DiskRead   = SystemMetricsService.FormatBps(m.DiskReadBps);
        DiskWrite  = SystemMetricsService.FormatBps(m.DiskWriteBps);
        NetworkRx  = SystemMetricsService.FormatBps(m.NetRecvBps);
        NetworkTx  = SystemMetricsService.FormatBps(m.NetSendBps);

        Push(_diskReadBuf,  m.DiskReadBps);
        Push(_diskWriteBuf, m.DiskWriteBps);
        Push(_netRecvBuf,   m.NetRecvBps);
        Push(_netSendBuf,   m.NetSendBps);

        DiskReadHistory  = [.. _diskReadBuf];
        DiskWriteHistory = [.. _diskWriteBuf];
        NetRecvHistory   = [.. _netRecvBuf];
        NetSendHistory   = [.. _netSendBuf];
    }

    private static void Push(List<double> buf, double value)
    {
        buf.Add(value);
        if (buf.Count > HistoryCapacity)
            buf.RemoveAt(0);
    }

    // ── Server status ─────────────────────────────────────────────────────────

    private void TickUptime()
    {
        var db    = FormatUptime(_dbStartTime);
        var world = FormatUptime(_worldStartTime);
        var logon = FormatUptime(_logonStartTime);
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            DatabaseUptime = db;
            WorldUptime    = world;
            LogonUptime    = logon;
        });
    }

    private void RefreshServerStatus()
    {
        var cfg = _settings.Current;

        // Database: use the service (PID-based) to avoid false positives from any
        // system-installed mysqld that is unrelated to the one Trion launched.
        bool dbRunning = _mysql.IsProcessRunning;
        int  dbPid     = _mysql.ProcessId ?? 0;
        if (!dbRunning)
            _dbStartTime = null;
        else
            _dbStartTime ??= _mysql.StartTime;

        bool    worldRunning = false;
        int     worldPid     = 0;
        bool    logonRunning = false;
        int     logonPid     = 0;

        CheckProcess(cfg.WotLKWorldExeName.Length > 0 ? cfg.WotLKWorldExeName : "worldserver",
            ref worldRunning, ref worldPid, ref _worldStartTime);
        CheckProcess(cfg.WotLKLogonExeName.Length > 0 ? cfg.WotLKLogonExeName : "authserver",
            ref logonRunning, ref logonPid, ref _logonStartTime);

        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            IsDatabaseRunning = dbRunning;
            IsWorldRunning    = worldRunning;
            IsLogonRunning    = logonRunning;
            DatabasePid       = dbPid;
            WorldPid          = worldPid;
            LogonPid          = logonPid;
            DatabaseUptime    = FormatUptime(_dbStartTime);
            WorldUptime       = FormatUptime(_worldStartTime);
            LogonUptime       = FormatUptime(_logonStartTime);
        });
    }

    private static void CheckProcess(string exeName,
        ref bool running, ref int pid, ref DateTime? startTime)
    {
        var name  = Path.GetFileNameWithoutExtension(exeName);
        var procs = Process.GetProcessesByName(name);
        if (procs.Length > 0)
        {
            running   = true;
            pid       = procs[0].Id;
            startTime ??= procs[0].StartTime;
        }
        else
        {
            running   = false;
            pid       = 0;
            startTime = null;
        }
    }

    private static string FormatUptime(DateTime? startTime)
    {
        if (startTime is null) return "00:00:00";
        return (DateTime.Now - startTime.Value).ToString(@"hh\:mm\:ss");
    }

    // ── Dispose ───────────────────────────────────────────────────────────────

    public void Dispose()
    {
        _mysql.ProgressChanged -= OnMySqlProgress;
        _mysql.StatusChanged   -= OnMySqlStatusChanged;
        _metrics.Updated       -= OnMetricsUpdated;
        _serverTimer.Stop();
        _serverTimer.Dispose();
        _uptimeTimer.Stop();
        _uptimeTimer.Dispose();
    }
}
