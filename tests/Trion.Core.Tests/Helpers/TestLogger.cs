using Microsoft.Extensions.Options;
using Trion.Core.Logging;

namespace Trion.Core.Tests;

/// <summary>
/// Provides a single shared <see cref="TrionLogger"/> instance for use in tests.
/// All output is suppressed (no console, no file, all levels disabled).
/// </summary>
internal static class TestLogger
{
    public static readonly TrionLogger Instance = CreateSilent();

    private static TrionLogger CreateSilent()
    {
        var opts = new LoggerOptions
        {
            Folder         = Path.Combine(Path.GetTempPath(), "trion_test_logs"),
            WriteToConsole = false,
            WriteToFile    = false,
            ShowInfo       = false,
            ShowSuccess    = false,
            ShowWarning    = false,
            ShowError      = false,
        };
        return new TrionLogger(new StaticOptionsMonitor(opts));
    }

    private sealed class StaticOptionsMonitor : IOptionsMonitor<LoggerOptions>
    {
        private readonly LoggerOptions _value;
        public StaticOptionsMonitor(LoggerOptions value) => _value = value;
        public LoggerOptions CurrentValue => _value;
        public LoggerOptions Get(string? name) => _value;
        public IDisposable? OnChange(Action<LoggerOptions, string?> listener) => null;
    }
}
