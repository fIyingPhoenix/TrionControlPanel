using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Trion.Desktop.Models;
using Trion.Desktop.Services;
using Trion.Desktop.Views;
using static Trion.Desktop.Models.AppLinks;
using static Trion.Desktop.ViewModels.DownloadFormatHelper;

namespace Trion.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider        _serviceProvider;
    private readonly ILocalizationService    _loc;
    private readonly IThemeService           _themeService;
    private readonly INotificationService    _notifications;
    private readonly IMySqlSetupService      _mysql;
    private readonly ServerControlViewModel  _serverControl;
    private readonly IAccountService         _account;

    [ObservableProperty] private ObservableObject? _currentViewModel;
    [ObservableProperty] private bool              _isSidebarCollapsed = true;

    // ── Download indicator ────────────────────────────────────────────────────

    [ObservableProperty] private bool   _isDownloading;
    [ObservableProperty] private bool   _isDownloadPopupOpen;
    [ObservableProperty] private int    _downloadPercent;
    [ObservableProperty] private string _downloadedSize = "—";
    [ObservableProperty] private string _downloadSpeed  = "—";
    [ObservableProperty] private string _downloadEta    = "—";

    // ── Notification state ────────────────────────────────────────────────────

    [ObservableProperty] private bool _isNotificationsPopupOpen;

    private int  _unreadCount;
    public  int  UnreadCount             => _unreadCount;
    public  bool HasUnreadNotifications  => _unreadCount > 0;

    /// <summary>Bound directly to the popup's ListBox (newest-first, up to 500 entries).</summary>
    public ObservableCollection<NotificationEntry> Notifications => _notifications.Entries;

    // ── Theme picker state ─────────────────────────────────────────────────────

    [ObservableProperty] private bool _isThemePopupOpen;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsTrionBlueSelected))]
    [NotifyPropertyChangedFor(nameof(IsOceanSelected))]
    [NotifyPropertyChangedFor(nameof(IsGreenSelected))]
    [NotifyPropertyChangedFor(nameof(IsOrangeSelected))]
    [NotifyPropertyChangedFor(nameof(IsLilaSelected))]
    private string _currentColor = "TrionBlue";

    [ObservableProperty] private bool _isThemeDark = true;

    public bool IsTrionBlueSelected => CurrentColor == "TrionBlue";
    public bool IsOceanSelected     => CurrentColor == "Ocean";
    public bool IsGreenSelected     => CurrentColor == "Green";
    public bool IsOrangeSelected    => CurrentColor == "Orange";
    public bool IsLilaSelected      => CurrentColor == "Lila";

    // ── Account state ──────────────────────────────────────────────────────────

    [ObservableProperty] private bool   _isAccountPopupOpen;
    [ObservableProperty] private string _accountUsername = string.Empty;
    [ObservableProperty] private string _accountEmail    = string.Empty;
    [ObservableProperty] private bool   _isLoggedIn;
    [ObservableProperty] private bool   _isGuest;
    [ObservableProperty] private string _accountRole    = string.Empty;
    [ObservableProperty] private string _accountApiTier = string.Empty;
    [ObservableProperty] private string _accountApiKey  = string.Empty;
    [ObservableProperty] private string _accountLastLogin = string.Empty;
    [ObservableProperty] private string _accountCreatedAt = string.Empty;

    // ── Bottom bar server state ────────────────────────────────────────────────

    [ObservableProperty] private bool _isDatabaseRunning;
    [ObservableProperty] private bool _isLogonRunning;
    [ObservableProperty] private bool _isWorldRunning;
    [ObservableProperty] private bool _isEmulatorInstalled;

    public ILocalizationService Loc => _loc;

    public string DatabaseButtonLabel => IsDatabaseRunning ? _loc["BTNStartDatabaseTextON"]  : _loc["BTNStartDatabaseTextOFF"];
    public string LogonButtonLabel    => IsLogonRunning    ? _loc["BTNStartLogonTextON"]      : _loc["BTNStartLogonTextOFF"];
    public string WorldButtonLabel    => IsWorldRunning    ? _loc["BTNStartWorldTextON"]      : _loc["BTNStartWorldTextOFF"];

    partial void OnIsDatabaseRunningChanged(bool value) => OnPropertyChanged(nameof(DatabaseButtonLabel));
    partial void OnIsLogonRunningChanged(bool value)    => OnPropertyChanged(nameof(LogonButtonLabel));
    partial void OnIsWorldRunningChanged(bool value)    => OnPropertyChanged(nameof(WorldButtonLabel));

    public ObservableCollection<NavigationItem> NavItems { get; } = [];

    public MainWindowViewModel(IServiceProvider       serviceProvider,
                               ILocalizationService   loc,
                               IThemeService          themeService,
                               INotificationService   notifications,
                               IMySqlSetupService     mysql,
                               ServerControlViewModel serverControl,
                               IAccountService        account)
    {
        _serviceProvider = serviceProvider;
        _loc             = loc;
        _themeService    = themeService;
        _notifications   = notifications;
        _mysql           = mysql;
        _serverControl   = serverControl;
        _account         = account;

        RefreshAccountState();
        _account.SessionChanged += RefreshAccountState;

        _mysql.DownloadProgressChanged += OnDownloadProgress;
        if (_mysql.CurrentDownload is { } snap)
            ApplyDownloadProgress(snap);

        // ── Sync bottom-bar state from the singleton ServerControlViewModel ────
        IsDatabaseRunning   = _serverControl.IsDatabaseRunning;
        IsWorldRunning      = _serverControl.IsWorldRunning;
        IsLogonRunning      = _serverControl.IsLogonRunning;
        IsEmulatorInstalled = _serverControl.IsEmulatorInstalled;

        _serverControl.PropertyChanged += (_, e) =>
        {
            switch (e.PropertyName)
            {
                case nameof(ServerControlViewModel.IsDatabaseRunning):
                    IsDatabaseRunning = _serverControl.IsDatabaseRunning; break;
                case nameof(ServerControlViewModel.IsWorldRunning):
                    IsWorldRunning = _serverControl.IsWorldRunning;
                    ToggleWorldServerCommand.NotifyCanExecuteChanged(); break;
                case nameof(ServerControlViewModel.IsLogonRunning):
                    IsLogonRunning = _serverControl.IsLogonRunning;
                    ToggleLogonServerCommand.NotifyCanExecuteChanged(); break;
                case nameof(ServerControlViewModel.IsEmulatorInstalled):
                    IsEmulatorInstalled = _serverControl.IsEmulatorInstalled;
                    ToggleWorldServerCommand.NotifyCanExecuteChanged();
                    ToggleLogonServerCommand.NotifyCanExecuteChanged(); break;
            }
        };

        _notifications.UnreadCountChanged += OnUnreadCountChanged;

        _loc.PropertyChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(DatabaseButtonLabel));
            OnPropertyChanged(nameof(LogonButtonLabel));
            OnPropertyChanged(nameof(WorldButtonLabel));
        };

        CurrentColor = _themeService.CurrentColor;
        IsThemeDark  = _themeService.IsDark;

        BuildNavItems();
        Navigate(NavItems[0]);
    }

    private void OnUnreadCountChanged()
    {
        _unreadCount = _notifications.UnreadCount;
        OnPropertyChanged(nameof(UnreadCount));
        OnPropertyChanged(nameof(HasUnreadNotifications));
    }

    private void BuildNavItems()
    {
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Home",       LocKey = "NavHome",     ViewModelType = typeof(DashboardViewModel) });
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Controller", LocKey = "TabSPPTitle", ViewModelType = typeof(SinglePlayerViewModel) });
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Database",   LocKey = "NavDatabase", ViewModelType = typeof(DatabaseEditorViewModel) });
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Ddns",       LocKey = "NavDdns",     ViewModelType = typeof(DdnsViewModel) });
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Log",        LocKey = "NavEventLog",   ViewModelType = typeof(LogsViewModel) });
        NavItems.Add(new NavigationItem(_loc) { IconKey = "Icon.Donate",    LocKey = "NavSupporters", ViewModelType = typeof(SupportersViewModel) });
    }

    [RelayCommand]
    private void Navigate(NavigationItem item)
    {
        foreach (var nav in NavItems)
            nav.IsSelected = false;

        item.IsSelected = true;
        CurrentViewModel = _serviceProvider.GetRequiredService(item.ViewModelType) as ObservableObject;
    }

    [RelayCommand]
    private void NavigateToSettings()
    {
        foreach (var nav in NavItems)
            nav.IsSelected = false;

        CurrentViewModel = _serviceProvider.GetRequiredService<SettingsViewModel>();
    }

    [RelayCommand]
    private void ToggleSidebar() => IsSidebarCollapsed = !IsSidebarCollapsed;

    // ── Notification popup ────────────────────────────────────────────────────

    [RelayCommand]
    private void ShowNotifications()
    {
        IsNotificationsPopupOpen = !IsNotificationsPopupOpen;
        if (IsNotificationsPopupOpen)
            _notifications.MarkAllRead();
    }

    // ── Download indicator ─────────────────────────────────────────────────────

    [RelayCommand]
    private void ShowDownloadProgress() => IsDownloadPopupOpen = !IsDownloadPopupOpen;

    private void OnDownloadProgress(DownloadProgress? p)
        => App.Current.Dispatcher.InvokeAsync(() => ApplyDownloadProgress(p));

    private void ApplyDownloadProgress(DownloadProgress? p)
    {
        if (p is null)
        {
            IsDownloading       = false;
            IsDownloadPopupOpen = false;
            DownloadPercent     = 0;
            DownloadedSize      = "—";
            DownloadSpeed       = "—";
            DownloadEta         = "—";
            return;
        }

        IsDownloading   = true;
        DownloadPercent = p.Percent;
        DownloadedSize  = FormatSize(p.DownloadedBytes, p.TotalBytes);
        DownloadSpeed   = $"{FormatBytes((long)p.SpeedBytesPerSec)}/s";
        DownloadEta     = p.Eta.HasValue ? $"{p.Eta.Value:mm\\:ss}" : "—";
    }

    [RelayCommand]
    private void ViewAllLogs()
    {
        IsNotificationsPopupOpen = false;

        // Navigate to the Event Log nav item
        var logsItem = NavItems.FirstOrDefault(n => n.ViewModelType == typeof(LogsViewModel));
        if (logsItem is not null)
            Navigate(logsItem);
    }

    [RelayCommand]
    private void OpenDonation()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName        = DonationUrl,
            UseShellExecute = true
        });
    }

    // ── Account commands ───────────────────────────────────────────────────────

    [RelayCommand]
    private void ToggleAccountPopup() => IsAccountPopupOpen = !IsAccountPopupOpen;

    [RelayCommand]
    private void Logout()
    {
        _account.Logout();
        IsAccountPopupOpen = false;
    }

    [RelayCommand]
    private void ShowLogin()
    {
        IsAccountPopupOpen = false;
        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        loginWindow.ShowDialog();
    }

    private void RefreshAccountState()
    {
        var dispatcher = App.Current?.Dispatcher;
        if (dispatcher is null) return;

        if (dispatcher.CheckAccess())
            ApplyAccountState();
        else
            dispatcher.InvokeAsync(ApplyAccountState);
    }

    private void ApplyAccountState()
    {
        IsLoggedIn       = _account.IsLoggedIn;
        IsGuest          = _account.IsGuest;
        AccountUsername  = _account.Username         ?? (_account.IsGuest ? "Guest" : string.Empty);
        AccountEmail     = _account.Email            ?? string.Empty;
        AccountRole      = _account.Profile?.Role    ?? string.Empty;
        AccountApiTier   = _account.Profile?.ApiTier ?? string.Empty;
        AccountApiKey    = _account.Profile?.ApiKey  ?? string.Empty;
        AccountLastLogin = _account.Profile?.LastLogin ?? string.Empty;
        AccountCreatedAt = _account.Profile?.CreatedAt ?? string.Empty;
    }

    // ── Theme picker commands ──────────────────────────────────────────────────

    [RelayCommand]
    private void ToggleThemePopup() => IsThemePopupOpen = !IsThemePopupOpen;

    [RelayCommand]
    private void SetVariant(string variant)
    {
        bool wantDark = variant == "Dark";
        if (_themeService.IsDark == wantDark) return;
        _themeService.ToggleVariant();
        IsThemeDark = _themeService.IsDark;
    }

    [RelayCommand]
    private void SetColor(string color)
    {
        _themeService.SetColor(color);
        CurrentColor = _themeService.CurrentColor;
    }

    // ── Bottom bar toggle commands — delegate to ServerControlViewModel ────────

    [RelayCommand]
    private Task ToggleDatabaseServer() => _serverControl.ToggleDatabaseCommand.ExecuteAsync(null);

    [RelayCommand(CanExecute = nameof(CanToggleWorldServer))]
    private Task ToggleWorldServer() => _serverControl.ToggleWorldCommand.ExecuteAsync(null);
    private bool CanToggleWorldServer() => _serverControl.IsWorldRunning || _serverControl.IsEmulatorInstalled;

    [RelayCommand(CanExecute = nameof(CanToggleLogonServer))]
    private Task ToggleLogonServer() => _serverControl.ToggleLogonCommand.ExecuteAsync(null);
    private bool CanToggleLogonServer() => _serverControl.IsLogonRunning || _serverControl.IsEmulatorInstalled;
}
