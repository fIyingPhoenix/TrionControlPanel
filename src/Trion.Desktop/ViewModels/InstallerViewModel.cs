using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trion.Desktop.Models;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public partial class InstallerViewModel : ObservableObject
{
    private readonly ISettingsService          _settings;
    private readonly ILocalizationService      _loc;
    private readonly IEmulatorInstallerService _installer;

    public ILocalizationService Loc => _loc;

    // ── Expansion rows (drives the ItemsControl) ──────────────────────────────

    public ObservableCollection<ExpansionItem> Expansions { get; } = [];

    // ── Progress state ────────────────────────────────────────────────────────

    [ObservableProperty] private bool   _isInstalling;
    [ObservableProperty] private double _progressValue;
    [ObservableProperty] private string _progressPhase  = string.Empty;
    [ObservableProperty] private string _progressStatus = string.Empty;
    [ObservableProperty] private double _speedMbps;

    /// <summary>Formatted download speed, e.g. "14.2 MB/s". Empty when idle.</summary>
    public string SpeedText => SpeedMbps > 0.05 ? $"{SpeedMbps:F1} MB/s" : string.Empty;

    private CancellationTokenSource? _cts;

    // ── Constructor ───────────────────────────────────────────────────────────

    public InstallerViewModel(
        ISettingsService          settings,
        ILocalizationService      loc,
        IEmulatorInstallerService installer)
    {
        _settings  = settings;
        _loc       = loc;
        _installer = installer;

        BuildExpansions();
        _ = LoadLatestVersionsAsync();
    }

    // ── Build expansion list from current settings ────────────────────────────

    private void BuildExpansions()
    {
        var cfg     = _settings.Current;
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // If the user hasn't configured an install path yet, default to servers/<name>/
        string DefaultDir(string configured, string apiName) =>
            string.IsNullOrWhiteSpace(configured)
                ? Path.Combine(baseDir, "servers", apiName)
                : configured;

        var rows = new[]
        {
            new ExpansionItem("Custom",               "custom")
            {
                InstallPath  = DefaultDir(cfg.CustomWorkingDirectory,  "custom"),
                ExePath      = cfg.CustomWorldExeLoc,
                IsInstalled  = cfg.CustomInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.CustomWorldExeLoc),
            },
            new ExpansionItem("Classic",              "classic")
            {
                InstallPath  = DefaultDir(cfg.ClassicWorkingDirectory, "classic"),
                ExePath      = cfg.ClassicWorldExeLoc,
                IsInstalled  = cfg.ClassicInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.ClassicWorldExeLoc),
            },
            new ExpansionItem("The Burning Crusade",  "tbc")
            {
                InstallPath  = DefaultDir(cfg.TBCWorkingDirectory,     "tbc"),
                ExePath      = cfg.TBCWorldExeLoc,
                IsInstalled  = cfg.TBCInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.TBCWorldExeLoc),
            },
            new ExpansionItem("Wrath of the Lich King", "wotlk")
            {
                InstallPath  = DefaultDir(cfg.WotLKWorkingDirectory,   "wotlk"),
                ExePath      = cfg.WotLKWorldExeLoc,
                IsInstalled  = cfg.WotLKInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.WotLKWorldExeLoc),
            },
            new ExpansionItem("Cataclysm",            "cata")
            {
                InstallPath  = DefaultDir(cfg.CataWorkingDirectory,    "cata"),
                ExePath      = cfg.CataWorldExeLoc,
                IsInstalled  = cfg.CataInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.CataWorldExeLoc),
            },
            new ExpansionItem("Mists of Pandaria",    "mop")
            {
                InstallPath  = DefaultDir(cfg.MopWorkingDirectory,     "mop"),
                ExePath      = cfg.MopWorldExeLoc,
                IsInstalled  = cfg.MOPInstalled,
                LocalVersion = _installer.GetLocalVersion(cfg.MopWorldExeLoc),
            },
        };

        foreach (var row in rows)
            Expansions.Add(row);
    }

    // ── Fetch latest versions from the API ────────────────────────────────────

    private async Task LoadLatestVersionsAsync()
    {
        var v = await _installer.FetchLatestVersionsAsync(CancellationToken.None);
        SetLatest("classic", v.Classic);
        SetLatest("tbc",     v.Tbc);
        SetLatest("wotlk",   v.Wotlk);
        SetLatest("cata",    v.Cata);
        SetLatest("mop",     v.Mop);
    }

    private void SetLatest(string apiName, string version)
    {
        var item = Expansions.FirstOrDefault(e => e.ApiName == apiName);
        if (item is not null) item.LatestVersion = version;
    }

    // ── Commands ──────────────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanInstall))]
    private async Task InstallAsync(ExpansionItem expansion) =>
        await RunAsync(expansion,
            (p, ct) => _installer.InstallAsync(expansion.ApiName, expansion.InstallPath, p, ct),
            installed: true);

    private bool CanInstall(ExpansionItem? e) => !IsInstalling && e is { IsInstalled: false };

    [RelayCommand(CanExecute = nameof(CanRepair))]
    private async Task RepairAsync(ExpansionItem expansion) =>
        await RunAsync(expansion,
            (p, ct) => _installer.RepairAsync(expansion.ApiName, expansion.InstallPath, p, ct),
            installed: true);

    private bool CanRepair(ExpansionItem? e) => !IsInstalling && e is { IsInstalled: true };

    [RelayCommand(CanExecute = nameof(CanUninstall))]
    private async Task UninstallAsync(ExpansionItem expansion) =>
        await RunAsync(expansion,
            (p, ct) => _installer.UninstallAsync(expansion.InstallPath, p, ct),
            installed: false);

    private bool CanUninstall(ExpansionItem? e) => !IsInstalling && e is { IsInstalled: true };

    [RelayCommand(CanExecute = nameof(CanCancel))]
    private void Cancel() => _cts?.Cancel();
    private bool CanCancel() => IsInstalling;

    // ── Central operation runner ──────────────────────────────────────────────

    private async Task RunAsync(
        ExpansionItem expansion,
        Func<IProgress<InstallProgress>, CancellationToken, Task> operation,
        bool installed)
    {
        _cts          = new CancellationTokenSource();
        IsInstalling  = true;
        ProgressValue = 0;
        ProgressPhase  = string.Empty;
        ProgressStatus = string.Empty;
        SpeedMbps      = 0;

        var progress = new Progress<InstallProgress>(p =>
        {
            ProgressValue  = p.Percent;
            ProgressPhase  = p.Phase;
            ProgressStatus = p.StatusText;
            SpeedMbps      = p.SpeedMbps;
        });

        try
        {
            await operation(progress, _cts.Token);

            expansion.IsInstalled  = installed;
            expansion.LocalVersion = installed
                ? _installer.GetLocalVersion(expansion.ExePath)
                : "—";

            SaveState(expansion.ApiName, installed);
        }
        catch (OperationCanceledException)
        {
            ProgressStatus = "Cancelled.";
        }
        catch (Exception ex)
        {
            ProgressStatus = $"Error: {ex.Message}";
        }
        finally
        {
            IsInstalling = false;
            _cts.Dispose();
            _cts = null;
        }
    }

    // Notify all commands whenever IsInstalling changes
    partial void OnIsInstallingChanged(bool value) => RefreshCommands();

    // Expose SpeedText as a computed property tied to SpeedMbps
    partial void OnSpeedMbpsChanged(double value) => OnPropertyChanged(nameof(SpeedText));

    private void RefreshCommands()
    {
        InstallCommand.NotifyCanExecuteChanged();
        RepairCommand.NotifyCanExecuteChanged();
        UninstallCommand.NotifyCanExecuteChanged();
        CancelCommand.NotifyCanExecuteChanged();
    }

    // ── Settings persistence ──────────────────────────────────────────────────

    private void SaveState(string apiName, bool installed)
    {
        var c = _settings.Current;
        switch (apiName)
        {
            case "custom":  c.CustomInstalled  = installed; break;
            case "classic": c.ClassicInstalled = installed; break;
            case "tbc":     c.TBCInstalled     = installed; break;
            case "wotlk":   c.WotLKInstalled   = installed; break;
            case "cata":    c.CataInstalled     = installed; break;
            case "mop":     c.MOPInstalled      = installed; break;
        }
        _settings.Save();
    }
}
