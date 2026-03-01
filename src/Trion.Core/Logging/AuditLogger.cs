using System.Text.Json;

namespace Trion.Core.Logging;

public sealed class AuditLogger : IAsyncDisposable
{
    private readonly string _folder;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private StreamWriter? _writer;
    private DateTime _currentDate;

    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public AuditLogger(string? folder = null)
    {
        _folder = folder ?? Path.Combine(AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(_folder);
        OpenFile(DateTime.UtcNow.Date);
    }

    public void Record(AuditEvent evt)
    {
        // Fire-and-forget — audit writes must not block callers
        _ = WriteAsync(evt);
    }

    private async Task WriteAsync(AuditEvent evt)
    {
        await _gate.WaitAsync().ConfigureAwait(false);
        try
        {
            var today = DateTime.UtcNow.Date;
            if (today != _currentDate)
            {
                _writer?.Dispose();
                OpenFile(today);
            }

            var line = JsonSerializer.Serialize(evt, _jsonOpts);
            await _writer!.WriteLineAsync(line).ConfigureAwait(false);
            await _writer.FlushAsync().ConfigureAwait(false);
        }
        catch
        {
            // Audit write failures must not surface to callers under any circumstances
        }
        finally
        {
            _gate.Release();
        }
    }

    private void OpenFile(DateTime date)
    {
        _currentDate = date;
        var path = Path.Combine(_folder, $"audit-{date:yyyyMMdd}.log");
        // FileShare.Read: other processes can read but not write
        var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
        _writer = new StreamWriter(stream, System.Text.Encoding.UTF8) { AutoFlush = false };
    }

    public async ValueTask DisposeAsync()
    {
        await _gate.WaitAsync().ConfigureAwait(false);
        try
        {
            _writer?.Dispose();
        }
        finally
        {
            _gate.Release();
            _gate.Dispose();
        }
    }
}
