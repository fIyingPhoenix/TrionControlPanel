
using System.DirectoryServices.ActiveDirectory;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    // DDNSUpdate class for generating DDNS update URLs based on settings.
    public class DDNSUpdate
    {
        // Generates the DDNS update URL based on the provided settings.
        public static string DDNSUpdateURL(AppSettings Settings)
        {
            // Validate settings parameters
            if (string.IsNullOrEmpty(Settings.DDNSDomain) || string.IsNullOrEmpty(Settings.DDNSPassword) || string.IsNullOrEmpty(Settings.DDNSUsername))
            {
                throw new ArgumentException("DDNS settings parameters cannot be null or empty.");
            }

            // URL-encode parameters
            string domain = Uri.EscapeDataString(Settings.DDNSDomain);
            string username = Uri.EscapeDataString(Settings.DDNSUsername);
            string password = Uri.EscapeDataString(Settings.DDNSPassword);
            string ipAddress = Uri.EscapeDataString(Settings.IPAddress ?? "");

            return Settings.DDNSerivce switch
            {
                Enums.DDNSerivce.DuckDNS => $"http://www.duckdns.org/update?domains={domain}&token={password}&ip={ipAddress}",
                Enums.DDNSerivce.NoIP => $"http://{username}:{password}@dynupdate.no-ip.com/nic/update?hostname={domain}&myip={ipAddress}",
                Enums.DDNSerivce.Dynu => $"http://{username}:{password}@members.dyndns.org/v3/update?hostname={domain}&myip={ipAddress}",
                Enums.DDNSerivce.Enom => $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={domain}&Zone={username}&DomainPassword={password}&Address={ipAddress}",
                Enums.DDNSerivce.AllInkl => $"http://{username}:{password}@dyndns.kasserver.com/?myip={ipAddress}",
                Enums.DDNSerivce.dynDNS => $"http://{username}:{password}@update.dyndns.it/nic/update?hostname={domain}",
                Enums.DDNSerivce.STRATO => $"http://{username}:{password}@dyndns.strato.com/nic/update?hostname={domain}&myip={ipAddress}",
                Enums.DDNSerivce.Freemyip => $"http://freemyip.com/update?domain={domain}&token={username}&myip={ipAddress}",
                Enums.DDNSerivce.Afraid => $"http://sync.afraid.org/u/{username}/",
                Enums.DDNSerivce.OVH => $"http://{username}:{password}@www.ovh.com/nic/update?system=dyndns&hostname={domain}&myip={ipAddress}",
                Enums.DDNSerivce.Cloudflare => $"https://api.cloudflare.com/client/v4/zones/{username}/dns_records/{password}",
                _ => throw new NotSupportedException($"The DDNS service '{Settings.DDNSerivce}' is not supported."),
            };
        }
    }
}
