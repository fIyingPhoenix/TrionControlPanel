using Dapper;
using MySqlConnector;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Database.Models;

namespace Trion.Core.Database.CMaNGOS;

/// <summary>
/// IEmulatorRepository for CMaNGOS (classic / TBC / WotLK variants).
/// Key schema difference: gmlevel is stored directly in the account table,
/// not in a separate account_access table.  Passwords use SRP6 (v/s fields);
/// PasswordHash is returned as null — auth falls back to the local account.
/// </summary>
internal sealed class CMaNGOSEmulatorRepository : IEmulatorRepository
{
    private readonly string  _authCs;
    private readonly string? _charCs;

    internal CMaNGOSEmulatorRepository(string authCs, string? charCs)
    {
        _authCs = authCs;
        _charCs = charCs;
    }

    // ── Account ──────────────────────────────────────────────────────────────

    public async Task<AccountRecord?> GetAccountAsync(string username, CancellationToken ct = default)
    {
        const string sql = """
            SELECT id       AS Id,
                   username AS Username,
                   email    AS Email,
                   gmlevel  AS GmLevel,
                   joindate AS CreatedAt,
                   locked   AS IsLocked
            FROM account
            WHERE username = @Username
            LIMIT 1
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var row = await conn.QueryFirstOrDefaultAsync<AccountRow>(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));

        if (row is null) return null;
        // CMaNGOS uses SRP6 — no SHA1 hash to compare; return null so auth service
        // falls through to the local bootstrap account path
        return new AccountRecord(row.Id, row.Username, row.Email, row.GmLevel,
            new DateTimeOffset(row.CreatedAt, TimeSpan.Zero), row.IsLocked, PasswordHash: null);
    }

    public async Task<AccountRecord> CreateAccountAsync(
        string username, string passwordHash, string email, int gmLevel,
        CancellationToken ct = default)
    {
        const string sql = """
            INSERT INTO account (username, gmlevel, email, joindate)
            VALUES (@Username, @GmLevel, @Email, NOW());
            SELECT LAST_INSERT_ID();
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var id = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(sql,
                new { Username = username.ToUpperInvariant(), GmLevel = gmLevel, Email = email },
                cancellationToken: ct));
        return new AccountRecord(id, username, email, gmLevel, DateTimeOffset.UtcNow, false, null);
    }

    public async Task<bool> UpdateGmLevelAsync(string username, int newLevel, CancellationToken ct = default)
    {
        const string sql = "UPDATE account SET gmlevel = @GmLevel WHERE username = @Username";
        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(
            new CommandDefinition(sql,
                new { Username = username.ToUpperInvariant(), GmLevel = newLevel },
                cancellationToken: ct));
        return affected > 0;
    }

    public async Task<bool> DeleteAccountAsync(string username, CancellationToken ct = default)
    {
        const string sql = "DELETE FROM account WHERE username = @Username";
        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));
        return affected > 0;
    }

    public async Task<Page<AccountRecord>> ListAccountsAsync(
        int page, int pageSize, CancellationToken ct = default)
    {
        const string countSql = "SELECT COUNT(*) FROM account";
        const string rowsSql = """
            SELECT id       AS Id,
                   username AS Username,
                   email    AS Email,
                   gmlevel  AS GmLevel,
                   joindate AS CreatedAt,
                   locked   AS IsLocked
            FROM account
            ORDER BY id
            LIMIT @Limit OFFSET @Offset
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var total = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(countSql, cancellationToken: ct));
        var rows = await conn.QueryAsync<AccountRow>(
            new CommandDefinition(rowsSql,
                new { Limit = pageSize, Offset = (page - 1) * pageSize },
                cancellationToken: ct));
        return new Page<AccountRecord>(
            rows.Select(r => new AccountRecord(r.Id, r.Username, r.Email, r.GmLevel,
                new DateTimeOffset(r.CreatedAt, TimeSpan.Zero), r.IsLocked, null)).ToList(),
            total, page, pageSize);
    }

    // ── GM Level ─────────────────────────────────────────────────────────────

    public async Task<int> GetGmLevelAsync(string username, CancellationToken ct = default)
    {
        const string sql = "SELECT gmlevel FROM account WHERE username = @Username LIMIT 1";
        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        return await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));
    }

    // ── Realms ───────────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<RealmRecord>> GetRealmsAsync(CancellationToken ct = default)
    {
        // CMaNGOS uses 'realmflags' (bit 1 = offline) instead of 'flag'
        const string sql = """
            SELECT id                           AS Id,
                   name                         AS Name,
                   address                      AS Address,
                   port                         AS Port,
                   ((realmflags & 2) = 0)       AS Online,
                   CAST(population AS SIGNED)   AS Population
            FROM realmlist
            ORDER BY id
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var rows = await conn.QueryAsync<RealmRow>(new CommandDefinition(sql, cancellationToken: ct));
        return rows.Select(r => new RealmRecord(r.Id, r.Name, r.Address, r.Port, r.Online, r.Population)).ToList();
    }

    public async Task<bool> UpdateRealmAddressAsync(int realmId, string address, CancellationToken ct = default)
    {
        const string sql = "UPDATE realmlist SET address = @Address WHERE id = @Id";
        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(
            new CommandDefinition(sql, new { Address = address, Id = realmId }, cancellationToken: ct));
        return affected > 0;
    }

    // ── Characters ───────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<CharacterSummary>> GetCharacterSummariesAsync(
        int accountId, CancellationToken ct = default)
    {
        if (_charCs is null) return [];

        const string sql = """
            SELECT guid     AS Id,
                   name     AS Name,
                   level    AS Level,
                   race     AS Race,
                   class    AS Class,
                   account  AS AccountId
            FROM characters
            WHERE account = @AccountId
            ORDER BY level DESC
            """;

        await using var conn = new MySqlConnection(_charCs);
        await conn.OpenAsync(ct);
        var rows = await conn.QueryAsync<CharRow>(
            new CommandDefinition(sql, new { AccountId = accountId }, cancellationToken: ct));
        return rows.Select(r => new CharacterSummary(r.Id, r.Name, r.Level, r.Race, r.Class, r.AccountId)).ToList();
    }

    // ── Sessions ─────────────────────────────────────────────────────────────

    public async Task<int> GetOnlineCountAsync(CancellationToken ct = default)
    {
        const string sql = "SELECT COUNT(*) FROM account WHERE online = 1";
        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        return await conn.ExecuteScalarAsync<int>(new CommandDefinition(sql, cancellationToken: ct));
    }

    public async Task<IReadOnlyList<SessionRecord>> ListActiveSessionsAsync(CancellationToken ct = default)
    {
        const string sql = """
            SELECT username   AS Username,
                   last_ip    AS IpAddress,
                   last_login AS LoginTime
            FROM account
            WHERE online = 1
            ORDER BY last_login DESC
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var rows = await conn.QueryAsync<SessionRow>(new CommandDefinition(sql, cancellationToken: ct));
        return rows.Select(r => new SessionRecord(
            r.Username, r.IpAddress, new DateTimeOffset(r.LoginTime, TimeSpan.Zero))).ToList();
    }

    // ── GM Tickets ───────────────────────────────────────────────────────────
    // CMaNGOS stores tickets in the world/character DB; not wired up here.

    public Task<IReadOnlyList<GmTicket>> ListGmTicketsAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<GmTicket>>([]);

    public Task<bool> CloseTicketAsync(int ticketId, CancellationToken ct = default)
        => Task.FromResult(false);

    // ── Bans ─────────────────────────────────────────────────────────────────

    public async Task<bool> BanAccountAsync(BanRecord ban, CancellationToken ct = default)
    {
        const string sql = """
            INSERT INTO account_banned (id, bandate, unbandate, bannedby, banreason, active)
            SELECT id, @BanDate, @UnbanDate, @BannedBy, @BanReason, 1
            FROM   account WHERE username = @Username
            ON DUPLICATE KEY UPDATE
                bandate   = @BanDate,
                unbandate = @UnbanDate,
                bannedby  = @BannedBy,
                banreason = @BanReason,
                active    = 1;
            """;

        var banDate   = ban.BannedAt.ToUnixTimeSeconds();
        var unbanDate = ban.IsPermanent ? 0L : ban.ExpiresAt.ToUnixTimeSeconds();

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(new CommandDefinition(sql,
            new {
                Username  = ban.BannedUsername.ToUpperInvariant(),
                BanDate   = banDate,
                UnbanDate = unbanDate,
                BannedBy  = ban.BannedBy,
                BanReason = ban.BanReason
            }, cancellationToken: ct));
        return affected > 0;
    }

    public async Task<bool> UnbanAccountAsync(string username, CancellationToken ct = default)
    {
        const string sql = """
            UPDATE account_banned ab
            JOIN   account a ON a.id = ab.id
            SET    ab.active = 0
            WHERE  a.username = @Username AND ab.active = 1
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));
        return affected > 0;
    }

    // ── Dapper POCOs ─────────────────────────────────────────────────────────

    private sealed class AccountRow
    {
        public int      Id        { get; set; }
        public string   Username  { get; set; } = "";
        public string   Email     { get; set; } = "";
        public int      GmLevel   { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool     IsLocked  { get; set; }
    }

    private sealed class RealmRow
    {
        public int    Id         { get; set; }
        public string Name       { get; set; } = "";
        public string Address    { get; set; } = "";
        public int    Port       { get; set; }
        public bool   Online     { get; set; }
        public int    Population { get; set; }
    }

    private sealed class CharRow
    {
        public int    Id        { get; set; }
        public string Name      { get; set; } = "";
        public int    Level     { get; set; }
        public int    Race      { get; set; }
        public int    Class     { get; set; }
        public int    AccountId { get; set; }
    }

    private sealed class SessionRow
    {
        public string   Username  { get; set; } = "";
        public string   IpAddress { get; set; } = "";
        public DateTime LoginTime { get; set; }
    }
}
