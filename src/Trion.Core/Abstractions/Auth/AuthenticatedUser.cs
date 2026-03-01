namespace Trion.Core.Abstractions.Auth;

public sealed record AuthenticatedUser(
    string Username,
    int GmLevel,
    string IpAddress);
