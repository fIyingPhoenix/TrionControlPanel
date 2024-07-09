using TrionLibrary;

namespace TrionControlPanelDesktop.FormData
{
    public class WebLinks
    {
        public static string MainHost { get => "http://localhost/"; }
        public class Version
        {
            public static string Trion { get => ""; }
            public static string Database { get => ""; }
            public static string Classic { get => ""; }
            public static string TBC { get => ""; }
            public static string WotLK { get => ""; }
            public static string Cata { get => ""; }
            public static string Mop { get => ""; }
        }
        public class Update
        {
            public static string Trion { get => ""; }
            public static string Database { get => ""; }
            public static string Classic { get => ""; }
            public static string TBC { get => ""; }
            public static string WotLK { get => ""; }
            public static string Cata { get => ""; }
            public static string Mop { get => ""; }
        }
        public class Files
        {
            public static string Trion { get => ""; }
            public static string Database { get => ""; }
            public static string Classic { get => ""; }
            public static string TBC { get => ""; }
            public static string WotLK { get => ""; }
            public static string Cata { get => ""; }
            public static string Mop { get => ""; }

        }
        public static string Discord { get => "https://discord.gg/By4nkETRXS"; }
        public static string DDNSWebsits()
        {
            switch (Data.Settings.DDNSerivce)
            {
                case EnumModels.DDNSerivce.DuckDNS:
                    return $"https://www.duckdns.org/";
                case EnumModels.DDNSerivce.NoIP:
                    return $"https://www.noip.com/sign-up";
                case EnumModels.DDNSerivce.Dynu:
                    return $"https://www.dynu.com";
                case EnumModels.DDNSerivce.Enom:
                    return $"https://www.enom.com/";
                case EnumModels.DDNSerivce.AllInkl:
                    return $"https://all-inkl.com/";
                case EnumModels.DDNSerivce.dynDNS:
                    return $"https://account.dyn.com/";
                case EnumModels.DDNSerivce.STRATO:
                    return $"https://www.strato.de/";
                case EnumModels.DDNSerivce.Freemyip:
                    return $"https://freemyip.com/";
                case EnumModels.DDNSerivce.Afraid:
                    return $"https://freedns.afraid.org/";
                case EnumModels.DDNSerivce.OVH:
                    return $"https://www.ovhcloud.com/";
                case EnumModels.DDNSerivce.Cloudflare:
                    return $"https://www.cloudflare.com/";
                default:
                    return "";
            }
        }
    }
    
}
