using Trion.Core.Abstractions.Services;
using Trion.Core.Tests;
using Trion.Core.Agent;
using Trion.Core.Services.Orchestration;

namespace Trion.Core.Tests.Orchestration;

public sealed class EmulatorOrchestratorTests
{
    private static EmulatorProfile MakeProfile(string id = "test") =>
        new(id, "Test Server", EmulatorType.TrinityCore,
            "/opt/trinitycore/worldserver", "/opt/trinitycore",
            AutoRestart: true, MaxRestartAttempts: 3);

    // ── Start ──────────────────────────────────────────────────────────────────

    [Fact]
    public async Task StartAsync_SuccessfulLaunch_StateIsRunning()
    {
        var client = new FakeAgentClient { LaunchSucceeds = true };
        var sut    = new EmulatorOrchestrator(client, TestLogger.Instance);

        await sut.StartAsync(MakeProfile());

        var status = await sut.GetStatusAsync("test");
        Assert.Equal(ProcessState.Running, status.State);
        Assert.NotNull(status.Pid);
        Assert.Equal(0, status.RestartCount);
    }

    [Fact]
    public async Task StartAsync_LaunchFails_StateIsCrashed()
    {
        var client = new FakeAgentClient { LaunchSucceeds = false };
        var sut    = new EmulatorOrchestrator(client, TestLogger.Instance);

        await sut.StartAsync(MakeProfile());

        var status = await sut.GetStatusAsync("test");
        Assert.Equal(ProcessState.Crashed, status.State);
        Assert.Null(status.Pid);
        Assert.NotNull(status.LastError);
    }

    [Fact]
    public async Task StartAsync_AlreadyRunning_SecondCallIsNoOp()
    {
        var client = new FakeAgentClient { LaunchSucceeds = true };
        var sut    = new EmulatorOrchestrator(client, TestLogger.Instance);

        await sut.StartAsync(MakeProfile());
        await sut.StartAsync(MakeProfile());   // should be ignored

        // LaunchProcessAsync should have been called only once
        Assert.Equal(1, client.LaunchCallCount);
    }

    // ── Stop ───────────────────────────────────────────────────────────────────

    [Fact]
    public async Task StopAsync_WhileRunning_StateIsStopped()
    {
        var client = new FakeAgentClient { LaunchSucceeds = true, KillSucceeds = true };
        var sut    = new EmulatorOrchestrator(client, TestLogger.Instance);

        await sut.StartAsync(MakeProfile());
        await sut.StopAsync("test");

        var status = await sut.GetStatusAsync("test");
        Assert.Equal(ProcessState.Stopped, status.State);
        Assert.Null(status.Pid);
    }

    [Fact]
    public async Task StopAsync_UnknownProfile_DoesNotThrow()
    {
        var sut = new EmulatorOrchestrator(
            new FakeAgentClient(), TestLogger.Instance);

        // Should not throw
        await sut.StopAsync("nonexistent");
    }

    // ── GetStatus ──────────────────────────────────────────────────────────────

    [Fact]
    public async Task GetStatusAsync_UnknownProfile_ReturnsStopped()
    {
        var sut    = new EmulatorOrchestrator(
            new FakeAgentClient(), TestLogger.Instance);
        var status = await sut.GetStatusAsync("ghost");

        Assert.Equal(ProcessState.Stopped, status.State);
        Assert.Equal("ghost", status.ProfileId);
    }

    // ── GetAllStatuses ─────────────────────────────────────────────────────────

    [Fact]
    public void GetAllStatuses_EmptyOrchestrator_ReturnsEmptyList()
    {
        var sut = new EmulatorOrchestrator(
            new FakeAgentClient(), TestLogger.Instance);

        Assert.Empty(sut.GetAllStatuses());
    }

    [Fact]
    public async Task GetAllStatuses_AfterStart_ContainsProfile()
    {
        var sut = new EmulatorOrchestrator(
            new FakeAgentClient { LaunchSucceeds = true },
            TestLogger.Instance);

        await sut.StartAsync(MakeProfile("alpha"));
        await sut.StartAsync(MakeProfile("beta"));

        var statuses = sut.GetAllStatuses();
        Assert.Equal(2, statuses.Count);
        Assert.Contains(statuses, s => s.ProfileId == "alpha");
        Assert.Contains(statuses, s => s.ProfileId == "beta");
    }

    // ── Minimal fake ──────────────────────────────────────────────────────────

    private sealed class FakeAgentClient : IAgentClient
    {
        public bool LaunchSucceeds { get; set; } = true;
        public bool KillSucceeds   { get; set; } = true;
        public bool AliveResult    { get; set; } = true;
        public int  LaunchCallCount { get; private set; }

        public Task<LaunchResult> LaunchProcessAsync(
            LaunchRequest request, string? pnToken = null, CancellationToken ct = default)
        {
            LaunchCallCount++;
            return Task.FromResult(
                LaunchSucceeds
                    ? LaunchResult.Ok(12345, DateTimeOffset.UtcNow)
                    : LaunchResult.Fail("launch failed"));
        }

        public Task<bool> KillProcessAsync(
            int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default) =>
            Task.FromResult(KillSucceeds);

        public Task<bool> IsProcessAliveAsync(
            int pid, DateTimeOffset expectedStartTime, CancellationToken ct = default) =>
            Task.FromResult(AliveResult);

        public Task<ServiceStatus> GetServiceStatusAsync(
            string serviceName, CancellationToken ct = default) =>
            Task.FromResult(new ServiceStatus(serviceName, ServiceState.Unknown));

        public Task<bool> StartServiceAsync(
            string serviceName, CancellationToken ct = default) => Task.FromResult(true);

        public Task<bool> StopServiceAsync(
            string serviceName, CancellationToken ct = default) => Task.FromResult(true);

        public Task<bool> RestartServiceAsync(
            string serviceName, CancellationToken ct = default) => Task.FromResult(true);

        public Task<bool> PingAsync(CancellationToken ct = default) =>
            Task.FromResult(true);
    }
}
