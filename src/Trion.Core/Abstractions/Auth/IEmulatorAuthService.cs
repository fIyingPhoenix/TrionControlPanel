using Trion.Core.Abstractions.Services;

namespace Trion.Core.Abstractions.Auth;

public interface IEmulatorAuthService
{
    Task<AuthResult> AuthenticateAsync(
        string username,
        string password,
        EmulatorType emulatorType,
        CancellationToken ct = default);
}
