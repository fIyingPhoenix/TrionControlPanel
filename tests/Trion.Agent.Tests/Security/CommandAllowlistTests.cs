using Trion.Agent.Security;
using Xunit;

namespace Trion.Agent.Tests.Security;

public sealed class CommandAllowlistTests
{
    private readonly CommandAllowlist _sut = new();

    // ── Emulator executables ──────────────────────────────────────────────────

    [Theory]
    [InlineData("/opt/trinitycore/worldserver",     new string[0])]
    [InlineData("/opt/trinitycore/authserver",      new string[0])]
    [InlineData("/opt/mangos/mangosd",              new string[0])]
    [InlineData("/opt/mangos/realmd",               new string[0])]
    [InlineData(@"C:\TC\worldserver.exe",           new string[0])]
    [InlineData(@"C:\TC\authserver.exe",            new string[0])]
    public void KnownEmulatorExecutable_IsPermitted(string path, string[] args)
        => Assert.True(_sut.IsPermitted(path, args));

    [Fact]
    public void UnknownExecutable_IsRejected()
        => Assert.False(_sut.IsPermitted("/usr/bin/bash", []));

    [Fact]
    public void RelativePath_IsRejected()
        => Assert.False(_sut.IsPermitted("worldserver", []));

    // ── Shell metacharacters ──────────────────────────────────────────────────

    [Theory]
    [InlineData("/opt/trinitycore/worldserver", new[] { "; rm -rf /" })]
    [InlineData("/opt/trinitycore/worldserver", new[] { "$(evil)" })]
    [InlineData("/opt/trinitycore/worldserver", new[] { "arg | cat /etc/passwd" })]
    [InlineData("/opt/trinitycore/worldserver", new[] { "arg & whoami" })]
    public void MetacharactersInArguments_AreRejected(string path, string[] args)
        => Assert.False(_sut.IsPermitted(path, args));

    // ── systemctl (Linux) ────────────────────────────────────────────────────
    // [InlineData] cannot pass a string[] as the sole argument (CS0182);
    // use [MemberData] with TheoryData<string[]> instead.

    public static TheoryData<string[]> ValidSystemctlArgs => new()
    {
        { new[] { "start",   "worldserver" } },
        { new[] { "stop",    "worldserver" } },
        { new[] { "restart", "trion-web"   } },
        { new[] { "status",  "mysql"       } },
    };

    [Theory]
    [MemberData(nameof(ValidSystemctlArgs))]
    public void SystemctlWithValidArgs_IsPermitted(string[] args)
        => Assert.True(_sut.IsPermitted("/usr/bin/systemctl", args));

    public static TheoryData<string[]> InvalidSystemctlArgs => new()
    {
        { new[] { "enable",  "worldserver"      } },  // not in allowlist
        { new[] { "start",   "../../etc/passwd" } },  // path traversal in service name
        { new[] { "start",   "name with spaces" } },  // fails service name regex
    };

    [Theory]
    [MemberData(nameof(InvalidSystemctlArgs))]
    public void SystemctlWithInvalidArgs_IsRejected(string[] args)
        => Assert.False(_sut.IsPermitted("/usr/bin/systemctl", args));

    // ── Client data extraction tools ──────────────────────────────────────────

    [Theory]
    [InlineData("/opt/trinitycore/tools/ad",                new string[0])]
    [InlineData("/opt/trinitycore/tools/vmap4extractor",    new string[0])]
    [InlineData("/opt/trinitycore/tools/mmaps_generator",   new string[0])]
    [InlineData(@"C:\TC\tools\ad.exe",                      new string[0])]
    [InlineData(@"C:\TC\tools\vmap4extractor.exe",          new string[0])]
    [InlineData(@"C:\TC\tools\mmaps_generator.exe",         new string[0])]
    public void ExtractionTools_ArePermitted(string path, string[] args)
        => Assert.True(_sut.IsPermitted(path, args));

    // ── git clone / fetch (URL allowlist) ────────────────────────────────────

    public static TheoryData<string[]> ValidGitArgs => new()
    {
        { new[] { "clone", "https://github.com/TrinityCore/TrinityCore.git",        "/opt/tc" } },
        { new[] { "clone", "https://github.com/azerothcore/azerothcore-wotlk.git",  "/opt/ac" } },
        { new[] { "clone", "https://github.com/cmangos/mangos-wotlk.git",           "/opt/cm" } },
        { new[] { "fetch", "https://github.com/TrinityCore/TrinityCore.git" } },
        { new[] { "fetch" } },  // bare fetch (no remote specified)
    };

    [Theory]
    [MemberData(nameof(ValidGitArgs))]
    public void GitWithAllowedUrl_IsPermitted(string[] args)
        => Assert.True(_sut.IsPermitted("/usr/bin/git", args));

    public static TheoryData<string[]> InvalidGitArgs => new()
    {
        { new[] { "clone", "https://evil.example.com/malware.git" } },   // unknown URL
        { new[] { "push",  "https://github.com/TrinityCore/TrinityCore.git" } }, // push forbidden
        { new[] { "clone" } },                                             // missing URL
    };

    [Theory]
    [MemberData(nameof(InvalidGitArgs))]
    public void GitWithDisallowedArgs_IsRejected(string[] args)
        => Assert.False(_sut.IsPermitted("/usr/bin/git", args));

    [Fact]
    public void UnknownGitExecutable_IsRejected()
        => Assert.False(_sut.IsPermitted("/usr/bin/git-wrapper",
            ["clone", "https://github.com/TrinityCore/TrinityCore.git"]));

    // ── apt-get / dnf package installation ───────────────────────────────────

    [Theory]
    [InlineData("/usr/bin/apt-get", new[] { "install", "-y", "mysql-server" })]
    [InlineData("/usr/bin/apt-get", new[] { "install", "nginx" })]
    [InlineData("/usr/bin/dnf",     new[] { "install", "-y", "mysql-server" })]
    [InlineData("/usr/bin/dnf",     new[] { "install", "nginx" })]
    public void PackageManager_ValidInstall_IsPermitted(string path, string[] args)
        => Assert.True(_sut.IsPermitted(path, args));

    [Theory]
    [InlineData("/usr/bin/apt-get", new[] { "remove",  "mysql-server" })]  // remove not allowed
    [InlineData("/usr/bin/apt-get", new[] { "install"                 })]  // no package
    public void PackageManager_InvalidCommand_IsRejected(string path, string[] args)
        => Assert.False(_sut.IsPermitted(path, args));

    // ── msiexec (Windows silent install) ─────────────────────────────────────
    // [InlineData] cannot pass a string[] as the sole argument (CS0182);
    // use [MemberData] with TheoryData<string[]> instead.

    public static TheoryData<string[]> ValidMsiExecArgs => new()
    {
        { new[] { "/i", @"C:\Temp\mysql.msi", "/quiet", "/norestart" } },
        { new[] { "/i", @"C:\Temp\mysql.msi", "/passive" } },
        { new[] { "/i", @"C:\Temp\mysql.msi" } },
    };

    [Theory]
    [MemberData(nameof(ValidMsiExecArgs))]
    public void MsiExec_ValidInstall_IsPermitted(string[] args)
        => Assert.True(_sut.IsPermitted(@"C:\Windows\System32\msiexec.exe", args));

    public static TheoryData<string[]> InvalidMsiExecArgs => new()
    {
        { new[] { "/x", @"C:\Temp\mysql.msi" } },              // /x = uninstall
        { new[] { "/i", @"C:\Temp\mysql.txt" } },              // not an MSI
        { new[] { "/i", "mysql.msi" } },                        // relative path
        { new[] { "/i", @"C:\Temp\mysql.msi", "/force" } },   // unknown flag
    };

    [Theory]
    [MemberData(nameof(InvalidMsiExecArgs))]
    public void MsiExec_InvalidArgs_IsRejected(string[] args)
        => Assert.False(_sut.IsPermitted(@"C:\Windows\System32\msiexec.exe", args));

    // ── Path traversal in executable path ────────────────────────────────────

    [Fact]
    public void PathTraversal_IsRejected()
        => Assert.False(_sut.IsPermitted("/opt/trion/../../etc/worldserver", []));
}
