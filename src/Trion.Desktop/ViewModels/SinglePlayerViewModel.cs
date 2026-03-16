using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public partial class SinglePlayerViewModel : ObservableObject
{
    private readonly ILocalizationService        _loc;
    private readonly ISettingsService            _settings;
    private readonly IEmulatorInstallerService   _installer;

    public ILocalizationService Loc => _loc;
    public ObservableCollection<EmulatorCardViewModel> Emulators { get; }

    public SinglePlayerViewModel(ISettingsService settings, ILocalizationService loc,
                                 IEmulatorInstallerService installer,
                                 INotificationService notifications)
    {
        _loc       = loc;
        _settings  = settings;
        _installer = installer;

        Emulators = BuildCards(settings, installer, notifications);

        foreach (var card in Emulators)
            card.PropertyChanged += OnCardPropertyChanged;

        _ = LoadVersionsAsync();
    }

    // ── Mutual-exclusion: only one expansion active at a time ─────────────────

    private void OnCardPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(EmulatorCardViewModel.IsEnabled)) return;
        if (sender is not EmulatorCardViewModel changed || !changed.IsEnabled) return;

        foreach (var card in Emulators)
        {
            if (card != changed)
                card.DisableExternally();
        }
    }

    // ── Version loading ───────────────────────────────────────────────────────

    private async Task LoadVersionsAsync()
    {
        var versions = await _installer.FetchLatestVersionsAsync(CancellationToken.None);

        // Distribute latest versions to each card
        App.Current?.Dispatcher.InvokeAsync(() =>
        {
            // Index matches BuildCards order: 0=Classic,1=TBC,2=WotLK,3=Cata,4=MoP
            if (Emulators.Count >= 5)
            {
                Emulators[0].SetLatestVersion(versions.Classic);
                Emulators[1].SetLatestVersion(versions.Tbc);
                Emulators[2].SetLatestVersion(versions.Wotlk);
                Emulators[3].SetLatestVersion(versions.Cata);
                Emulators[4].SetLatestVersion(versions.Mop);
            }
        });
    }

    // ── Card factory ──────────────────────────────────────────────────────────

    private static Brush Hex(string hex) =>
        new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));

    private static ObservableCollection<EmulatorCardViewModel> BuildCards(
        ISettingsService s, IEmulatorInstallerService installer, INotificationService n) =>
    [
        new(s, installer, n,
            name: "World of Warcraft",      shortName: "Classic", apiEmulatorName: "classic",
            accentBrush:    Hex("#795548"),
            getInstalled:   c => c.ClassicInstalled,          setInstalled:   (c, v) => c.ClassicInstalled  = v,
            getEnabled:     c => c.LaunchClassicCore,         setEnabled:     (c, v) => c.LaunchClassicCore = v,
            getVersion:     c => c.ClassicInstalledVersion,   setVersion:     (c, v) => c.ClassicInstalledVersion = v,
            getInstallPath: c => c.ClassicWorkingDirectory,   setInstallPath: (c, v) => c.ClassicWorkingDirectory = v,
            getExePath:     c => c.ClassicWorldExeLoc),

        new(s, installer, n,
            name: "The Burning Crusade",    shortName: "TBC",     apiEmulatorName: "tbc",
            accentBrush:    Hex("#1565C0"),
            getInstalled:   c => c.TBCInstalled,              setInstalled:   (c, v) => c.TBCInstalled      = v,
            getEnabled:     c => c.LaunchTBCCore,             setEnabled:     (c, v) => c.LaunchTBCCore     = v,
            getVersion:     c => c.TBCInstalledVersion,       setVersion:     (c, v) => c.TBCInstalledVersion     = v,
            getInstallPath: c => c.TBCWorkingDirectory,       setInstallPath: (c, v) => c.TBCWorkingDirectory     = v,
            getExePath:     c => c.TBCWorldExeLoc),

        new(s, installer, n,
            name: "Wrath of the Lich King", shortName: "WotLK",   apiEmulatorName: "wotlk",
            accentBrush:    Hex("#0277BD"),
            getInstalled:   c => c.WotLKInstalled,            setInstalled:   (c, v) => c.WotLKInstalled    = v,
            getEnabled:     c => c.LaunchWotLKCore,           setEnabled:     (c, v) => c.LaunchWotLKCore   = v,
            getVersion:     c => c.WotLKInstalledVersion,     setVersion:     (c, v) => c.WotLKInstalledVersion   = v,
            getInstallPath: c => c.WotLKWorkingDirectory,     setInstallPath: (c, v) => c.WotLKWorkingDirectory   = v,
            getExePath:     c => c.WotLKWorldExeLoc),

        new(s, installer, n,
            name: "Cataclysm",             shortName: "Cata",    apiEmulatorName: "cata",
            accentBrush:    Hex("#BF360C"),
            getInstalled:   c => c.CataInstalled,             setInstalled:   (c, v) => c.CataInstalled     = v,
            getEnabled:     c => c.LaunchCataCore,            setEnabled:     (c, v) => c.LaunchCataCore    = v,
            getVersion:     c => c.CataInstalledVersion,      setVersion:     (c, v) => c.CataInstalledVersion    = v,
            getInstallPath: c => c.CataWorkingDirectory,      setInstallPath: (c, v) => c.CataWorkingDirectory    = v,
            getExePath:     c => c.CataWorldExeLoc),

        new(s, installer, n,
            name: "Mists of Pandaria",      shortName: "MoP",     apiEmulatorName: "mop",
            accentBrush:    Hex("#2E7D32"),
            getInstalled:   c => c.MOPInstalled,              setInstalled:   (c, v) => c.MOPInstalled      = v,
            getEnabled:     c => c.LaunchMoPCore,             setEnabled:     (c, v) => c.LaunchMoPCore     = v,
            getVersion:     c => c.MopInstalledVersion,       setVersion:     (c, v) => c.MopInstalledVersion     = v,
            getInstallPath: c => c.MopWorkingDirectory,       setInstallPath: (c, v) => c.MopWorkingDirectory     = v,
            getExePath:     c => c.MopWorldExeLoc),
    ];
}
