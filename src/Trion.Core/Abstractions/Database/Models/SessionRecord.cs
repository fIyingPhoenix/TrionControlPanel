namespace Trion.Core.Abstractions.Database.Models;

public sealed record SessionRecord(
    string Username,
    string IpAddress,
    DateTimeOffset LoginTime,
    int? ActiveCharacterId = null);
