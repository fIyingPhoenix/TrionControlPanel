// TrionController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Globalization;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;
using TrionControlPanel.API.Classes.Lists;

namespace TrionControlPanel.API.Api;

[Route("[controller]")]
[ApiController]
public sealed class TrionController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _cfg;
    private readonly DatabaseManager _db;

    public TrionController(IMemoryCache cache, IConfiguration cfg, DatabaseManager db)
    {
        _cache = cache;
        _cfg = cfg;
        _db = db;
    }

    #region Public endpoints

    [HttpGet("RepairSPP")]
    public Task<IActionResult> RepairSPP([FromQuery] string emulator, [FromQuery] string key)
        => GetAndCacheFileListAsync(emulator, key, "Repair", false);

    [HttpGet("InstallSPP")]
    public Task<IActionResult> InstallSPP([FromQuery] string emulator, [FromQuery] string key)
        => GetAndCacheFileListAsync(emulator, key, "Install", true);

    [HttpPost("DownloadFile")]
    public async Task<IActionResult> DownloadFile([FromQuery] string emulator, [FromQuery] string key, [FromBody] FileRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FilePath))
            return BadRequest("filePath missing");

        var isEarly = !string.IsNullOrEmpty(key) && await _db.GetKeyVerified(key);
        var basePath = GetRepackPath(emulator, isEarly, "BaseLocation");

        if (string.IsNullOrEmpty(basePath) || !Directory.Exists(basePath))
            return NotFound("Package not found.");

        // Kill path traversal
        var fullPath = Path.GetFullPath(Path.Combine(basePath, request.FilePath.TrimStart('/', '\\')));
        if (!fullPath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
        {
            TrionLogger.Log($"Path traversal attempt: {request.FilePath}");
            return BadRequest("Illegal path.");
        }

        if (!System.IO.File.Exists(fullPath))
            return NotFound("File not found.");

        // Return stream instead of byte[] to avoid double memory allocation
        var stream = System.IO.File.OpenRead(fullPath);
        return File(stream, "application/octet-stream", Path.GetFileName(fullPath));
    }

    [HttpPost("SendSupporterKey")]
    public async Task<IActionResult> SendSupporterKey([FromBody] SupporterKey dto, [FromHeader(Name = "APIKey")] string? headerKey)
    {
        var configuredKey = _cfg["APIKey"];
        if (string.IsNullOrEmpty(headerKey) || headerKey != configuredKey)
            return Unauthorized("Invalid or missing API Key.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _db.GetKeyVerified(dto.Key);
        if (exists)
            return Conflict("Key already exists.");

        await _db.InsertSupporterKey(dto.Key, dto.UID);
        return CreatedAtAction(nameof(SendSupporterKey), new { key = dto.Key },
            new { message = "Supporter Key created." });
    }

    [HttpGet("GetFileVersion")]
    public async Task<IActionResult> GetRepackVersion([FromQuery] string key)
    {
        var isEarly = !string.IsNullOrEmpty(key) && await _db.GetKeyVerified(key);
        var suffix = isEarly ? "EarlyAccess" : "Default";

        return Ok(new
        {
            Trion = GetVersion("trion", suffix),
            Database = GetVersion("database", suffix),
            ClassicSPP = GetVersion("classicSPP", suffix),
            TbcSPP = GetVersion("tbcSPP", suffix),
            WotlkSPP = GetVersion("wotlkSPP", suffix),
            CataSPP = GetVersion("cataSPP", suffix),
            MopSPP = GetVersion("mopSPP", suffix)
        });
    }

    [HttpGet("GetExternalIPv4")]
    public IActionResult GetExternalIPv4()
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        return string.IsNullOrEmpty(ip)
            ? BadRequest("Unable to determine IP.")
            : Ok(new { IPv4Address = ip });
    }

    [HttpGet("Ping")]
    public IActionResult Ping()
    {
        Response.Headers.CacheControl = "no-store, no-cache, must-revalidate, proxy-revalidate";
        return Ok();
    }

    [HttpGet("DownloadSpeedTest")]
    public IActionResult DownloadSpeedTest([FromQuery] int sizeInMB = 100)
    {
        sizeInMB = Math.Clamp(sizeInMB, 1, 1_000);
        var stream = Network.GenerateRandomStream(sizeInMB);
        return new FileStreamResult(stream, "application/octet-stream")
        {
            FileDownloadName = $"speedtest_{sizeInMB}MB.bin",
            EnableRangeProcessing = false
        };
    }

    #endregion

    #region Helpers

    private async Task<IActionResult> GetAndCacheFileListAsync(string emulator, string key, string operation, bool install)
    {
        if (string.IsNullOrWhiteSpace(emulator))
            return BadRequest("Invalid emulator.");

        var isEarly = !string.IsNullOrEmpty(key) && await _db.GetKeyVerified(key);
        var cacheKey = $"Files_{emulator}_{isEarly}_{operation}";

        if (_cache.TryGetValue<ConcurrentBag<FileList>>(cacheKey, out var files))
        {
            TrionLogger.Log($"Cache hit for file list: '{cacheKey}'.");
            return Ok(new { Files = files });
        }

        var path = GetRepackPath(emulator, isEarly, operation);
        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            return BadRequest($"Path not found for {operation}.");

        files = await FileManager.GetFilesAsync(path, install);

        _cache.Set(cacheKey, files, TimeSpan.FromHours(1));
        TrionLogger.Log($"Fetched {files.Count} files for '{cacheKey}'. Cached.");
        return Ok(new { Files = files });
    }

    private string? GetRepackPath(string emulator, bool isEarly, string operation)
    {
        var access = isEarly ? "EarlyAccess" : "Default";
        var key = emulator.Equals("trion", StringComparison.OrdinalIgnoreCase) ||
                  emulator.Equals("database", StringComparison.OrdinalIgnoreCase)
            ? $"{emulator.ToLowerInvariant()}:{access}:{operation}"
            : $"{emulator.ToLowerInvariant()}SPP:{access}:{operation}";

        return _cfg[key];
    }

    private string GetVersion(string emulatorPrefix, string accessSuffix)
    {
        return FileManager.GetVersion(_cfg[$"{emulatorPrefix}:Version:{accessSuffix}"]!);
    }

    #endregion
}