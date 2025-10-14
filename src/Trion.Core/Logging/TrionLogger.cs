using System.Collections.Concurrent;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;


namespace Trion.Core.Logging;

public sealed class TrionLogger : IAsyncDisposable
{
    private readonly LoggerOptions _opts;
    private readonly BlockingCollection<LogEntry> _queue = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly Task _writerTask;
    private readonly SemaphoreSlim _fileGate = new(1, 1);

    private StreamWriter? _todayWriter;
    private DateTime _todayDate;

    public TrionLogger(LoggerOptions? opts = null)
    {
        _opts = opts ?? new LoggerOptions();
        Directory.CreateDirectory(_opts.Folder);
        OpenTodayFile();
        _writerTask = Task.Run(WritePump);
        _ = Task.Run(HouseKeepingLoop);
    }

    /* ------------- public API ------------- */
    public void Info(string message, string category = "",
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Info, message, category, member, file, line);

    public void Success(string message, string category = "",
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Success, message, category, member, file, line);

    public void Warning(string message, string category = "",
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Warning, message, category, member, file, line);

    public void Error(string message, string category = "",
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Error, message, category, member, file, line);

    public void Error(Exception ex, string category = "",
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Error(ex.ToString(), category, member, file, line);

    /* ------------- private ------------- */
    private void Write(LogLevel lvl, string msg, string cat,
                       string member, string file, int line)
    {
        bool allowed = lvl switch
        {
            LogLevel.Info => _opts.ShowInfo,
            LogLevel.Success => _opts.ShowSuccess,
            LogLevel.Warning => _opts.ShowWarning,
            LogLevel.Error => _opts.ShowError,
            _ => false
        };

        if (!allowed) return;

        var entry = new LogEntry(
            Timestamp: DateTimeOffset.UtcNow,
            Level: lvl,
            Message: msg,
            Category: cat,
            Member: member,
            FilePath: Path.GetFileName(file),
            Line: line);

        _queue.Add(entry);
    }

    private async Task WritePump()
    {
        await Task.Run(() =>
        {
            foreach (var entry in _queue.GetConsumingEnumerable(_cts.Token))
            {
                var line = Format(entry);
                if (_opts.WriteToConsole) WriteConsole(line, entry.Level);
                if (_opts.WriteToFile) WriteFileAsync(line).GetAwaiter().GetResult();
            }
        }, _cts.Token);
    }

    private static string Format(LogEntry e) =>
        $"{e.Timestamp:O}|{e.Level,-7}|{e.Category,-15}|{e.Member}({e.FilePath}:{e.Line})|{e.Message}";

    private static void WriteConsole(string text, LogLevel lvl)
    {
        var color = lvl switch
        {
            LogLevel.Info => ConsoleColor.Cyan,
            LogLevel.Success => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.Gray
        };
        lock (Console.Out)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prev;
        }
    }

    private async ValueTask WriteFileAsync(string line)
    {
        await _fileGate.WaitAsync();
        try
        {
            var now = DateTime.UtcNow;
            if (now.Date != _todayDate)
            {
                _todayWriter?.Dispose();
                OpenTodayFile();
            }
            await _todayWriter!.WriteLineAsync(line);
            await _todayWriter.FlushAsync();
        }
        finally { _fileGate.Release(); }
    }

    private void OpenTodayFile()
    {
        _todayDate = DateTime.UtcNow.Date;
        var path = Path.Combine(_opts.Folder, $"trion-{_todayDate:yyyyMMdd}.log");
        _todayWriter = new StreamWriter(path, append: true, Encoding.UTF8) { AutoFlush = false };
    }

    private async Task HouseKeepingLoop()
    {
        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(6), _cts.Token);
                HouseKeeping();
            }
            catch { /* best effort */ }
        }
    }

    private void HouseKeeping()
    {
        var now = DateTime.UtcNow;
        var compressDate = now.AddDays(-_opts.DaysToCompress);
        var deleteDate = now.AddDays(-_opts.DaysToKeep);

        foreach (var f in Directory.GetFiles(_opts.Folder, "trion-*.log"))
        {
            var fi = new FileInfo(f);
            if (fi.LastWriteTimeUtc < deleteDate)
            {
                fi.Delete();
            }
            else if (fi.LastWriteTimeUtc < compressDate &&
                     !f.EndsWith(".gz", StringComparison.OrdinalIgnoreCase))
            {
                var gz = f + ".gz";
                if (!File.Exists(gz))
                {
                    using var original = fi.OpenRead();
                    using var compressed = File.Create(gz);
                    using var gzip = new GZipStream(compressed, CompressionMode.Compress);
                    original.CopyTo(gzip);
                }
                fi.Delete();
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        _queue.CompleteAdding();
        _cts.Cancel();
        await _writerTask;
        _cts.Dispose();
        _queue.Dispose();
        await _fileGate.WaitAsync();
        _todayWriter?.Dispose();
        _fileGate.Dispose();
    }
}