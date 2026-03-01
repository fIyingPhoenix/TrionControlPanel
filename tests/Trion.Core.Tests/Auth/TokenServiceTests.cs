using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;
using Trion.Core.Tests;
using Microsoft.IdentityModel.Tokens;
using Trion.Core.Abstractions.Auth;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Auth;
using Trion.Core.Logging;
using Trion.Core.Settings;

namespace Trion.Core.Tests.Auth;

public sealed class TokenServiceTests : IAsyncLifetime
{
    // Unique named in-memory SQLite database for the refresh_tokens table
    private readonly string            _dbCs;
    private readonly SqliteConnection  _conn;
    private readonly RefreshTokenStore _tokenStore;
    private readonly AuditLogger       _audit;
    private readonly string            _auditDir;
    private readonly InMemorySecretStore _secrets;

    public TokenServiceTests()
    {
        var dbName  = Guid.NewGuid().ToString("N");
        _dbCs       = $"Data Source={dbName};Mode=Memory;Cache=Shared";
        _conn       = new SqliteConnection(_dbCs);
        _conn.Open();

        _tokenStore = new RefreshTokenStore(_dbCs);
        _secrets    = new InMemorySecretStore();

        _auditDir = Path.Combine(Path.GetTempPath(), "trion_tok_" + dbName);
        _audit    = new AuditLogger(_auditDir);
    }

    public async Task InitializeAsync()
    {
        await SettingsMigrations.RunAsync(_conn);
    }

    public async Task DisposeAsync()
    {
        await _audit.DisposeAsync();
        await _conn.DisposeAsync();
        if (Directory.Exists(_auditDir))
            Directory.Delete(_auditDir, recursive: true);
    }

    private TokenService CreateSut() =>
        new(_secrets, _tokenStore, _audit, TestLogger.Instance);

    private static AuthenticatedUser MakeUser(int gmLevel = 2) =>
        new("testuser", gmLevel, "127.0.0.1");

    // ── JWT claims ───────────────────────────────────────────────────────────

    [Fact]
    public async Task IssueTokens_Returns_JwtWithCorrectClaims()
    {
        var sut    = CreateSut();
        var user   = MakeUser(gmLevel: 2);
        var tokens = await sut.IssueTokensAsync(user);

        Assert.False(string.IsNullOrEmpty(tokens.AccessToken));
        Assert.False(string.IsNullOrEmpty(tokens.RefreshToken));
        Assert.True(tokens.ExpiresIn > 0);

        var handler = new JwtSecurityTokenHandler();
        var jwt     = handler.ReadJwtToken(tokens.AccessToken);

        Assert.Equal("trion", jwt.Issuer);
        Assert.Equal("testuser", jwt.Subject);
        Assert.Contains(jwt.Claims, c => c.Type == "gm_level" && c.Value == "2");
        Assert.True(jwt.ValidTo > DateTime.UtcNow);
    }

    [Fact]
    public async Task IssueTokens_AccessToken_Validates_WithPublicKey()
    {
        var sut    = CreateSut();
        var tokens = await sut.IssueTokensAsync(MakeUser());

        // Export public key the same way Program.cs does
        var pem = sut.GetPublicKeyPem();
        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(
            Convert.FromBase64String(
                pem.Replace("-----BEGIN PUBLIC KEY-----", string.Empty)
                   .Replace("-----END PUBLIC KEY-----", string.Empty)
                   .Replace("\n", string.Empty)
                   .Trim()),
            out _);

        var handler    = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = "trion",
            ValidateAudience         = false,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new RsaSecurityKey(rsa),
            ClockSkew                = TimeSpan.Zero,
        };

        var principal = handler.ValidateToken(tokens.AccessToken, parameters, out _);
        Assert.NotNull(principal);
    }

    // ── Refresh token rotation ───────────────────────────────────────────────

    [Fact]
    public async Task Refresh_Issues_NewTokenPair_And_Revokes_Old()
    {
        var sut    = CreateSut();
        var first  = await sut.IssueTokensAsync(MakeUser());

        var second = await sut.RefreshAsync(first.RefreshToken, "127.0.0.1");

        Assert.NotNull(second);
        Assert.NotEqual(first.RefreshToken, second.RefreshToken);
        Assert.False(string.IsNullOrEmpty(second.AccessToken));

        // Old token must now be revoked
        var third = await sut.RefreshAsync(first.RefreshToken, "127.0.0.1");
        Assert.Null(third);
    }

    [Fact]
    public async Task Refresh_PreservesUsername_InNewJwt()
    {
        var sut    = CreateSut();
        var user   = MakeUser(gmLevel: 3);
        var first  = await sut.IssueTokensAsync(user);

        var second = await sut.RefreshAsync(first.RefreshToken, "127.0.0.1");

        Assert.NotNull(second);
        var handler = new JwtSecurityTokenHandler();
        var jwt     = handler.ReadJwtToken(second!.AccessToken);

        Assert.Equal("testuser", jwt.Subject);
        Assert.Contains(jwt.Claims, c => c.Type == "gm_level" && c.Value == "3");
    }

    // ── Revoke ───────────────────────────────────────────────────────────────

    [Fact]
    public async Task Revoke_PreventsSubsequentRefresh()
    {
        var sut    = CreateSut();
        var tokens = await sut.IssueTokensAsync(MakeUser());

        await sut.RevokeAsync(tokens.RefreshToken);

        var result = await sut.RefreshAsync(tokens.RefreshToken, "127.0.0.1");
        Assert.Null(result);
    }

    // ── Unknown / non-existent token ─────────────────────────────────────────

    [Fact]
    public async Task Refresh_UnknownToken_ReturnsNull()
    {
        var sut    = CreateSut();
        var result = await sut.RefreshAsync("this-token-does-not-exist", "127.0.0.1");
        Assert.Null(result);
    }

    // ── Key persistence ──────────────────────────────────────────────────────

    [Fact]
    public async Task SecondInstance_ReusesExistingKey()
    {
        // First instance generates and stores the RSA key in _secrets
        var sut1   = CreateSut();
        var tokens = await sut1.IssueTokensAsync(MakeUser());

        // Second instance should load from _secrets and validate the first token
        var sut2 = CreateSut();
        var pem  = sut2.GetPublicKeyPem();

        var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(
            Convert.FromBase64String(
                pem.Replace("-----BEGIN PUBLIC KEY-----", string.Empty)
                   .Replace("-----END PUBLIC KEY-----", string.Empty)
                   .Replace("\n", string.Empty)
                   .Trim()),
            out _);

        var handler    = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = "trion",
            ValidateAudience         = false,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new RsaSecurityKey(rsa),
            ClockSkew                = TimeSpan.Zero,
        };

        // Must not throw
        var principal = handler.ValidateToken(tokens.AccessToken, parameters, out _);
        Assert.NotNull(principal);
    }

    // ── In-memory secret store ────────────────────────────────────────────────

    private sealed class InMemorySecretStore : ISecretStore
    {
        private readonly Dictionary<string, string> _data = [];

        public string? GetSecret(string key)            => _data.GetValueOrDefault(key);
        public void SetSecret(string key, string value) => _data[key] = value;
        public void DeleteSecret(string key)            => _data.Remove(key);
    }
}
