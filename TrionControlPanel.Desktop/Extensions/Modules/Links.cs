using TrionControlPanel.Desktop.Extensions.Modules;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using System;
using System.IO;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanelDesktop.Extensions.Modules
{
    public class Links
    {
        public static string MainHost { get => "https://api.aclab.tech"; }
        public static string BackupHost { get => "http://localhost:5000"; }
        public static string APIServer { get; set; } = "https://api.aclab.tech";  // Set default value
        public static string WebServer { get => "https://flying-phoenix.dev/"; }
        public static string Support { get => "https://flying-phoenix.dev/support.php"; }
        public static string Discord { get => "https://discord.gg/By4nkETRXS"; }

        public class APIRequests
        {
            public static string DownlaodFiles()
            {
                var url = APIServer;
                if (string.IsNullOrEmpty(url)) throw new InvalidOperationException("API server URL is not set.");
                return $"{url}/Trion/DownloadFile";
            }
            public static string GetServerFiles(string Emulator, string key)
            {
                var url = APIServer;
                if (string.IsNullOrEmpty(url)) throw new InvalidOperationException("API server URL is not set.");
                return $"{url}/Trion/GetServerFiles?Emulator={Emulator}&Key={key}";
            }

            public static string GetSPPVersion(string key)
            {
                var url = APIServer;
                if (string.IsNullOrEmpty(url)) throw new InvalidOperationException("API server URL is not set.");
                return $"{url}/Trion/GetFileVersion?Key={key}";
            }

            public static string GetExternalIPv4()
            {
                var url = APIServer;
                if (string.IsNullOrEmpty(url)) throw new InvalidOperationException("API server URL is not set.");
                return $"{url}/Trion/GetExternalIPv4";
            }
        }

        public class Emulators
        {
            public static string AscEmu { get => "https://github.com/AscEmu/"; }
            public static string AzerothCore { get => "https://github.com/AzerothCore/"; }
            public static string CMaNGOS { get => "https://github.com/cmangos/"; }
            public static string CypherCore { get => "https://github.com/CypherCore/"; }
            public static string FirelandsCore { get => "https://github.com/FirelandsProject"; }
            public static string TrinityCore { get => "https://github.com/trinityCore/"; }
            public static string VMaNGOS { get => "https://github.com/vmangos/"; }
            public static string SkyFire { get => "https://codeberg.org/ProjectSkyfire/"; }
        }

        public class Install
        {
            public static string Trion { get => Path.Combine(Directory.GetCurrentDirectory(), "Trion"); }
            public static string Database { get => Path.Combine(Directory.GetCurrentDirectory(), "database"); }
            public static string Classic { get => Path.Combine(Directory.GetCurrentDirectory(), "classic"); }
            public static string TBC { get => Path.Combine(Directory.GetCurrentDirectory(), "tbc"); }
            public static string WotLK { get => Path.Combine(Directory.GetCurrentDirectory(), "wotlk"); }
            public static string Cata { get => Path.Combine(Directory.GetCurrentDirectory(), "cata"); }
            public static string Mop { get => Path.Combine(Directory.GetCurrentDirectory(), "mop"); }
        }

        public static string DDNSWebsits(Enums.DDNSService DDNSerivce)
        {
            // Create a dictionary to map DDNS services to their corresponding URLs.
            var ddnsServices = new Dictionary<Enums.DDNSService, string>
            {
                { Enums.DDNSService.DuckDNS, "https://www.duckdns.org/" },
                { Enums.DDNSService.NoIP, "https://www.noip.com/sign-up" },
                { Enums.DDNSService.Dynu, "https://www.dynu.com" },
                { Enums.DDNSService.Enom, "https://www.enom.com/" },
                { Enums.DDNSService.AllInkl, "https://all-inkl.com/" },
                { Enums.DDNSService.DynDNS, "https://account.dyn.com/" },
                { Enums.DDNSService.STRATO, "https://www.strato.de/" },
                { Enums.DDNSService.Freemyip, "https://freemyip.com/" },
                { Enums.DDNSService.Afraid, "https://freedns.afraid.org/" },
                { Enums.DDNSService.OVH, "https://www.ovhcloud.com/" },
                { Enums.DDNSService.Cloudflare, "https://www.cloudflare.com/" }
            };

            // Try to get the URL for the given DDNSService
            if (ddnsServices.TryGetValue(DDNSerivce, out var url))
            {
                return url;
            }
            else
            {
                // Handle invalid DDNS service case (log or throw exception as needed)
                // Example: Logging the invalid service and returning a default URL or empty string
                TrionLogger.Log($"Invalid DDNS service: {DDNSerivce}");
                return ""; // or return a default URL like "https://www.example.com" 
            }
        }
    }
}
