namespace Trion.Core.Abstractions.Services;

public sealed record UpdateCheckResult(
    bool UpdateAvailable,
    string? NewVersion,
    string? ReleaseNotes,
    string? ManifestUrl,
    string? MinimumVersion);
