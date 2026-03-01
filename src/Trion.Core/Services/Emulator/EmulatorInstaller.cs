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

    private readonly IAgentClient  _agent;
    private readonly AuditLogger   _audit;
    private readonly ILogger<EmulatorInstaller> _logger;

    public EmulatorInstaller(
        IAgentClient              agent,
        AuditLogger               audit,
        ILogger<EmulatorInstaller> logger)
    {
        _agent  = agent;
        _audit  = audit;
        _logger = logger;
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
        await Report(progress, "Validating client path", 5, ct);

        var (valid, reason) = ValidateWowClientPath(wowClientPath, installDirectory);
        if (!valid)
        {
            await ReportError(progress, "Validation failed", reason!, ct);
            return;
        }

        _logger.LogInformation("WoW client path validated: {Path}", wowClientPath);
        _audit.WriteInfo($"EmulatorInstaller: WoW client path validated — {wowClientPath}");

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

        _audit.WriteInfo($"EmulatorInstaller: starting git clone {repoUrl}");

        var launch = await _agent.LaunchProcessAsync(
            new LaunchRequest(GitPath, ["clone", repoUrl, installDirectory], null),
            ct: ct);

        if (!launch.Success)
        {
            await ReportError(progress, "Clone failed", launch.ErrorMessage, ct);
            _audit.WriteError($"EmulatorInstaller: git clone failed — {launch.ErrorMessage}");
            return;
        }

        // ── Step 3: Poll until git clone finishes ────────────────────────────
        _logger.LogInformation("git clone started (PID {Pid}); waiting for completion.", launch.Pid);

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
            await ReportError(progress, "Clone failed",
                $"Expected directory '{installDirectory}' not found after clone.", ct);
            _audit.WriteError($"EmulatorInstaller: post-clone directory missing — {installDirectory}");
            return;
        }

        _audit.WriteInfo($"EmulatorInstaller: git clone complete — {installDirectory}");
        await Report(progress, "Repository cloned", 50,
            $"{type} cloned successfully into {installDirectory}", ct);

        // ── Step 4: Final audit + completion ─────────────────────────────────
        await Report(progress, "Installation complete", 100, null, ct);
        _audit.WriteInfo($"EmulatorInstaller: installation complete for {type}");
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
