using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;
using Trion.Core.Logging;

namespace Trion.Agent.Handlers;

/// <summary>
/// Handles start / stop / restart / status operations on OS services.
/// Linux: delegates to <c>systemctl</c> via <see cref="ProcessLaunchHandler"/>.
/// Windows: stub — full <c>ServiceController</c> integration is a future milestone.
/// </summary>
public sealed class ServiceControlHandler
{
    private readonly ProcessLaunchHandler _launcher;
    private readonly ILogger              _log;

    public ServiceControlHandler(ProcessLaunchHandler launcher, TrionLogger trionLogger)
    {
        _launcher = launcher;
        _log      = trionLogger.CreateLogger(nameof(ServiceControlHandler));
    }

    public async Task<ServiceControlResponse> HandleAsync(
        ServiceControlCommand cmd, CancellationToken ct = default)
    {
        var action = cmd.Action.ToLowerInvariant();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return await HandleLinuxAsync(cmd.ServiceName, action, ct);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return HandleWindows(cmd.ServiceName, action);

        return new ServiceControlResponse(
            Success: false,
            PreviousState: ServiceState.Unknown.ToString(),
            CurrentState:  ServiceState.Unknown.ToString(),
            ErrorMessage:  "Unsupported platform");
    }

    // ── Linux: delegate to systemctl ─────────────────────────────────────────

    private async Task<ServiceControlResponse> HandleLinuxAsync(
        string serviceName, string action, CancellationToken ct)
    {
        // "status" maps to "is-active" but we reuse status for simplicity
        var systemctlAction = action switch
        {
            "start"   => "start",
            "stop"    => "stop",
            "restart" => "restart",
            "status"  => "status",
            _         => null
        };

        if (systemctlAction is null)
        {
            return new ServiceControlResponse(false,
                ServiceState.Unknown.ToString(), ServiceState.Unknown.ToString(),
                $"Unknown action: {action}");
        }

        // Query previous state
        var prevState = await QueryLinuxStateAsync(serviceName, ct);

        var launchCmd = new LaunchProcessCommand(
            ExecutablePath:   "/usr/bin/systemctl",
            Arguments:        [systemctlAction, serviceName],
            WorkingDirectory: null);

        var result = await _launcher.HandleAsync(launchCmd, ct);

        if (!result.Success)
        {
            return new ServiceControlResponse(false,
                prevState.ToString(), prevState.ToString(),
                result.ErrorMessage);
        }

        // Give systemctl a moment to transition, then re-query
        await Task.Delay(500, ct);
        var newState = await QueryLinuxStateAsync(serviceName, ct);

        _log.LogInformation("Service {Name}: {Action} → {State}", serviceName, action, newState);

        return new ServiceControlResponse(
            Success:       true,
            PreviousState: prevState.ToString(),
            CurrentState:  newState.ToString(),
            ErrorMessage:  null);
    }

    private async Task<ServiceState> QueryLinuxStateAsync(string serviceName, CancellationToken ct)
    {
        var isActiveCmd = new LaunchProcessCommand(
            ExecutablePath:   "/usr/bin/systemctl",
            Arguments:        ["is-active", "--quiet", serviceName],
            WorkingDirectory: null);

        // is-active exits 0 if active, non-zero otherwise — but since we don't
        // capture exit code directly here, we just treat success as Running.
        var result = await _launcher.HandleAsync(isActiveCmd, ct);
        return result.Success ? ServiceState.Running : ServiceState.Stopped;
    }

    // ── Windows: stub (full ServiceController impl is a future milestone) ────

    private static ServiceControlResponse HandleWindows(string serviceName, string action)
    {
        // TODO (future): use System.ServiceProcess.ServiceController
        return new ServiceControlResponse(
            Success:       false,
            PreviousState: ServiceState.Unknown.ToString(),
            CurrentState:  ServiceState.Unknown.ToString(),
            ErrorMessage:  "Windows service control not yet implemented");
    }
}
