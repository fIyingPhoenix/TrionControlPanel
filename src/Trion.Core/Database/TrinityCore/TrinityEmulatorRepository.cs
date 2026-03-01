using Dapper;
using MySqlConnector;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Database.Models;

namespace Trion.Core.Database.TrinityCore;

/// <summary>
/// IEmulatorRepository for TrinityCore (wotlk 3.3.5a).
/// Auth DB schema: account + account_access (gmlevel stored separately).
/// </summary>
internal class TrinityEmulatorRepository : IEmulatorRepository
{
    private readonly string  _authCs;
    private readonly string? _charCs;
    private readonly string? _worldCs;

    internal TrinityEmulatorRepository(string authCs, string? charCs, string? worldCs)
    {
        _authCs  = authCs;
        _charCs  = charCs;
        _worldCs = worldCs;
    }

    // ── Account ──────────────────────────────────────────────────────────────

    public async Task<AccountRecord?> GetAccountAsync(string username, CancellationToken ct = default)
    {
        const string sql = """
            SELECT a.id              AS Id,
                   a.username        AS Username,
                   a.email           AS Email,
                   COALESCE(aa.gmlevel, 0) AS GmLevel,
                   a.joindate        AS CreatedAt,
                   a.locked          AS IsLocked,
                   a.sha_pass_hash   AS PasswordHash
            FROM   account a
            LEFT JOIN account_access aa ON aa.id = a.id AND aa.RealmID = -1
            WHERE  a.username = @Username
            LIMIT  1
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var row = await conn.QueryFirstOrDefaultAsync<AccountRow>(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));
        return row is null ? null : MapAccount(row);
    }

    public async Task<AccountRecord> CreateAccountAsync(
        string username, string passwordHash, string email, int gmLevel,
        CancellationToken ct = default)
    {
        const string insertAccount = """
            INSERT INTO account (username, sha_pass_hash, email, joindate, expansion)
            VALUES (@Username, @PasswordHash, @Email, NOW(), 2);
            SELECT LAST_INSERT_ID();
            """;

        const string insertAccess = """
            INSERT INTO account_access (id, gmlevel, RealmID)
            VALUES (@Id, @GmLevel, -1)
            ON DUPLICATE KEY UPDATE gmlevel = @GmLevel;
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        await using var tx = await conn.BeginTransactionAsync(ct);

        var id = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(insertAccount,
                new { Username = username.ToUpperInvariant(), PasswordHash = passwordHash, Email = email },
                transaction: tx, cancellationToken: ct));

        if (gmLevel > 0)
            await conn.ExecuteAsync(
                new CommandDefinition(insertAccess,
                    new { Id = id, GmLevel = gmLevel },
                    transaction: tx, cancellationToken: ct));

        await tx.CommitAsync(ct);
        return new AccountRecord(id, username, email, gmLevel, DateTimeOffset.UtcNow, false, null);
    }

    public async Task<bool> UpdateGmLevelAsync(string username, int newLevel, CancellationToken ct = default)
    {
        const string sql = """
            INSERT INTO account_access (id, gmlevel, RealmID)
            SELECT id, @GmLevel, -1 FROM account WHERE username = @Username
            ON DUPLICATE KEY UPDATE gmlevel = @GmLevel;
            """;

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
            SELECT a.id              AS Id,
                   a.username        AS Username,
                   a.email           AS Email,
                   COALESCE(aa.gmlevel, 0) AS GmLevel,
                   a.joindate        AS CreatedAt,
                   a.locked          AS IsLocked,
                   a.sha_pass_hash   AS PasswordHash
            FROM   account a
            LEFT JOIN account_access aa ON aa.id = a.id AND aa.RealmID = -1
            ORDER  BY a.id
            LIMIT  @Limit OFFSET @Offset
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var total = await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(countSql, cancellationToken: ct));
        var rows = await conn.QueryAsync<AccountRow>(
            new CommandDefinition(rowsSql,
                new { Limit = pageSize, Offset = (page - 1) * pageSize },
                cancellationToken: ct));
        return new Page<AccountRecord>(rows.Select(MapAccount).ToList(), total, page, pageSize);
    }

    // ── GM Level ─────────────────────────────────────────────────────────────

    public async Task<int> GetGmLevelAsync(string username, CancellationToken ct = default)
    {
        const string sql = """
            SELECT COALESCE(aa.gmlevel, 0)
            FROM   account a
            LEFT JOIN account_access aa ON aa.id = a.id AND aa.RealmID = -1
            WHERE  a.username = @Username
            LIMIT  1
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        return await conn.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, new { Username = username.ToUpperInvariant() }, cancellationToken: ct));
    }

    // ── Realms ───────────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<RealmRecord>> GetRealmsAsync(CancellationToken ct = default)
    {
        // flag & 2 is the "offline" bit in TrinityCore realmlist
        const string sql = """
            SELECT id                       AS Id,
                   name                     AS Name,
                   address                  AS Address,
                   port                     AS Port,
                   ((flag & 2) = 0)         AS Online,
                   CAST(population AS SIGNED) AS Population
            FROM realmlist
            ORDER BY id
            """;

        await using var conn = new MySqlConnection(_authCs);
        await conn.OpenAsync(ct);
        var rows = await conn.QueryAsync<RealmRow>(
            new CommandDefinition(sql, cancellationToken: ct));
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

    public async Task<IReadOnlyList<GmTicket>> ListGmTicketsAsync(CancellationToken ct = default)
    {
        if (_worldCs is null) return [];

        // closedBy: 0 = open, -1 = deleted, positive = GM who closed it
        const string sql = """
            SELECT id          AS Id,
                   name        AS AuthorName,
                   description AS Text,
                   createTime  AS CreatedAt,
                   (closedBy != 0) AS Closed,
                   assignedTo  AS AssignedTo
            FROM gm_ticket
            WHERE closedBy > -1
            ORDER BY createTime DESC
            """;

        await using var conn = new MySqlConnection(_worldCs);
        await conn.OpenAsync(ct);
        var rows = await conn.QueryAsync<TicketRow>(new CommandDefinition(sql, cancellationToken: ct));
        return rows.Select(r => new GmTicket(
            r.Id, r.AuthorName, r.Text,
            DateTimeOffset.FromUnixTimeSeconds(r.CreatedAt),
            r.Closed,
            r.AssignedTo > 0 ? r.AssignedTo.ToString() : null)).ToList();
    }

    public async Task<bool> CloseTicketAsync(int ticketId, CancellationToken ct = default)
    {
        if (_worldCs is null) return false;

        // Set closedBy = -1 to mark resolved/closed from the control panel
        const string sql = """
            UPDATE gm_ticket
            SET closedBy = -1
            WHERE id = @Id AND closedBy = 0
            """;

        await using var conn = new MySqlConnection(_worldCs);
        await conn.OpenAsync(ct);
        var affected = await conn.ExecuteAsync(
            new CommandDefinition(sql, new { Id = ticketId }, cancellationToken: ct));
        return affected > 0;
    }

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

    // ── Private helpers ───────────────────────────────────────────────────────

    private static AccountRecord MapAccount(AccountRow r) =>
        new(r.Id, r.Username, r.Email, r.GmLevel,
            new DateTimeOffset(r.CreatedAt, TimeSpan.Zero),
            r.IsLocked, r.PasswordHash);

    // ── Dapper POCOs ─────────────────────────────────────────────────────────

    private sealed class AccountRow
    {
        public int      Id           { get; set; }
        public string   Username     { get; set; } = "";
        public string   Email        { get; set; } = "";
        public int      GmLevel      { get; set; }
        public DateTime CreatedAt    { get; set; }
        public bool     IsLocked     { get; set; }
        public string?  PasswordHash { get; set; }
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

    private sealed class TicketRow
    {
        public int    Id         { get; set; }
        public string AuthorName { get; set; } = "";
        public string Text       { get; set; } = "";
        public long   CreatedAt  { get; set; }
        public bool   Closed     { get; set; }
        public int    AssignedTo { get; set; }
    }
}
