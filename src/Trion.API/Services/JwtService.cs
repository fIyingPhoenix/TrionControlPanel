using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Trion.API.Models;

namespace Trion.API.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}

public sealed class JwtService : IJwtService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string               _issuer;
    private readonly string               _audience;
    private readonly int                  _expiryHours;

    public JwtService(IConfiguration cfg)
    {
        var s        = cfg.GetSection("Jwt");
        var secret   = s["SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey is missing.");
        _key         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        _issuer      = s["Issuer"]   ?? "TrionAPI";
        _audience    = s["Audience"] ?? "TrionClient";
        _expiryHours = int.TryParse(s["ExpiryHours"], out var h) ? h : 24;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new(JwtRegisteredClaimNames.Sub,   user.ID.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
            new(ClaimTypes.Name,               user.Username),
            new(ClaimTypes.Role,               user.Role),
            new("api_key",                     user.ApiKey),
            new("api_tier",                    user.ApiTier),
        ];

        var token = new JwtSecurityToken(
            issuer:             _issuer,
            audience:           _audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(_expiryHours),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
