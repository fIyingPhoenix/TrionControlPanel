using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Trion.Desktop.Infrastructure.Constants;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class EmulatorInstallerService : IEmulatorInstallerService
{
    private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromHours(2) };

    private readonly IApiKeyService _apiKeys;

    public EmulatorInstallerService(IApiKeyService apiKeys)
    {
        _apiKeys = apiKeys;
    }

    // ── Public API ────────────────────────────────────────────────────────────

    public Task InstallAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
        => RunInstallCoreAsync(emulatorName, installPath, progress, ct);

    public Task RepairAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
        => RunRepairCoreAsync(emulatorName, installPath, progress, ct);

    public async Task UninstallAsync(string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        progress?.Report(new InstallProgress("Removing", 0, 0, "Deleting files…"));
        if (Directory.Exists(installPath))
            await Task.Run(() => Directory.Delete(installPath, recursive: true), ct);
        progress?.Report(new InstallProgress("Done", 100, 0, "Uninstalled."));
    }

    public async Task<EmulatorVersions> FetchLatestVersionsAsync(CancellationToken ct)
    {
        try
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, AppLinks.GetFileVersionUrl);
            AddApiKeyHeader(req);
            using var resp = await _http.SendAsync(req, ct);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<EmulatorVersions>(ct) ?? new EmulatorVersions();
        }
        catch
        {
            return new EmulatorVersions();
        }
    }

    public string GetLocalVersion(string exePath)
    {
        if (string.IsNullOrWhiteSpace(exePath) || !File.Exists(exePath))
            return "—";
        try
        {
            var info    = FileVersionInfo.GetVersionInfo(exePath);
            var version = info.FileVersion ?? "";

            if (version.Contains("SPP", StringComparison.OrdinalIgnoreCase))
            {
                var m = Regex.Match(version, @"\b\d{4}[-/]\d{2}[-/]\d{2}\b");
                if (m.Success) return m.Value;
            }

            return string.IsNullOrEmpty(version) ? "—" : version;
        }
        catch
        {
            return "—";
        }
    }

    // ── Install: download full ZIP and extract ────────────────────────────────

    private async Task RunInstallCoreAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        Directory.CreateDirectory(installPath);

        var zipPath = Path.Combine(Path.GetTempPath(), $"trion_{emulatorName}_{Guid.NewGuid():N}.zip");
        try
        {
            await DownloadZipAsync(AppLinks.DownloadFileUrl(emulatorName), zipPath, "Installing", progress, ct);
            await ExtractZipAsync(zipPath, installPath, progress, ct);
            progress?.Report(new InstallProgress("Cleaning up", 99, 0, "Removing temporary files…"));
        }
        finally
        {
            TryDelete(zipPath);
        }

        progress?.Report(new InstallProgress("Done", 100, 0, "Installation complete."));
    }

    // ── Repair: diff-based — only download what's missing or corrupt ──────────

    private async Task RunRepairCoreAsync(string emulatorName, string installPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        // 1. Fetch the server file manifest
        progress?.Report(new InstallProgress("Fetching manifest", 0, 0, "Contacting server…"));
        var manifest = await FetchRepairManifestAsync(emulatorName, ct);

        if (manifest.Count == 0)
            throw new InvalidOperationException("Server returned an empty file manifest.");

        progress?.Report(new InstallProgress("Fetching manifest", 100, 0, $"{manifest.Count} files on server."));

        // 2. Scan local files and compute MD5 hashes concurrently
        var localHashes = await ScanLocalFilesAsync(installPath, emulatorName, progress, ct);

        // 3. Compare — find files that are missing or have a wrong hash
        progress?.Report(new InstallProgress("Comparing", 100, 0, "Identifying files to repair…"));
        var toRepair = FindFilesToRepair(manifest, localHashes, emulatorName);

        if (toRepair.Count == 0)
        {
            progress?.Report(new InstallProgress("Done", 100, 0, "All files verified. Nothing to repair."));
            return;
        }

        progress?.Report(new InstallProgress("Comparing", 100, 0, $"{toRepair.Count} file(s) need repair."));

        // 4. Download only the missing / corrupt files
        await DownloadRepairFilesAsync(emulatorName, installPath, toRepair, progress, ct);

        progress?.Report(new InstallProgress("Done", 100, 0, $"Repair complete — {toRepair.Count} file(s) restored."));
    }

    // ── Manifest fetch ────────────────────────────────────────────────────────

    private async Task<List<FileManifestEntry>> FetchRepairManifestAsync(
        string emulatorName, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, AppLinks.RepairSppUrl);
        AddApiKeyHeader(req);
        req.Content = JsonContent.Create(new { emulator = emulatorName });

        using var resp = await _http.SendAsync(req, ct);
        resp.EnsureSuccessStatusCode();

        var manifest = await resp.Content.ReadFromJsonAsync<FileManifest>(ct);
        return manifest?.Files ?? [];
    }

    // ── Local scan (concurrent MD5 hashing) ───────────────────────────────────

    private static async Task<Dictionary<string, string>> ScanLocalFilesAsync(
        string installPath, string emulatorName,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        // Key: normalised server-style path  e.g. "/wotlk/bin/worldserver.exe"
        // Value: MD5 hash (uppercase hex)
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!Directory.Exists(installPath))
            return result;

        var files      = Directory.EnumerateFiles(installPath, "*", SearchOption.AllDirectories).ToList();
        var prefix     = $"/{emulatorName.Trim('/')}";
        int done       = 0;
        var semaphore  = new SemaphoreSlim(Environment.ProcessorCount * 2);

        var tasks = files.Select(async file =>
        {
            await semaphore.WaitAsync(ct);
            try
            {
                ct.ThrowIfCancellationRequested();

                var hash     = await ComputeMd5Async(file, ct);
                var relative = Path.GetRelativePath(installPath, file).Replace('\\', '/');
                var key      = $"{prefix}/{relative}";

                lock (result)
                    result[key] = hash;

                int scanned = Interlocked.Increment(ref done);
                if (scanned % 50 == 0 || scanned == files.Count)
                {
                    int pct = files.Count > 0 ? scanned * 100 / files.Count : 100;
                    progress?.Report(new InstallProgress("Scanning", pct, 0,
                        $"{scanned} / {files.Count} files scanned"));
                }
            }
            finally { semaphore.Release(); }
        });

        await Task.WhenAll(tasks);
        return result;
    }

    // ── Comparison ────────────────────────────────────────────────────────────

    private static List<FileManifestEntry> FindFilesToRepair(
        List<FileManifestEntry> manifest,
        Dictionary<string, string> localHashes,
        string emulatorName)
    {
        // Build the same normalised key the scanner uses: "/wotlk/bin/worldserver.exe"
        return manifest
            .Where(entry =>
            {
                var key = $"{entry.Path.TrimEnd('/')}/{entry.Name}";
                return !localHashes.TryGetValue(key, out var localHash)
                       || !string.Equals(localHash, entry.Hash, StringComparison.OrdinalIgnoreCase);
            })
            .ToList();
    }

    // ── Download individual repair files ──────────────────────────────────────

    private async Task DownloadRepairFilesAsync(
        string emulatorName, string installPath,
        List<FileManifestEntry> files,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        var prefix    = $"/{emulatorName.Trim('/')}/";
        var url       = AppLinks.DownloadSingleFileUrl;  // POST body carries emulator + filePath
        int total     = files.Count;
        var sw        = new Stopwatch();

        for (int i = 0; i < total; i++)
        {
            ct.ThrowIfCancellationRequested();

            var entry    = files[i];
            var fullPath = $"{entry.Path.TrimEnd('/')}/{entry.Name}";

            // Derive the local destination path by stripping the emulator prefix
            var relative = fullPath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? fullPath[prefix.Length..]
                : fullPath.TrimStart('/');

            var destPath = Path.GetFullPath(Path.Combine(installPath, relative.Replace('/', Path.DirectorySeparatorChar)));

            // Path traversal guard
            if (!destPath.StartsWith(Path.GetFullPath(installPath), StringComparison.OrdinalIgnoreCase))
                continue;

            Directory.CreateDirectory(Path.GetDirectoryName(destPath)!);

            progress?.Report(new InstallProgress("Repairing", i * 100 / total, 0,
                $"{entry.Name}  ({i + 1} / {total})"));

            sw.Restart();
            await DownloadSingleFileAsync(url, emulatorName, fullPath, destPath, i, total, progress, ct);

            // Preserve file timestamps from manifest
            if (File.Exists(destPath) && entry.LastWriteTime != default)
            {
                File.SetLastWriteTimeUtc(destPath, entry.LastWriteTime);
                File.SetCreationTimeUtc(destPath, entry.LastWriteTime);
            }
        }
    }

    private async Task DownloadSingleFileAsync(
        string url, string emulatorName, string serverFilePath, string destPath,
        int fileIndex, int total,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, url);
        AddApiKeyHeader(req);
        req.Content = JsonContent.Create(new { emulator = emulatorName, filePath = serverFilePath });

        using var resp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
        resp.EnsureSuccessStatusCode();

        long   contentLen = resp.Content.Headers.ContentLength ?? -1;
        long   total_     = 0;
        long   prevRead   = 0;
        var    sw         = Stopwatch.StartNew();

        await using var src = await resp.Content.ReadAsStreamAsync(ct);
        await using var dst = File.Create(destPath);

        var buf  = new byte[81_920];
        int read;

        while ((read = await src.ReadAsync(buf, ct)) > 0)
        {
            await dst.WriteAsync(buf.AsMemory(0, read), ct);
            total_ += read;

            if (sw.Elapsed.TotalMilliseconds >= 400)
            {
                double speed  = (total_ - prevRead) / sw.Elapsed.TotalSeconds / 1_048_576.0;
                prevRead      = total_;
                sw.Restart();

                int    pct    = contentLen > 0 ? (int)(total_ * 100L / contentLen) : 0;
                string status = contentLen > 0
                    ? $"{FormatBytes(total_)} / {FormatBytes(contentLen)}  ({fileIndex + 1} / {total})"
                    : $"{FormatBytes(total_)}  ({fileIndex + 1} / {total})";

                progress?.Report(new InstallProgress("Repairing", pct, speed, status));
            }
        }
    }

    // ── Install helpers ───────────────────────────────────────────────────────

    private async Task DownloadZipAsync(string url, string destPath, string phase,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        AddApiKeyHeader(req);

        using var resp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
        resp.EnsureSuccessStatusCode();

        long contentLen = resp.Content.Headers.ContentLength ?? -1;
        long total      = 0;
        long prevRead   = 0;
        var  sw         = Stopwatch.StartNew();

        await using var src = await resp.Content.ReadAsStreamAsync(ct);
        await using var dst = File.Create(destPath);

        var buf  = new byte[81_920];
        int read;

        while ((read = await src.ReadAsync(buf, ct)) > 0)
        {
            await dst.WriteAsync(buf.AsMemory(0, read), ct);
            total += read;

            if (sw.Elapsed.TotalMilliseconds >= 400)
            {
                double speed  = (total - prevRead) / sw.Elapsed.TotalSeconds / 1_048_576.0;
                prevRead      = total;
                sw.Restart();

                int    pct    = contentLen > 0 ? (int)(total * 100L / contentLen) : 0;
                string status = contentLen > 0
                    ? $"{FormatBytes(total)} / {FormatBytes(contentLen)}"
                    : FormatBytes(total);

                progress?.Report(new InstallProgress(phase, pct, speed, status));
            }
        }

        progress?.Report(new InstallProgress(phase, 100, 0, "Download complete."));
    }

    private static async Task ExtractZipAsync(string zipPath, string destPath,
        IProgress<InstallProgress>? progress, CancellationToken ct)
    {
        await Task.Run(() =>
        {
            using var archive = ZipFile.OpenRead(zipPath);

            int total = archive.Entries.Count(e => !string.IsNullOrEmpty(e.Name));
            int done  = 0;

            foreach (var entry in archive.Entries)
            {
                ct.ThrowIfCancellationRequested();

                var targetPath = Path.GetFullPath(Path.Combine(destPath, entry.FullName));

                if (!targetPath.StartsWith(Path.GetFullPath(destPath), StringComparison.OrdinalIgnoreCase))
                    continue;

                if (string.IsNullOrEmpty(entry.Name))
                {
                    Directory.CreateDirectory(targetPath);
                    continue;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
                entry.ExtractToFile(targetPath, overwrite: true);

                // Preserve archive timestamps
                File.SetLastWriteTime(targetPath, entry.LastWriteTime.DateTime);
                File.SetCreationTime(targetPath, entry.LastWriteTime.DateTime);

                done++;
                int pct = done * 100 / Math.Max(total, 1);
                progress?.Report(new InstallProgress("Extracting", pct, 0, entry.Name));
            }
        }, ct);

        progress?.Report(new InstallProgress("Extracting", 100, 0, "Extraction complete."));
    }

    // ── MD5 hashing (for file integrity, not cryptography) ───────────────────

#pragma warning disable CA5351
    private static async Task<string> ComputeMd5Async(string filePath, CancellationToken ct)
    {
        await using var stream = new FileStream(
            filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);

        using var md5  = MD5.Create();
        var       hash = await md5.ComputeHashAsync(stream, ct);
        return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
    }
#pragma warning restore CA5351

    // ── Shared helpers ────────────────────────────────────────────────────────

    private void AddApiKeyHeader(HttpRequestMessage req)
    {
        var key = _apiKeys.ActiveKey;
        if (!string.IsNullOrEmpty(key))
            req.Headers.TryAddWithoutValidation("X-API-Key", key);
    }

    private static string FormatBytes(long bytes) => bytes switch
    {
        >= 1_073_741_824 => $"{bytes / 1_073_741_824.0:F2} GB",
        >= 1_048_576     => $"{bytes / 1_048_576.0:F1} MB",
        >= 1_024         => $"{bytes / 1_024.0:F0} KB",
        _                => $"{bytes} B"
    };

    private static void TryDelete(string path)
    {
        try { if (File.Exists(path)) File.Delete(path); } catch { }
    }

    // ── API response models ───────────────────────────────────────────────────

    private sealed record FileManifest(
        [property: JsonPropertyName("files")] List<FileManifestEntry> Files);

    private sealed record FileManifestEntry(
        [property: JsonPropertyName("name")]           string   Name,
        [property: JsonPropertyName("size")]           double   SizeMb,
        [property: JsonPropertyName("hash")]           string   Hash,
        [property: JsonPropertyName("path")]           string   Path,
        [property: JsonPropertyName("last_write_utc")] DateTime LastWriteTime);
}
