using Trion.Core.Abstractions.Database;
using Trion.Core.Abstractions.Services;

namespace Trion.Core.Database;

/// <summary>
/// Placeholder factory — returns null for all types until M4 registers real repositories.
/// Registered in DI so DI validation passes; swapped out in Milestone 4.
/// </summary>
public sealed class StubRepositoryFactory : IEmulatorRepositoryFactory
{
    public IEmulatorRepository? Get(EmulatorType emulatorType) => null;
}
