using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAccountService     _account;
    private readonly ILocalizationService _loc;

    [ObservableProperty] private string _username      = string.Empty;
    [ObservableProperty] private string _password      = string.Empty;
    [ObservableProperty] private string _errorMessage  = string.Empty;
    [ObservableProperty] private bool   _isBusy;
    [ObservableProperty] private bool   _rememberMe    = false;

    public ILocalizationService Loc => _loc;

    public static string AppVersion =>
        Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "—";

    /// <summary>Set to true when login succeeds; the window listens and closes.</summary>
    public bool LoginSucceeded { get; private set; }

    /// <summary>Set to true when the user chooses to continue without an account.</summary>
    public bool SkippedLogin { get; private set; }

    public event Action? RequestClose;

    public LoginViewModel(IAccountService account, ILocalizationService loc)
    {
        _account = account;
        _loc     = loc;
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        ErrorMessage = string.Empty;
        IsBusy = true;

        var result = await _account.LoginAsync(Username.Trim(), Password, RememberMe);

        IsBusy = false;

        if (result.Success)
        {
            LoginSucceeded = true;
            RequestClose?.Invoke();
        }
        else
        {
            ErrorMessage = result.Error ?? "Login failed.";
        }
    }

    private bool CanLogin() => !IsBusy && !string.IsNullOrWhiteSpace(Username) && Password.Length > 0;

    partial void OnUsernameChanged(string value) => LoginCommand.NotifyCanExecuteChanged();
    partial void OnPasswordChanged(string value) => LoginCommand.NotifyCanExecuteChanged();
    partial void OnIsBusyChanged(bool value)     => LoginCommand.NotifyCanExecuteChanged();

    [RelayCommand]
    private void ContinueOffline()
    {
        SkippedLogin = true;
        _account.ContinueAsGuest();
        RequestClose?.Invoke();
    }
}
