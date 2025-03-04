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
        private readonly string _logFilePath = "logs";

        // Constructor to initialize dependencies
        public TrionController(IMemoryCache cache, IConfiguration configuration, DatabaseManager databaseManager)
        {
            _cache = cache;
            _configuration = configuration;
            _databaseManager = databaseManager;

            // Creating logging directory if not exists
            if (!Directory.Exists(_logFilePath))
            {
                Directory.CreateDirectory(_logFilePath);
            }
            TrionLogger.Log("TrionController initialized.", "INFO");
        }

        // Endpoint to get external IPv4 address of the client
        [HttpGet("GetExternalIPv4")]
        public IActionResult GetExternalIPv4()
        {
            TrionLogger.Log("GetExternalIPv4 called.", "INFO");

            // Try to get the client IP from the request headers (X-Forwarded-For)
            string? clientIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            // If not found in headers, fall back to connection remote IP
            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            else
            {
                // If X-Forwarded-For contains multiple IPs, take the first one (the original client IP)
                clientIp = clientIp.Split(',')[0];
            }

            // If client IP is still not determined, return a bad request response
            if (string.IsNullOrEmpty(clientIp))
            {
                TrionLogger.Log("Unable to determine the IP address.", "ERROR");
                return BadRequest("Unable to determine the IP address.");
            }

            // Return the client IP as a response
            TrionLogger.Log($"Returning IP address: {clientIp}", "INFO");
            return Ok(new { IPv4Address = clientIp });
        }

        // Endpoint to get a list of files for a given emulator
        [HttpGet("GetServerFiles")]
        public async Task<IActionResult> GetFiles([FromQuery] string Emulator, [FromQuery] string Key)
        {
            try
            {
                TrionLogger.Log($"GetServerFiles called for Emulator: {Emulator} with Key: {Key}", "INFO");

                // Check if emulator type is provided
                if (string.IsNullOrEmpty(Emulator))
                {
                    TrionLogger.Log("Invalid or missing emulator type.", "ERROR");
                    return BadRequest("Invalid or missing emulator type.");
                }

                // Log the incoming request for the emulator and key
                TrionLogger.Log($"Request received for Emulator: {Emulator} with Key: {Key}", "INFO");

                // Check if the key is valid for early access
                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);
                TrionLogger.Log($"Early Access: {isEarlyAccess}", "INFO");

                // Create cache key based on emulator and early access status
                var fileCacheKey = $"Files_{Emulator}_{isEarlyAccess}";

                // Log cache lookup attempt
                TrionLogger.Log($"Checking cache for key: {fileCacheKey}", "INFO");

                // Check if files are already cached
                if (_cache.TryGetValue(fileCacheKey, out ConcurrentBag<FileList>? files))
                {
                    TrionLogger.Log("Cache hit - Returning files from cache.", "INFO");
                    return Ok(new { Files = files });
                }

                // Get the repack location based on emulator and early access status
                string? RepackLocation = Emulator.ToLower() switch
                {
                    "classic" => isEarlyAccess ? _configuration["classicSPP:EarlyAccessKey"] : _configuration["classicSPP:Default"],
                    "tbc" => isEarlyAccess ? _configuration["tbcSPP:EarlyAccessKey"] : _configuration["tbcSPP:Default"],
                    "wotlk" => isEarlyAccess ? _configuration["wotlkSPP:EarlyAccessKey"] : _configuration["wotlkSPP:Default"],
                    "cata" => isEarlyAccess ? _configuration["cataSPP:EarlyAccessKey"] : _configuration["cataSPP:Default"],
                    "mop" => isEarlyAccess ? _configuration["mopSPP:EarlyAccessKey"] : _configuration["mopSPP:Default"],
                    "trion" => isEarlyAccess ? _configuration["trion:EarlyAccessKey"] : _configuration["trion:Default"],
                    "database" => isEarlyAccess ? _configuration["database:EarlyAccessKey"] : _configuration["database:Default"],
                    _ => null
                };

                // Log the repack location
                TrionLogger.Log($"Repack Location for Emulator '{Emulator}': {RepackLocation}", "INFO");

                // If repack location is not found, return a bad request
                if (string.IsNullOrEmpty(RepackLocation))
                {
                    TrionLogger.Log("Invalid emulator type.", "ERROR");
                    return BadRequest("Invalid emulator type.");
                }

                // Fetch file list from the specified location
                TrionLogger.Log($"Fetching files from location: {RepackLocation}", "INFO");
                files = await FileManager.GetFilesAsync(RepackLocation!);

                // Log the number of files fetched
                TrionLogger.Log($"Fetched {files.Count} files.", "INFO");

                // Cache the file list for 1 hour
                var fileCacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                _cache.Set(fileCacheKey, files, fileCacheOptions);

                // Return the list of files
                return Ok(new { Files = files });
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log and return access denied error
                TrionLogger.Log($"Access denied: {ex.Message}", "ERROR");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log and return general error
                TrionLogger.Log($"Error: {ex.Message}", "ERROR");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to download a file
        [HttpPost("DownloadFile")]
        public IActionResult DownloadFile([FromBody] FileRequest request)
        {
            try
            {
                TrionLogger.Log("DownloadFile called.", "INFO");

                // Validate the request and file path
                if (request == null || string.IsNullOrEmpty(request.FilePath) || !System.IO.File.Exists(request.FilePath))
                {
                    TrionLogger.Log("Invalid or missing file path.", "ERROR");
                    return BadRequest("Invalid or missing file path.");
                }

                // Get the file name from the path
                string fileName = Path.GetFileName(request.FilePath);

                // Read the file content
                byte[] fileBytes = System.IO.File.ReadAllBytes(request.FilePath);

                // Return the file as a downloadable response
                TrionLogger.Log($"Returning file: {fileName}", "INFO");
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (UnauthorizedAccessException ex)
            {
                TrionLogger.Log($"Access denied: {ex.Message}", "ERROR");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}", "ERROR");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to get the version of different repacks
        [HttpGet("GetFileVersion")]
        public async Task<IActionResult> GetRepackVersion([FromQuery] string Key)
        {
            try
            {
                TrionLogger.Log($"GetFileVersion called with Key: {Key}", "INFO");

                // Check if key is verified for early access
                bool isEarlyAccess = !string.IsNullOrEmpty(Key) && await _databaseManager.GetKeyVerified(Key);

                // Function to get version for a given repack
                string GetVersion(string keyBase) =>
                    FileManager.GetVersion(_configuration[$"{keyBase}:Version:{(isEarlyAccess ? "EarlyAccess" : "Default")}"]!);

                // Get versions for different repacks
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

                TrionLogger.Log("Returning versions.", "INFO");

                // Return the versions
                return Ok(versions);
            }
            catch (UnauthorizedAccessException ex)
            {
                TrionLogger.Log($"Access denied: {ex.Message}", "ERROR");
                return StatusCode(403, $"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}", "ERROR");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // Endpoint to check website ping (returns an empty OK response)
        [HttpGet("GetWebsitePing")]
        public IActionResult GetWebsitePing()
        {
            TrionLogger.Log("GetWebsitePing called.", "INFO");
            return Ok();
        }

        // Endpoint to perform a download speed test
        [HttpGet("DownloadSpeed")]
        public IActionResult DownloadSpeedTest()
        {
            TrionLogger.Log("DownloadSpeedTest called.", "INFO");
            return new FileStreamResult(Network.GenerateRandomStream(), "application/octet-stream")
            {
                FileDownloadName = "speedtest.bin"
            };
        }
    }
}
