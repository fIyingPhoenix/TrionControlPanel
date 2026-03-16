using System.Collections.Concurrent;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Trion.API.Data;

namespace Trion.API.Endpoints;

public static class AdminEndpoints
{
    public static WebApplication MapAdminEndpoints(this WebApplication app)
    {
        var g = app.MapGroup("/Trion/admin").WithTags("Admin");

        g.MapPost("supporter-key",  CreateSupporterKey);
        g.MapPost("upload-emulator", UploadEmulator);

        return app;
    }

    // ── POST /Trion/admin/supporter-key ──────────────────────────────────────

    private static async Task<IResult> CreateSupporterKey(
        [FromBody]                       SupporterKeyRequest req,
        [FromHeader(Name = "X-API-Key")] string?             apiKey,
        IConfiguration                                       cfg,
        TrionDbAccess                                        db,
        ILogger<Program>                                     log)
    {
        if (!IsAdminKey(apiKey, cfg))
            return Results.Unauthorized();

        if (string.IsNullOrWhiteSpace(req.Key) || req.UID <= 0)
            return Results.BadRequest(new { message = "key and uID are required." });

        var exists = await db.QuerySingleAsync<bool>(TrionSql.GetKeyExists, new { ApiKey = req.Key });
        if (exists) return Results.Conflict(new { message = "Key already exists." });

        await db.ExecuteAsync(TrionSql.InsertSupporterKey, new { ApiKey = req.Key, req.UID });
        log.LogInformation("Supporter key created for UID {UID}.", req.UID);

        return Results.Created($"/Trion/admin/supporter-key/{req.Key}",
            new { message = "Supporter key created." });
    }

    // ── POST /Trion/admin/upload-emulator ─────────────────────────────────────

    private static async Task<IResult> UploadEmulator(
        HttpContext                      ctx,
        [FromHeader(Name = "X-API-Key")] string? apiKey,
        IConfiguration                           cfg,
        ILogger<Program>                         log,
        CancellationToken                        ct)
    {
        if (!IsAdminKey(apiKey, cfg))
            return Results.Unauthorized();

        if (!ctx.Request.HasFormContentType)
            return Results.BadRequest(new { message = "Multipart form data expected." });

        var form = await ctx.Request.ReadFormAsync(ct);

        var emulator = form["emulator"].ToString();
        var version  = form["version"].ToString();
        var tier     = form["tier"].ToString();
        var file     = form.Files.GetFile("file");

        // ── Validate ──────────────────────────────────────────────────────────
        if (file is null || file.Length == 0)
            return Results.BadRequest(new { message = "No file uploaded." });

        if (!file.FileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            return Results.BadRequest(new { message = "Only .zip files are accepted." });

        if (string.IsNullOrWhiteSpace(emulator))
            return Results.BadRequest(new { message = "emulator is required." });

        tier = string.IsNullOrWhiteSpace(tier) ? "Default" : tier.Trim();
        if (tier != "Default" && tier != "EarlyAccess")
            return Results.BadRequest(new { message = "tier must be 'Default' or 'EarlyAccess'." });

        var cfgKey   = NormalizeEmulatorName(emulator);
        var zipPath  = cfg[$"{cfgKey}:ZipPath:{tier}"];
        var filesDir = cfg[$"{cfgKey}:FilesPath:{tier}"];

        if (string.IsNullOrEmpty(zipPath))
            return Results.BadRequest(new { message = $"ZipPath for '{emulator}' ({tier}) is not configured." });

        if (string.IsNullOrEmpty(filesDir))
            return Results.BadRequest(new { message = $"FilesPath for '{emulator}' ({tier}) is not configured." });

        // ── Save ZIP ──────────────────────────────────────────────────────────
        Directory.CreateDirectory(Path.GetDirectoryName(zipPath)!);
        await using (var dest = File.Create(zipPath))
            await file.CopyToAsync(dest, ct);

        log.LogInformation("Saved ZIP for '{Emulator}' ({Tier}) — {Bytes:N0} bytes → '{Path}'",
            emulator, tier, file.Length, zipPath);

        // ── Extract ───────────────────────────────────────────────────────────
        if (Directory.Exists(filesDir))
            Directory.Delete(filesDir, recursive: true);
        Directory.CreateDirectory(filesDir);

        await Task.Run(() =>
        {
            using var archive = ZipFile.OpenRead(zipPath);
            var baseDir = Path.GetFullPath(filesDir);

            foreach (var entry in archive.Entries)
            {
                ct.ThrowIfCancellationRequested();
                var target = Path.GetFullPath(Path.Combine(filesDir, entry.FullName));
                if (!target.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase)) continue;

                if (string.IsNullOrEmpty(entry.Name)) { Directory.CreateDirectory(target); continue; }

                Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                entry.ExtractToFile(target, overwrite: true);
                File.SetLastWriteTimeUtc(target, entry.LastWriteTime.UtcDateTime);
            }
        }, ct);

        log.LogInformation("Extracted '{Emulator}' ({Tier}) → '{Dir}'", emulator, tier, filesDir);

        // ── Write version.txt ─────────────────────────────────────────────────
        if (!string.IsNullOrWhiteSpace(version))
            await File.WriteAllTextAsync(Path.Combine(filesDir, "version.txt"), version.Trim(), ct);

        // ── Build manifest ────────────────────────────────────────────────────
        var manifest  = await BuildManifestAsync(filesDir, emulator.Trim().ToLowerInvariant(), ct);
        var fileCount = CountFiles(manifest);

        await File.WriteAllTextAsync(Path.Combine(filesDir, "manifest.json"), manifest, ct);

        log.LogInformation("Generated manifest for '{Emulator}' ({Tier}) — {Count} files",
            emulator, tier, fileCount);

        return Results.Ok(new { message = $"'{emulator}' ({tier}) processed successfully.", version = version?.Trim(), files = fileCount });
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static bool IsAdminKey(string? key, IConfiguration cfg)
    {
        var configured = cfg["AdminApiKey"];
        return !string.IsNullOrEmpty(key) && key == configured;
    }

    private static string NormalizeEmulatorName(string emulator) =>
        emulator.Trim().ToLowerInvariant() switch
        {
            "classic" => "classicSPP",
            "tbc"     => "tbcSPP",
            "wotlk"   => "wotlkSPP",
            "cata"    => "cataSPP",
            "mop"     => "mopSPP",
            var other => other
        };

    private static async Task<string> BuildManifestAsync(
        string filesPath, string emulatorName, CancellationToken ct)
    {
        var files = Directory.EnumerateFiles(filesPath, "*", SearchOption.AllDirectories)
            .Where(f => Path.GetFileName(f) is not ("manifest.json" or "version.txt"))
            .ToList();

        var entries = new ConcurrentBag<object>();
        var sem     = new SemaphoreSlim(Environment.ProcessorCount * 2);

        await Task.WhenAll(files.Select(async f =>
        {
            await sem.WaitAsync(ct);
            try
            {
                var info     = new FileInfo(f);
                var relative = Path.GetRelativePath(filesPath, f).Replace('\\', '/');
                var dir      = Path.GetDirectoryName(relative)?.Replace('\\', '/') ?? "";
                var hash     = await ComputeMd5Async(f, ct);

                entries.Add(new
                {
                    name           = info.Name,
                    size           = Math.Round(info.Length / 1_048_576.0, 4),
                    hash,
                    path           = string.IsNullOrEmpty(dir) ? $"/{emulatorName}" : $"/{emulatorName}/{dir}",
                    last_write_utc = info.LastWriteTimeUtc,
                });
            }
            finally { sem.Release(); }
        }));

        return JsonSerializer.Serialize(new { files = entries.ToList() });
    }

#pragma warning disable CA5351   // MD5 is used for file integrity, not security
    private static async Task<string> ComputeMd5Async(string path, CancellationToken ct)
    {
        await using var stream = new FileStream(
            path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        using var md5 = MD5.Create();
        return BitConverter.ToString(await md5.ComputeHashAsync(stream, ct)).Replace("-", "").ToUpperInvariant();
    }
#pragma warning restore CA5351

    private static int CountFiles(string manifestJson) =>
        manifestJson.Split("\"name\"").Length - 1;
}

public sealed record SupporterKeyRequest(string Key, int UID);
