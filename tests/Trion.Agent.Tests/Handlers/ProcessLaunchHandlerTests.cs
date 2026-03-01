using Microsoft.Extensions.Logging.Abstractions;
using Trion.Agent.Handlers;
using Trion.Agent.Security;
using Trion.Core.Agent;
using Xunit;

namespace Trion.Agent.Tests.Handlers;

public sealed class ProcessLaunchHandlerTests
{
    private readonly ProcessLaunchHandler _sut =
        new(new CommandAllowlist(), NullLogger<ProcessLaunchHandler>.Instance);

    // ── Allowlist enforcement ──────────────────────────────────────────────────

    [Fact]
    public async Task NonAllowlistedPath_ReturnsFailure()
    {
        var cmd = new LaunchProcessCommand("/usr/bin/bash", [], null);
        var resp = await _sut.HandleAsync(cmd);

        Assert.False(resp.Success);
        Assert.Null(resp.Pid);
        Assert.NotNull(resp.ErrorMessage);
    }

    [Fact]
    public async Task RelativePath_ReturnsFailure()
    {
        var cmd = new LaunchProcessCommand("worldserver", [], null);
        var resp = await _sut.HandleAsync(cmd);

        Assert.False(resp.Success);
        Assert.Null(resp.Pid);
    }

    [Theory]
    [InlineData("/opt/trinitycore/worldserver", "; rm -rf /")]
    [InlineData("/opt/trinitycore/worldserver", "$(evil)")]
    [InlineData("/opt/trinitycore/worldserver", "arg | cat")]
    public async Task MetacharactersInArgs_ReturnsFailure(string path, string arg)
    {
        var cmd = new LaunchProcessCommand(path, [arg], null);
        var resp = await _sut.HandleAsync(cmd);

        Assert.False(resp.Success);
    }

    // ── Allowlisted path with non-existent binary ─────────────────────────────
    // The allowlist check passes; the OS then rejects the missing file.

    [Fact]
    public async Task AllowlistedPath_NonExistentBinary_ReturnsOsError()
    {
        // /opt/trinitycore/worldserver is in the allowlist but won't exist on the test host
        var cmd = new LaunchProcessCommand("/opt/trinitycore/worldserver", [], null);
        var resp = await _sut.HandleAsync(cmd);

        // Must NOT return "not permitted" — the allowlist passed
        Assert.DoesNotContain("permitted", resp.ErrorMessage ?? "", StringComparison.OrdinalIgnoreCase);
        // Must fail because the binary doesn't exist
        Assert.False(resp.Success);
    }
}
