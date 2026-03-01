using System.Diagnostics;
using Trion.Agent.Handlers;
using Trion.Core.Agent;
using Xunit;

namespace Trion.Agent.Tests.Handlers;

public sealed class ProcessKillHandlerTests
{
    private readonly ProcessKillHandler _sut =
        new(TestLogger.Instance);

    // ── Kill: PID not found ───────────────────────────────────────────────────

    [Fact]
    public async Task Kill_InvalidPid_ReturnsFailure()
    {
        // PID 0 and negative PIDs are guaranteed not to exist as user processes
        var cmd  = new KillProcessCommand(Pid: -1, ExpectedStartTime: DateTimeOffset.UtcNow);
        var resp = await _sut.HandleAsync(cmd);

        Assert.False(resp.Success);
        Assert.NotNull(resp.ErrorMessage);
    }

    // ── Kill: wrong start time ────────────────────────────────────────────────

    [Fact]
    public async Task Kill_WrongStartTime_ReturnsFailure()
    {
        // Use the current process — it definitely exists, but we supply a bogus start time
        var self = Process.GetCurrentProcess();
        var cmd  = new KillProcessCommand(
            Pid:               self.Id,
            ExpectedStartTime: DateTimeOffset.UnixEpoch); // clearly wrong

        var resp = await _sut.HandleAsync(cmd);

        Assert.False(resp.Success);
        Assert.Contains("mismatch", resp.ErrorMessage ?? "", StringComparison.OrdinalIgnoreCase);
    }

    // ── IsAlive: invalid PID ──────────────────────────────────────────────────

    [Fact]
    public async Task IsAlive_InvalidPid_ReturnsFalse()
    {
        var cmd  = new IsProcessAliveCommand(Pid: -1, ExpectedStartTime: DateTimeOffset.UtcNow);
        var resp = await _sut.IsAliveAsync(cmd);

        Assert.False(resp.Alive);
    }

    // ── IsAlive: current process, correct start time ──────────────────────────

    [Fact]
    public async Task IsAlive_CurrentProcess_CorrectStartTime_ReturnsTrue()
    {
        var self      = Process.GetCurrentProcess();
        var startTime = new DateTimeOffset(self.StartTime.ToUniversalTime(), TimeSpan.Zero);

        var cmd  = new IsProcessAliveCommand(self.Id, startTime);
        var resp = await _sut.IsAliveAsync(cmd);

        Assert.True(resp.Alive);
    }

    // ── IsAlive: current process, wrong start time ────────────────────────────

    [Fact]
    public async Task IsAlive_CurrentProcess_WrongStartTime_ReturnsFalse()
    {
        var self = Process.GetCurrentProcess();
        var cmd  = new IsProcessAliveCommand(self.Id, DateTimeOffset.UnixEpoch);
        var resp = await _sut.IsAliveAsync(cmd);

        Assert.False(resp.Alive);
    }
}
