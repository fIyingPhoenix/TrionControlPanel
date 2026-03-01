namespace Trion.Core.Abstractions.Services;

public sealed record LaunchResult(
    bool Success,
    int? Pid,
    DateTimeOffset? StartTime,
    string? ErrorMessage)
{
    public static LaunchResult Ok(int pid, DateTimeOffset startTime) =>
        new(true, pid, startTime, null);

    public static LaunchResult Fail(string error) =>
        new(false, null, null, error);
}
