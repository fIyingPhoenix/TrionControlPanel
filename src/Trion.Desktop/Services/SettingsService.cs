using Microsoft.Data.Sqlite;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Persists <see cref="AppSettings"/> to a SQLite database (Settings.db) beside the exe.
/// The whole settings object is stored as a single JSON blob in a one-row AppConfig table.
/// Load() is called once at startup; Save() is called on explicit user save and on app exit.
/// </summary>
public class SettingsService : ISettingsService
{
    private static readonly string DbPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "Settings.db");

    private static readonly string ConnectionString =
        $"Data Source={DbPath};Cache=Shared;";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented            = true,
        NumberHandling           = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        DefaultIgnoreCondition   = JsonIgnoreCondition.Never,
    };

    private const string EnsureTable = """
        CREATE TABLE IF NOT EXISTS AppConfig (
            Id   INTEGER PRIMARY KEY CHECK (Id = 1),
            Data TEXT    NOT NULL
        )
        """;

    public AppSettings Current { get; private set; } = new();

    // ── Public API ────────────────────────────────────────────────────────────

    public void Load()
    {
        using var conn = Open();
        EnsureSchema(conn);

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Data FROM AppConfig WHERE Id = 1";
        var blob = cmd.ExecuteScalar() as string;

        if (blob is null)
        {
            // First run — write defaults so the row exists for future saves
            Current = new AppSettings();
            WriteBlob(conn, JsonSerializer.Serialize(Current, JsonOptions));
        }
        else
        {
            try
            {
                Current = JsonSerializer.Deserialize<AppSettings>(blob, JsonOptions)
                          ?? new AppSettings();
            }
            catch
            {
                // Corrupt blob — reset to defaults
                Current = new AppSettings();
                WriteBlob(conn, JsonSerializer.Serialize(Current, JsonOptions));
            }
        }
    }

    public void Save()
    {
        using var conn = Open();
        EnsureSchema(conn);
        WriteBlob(conn, JsonSerializer.Serialize(Current, JsonOptions));
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static SqliteConnection Open()
    {
        var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    private static void EnsureSchema(SqliteConnection conn)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = EnsureTable;
        cmd.ExecuteNonQuery();
    }

    private static void WriteBlob(SqliteConnection conn, string json)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT OR REPLACE INTO AppConfig (Id, Data) VALUES (1, @data)";
        cmd.Parameters.AddWithValue("@data", json);
        cmd.ExecuteNonQuery();
    }
}
