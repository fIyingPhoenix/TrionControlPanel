using Microsoft.Data.Sqlite;
using Trion.Core.Tests;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Auth;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Database.Models;
using Trion.Core.Abstractions.Services;
using Trion.Core.Auth;
using Trion.Core.Logging;
using Trion.Core.Settings;

namespace Trion.Core.Tests.Auth;

public sealed class EmulatorAuthServiceTests : IAsyncLifetime
{
    // A uniquely-named in-memory SQLite database shared across all connections in this test
    private readonly string            _dbCs;
    private readonly SqliteConnection  _conn;
    private readonly SettingsRepository _settings;
    private readonly AuditLogger        _audit;
    private readonly string             _auditDir;

    public EmulatorAuthServiceTests()
    {
        var dbName = Guid.NewGuid().ToString("N");
        _dbCs     = $"Data Source={dbName};Mode=Memory;Cache=Shared";
        _conn     = new SqliteConnection(_dbCs);
        _conn.Open();                        // keep open so the in-memory db persists

        // SettingsRepository wraps the path with "Data Source=...;" so pass just the
        // named-db identifier (SQLite will resolve it via the shared cache pool).
        _settings = new SettingsRepository($"{dbName};Mode=Memory;Cache=Shared");

        _auditDir = Path.Combine(Path.GetTempPath(), "trion_audit_" + dbName);
        _audit    = new AuditLogger(_auditDir);
    }

    public async Task InitializeAsync()
    {
        await SettingsMigrations.RunAsync(_conn);
        await BootstrapAccount.EnsureCreatedAsync(_settings, initialPassword: "testpass");
    }

    public async Task DisposeAsync()
    {
        await _audit.DisposeAsync();
        await _conn.DisposeAsync();
        if (Directory.Exists(_auditDir))
            Directory.Delete(_auditDir, recursive: true);
    }

    // ── helpers ─────────────────────────────────────────────────────────────

    private EmulatorAuthService CreateSut(
        IEmulatorRepositoryFactory? factory = null,
        EmulatorAuthOptions?        opts    = null)
    {
        factory ??= new NullRepositoryFactory();
        opts    ??= new EmulatorAuthOptions();
        return new EmulatorAuthService(
            factory,
            _settings,
            _audit,
            new FakeOptionsMonitor<EmulatorAuthOptions>(opts),
            TestLogger.Instance);
    }

    // ── Bootstrap account ────────────────────────────────────────────────────

    [Fact]
    public async Task Bootstrap_Login_Succeeds_WithCorrectCredentials()
    {
        var sut    = CreateSut();
        var result = await sut.AuthenticateAsync("admin", "testpass", EmulatorType.TrinityCore);

        Assert.True(result.Success);
        Assert.Equal("admin", result.Username);
        Assert.Equal(3, result.GmLevel);   // bootstrap is always Owner (GM 3)
    }

    [Fact]
    public async Task Bootstrap_Login_Fails_WithWrongPassword()
    {
        var sut    = CreateSut();
        var result = await sut.AuthenticateAsync("admin", "WRONG", EmulatorType.TrinityCore);

        Assert.False(result.Success);
        Assert.NotNull(result.FailureReason);
    }

    // ── Lockout mechanics ────────────────────────────────────────────────────

    [Fact]
    public async Task FailedLogins_Lock_Account_AfterMaxAttempts()
    {
        var opts = new EmulatorAuthOptions { MaxFailedAttempts = 3, LockoutDuration = TimeSpan.FromMinutes(15) };
        var sut  = CreateSut(opts: opts);

        // The stub factory returns null → "No emulator database configured" → failure path
        for (int i = 0; i < 3; i++)
            await sut.AuthenticateAsync("hacker", "pw", EmulatorType.TrinityCore);

        var result = await sut.AuthenticateAsync("hacker", "pw", EmulatorType.TrinityCore);

        Assert.False(result.Success);
        Assert.Contains("locked", result.FailureReason, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Locked_Account_IsBlocked_BeforeLockoutExpires()
    {
        var opts = new EmulatorAuthOptions { MaxFailedAttempts = 2, LockoutDuration = TimeSpan.FromHours(1) };
        var sut  = CreateSut(opts: opts);

        // Trigger lockout with 2 failures
        await sut.AuthenticateAsync("user1", "bad", EmulatorType.TrinityCore);
        await sut.AuthenticateAsync("user1", "bad", EmulatorType.TrinityCore);

        // 3rd attempt should see lockout immediately
        var result = await sut.AuthenticateAsync("user1", "bad", EmulatorType.TrinityCore);

        Assert.False(result.Success);
        Assert.Contains("locked", result.FailureReason, StringComparison.OrdinalIgnoreCase);
    }

    // ── GM level enforcement ─────────────────────────────────────────────────

    [Fact]
    public async Task GmLevel0_IsRejected()
    {
        var factory = new FakeRepositoryFactory(gmLevel: 0, passwordHash: Sha1Hash("user", "pass"));
        var sut     = CreateSut(factory);

        var result = await sut.AuthenticateAsync("user", "pass", EmulatorType.TrinityCore);

        Assert.False(result.Success);
        Assert.Contains("GM level 0", result.FailureReason);
    }

    [Fact]
    public async Task GmLevel1_IsAccepted()
    {
        var factory = new FakeRepositoryFactory(gmLevel: 1, passwordHash: Sha1Hash("user", "pass"));
        var sut     = CreateSut(factory);

        var result = await sut.AuthenticateAsync("user", "pass", EmulatorType.TrinityCore);

        Assert.True(result.Success);
        Assert.Equal(1, result.GmLevel);
    }

    [Fact]
    public async Task WrongPassword_IsRejected_EvenWithValidAccount()
    {
        var factory = new FakeRepositoryFactory(gmLevel: 2, passwordHash: Sha1Hash("user", "correct"));
        var sut     = CreateSut(factory);

        var result = await sut.AuthenticateAsync("user", "wrong", EmulatorType.TrinityCore);

        Assert.False(result.Success);
    }

    // ── SHA-1 hash format ────────────────────────────────────────────────────

    [Fact]
    public async Task Sha1Hash_MatchesTrinityCore_Format()
    {
        // TrinityCore format: SHA1(UPPER(username) + ":" + UPPER(password)) as uppercase hex
        var input    = System.Text.Encoding.UTF8.GetBytes("ADMIN:SECRET");
        var expected = Convert.ToHexString(System.Security.Cryptography.SHA1.HashData(input));

        // A successful auth confirms the hash matches — verify via FakeRepository
        var factory = new FakeRepositoryFactory(gmLevel: 1, passwordHash: expected);
        var sut     = CreateSut(factory);

        var result = await sut.AuthenticateAsync("admin", "secret", EmulatorType.TrinityCore);

        Assert.True(result.Success);
    }

    // ── Static helpers ───────────────────────────────────────────────────────

    private static string Sha1Hash(string username, string password)
    {
        var input = System.Text.Encoding.UTF8.GetBytes(
            $"{username.ToUpperInvariant()}:{password.ToUpperInvariant()}");
        return Convert.ToHexString(System.Security.Cryptography.SHA1.HashData(input));
    }

    // ── Test fakes ───────────────────────────────────────────────────────────

    private sealed class NullRepositoryFactory : IEmulatorRepositoryFactory
    {
        public IEmulatorRepository? Get(EmulatorType type) => null;
    }

    private sealed class FakeRepositoryFactory(int gmLevel, string? passwordHash)
        : IEmulatorRepositoryFactory
    {
        public IEmulatorRepository? Get(EmulatorType type) =>
            new FakeEmulatorRepository(gmLevel, passwordHash);
    }

    private sealed class FakeEmulatorRepository(int gmLevel, string? passwordHash)
        : IEmulatorRepository
    {
        public Task<AccountRecord?> GetAccountAsync(string username, CancellationToken ct = default) =>
            Task.FromResult<AccountRecord?>(new AccountRecord(
                Id:           1,
                Username:     username,
                Email:        string.Empty,
                GmLevel:      gmLevel,
                CreatedAt:    DateTimeOffset.UtcNow,
                PasswordHash: passwordHash));

        public Task<int> GetGmLevelAsync(string username, CancellationToken ct = default) =>
            Task.FromResult(gmLevel);

        // ── Unused interface members ──────────────────────────────────────

        public Task<AccountRecord> CreateAccountAsync(string username, string passwordHash,
            string email, int level, CancellationToken ct = default) =>
            throw new NotImplementedException();

        public Task<bool> UpdateGmLevelAsync(string username, int newLevel, CancellationToken ct = default) =>
            Task.FromResult(true);

        public Task<bool> DeleteAccountAsync(string username, CancellationToken ct = default) =>
            Task.FromResult(true);

        public Task<Page<AccountRecord>> ListAccountsAsync(int page, int pageSize, CancellationToken ct = default) =>
            Task.FromResult(new Page<AccountRecord>([], 0, page, pageSize));

        public Task<IReadOnlyList<RealmRecord>> GetRealmsAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<RealmRecord>>([]);

        public Task<bool> UpdateRealmAddressAsync(int realmId, string address, CancellationToken ct = default) =>
            Task.FromResult(true);

        public Task<IReadOnlyList<CharacterSummary>> GetCharacterSummariesAsync(int accountId, CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<CharacterSummary>>([]);

        public Task<int> GetOnlineCountAsync(CancellationToken ct = default) =>
            Task.FromResult(0);

        public Task<IReadOnlyList<SessionRecord>> ListActiveSessionsAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<SessionRecord>>([]);

        public Task<IReadOnlyList<GmTicket>> ListGmTicketsAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyList<GmTicket>>([]);

        public Task<bool> CloseTicketAsync(int ticketId, CancellationToken ct = default) =>
            Task.FromResult(true);

        public Task<bool> BanAccountAsync(BanRecord ban, CancellationToken ct = default) =>
            Task.FromResult(true);

        public Task<bool> UnbanAccountAsync(string username, CancellationToken ct = default) =>
            Task.FromResult(true);
    }
}

// ── Shared fakes (file-scoped so they don't leak into other test files) ────────

file sealed class FakeOptionsMonitor<T>(T value) : IOptionsMonitor<T>
{
    public T CurrentValue => value;
    public T Get(string? name) => value;
    public IDisposable? OnChange(Action<T, string?> listener) => null;
}
