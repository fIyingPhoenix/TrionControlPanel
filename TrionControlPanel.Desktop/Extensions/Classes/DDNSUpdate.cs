using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using System.Collections.Generic;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    public class DDNSUpdate
    {
        // Dictionary to hold the DDNS service URL patterns
        private static readonly Dictionary<Enums.DDNSService, string> DdnsServiceUrls = new()
        {
            { Enums.DDNSService.DuckDNS, "http://www.duckdns.org/update?domains={0}&token={1}&ip={2}" },
            { Enums.DDNSService.NoIP, "http://{0}:{1}@dynupdate.no-ip.com/nic/update?hostname={2}&myip={3}" },
            { Enums.DDNSService.Dynu, "http://{0}:{1}@members.dyndns.org/v3/update?hostname={2}&myip={3}" },
            { Enums.DDNSService.Enom, "http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={2}&Zone={0}&DomainPassword={1}&Address={3}" },
            { Enums.DDNSService.AllInkl, "http://{0}:{1}@dyndns.kasserver.com/?myip={2}" },
            { Enums.DDNSService.DynDNS, "http://{0}:{1}@update.dyndns.it/nic/update?hostname={2}" },
            { Enums.DDNSService.STRATO, "http://{0}:{1}@dyndns.strato.com/nic/update?hostname={2}&myip={3}" },
            { Enums.DDNSService.Freemyip, "http://freemyip.com/update?domain={2}&token={0}&myip={3}" },
            { Enums.DDNSService.Afraid, "http://sync.afraid.org/u/{0}/" },
            { Enums.DDNSService.OVH, "http://{0}:{1}@www.ovh.com/nic/update?system=dyndns&hostname={2}&myip={3}" },
            { Enums.DDNSService.Cloudflare, "https://api.cloudflare.com/client/v4/zones/{0}/dns_records/{1}" }
        };

        // Generate DDNS update URL based on the settings
        public static string DDNSUpdateURL(AppSettings Settings)
        {
            try
            {
                // Log the incoming settings for DDNS service
                TrionLogger.Log($"DDNSUpdateURL invoked with DDNSService: {Settings.DDNSerivce}", "INFO");

                // Try to find the URL pattern from the dictionary
                if (DdnsServiceUrls.TryGetValue(Settings.DDNSerivce, out string urlPattern))
                {
                    // If the URL pattern is found, format it with the appropriate settings
                    string url = string.Format(urlPattern, Settings.DDNSUsername, Settings.DDNSPassword, Settings.DDNSDomain, Settings.IPAddress ?? string.Empty);

                    // Log the final URL generated
                    TrionLogger.Log($"DDNSUpdateURL generated: {url}", "INFO");
                    return url;
                }
                else
                {
                    // If the service is not found, log the issue and return empty
                    TrionLogger.Log($"DDNSUpdateURL: No matching service found for {Settings.DDNSerivce}", "ERROR");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during URL generation
                TrionLogger.Log($"Error in DDNSUpdateURL: {ex.Message}", "ERROR");
                return string.Empty; // Return empty string in case of error
            }
        }
    }
}
