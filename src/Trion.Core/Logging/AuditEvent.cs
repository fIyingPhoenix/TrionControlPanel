namespace Trion.Core.Logging;

public sealed record AuditEvent(
    DateTimeOffset Timestamp,
    string? Username,
    string IpAddress,
    string Action,
    string Result,
    string? Detail = null);
