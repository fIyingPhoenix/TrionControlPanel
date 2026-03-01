namespace Trion.Core.Abstractions.Database.Models;

public sealed record AccountRecord(
    int            Id,
    string         Username,
    string         Email,
    int            GmLevel,
    DateTimeOffset CreatedAt,
    bool           IsLocked     = false,
    string?        PasswordHash = null);   // raw stored hash — used by EmulatorAuthService for comparison
