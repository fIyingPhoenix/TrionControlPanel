namespace Trion.Core.Abstractions.Auth;

public sealed record TokenPair(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn);
