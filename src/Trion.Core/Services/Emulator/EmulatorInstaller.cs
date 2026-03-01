using System.Runtime.InteropServices;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;
using Trion.Core.Logging;

namespace Trion.Core.Services.Emulator;

/// <summary>
/// Clones an emulator repository via the privileged agent and prepares the install directory.
/// All Git URLs are hardcoded constants — not configurable by the user.
/// </summary>
public sealed class EmulatorInstaller
{
    // ── Hardcoded repository URLs ──────────────────────────────────────────────
    private const string TrinityCoreUrl =
        "https://github.com/TrinityCore/TrinityCore.git";
    private const string AzerothCoreUrl =
        "https://github.com/azerothcore/azerothcore-wotlk.git";
    private const string CMaNGOSUrl =
        "https://github.com/cmangos/mangos-wotlk.git";

    // ── Git executable paths ───────────────────────────────────────────────────
    private static readonly string GitPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? @"C:\Program Files\Git\bin\git.exe"
        : "/usr/bin/git";

    // ── Expected WoW client binaries by platform ───────────────────────────────
    private static readonly string[] WowClientBinaries = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? ["WoW.exe", "Wow.exe"]
        : ["wow", "Wow"];

    private readonly IAgentClient _agent;
    private readonly ILogger      _log;

    public EmulatorInstaller(IAgentClient agent, TrionLogger trionLogger)
    {
        _agent = agent;
        _log   = trionLogger.CreateLogger(nameof(EmulatorInstaller));
    }

    /// <summary>
    /// Validates the WoW client directory, then clones the emulator repository into
    /// <paramref name="installDirectory"/>. Progress is written to <paramref name="progress"/>.
    /// </summary>
    public async Task InstallAsync(
        EmulatorType              type,
        string                    wowClientPath,
        string                    installDirectory,
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken         ct = default)
    {
        // ── Step 1: Validate WoW client path ──────────────────────────────────
        await Report(progress, "Validating client path", 5, null, ct);

        var (valid, reason) = ValidateWowClientPath(wowClientPath, installDirectory);
        if (!valid)
        {
            await ReportError(progress, "Validation failed", reason!, ct);
            return;
        }

        _log.LogInformation("WoW client path validated: {Path}", wowClientPath);

        // ── Step 2: Clone emulator repository via agent ───────────────────────
        var repoUrl = type switch
        {
            EmulatorType.TrinityCore => TrinityCoreUrl,
            EmulatorType.AzerothCore => AzerothCoreUrl,
            EmulatorType.CMaNGOS     => CMaNGOSUrl,
            _                        => throw new ArgumentOutOfRangeException(nameof(type))
        };

        await Report(progress, "Cloning repository", 10,
            $"git clone {repoUrl} → {installDirectory}", ct);

        _log.LogInformation("Starting git clone {Url}", repoUrl);

        var launch = await _agent.LaunchProcessAsync(
            new LaunchRequest(GitPath, ["clone", repoUrl, installDirectory], null),
            ct: ct);

        if (!launch.Success)
        {
            _log.LogError("git clone failed: {Err}", launch.ErrorMessage);
            await ReportError(progress, "Clone failed", launch.ErrorMessage, ct);
            return;
        }

        // ── Step 3: Poll until git clone finishes ────────────────────────────
        _log.LogInformation("git clone started (PID {Pid}); waiting for completion.", launch.Pid);

        int pollPercent = 10;
        while (launch.Pid is { } pid && launch.StartTime is { } startTime
               && await _agent.IsProcessAliveAsync(pid, startTime, ct))
        {
            pollPercent = Math.Min(pollPercent + 5, 45);
            await Report(progress, "Cloning repository", pollPercent,
                "Waiting for git clone to finish…", ct);
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
        }

        // Verify the target directory exists (basic success check)
        if (!Directory.Exists(installDirectory))
        {
            _log.LogError("Post-clone directory missing: {Dir}", installDirectory);
            await ReportError(progress, "Clone failed",
                $"Expected directory '{installDirectory}' not found after clone.", ct);
            return;
        }

        _log.LogInformation("git clone complete: {Dir}", installDirectory);
        await Report(progress, "Repository cloned", 50,
            $"{type} cloned successfully into {installDirectory}", ct);

        // ── Step 4: Final completion ──────────────────────────────────────────
        _log.LogInformation("Installation complete for {Type}", type);
        await Report(progress, "Installation complete", 100, null, ct);
    }

    // ── Validation ─────────────────────────────────────────────────────────────

    private static (bool Valid, string? Reason) ValidateWowClientPath(
        string wowClientPath, string installDirectory)
    {
        if (string.IsNullOrWhiteSpace(wowClientPath))
            return (false, "WoW client path is required.");

        if (!Path.IsPathRooted(wowClientPath))
            return (false, "WoW client path must be an absolute path.");

        // Must not be inside the Trion install directory
        if (!string.IsNullOrEmpty(installDirectory) &&
            wowClientPath.StartsWith(installDirectory, StringComparison.OrdinalIgnoreCase))
            return (false, "WoW client path must not be inside the emulator install directory.");

        // Must contain an expected client binary
        var foundBinary = WowClientBinaries.Any(
            bin => File.Exists(Path.Combine(wowClientPath, bin)));

        if (!foundBinary)
            return (false,
                $"WoW client binary not found in '{wowClientPath}'. " +
                $"Expected one of: {string.Join(", ", WowClientBinaries)}");

        return (true, null);
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    private static ValueTask Report(
        ChannelWriter<InstallProgressEvent> w,
        string step, int pct, string? msg,
        CancellationToken ct) =>
        w.WriteAsync(new InstallProgressEvent(step, pct, msg), ct);

    private static async ValueTask ReportError(
        ChannelWriter<InstallProgressEvent> w,
        string step, string? msg,
        CancellationToken ct)
    {
        await w.WriteAsync(new InstallProgressEvent(step, -1, msg, IsError: true), ct);
        w.TryComplete(new InvalidOperationException(msg));
    }
}
