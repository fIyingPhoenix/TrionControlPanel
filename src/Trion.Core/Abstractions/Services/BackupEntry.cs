namespace Trion.Core.Abstractions.Services;

public sealed record BackupEntry(
    string Id,
    string FileName,
    long SizeBytes,
    DateTimeOffset CreatedAt,
    string StorageKey);
