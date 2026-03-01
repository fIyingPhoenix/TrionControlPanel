namespace Trion.Core.Abstractions.Services;

public sealed record LaunchRequest(
    string ExecutablePath,
    string[] Arguments,
    string? WorkingDirectory = null);
