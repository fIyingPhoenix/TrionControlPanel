using TrionControlPanel.Desktop.Extensions.Modules;


namespace TrionControlPanelDesktop.Extensions.Modules
{
    public class Links
    {
        public static string MainHost { get => "https://api.flying-phoenix.dev"; }
        public static string BackupHost { get => "http://localhost:5000"; }
        public static string APIServer { get; set; }
        public static string WebServer { get => "https://flying-phoenix.dev/"; }
        public static string Support { get => "https://flying-phoenix.dev/support.php"; }
        public static string Discord { get => "https://discord.gg/By4nkETRXS"; }
        public class APIRequests
        {
            public static string InstallSPP(string Emulator, string key)
            {
                var url = APIServer;
                return $"{url}/Trion/InstallSPP?Emulator={Emulator}&Key={key}";
            }
            public static string DownlaodFiles(string emulator, string key)
            {
                var url = APIServer;
                return $"{url}/Trion/DownloadFile?Emulator={Uri.EscapeDataString(emulator)}&Key={Uri.EscapeDataString(key)}";
            }
            public static string GetInstallFiles(string Emulator, string key)
            {
                var url = APIServer;
                return $"{url}/Trion/InstallSPP?Emulator={Emulator}&Key={key}";
            }
            public static string GetReapirFiles(string Emulator, string key)
            {
                var url = APIServer;
                return $"{url}/Trion/RepairSPP?Emulator={Emulator}&Key={key}";
            }
            public static string GetSPPVersion(string key)
            {
                var url = APIServer;
                return $"{url}/Trion/GetFileVersion?Key={key}";
            }

            public static string GetExternalIPv4()
            {
                var url = APIServer;
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
            public static string Trion { get => $"{Directory.GetCurrentDirectory()}"; }
            public static string Database { get => $"{Directory.GetCurrentDirectory()}/database"; }
            public static string Classic { get => $"{Directory.GetCurrentDirectory()}/classic"; }
            public static string TBC { get => $"{Directory.GetCurrentDirectory()}/tbc"; }
            public static string WotLK { get => $"{Directory.GetCurrentDirectory()}/wotlk"; }
            public static string Cata { get => $"{Directory.GetCurrentDirectory()}/cata"; }
            public static string Mop { get => $"{Directory.GetCurrentDirectory()}/mop"; }
        }
        /// <summary>
        /// Gets the signup/login website URL for a DDNS service provider.
        /// </summary>
        /// <param name="service">The DDNS service provider.</param>
        /// <returns>The website URL for the service.</returns>
        public static string DDNSWebsites(Enums.DDNSService service)
        {
            return service switch
            {
                Enums.DDNSService.DuckDNS => "https://www.duckdns.org/",
                Enums.DDNSService.NoIP => "https://www.noip.com/sign-up",
                Enums.DDNSService.Dynu => "https://www.dynu.com",
                Enums.DDNSService.Enom => "https://www.enom.com/",
                Enums.DDNSService.AllInkl => "https://all-inkl.com/",
                Enums.DDNSService.DynDNS => "https://account.dyn.com/",
                Enums.DDNSService.STRATO => "https://www.strato.de/",
                Enums.DDNSService.Freemyip => "https://freemyip.com/",
                Enums.DDNSService.Afraid => "https://freedns.afraid.org/",
                Enums.DDNSService.OVH => "https://www.ovhcloud.com/",
                Enums.DDNSService.Cloudflare => "https://www.cloudflare.com/",
                _ => "",
            };
        }
    }

}
