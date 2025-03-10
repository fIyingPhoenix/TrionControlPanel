using TrionControlPanel.Desktop.Extensions.Modules;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace TrionControlPanelDesktop.Extensions.Modules
{
    public class Links
    {
        public static string MainHost { get => "https://api.aclab.tech"; }
        public static string BackupHost { get => "http://localhost:5000"; }
        public static string APIServer { get; set; }
        public static string WebServer { get => "https://flying-phoenix.dev/"; }
        public static string Support { get => "https://flying-phoenix.dev/support.php"; }
        public static string Discord { get => "https://discord.gg/By4nkETRXS"; }
        public class APIRequests
        {
            public static string DownlaodFiles()
            {
                var url = APIServer;
                return $"{url}/Trion/DownloadFile";
            }
            public static string GetServerFiles(string Emulator, string key)
            {
                var url = APIServer;
                return $"{url}/Trion/GetServerFiles?Emulator={Emulator}&Key={key}";
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
        public static string DDNSWebsits(Enums.DDNSerivce DDNSerivce)
        {
            return DDNSerivce switch
            {
                Enums.DDNSerivce.DuckDNS => $"https://www.duckdns.org/",
                Enums.DDNSerivce.NoIP => $"https://www.noip.com/sign-up",
                Enums.DDNSerivce.Dynu => $"https://www.dynu.com",
                Enums.DDNSerivce.Enom => $"https://www.enom.com/",
                Enums.DDNSerivce.AllInkl => $"https://all-inkl.com/",
                Enums.DDNSerivce.dynDNS => $"https://account.dyn.com/",
                Enums.DDNSerivce.STRATO => $"https://www.strato.de/",
                Enums.DDNSerivce.Freemyip => $"https://freemyip.com/",
                Enums.DDNSerivce.Afraid => $"https://freedns.afraid.org/",
                Enums.DDNSerivce.OVH => $"https://www.ovhcloud.com/",
                Enums.DDNSerivce.Cloudflare => $"https://www.cloudflare.com/",
                _ => "",
            };
        }
    }

}
