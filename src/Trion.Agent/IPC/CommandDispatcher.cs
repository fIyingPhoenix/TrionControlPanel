using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Agent.Handlers;
using Trion.Agent.Json;
using Trion.Agent.Security;
using Trion.Core.Agent;

namespace Trion.Agent.IPC;

/// <summary>
/// Validates the HMAC on every incoming request, then routes the command
/// to the appropriate handler and returns a serialised <see cref="AgentResponse"/>.
/// </summary>
public sealed class CommandDispatcher
{
    private readonly HmacValidator            _hmac;
    private readonly string                   _sharedKey;
    private readonly ProcessLaunchHandler     _launch;
    private readonly ServiceControlHandler    _service;
    private readonly ProcessKillHandler       _kill;
    private readonly ILogger<CommandDispatcher> _logger;

    public CommandDispatcher(
        HmacValidator              hmac,
        IOptions<AgentOptions>     opts,
        ProcessLaunchHandler       launch,
        ServiceControlHandler      service,
        ProcessKillHandler         kill,
        ILogger<CommandDispatcher> logger)
    {
        _hmac       = hmac;
        _sharedKey  = opts.Value.HmacSharedKey;
        _launch     = launch;
        _service    = service;
        _kill       = kill;
        _logger     = logger;
    }

    /// <summary>
    /// Deserializes, validates HMAC, dispatches to handler, returns JSON response.
    /// Never throws — all errors are encoded in the returned <see cref="AgentResponse"/>.
    /// </summary>
    public async Task<string> DispatchAsync(string requestJson, CancellationToken ct = default)
    {
        // ── Deserialize envelope ─────────────────────────────────────────────
        AgentCommand? command;
        try
        {
            command = JsonSerializer.Deserialize(requestJson, AgentJsonContext.Default.AgentCommand);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to deserialize AgentCommand");
            return Fail("INVALID_JSON");
        }

        if (command is null)
            return Fail("NULL_COMMAND");

        // ── HMAC validation ──────────────────────────────────────────────────
        var signedData = command.CommandType + command.Payload;
        if (!_hmac.Validate(signedData, command.Hmac, _sharedKey))
        {
            _logger.LogWarning("HMAC validation failed for command type: {Type}", command.CommandType);
            return Fail("INVALID_HMAC");
        }

        // ── Route ────────────────────────────────────────────────────────────
        try
        {
            return command.CommandType switch
            {
                AgentCommandType.LaunchProcess  => await LaunchAsync(command.Payload, ct),
                AgentCommandType.KillProcess    => await KillAsync(command.Payload, ct),
                AgentCommandType.ServiceControl => await ServiceAsync(command.Payload, ct),
                AgentCommandType.IsProcessAlive => await IsAliveAsync(command.Payload, ct),
                AgentCommandType.Ping           => Ping(),
                _                               => Fail($"UNKNOWN_COMMAND:{command.CommandType}")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error dispatching {Type}", command.CommandType);
            return Fail($"INTERNAL_ERROR:{ex.GetType().Name}");
        }
    }

    // ── Per-command dispatch ──────────────────────────────────────────────────

    private async Task<string> LaunchAsync(string payload, CancellationToken ct)
    {
        var cmd = Deserialize(payload, AgentJsonContext.Default.LaunchProcessCommand);
        if (cmd is null) return Fail("BAD_PAYLOAD");
        var resp = await _launch.HandleAsync(cmd, ct);
        return Succeed(JsonSerializer.Serialize(resp, AgentJsonContext.Default.LaunchProcessResponse));
    }

    private async Task<string> KillAsync(string payload, CancellationToken ct)
    {
        var cmd = Deserialize(payload, AgentJsonContext.Default.KillProcessCommand);
        if (cmd is null) return Fail("BAD_PAYLOAD");
        var resp = await _kill.HandleAsync(cmd, ct);
        return Succeed(JsonSerializer.Serialize(resp, AgentJsonContext.Default.KillProcessResponse));
    }

    private async Task<string> ServiceAsync(string payload, CancellationToken ct)
    {
        var cmd = Deserialize(payload, AgentJsonContext.Default.ServiceControlCommand);
        if (cmd is null) return Fail("BAD_PAYLOAD");
        var resp = await _service.HandleAsync(cmd, ct);
        return Succeed(JsonSerializer.Serialize(resp, AgentJsonContext.Default.ServiceControlResponse));
    }

    private async Task<string> IsAliveAsync(string payload, CancellationToken ct)
    {
        var cmd = Deserialize(payload, AgentJsonContext.Default.IsProcessAliveCommand);
        if (cmd is null) return Fail("BAD_PAYLOAD");
        var resp = await _kill.IsAliveAsync(cmd, ct);
        return Succeed(JsonSerializer.Serialize(resp, AgentJsonContext.Default.IsProcessAliveResponse));
    }

    private static string Ping()
    {
        var resp = new PingResponse(DateTimeOffset.UtcNow);
        return Succeed(JsonSerializer.Serialize(resp, AgentJsonContext.Default.PingResponse));
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static T? Deserialize<T>(string payload, System.Text.Json.Serialization.Metadata.JsonTypeInfo<T> info)
    {
        try { return JsonSerializer.Deserialize(payload, info); }
        catch { return default; }
    }

    private static string Fail(string error) =>
        JsonSerializer.Serialize(
            new AgentResponse(false, null, error),
            AgentJsonContext.Default.AgentResponse);

    private static string Succeed(string responsePayload) =>
        JsonSerializer.Serialize(
            new AgentResponse(true, responsePayload, null),
            AgentJsonContext.Default.AgentResponse);
}
