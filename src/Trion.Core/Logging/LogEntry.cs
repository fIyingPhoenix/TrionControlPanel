namespace Trion.Core.Logging;

internal readonly record struct LogEntry(
    DateTimeOffset Timestamp,
    LogLevel Level,
    string Message,
    string Category,
    string Member,
    string FilePath,
    int Line);