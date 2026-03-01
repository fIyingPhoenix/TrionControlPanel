namespace Trion.Core.Abstractions.Auth;

public interface ITokenService
{
    Task<TokenPair> IssueTokensAsync(AuthenticatedUser user, CancellationToken ct = default);
    Task<TokenPair?> RefreshAsync(string refreshToken, string ipAddress, CancellationToken ct = default);
    Task RevokeAsync(string refreshToken, CancellationToken ct = default);
}
