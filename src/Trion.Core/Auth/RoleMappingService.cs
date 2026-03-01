using Microsoft.Extensions.Caching.Memory;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Services;
using Trion.Core.Abstractions.Settings;

namespace Trion.Core.Auth;

/// <summary>
/// Maps emulator GM levels to <see cref="TrionRole"/> with a 5-minute memory-cache TTL.
/// </summary>
public sealed class RoleMappingService
{
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);

    private readonly IEmulatorRepositoryFactory _repoFactory;
    private readonly ISettingsRepository        _settings;
    private readonly IMemoryCache               _cache;

    public RoleMappingService(
        IEmulatorRepositoryFactory repoFactory,
        ISettingsRepository        settings,
        IMemoryCache               cache)
    {
        _repoFactory = repoFactory;
        _settings    = settings;
        _cache       = cache;
    }

    /// <summary>
    /// Returns the <see cref="TrionRole"/> for a user.
    /// Checks the bootstrap account first; falls back to emulator repository.
    /// </summary>
    public async Task<TrionRole> GetRoleAsync(
        string       username,
        EmulatorType emulatorType,
        CancellationToken ct = default)
    {
        var cacheKey = $"role:{username.ToLowerInvariant()}:{emulatorType}";
        if (_cache.TryGetValue(cacheKey, out TrionRole cached))
            return cached;

        // Bootstrap account always gets Owner
        if (username.Equals(BootstrapAccount.DefaultUsername, StringComparison.OrdinalIgnoreCase))
        {
            _cache.Set(cacheKey, TrionRole.Owner, CacheTtl);
            return TrionRole.Owner;
        }

        var repo = _repoFactory.Get(emulatorType);
        if (repo is null) return TrionRole.None;

        var gmLevel = await repo.GetGmLevelAsync(username, ct);
        var role    = MapGmLevel(gmLevel);

        _cache.Set(cacheKey, role, CacheTtl);
        return role;
    }

    /// <summary>Removes the cached role so the next call performs a fresh lookup.</summary>
    public void InvalidateCache(string username, EmulatorType emulatorType)
    {
        var cacheKey = $"role:{username.ToLowerInvariant()}:{emulatorType}";
        _cache.Remove(cacheKey);
    }

    /// <summary>
    /// Flags the user for forced re-authentication on next request.
    /// The API middleware checks this key and returns 401 if set.
    /// </summary>
    public async Task ForceReauthAsync(
        string username,
        CancellationToken ct = default)
    {
        await _settings.SetAsync(
            $"force_reauth:{username.ToLowerInvariant()}",
            DateTimeOffset.UtcNow,
            ct);
    }

    private static TrionRole MapGmLevel(int gmLevel) => gmLevel switch
    {
        >= 3 => TrionRole.Owner,
        2    => TrionRole.Moderator,
        1    => TrionRole.Observer,
        _    => TrionRole.None
    };
}
