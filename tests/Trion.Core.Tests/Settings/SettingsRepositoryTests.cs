using Microsoft.Data.Sqlite;
using Trion.Core.Settings;

namespace Trion.Core.Tests.Settings;

/// <summary>
/// Tests run against an in-memory SQLite database so no temp files are created.
/// </summary>
public sealed class SettingsRepositoryTests : IAsyncLifetime
{
    // Keep the connection open for the lifetime of the test so :memory: db persists
    private readonly SqliteConnection _connection;
    private readonly SettingsRepository _sut;

    public SettingsRepositoryTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        // Use the same connection string that SettingsRepository will use
        _sut = new SettingsRepository("Data Source=:memory:");
    }

    public async Task InitializeAsync()
    {
        await SettingsMigrations.RunAsync(_connection);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task GetAsync_ReturnsNull_WhenKeyNotSet()
    {
        var result = await _sut.GetAsync<string>("missing_key");
        Assert.Null(result);
    }

    [Fact]
    public async Task SetAndGet_RoundTrip_StringValue()
    {
        await _sut.SetAsync("username", "admin");
        var result = await _sut.GetAsync<string>("username");
        Assert.Equal("admin", result);
    }

    [Fact]
    public async Task SetAndGet_RoundTrip_ComplexType()
    {
        var value = new TestConfig { Host = "localhost", Port = 3306 };
        await _sut.SetAsync("db_config", value);

        var result = await _sut.GetAsync<TestConfig>("db_config");
        Assert.NotNull(result);
        Assert.Equal("localhost", result.Host);
        Assert.Equal(3306, result.Port);
    }

    [Fact]
    public async Task Set_Overwrites_ExistingValue()
    {
        await _sut.SetAsync("key", "first");
        await _sut.SetAsync("key", "second");

        var result = await _sut.GetAsync<string>("key");
        Assert.Equal("second", result);
    }

    [Fact]
    public async Task DeleteAsync_RemovesKey()
    {
        await _sut.SetAsync("to_delete", "value");
        await _sut.DeleteAsync("to_delete");

        var result = await _sut.GetAsync<string>("to_delete");
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_DoesNotThrow_WhenKeyMissing()
    {
        // Should be idempotent
        await _sut.DeleteAsync("nonexistent");
        Assert.True(true);
    }

    [Fact]
    public async Task Migrations_RunExactlyOnce()
    {
        // Running migrations again on an already-migrated DB must be a no-op
        await SettingsMigrations.RunAsync(_connection);
        await SettingsMigrations.RunAsync(_connection);

        // Verify version is still correct (schema_version should not exceed max migration)
        var version = await _connection.QuerySingleAsync<int>("SELECT version FROM schema_version");
        Assert.Equal(2, version);
    }

    // helper for Dapper query in test
    private static class ConnectionExtensions
    {
    }

    private sealed record TestConfig
    {
        public string Host { get; init; } = string.Empty;
        public int    Port { get; init; }
    }
}

// Minimal Dapper shim for the test — avoids adding Dapper to the test project directly
file static class SqliteConnectionExtensions
{
    public static async Task<T> QuerySingleAsync<T>(
        this SqliteConnection conn,
        string sql)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        var result = await cmd.ExecuteScalarAsync();
        return (T)Convert.ChangeType(result!, typeof(T));
    }
}
