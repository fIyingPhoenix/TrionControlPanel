using MySqlConnector;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Real MySQL implementation of <see cref="IDatabaseService"/>.
/// Supports AzerothCore (LegacySHA1 SRP6), TrinityCore 3.3.5a (sha_pass_hash),
/// and CMaNGOS/VMaNGOS (CMaNGOS SRP6 with hex-encoded v/s columns).
///
/// The active schema is determined by <see cref="AppSettings.SelectedDatabases"/>:
///   0 = Custom      → AzerothCore schema (safe default for SPP)
///   1 = TrinityCore → sha_pass_hash (TC 3.3.5a)
///   2 = AzerothCore → LegacySHA1 SRP6 (binary salt + verifier columns)
///   3 = CMaNGOS     → CMaNGOS SRP6 (hex v/s columns, realmbuilds column)
///
/// All SQL strings are defined in <see cref="DbQueries"/>.
/// </summary>
public sealed class DatabaseService : IDatabaseService
{
    private enum CoreSchema { AzerothCore, TrinityCore335, CMaNGOS, TrinityCore4 }

    private readonly ISettingsService _settings;

    public DatabaseService(ISettingsService settings) => _settings = settings;

    // ── Connection strings ────────────────────────────────────────────────────

    private string AuthConnectionString() =>
        BuildConnectionString(
            _settings.Current.DBServerHost,
            _settings.Current.DBServerPort,
            _settings.Current.DBServerUser,
            _settings.Current.DBServerPassword,
            _settings.Current.AuthDatabase);

    private static string BuildConnectionString(
        string host, string portStr, string user, string password, string database)
    {
        uint port = uint.TryParse(portStr, out var p) ? p : 3306u;
        return new MySqlConnectionStringBuilder
        {
            Server             = host,
            Port               = port,
            UserID             = user,
            Password           = password,
            Database           = database,
            ConnectionTimeout  = 10,
            AllowUserVariables = true,
        }.ConnectionString;
    }

    private CoreSchema ActiveSchema() => _settings.Current.SelectedDatabases switch
    {
        0 => CoreSchema.TrinityCore335,  // 0 = TrinityCore 3.3.5a
        2 => CoreSchema.CMaNGOS,         // 2 = CMaNGOS / VMaNGOS
        3 => CoreSchema.TrinityCore4,    // 3 = TrinityCore 4.x / Latest (Battle.net auth)
        _ => CoreSchema.AzerothCore,     // 1 = AzerothCore (default)
    };

    // ── PingAsync ─────────────────────────────────────────────────────────────

    public Task<(bool Success, string Message, TimeSpan Latency)> PingAsync(
        CancellationToken ct = default) =>
        TestConnectionAsync(
            _settings.Current.DBServerHost,
            _settings.Current.DBServerPort,
            "root",
            MySqlInitSql.RootPassword,
            ct);

    // ── TestConnectionAsync ───────────────────────────────────────────────────

    public async Task<(bool Success, string Message, TimeSpan Latency)> TestConnectionAsync(
        string host, string port, string user, string password,
        CancellationToken ct = default)
    {
        var connStr = BuildConnectionString(host, port, user, password, "information_schema");
        var sw = Stopwatch.StartNew();
        try
        {
            await using var conn = new MySqlConnection(connStr);
            await conn.OpenAsync(ct);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = DbQueries.Ping;
            await cmd.ExecuteScalarAsync(ct);
            sw.Stop();
            return (true, $"Connected  ({sw.ElapsedMilliseconds} ms)", sw.Elapsed);
        }
        catch (Exception ex)
        {
            sw.Stop();
            return (false, ex.Message, sw.Elapsed);
        }
    }

    // ── GetRealmsAsync ────────────────────────────────────────────────────────

    public async Task<List<RealmEntry>> GetRealmsAsync(CancellationToken ct = default)
    {
        bool isCmangos = ActiveSchema() == CoreSchema.CMaNGOS;
        var  sql       = isCmangos ? DbQueries.GetRealmsCmangos : DbQueries.GetRealms;
        var  result    = new List<RealmEntry>();

        await using var conn = new MySqlConnection(AuthConnectionString());
        await conn.OpenAsync(ct);
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            result.Add(new RealmEntry
            {
                Id              = reader.GetInt32("id"),
                Name            = reader.GetString("name"),
                Address         = reader.GetString("address"),
                LocalAddress    = reader.IsDBNull(reader.GetOrdinal("localAddress"))
                                    ? string.Empty : reader.GetString("localAddress"),
                LocalSubnetMask = reader.IsDBNull(reader.GetOrdinal("localSubnetMask"))
                                    ? "255.255.255.0" : reader.GetString("localSubnetMask"),
                Port            = reader.GetInt32("port"),
                GameBuild       = reader.GetInt32("gamebuild"),
            });
        }

        return result;
    }

    // ── SaveRealmAsync ────────────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> SaveRealmAsync(
        RealmEntry realm, CancellationToken ct = default)
    {
        try
        {
            var sql = ActiveSchema() == CoreSchema.CMaNGOS
                ? DbQueries.SaveRealmCmangos
                : DbQueries.SaveRealm;

            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddRealmParameters(cmd, realm);
            cmd.Parameters.AddWithValue("@Id", realm.Id);
            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── CreateRealmAsync ──────────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> CreateRealmAsync(
        RealmEntry realm, CancellationToken ct = default)
    {
        try
        {
            var sql = ActiveSchema() == CoreSchema.CMaNGOS
                ? DbQueries.CreateRealmCmangos
                : DbQueries.CreateRealm;

            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddRealmParameters(cmd, realm);
            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── DeleteRealmAsync ──────────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> DeleteRealmAsync(
        int realmId, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = DbQueries.DeleteRealm;
            cmd.Parameters.AddWithValue("@Id", realmId);
            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── UpdateRealmAddressAsync ───────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> UpdateRealmAddressAsync(
        int realmId, string address, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = DbQueries.UpdateRealmAddress;
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Id",      realmId);
            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── CreateAccountAsync ────────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> CreateAccountAsync(
        string username, string password, string email, int expansion,
        CancellationToken ct = default)
    {
        if (username.Length > 16) return (false, "Username too long (max 16 chars).");
        // TC4 BNet passwords have no 16-char limit; the game account password is auto-truncated.
        if (password.Length > 16 && ActiveSchema() != CoreSchema.TrinityCore4)
            return (false, "Password too long (max 16 chars).");
        if (email.Length > 64) return (false, "Email too long (max 64 chars).");

        try
        {
            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);

            // TrinityCore 4.x uses a two-table BattleNet system — handled separately below.
            if (ActiveSchema() == CoreSchema.TrinityCore4)
                return await CreateTc4AccountAsync(conn, password, email, ct);

            if (await UsernameExistsAsync(conn, username, ct))
                return (false, $"Account '{username}' already exists.");
            if (await EmailExistsAsync(conn, email, ct))
                return (false, $"Email '{email}' is already registered.");

            await using var cmd = conn.CreateCommand();

            switch (ActiveSchema())
            {
                case CoreSchema.AzerothCore:
                {
                    byte[] salt     = Srp6.GenerateSalt();
                    byte[] verifier = Srp6.LegacySha1.CreateVerifier(username, password, salt);
                    cmd.CommandText = DbQueries.CreateAccountAzerothCore;
                    cmd.Parameters.AddWithValue("@Username",  username.ToUpperInvariant());
                    cmd.Parameters.AddWithValue("@Salt",      salt);
                    cmd.Parameters.AddWithValue("@Verifier",  verifier);
                    cmd.Parameters.AddWithValue("@Email",     email);
                    cmd.Parameters.AddWithValue("@RegMail",   email);
                    cmd.Parameters.AddWithValue("@JoinDate",  DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@Expansion", expansion);
                    break;
                }

                case CoreSchema.TrinityCore335:
                {
                    string hash = Sha1HexPassHash(username, password);
                    cmd.CommandText = DbQueries.CreateAccountTc335;
                    cmd.Parameters.AddWithValue("@Username",    username.ToUpperInvariant());
                    cmd.Parameters.AddWithValue("@ShaPassHash", hash);
                    cmd.Parameters.AddWithValue("@Email",       email);
                    cmd.Parameters.AddWithValue("@JoinDate",    DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@Expansion",   expansion);
                    break;
                }

                case CoreSchema.CMaNGOS:
                {
                    byte[] salt     = Srp6.GenerateSalt();
                    byte[] verifier = Srp6.CMaNGos.CreateVerifier(username, password, salt);
                    cmd.CommandText = DbQueries.CreateAccountCmangos;
                    cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
                    cmd.Parameters.AddWithValue("@Verifier", Convert.ToHexString(verifier));
                    cmd.Parameters.AddWithValue("@Salt",     Convert.ToHexString(salt));
                    cmd.Parameters.AddWithValue("@Email",    email);
                    cmd.Parameters.AddWithValue("@JoinDate", DateTime.UtcNow);
                    break;
                }
            }

            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── SetGmLevelAsync ───────────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> SetGmLevelAsync(
        string username, int gmLevel, CancellationToken ct = default)
    {
        try
        {
            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);

            int accountId = await GetAccountIdAsync(conn, username, ct);
            if (accountId == 0)
                return (false, $"Account '{username}' not found.");

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = ActiveSchema() == CoreSchema.CMaNGOS
                ? DbQueries.SetGmLevelCmangos
                : DbQueries.SetGmLevelAcTc;
            cmd.Parameters.AddWithValue("@Id",      accountId);
            cmd.Parameters.AddWithValue("@GmLevel", gmLevel);
            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── ChangePasswordAsync ───────────────────────────────────────────────────

    public async Task<(bool Ok, string Error)> ChangePasswordAsync(
        string username, string newPassword, CancellationToken ct = default)
    {
        // TC4 BNet passwords have no 16-char limit; the game account password is auto-truncated.
        if (newPassword.Length > 16 && ActiveSchema() != CoreSchema.TrinityCore4)
            return (false, "Password too long (max 16 chars).");

        try
        {
            await using var conn = new MySqlConnection(AuthConnectionString());
            await conn.OpenAsync(ct);

            if (!await UsernameExistsAsync(conn, username, ct))
                return (false, $"Account '{username}' not found.");

            await using var cmd = conn.CreateCommand();

            switch (ActiveSchema())
            {
                case CoreSchema.AzerothCore:
                {
                    byte[] salt     = Srp6.GenerateSalt();
                    byte[] verifier = Srp6.LegacySha1.CreateVerifier(username, newPassword, salt);
                    cmd.CommandText = DbQueries.ChangePasswordAzerothCore;
                    cmd.Parameters.AddWithValue("@Salt",     salt);
                    cmd.Parameters.AddWithValue("@Verifier", verifier);
                    cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
                    break;
                }

                case CoreSchema.TrinityCore335:
                {
                    string hash = Sha1HexPassHash(username, newPassword);
                    cmd.CommandText = DbQueries.ChangePasswordTc335;
                    cmd.Parameters.AddWithValue("@Hash",     hash);
                    cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
                    break;
                }

                case CoreSchema.CMaNGOS:
                {
                    byte[] salt     = Srp6.GenerateSalt();
                    byte[] verifier = Srp6.CMaNGos.CreateVerifier(username, newPassword, salt);
                    cmd.CommandText = DbQueries.ChangePasswordCmangos;
                    cmd.Parameters.AddWithValue("@Verifier", Convert.ToHexString(verifier));
                    cmd.Parameters.AddWithValue("@Salt",     Convert.ToHexString(salt));
                    cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
                    break;
                }

                case CoreSchema.TrinityCore4:
                {
                    // Password lives on battlenet_accounts. Both the BNet account and the
                    // linked game account store independent SRP6 credentials and must each
                    // be updated on a password change.
                    var (bnetId, bnetEmail) = await GetBnetIdAndEmailByGameUsernameAsync(conn, username, ct);
                    if (bnetId == 0)
                        return (false, "Battle.net account linked to this game account was not found.");

                    // 1. Recompute and persist BNet SRP6v2 credentials.
                    string srpUsername = Srp6.BNet.GetSrpUsername(bnetEmail);
                    (byte[] bnetSalt, byte[] bnetVerifier) = Srp6.BNet.MakeRegistrationData(srpUsername, newPassword);
                    await using var bnetCmd = conn.CreateCommand();
                    bnetCmd.CommandText = DbQueries.ChangePasswordBnet;
                    bnetCmd.Parameters.AddWithValue("@SrpVersion", 2);
                    bnetCmd.Parameters.AddWithValue("@Salt",       bnetSalt);
                    bnetCmd.Parameters.AddWithValue("@Verifier",   bnetVerifier);
                    bnetCmd.Parameters.AddWithValue("@BnetId",     bnetId);
                    await bnetCmd.ExecuteNonQueryAsync(ct);

                    // 2. Recompute and persist GruntSRP6 credentials on the game account row.
                    string gamePassword = newPassword[..Math.Min(16, newPassword.Length)].ToUpperInvariant();
                    (byte[] gameSalt, byte[] gameVerifier) = Srp6.GruntSRP6.MakeRegistrationData(username, gamePassword);
                    await using var gameCmd = conn.CreateCommand();
                    gameCmd.CommandText = DbQueries.ChangePasswordAzerothCore;
                    gameCmd.Parameters.AddWithValue("@Salt",     gameSalt);
                    gameCmd.Parameters.AddWithValue("@Verifier", gameVerifier);
                    gameCmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
                    await gameCmd.ExecuteNonQueryAsync(ct);

                    return (true, string.Empty);
                }
            }

            await cmd.ExecuteNonQueryAsync(ct);
            return (true, string.Empty);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private static void AddRealmParameters(MySqlCommand cmd, RealmEntry realm)
    {
        cmd.Parameters.AddWithValue("@Name",            realm.Name);
        cmd.Parameters.AddWithValue("@Address",         realm.Address);
        cmd.Parameters.AddWithValue("@LocalAddress",    realm.LocalAddress);
        cmd.Parameters.AddWithValue("@LocalSubnetMask", realm.LocalSubnetMask);
        cmd.Parameters.AddWithValue("@Port",            realm.Port);
        cmd.Parameters.AddWithValue("@GameBuild",       realm.GameBuild);
    }

    private static async Task<bool> UsernameExistsAsync(
        MySqlConnection conn, string username, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.UsernameExists;
        cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
        var result = await cmd.ExecuteScalarAsync(ct);
        return Convert.ToInt64(result) > 0;
    }

    private static async Task<bool> EmailExistsAsync(
        MySqlConnection conn, string email, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.EmailExists;
        cmd.Parameters.AddWithValue("@Email", email);
        var result = await cmd.ExecuteScalarAsync(ct);
        return Convert.ToInt64(result) > 0;
    }

    private static async Task<int> GetAccountIdAsync(
        MySqlConnection conn, string username, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.GetAccountId;
        cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
        var result = await cmd.ExecuteScalarAsync(ct);
        return result is null or DBNull ? 0 : Convert.ToInt32(result);
    }

    // ── TrinityCore 4.x / CypherCore BattleNet account creation ─────────────

    /// <summary>
    /// Creates a TrinityCore 4.x / CypherCore Battle.net account
    /// (battlenet_accounts row) and the linked in-game account (account row).
    ///
    /// Matches CypherCore BNetAccountManager.CreateBattlenetAccount exactly:
    ///   • battlenet_accounts gets SRP6v2 credentials (PBKDF2-SHA512, 2048-bit prime)
    ///   • account.username = "{bnetId}#1"
    ///   • account salt/verifier = GruntSRP6 (SHA1, big-endian, 32-byte WoW prime)
    ///   • game password = first 16 chars of the BNet password, uppercased
    /// </summary>
    private static async Task<(bool Ok, string Error)> CreateTc4AccountAsync(
        MySqlConnection conn,
        string password, string email,
        CancellationToken ct)
    {
        if (await BnetEmailExistsAsync(conn, email, ct))
            return (false, $"Battle.net email '{email}' is already registered.");

        // 1. Compute and insert Battle.net SRP6v2 credentials.
        string srpUsername = Srp6.BNet.GetSrpUsername(email);
        (byte[] bnetSalt, byte[] bnetVerifier) = Srp6.BNet.MakeRegistrationData(srpUsername, password);
        await using var bnetCmd = conn.CreateCommand();
        bnetCmd.CommandText = DbQueries.CreateBnetAccount;
        bnetCmd.Parameters.AddWithValue("@Email",      email);
        bnetCmd.Parameters.AddWithValue("@SrpVersion", 2);
        bnetCmd.Parameters.AddWithValue("@Salt",       bnetSalt);
        bnetCmd.Parameters.AddWithValue("@Verifier",   bnetVerifier);
        await bnetCmd.ExecuteNonQueryAsync(ct);

        // 2. Retrieve the auto-generated BNet account ID.
        long bnetId = await GetBnetAccountIdAsync(conn, email, ct);

        // 3. Build game account credentials (GruntSRP6, big-endian x).
        string gameUsername = $"{bnetId}#1";
        string gamePassword = password[..Math.Min(16, password.Length)].ToUpperInvariant();
        (byte[] gameSalt, byte[] gameVerifier) = Srp6.GruntSRP6.MakeRegistrationData(gameUsername, gamePassword);

        await using var gameCmd = conn.CreateCommand();
        gameCmd.CommandText = DbQueries.CreateTc4GameAccount;
        gameCmd.Parameters.AddWithValue("@Username", gameUsername);
        gameCmd.Parameters.AddWithValue("@Salt",     gameSalt);
        gameCmd.Parameters.AddWithValue("@Verifier", gameVerifier);
        gameCmd.Parameters.AddWithValue("@RegMail",  email);
        gameCmd.Parameters.AddWithValue("@Email",    email);
        gameCmd.Parameters.AddWithValue("@BnetId",   bnetId);
        await gameCmd.ExecuteNonQueryAsync(ct);

        return (true, string.Empty);
    }

    // ── Shared helpers ────────────────────────────────────────────────────────

    private static string Sha1HexPassHash(string username, string password)
    {
        var data = Encoding.UTF8.GetBytes(
            $"{username.ToUpperInvariant()}:{password.ToUpperInvariant()}");
        return Convert.ToHexString(SHA1.HashData(data));
    }

    private static async Task<bool> BnetEmailExistsAsync(
        MySqlConnection conn, string email, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.BnetEmailExists;
        cmd.Parameters.AddWithValue("@Email", email);
        var result = await cmd.ExecuteScalarAsync(ct);
        return Convert.ToInt64(result) > 0;
    }

    private static async Task<long> GetBnetAccountIdAsync(
        MySqlConnection conn, string email, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.GetBnetAccountId;
        cmd.Parameters.AddWithValue("@Email", email);
        var result = await cmd.ExecuteScalarAsync(ct);
        return result is null or DBNull ? 0 : Convert.ToInt64(result);
    }

    private static async Task<(long Id, string Email)> GetBnetIdAndEmailByGameUsernameAsync(
        MySqlConnection conn, string username, CancellationToken ct)
    {
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = DbQueries.GetBnetIdAndEmailByGameUsername;
        cmd.Parameters.AddWithValue("@Username", username.ToUpperInvariant());
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        if (!await reader.ReadAsync(ct)) return (0, string.Empty);
        return (reader.GetInt64(0), reader.GetString(1));
    }

    // ── SRP6 crypto (ported from Desktop/SRP6Hashing.cs) ─────────────────────

#pragma warning disable CA5350, CA5351  // SHA1/SHA256 used intentionally for SRP6 protocol

    private static class Srp6
    {
        public static byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(32);

        /// <summary>
        /// Legacy SHA-1 SRP6 — AzerothCore / TrinityCore 3.3.5a.
        /// N is the 32-byte WoW safe prime, g = 7.
        /// </summary>
        public static class LegacySha1
        {
            private const int G = 7;
            private static readonly BigInteger N = new(
                Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"),
                isUnsigned: true, isBigEndian: true);

            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                var h = SHA1.HashData(
                    Encoding.UTF8.GetBytes(
                        $"{username}:{password}".ToUpper(CultureInfo.InvariantCulture)));

                var x = new BigInteger(SHA1.HashData([.. salt, .. h]), isUnsigned: true);
                var v = BigInteger.ModPow(G, x, N).ToByteArray();
                if (v.Length < 32) Array.Resize(ref v, 32);
                return v;
            }
        }

        /// <summary>
        /// TrinityCore 4.x / CypherCore — Battle.net SRP6v2.
        /// PBKDF2-HMAC-SHA512, 15 000 iterations, 2048-bit RFC 3526 prime, g=2.
        /// srpUsername = uppercase hex of SHA256(UPPER(email)).
        /// Mirrors CypherCore BNetAccountManager + SRP6/BnetSRP6v2Hash256.
        /// </summary>
        public static class BNet
        {
            private const int G = 2;
            private static readonly BigInteger N = new(
                Convert.FromHexString(
                    "AC6BDB41324A9A9BF166DE5E1389582FAF72B6651987EE07FC3192943DB56050" +
                    "A37329CBB4A099ED8193E0757767A13DD52312AB4B03310DCD7F48A9DA04FD50" +
                    "E8083969EDB767B0CF6095179A163AB3661A05FBD5FAAAE82918A9962F0B93B8" +
                    "55F97993EC975EEAA80D740ADBF4FF747359D041D5C33EA71D281E446B14773B" +
                    "CA97B43A23FB801676BD207A436C6481F1D2B9078717461A5B9D32E688F87748" +
                    "544523B524B0D57D5EA77A2775D2ECFA032CFBDBF52FB3786160279004E57AE6" +
                    "AF874E7303CE53299CCC041C7BC308D82A5698F3A8D0C38271AE35F8E9DBFBB6" +
                    "94B5C803D89F7AE435DE236D525F54759B65E372FCD68EF20FA7111F9E4AFF73"),
                isUnsigned: true, isBigEndian: true);

            /// <summary>Returns HEX(SHA256(UPPER(email))) — the SRP identity string.</summary>
            public static string GetSrpUsername(string email) =>
                Convert.ToHexString(SHA256.HashData(
                    Encoding.UTF8.GetBytes(email.ToUpperInvariant())));

            /// <summary>Generates a random salt and computes the SRP6v2 verifier.</summary>
            public static (byte[] Salt, byte[] Verifier) MakeRegistrationData(
                string srpUsername, string password)
            {
                byte[] salt     = RandomNumberGenerator.GetBytes(32);
                byte[] verifier = CalculateVerifier(srpUsername, password, salt);
                return (salt, verifier);
            }

            private static byte[] CalculateVerifier(
                string srpUsername, string password, byte[] salt)
            {
                BigInteger x = CalculateX(srpUsername, password, salt);
                return BigInteger.ModPow(G, x, N).ToByteArray(isUnsigned: true, isBigEndian: false);
            }

            private static BigInteger CalculateX(
                string srpUsername, string password, byte[] salt)
            {
                byte[] input  = Encoding.UTF8.GetBytes(srpUsername + ":" + password);
                byte[] xBytes = Rfc2898DeriveBytes.Pbkdf2(input, salt, 15_000, HashAlgorithmName.SHA512, 64);
                BigInteger x = new(xBytes, isUnsigned: true, isBigEndian: true);
                // Overflow fix from CypherCore: if MSB is set, subtract 2^512
                if ((xBytes[0] & 0x80) != 0)
                    x -= new BigInteger([1, ..new byte[64]], isUnsigned: true);
                if (x.Sign == -1)
                    x += N - 1;
                return x % (N - 1);
            }
        }

        /// <summary>
        /// GruntSRP6 — TrinityCore 4.x / CypherCore in-game account SRP6.
        /// SHA1, big-endian x, 32-byte WoW prime, g=7.
        /// Used for game accounts linked to BNet accounts (username = "{bnetId}#1").
        /// Mirrors CypherCore SRP6.GruntSRP6.
        /// </summary>
        public static class GruntSRP6
        {
            private const int G = 7;
            private static readonly BigInteger N = new(
                Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"),
                isUnsigned: true, isBigEndian: true);

            /// <summary>Generates a random salt and computes the SRP6 verifier (big-endian x).</summary>
            public static (byte[] Salt, byte[] Verifier) MakeRegistrationData(
                string username, string password)
            {
                byte[] salt = RandomNumberGenerator.GetBytes(32);
                byte[] h = SHA1.HashData(Encoding.UTF8.GetBytes(
                    $"{username.ToUpperInvariant()}:{password.ToUpperInvariant()}"));
                // CypherCore uses big-endian byte order for x — distinct from LegacySha1
                BigInteger x = new(SHA1.HashData([..salt, ..h]),
                    isUnsigned: true, isBigEndian: true);
                byte[] v = BigInteger.ModPow(G, x, N).ToByteArray(isUnsigned: true, isBigEndian: false);
                if (v.Length < 32) Array.Resize(ref v, 32);
                return (salt, v[..32]);
            }
        }

        /// <summary>
        /// CMaNGOS SRP6 — little-endian byte ordering; v/s stored as uppercase hex strings.
        /// Uses the same 32-byte WoW prime as LegacySha1.
        /// </summary>
        public static class CMaNGos
        {
            private const int G = 7;
            private static readonly BigInteger N = new(
                Convert.FromHexString("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7"),
                isUnsigned: true, isBigEndian: true);

            public static byte[] CreateVerifier(string username, string password, byte[] salt)
            {
                var creds    = SHA1.HashData(
                    Encoding.UTF8.GetBytes($"{username.ToUpper()}:{password.ToUpper()}"));
                var revSalt  = salt.Reverse().ToArray();
                var revCreds = creds.Reverse().ToArray();
                var xBytes   = SHA1.HashData([.. revSalt, .. revCreds]);

                var x = new BigInteger(xBytes, isUnsigned: true, isBigEndian: false);
                var v = BigInteger.ModPow(G, x, N);

                var vBytes = v.ToByteArray(isUnsigned: true, isBigEndian: false);
                if (vBytes.Length < 32)
                {
                    var padded = new byte[32];
                    Array.Copy(vBytes, 0, padded, 32 - vBytes.Length, vBytes.Length);
                    return padded;
                }
                if (vBytes.Length > 32)
                {
                    var trimmed = new byte[32];
                    Array.Copy(vBytes, vBytes.Length - 32, trimmed, 0, 32);
                    return trimmed;
                }
                return vBytes;
            }
        }
    }

#pragma warning restore CA5350, CA5351
}
