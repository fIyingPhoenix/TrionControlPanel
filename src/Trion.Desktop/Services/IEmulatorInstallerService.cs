using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface IEmulatorInstallerService
{
    /// <summary>Download the full emulator package and extract it to <paramref name="installPath"/>.</summary>
    Task InstallAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct);

    /// <summary>Re-download and re-extract, overwriting existing files (acts as full repair).</summary>
    Task RepairAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct);

    /// <summary>Deletes the installation directory.</summary>
    Task UninstallAsync(string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct);

    /// <summary>Fetch the latest versions for all expansions from the API.</summary>
    Task<EmulatorVersions> FetchLatestVersionsAsync(CancellationToken ct);

    /// <summary>Read the local version string from a world-server exe (returns "—" on failure).</summary>
    string GetLocalVersion(string exePath);
}
