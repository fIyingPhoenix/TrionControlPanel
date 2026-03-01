namespace Trion.Core.Abstractions.Database.Models;

public sealed record BanRecord(
    string BannedUsername,
    string BannedBy,
    string BanReason,
    DateTimeOffset BannedAt,
    DateTimeOffset ExpiresAt,
    bool IsPermanent = false);
