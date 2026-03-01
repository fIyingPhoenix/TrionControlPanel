using Trion.Core.Abstractions.Services;

namespace Trion.Core.Abstractions.Database;

/// <summary>
/// Resolves the correct <see cref="IEmulatorRepository"/> implementation
/// for a given <see cref="EmulatorType"/>.
/// Implemented in Milestone 4 with real MySQL repositories.
/// </summary>
public interface IEmulatorRepositoryFactory
{
    /// <summary>
    /// Returns the repository for the given emulator type,
    /// or <c>null</c> if no database connection has been configured for it.
    /// </summary>
    IEmulatorRepository? Get(EmulatorType emulatorType);
}
