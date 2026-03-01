using Trion.Core.Database.TrinityCore;

namespace Trion.Core.Database.AzerothCore;

/// <summary>
/// IEmulatorRepository for AzerothCore.
/// Auth DB schema is identical to TrinityCore for the fields Trion queries.
/// Override specific methods here when/if AzerothCore diverges.
/// </summary>
internal sealed class AzerothEmulatorRepository : TrinityEmulatorRepository
{
    internal AzerothEmulatorRepository(string authCs, string? charCs, string? worldCs)
        : base(authCs, charCs, worldCs) { }
}
