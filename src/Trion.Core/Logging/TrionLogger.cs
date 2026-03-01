using System.Collections.Concurrent;
using System.IO.Compression;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Trion.Core.Logging;

/// <summary>
/// Custom ILoggerProvider that writes to dated, auto-compressed log files.
/// Backed by a bounded BlockingCollection on a dedicated background thread.
/// </summary>
public sealed class TrionLogger : ILoggerProvider, IAsyncDisposable
{
    private readonly IOptionsMonitor<LoggerOptions> _optsMon;
    private readonly BlockingCollection<LogEntry>   _queue = new(boundedCapacity: 1000);
    private readonly CancellationTokenSource        _cts   = new();
    private readonly Thread                         _writerThread;
    private readonly SemaphoreSlim                  _fileGate = new(1, 1);

    private long          _droppedCount;
    private DateTimeOffset _lastDropLogTime;
    private StreamWriter? _todayWriter;
    private DateTime      _todayDate;

    public TrionLogger(IOptionsMonitor<LoggerOptions> optsMon)
    {
        _optsMon = optsMon;
        Directory.CreateDirectory(ResolveFolder());
        OpenTodayFile();

        _writerThread = new Thread(WritePump)
        {
            IsBackground = true,
            Name         = "TrionLogWriter"
        };
        _writerThread.Start();

        _ = Task.Run(HouseKeepingLoop);
    }

    // ── ILoggerProvider ──────────────────────────────────────────────────────

    public ILogger CreateLogger(string categoryName)
        => new TrionLoggerCategory(this, categoryName);

    // ── Internal enqueue called by TrionLoggerCategory ───────────────────────

    internal void Enqueue(MsLogLevel level, string category, string message, Exception? exception)
    {
        var opts      = _optsMon.CurrentValue;
        var trionLevel = MapLevel(level);

        bool allowed = trionLevel switch
        {
            LogLevel.Info    => opts.ShowInfo,
            LogLevel.Success => opts.ShowSuccess,
            LogLevel.Warning => opts.ShowWarning,
            LogLevel.Error   => opts.ShowError,
            _                => false
        };

        if (!allowed) return;

        var text = exception is null ? message : $"{message}\n{exception}";
        var entry = new LogEntry(
            Timestamp: DateTimeOffset.UtcNow,
            Level:     trionLevel,
            Message:   text,
            Category:  category,
            Member:    string.Empty,
            FilePath:  string.Empty,
            Line:      0);

        if (!_queue.TryAdd(entry))
            Interlocked.Increment(ref _droppedCount);
    }

    // ── Background writer ────────────────────────────────────────────────────

    private void WritePump()
    {
        try
        {
            foreach (var entry in _queue.GetConsumingEnumerable(_cts.Token))
            {
                FlushDropCountIfNeeded();
                WriteEntry(entry);
            }
        }
        catch (OperationCanceledException) { /* graceful shutdown */ }
        catch { /* best effort — writer thread must not crash the process */ }
    }

    private void FlushDropCountIfNeeded()
    {
        var dropped = Interlocked.Exchange(ref _droppedCount, 0);
        if (dropped == 0) return;
        if ((DateTimeOffset.UtcNow - _lastDropLogTime).TotalMinutes < 1)
        {
            // Not time yet — put the count back
            Interlocked.Add(ref _droppedCount, dropped);
            return;
        }

        _lastDropLogTime = DateTimeOffset.UtcNow;
        var warn = new LogEntry(DateTimeOffset.UtcNow, LogLevel.Warning,
            $"Log queue overflow: {dropped} entries dropped.",
            "TrionLogger", string.Empty, string.Empty, 0);
        WriteEntry(warn);
    }

    private void WriteEntry(LogEntry entry)
    {
        var opts = _optsMon.CurrentValue;
        var line = Format(entry);
        if (opts.WriteToConsole) WriteConsole(line, entry.Level);
        if (opts.WriteToFile)    WriteFileSafe(line);
    }

    private void WriteFileSafe(string line)
    {
        _fileGate.Wait();
        try
        {
            if (DateTime.UtcNow.Date != _todayDate)
            {
                _todayWriter?.Dispose();
                OpenTodayFile();
            }
            _todayWriter!.WriteLine(line);
            _todayWriter.Flush();
        }
        catch { /* best effort */ }
        finally { _fileGate.Release(); }
    }

    private void OpenTodayFile()
    {
        _todayDate  = DateTime.UtcNow.Date;
        var folder  = ResolveFolder();
        Directory.CreateDirectory(folder);
        var path    = Path.Combine(folder, $"trion-{_todayDate:yyyyMMdd}.log");
        _todayWriter = new StreamWriter(path, append: true, Encoding.UTF8) { AutoFlush = false };
    }

    private string ResolveFolder()
    {
        var folder = _optsMon.CurrentValue.Folder;
        return string.IsNullOrWhiteSpace(folder)
            ? Path.Combine(AppContext.BaseDirectory, "Logs")
            : folder;
    }

    // ── Housekeeping ──────────────────────────────────────────────────────────

    private async Task HouseKeepingLoop()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(6), _cts.Token);
                HouseKeeping();
            }
            catch (OperationCanceledException) { break; }
            catch { /* best effort */ }
        }
    }

    private void HouseKeeping()
    {
        var opts       = _optsMon.CurrentValue;
        var folder     = ResolveFolder();
        var now        = DateTime.UtcNow;
        var compress   = now.AddDays(-opts.DaysToCompress);
        var deleteDate = now.AddDays(-opts.DaysToKeep);

        foreach (var f in Directory.GetFiles(folder, "trion-*.log"))
        {
            var fi = new FileInfo(f);
            if (fi.LastWriteTimeUtc < deleteDate)
            {
                fi.Delete();
            }
            else if (fi.LastWriteTimeUtc < compress)
            {
                var gz = f + ".gz";
                if (File.Exists(gz)) continue;
                try
                {
                    // Compress
                    using (var src  = fi.OpenRead())
                    using (var dst  = File.Create(gz))
                    using (var gzip = new GZipStream(dst, CompressionLevel.Optimal))
                        src.CopyTo(gzip);

                    // Verify compressed file is readable before removing original
                    using (var verify    = File.OpenRead(gz))
                    using (var gzipRead  = new GZipStream(verify, CompressionMode.Decompress))
                    {
                        if (gzipRead.ReadByte() == -1)
                            throw new InvalidDataException("Compressed file is empty.");
                    }

                    fi.Delete();
                }
                catch
                {
                    // Keep original — compressed file may be corrupt
                    try { File.Delete(gz); } catch { /* ignore */ }
                }
            }
        }
    }

    // ── Formatting ────────────────────────────────────────────────────────────

    private static string Format(LogEntry e)
        => $"{e.Timestamp:O}|{e.Level,-7}|{e.Category,-20}|{e.Message}";

    private static void WriteConsole(string text, LogLevel lvl)
    {
        var color = lvl switch
        {
            LogLevel.Info    => ConsoleColor.Cyan,
            LogLevel.Success => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error   => ConsoleColor.Red,
            _                => ConsoleColor.Gray
        };
        lock (Console.Out)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prev;
        }
    }

    private static LogLevel MapLevel(MsLogLevel level) => level switch
    {
        MsLogLevel.Trace       => LogLevel.Info,
        MsLogLevel.Debug       => LogLevel.Info,
        MsLogLevel.Information => LogLevel.Info,
        MsLogLevel.Warning     => LogLevel.Warning,
        MsLogLevel.Error       => LogLevel.Error,
        MsLogLevel.Critical    => LogLevel.Error,
        _                      => LogLevel.Info
    };

    // ── Disposal ──────────────────────────────────────────────────────────────

    public async ValueTask DisposeAsync()
    {
        _queue.CompleteAdding();
        await _cts.CancelAsync();
        _writerThread.Join(TimeSpan.FromSeconds(5));
        _cts.Dispose();
        _queue.Dispose();
        _fileGate.Wait();
        _todayWriter?.Dispose();
        _fileGate.Dispose();
    }

    void IDisposable.Dispose() => DisposeAsync().AsTask().GetAwaiter().GetResult();
}

// ── Per-category ILogger wrapper ─────────────────────────────────────────────

internal sealed class TrionLoggerCategory : ILogger
{
    private readonly TrionLogger _parent;
    private readonly string      _category;

    internal TrionLoggerCategory(TrionLogger parent, string category)
    {
        _parent   = parent;
        _category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(MsLogLevel logLevel)
        => logLevel is not MsLogLevel.None;

    public void Log<TState>(
        MsLogLevel logLevel,
        EventId    eventId,
        TState     state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        _parent.Enqueue(logLevel, _category, formatter(state, exception), exception);
    }
}
