using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Trion.Core.Abstractions.Auth;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Logging;

namespace Trion.Core.Auth;

public sealed class TokenService : ITokenService
{
    private const string PrivateKeySecretName = "jwt_signing_key_pem";
    private const string PublicKeySecretName  = "jwt_signing_public_pem";
    private const string Issuer               = "trion";
    private const int    AccessTokenMinutes   = 15;
    private const string RefreshCookieName    = "trion_refresh";

    private readonly ISecretStore    _secrets;
    private readonly RefreshTokenStore _tokenStore;
    private readonly AuditLogger     _audit;
    private readonly ILogger<TokenService> _logger;

    // Lazily initialised RSA key — created or loaded on first use
    private RSA?     _rsa;
    private readonly Lock _rsaLock = new();

    public TokenService(
        ISecretStore          secrets,
        RefreshTokenStore     tokenStore,
        AuditLogger           audit,
        ILogger<TokenService> logger)
    {
        _secrets    = secrets;
        _tokenStore = tokenStore;
        _audit      = audit;
        _logger     = logger;
    }

    // ── ITokenService ──────────────────────────────────────────────────────

    public async Task<TokenPair> IssueTokensAsync(
        AuthenticatedUser user,
        CancellationToken ct = default)
    {
        var rsa = EnsureRsaKey();
        var jwt = CreateJwt(user, rsa);

        var rawRefresh    = GenerateOpaqueToken();
        var refreshExpiry = DateTimeOffset.UtcNow.AddDays(30);
        // Store "username:gmLevel" so RefreshAsync can re-issue a properly scoped JWT
        var userId = $"{user.Username}:{user.GmLevel}";
        await _tokenStore.StoreAsync(userId, rawRefresh, user.IpAddress, refreshExpiry, ct);

        _audit.Record(new AuditEvent(
            Timestamp: DateTimeOffset.UtcNow,
            Username:  user.Username,
            IpAddress: user.IpAddress,
            Action:    AuditAction.TokenRefresh,
            Result:    "issued",
            Detail:    null));

        return new TokenPair(jwt, rawRefresh, AccessTokenMinutes * 60);
    }

    public async Task<TokenPair?> RefreshAsync(
        string rawRefresh,
        string ipAddress,
        CancellationToken ct = default)
    {
        var record = await _tokenStore.FindAsync(rawRefresh, ct);

        if (record is null)                                      return null;
        if (record.Revoked)                                      return null;
        if (DateTimeOffset.Parse(record.ExpiresAt) < DateTimeOffset.UtcNow) return null;

        // Rotate: revoke old token, issue new one
        await _tokenStore.RevokeAsync(rawRefresh, ct);

        // Username was stored as "username:gmLevel"
        var parts   = record.Username.Split(':', 2);
        var uname   = parts[0];
        int gmLevel = parts.Length > 1 && int.TryParse(parts[1], out var g) ? g : 1;
        var rsa     = EnsureRsaKey();

        var user = new AuthenticatedUser(uname, gmLevel, ipAddress);
        var jwt  = CreateJwt(user, rsa);

        var newRaw    = GenerateOpaqueToken();
        var newExpiry = DateTimeOffset.UtcNow.AddDays(30);
        var newUserId = $"{user.Username}:{user.GmLevel}";
        await _tokenStore.StoreAsync(newUserId, newRaw, ipAddress, newExpiry, ct);

        _audit.Record(new AuditEvent(
            Timestamp: DateTimeOffset.UtcNow,
            Username:  uname,
            IpAddress: ipAddress,
            Action:    AuditAction.TokenRefresh,
            Result:    "rotated",
            Detail:    null));

        return new TokenPair(jwt, newRaw, AccessTokenMinutes * 60);
    }

    public async Task RevokeAsync(string rawRefresh, CancellationToken ct = default)
    {
        await _tokenStore.RevokeAsync(rawRefresh, ct);
        _audit.Record(new AuditEvent(
            Timestamp: DateTimeOffset.UtcNow,
            Username:  null,
            IpAddress: "internal",
            Action:    AuditAction.TokenRevoke,
            Result:    "revoked",
            Detail:    null));
    }

    // ── Public key export (used by Program.cs JWT validation) ─────────────

    public string GetPublicKeyPem()
    {
        var rsa = EnsureRsaKey();
        return rsa.ExportSubjectPublicKeyInfoPem();
    }

    // ── Internals ────────────────────────────────────────────────────────────

    private RSA EnsureRsaKey()
    {
        if (_rsa is not null) return _rsa;

        lock (_rsaLock)
        {
            if (_rsa is not null) return _rsa;

            var pem = _secrets.GetSecret(PrivateKeySecretName);
            if (pem is not null)
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(pem);
                _rsa = rsa;
                _logger.LogInformation("JWT signing key loaded from secret store.");
            }
            else
            {
                _logger.LogInformation("Generating new RSA-2048 JWT signing key.");
                var rsa = RSA.Create(keySizeInBits: 2048);
                _secrets.SetSecret(PrivateKeySecretName, rsa.ExportPkcs8PrivateKeyPem());
                _secrets.SetSecret(PublicKeySecretName,  rsa.ExportSubjectPublicKeyInfoPem());
                _rsa = rsa;
            }

            return _rsa;
        }
    }

    private static string CreateJwt(AuthenticatedUser user, RSA rsa)
    {
        var key         = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("gm_level", user.GmLevel.ToString()),
            new Claim(ClaimTypes.Role, GmLevelToRole(user.GmLevel).ToString()),
        };

        var now    = DateTime.UtcNow;
        var token  = new JwtSecurityToken(
            issuer:             Issuer,
            audience:           null,
            claims:             claims,
            notBefore:          now,
            expires:            now.AddMinutes(AccessTokenMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateOpaqueToken()
    {
        var bytes = new byte[32]; // 256-bit
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    private static TrionRole GmLevelToRole(int gmLevel) => gmLevel switch
    {
        >= 3 => TrionRole.Owner,
        2    => TrionRole.Moderator,
        _    => TrionRole.Observer
    };
}
