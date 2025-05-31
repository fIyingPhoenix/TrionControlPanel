using Microsoft.AspNetCore.Mvc;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;
using TrionControlPanel.API.Classes.Lists;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace TrionControlPanel.API.api
{
    // TrionController class for handling API requests.
    [Route("[controller]")]
    [ApiController]
    public class TrionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly DatabaseManager _databaseManager;

        // Constructor to initialize dependencies.
        public TrionController(IMemoryCache cache, IConfiguration configuration, DatabaseManager databaseManager)
        {
            _cache = cache;
            _configuration = configuration;
            _databaseManager = databaseManager;
        }

        // Endpoint to get the external IPv4 address of the client.
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
                clientIp = clientIp.Split(',')[0];
            }

            if (string.IsNullOrEmpty(clientIp))
            {
                return BadRequest("Unable to determine the IP address.");
            }

            return Ok(new { IPv4Address = clientIp });
        }

        [HttpGet("InstallSPP")]
        public async Task<IActionResult> InstallSPP([FromQuery] string Emulator, [FromQuery] string Key)
        {
            try
            {
                if (string.IsNullOrEmpty(Emulator))
                {
                    return BadRequest("Invalid or missing emulator type.");
                }
                TrionLogger.Log($"Request received for Emulator: {Emulator} with Key: {Key}");

                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);

                TrionLogger.Log($"Early Access: {isEarlyAccess}");

                var fileCacheKey = $"Files_{Emulator}_{isEarlyAccess}";

                TrionLogger.Log($"Checking cache for key: {fileCacheKey}");

                if (_cache.TryGetValue(fileCacheKey, out ConcurrentBag<FileList>? files))
                {
                    TrionLogger.Log("Cache hit - Returning files from cache.");
                    return Ok(new { Files = files });
                }

                string? RepackLocation = Emulator.ToLower() switch
                {
                    "classic" => isEarlyAccess ? _configuration["classicSPP:EarlyAccessKey:Install"] : _configuration["classicSPP:Default:Install"],
                    "tbc" => isEarlyAccess ? _configuration["tbcSPP:EarlyAccessKey:Install"] : _configuration["tbcSPP:Default:Install"],
                    "wotlk" => isEarlyAccess ? _configuration["wotlkSPP:EarlyAccessKey:Install"] : _configuration["wotlkSPP:Default:Install"],
                    "cata" => isEarlyAccess ? _configuration["cataSPP:EarlyAccessKey:Install"] : _configuration["cataSPP:Default:Install"],
                    "mop" => isEarlyAccess ? _configuration["mopSPP:EarlyAccessKey:Install"] : _configuration["mopSPP:Default:Install"],
                    "trion" => isEarlyAccess ? _configuration["trion:EarlyAccessKey:Install"] : _configuration["trion:Default:Install"],
                    "database" => isEarlyAccess ? _configuration["database:EarlyAccessKey:Install"] : _configuration["database:Default:Install"],
                    _ => null
                };

                TrionLogger.Log($"Repack Location for Emulator '{Emulator}': {RepackLocation}");

                if (string.IsNullOrEmpty(RepackLocation))
                {
                    return BadRequest("Invalid emulator type.");
                }

                TrionLogger.Log($"Fetching files from location: {RepackLocation}");

                files = await FileManager.GetFilesAsync(RepackLocation!);

                TrionLogger.Log($"Fetched {files.Count} files.");

                var fileCacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(fileCacheKey, files, fileCacheOptions);

                return Ok(new { Files = files });
            }
            catch (UnauthorizedAccessException ex)
            {
                TrionLogger.Log($"Access denied: {ex.Message}");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        // Endpoint to get the list of server files based on the emulator type and key.
        [HttpGet("GetServerFiles")]
        public async Task<IActionResult> GetFiles([FromQuery] string Emulator, [FromQuery] string Key)
        {
            try
            {
                if (string.IsNullOrEmpty(Emulator))
                {
                    return BadRequest("Invalid or missing emulator type.");
                }
                TrionLogger.Log($"Request received for Emulator: {Emulator} with Key: {Key}");

                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);

                TrionLogger.Log($"Early Access: {isEarlyAccess}");

                var fileCacheKey = $"Files_{Emulator}_{isEarlyAccess}";

                TrionLogger.Log($"Checking cache for key: {fileCacheKey}");

                if (_cache.TryGetValue(fileCacheKey, out ConcurrentBag<FileList>? files))
                {
                    TrionLogger.Log("Cache hit - Returning files from cache.");
                    return Ok(new { Files = files });
                }

                string? RepackLocation = Emulator.ToLower() switch
                {
                    "classic" => isEarlyAccess ? _configuration["classicSPP:EarlyAccessKey:Repair"] : _configuration["classicSPP:Default:Repair"],
                    "tbc" => isEarlyAccess ? _configuration["tbcSPP:EarlyAccessKey:Repair"] : _configuration["tbcSPP:Default:Repair"],
                    "wotlk" => isEarlyAccess ? _configuration["wotlkSPP:EarlyAccessKey:Repair"] : _configuration["wotlkSPP:Default:Repair"],
                    "cata" => isEarlyAccess ? _configuration["cataSPP:EarlyAccessKey:Repair"] : _configuration["cataSPP:Default:Repair"],
                    "mop" => isEarlyAccess ? _configuration["mopSPP:EarlyAccessKey:Repair"] : _configuration["mopSPP:Default:Repair"],
                    "trion" => isEarlyAccess ? _configuration["trion:EarlyAccessKey:Repair"] : _configuration["trion:Default:Repair"],
                    "database" => isEarlyAccess ? _configuration["database:EarlyAccessKey:Repair"] : _configuration["database:Default:Repair"],
                    _ => null
                };

                TrionLogger.Log($"Repack Location for Emulator '{Emulator}': {RepackLocation}");

                if (string.IsNullOrEmpty(RepackLocation))
                {
                    return BadRequest("Invalid emulator type.");
                }

                TrionLogger.Log($"Fetching files from location: {RepackLocation}");

                files = await FileManager.GetFilesAsync(RepackLocation!);

                TrionLogger.Log($"Fetched {files.Count} files.");

                var fileCacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(fileCacheKey, files, fileCacheOptions);

                return Ok(new { Files = files });
            }
            catch (UnauthorizedAccessException ex)
            {
                TrionLogger.Log($"Access denied: {ex.Message}");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to download a file based on the provided file request.
        [HttpPost("DownloadFile")]
        public IActionResult DownloadFile([FromBody] FileRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.FilePath) || !System.IO.File.Exists(request.FilePath))
                {
                    return BadRequest("Invalid or missing file path.");
                }

                string fileName = Path.GetFileName(request.FilePath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(request.FilePath);

                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (UnauthorizedAccessException ex)
            {
                TrionLogger.Log($"Access denied: {ex.Message}");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to get the version of various repacks based on the provided key.
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
                TrionLogger.Log($"Access denied: {ex.Message}");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to check the website ping.
        [HttpGet("GetWebsitePing")]
        public IActionResult GetWebsitePing()
        {
            return Ok();
        }

        // Endpoint to test download speed by generating a random file stream.
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