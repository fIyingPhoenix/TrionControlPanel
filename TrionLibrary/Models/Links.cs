using System.IO;
using TrionLibrary.Models;
using TrionLibrary.Setting;
namespace TrionControlPanelDesktop.Data
{
    public class Links
    {
        public static string MainCDNHost { get => "https://cdn1.flying-phoenix.dev/"; }
        public static string BackupCDNHost { get => "https://cdn.aclab.tech/"; }
        public static string WebServer { get => "https://flying-phoenix.dev/"; }
        public static string APIServer { get => "https://cdn1.flying-phoenix.dev/"; }
        public static string Support { get => "https://flying-phoenix.dev/support.php"; }
        public class Emulators
        {
            public static string AscEmu { get => "https://github.com/AscEmu/"; }
            public static string AzerothCore { get => "https://github.com/AzerothCore/"; }
            public static string CMaNGOS  { get => "https://github.com/cmangos/"; }
            public static string CypherCore { get => "https://github.com/CypherCore/"; }
            public static string FirelandsCore{ get => "https://github.com/FirelandsProject"; }
            public static string TrinityCore { get => "https://github.com/trinityCore/"; }
            public static string VMaNGOS { get => "https://github.com/vmangos/"; }
            public static string SkyFire { get => "https://codeberg.org/ProjectSkyfire/"; }
        }
        public class Version
        {
            public static string Trion { get => "version/trion.ver"; }
            public static string Database { get => "version/database.ver"; }
            public static string Classic { get => "version/classic.ver"; }
            public static string TBC { get => "version/tbc.ver"; }
            public static string WotLK { get => "version/wotlk.ver"; }
            public static string Cata { get => "version/cata.ver"; }
            public static string Mop { get => "version/mop.ver"; }
        }
        public class Hashe
        {
            public static string Trion { get => "data/trionHashes.xml"; }
            public static string Database { get => "data/databaseHashes.xml"; }
            public static string Classic { get => "data/classicHashes.xml"; }
            public static string TBC { get => "data/tbcHashes.xml"; }
            public static string WotLK { get => "data/wotlkHashes.xml"; }
            public static string Cata { get => "data/cataHashes.xml"; }
            public static string Mop { get => "data/mopHashes.xml"; }
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
        public static string Discord { get => "https://discord.gg/By4nkETRXS"; }
        public static string DDNSWebsits()
        {
            return Setting.List.DDNSerivce switch
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
