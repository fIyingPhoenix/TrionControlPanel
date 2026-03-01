using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;

namespace Trion.Platform.Shared;

/// <summary>
/// Connects to <c>Trion.Agent</c> over the platform IPC channel (named pipe on
/// Windows, Unix domain socket on Linux), signs every command with HMAC-SHA256,
/// and deserialises the response.
///
/// Wire protocol: 4-byte little-endian length prefix + UTF-8 JSON per message.
/// Thread-safety: a <see cref="SemaphoreSlim"/> serialises concurrent callers.
/// </summary>
public sealed class AgentClient : IAgentClient, IDisposable
{
    private readonly AgentClientOptions         _opts;
    private readonly ILogger<AgentClient>       _logger;
    private readonly SemaphoreSlim              _gate = new(1, 1);
    private static readonly TimeSpan            ConnectTimeout = TimeSpan.FromSeconds(5);
    private const int                           MaxRetries = 3;

    public AgentClient(IOptions<AgentClientOptions> opts, ILogger<AgentClient> logger)
    {
        _opts   = opts.Value;
        _logger = logger;
    }

    // ── IAgentClient ──────────────────────────────────────────────────────────

    public async Task<LaunchResult> LaunchProcessAsync(
        LaunchRequest request, string? pnToken = null, CancellationToken ct = default)
    {
        var cmd = new LaunchProcessCommand(
            request.ExecutablePath, request.Arguments, request.WorkingDirectory);
        var resp = await SendAsync<LaunchProcessCommand, LaunchProcessResponse>(
            AgentCommandType.LaunchProcess, cmd, pnToken, ct);

        return resp is null
            ? new LaunchResult(false, null, "No response from agent")
            : new LaunchResult(resp.Success, resp.Pid, resp.ErrorMessage);
    }

    public async Task<bool> KillProcessAsync(
        int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default)
    {
        var cmd  = new KillProcessCommand(pid, expectedStartTime);
        var resp = await SendAsync<KillProcessCommand, KillProcessResponse>(
            AgentCommandType.KillProcess, cmd, null, ct);
        return resp?.Success ?? false;
    }

    public async Task<ServiceStatus> GetServiceStatusAsync(
        string serviceName, CancellationToken ct = default)
    {
        var cmd  = new ServiceControlCommand(serviceName, "status");
        var resp = await SendAsync<ServiceControlCommand, ServiceControlResponse>(
            AgentCommandType.ServiceControl, cmd, null, ct);

        if (resp is null)
            return new ServiceStatus(serviceName, ServiceState.Unknown, "No response");

        var state = Enum.TryParse<ServiceState>(resp.CurrentState, out var s)
                    ? s : ServiceState.Unknown;
        return new ServiceStatus(serviceName, state, resp.ErrorMessage);
    }

    public async Task<bool> StartServiceAsync(string serviceName, CancellationToken ct = default)
    {
        var cmd  = new ServiceControlCommand(serviceName, "start");
        var resp = await SendAsync<ServiceControlCommand, ServiceControlResponse>(
            AgentCommandType.ServiceControl, cmd, null, ct);
        return resp?.Success ?? false;
    }

    public async Task<bool> StopServiceAsync(string serviceName, CancellationToken ct = default)
    {
        var cmd  = new ServiceControlCommand(serviceName, "stop");
        var resp = await SendAsync<ServiceControlCommand, ServiceControlResponse>(
            AgentCommandType.ServiceControl, cmd, null, ct);
        return resp?.Success ?? false;
    }

    public async Task<bool> RestartServiceAsync(string serviceName, CancellationToken ct = default)
    {
        var cmd  = new ServiceControlCommand(serviceName, "restart");
        var resp = await SendAsync<ServiceControlCommand, ServiceControlResponse>(
            AgentCommandType.ServiceControl, cmd, null, ct);
        return resp?.Success ?? false;
    }

    public async Task<bool> IsProcessAliveAsync(
        int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default)
    {
        var cmd  = new IsProcessAliveCommand(pid, expectedStartTime);
        var resp = await SendAsync<IsProcessAliveCommand, IsProcessAliveResponse>(
            AgentCommandType.IsProcessAlive, cmd, null, ct);
        return resp?.Alive ?? false;
    }

    public async Task<bool> PingAsync(CancellationToken ct = default)
    {
        var resp = await SendAsync<PingCommand, PingResponse>(
            AgentCommandType.Ping, new PingCommand(), null, ct);
        return resp is not null;
    }

    // ── Core send/receive ─────────────────────────────────────────────────────

    private async Task<TResponse?> SendAsync<TCommand, TResponse>(
        string commandType,
        TCommand body,
        string? pnToken,
        CancellationToken ct)
        where TCommand  : class
        where TResponse : class
    {
        var payload = JsonSerializer.Serialize(body);
        var hmac    = Sign(commandType + payload, _opts.HmacSharedKey);
        var command = new AgentCommand(commandType, payload, hmac, pnToken);
        var request = JsonSerializer.Serialize(command);

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
        timeout.CancelAfter(TimeSpan.FromSeconds(_opts.TimeoutSeconds));

        await _gate.WaitAsync(timeout.Token);
        try
        {
            var responseJson = await SendWithRetryAsync(request, timeout.Token);
            var envelope     = JsonSerializer.Deserialize<AgentResponse>(responseJson);

            if (envelope is null || !envelope.Success || envelope.Payload is null)
            {
                _logger.LogWarning("Agent returned failure for {Type}: {Error}",
                    commandType, envelope?.Error ?? "null envelope");
                return null;
            }

            return JsonSerializer.Deserialize<TResponse>(envelope.Payload);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<string> SendWithRetryAsync(string requestJson, CancellationToken ct)
    {
        var requestBytes = Encoding.UTF8.GetBytes(requestJson);
        Exception? last  = null;

        for (int attempt = 0; attempt < MaxRetries; attempt++)
        {
            if (attempt > 0)
                await Task.Delay(TimeSpan.FromSeconds(1), ct);

            try
            {
                return await SendOnceAsync(requestBytes, ct);
            }
            catch (IOException ex)
            {
                last = ex;
                _logger.LogWarning("Agent IPC attempt {N} failed: {Msg}", attempt + 1, ex.Message);
            }
            catch (SocketException ex)
            {
                last = ex;
                _logger.LogWarning("Agent socket attempt {N} failed: {Msg}", attempt + 1, ex.Message);
            }
        }

        throw new IOException($"Agent unreachable after {MaxRetries} attempts", last);
    }

    private async Task<string> SendOnceAsync(byte[] requestBytes, CancellationToken ct)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return await SendViaPipeAsync(requestBytes, ct);

        return await SendViaSocketAsync(requestBytes, ct);
    }

    // ── Windows transport ─────────────────────────────────────────────────────

    private async Task<string> SendViaPipeAsync(byte[] requestBytes, CancellationToken ct)
    {
        await using var pipe = new NamedPipeClientStream(
            ".", _opts.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

        await pipe.ConnectAsync((int)ConnectTimeout.TotalMilliseconds, ct);

        await WriteMessageAsync(pipe, requestBytes, ct);
        var responseBytes = await ReadMessageAsync(pipe, ct);
        return Encoding.UTF8.GetString(responseBytes);
    }

    // ── Linux transport ───────────────────────────────────────────────────────

    private async Task<string> SendViaSocketAsync(byte[] requestBytes, CancellationToken ct)
    {
        using var socket = new Socket(
            AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

        await socket.ConnectAsync(new UnixDomainSocketEndPoint(_opts.SocketPath), ct);
        using var ns = new NetworkStream(socket, ownsSocket: false);

        await WriteMessageAsync(ns, requestBytes, ct);
        var responseBytes = await ReadMessageAsync(ns, ct);
        return Encoding.UTF8.GetString(responseBytes);
    }

    // ── Wire-protocol helpers ─────────────────────────────────────────────────

    private const int MaxMessageBytes = 4 * 1024 * 1024;

    private static async Task WriteMessageAsync(Stream stream, byte[] data, CancellationToken ct)
    {
        await stream.WriteAsync(BitConverter.GetBytes(data.Length), ct);
        await stream.WriteAsync(data, ct);
        await stream.FlushAsync(ct);
    }

    private static async Task<byte[]> ReadMessageAsync(Stream stream, CancellationToken ct)
    {
        var lenBuf = new byte[4];
        await stream.ReadExactlyAsync(lenBuf, ct);
        var length = BitConverter.ToInt32(lenBuf, 0);

        if (length <= 0 || length > MaxMessageBytes)
            throw new InvalidDataException($"Invalid response length from agent: {length}");

        var buffer = new byte[length];
        await stream.ReadExactlyAsync(buffer, ct);
        return buffer;
    }

    // ── HMAC (inlined to avoid referencing Trion.Agent from Trion.Platform) ──

    private static string Sign(string payload, string key)
    {
        var keyBytes  = Encoding.UTF8.GetBytes(key);
        var dataBytes = Encoding.UTF8.GetBytes(payload);
        return Convert.ToHexString(HMACSHA256.HashData(keyBytes, dataBytes)).ToLowerInvariant();
    }

    public void Dispose() => _gate.Dispose();
}
