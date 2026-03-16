using Dapper;
using MySqlConnector;

namespace Trion.API.Data;

/// <summary>
/// Lightweight Dapper wrapper with automatic retry on transient MySQL errors.
/// Registered as a singleton — uses pooled connections per operation.
/// </summary>
public sealed class TrionDbAccess
{
    private readonly string              _cs;
    private readonly ILogger<TrionDbAccess> _log;
    private const    int                 MaxRetries = 3;

    public TrionDbAccess(IConfiguration cfg, ILogger<TrionDbAccess> log)
    {
        _cs  = cfg.GetConnectionString("Default")
            ?? throw new InvalidOperationException("ConnectionStrings:Default is not configured.");
        _log = log;
    }

    public Task<List<T>>  QueryAsync<T>(string sql, object? p = null)
        => RunAsync(async cn => (await cn.QueryAsync<T>(sql, p)).AsList(), []);

    public Task<T?>  QuerySingleAsync<T>(string sql, object? p = null)
        => RunAsync(cn => cn.QueryFirstOrDefaultAsync<T>(sql, p), default);

    public Task<int> ExecuteAsync(string sql, object? p = null)
        => RunAsync(cn => cn.ExecuteAsync(sql, p), 0);

    // ── Core retry wrapper ────────────────────────────────────────────────────

    private async Task<T> RunAsync<T>(Func<MySqlConnection, Task<T>> op, T fallback)
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            try
            {
                await using var cn = new MySqlConnection(_cs);
                await cn.OpenAsync();
                return await op(cn);
            }
            catch (MySqlException ex) when (IsTransient(ex) && attempt < MaxRetries)
            {
                _log.LogWarning("Transient DB error (attempt {A}/{Max}): {Msg}",
                    attempt, MaxRetries, ex.Message);
                await Task.Delay(TimeSpan.FromSeconds(attempt));
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "DB operation failed on attempt {A}", attempt);
                break;
            }
        }
        return fallback;
    }

    private static bool IsTransient(MySqlException ex) =>
        ex.Number is 1205   // lock wait timeout
                  or 1213   // deadlock found
                  or 1042   // can't get hostname
                  or 1043;  // bad handshake
}
