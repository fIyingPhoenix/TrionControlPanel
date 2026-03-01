using Microsoft.Extensions.Logging.Abstractions;
using Trion.Agent.Handlers;
using Trion.Agent.Security;
using Trion.Core.Agent;
using Xunit;

namespace Trion.Agent.Tests.Handlers;

public sealed class ServiceControlHandlerTests
{
    // ServiceControlHandler delegates Linux ops to ProcessLaunchHandler.
    // These tests validate the routing/validation logic without actually
    // calling systemctl (non-existent on most CI runners).

    private static ServiceControlHandler CreateSut() =>
        new(
            new ProcessLaunchHandler(
                new CommandAllowlist(),
                NullLogger<ProcessLaunchHandler>.Instance),
            NullLogger<ServiceControlHandler>.Instance);

    // ── Action routing ────────────────────────────────────────────────────────

    [Theory]
    [InlineData("start")]
    [InlineData("stop")]
    [InlineData("restart")]
    [InlineData("status")]
    public async Task KnownAction_DoesNotReturnUnknownActionError(string action)
    {
        var sut  = CreateSut();
        var cmd  = new ServiceControlCommand("worldserver", action);
        var resp = await sut.HandleAsync(cmd);

        // Should not produce an "Unknown action" error — routing worked
        Assert.DoesNotContain("Unknown action", resp.ErrorMessage ?? "",
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task UnknownAction_ReturnsError()
    {
        var sut  = CreateSut();
        var cmd  = new ServiceControlCommand("worldserver", "enable");
        var resp = await sut.HandleAsync(cmd);

        Assert.False(resp.Success);
        Assert.Contains("Unknown action", resp.ErrorMessage ?? "",
            StringComparison.OrdinalIgnoreCase);
    }

    // ── Service name passed through to systemctl args ─────────────────────────
    // On Linux, systemctl is at /usr/bin/systemctl which is in the allowlist.
    // The handler will fail with an OS error (binary not found or access denied
    // in the test environment) but NOT with a "not permitted" allowlist error,
    // proving the service-name was forwarded correctly.

    [Fact]
    public async Task ValidServiceName_AllowlistPassesForSystemctl()
    {
        var sut  = CreateSut();
        var cmd  = new ServiceControlCommand("mysql", "start");
        var resp = await sut.HandleAsync(cmd);

        // The error (if any) should NOT be an allowlist rejection
        Assert.DoesNotContain("not permitted", resp.ErrorMessage ?? "",
            StringComparison.OrdinalIgnoreCase);
    }
}
