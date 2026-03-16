using CommunityToolkit.Mvvm.ComponentModel;

namespace Trion.Desktop.ViewModels;

/// <summary>
/// Represents a single emulator expansion row in the Installer view.
/// Holds display state (installed, versions) and the info needed to call the service.
/// </summary>
public partial class ExpansionItem : ObservableObject
{
    /// <summary>User-visible name shown in the row, e.g. "Wrath of the Lich King".</summary>
    public string DisplayName { get; }

    /// <summary>API name sent to the Trion server, e.g. "wotlk", "classic".</summary>
    public string ApiName { get; }

    /// <summary>Local folder where the emulator lives (or will be installed).</summary>
    public string InstallPath { get; set; } = "";

    /// <summary>Path to the world-server exe — used to read the installed version string.</summary>
    public string ExePath { get; set; } = "";

    [ObservableProperty] private bool   _isInstalled;
    [ObservableProperty] private string _localVersion  = "—";
    [ObservableProperty] private string _latestVersion = "—";

    /// <summary>Inverse of <see cref="IsInstalled"/> — used by the "Install" button's Visibility binding.</summary>
    public bool NotIsInstalled => !IsInstalled;

    partial void OnIsInstalledChanged(bool value) => OnPropertyChanged(nameof(NotIsInstalled));

    public ExpansionItem(string displayName, string apiName)
    {
        DisplayName = displayName;
        ApiName     = apiName;
    }
}
