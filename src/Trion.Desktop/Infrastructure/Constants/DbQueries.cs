namespace Trion.Desktop.Infrastructure.Constants;

/// <summary>
/// All raw SQL statements used by <c>DatabaseService</c>.
/// Queries that differ between emulator schemas are exposed as static methods;
/// queries that are identical across all cores are plain string constants.
///
/// Schema variants (<c>CoreSchema</c>) map to <see cref="AppSettings.SelectedDatabases"/>:
///   AzerothCore  — 0 (Custom) and 2 (AzerothCore): binary salt/verifier columns
///   TrinityCore335 — 1 (TrinityCore 3.3.5a): sha_pass_hash column
///   CMaNGOS      — 3 (CMaNGOS/VMaNGOS): hex v/s columns, realmbuilds column
/// </summary>
internal static class DbQueries
{
    // ── Connectivity ──────────────────────────────────────────────────────────

    public const string Ping = "SELECT 1";

    // ── RealmList ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Fetches up to 10 realms.
    /// CMaNGOS uses <c>realmbuilds</c> instead of <c>gamebuild</c>
    /// and has no <c>localAddress</c> / <c>localSubnetMask</c> columns.
    /// </summary>
    public const string GetRealms =
        "SELECT id, name, address, localAddress, localSubnetMask, port, gamebuild " +
        "FROM realmlist LIMIT 10";

    public const string GetRealmsCmangos =
        "SELECT id, name, address, '' AS localAddress, '' AS localSubnetMask, " +
        "port, realmbuilds AS gamebuild " +
        "FROM realmlist LIMIT 10";

    public const string UpdateRealmAddress =
        "UPDATE realmlist SET address = @Address WHERE id = @Id";

    public const string DeleteRealm =
        "DELETE FROM realmlist WHERE id = @Id";

    /// <summary>UPDATE for AzerothCore / TrinityCore — includes localAddress and localSubnetMask.</summary>
    public const string SaveRealm =
        "UPDATE realmlist " +
        "SET name = @Name, address = @Address, localAddress = @LocalAddress, " +
        "localSubnetMask = @LocalSubnetMask, port = @Port, gamebuild = @GameBuild " +
        "WHERE id = @Id";

    public const string SaveRealmCmangos =
        "UPDATE realmlist " +
        "SET name = @Name, address = @Address, port = @Port, realmbuilds = @GameBuild " +
        "WHERE id = @Id";

    /// <summary>INSERT for AzerothCore / TrinityCore.</summary>
    public const string CreateRealm =
        "INSERT INTO realmlist (name, address, localAddress, localSubnetMask, port, gamebuild) " +
        "VALUES (@Name, @Address, @LocalAddress, @LocalSubnetMask, @Port, @GameBuild)";

    public const string CreateRealmCmangos =
        "INSERT INTO realmlist (name, address, port, realmbuilds) " +
        "VALUES (@Name, @Address, @Port, @GameBuild)";

    // ── Account — shared lookups ───────────────────────────────────────────────

    public const string UsernameExists =
        "SELECT COUNT(*) FROM account WHERE username = @Username";

    public const string EmailExists =
        "SELECT COUNT(*) FROM account WHERE email = @Email";

    public const string GetAccountId =
        "SELECT id FROM account WHERE username = @Username";

    // ── Account — CREATE ──────────────────────────────────────────────────────

    /// <summary>
    /// AzerothCore: binary salt + verifier columns, plus reg_mail and expansion.
    /// </summary>
    public const string CreateAccountAzerothCore =
        "INSERT INTO account " +
        "(username, salt, verifier, email, reg_mail, joindate, expansion) " +
        "VALUES (@Username, @Salt, @Verifier, @Email, @RegMail, @JoinDate, @Expansion)";

    /// <summary>
    /// TrinityCore 3.3.5a: single sha_pass_hash column (SHA1 hex of UPPER(user):UPPER(pass)).
    /// </summary>
    public const string CreateAccountTc335 =
        "INSERT INTO account " +
        "(username, sha_pass_hash, email, joindate, expansion) " +
        "VALUES (@Username, @ShaPassHash, @Email, @JoinDate, @Expansion)";

    /// <summary>
    /// CMaNGOS / VMaNGOS: hex-encoded v (verifier) and s (salt) columns, no expansion.
    /// </summary>
    public const string CreateAccountCmangos =
        "INSERT INTO account " +
        "(username, v, s, email, joindate) " +
        "VALUES (@Username, @Verifier, @Salt, @Email, @JoinDate)";

    // ── Account — SET GM LEVEL ────────────────────────────────────────────────

    /// <summary>
    /// AzerothCore / TrinityCore: UPSERT into the account_access table.
    /// </summary>
    public const string SetGmLevelAcTc =
        "INSERT INTO account_access (id, gmlevel, RealmID) " +
        "VALUES (@Id, @GmLevel, -1) " +
        "ON DUPLICATE KEY UPDATE gmlevel = @GmLevel";

    /// <summary>
    /// CMaNGOS: gmlevel lives directly on the account row.
    /// </summary>
    public const string SetGmLevelCmangos =
        "UPDATE account SET gmlevel = @GmLevel WHERE id = @Id";

    // ── Account — CHANGE PASSWORD ─────────────────────────────────────────────

    /// <summary>AzerothCore: replace binary salt and verifier.</summary>
    public const string ChangePasswordAzerothCore =
        "UPDATE account SET salt = @Salt, verifier = @Verifier WHERE username = @Username";

    /// <summary>TrinityCore 3.3.5a: replace the sha_pass_hash hex string.</summary>
    public const string ChangePasswordTc335 =
        "UPDATE account SET sha_pass_hash = @Hash WHERE username = @Username";

    /// <summary>CMaNGOS: replace hex v and s.</summary>
    public const string ChangePasswordCmangos =
        "UPDATE account SET v = @Verifier, s = @Salt WHERE username = @Username";

    // ══════════════════════════════════════════════════════════════════════════
    // CypherCore / TrinityCore 4.x — Battle.net auth schema
    //
    // Auth is split into two tables:
    //
    //   battlenet_accounts — top-level login identity (email + SRP6 v2 credentials)
    //     srp_version = 2
    //     salt        = 32 random bytes  (BINARY(32))
    //     verifier    = g^x mod N        (BLOB, up to 257 bytes for 2048-bit prime)
    //
    //   account — in-game session identity (GruntSRP6 credentials + BNet FK)
    //     salt      = 32 random bytes  (BINARY(32))
    //     verifier  = 7^x mod N_32     (BINARY(32), 32-byte WoW prime)
    //     username  = "{bnetId}#1"
    //     reg_mail  = email
    //
    // References: CypherCore BNetAccountManager.cs + AccountManager.cs
    //             LoginDatabase.cs INS_BNET_ACCOUNT / INS_ACCOUNT statements
    // ══════════════════════════════════════════════════════════════════════════

    // ── TC4 — duplicate-check lookups ─────────────────────────────────────────

    /// <summary>Check for an existing email in the Battle.net accounts table.</summary>
    public const string BnetEmailExists =
        "SELECT COUNT(*) FROM battlenet_accounts WHERE email = @Email";

    // ── TC4 — CREATE ──────────────────────────────────────────────────────────

    /// <summary>
    /// Insert a Battle.net top-level account.
    /// srp_version = 2 (PBKDF2-SHA512, 15 000 iterations, 2048-bit prime).
    /// </summary>
    public const string CreateBnetAccount =
        "INSERT INTO battlenet_accounts (email, srp_version, salt, verifier) " +
        "VALUES (@Email, @SrpVersion, @Salt, @Verifier)";

    /// <summary>
    /// Insert the linked in-game account using GruntSRP6 credentials.
    /// username  = "{bnetId}#1"
    /// reg_mail  = same as email
    /// </summary>
    public const string CreateTc4GameAccount =
        "INSERT INTO account " +
        "(username, salt, verifier, reg_mail, email, joindate, battlenet_account, battlenet_index) " +
        "VALUES (@Username, @Salt, @Verifier, @RegMail, @Email, NOW(), @BnetId, 1)";

    // ── TC4 — helpers ─────────────────────────────────────────────────────────

    /// <summary>Fetch the auto-generated ID of a newly created BNet account.</summary>
    public const string GetBnetAccountId =
        "SELECT id FROM battlenet_accounts WHERE email = @Email";

    /// <summary>
    /// Return the BNet account ID and email for the given game-account username.
    /// Both are needed to recompute the SRP6 verifier on password change.
    /// </summary>
    public const string GetBnetIdAndEmailByGameUsername =
        "SELECT ba.id, ba.email " +
        "FROM battlenet_accounts ba " +
        "INNER JOIN account a ON a.battlenet_account = ba.id " +
        "WHERE a.username = @Username";

    // ── TC4 — CHANGE PASSWORD ─────────────────────────────────────────────────

    /// <summary>
    /// Replace the SRP6 credentials on the BNet account row.
    /// Lookup is by BNet account ID (obtained separately via GetBnetIdAndEmailByGameUsername).
    /// </summary>
    public const string ChangePasswordBnet =
        "UPDATE battlenet_accounts " +
        "SET srp_version = @SrpVersion, salt = @Salt, verifier = @Verifier " +
        "WHERE id = @BnetId";
}
