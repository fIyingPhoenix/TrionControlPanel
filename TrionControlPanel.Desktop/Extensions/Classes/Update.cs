
using TrionControlPanel.Desktop.Extensions.Modules;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    public class Update
    {
        public static string DDNSUpdateURL(Enums.DDNSerivce DnsService, string Domain, string Username, string Password)
        {
            return DnsService switch
            {
                Enums.DDNSerivce.DuckDNS => $"http://www.duckdns.org/update?domains={Domain}&token={Password}&ip={""}",
                Enums.DDNSerivce.NoIP => $"http://{Username}:{Password}@dynupdate.no-ip.com/nic/update?hostname={Domain}&myip={""}",
                Enums.DDNSerivce.Dynu => $"http://{Username}:{Password}@members.dyndns.org/v3/update?hostname={Domain}&myip={""}",
                Enums.DDNSerivce.Enom => $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={Domain}&Zone={Username}&DomainPassword={Password}&Address={""}",
                Enums.DDNSerivce.AllInkl => $"http://{Username}:{Password}@dyndns.kasserver.com/?myip={""}",
                Enums.DDNSerivce.dynDNS => $"http://{Username}:{Password}@update.dyndns.it/nic/update?hostname={Domain}",
                Enums.DDNSerivce.STRATO => $"http://{Username}:{Password}@dyndns.strato.com/nic/update?hostname={Domain}&myip={""}",
                Enums.DDNSerivce.Freemyip => $"http://freemyip.com/update?domain={Domain}&token={Username}&myip={""}",
                Enums.DDNSerivce.Afraid => $"http://sync.afraid.org/u/{Username}/",
                Enums.DDNSerivce.OVH => $"http://{Username}:{Password}@www.ovh.com/nic/update?system=dyndns&hostname={Domain}&myip={""}",
                Enums.DDNSerivce.Cloudflare => $"https://api.cloudflare.com/client/v4/zones/{Username}/dns_records/{Password}",
                _ => "",
            };
        }
    }
}
