using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public enum SettingsTab { Trion, Custom, Database }

public partial class SettingsViewModel : ObservableObject, IDisposable
{
    private readonly ISettingsService     _settings;
    private readonly IThemeService        _theme;
    private readonly ILocalizationService _loc;
    private readonly IMySqlSetupService   _mysql;
    private readonly IDatabaseService     _db;

    [ObservableProperty] private SettingsTab _activeTab = SettingsTab.Trion;
    [ObservableProperty] private string _statusMessage = string.Empty;

    // ── Trion tab ─────────────────────────────────────────────────────────────

    [ObservableProperty] private bool   _serverCrashDetection;
    [ObservableProperty] private bool   _runServerWithWindows;
    [ObservableProperty] private bool   _runTrionWithWindows;
    [ObservableProperty] private bool   _hideConsole;
    [ObservableProperty] private bool   _notificationSound;
    [ObservableProperty] private bool   _stayInTray;
    [ObservableProperty] private bool   _autoUpdateTrion;
    [ObservableProperty] private bool   _autoUpdateCore;
    [ObservableProperty] private bool   _autoUpdateDatabase;
    [ObservableProperty] private string _selectedLanguage  = "en-US";
    [ObservableProperty] private int    _metricsInterval   = 2;

    // ── Custom tab ────────────────────────────────────────────────────────────

    [ObservableProperty] private bool   _useCustomServer;
    [ObservableProperty] private int    _customCoreType          = 1;   // index into CustomCoreTypes
    [ObservableProperty] private string _customEmulatorLocation  = string.Empty;
    [ObservableProperty] private string _customDatabaseLocation  = string.Empty;
    [ObservableProperty] private string _customWorldExeName      = string.Empty;
    [ObservableProperty] private string _customLogonExeName      = string.Empty;
    [ObservableProperty] private string _customDatabaseExeName   = string.Empty;

    /// <summary>Display names for the custom core type selector. Index matches AppSettings.SelectedDatabases.</summary>
    public static IReadOnlyList<string> CustomCoreTypes { get; } =
    [
        "TrinityCore 3.3.5a",
        "AzerothCore",
        "CMaNGOS / VMaNGOS",
    ];

    // ── MySQL management (Database tab) ───────────────────────────────────────

    [ObservableProperty] private bool   _isMySqlInstalled;
    [ObservableProperty] private bool   _isMySqlRunning;
    [ObservableProperty] private bool   _isMySqlBusy;
    [ObservableProperty] private string _mySqlStatusMessage = string.Empty;
    [ObservableProperty] private string _mySqlVersionInfo   = string.Empty;
    [ObservableProperty] private string _mySqlInstallPath   = string.Empty;
    [ObservableProperty] private string _mySqlProcessInfo   = string.Empty;

    // ── Database tab ──────────────────────────────────────────────────────────

    [ObservableProperty] private string _dbHost       = "localhost";
    [ObservableProperty] private string _dbPort       = "3306";
    [ObservableProperty] private string _dbUser       = "phoenix";
    [ObservableProperty] private string _dbPassword   = "phoenix";
    [ObservableProperty] private string _worldDb      = "wotlk_world";
    [ObservableProperty] private string _authDb       = "wotlk_auth";
    [ObservableProperty] private string _charDb       = "wotlk_characters";

    public ILocalizationService         Loc              => _loc;
    public IReadOnlyList<string>        Languages        => _loc.AvailableLanguages;
    public static IReadOnlyList<int>    MetricsIntervals { get; } = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    public SettingsViewModel(ISettingsService settings, IThemeService theme,
                             ILocalizationService loc, IMySqlSetupService mysql,
                             IDatabaseService db)
    {
        _settings = settings;
        _theme    = theme;
        _loc      = loc;
        _mysql    = mysql;
        _db       = db;

        _mysql.ProgressChanged += OnMySqlProgress;
        _mysql.StatusChanged   += OnMySqlStatus;

        LoadFromSettings();
        RefreshMySqlState();
    }

    private void LoadFromSettings()
    {
        var cfg = _settings.Current;

        ServerCrashDetection = cfg.ServerCrashDetection;
        RunServerWithWindows = cfg.RunServerWithWindows;
        RunTrionWithWindows  = cfg.RunWithWindows;
        HideConsole          = cfg.ConsoleHide;
        NotificationSound    = cfg.NotificationSound;
        StayInTray           = cfg.StayInTray;
        AutoUpdateTrion      = cfg.AutoUpdateTrion;
        AutoUpdateCore       = cfg.AutoUpdateCore;
        AutoUpdateDatabase   = cfg.AutoUpdateDatabase;
        SelectedLanguage     = cfg.Language;
        MetricsInterval      = Math.Clamp(cfg.MetricsIntervalSeconds, 1, 10);

        UseCustomServer         = cfg.LaunchCustomCore;
        CustomCoreType          = Math.Clamp(cfg.SelectedDatabases, 0, CustomCoreTypes.Count - 1);
        CustomEmulatorLocation  = cfg.CustomWorkingDirectory;
        CustomWorldExeName      = cfg.CustomWorldExeName;
        CustomLogonExeName      = cfg.CustomLogonExeName;
        CustomDatabaseExeName   = cfg.DBExeName;

        DbHost     = cfg.DBServerHost;
        DbPort     = cfg.DBServerPort;
        DbUser     = cfg.DBServerUser;
        DbPassword = cfg.DBServerPassword;
        WorldDb    = cfg.WorldDatabase;
        AuthDb     = cfg.AuthDatabase;
        CharDb     = cfg.CharactersDatabase;
    }

    [RelayCommand]
    private void SwitchTab(SettingsTab tab) => ActiveTab = tab;

    [RelayCommand]
    private void Save()
    {
        var cfg = _settings.Current;

        cfg.ServerCrashDetection = ServerCrashDetection;
        cfg.RunServerWithWindows = RunServerWithWindows;
        cfg.RunWithWindows       = RunTrionWithWindows;
        cfg.ConsoleHide          = HideConsole;
        cfg.NotificationSound    = NotificationSound;
        cfg.StayInTray           = StayInTray;
        cfg.AutoUpdateTrion      = AutoUpdateTrion;
        cfg.AutoUpdateCore       = AutoUpdateCore;
        cfg.AutoUpdateDatabase   = AutoUpdateDatabase;
        cfg.Theme                = _theme.CurrentTheme;   // persists whatever is active from the title-bar picker
        cfg.Language                = SelectedLanguage;
        cfg.MetricsIntervalSeconds  = Math.Clamp(MetricsInterval, 1, 10);

        cfg.LaunchCustomCore       = UseCustomServer;
        cfg.SelectedDatabases      = CustomCoreType;
        cfg.CustomWorkingDirectory = CustomEmulatorLocation;
        cfg.CustomWorldExeName     = CustomWorldExeName;
        cfg.CustomLogonExeName     = CustomLogonExeName;
        cfg.DBExeName              = CustomDatabaseExeName;

        cfg.DBServerHost     = DbHost;
        cfg.DBServerPort     = DbPort;
        cfg.DBServerUser     = DbUser;
        cfg.DBServerPassword = DbPassword;
        cfg.WorldDatabase    = WorldDb;
        cfg.AuthDatabase     = AuthDb;
        cfg.CharactersDatabase = CharDb;

        _settings.Save();

        _loc.LoadLanguage(SelectedLanguage);

        StatusMessage = "Settings saved.";
    }

    [RelayCommand]
    private async Task TestConnectionAsync()
    {
        StatusMessage = "Testing connection…";
        var (success, message, _) = await _db.TestConnectionAsync(DbHost, DbPort, DbUser, DbPassword);
        StatusMessage = success
            ? $"Connection to {DbHost}:{DbPort} — {message}"
            : $"Connection failed: {message}";
    }

    [RelayCommand]
    private void BrowseEmulatorLocation()
    {
        var dlg = new Microsoft.Win32.OpenFolderDialog { Title = "Select Emulator Directory" };
        if (dlg.ShowDialog() == true)
            CustomEmulatorLocation = dlg.FolderName;
    }

    [RelayCommand]
    private void BrowseDatabaseLocation()
    {
        var dlg = new Microsoft.Win32.OpenFolderDialog { Title = "Select Database Directory" };
        if (dlg.ShowDialog() == true)
            CustomDatabaseLocation = dlg.FolderName;
    }

    // ── MySQL management commands ─────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanMySqlAction))]
    private async Task RepairMySqlAsync()
    {
        IsMySqlBusy = true;
        NotifyMySqlCommandsChanged();
        try   { await _mysql.RepairAsync(); }
        finally { IsMySqlBusy = false; RefreshMySqlState(); NotifyMySqlCommandsChanged(); }
    }

    [RelayCommand(CanExecute = nameof(CanMySqlAction))]
    private async Task ReinstallMySqlAsync()
    {
        IsMySqlBusy = true;
        NotifyMySqlCommandsChanged();
        try   { await _mysql.ReinstallAsync(); }
        finally { IsMySqlBusy = false; RefreshMySqlState(); NotifyMySqlCommandsChanged(); }
    }

    [RelayCommand(CanExecute = nameof(CanMySqlUninstall))]
    private async Task UninstallMySqlAsync()
    {
        IsMySqlBusy = true;
        NotifyMySqlCommandsChanged();
        try   { await _mysql.UninstallAsync(); }
        finally { IsMySqlBusy = false; RefreshMySqlState(); NotifyMySqlCommandsChanged(); }
    }

    private bool CanMySqlAction()   => !IsMySqlBusy && !_mysql.IsRunning;
    private bool CanMySqlUninstall() => IsMySqlInstalled && !IsMySqlBusy && !_mysql.IsRunning;

    private void NotifyMySqlCommandsChanged()
    {
        RepairMySqlCommand.NotifyCanExecuteChanged();
        ReinstallMySqlCommand.NotifyCanExecuteChanged();
        UninstallMySqlCommand.NotifyCanExecuteChanged();
    }

    private void RefreshMySqlState()
    {
        IsMySqlInstalled  = _mysql.IsInstalled;
        IsMySqlRunning    = _mysql.IsProcessRunning;
        MySqlVersionInfo  = $"MySQL {ExternalLinks.MySqlVersion}";
        MySqlInstallPath  = IsMySqlInstalled
            ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database")
            : "Not installed";
        MySqlProcessInfo  = IsMySqlRunning && _mysql.ProcessId.HasValue
            ? $"PID {_mysql.ProcessId}  ·  {_mysql.Uptime:hh\\:mm\\:ss}"
            : "—";
    }

    private void OnMySqlProgress(string msg)
    {
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            MySqlStatusMessage = msg;
            IsMySqlBusy        = _mysql.IsRunning;
            NotifyMySqlCommandsChanged();
        });
    }

    private void OnMySqlStatus(bool running)
    {
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            IsMySqlRunning   = running;
            MySqlProcessInfo = running && _mysql.ProcessId.HasValue
                ? $"PID {_mysql.ProcessId}  ·  {_mysql.Uptime:hh\\:mm\\:ss}"
                : "—";
            NotifyMySqlCommandsChanged();
        });
    }

    // ── Dispose ───────────────────────────────────────────────────────────────

    public void Dispose()
    {
        _mysql.ProgressChanged -= OnMySqlProgress;
        _mysql.StatusChanged   -= OnMySqlStatus;
    }
}
