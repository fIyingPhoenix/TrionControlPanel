
using System.DirectoryServices.ActiveDirectory;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    public class DDNSUpdate
    {
        public static string DDNSUpdateURL(AppSettings Settings)
        {
            return Settings.DDNSerivce switch
            {
                Enums.DDNSerivce.DuckDNS => $"http://www.duckdns.org/update?domains={Settings.DDNSDomain}&token={Settings.DDNSPassword}&ip={Settings.IPAddress}",
                Enums.DDNSerivce.NoIP => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@dynupdate.no-ip.com/nic/update?hostname={Settings.DDNSDomain}&myip={""}",
                Enums.DDNSerivce.Dynu => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@members.dyndns.org/v3/update?hostname={Settings.DDNSDomain}&myip={""}",
                Enums.DDNSerivce.Enom => $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={Settings.DDNSDomain}&Zone={Settings.DDNSUsername}&DomainPassword={Settings.DDNSPassword}&Address={""}",
                Enums.DDNSerivce.AllInkl => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@dyndns.kasserver.com/?myip={""}",
                Enums.DDNSerivce.dynDNS => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@update.dyndns.it/nic/update?hostname={Settings.DDNSDomain}",
                Enums.DDNSerivce.STRATO => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@dyndns.strato.com/nic/update?hostname={Settings.DDNSDomain}&myip={""}",
                Enums.DDNSerivce.Freemyip => $"http://freemyip.com/update?domain={Settings.DDNSDomain}&token={Settings.DDNSUsername}&myip={""}",
                Enums.DDNSerivce.Afraid => $"http://sync.afraid.org/u/{Settings.DDNSUsername}/",
                Enums.DDNSerivce.OVH => $"http://{Settings.DDNSUsername}:{Settings.DDNSPassword}@www.ovh.com/nic/update?system=dyndns&hostname={Settings.DDNSDomain}&myip={""}",
                Enums.DDNSerivce.Cloudflare => $"https://api.cloudflare.com/client/v4/zones/{Settings.DDNSUsername}/dns_records/{Settings.DDNSPassword}",
                _ => "",
            };
        }
    }
}
