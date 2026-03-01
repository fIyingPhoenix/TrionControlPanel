using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Logging;

namespace Trion.Agent.IPC;

/// <summary>
/// Listens for IPC connections from Trion.Web:
///   • Windows — named pipe  (<see cref="AgentOptions.PipeName"/>)
///   • Linux   — Unix domain socket  (<see cref="AgentOptions.SocketPath"/>)
///
/// Wire protocol (per connection):
///   → 4-byte little-endian message length + UTF-8 JSON (AgentCommand)
///   ← 4-byte little-endian message length + UTF-8 JSON (AgentResponse)
///
/// Each connection is handled in its own Task.
/// </summary>
public sealed class AgentPipeServer : BackgroundService
{
    private readonly AgentOptions      _options;
    private readonly CommandDispatcher _dispatcher;
    private readonly ILogger           _log;

    public AgentPipeServer(
        IOptions<AgentOptions> options,
        CommandDispatcher      dispatcher,
        TrionLogger            trionLogger)
    {
        _options    = options.Value;
        _dispatcher = dispatcher;
        _log        = trionLogger.CreateLogger(nameof(AgentPipeServer));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return RunWindowsPipeAsync(stoppingToken);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return RunLinuxSocketAsync(stoppingToken);

        _log.LogError("AgentPipeServer: unsupported platform — agent will not accept connections");
        return Task.Delay(Timeout.Infinite, stoppingToken);
    }

    // ── Windows: named pipe ───────────────────────────────────────────────────

    private async Task RunWindowsPipeAsync(CancellationToken ct)
    {
        _log.LogInformation("AgentPipeServer listening on named pipe: {Name}", _options.PipeName);

        while (!ct.IsCancellationRequested)
        {
            var pipe = new NamedPipeServerStream(
                _options.PipeName,
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous | PipeOptions.WriteThrough);

            try
            {
                await pipe.WaitForConnectionAsync(ct);
                _ = HandlePipeAsync(pipe, ct);  // fire-and-forget
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                await pipe.DisposeAsync();
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error accepting named pipe connection");
                await pipe.DisposeAsync();
            }
        }
    }

    private async Task HandlePipeAsync(NamedPipeServerStream pipe, CancellationToken ct)
    {
        await using (pipe)
        {
            try
            {
                var req  = await ReadMessageAsync(pipe, ct);
                var json = Encoding.UTF8.GetString(req);
                var resp = await _dispatcher.DispatchAsync(json, ct);
                await WriteMessageAsync(pipe, Encoding.UTF8.GetBytes(resp), ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex) { _log.LogError(ex, "Error handling pipe connection"); }
        }
    }

    // ── Linux: Unix domain socket ─────────────────────────────────────────────

    private async Task RunLinuxSocketAsync(CancellationToken ct)
    {
        var socketPath = _options.SocketPath;

        // Clean up stale socket file from a previous (crashed) run
        if (File.Exists(socketPath))
            File.Delete(socketPath);

        // Ensure parent directory exists
        var dir = Path.GetDirectoryName(socketPath);
        if (dir is not null && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        using var server = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        server.Bind(new UnixDomainSocketEndPoint(socketPath));
        server.Listen(backlog: 10);

        // Allow group read/write (0660) — both agent and web service share a group in production
#pragma warning disable CA1416  // This method is only called on Linux (see ExecuteAsync)
        File.SetUnixFileMode(socketPath,
            UnixFileMode.UserRead  | UnixFileMode.UserWrite |
            UnixFileMode.GroupRead | UnixFileMode.GroupWrite);
#pragma warning restore CA1416

        _log.LogInformation("AgentPipeServer listening on socket: {Path}", socketPath);

        while (!ct.IsCancellationRequested)
        {
            Socket client;
            try
            {
                client = await server.AcceptAsync(ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error accepting Unix socket connection");
                continue;
            }

            _ = HandleSocketAsync(client, ct);  // fire-and-forget
        }

        // Clean up socket file on graceful shutdown
        if (File.Exists(socketPath))
            File.Delete(socketPath);
    }

    private async Task HandleSocketAsync(Socket client, CancellationToken ct)
    {
        using var ns = new NetworkStream(client, ownsSocket: true);
        try
        {
            var req  = await ReadMessageAsync(ns, ct);
            var json = Encoding.UTF8.GetString(req);
            var resp = await _dispatcher.DispatchAsync(json, ct);
            await WriteMessageAsync(ns, Encoding.UTF8.GetBytes(resp), ct);
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
        catch (Exception ex) { _log.LogError(ex, "Error handling socket connection"); }
    }

    // ── Wire-protocol helpers ─────────────────────────────────────────────────

    private const int MaxMessageBytes = 4 * 1024 * 1024; // 4 MB safety cap

    private static async Task<byte[]> ReadMessageAsync(Stream stream, CancellationToken ct)
    {
        var lenBuf = new byte[4];
        await stream.ReadExactlyAsync(lenBuf, ct);
        var length = BitConverter.ToInt32(lenBuf, 0);

        if (length <= 0 || length > MaxMessageBytes)
            throw new InvalidDataException($"Invalid message length: {length}");

        var buffer = new byte[length];
        await stream.ReadExactlyAsync(buffer, ct);
        return buffer;
    }

    private static async Task WriteMessageAsync(Stream stream, byte[] data, CancellationToken ct)
    {
        var lenBuf = BitConverter.GetBytes(data.Length);
        await stream.WriteAsync(lenBuf, ct);
        await stream.WriteAsync(data, ct);
        await stream.FlushAsync(ct);
    }
}
