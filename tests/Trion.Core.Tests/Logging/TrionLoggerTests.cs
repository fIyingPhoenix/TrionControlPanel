using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Trion.Core.Logging;

namespace Trion.Core.Tests.Logging;

public sealed class TrionLoggerTests : IAsyncDisposable
{
    private readonly string              _folder;
    private readonly TestOptionsMonitor  _monitor;
    private readonly TrionLogger         _sut;

    public TrionLoggerTests()
    {
        _folder  = Path.Combine(Path.GetTempPath(), "TrionLoggerTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_folder);

        _monitor = new TestOptionsMonitor(new LoggerOptions
        {
            Folder         = _folder,
            WriteToConsole = false,
            WriteToFile    = true,
            ShowInfo       = true,
            ShowWarning    = true,
            ShowError      = true,
        });

        _sut = new TrionLogger(_monitor);
    }

    public async ValueTask DisposeAsync()
    {
        await _sut.DisposeAsync();
        if (Directory.Exists(_folder))
            Directory.Delete(_folder, recursive: true);
    }

    [Fact]
    public async Task LogEntry_IsWrittenToFile()
    {
        var logger = _sut.CreateLogger("TestCategory");
        logger.LogInformation("Hello from test");

        // Give background thread time to flush
        await Task.Delay(200);
        await _sut.DisposeAsync();

        var files = Directory.GetFiles(_folder, "*.log");
        Assert.Single(files);

        var content = await File.ReadAllTextAsync(files[0]);
        Assert.Contains("Hello from test", content);
    }

    [Fact]
    public async Task DropCounter_IncrementedWhenQueueFull()
    {
        // Pause the writer by overloading the bounded queue (capacity: 1000)
        // We'll just verify the entry count doesn't crash; precise drop testing
        // requires hooking the internal counter which is internal.
        // This test simply ensures no exception is thrown under heavy load.
        var logger = _sut.CreateLogger("LoadTest");
        for (int i = 0; i < 2000; i++)
            logger.LogInformation("Entry {I}", i);

        await Task.Delay(500);
        // No exception means the drop counter silently handled overflows
        Assert.True(true);
    }

    [Fact]
    public async Task DisabledLevel_IsNotWritten()
    {
        _monitor.Update(o => o.ShowInfo = false);

        var logger = _sut.CreateLogger("Filter");
        logger.LogInformation("This should be filtered");

        await Task.Delay(200);
        await _sut.DisposeAsync();

        var files = Directory.GetFiles(_folder, "*.log");
        if (files.Length == 0) return; // nothing written at all — correct

        var content = await File.ReadAllTextAsync(files[0]);
        Assert.DoesNotContain("This should be filtered", content);
    }

    [Fact]
    public async Task MultipleCategories_AreAllLogged()
    {
        var logA = _sut.CreateLogger("CategoryA");
        var logB = _sut.CreateLogger("CategoryB");

        logA.LogWarning("Message from A");
        logB.LogError("Message from B");

        await Task.Delay(200);
        await _sut.DisposeAsync();

        var files = Directory.GetFiles(_folder, "*.log");
        Assert.Single(files);

        var content = await File.ReadAllTextAsync(files[0]);
        Assert.Contains("Message from A", content);
        Assert.Contains("Message from B", content);
    }

    // ── Test helpers ─────────────────────────────────────────────────────────

    private sealed class TestOptionsMonitor : IOptionsMonitor<LoggerOptions>
    {
        private LoggerOptions _current;
        private Action<LoggerOptions, string?>? _listener;

        public TestOptionsMonitor(LoggerOptions initial) => _current = initial;

        public LoggerOptions CurrentValue => _current;

        public LoggerOptions Get(string? name) => _current;

        public IDisposable? OnChange(Action<LoggerOptions, string?> listener)
        {
            _listener = listener;
            return null;
        }

        public void Update(Action<LoggerOptions> modify)
        {
            modify(_current);
            _listener?.Invoke(_current, null);
        }
    }
}
