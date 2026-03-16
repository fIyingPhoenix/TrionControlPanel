using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public record LoginResult(bool Success, string? Error = null);

public interface IAccountService
{
    string?         Username   { get; }
    string?         Email      { get; }
    bool            IsLoggedIn { get; }
    bool            IsGuest    { get; }
    AccountProfile? Profile    { get; }

    event Action? SessionChanged;

    Task<LoginResult> LoginAsync(string username, string password, bool rememberMe = false, CancellationToken ct = default);
    void Logout();
    void ContinueAsGuest();
}
