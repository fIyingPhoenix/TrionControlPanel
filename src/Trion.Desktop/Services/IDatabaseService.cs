using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Direct MySQL operations for the WoW auth / world databases.
/// All methods open their own short-lived connection; no pooling state is held.
/// </summary>
public interface IDatabaseService
{
    // ── Connectivity ──────────────────────────────────────────────────────────

    /// <summary>
    /// Pings the MySQL server using the well-known <c>root</c> account
    /// (password set by <c>MySqlInitSql.FirstRunSetup</c>).
    /// Intended for internal health checks where application-user credentials
    /// may not yet exist (e.g. right after a fresh install).
    /// </summary>
    Task<(bool Success, string Message, TimeSpan Latency)> PingAsync(
        CancellationToken ct = default);

    /// <summary>
    /// Opens a connection to <c>information_schema</c> and runs <c>SELECT 1</c>.
    /// Returns success flag, a human-readable message, and the round-trip latency.
    /// The provided credentials are used directly — nothing is read from settings.
    /// </summary>
    Task<(bool Success, string Message, TimeSpan Latency)> TestConnectionAsync(
        string host, string port, string user, string password,
        CancellationToken ct = default);

    // ── RealmList ─────────────────────────────────────────────────────────────

    /// <summary>Returns up to 10 realms from the auth database realmlist table.</summary>
    Task<List<RealmEntry>> GetRealmsAsync(CancellationToken ct = default);

    /// <summary>Updates an existing realm row (identified by <see cref="RealmEntry.Id"/>).</summary>
    Task<(bool Ok, string Error)> SaveRealmAsync(RealmEntry realm, CancellationToken ct = default);

    /// <summary>Inserts a new realm row.</summary>
    Task<(bool Ok, string Error)> CreateRealmAsync(RealmEntry realm, CancellationToken ct = default);

    /// <summary>Deletes the realm with the given ID.</summary>
    Task<(bool Ok, string Error)> DeleteRealmAsync(int realmId, CancellationToken ct = default);

    /// <summary>Updates only the <c>address</c> column for a specific realm ID.</summary>
    Task<(bool Ok, string Error)> UpdateRealmAddressAsync(int realmId, string address,
        CancellationToken ct = default);

    // ── Accounts ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Creates a game account with the correct SRP6 hashing for the active core.
    /// Validates length constraints, checks for duplicate username/email, then inserts.
    /// </summary>
    Task<(bool Ok, string Error)> CreateAccountAsync(
        string username, string password, string email, int expansion,
        CancellationToken ct = default);

    /// <summary>
    /// Sets the GM access level for <paramref name="username"/> on all realms.
    /// Looks up the account ID first; returns an error if the account does not exist.
    /// </summary>
    Task<(bool Ok, string Error)> SetGmLevelAsync(string username, int gmLevel,
        CancellationToken ct = default);

    /// <summary>
    /// Re-hashes <paramref name="newPassword"/> with the active core's algorithm
    /// and writes the result back to the account row.
    /// </summary>
    Task<(bool Ok, string Error)> ChangePasswordAsync(string username, string newPassword,
        CancellationToken ct = default);
}
