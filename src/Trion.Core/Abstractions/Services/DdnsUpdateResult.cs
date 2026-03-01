namespace Trion.Core.Abstractions.Services;

public sealed record DdnsUpdateResult(
    bool Success,
    string? NewIp,
    DateTimeOffset Timestamp,
    string? ErrorMessage = null);
