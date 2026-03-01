namespace Trion.Core.Abstractions.Auth;

public sealed record AuthResult(
    bool Success,
    string? Username,
    int GmLevel,
    string? FailureReason)
{
    public static AuthResult Ok(string username, int gmLevel) =>
        new(true, username, gmLevel, null);

    public static AuthResult Fail(string reason) =>
        new(false, null, 0, reason);
}
