using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Trion.Core.Auth;

public sealed class RefreshTokenStore
{
    private readonly string _connectionString;

    public RefreshTokenStore(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task StoreAsync(
        string          userId,
        string          rawToken,
        string          ipAddress,
        DateTimeOffset  expiry,
        CancellationToken ct = default)
    {
        var id        = Guid.NewGuid().ToString();
        var userHash  = HashValue(userId);
        var tokenHash = HashValue(rawToken);

        await using var db = new SqliteConnection(_connectionString);
        await db.ExecuteAsync(
            """
            INSERT INTO refresh_tokens
                (id, username, user_hash, token_hash, issued_at, expires_at, revoked, issuing_ip)
            VALUES
                (@Id, @Username, @UserHash, @TokenHash, @IssuedAt, @ExpiresAt, 0, @IssuingIp)
            """,
            new
            {
                Id        = id,
                Username  = userId,
                UserHash  = userHash,
                TokenHash = tokenHash,
                IssuedAt  = DateTimeOffset.UtcNow.ToString("O"),
                ExpiresAt = expiry.ToString("O"),
                IssuingIp = ipAddress
            });
    }

    public async Task<RefreshTokenRecord?> FindAsync(
        string rawToken,
        CancellationToken ct = default)
    {
        var tokenHash = HashValue(rawToken);

        await using var db = new SqliteConnection(_connectionString);
        return await db.QuerySingleOrDefaultAsync<RefreshTokenRecord>(
            """
            SELECT id, username, user_hash, token_hash,
                   issued_at, expires_at, revoked, issuing_ip, revoked_at
            FROM refresh_tokens
            WHERE token_hash = @TokenHash
            """,
            new { TokenHash = tokenHash });
    }

    public async Task RevokeAsync(string rawToken, CancellationToken ct = default)
    {
        var tokenHash = HashValue(rawToken);

        await using var db = new SqliteConnection(_connectionString);
        await db.ExecuteAsync(
            """
            UPDATE refresh_tokens
            SET revoked = 1, revoked_at = @RevokedAt
            WHERE token_hash = @TokenHash
            """,
            new { TokenHash = tokenHash, RevokedAt = DateTimeOffset.UtcNow.ToString("O") });
    }

    public async Task PurgeExpiredAsync(CancellationToken ct = default)
    {
        await using var db = new SqliteConnection(_connectionString);
        await db.ExecuteAsync(
            "DELETE FROM refresh_tokens WHERE expires_at < @Now",
            new { Now = DateTimeOffset.UtcNow.ToString("O") });
    }

    private static string HashValue(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
