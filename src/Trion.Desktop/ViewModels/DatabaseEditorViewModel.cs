using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Trion.Desktop.Models;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public enum DatabaseEditorTab { RealmList, Accounts }

public partial class DatabaseEditorViewModel : ObservableObject
{
    private readonly ISettingsService     _settings;
    private readonly INetworkService      _network;
    private readonly ILocalizationService _loc;
    private readonly IDatabaseService     _db;

    public ILocalizationService Loc => _loc;

    [ObservableProperty] private DatabaseEditorTab _activeTab = DatabaseEditorTab.RealmList;
    [ObservableProperty] private string _statusMessage = string.Empty;
    [ObservableProperty] private bool   _isBusy        = false;
    [ObservableProperty] private bool   _isDetecting   = false;

    // ── RealmList ─────────────────────────────────────────────────────────────

    /// <summary>Loaded realm entries from the database.</summary>
    public ObservableCollection<RealmEntry> Realms { get; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasSelectedRealm))]
    private RealmEntry? _selectedRealm;

    public bool HasSelectedRealm => SelectedRealm is not null;

    partial void OnSelectedRealmChanged(RealmEntry? value)
    {
        if (value is null) return;
        RealmId          = value.Id;
        RealmName        = value.Name;
        RealmAddress     = value.Address;
        RealmLocalAddress = value.LocalAddress;
        RealmSubnetMask  = value.LocalSubnetMask;
        RealmPort        = value.Port.ToString();
        RealmGameBuild   = value.GameBuild.ToString();
    }

    [ObservableProperty] private int    _realmId;
    [ObservableProperty] private string _realmName         = string.Empty;
    [ObservableProperty] private string _realmAddress      = string.Empty;
    [ObservableProperty] private string _realmLocalAddress = string.Empty;
    [ObservableProperty] private string _realmSubnetMask   = "255.255.255.0";
    [ObservableProperty] private string _realmPort         = "8085";
    [ObservableProperty] private string _realmGameBuild    = "12340";
    [ObservableProperty] private string _domainName        = string.Empty;

    /// <summary>All detected physical IPv4 addresses on this machine.</summary>
    public ObservableCollection<string> InternalIps { get; } = [];

    [ObservableProperty] private string _internalIp  = string.Empty;
    [ObservableProperty] private string _publicIp    = string.Empty;
    [ObservableProperty] private bool   _isIpVisible = false;

    // ── Account ───────────────────────────────────────────────────────────────

    [ObservableProperty] private string _createUsername  = string.Empty;
    [ObservableProperty] private string _createPassword  = string.Empty;
    [ObservableProperty] private string _createEmail     = string.Empty;
    [ObservableProperty] private int    _createExpansion = 2;

    [ObservableProperty] private string _gmUsername = string.Empty;
    [ObservableProperty] private int    _gmLevel    = 0;

    [ObservableProperty] private string _passUsername    = string.Empty;
    [ObservableProperty] private string _passNewPassword = string.Empty;
    [ObservableProperty] private string _passConfirm     = string.Empty;

    public ObservableCollection<string> Expansions { get; } =
    [
        "Classic",
        "The Burning Crusade",
        "Wrath of the Lich King",
        "Cataclysm",
        "Mists of Pandaria",
        "Warlords of Draenor",
        "Legion",
        "Battle for Azeroth",
        "Shadowlands",
        "Dragonflight"
    ];

    public ObservableCollection<string> AccessLevels { get; } =
    [
        "Player",
        "Basic Game Master",
        "Advanced Game Master",
        "Senior Game Master",
        "Admin"
    ];

    public DatabaseEditorViewModel(
        ISettingsService settings, INetworkService network,
        ILocalizationService loc, IDatabaseService db)
    {
        _settings = settings;
        _network  = network;
        _loc      = loc;
        _db       = db;

        var savedDomain = settings.Current.DDNSDomain;
        DomainName = string.IsNullOrWhiteSpace(savedDomain) ? string.Empty : savedDomain;

        _ = DetectNetworkAsync();
    }

    [RelayCommand]
    private void SwitchTab(DatabaseEditorTab tab) => ActiveTab = tab;

    [RelayCommand]
    private void ToggleIpVisibility() => IsIpVisible = !IsIpVisible;

    // ── Network Detection ─────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanDetect))]
    private async Task DetectNetworkAsync()
    {
        IsDetecting   = true;
        StatusMessage = "Detecting network addresses…";

        var internalAddrs = _network.GetInternalIpv4Addresses();
        InternalIps.Clear();
        foreach (var addr in internalAddrs)
            InternalIps.Add(addr);

        if (!InternalIps.Contains(InternalIp))
            InternalIp = InternalIps.FirstOrDefault() ?? string.Empty;

        PublicIp = await _network.GetPublicIpv4Async().ConfigureAwait(false);

        StatusMessage = "Network addresses detected.";
        IsDetecting   = false;
    }
    private bool CanDetect() => !IsDetecting && !IsBusy;

    // ── RealmList Commands ────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task RefreshRealmAsync()
    {
        IsBusy        = true;
        StatusMessage = "Loading realm list…";
        NotifyRealmCommandsChanged();
        try
        {
            var realms = await _db.GetRealmsAsync();
            Realms.Clear();
            foreach (var r in realms)
                Realms.Add(r);

            // Auto-select first if nothing is selected
            if (SelectedRealm is null && Realms.Count > 0)
                SelectedRealm = Realms[0];

            StatusMessage = Realms.Count > 0
                ? $"Loaded {Realms.Count} realm(s)."
                : "No realms found. Check your database settings.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            NotifyRealmCommandsChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task SaveRealmAsync()
    {
        IsBusy        = true;
        StatusMessage = "Saving realm…";
        NotifyRealmCommandsChanged();
        try
        {
            var realm = BuildRealmFromFields();

            var (ok, error) = realm.Id > 0
                ? await _db.SaveRealmAsync(realm)
                : await _db.CreateRealmAsync(realm);

            StatusMessage = ok ? "Realm saved." : $"Error: {error}";

            if (ok) await RefreshRealmAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            NotifyRealmCommandsChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanEditSelectedRealm))]
    private async Task DeleteRealmAsync()
    {
        if (SelectedRealm is null) return;

        IsBusy        = true;
        StatusMessage = $"Deleting realm '{SelectedRealm.Name}'…";
        NotifyRealmCommandsChanged();
        try
        {
            var (ok, error) = await _db.DeleteRealmAsync(SelectedRealm.Id);
            StatusMessage = ok ? "Realm deleted." : $"Error: {error}";
            if (ok)
            {
                SelectedRealm = null;
                await RefreshRealmAsync();
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            NotifyRealmCommandsChanged();
        }
    }

    /// <summary>Sets realm address to the detected public IP and saves.</summary>
    [RelayCommand(CanExecute = nameof(CanEditSelectedRealm))]
    private async Task OpenPublicAsync()
    {
        if (SelectedRealm is null || string.IsNullOrWhiteSpace(PublicIp)) return;

        IsBusy        = true;
        StatusMessage = $"Setting realm to public IP ({PublicIp})…";
        NotifyRealmCommandsChanged();
        try
        {
            var (ok, error) = await _db.UpdateRealmAddressAsync(SelectedRealm.Id, PublicIp);
            if (ok)
            {
                RealmAddress = PublicIp;
                SelectedRealm.Address = PublicIp;
                StatusMessage = $"Realm address set to {PublicIp}.";
            }
            else
            {
                StatusMessage = $"Error: {error}";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            NotifyRealmCommandsChanged();
        }
    }

    /// <summary>Sets realm address to the selected local IP (or domain name) and saves.</summary>
    [RelayCommand(CanExecute = nameof(CanEditSelectedRealm))]
    private async Task OpenLocalAsync()
    {
        if (SelectedRealm is null) return;

        // Prefer domain name if set; fall back to selected LAN IP
        var target = !string.IsNullOrWhiteSpace(DomainName) ? DomainName : InternalIp;
        if (string.IsNullOrWhiteSpace(target))
        {
            StatusMessage = "No local IP or domain name detected.";
            return;
        }

        IsBusy        = true;
        StatusMessage = $"Setting realm to local ({target})…";
        NotifyRealmCommandsChanged();
        try
        {
            var (ok, error) = await _db.UpdateRealmAddressAsync(SelectedRealm.Id, target);
            if (ok)
            {
                RealmAddress = target;
                SelectedRealm.Address = target;
                StatusMessage = $"Realm address set to {target}.";
            }
            else
            {
                StatusMessage = $"Error: {error}";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            NotifyRealmCommandsChanged();
        }
    }

    // ── Account Commands ──────────────────────────────────────────────────────

    [RelayCommand(CanExecute = nameof(CanCreateAccount))]
    private async Task CreateAccountAsync()
    {
        IsBusy        = true;
        StatusMessage = $"Creating account '{CreateUsername}'…";
        CreateAccountCommand.NotifyCanExecuteChanged();
        try
        {
            var (ok, error) = await _db.CreateAccountAsync(
                CreateUsername, CreatePassword, CreateEmail, CreateExpansion);

            StatusMessage = ok
                ? $"Account '{CreateUsername}' created successfully."
                : $"Error: {error}";

            if (ok)
                CreateUsername = CreatePassword = CreateEmail = string.Empty;
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            CreateAccountCommand.NotifyCanExecuteChanged();
        }
    }
    private bool CanCreateAccount() =>
        !IsBusy &&
        !string.IsNullOrWhiteSpace(CreateUsername) &&
        !string.IsNullOrWhiteSpace(CreatePassword);

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task SetGmAccessAsync()
    {
        if (string.IsNullOrWhiteSpace(GmUsername))
        {
            StatusMessage = "Enter a username.";
            return;
        }

        IsBusy        = true;
        StatusMessage = $"Setting GM level {GmLevel} for '{GmUsername}'…";
        SetGmAccessCommand.NotifyCanExecuteChanged();
        try
        {
            var (ok, error) = await _db.SetGmLevelAsync(GmUsername, GmLevel);
            StatusMessage = ok
                ? $"GM level {GmLevel} set for '{GmUsername}'."
                : $"Error: {error}";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            SetGmAccessCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanChangePassword))]
    private async Task ChangePasswordAsync()
    {
        if (PassNewPassword != PassConfirm)
        {
            StatusMessage = "Passwords do not match.";
            return;
        }

        IsBusy        = true;
        StatusMessage = $"Changing password for '{PassUsername}'…";
        ChangePasswordCommand.NotifyCanExecuteChanged();
        try
        {
            var (ok, error) = await _db.ChangePasswordAsync(PassUsername, PassNewPassword);
            StatusMessage = ok
                ? $"Password changed for '{PassUsername}'."
                : $"Error: {error}";

            if (ok)
                PassUsername = PassNewPassword = PassConfirm = string.Empty;
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            ChangePasswordCommand.NotifyCanExecuteChanged();
        }
    }
    private bool CanChangePassword() =>
        !IsBusy &&
        !string.IsNullOrWhiteSpace(PassUsername) &&
        !string.IsNullOrWhiteSpace(PassNewPassword);

    // ── Helpers ───────────────────────────────────────────────────────────────

    private bool CanEdit()            => !IsBusy;
    private bool CanEditSelectedRealm() => !IsBusy && SelectedRealm is not null;

    private void NotifyRealmCommandsChanged()
    {
        RefreshRealmCommand.NotifyCanExecuteChanged();
        SaveRealmCommand.NotifyCanExecuteChanged();
        DeleteRealmCommand.NotifyCanExecuteChanged();
        OpenPublicCommand.NotifyCanExecuteChanged();
        OpenLocalCommand.NotifyCanExecuteChanged();
        DetectNetworkCommand.NotifyCanExecuteChanged();
        SetGmAccessCommand.NotifyCanExecuteChanged();
    }

    private RealmEntry BuildRealmFromFields() => new()
    {
        Id              = RealmId,
        Name            = RealmName,
        Address         = RealmAddress,
        LocalAddress    = RealmLocalAddress,
        LocalSubnetMask = RealmSubnetMask,
        Port            = int.TryParse(RealmPort,      out var p) ? p : 8085,
        GameBuild       = int.TryParse(RealmGameBuild, out var g) ? g : 12340,
    };
}
