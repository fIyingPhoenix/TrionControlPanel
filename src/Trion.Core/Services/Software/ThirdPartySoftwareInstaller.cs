using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Services;
using Trion.Core.Agent;
using Trion.Core.Logging;
using Trion.Core.Services.Emulator;

namespace Trion.Core.Services.Software;

/// <summary>
/// Installs MySQL Server and Nginx on the host machine.
///
/// Linux path: invokes package manager via the privileged agent.
/// Windows path: downloads verified MSI from hardcoded URL, checks SHA-256,
///               then invokes msiexec via the agent with silent flags.
///
/// NOTE: When updating the MySQL/Nginx MSI version, regenerate the SHA-256
/// constants below using: certutil -hashfile <file> SHA256  (Windows)
///                                  sha256sum <file>         (Linux)
/// </summary>
public sealed class ThirdPartySoftwareInstaller
{
    // ── MySQL Windows installer ─────────────────────────────────────────────
    // MySQL Community Server 8.0.39 MSI — update when bumping version.
    private const string MySqlWindowsUrl =
        "https://dev.mysql.com/get/Downloads/MySQL-8.0/mysql-8.0.39-winx64.msi";
    private const string MySqlWindowsSha256 =
        "PLACEHOLDER_UPDATE_WITH_ACTUAL_SHA256_FOR_MYSQL_8.0.39_WINX64_MSI";

    // ── Nginx Windows installer ─────────────────────────────────────────────
    // Nginx 1.26.1 for Windows — update when bumping version.
    private const string NginxWindowsUrl =
        "https://nginx.org/download/nginx-1.26.1.zip";
    private const string NginxWindowsSha256 =
        "PLACEHOLDER_UPDATE_WITH_ACTUAL_SHA256_FOR_NGINX_1.26.1_ZIP";

    // ── Package names for Linux ────────────────────────────────────────────
    private const string MySqlLinuxPackage  = "mysql-server";
    private const string NginxLinuxPackage  = "nginx";

    // ── msiexec / apt-get / dnf paths ──────────────────────────────────────
    private const string MsiExecPath = @"C:\Windows\System32\msiexec.exe";
    private const string AptGetPath  = "/usr/bin/apt-get";
    private const string DnfPath     = "/usr/bin/dnf";

    private readonly IAgentClient _agent;
    private readonly HttpClient   _http;
    private readonly ILogger      _log;

    public ThirdPartySoftwareInstaller(IAgentClient agent, HttpClient http, TrionLogger trionLogger)
    {
        _agent = agent;
        _http  = http;
        _log   = trionLogger.CreateLogger(nameof(ThirdPartySoftwareInstaller));
    }

    public Task InstallMySqlAsync(
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken ct = default) =>
        InstallPackageAsync("MySQL Server", MySqlWindowsUrl, MySqlWindowsSha256,
            MySqlLinuxPackage, progress, ct);

    public Task InstallNginxAsync(
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken ct = default) =>
        InstallPackageAsync("Nginx", NginxWindowsUrl, NginxWindowsSha256,
            NginxLinuxPackage, progress, ct);

    // ── Core installation routine ───────────────────────────────────────────

    private async Task InstallPackageAsync(
        string displayName,
        string windowsUrl,
        string windowsSha256,
        string linuxPackageName,
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken ct)
    {
        _log.LogInformation("Starting {Name} installation", displayName);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            await InstallWindowsMsiAsync(displayName, windowsUrl, windowsSha256, progress, ct);
        else
            await InstallLinuxPackageAsync(displayName, linuxPackageName, progress, ct);

        _log.LogInformation("{Name} installation complete", displayName);
        progress.TryComplete();
    }

    // ── Windows path ────────────────────────────────────────────────────────

    private async Task InstallWindowsMsiAsync(
        string displayName,
        string msiUrl,
        string expectedSha256,
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken ct)
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"trion-{displayName.ToLower().Replace(" ", "-")}.msi");

        try
        {
            // Download
            await Report(progress, "Downloading", 10, $"Downloading {displayName} installer…", ct);
            _log.LogInformation("Downloading {Name} from {Url}", displayName, msiUrl);

            await using (var responseStream = await _http.GetStreamAsync(msiUrl, ct))
            await using (var fileStream = File.Create(tempPath))
            {
                await responseStream.CopyToAsync(fileStream, ct);
            }

            await Report(progress, "Verifying", 40, "Verifying SHA-256 checksum…", ct);

            // Checksum verification
            using var sha256 = SHA256.Create();
            await using var verifyStream = File.OpenRead(tempPath);
            var hashBytes = await sha256.ComputeHashAsync(verifyStream, ct);
            var actualHash = Convert.ToHexString(hashBytes);

            if (!actualHash.Equals(expectedSha256, StringComparison.OrdinalIgnoreCase))
            {
                var msg = $"SHA-256 mismatch for {displayName}: expected {expectedSha256}, got {actualHash}";
                _log.LogError("{Msg}", msg);
                await Report(progress, "Verification failed", -1, msg, ct);
                progress.TryComplete(new InvalidOperationException(msg));
                return;
            }

            // Launch silent install via agent
            await Report(progress, "Installing", 50, $"Running {displayName} installer silently…", ct);

            var result = await _agent.LaunchProcessAsync(
                new LaunchRequest(MsiExecPath, ["/i", tempPath, "/quiet", "/norestart"], null),
                ct: ct);

            if (!result.Success)
            {
                await Report(progress, "Install failed", -1, result.ErrorMessage, ct);
                progress.TryComplete(new InvalidOperationException(result.ErrorMessage));
                return;
            }

            // Wait for MSI to complete
            int pct = 55;
            while (result.Pid is { } pid && result.StartTime is { } st
                   && await _agent.IsProcessAliveAsync(pid, st, ct))
            {
                pct = Math.Min(pct + 5, 95);
                await Report(progress, "Installing", pct, "Installation in progress…", ct);
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
            }

            await Report(progress, "Complete", 100, $"{displayName} installed successfully.", ct);
        }
        finally
        {
            // Clean up temp MSI file
            try { File.Delete(tempPath); } catch { /* best-effort */ }
        }
    }

    // ── Linux path ──────────────────────────────────────────────────────────

    private async Task InstallLinuxPackageAsync(
        string displayName,
        string packageName,
        ChannelWriter<InstallProgressEvent> progress,
        CancellationToken ct)
    {
        await Report(progress, "Detecting package manager", 5, null, ct);

        var pkgManager = DetectLinuxPackageManager();
        if (pkgManager is null)
        {
            var msg = "Could not detect a supported package manager (apt-get or dnf).";
            await Report(progress, "Detection failed", -1, msg, ct);
            progress.TryComplete(new InvalidOperationException(msg));
            return;
        }

        await Report(progress, "Installing", 20,
            $"Running {pkgManager} install {packageName}…", ct);

        _log.LogInformation("Installing {Name} via {Mgr}", displayName, pkgManager);

        var result = await _agent.LaunchProcessAsync(
            new LaunchRequest(pkgManager, ["install", "-y", packageName], null),
            ct: ct);

        if (!result.Success)
        {
            await Report(progress, "Install failed", -1, result.ErrorMessage, ct);
            progress.TryComplete(new InvalidOperationException(result.ErrorMessage));
            return;
        }

        // Poll until package manager exits
        int pct = 25;
        while (result.Pid is { } pid && result.StartTime is { } st
               && await _agent.IsProcessAliveAsync(pid, st, ct))
        {
            pct = Math.Min(pct + 10, 90);
            await Report(progress, "Installing", pct, "Package installation in progress…", ct);
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
        }

        await Report(progress, "Complete", 100, $"{displayName} installed successfully.", ct);
    }

    // ── Distro detection ────────────────────────────────────────────────────

    /// <summary>
    /// Reads /etc/os-release to determine the package manager binary.
    /// Returns <c>null</c> if neither apt-get nor dnf is detected.
    /// </summary>
    private static string? DetectLinuxPackageManager()
    {
        const string osReleasePath = "/etc/os-release";
        if (!File.Exists(osReleasePath))
            return File.Exists(AptGetPath) ? AptGetPath
                 : File.Exists(DnfPath)    ? DnfPath
                 : null;

        var lines = File.ReadAllLines(osReleasePath);
        var id    = lines
            .Where(l => l.StartsWith("ID=", StringComparison.OrdinalIgnoreCase))
            .Select(l => l["ID=".Length..].Trim('"'))
            .FirstOrDefault() ?? string.Empty;

        return id.ToLowerInvariant() switch
        {
            "ubuntu" or "debian" or "linuxmint" or "pop"  => AptGetPath,
            "fedora" or "rhel"   or "centos"   or "rocky" or "almalinux" => DnfPath,
            _ when File.Exists(AptGetPath) => AptGetPath,
            _ when File.Exists(DnfPath)    => DnfPath,
            _ => null
        };
    }

    // ── Helpers ─────────────────────────────────────────────────────────────

    private static ValueTask Report(
        ChannelWriter<InstallProgressEvent> w,
        string step, int pct, string? msg,
        CancellationToken ct) =>
        w.WriteAsync(new InstallProgressEvent(step, pct, msg), ct);
}
