using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Trion.Core.Abstractions.Auth;
using Trion.Core.Abstractions.Services;
using Trion.Core.Auth;

namespace Trion.Web.Api;

public static class AuthEndpoints
{
    private const string RefreshCookieName = "trion_refresh";

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Auth");

        group.MapPost("/login", Login)
            .WithName("Login")
            .WithSummary("Authenticate with emulator credentials and receive a JWT.")
            .RequireRateLimiting("auth")
            .AllowAnonymous();

        group.MapPost("/refresh", Refresh)
            .WithName("RefreshToken")
            .WithSummary("Exchange the refresh cookie for a new access token.")
            .AllowAnonymous();

        group.MapPost("/revoke", Revoke)
            .WithName("RevokeToken")
            .WithSummary("Revoke the current refresh token and clear the cookie.")
            .RequireAuthorization();

        return app;
    }

    // ── POST /api/auth/login ──────────────────────────────────────────────

    private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult, BadRequest<string>>> Login(
        [FromBody] LoginRequest      request,
        HttpContext                  ctx,
        EmulatorAuthService          authService,
        TokenService                 tokenService,
        CancellationToken            ct)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
            return TypedResults.BadRequest("Username and password are required.");

        var ip = GetClientIp(ctx);

        var authResult = await authService.AuthenticateAsync(
            request.Username, request.Password, request.EmulatorType, ct);

        if (!authResult.Success)
            return TypedResults.Unauthorized();

        var user = new AuthenticatedUser(authResult.Username!, authResult.GmLevel, ip);
        var tokens = await tokenService.IssueTokensAsync(user, ct);

        SetRefreshCookie(ctx, tokens.RefreshToken);
        SetNoCacheHeaders(ctx);

        return TypedResults.Ok(new LoginResponse(tokens.AccessToken, tokens.ExpiresIn));
    }

    // ── POST /api/auth/refresh ────────────────────────────────────────────

    private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> Refresh(
        HttpContext  ctx,
        TokenService tokenService,
        CancellationToken ct)
    {
        var rawRefresh = ctx.Request.Cookies[RefreshCookieName];
        if (string.IsNullOrEmpty(rawRefresh))
            return TypedResults.Unauthorized();

        var ip     = GetClientIp(ctx);
        var tokens = await tokenService.RefreshAsync(rawRefresh, ip, ct);

        if (tokens is null)
            return TypedResults.Unauthorized();

        SetRefreshCookie(ctx, tokens.RefreshToken);
        SetNoCacheHeaders(ctx);

        return TypedResults.Ok(new LoginResponse(tokens.AccessToken, tokens.ExpiresIn));
    }

    // ── POST /api/auth/revoke ─────────────────────────────────────────────

    private static async Task<Ok> Revoke(
        HttpContext  ctx,
        TokenService tokenService,
        CancellationToken ct)
    {
        var rawRefresh = ctx.Request.Cookies[RefreshCookieName];
        if (!string.IsNullOrEmpty(rawRefresh))
            await tokenService.RevokeAsync(rawRefresh, ct);

        ClearRefreshCookie(ctx);
        SetNoCacheHeaders(ctx);
        return TypedResults.Ok();
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    private static void SetRefreshCookie(HttpContext ctx, string value)
    {
        ctx.Response.Cookies.Append(RefreshCookieName, value, new CookieOptions
        {
            HttpOnly  = true,
            Secure    = true,
            SameSite  = SameSiteMode.Strict,
            Expires   = DateTimeOffset.UtcNow.AddDays(30),
            Path      = "/api/auth"
        });
    }

    private static void ClearRefreshCookie(HttpContext ctx)
    {
        ctx.Response.Cookies.Delete(RefreshCookieName, new CookieOptions
        {
            HttpOnly = true,
            Secure   = true,
            SameSite = SameSiteMode.Strict,
            Path     = "/api/auth"
        });
    }

    private static void SetNoCacheHeaders(HttpContext ctx)
    {
        ctx.Response.Headers.CacheControl = "no-store";
        ctx.Response.Headers.Pragma       = "no-cache";
    }

    private static string GetClientIp(HttpContext ctx)
    {
        var forwarded = ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwarded))
            return forwarded.Split(',')[0].Trim();
        return ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}

// ── Request / Response DTOs ───────────────────────────────────────────────

public sealed record LoginRequest(
    string       Username,
    string       Password,
    EmulatorType EmulatorType = EmulatorType.TrinityCore);

public sealed record LoginResponse(
    string AccessToken,
    int    ExpiresIn);
