using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Trion.Agent.Security;

public sealed partial class CommandAllowlist
{
    // ── Permitted executable file names (case-insensitive on Windows) ─────────
    private static readonly HashSet<string> AllowedFileNames = new(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? StringComparer.OrdinalIgnoreCase
            : StringComparer.Ordinal)
    {
        // Emulator server binaries
        "worldserver",
        "worldserver.exe",
        "authserver",
        "authserver.exe",
        "mangosd",
        "mangosd.exe",
        "realmd",
        "realmd.exe",
        // Client data extraction tools
        "ad",
        "ad.exe",
        "vmap4extractor",
        "vmap4extractor.exe",
        "mmaps_generator",
        "mmaps_generator.exe",
    };

    // ── Permitted system executables with their own argument rules ────────────
    private static readonly Dictionary<string, ArgumentRule> SystemExecutables = new(
        StringComparer.Ordinal)
    {
        ["/usr/bin/systemctl"] = new ArgumentRule(
            AllowedFirstArgs: ["start", "stop", "restart", "status"],
            ServiceNamePattern: ServiceNameRegex()),
        [@"C:\Windows\System32\sc.exe"] = new ArgumentRule(
            AllowedFirstArgs: ["start", "stop", "query"],
            ServiceNamePattern: ServiceNameRegex()),
        // Package managers — only known packages permitted
        ["/usr/bin/apt-get"] = new ArgumentRule(
            AllowedFirstArgs: ["install"],
            ServiceNamePattern: PackageNameRegex()),
        ["/usr/bin/dnf"] = new ArgumentRule(
            AllowedFirstArgs: ["install"],
            ServiceNamePattern: PackageNameRegex()),
    };

    // ── Git executables — URL allowlist enforcement ────────────────────────────
    private static readonly HashSet<string> GitExecutables = new(StringComparer.Ordinal)
    {
        "/usr/bin/git",
        @"C:\Program Files\Git\bin\git.exe",
    };

    private static readonly HashSet<string> AllowedGitRepoUrls =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "https://github.com/TrinityCore/TrinityCore.git",
            "https://github.com/azerothcore/azerothcore-wotlk.git",
            "https://github.com/cmangos/mangos-wotlk.git",
        };

    // ── msiexec — Windows MSI installer (silent installs only) ───────────────
    private static readonly string MsiExecPath =
        @"C:\Windows\System32\msiexec.exe";

    private static readonly HashSet<string> AllowedMsiFlags =
        new(StringComparer.OrdinalIgnoreCase) { "/quiet", "/norestart", "/passive" };

    // ── Shell metacharacters that must never appear in any argument ───────────
    [GeneratedRegex(@"[;&|`$<>!\\*?{}\[\]()'""]")]
    private static partial Regex ShellMetacharRegex();

    [GeneratedRegex(@"^[a-zA-Z0-9_\-]{1,64}$")]
    private static partial Regex ServiceNameRegex();

    // Package names: letters, numbers, dots, plus, hyphens — mirrors Debian/RPM naming
    [GeneratedRegex(@"^[a-zA-Z0-9][a-zA-Z0-9\.\+\-]{0,127}$")]
    private static partial Regex PackageNameRegex();

    // ── Public validation ─────────────────────────────────────────────────────

    public bool IsPermitted(string executablePath, string[] arguments)
    {
        if (string.IsNullOrWhiteSpace(executablePath))
            return false;

        // Must be an absolute path
        if (!Path.IsPathRooted(executablePath))
            return false;

        // Check shell metacharacters in the path itself
        if (ShellMetacharRegex().IsMatch(executablePath))
            return false;

        // Check each argument for metacharacters
        foreach (var arg in arguments)
        {
            if (arg is null || ShellMetacharRegex().IsMatch(arg))
                return false;
        }

        var fileName = Path.GetFileName(executablePath);

        // Emulator executables + extraction tools: check file name, not full path
        // (install directory is user-configured)
        if (AllowedFileNames.Contains(fileName))
            return true;

        // System executables: check full path + argument rules
        if (SystemExecutables.TryGetValue(executablePath, out var rule))
            return ValidateSystemArguments(rule, arguments);

        // Git: restricted to clone/fetch from hardcoded repository URLs
        if (GitExecutables.Contains(executablePath))
            return ValidateGitArguments(arguments);

        // msiexec: only /i <path>.msi with safe flags
        if (executablePath.Equals(MsiExecPath, StringComparison.OrdinalIgnoreCase))
            return ValidateMsiExecArguments(arguments);

        return false;
    }

    private static bool ValidateSystemArguments(ArgumentRule rule, string[] arguments)
    {
        if (arguments.Length < 1)
            return false;

        var action = arguments[0];
        if (!rule.AllowedFirstArgs.Contains(action, StringComparer.Ordinal))
            return false;

        // Find the first non-flag argument (e.g. service name, package name).
        // Flag arguments such as -y / --yes are allowed but not validated by the pattern.
        for (int i = 1; i < arguments.Length; i++)
        {
            if (arguments[i].StartsWith('-'))
                continue;   // skip short/long flags

            if (!rule.ServiceNamePattern.IsMatch(arguments[i]))
                return false;

            break;   // only the first positional arg needs to match the pattern
        }

        return true;
    }

    private static bool ValidateGitArguments(string[] arguments)
    {
        if (arguments.Length < 1) return false;

        var verb = arguments[0];

        switch (verb)
        {
            case "clone":
                // Require exactly: clone <allowed-url> [<target-dir>]
                if (arguments.Length < 2) return false;
                if (!AllowedGitRepoUrls.Contains(arguments[1])) return false;
                // Optional third argument is the target directory — already checked for metacharacters
                return true;

            case "fetch":
                // Allow: fetch (no extra args, or first non-flag arg must be allowed URL)
                if (arguments.Length >= 2 && !arguments[1].StartsWith('-') &&
                    !AllowedGitRepoUrls.Contains(arguments[1]))
                    return false;
                return true;

            default:
                return false;
        }
    }

    private static bool ValidateMsiExecArguments(string[] arguments)
    {
        // Expected pattern: /i <installer.msi> [/quiet] [/norestart] [/passive]
        if (arguments.Length < 2) return false;
        if (!arguments[0].Equals("/i", StringComparison.OrdinalIgnoreCase)) return false;

        var msiPath = arguments[1];
        if (!msiPath.EndsWith(".msi", StringComparison.OrdinalIgnoreCase)) return false;
        if (!Path.IsPathRooted(msiPath)) return false;

        // Remaining arguments must all be known safe flags
        for (int i = 2; i < arguments.Length; i++)
        {
            if (!AllowedMsiFlags.Contains(arguments[i]))
                return false;
        }

        return true;
    }

    private sealed record ArgumentRule(
        string[] AllowedFirstArgs,
        Regex ServiceNamePattern);
}
