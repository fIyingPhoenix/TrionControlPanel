using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Auth;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Database.Models;
using Trion.Core.Abstractions.Services;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Logging;

namespace Trion.Core.Auth;

public sealed class EmulatorAuthService : IEmulatorAuthService
{
    private const string LockoutKeyPrefix  = "lockout:";
    private const string FailCountPrefix   = "failcount:";

    private readonly IEmulatorRepositoryFactory _repoFactory;
    private readonly ISettingsRepository        _settings;
    private readonly AuditLogger                _audit;
    private readonly IOptionsMonitor<EmulatorAuthOptions> _opts;
    private readonly ILogger                    _log;

    public EmulatorAuthService(
        IEmulatorRepositoryFactory           repoFactory,
        ISettingsRepository                  settings,
        AuditLogger                          audit,
        IOptionsMonitor<EmulatorAuthOptions> opts,
        TrionLogger                          trionLogger)
    {
        _repoFactory = repoFactory;
        _settings    = settings;
        _audit       = audit;
        _opts        = opts;
        _log         = trionLogger.CreateLogger(nameof(EmulatorAuthService));
    }

    public async Task<AuthResult> AuthenticateAsync(
        string       username,
        string       password,
        EmulatorType emulatorType,
        CancellationToken ct = default)
    {
        var lower = username.ToLowerInvariant();

        // ── Lockout check ────────────────────────────────────────────────────
        var lockoutKey = LockoutKeyPrefix + lower;
        var lockoutExpiry = await _settings.GetAsync<DateTimeOffset?>(lockoutKey, ct);
        if (lockoutExpiry.HasValue && lockoutExpiry.Value > DateTimeOffset.UtcNow)
        {
            var remaining = lockoutExpiry.Value - DateTimeOffset.UtcNow;
            RecordFailure(username, "locked", string.Empty);
            return AuthResult.Fail($"Account locked. Try again in {(int)remaining.TotalMinutes + 1} minute(s).");
        }

        // ── Bootstrap account (always available, not tied to an emulator DB) ─
        if (await BootstrapAccount.ValidateAsync(_settings, username, password, ct))
        {
            await ClearFailCount(lower, ct);
            _audit.Record(new AuditEvent(
                Timestamp: DateTimeOffset.UtcNow,
                Username:  username,
                IpAddress: "internal",
                Action:    AuditAction.LoginSuccess,
                Result:    "bootstrap",
                Detail:    null));
            return AuthResult.Ok(username, gmLevel: 3);
        }

        // ── Emulator repository auth ─────────────────────────────────────────
        var repo = _repoFactory.Get(emulatorType);
        if (repo is null)
        {
            // No DB configured yet (M4 not implemented) — deny cleanly
            return await Fail(lower, username, "No emulator database configured.", ct);
        }

        var expectedHash = ComputeHash(username, password, emulatorType);

        AccountRecord? account;
        try
        {
            account = await repo.GetAccountAsync(username, ct);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Failed to query emulator repository for user {Username}.", username);
            return AuthResult.Fail("Authentication service unavailable.");
        }

        if (account is null)
            return await Fail(lower, username, "Invalid username or password.", ct);

        // Verify stored hash matches
        var storedHash = account.PasswordHash;
        if (!string.Equals(expectedHash, storedHash, StringComparison.OrdinalIgnoreCase))
            return await Fail(lower, username, "Invalid username or password.", ct);

        // GM level check
        int gmLevel;
        try
        {
            gmLevel = await repo.GetGmLevelAsync(username, ct);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Failed to get GM level for {Username}.", username);
            return AuthResult.Fail("Authentication service unavailable.");
        }

        if (gmLevel < 1)
            return await Fail(lower, username, "Access denied: GM level 0.", ct);

        // Success
        await ClearFailCount(lower, ct);
        _audit.Record(new AuditEvent(
            Timestamp: DateTimeOffset.UtcNow,
            Username:  username,
            IpAddress: "internal",
            Action:    AuditAction.LoginSuccess,
            Result:    $"gm:{gmLevel}",
            Detail:    emulatorType.ToString()));

        return AuthResult.Ok(username, gmLevel);
    }

    // ── Failure tracking ──────────────────────────────────────────────────

    private async Task<AuthResult> Fail(
        string lower, string username, string reason, CancellationToken ct)
    {
        RecordFailure(username, reason, string.Empty);

        var failKey   = FailCountPrefix + lower;
        var failCount = (await _settings.GetAsync<int>(failKey, ct)) + 1;
        await _settings.SetAsync(failKey, failCount, ct);

        var opts = _opts.CurrentValue;
        if (failCount >= opts.MaxFailedAttempts)
        {
            var expiry = DateTimeOffset.UtcNow.Add(opts.LockoutDuration);
            await _settings.SetAsync(LockoutKeyPrefix + lower, expiry, ct);
            await _settings.DeleteAsync(failKey, ct);
            return AuthResult.Fail(
                $"Too many failed attempts. Account locked for {(int)opts.LockoutDuration.TotalMinutes} minute(s).");
        }

        return AuthResult.Fail(reason);
    }

    private async Task ClearFailCount(string lower, CancellationToken ct)
    {
        await _settings.DeleteAsync(FailCountPrefix + lower, ct);
        await _settings.DeleteAsync(LockoutKeyPrefix + lower, ct);
    }

    private void RecordFailure(string username, string reason, string ip)
    {
        _audit.Record(new AuditEvent(
            Timestamp: DateTimeOffset.UtcNow,
            Username:  username,
            IpAddress: ip.Length > 0 ? ip : "internal",
            Action:    AuditAction.LoginFailure,
            Result:    "failure",
            Detail:    reason));
    }

    // ── Password hash computation ─────────────────────────────────────────

    private static string ComputeHash(string username, string password, EmulatorType type)
    {
        // TrinityCore / CMaNGOS: SHA1(UPPER(username) + ":" + UPPER(password))
        // AzerothCore uses the same format by default
        var input = $"{username.ToUpperInvariant()}:{password.ToUpperInvariant()}";
        var bytes = SHA1.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes); // uppercase hex, no dashes
    }
}
