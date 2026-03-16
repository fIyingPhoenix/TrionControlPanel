using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Timers;
using Trion.Desktop.Services;
using Timer = System.Timers.Timer;

namespace Trion.Desktop.ViewModels;

public partial class DdnsViewModel : ObservableObject, IDisposable
{
    private readonly ISettingsService     _settings;
    private readonly INetworkService      _network;
    private readonly ILocalizationService _loc;
    private readonly IDdnsUpdateService   _ddns;
    private Timer? _countdownTimer;
    private int    _secondsRemaining;

    public ILocalizationService Loc => _loc;

    [ObservableProperty] private string _selectedService = "DuckDNS";
    [ObservableProperty] private string _domain          = string.Empty;
    [ObservableProperty] private string _username        = string.Empty;
    [ObservableProperty] private string _password        = string.Empty;
    [ObservableProperty] private int    _interval        = 300;
    [ObservableProperty] private bool   _runOnStartup;
    [ObservableProperty] private string _publicIp        = "—";
    [ObservableProperty] private bool   _isIpVisible;
    [ObservableProperty] private string _timerDisplay    = "—";
    [ObservableProperty] private bool   _isTimerRunning;
    [ObservableProperty] private string _statusMessage   = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateNowCommand))]
    private bool _isUpdating;

    [ObservableProperty] private string _lastUpdated  = "—";
    [ObservableProperty] private bool   _lastUpdateOk;

    public static IReadOnlyList<string> Services { get; } =
    [
        "Afraid",
        "AllInkl",
        "Cloudflare",
        "DuckDNS",
        "DynDNS",
        "Dynu",
        "Enom",
        "Freemyip",
        "NoIP",
        "OVH",
        "STRATO"
    ];

    public DdnsViewModel(
        ISettingsService     settings,
        INetworkService      network,
        ILocalizationService loc,
        IDdnsUpdateService   ddns)
    {
        _settings = settings;
        _network  = network;
        _loc      = loc;
        _ddns     = ddns;
        LoadFromSettings();
        _ = DetectPublicIpAsync();
    }

    private void LoadFromSettings()
    {
        var cfg = _settings.Current;
        Domain          = cfg.DDNSDomain   == "N/A" ? string.Empty : cfg.DDNSDomain;
        Username        = cfg.DDNSUsername == "N/A" ? string.Empty : cfg.DDNSUsername;
        Password        = cfg.DDNSPassword == "N/A" ? string.Empty : cfg.DDNSPassword;
        Interval        = cfg.DDNSInterval > 0 ? cfg.DDNSInterval : 300;
        RunOnStartup    = cfg.DDNSRunOnStartup;
        SelectedService = string.IsNullOrEmpty(cfg.DDNSServiceName) ? "DuckDNS" : cfg.DDNSServiceName;
    }

    private async Task DetectPublicIpAsync()
    {
        PublicIp = await _network.GetPublicIpv4Async().ConfigureAwait(false);
    }

    // ── IP visibility toggle ──────────────────────────────────────────────────

    [RelayCommand]
    private void ToggleIpVisibility() => IsIpVisible = !IsIpVisible;

    // ── Save settings ─────────────────────────────────────────────────────────

    [RelayCommand]
    private void SaveSettings()
    {
        var cfg = _settings.Current;
        cfg.DDNSDomain       = Domain;
        cfg.DDNSUsername     = Username;
        cfg.DDNSPassword     = Password;
        cfg.DDNSInterval     = Interval;
        cfg.DDNSRunOnStartup = RunOnStartup;
        cfg.DDNSServiceName  = SelectedService;
        _settings.Save();
        StatusMessage = _loc["DdnsSettingsSaved"];
    }

    // ── Countdown timer ───────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanStartTimer))]
    private void StartTimer()
    {
        _secondsRemaining = Interval;
        IsTimerRunning    = true;
        _countdownTimer   = new Timer(1000) { AutoReset = true };
        _countdownTimer.Elapsed += OnTimerTick;
        _countdownTimer.Start();
        StatusMessage = _loc["DdnsTimerStarted"];
        StartTimerCommand.NotifyCanExecuteChanged();
        StopTimerCommand.NotifyCanExecuteChanged();
    }
    private bool CanStartTimer() => !IsTimerRunning;

    [RelayCommand(CanExecute = nameof(CanStopTimer))]
    private void StopTimer()
    {
        _countdownTimer?.Stop();
        _countdownTimer?.Dispose();
        _countdownTimer = null;
        IsTimerRunning  = false;
        TimerDisplay    = "—";
        StatusMessage   = _loc["DdnsTimerStopped"];
        StartTimerCommand.NotifyCanExecuteChanged();
        StopTimerCommand.NotifyCanExecuteChanged();
    }
    private bool CanStopTimer() => IsTimerRunning;

    private void OnTimerTick(object? sender, ElapsedEventArgs e)
    {
        _secondsRemaining--;
        var span = TimeSpan.FromSeconds(Math.Max(0, _secondsRemaining));
        App.Current.Dispatcher.InvokeAsync(() => TimerDisplay = span.ToString(@"mm\:ss"));

        if (_secondsRemaining <= 0)
        {
            _secondsRemaining = Interval;
            _ = PerformUpdateAsync(force: false);
        }
    }

    // ── Manual "Update Now" ───────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanUpdateNow))]
    private Task UpdateNowAsync() => PerformUpdateAsync(force: true);
    private bool CanUpdateNow() => !IsUpdating;

    // ── Core update logic ─────────────────────────────────────────────────────

    private async Task PerformUpdateAsync(bool force)
    {
        if (IsUpdating) return;

        await App.Current.Dispatcher.InvokeAsync(() => IsUpdating = true);
        try
        {
            // 1. Refresh public IP
            string newIp = await _network.GetPublicIpv4Async().ConfigureAwait(false);
            await App.Current.Dispatcher.InvokeAsync(() => PublicIp = newIp);

            // 2. Skip if IP unchanged (unless forced)
            string storedIp = _settings.Current.IPAddress ?? string.Empty;
            if (!force && newIp == storedIp)
            {
                await App.Current.Dispatcher.InvokeAsync(() =>
                {
                    LastUpdated   = DateTime.Now.ToString("HH:mm:ss");
                    LastUpdateOk  = true;
                    StatusMessage = _loc["DdnsIpUnchanged"];
                });
                return;
            }

            // 3. Validate credentials
            if (string.IsNullOrWhiteSpace(Domain) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.Dispatcher.InvokeAsync(() =>
                    StatusMessage = _loc["DdnsMissingCredentials"]);
                return;
            }

            // 4. Send update to provider
            await App.Current.Dispatcher.InvokeAsync(() =>
                StatusMessage = $"{_loc["DdnsUpdating"]} {SelectedService}…");

            var result = await _ddns
                .UpdateAsync(SelectedService, Domain, Username, Password, newIp)
                .ConfigureAwait(false);

            // 5. On success, persist the new IP
            if (result.Success)
            {
                _settings.Current.IPAddress = newIp;
                _settings.Save();
            }

            await App.Current.Dispatcher.InvokeAsync(() =>
            {
                LastUpdateOk  = result.Success;
                LastUpdated   = DateTime.Now.ToString("HH:mm:ss");
                StatusMessage = result.Success
                    ? $"{_loc["DdnsUpdatedSuccess"]} → {newIp}"
                    : $"{_loc["DdnsUpdateFailed"]}: {result.Message}";
            });
        }
        finally
        {
            await App.Current.Dispatcher.InvokeAsync(() => IsUpdating = false);
        }
    }

    public void Dispose() => _countdownTimer?.Dispose();
}
