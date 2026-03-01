using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Services;
using Trion.Core.Database.AzerothCore;
using Trion.Core.Database.CMaNGOS;
using Trion.Core.Database.TrinityCore;

namespace Trion.Core.Database;

/// <summary>
/// Resolves the correct <see cref="IEmulatorRepository"/> based on the
/// <see cref="EmulatorConnectionOptions"/> configuration section.
/// Returns <c>null</c> for any emulator type whose AuthConnectionString is empty.
/// </summary>
public sealed class EmulatorRepositoryFactory : IEmulatorRepositoryFactory
{
    private readonly IReadOnlyDictionary<EmulatorType, IEmulatorRepository> _repos;

    public EmulatorRepositoryFactory(IOptions<EmulatorConnectionOptions> options)
    {
        var map = new Dictionary<EmulatorType, IEmulatorRepository>();
        var o   = options.Value;

        if (!string.IsNullOrWhiteSpace(o.TrinityCore?.AuthConnectionString))
            map[EmulatorType.TrinityCore] = new TrinityEmulatorRepository(
                o.TrinityCore.AuthConnectionString,
                o.TrinityCore.CharacterConnectionString,
                o.TrinityCore.WorldConnectionString);

        if (!string.IsNullOrWhiteSpace(o.AzerothCore?.AuthConnectionString))
            map[EmulatorType.AzerothCore] = new AzerothEmulatorRepository(
                o.AzerothCore.AuthConnectionString,
                o.AzerothCore.CharacterConnectionString,
                o.AzerothCore.WorldConnectionString);

        if (!string.IsNullOrWhiteSpace(o.CMaNGOS?.AuthConnectionString))
            map[EmulatorType.CMaNGOS] = new CMaNGOSEmulatorRepository(
                o.CMaNGOS.AuthConnectionString,
                o.CMaNGOS.CharacterConnectionString);

        _repos = map;
    }

    public IEmulatorRepository? Get(EmulatorType emulatorType) =>
        _repos.TryGetValue(emulatorType, out var repo) ? repo : null;
}
