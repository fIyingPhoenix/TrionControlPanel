namespace Trion.API.Models;

public sealed class User
{
    public int       ID           { get; init; }
    public string    Username     { get; init; } = "";
    public string    Email        { get; init; } = "";
    public string    PasswordHash { get; init; } = "";
    public string    Role         { get; init; } = "user";
    public bool      IsActive     { get; init; }
    public bool      IsBanned     { get; init; }
    public string    ApiKey       { get; init; } = "";
    public string    ApiTier      { get; init; } = "free";
    public DateTime? LastLogin    { get; init; }
    public DateTime  CreatedAt    { get; init; }
}
