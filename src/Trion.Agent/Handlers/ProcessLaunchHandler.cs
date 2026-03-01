using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Trion.Agent.Security;
using Trion.Core.Agent;

namespace Trion.Agent.Handlers;

/// <summary>
/// Validates and launches an emulator process on behalf of Trion.Web.
/// Security: the executable path and every argument are checked against
/// <see cref="CommandAllowlist"/> before a process is started.
/// The process is started without a shell (UseShellExecute = false) and
/// with stdout/stderr redirected to the logger.
/// </summary>
public sealed class ProcessLaunchHandler
{
    private readonly CommandAllowlist          _allowlist;
    private readonly ILogger<ProcessLaunchHandler> _logger;

    public ProcessLaunchHandler(CommandAllowlist allowlist, ILogger<ProcessLaunchHandler> logger)
    {
        _allowlist = allowlist;
        _logger    = logger;
    }

    public Task<LaunchProcessResponse> HandleAsync(LaunchProcessCommand cmd, CancellationToken ct = default)
    {
        // ── Security gate ────────────────────────────────────────────────────
        if (!_allowlist.IsPermitted(cmd.ExecutablePath, cmd.Arguments))
        {
            _logger.LogWarning("Launch rejected — not in allowlist: {Path}", cmd.ExecutablePath);
            return Task.FromResult(new LaunchProcessResponse(
                Success: false, Pid: null, StartTime: null,
                ErrorMessage: "Executable or arguments not permitted"));
        }

        var workDir = cmd.WorkingDirectory
                      ?? Path.GetDirectoryName(cmd.ExecutablePath)
                      ?? AppContext.BaseDirectory;

        var psi = new ProcessStartInfo
        {
            FileName               = cmd.ExecutablePath,
            WorkingDirectory       = workDir,
            UseShellExecute        = false,
            CreateNoWindow         = true,
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
        };

        // Use ArgumentList — never the single Arguments string — to avoid shell injection
        foreach (var arg in cmd.Arguments)
            psi.ArgumentList.Add(arg);

        Process process;
        try
        {
            process = Process.Start(psi)
                      ?? throw new InvalidOperationException("Process.Start returned null");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start process: {Path}", cmd.ExecutablePath);
            return Task.FromResult(new LaunchProcessResponse(
                Success: false, Pid: null, StartTime: null,
                ErrorMessage: ex.Message));
        }

        // Relay stdout/stderr to logger — fire and forget
        var procName = Path.GetFileNameWithoutExtension(cmd.ExecutablePath);
        _ = RelayStreamAsync(process.StandardOutput, procName, isError: false);
        _ = RelayStreamAsync(process.StandardError,  procName, isError: true);

        _logger.LogInformation("Launched {Name} (PID {Pid})", procName, process.Id);

        return Task.FromResult(new LaunchProcessResponse(
            Success:      true,
            Pid:          process.Id,
            StartTime:    process.StartTime.ToUniversalTime(),
            ErrorMessage: null));
    }

    private async Task RelayStreamAsync(StreamReader reader, string name, bool isError)
    {
        try
        {
            string? line;
            while ((line = await reader.ReadLineAsync()) is not null)
            {
                if (isError)
                    _logger.LogError("[{Name}] {Line}", name, line);
                else
                    _logger.LogInformation("[{Name}] {Line}", name, line);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Stream relay ended for {Name}", name);
        }
    }
}
