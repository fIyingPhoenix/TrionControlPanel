using Microsoft.AspNetCore.Mvc;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;
using TrionControlPanel.API.Classes.Lists;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace TrionControlPanel.API.api
{
    [Route("[controller]")]
    [ApiController]
    public class TrionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly DatabaseManager _databaseManager;
        public TrionController(IMemoryCache cache, IConfiguration configuration, DatabaseManager databaseManager)
        {
            _cache = cache;
            _configuration = configuration;
            _databaseManager = databaseManager;
        }

        [HttpGet("GetExternalIPv4")]
        public IActionResult GetExternalIPv4()
        {
            string? clientIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            else
            {
                // If X-Forwarded-For contains multiple IPs, take the first one (the original client IP)
                clientIp = clientIp.Split(',')[0];
            }

            if (string.IsNullOrEmpty(clientIp))
            {
                return BadRequest("Unable to determine the IP address.");
            }

            return Ok(new { IPv4Address = clientIp });
        }

        [HttpGet("GetServerFiles")]
        public async Task<IActionResult> GetFiles([FromQuery] string Emulator, [FromQuery] string Key)
        {
            try
            {
                if (string.IsNullOrEmpty(Emulator))
                {
                    return BadRequest("Invalid or missing emulator type.");
                }

                Console.WriteLine($"Emulator: {Emulator}");
                await TrionLogger.Log($"Request received for Emulator: {Emulator} with Key: {Key}");

                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);

                // Log early access status
                await TrionLogger.Log($"Early Access: {isEarlyAccess}");

                var fileCacheKey = $"Files_{Emulator}_{isEarlyAccess}";

                // Log cache lookup attempt
                await TrionLogger.Log($"Checking cache for key: {fileCacheKey}");

                if (_cache.TryGetValue(fileCacheKey, out ConcurrentBag<FileList> files))
                {
                    await TrionLogger.Log("Cache hit - Returning files from cache.");
                    return Ok(new { Files = files });
                }

                string? RepackLocation = Emulator.ToLower() switch
                {
                    "classic" => isEarlyAccess ? _configuration["classicSPP:EarlyAccessKey"] : _configuration["classicSPP:Default"],
                    "tbc" => isEarlyAccess ? _configuration["tbcSPP:EarlyAccessKey"] : _configuration["tbcSPP:Default"],
                    "wotlk" => isEarlyAccess ? _configuration["wotlkSPP:EarlyAccessKey"] : _configuration["wotlkSPP:Default"],
                    "cata" => isEarlyAccess ? _configuration["cataSPP:EarlyAccessKey"] : _configuration["cataSPP:Default"],
                    "mop" => isEarlyAccess ? _configuration["mopSPP:EarlyAccessKey"] : _configuration["mopSPP:Default"],
                    _ => null
                };

                // Log repack location
                await TrionLogger.Log($"Repack Location for Emulator '{Emulator}': {RepackLocation}");

                if (string.IsNullOrEmpty(RepackLocation))
                {
                    return BadRequest("Invalid emulator type.");
                }

                // Fetch file list from the specified location
                await TrionLogger.Log($"Fetching files from location: {RepackLocation}");

                files = await FileManager.GetFilesAsync(RepackLocation!);

                // Log file count
                await TrionLogger.Log($"Fetched {files.Count} files.");

                // Cache the file list for 1 hour
                var fileCacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(fileCacheKey, files, fileCacheOptions);

                return Ok(new { Files = files });
            }
            catch (UnauthorizedAccessException ex)
            {
                await TrionLogger.Log($"Access denied: {ex.Message}");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile([FromBody] string filePath)
        {
            try
            {
                // Validate the file path
                if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                {
                    return BadRequest("Invalid or missing file path.");
                }
                // Get file name for the download
                string fileName = Path.GetFileName(filePath);

                // Read the file content
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Return the file as a downloadable response
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("GetFileVersion")]
        public async Task<IActionResult> GetRepackVersion([FromQuery] string Key)
        {
            try
            {
                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);

                string GetVersion(string keyBase) =>
                    FileManager.GetVersion(_configuration[$"{keyBase}:Version:{(isEarlyAccess ? "EarlyAccess" : "Default")}"]!);

                var versions = new
                {
                    Trion = GetVersion("trion"),
                    Database = GetVersion("database"),
                    ClassicSPP = GetVersion("classicSPP"),
                    TbcSPP = GetVersion("tbcSPP"),
                    WotlkSPP = GetVersion("wotlkSPP"),
                    CataSPP = GetVersion("cataSPP"),
                    MopSPP = GetVersion("mopSPP"),
                };

                return Ok(versions);
            }
 
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        [HttpGet("GetWebsitePing")]
        public IActionResult GetWebsitePing()
        {
            return Ok();
        }
        [HttpGet("DownloadSpeed")]
        public IActionResult DownloadSpeedTest()
        {
            return new FileStreamResult(Network.GenerateRandomStream(), "application/octet-stream")
            {
                FileDownloadName = "speedtest.bin"
            };
        }

    }
}