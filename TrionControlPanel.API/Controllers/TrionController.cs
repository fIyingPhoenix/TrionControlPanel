using Microsoft.AspNetCore.Mvc;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;
using TrionControlPanel.API.Classes.Lists;
using Microsoft.Extensions.Configuration;

namespace TrionControlPanel.API.api
{
    [Route("[controller]")]
    [ApiController]
    public class TrionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TrionController(IConfiguration configuration)
        {
            _configuration = configuration;
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
                var RepackLocation = "";
                // Validate if folderPath is provided and exists
                if (string.IsNullOrEmpty(Emulator))
                {
                    return BadRequest("Invalid or missing folder path.");
                }
                if (Emulator == "classic")
                {
                    RepackLocation = !string.IsNullOrEmpty(Key) && DatabaseManager.GetKeyVerified(Key)
                    ? _configuration["classicSPP:EarlyAccessKey"]
                    : _configuration["classicSPP:Default"];
                    List<FileList> files = await FileManager.GetFilesAsync(RepackLocation!);
                    // Create a response with the total count and file details
                    var response = new
                    {
                        files.Count,
                        Files = files
                    };

                    return Ok(response);
                }
                if (Emulator == "tbc")
                {
                    RepackLocation = !string.IsNullOrEmpty(Key) && DatabaseManager.GetKeyVerified(Key)
                    ? _configuration["tbcSPP:EarlyAccessKey"]
                    : _configuration["tbcSPP:Default"];
                    List<FileList> files = await FileManager.GetFilesAsync(RepackLocation!);
                    // Create a response with the total count and file details
                    var response = new
                    {
                        files.Count,
                        Files = files
                    };

                    return Ok(response);
                }
                if (Emulator == "wotlk")
                {
                    RepackLocation = !string.IsNullOrEmpty(Key) && DatabaseManager.GetKeyVerified(Key)
                    ? _configuration["wotlkSPP:EarlyAccessKey"]
                    : _configuration["wotlkSPP:Default"];
                    List<FileList> files = await FileManager.GetFilesAsync(RepackLocation!);
                    // Create a response with the total count and file details
                    var response = new
                    {
                        files.Count,
                        Files = files
                    };

                    return Ok(response);
                }
                if (Emulator == "cata")
                {
                    RepackLocation = !string.IsNullOrEmpty(Key) && DatabaseManager.GetKeyVerified(Key)
                    ? _configuration["cataSPP:EarlyAccessKey"]
                    : _configuration["cataSPP:Default"];
                    List<FileList> files = await FileManager.GetFilesAsync(RepackLocation!);
                    // Create a response with the total count and file details
                    var response = new
                    {
                        files.Count,
                        Files = files
                    };

                    return Ok(response);
                }
                if (Emulator == "mop")
                {
                    RepackLocation = !string.IsNullOrEmpty(Key) && DatabaseManager.GetKeyVerified(Key)
                    ? _configuration["mopSPP:EarlyAccessKey"]
                    : _configuration["mopSPP:Default"];
                    List<FileList> files = await FileManager.GetFilesAsync(RepackLocation!);
                    // Create a response with the total count and file details
                    var response = new
                    {
                        files.Count,
                        Files = files
                    };

                    return Ok(response);
                }
                return StatusCode(400, "BadRequest");
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

        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile([FromQuery] string filePath)
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
        public IActionResult GetRepackVersion()
        {
            try
            {
                var Trion = FileManager.GetVersion("Files/TrionControlPanel/Latest/setup.exe");
                var Database = FileManager.GetVersion("Files/Database/Latest/Database/bin/mysqld.exe");
                var ClassicSPP = FileManager.GetVersion("Files/SPP/Classic/worldserver.exe");
                var TbcSPP = FileManager.GetVersion("Files/SPP/BurningCrusade/worldserver.exe");
                var WotlkSPP = FileManager.GetVersion("Files/SPP/WrathOfTheLichKing/mangosd.exe");
                var CataSPP = FileManager.GetVersion("Files/SPP/Cataclysm/worldserver.exe");
                var MopSPP = FileManager.GetVersion("Files/SPP/MistsOfPandaria/worldserver.exe");
                return Ok(new {
                    ClassicSPP,
                    TbcSPP, 
                    WotlkSPP, 
                    CataSPP, 
                    MopSPP,

                });
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

    }
}