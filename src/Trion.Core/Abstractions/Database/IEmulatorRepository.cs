using Trion.Core.Abstractions.Database.Models;

namespace Trion.Core.Abstractions.Database;

public interface IEmulatorRepository
{
    // Accounts
    Task<AccountRecord?> GetAccountAsync(string username, CancellationToken ct = default);
    Task<AccountRecord> CreateAccountAsync(string username, string passwordHash, string email, int gmLevel, CancellationToken ct = default);
    Task<bool> UpdateGmLevelAsync(string username, int newLevel, CancellationToken ct = default);
    Task<bool> DeleteAccountAsync(string username, CancellationToken ct = default);
    Task<Page<AccountRecord>> ListAccountsAsync(int page, int pageSize, CancellationToken ct = default);

    // GM Level
    Task<int> GetGmLevelAsync(string username, CancellationToken ct = default);

    // Realms
    Task<IReadOnlyList<RealmRecord>> GetRealmsAsync(CancellationToken ct = default);
    Task<bool> UpdateRealmAddressAsync(int realmId, string address, CancellationToken ct = default);

    // Characters
    Task<IReadOnlyList<CharacterSummary>> GetCharacterSummariesAsync(int accountId, CancellationToken ct = default);

    // Online / Sessions
    Task<int> GetOnlineCountAsync(CancellationToken ct = default);
    Task<IReadOnlyList<SessionRecord>> ListActiveSessionsAsync(CancellationToken ct = default);

    // GM Tickets
    Task<IReadOnlyList<GmTicket>> ListGmTicketsAsync(CancellationToken ct = default);
    Task<bool> CloseTicketAsync(int ticketId, CancellationToken ct = default);

    // Bans
    Task<bool> BanAccountAsync(BanRecord ban, CancellationToken ct = default);
    Task<bool> UnbanAccountAsync(string username, CancellationToken ct = default);
}
