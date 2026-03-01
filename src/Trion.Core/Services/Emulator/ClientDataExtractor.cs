using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;
using Trion.Core.Logging;

namespace Trion.Core.Services.Emulator;

/// <summary>
/// Runs client data extraction tools (ad, vmap4extractor, mmaps_generator)
/// via the privileged agent and validates the output.
/// Each extraction step is retried up to three times on failure.
/// </summary>
public sealed class ClientDataExtractor
{
    private const int MaxRetries = 3;
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);

    private readonly IAgentClient _agent;
    private readonly ILogger      _log;

    public ClientDataExtractor(IAgentClient agent, TrionLogger trionLogger)
    {
        _agent = agent;
        _log   = trionLogger.CreateLogger(nameof(ClientDataExtractor));
    }

    /// <summary>
    /// Runs all three extraction phases for TrinityCore/AzerothCore.
    /// </summary>
    /// <param name="clientPath">Absolute path to the WoW client directory.</param>
    /// <param name="toolsPath">Absolute path to the directory containing the extractor binaries.</param>
    /// <param name="outputPath">Absolute path where extracted data will be written.</param>
    /// <param name="progress">Receives progress events during extraction.</param>
    public async Task ExtractAsync(
        string                    clientPath,
        string                    toolsPath,
        string                    outputPath,
        ChannelWriter<ExtractProgressEvent> progress,
        CancellationToken         ct = default)
    {
        // ── Phase 1: Map extraction (ad) ──────────────────────────────────────
        await RunWithRetryAsync(
            toolsPath, "ad", clientPath, outputPath,
            "Map extraction", 5, 30,
            ValidateMapOutput, progress, ct);

        // ── Phase 2: VMap extraction ───────────────────────────────────────────
        await RunWithRetryAsync(
            toolsPath, "vmap4extractor", clientPath, outputPath,
            "VMap extraction", 35, 65,
            ValidateVmapOutput, progress, ct);

        // ── Phase 3: MMap generation ───────────────────────────────────────────
        await RunWithRetryAsync(
            toolsPath, "mmaps_generator", clientPath, outputPath,
            "MMap generation", 70, 98,
            ValidateMmapOutput, progress, ct);

        _log.LogInformation("ClientDataExtractor: all phases complete.");
        await progress.WriteAsync(new ExtractProgressEvent("Complete", 100, "All data extracted."), ct);
        progress.TryComplete();
    }

    // ── Internal helpers ───────────────────────────────────────────────────────

    private async Task RunWithRetryAsync(
        string toolsPath,
        string toolName,
        string clientPath,
        string outputPath,
        string stepLabel,
        int startPct,
        int endPct,
        Func<string, bool> outputValidator,
        ChannelWriter<ExtractProgressEvent> progress,
        CancellationToken ct)
    {
        var executablePath = Path.Combine(toolsPath, toolName);
        _log.LogInformation("Extraction step '{Step}': {Exe}", stepLabel, executablePath);

        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            await progress.WriteAsync(new ExtractProgressEvent(
                stepLabel, startPct,
                attempt > 1 ? $"Attempt {attempt}/{MaxRetries}…" : null), ct);

            var launch = await _agent.LaunchProcessAsync(
                new LaunchRequest(executablePath, [], clientPath), ct: ct);

            if (!launch.Success)
            {
                _log.LogWarning("{Step} attempt {N} failed to launch: {Err}",
                    stepLabel, attempt, launch.ErrorMessage);

                if (attempt == MaxRetries)
                {
                    await progress.WriteAsync(new ExtractProgressEvent(
                        stepLabel, -1, $"Failed after {MaxRetries} attempts: {launch.ErrorMessage}",
                        IsError: true), ct);
                    progress.TryComplete(new InvalidOperationException(launch.ErrorMessage));
                    return;
                }
                continue;
            }

            // Poll until process exits
            int pollPct = startPct;
            while (launch.Pid is { } pid && launch.StartTime is { } st &&
                   await _agent.IsProcessAliveAsync(pid, st, ct))
            {
                pollPct = Math.Min(pollPct + 2, endPct - 2);
                await progress.WriteAsync(
                    new ExtractProgressEvent(stepLabel, pollPct, "Running…"), ct);
                await Task.Delay(PollInterval, ct);
            }

            // Validate output
            if (outputValidator(outputPath))
            {
                _log.LogInformation("{Step} complete.", stepLabel);
                await progress.WriteAsync(
                    new ExtractProgressEvent(stepLabel, endPct, $"{stepLabel} complete."), ct);
                return;   // success
            }

            _log.LogWarning("{Step} output validation failed on attempt {N}.", stepLabel, attempt);
            if (attempt == MaxRetries)
            {
                await progress.WriteAsync(new ExtractProgressEvent(
                    stepLabel, -1, $"Output validation failed after {MaxRetries} attempts.",
                    IsError: true), ct);
                progress.TryComplete(new InvalidOperationException($"{stepLabel}: output validation failed."));
            }
        }
    }

    // ── Output validators ──────────────────────────────────────────────────────

    private static bool ValidateMapOutput(string outputPath)
    {
        var mapsDir = Path.Combine(outputPath, "maps");
        return Directory.Exists(mapsDir) && Directory.GetFiles(mapsDir, "*.map").Length > 10;
    }

    private static bool ValidateVmapOutput(string outputPath)
    {
        var vmapsDir = Path.Combine(outputPath, "vmaps");
        return Directory.Exists(vmapsDir) && Directory.GetFiles(vmapsDir, "*.vmtree").Length > 0;
    }

    private static bool ValidateMmapOutput(string outputPath)
    {
        var mmapsDir = Path.Combine(outputPath, "mmaps");
        return Directory.Exists(mmapsDir) && Directory.GetFiles(mmapsDir, "*.mmtile").Length > 100;
    }
}
