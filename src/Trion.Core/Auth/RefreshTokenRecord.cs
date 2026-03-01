namespace Trion.Core.Auth;

public sealed record RefreshTokenRecord(
    string          Id,
    string          Username,    // plaintext — needed to re-issue JWT on refresh
    string          UserHash,    // SHA-256 of username — for indexed lookup
    string          TokenHash,   // SHA-256 of raw token
    string          IssuedAt,    // ISO-8601 string (matches SQLite storage)
    string          ExpiresAt,
    bool            Revoked,
    string          IssuingIp,
    string?         RevokedAt = null);
