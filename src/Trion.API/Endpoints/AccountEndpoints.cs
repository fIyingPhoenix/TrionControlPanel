using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Trion.API.Models;
using Trion.API.Services;

namespace Trion.API.Endpoints;

public static class AccountEndpoints
{
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        var g = app.MapGroup("/Trion/account").WithTags("Account");

        g.MapPost("login",   Login).RequireRateLimiting("login");
        g.MapGet("profile",  Profile).RequireAuthorization();

        return app;
    }

    // ── POST /Trion/account/login ─────────────────────────────────────────────

    private static async Task<IResult> Login(
        [FromBody]          LoginRequest req,
        IUserService                     users,
        IJwtService                      jwt,
        ILogger<Program>                 log)
    {
        var user = await users.ValidateCredentialsAsync(req.Email, req.Password);

        if (user is null)
        {
            log.LogWarning("Failed login for {Email}", req.Email);
            return Results.Unauthorized();
        }

        log.LogInformation("User {Id} ({Email}) logged in.", user.ID, user.Email);

        return Results.Ok(ToResponse(user, jwt.GenerateToken(user)));
    }

    // ── GET /Trion/account/profile ────────────────────────────────────────────

    [Authorize]
    private static async Task<IResult> Profile(
        ClaimsPrincipal principal,
        IUserService    users)
    {
        var sub = principal.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? principal.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);

        if (!int.TryParse(sub, out var id)) return Results.Unauthorized();

        var user = await users.GetByIdAsync(id);
        return user is null
            ? Results.NotFound()
            : Results.Ok(ToResponse(user, token: ""));   // token not re-issued on profile refresh
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static LoginResponse ToResponse(User user, string token) => new(
        Token:     token,
        Id:        user.ID,
        Username:  user.Username,
        Email:     user.Email,
        Role:      user.Role,
        IsActive:  user.IsActive,
        IsBanned:  user.IsBanned,
        LastLogin: user.LastLogin?.ToString("yyyy-MM-dd HH:mm:ss"),
        CreatedAt: user.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
        ApiKey:    user.ApiKey,
        ApiTier:   user.ApiTier);
}
