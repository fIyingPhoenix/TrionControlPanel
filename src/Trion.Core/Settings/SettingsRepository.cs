using System.Text.Json;
using Dapper;
using Microsoft.Data.Sqlite;
using Trion.Core.Abstractions.Settings;

namespace Trion.Core.Settings;

/// <summary>
/// SQLite-backed implementation of <see cref="ISettingsRepository"/>.
/// All values are stored as JSON strings in the <c>settings</c> table.
/// </summary>
public sealed class SettingsRepository : ISettingsRepository
{
    private readonly string _connectionString;

    public SettingsRepository(string dbPath)
    {
        _connectionString = $"Data Source={dbPath};";
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        await using var db = new SqliteConnection(_connectionString);
        var json = await db.QuerySingleOrDefaultAsync<string?>(
            "SELECT value FROM settings WHERE key = @Key",
            new { Key = key });

        if (json is null) return default;
        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(value);
        await using var db = new SqliteConnection(_connectionString);
        await db.ExecuteAsync(
            """
            INSERT INTO settings (key, value, updated_at)
            VALUES (@Key, @Value, @UpdatedAt)
            ON CONFLICT(key) DO UPDATE SET value = excluded.value, updated_at = excluded.updated_at
            """,
            new
            {
                Key       = key,
                Value     = json,
                UpdatedAt = DateTimeOffset.UtcNow.ToString("O")
            });
    }

    public async Task DeleteAsync(string key, CancellationToken ct = default)
    {
        await using var db = new SqliteConnection(_connectionString);
        await db.ExecuteAsync(
            "DELETE FROM settings WHERE key = @Key",
            new { Key = key });
    }
}
