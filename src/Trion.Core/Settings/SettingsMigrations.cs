using System.Data;
using Dapper;

namespace Trion.Core.Settings;

/// <summary>
/// Forward-only schema migrations for the settings SQLite database.
/// Each migration runs inside a transaction and is recorded in <c>schema_version</c>.
/// </summary>
public static class SettingsMigrations
{
    private static readonly (int Version, string Sql)[] Migrations =
    [
        (1, """
            CREATE TABLE IF NOT EXISTS settings (
                key        TEXT NOT NULL PRIMARY KEY,
                value      TEXT NOT NULL,
                updated_at TEXT NOT NULL
            );
            """),

        (2, """
            CREATE TABLE IF NOT EXISTS refresh_tokens (
                id         TEXT NOT NULL PRIMARY KEY,
                username   TEXT NOT NULL,
                user_hash  TEXT NOT NULL,
                token_hash TEXT NOT NULL,
                issued_at  TEXT NOT NULL,
                expires_at TEXT NOT NULL,
                revoked    INTEGER NOT NULL DEFAULT 0,
                revoked_at TEXT,
                issuing_ip TEXT NOT NULL
            );
            CREATE INDEX IF NOT EXISTS idx_refresh_tokens_token_hash
                ON refresh_tokens(token_hash);
            """),
    ];

    public static async Task RunAsync(IDbConnection db)
    {
        // Create schema_version table if it does not exist
        await db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS schema_version (
                version INTEGER NOT NULL
            );
            INSERT INTO schema_version (version)
            SELECT 0 WHERE NOT EXISTS (SELECT 1 FROM schema_version);
            """);

        foreach (var (version, sql) in Migrations)
        {
            var current = await db.QuerySingleAsync<int>("SELECT version FROM schema_version");
            if (current >= version) continue;

            using var tx = db.BeginTransaction();
            try
            {
                await db.ExecuteAsync(sql, transaction: tx);
                await db.ExecuteAsync(
                    "UPDATE schema_version SET version = @Version",
                    new { Version = version },
                    transaction: tx);
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
