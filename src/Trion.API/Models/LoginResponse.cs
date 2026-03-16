namespace Trion.API.Models;

public sealed record LoginResponse(
    string  Token,
    int     Id,
    string  Username,
    string  Email,
    string  Role,
    bool    IsActive,
    bool    IsBanned,
    string? LastLogin,
    string  CreatedAt,
    string  ApiKey,
    string  ApiTier);
