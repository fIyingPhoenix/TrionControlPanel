// =============================================================================
// DDNSUpdate.cs
// Purpose: Generates DDNS update URLs for various Dynamic DNS service providers
// Used by: MainForm timer for periodic IP address updates
// Step 11 of IMPROVEMENTS.md - Add Inline Comments for Complex Logic
// =============================================================================
//
// Dynamic DNS (DDNS) Overview:
// ----------------------------
// DDNS services allow users with dynamic IP addresses to have a consistent
// domain name that automatically updates when their IP changes. This is
// essential for running game servers on residential internet connections.
//
// Each DDNS provider has a different API format for updating DNS records:
// - Basic Auth: Uses HTTP Basic Authentication (username:password@host)
// - Token-based: Uses a token/key in the URL query string
// - Custom: Provider-specific parameters and endpoints
// =============================================================================

using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;

namespace TrionControlPanelDesktop.Extensions.Classes
{
    /// <summary>
    /// Generates DDNS update URLs for various Dynamic DNS service providers.
    /// </summary>
    /// <remarks>
    /// Supported providers:
    /// - DuckDNS: Token-based, free service
    /// - NoIP: Basic auth, popular choice
    /// - Dynu: Basic auth with DynDNS protocol
    /// - Enom: Custom API format
    /// - All-Inkl: German hosting provider
    /// - DynDNS: Original dynamic DNS service
    /// - STRATO: German hosting provider
    /// - Freemyip: Token-based, free service
    /// - FreeDNS (Afraid): Hash-based authentication
    /// - OVH: French hosting provider
    /// - Cloudflare: Uses REST API (requires separate implementation for PUT request)
    /// </remarks>
    public class DDNSUpdate
    {
        /// <summary>
        /// Generates the DDNS update URL based on the provided settings.
        /// </summary>
        /// <param name="Settings">Application settings containing DDNS configuration.</param>
        /// <returns>A fully formed URL for updating the DNS record.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if required DDNS settings (domain, username, password) are empty.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown if the selected DDNS service is not implemented.
        /// </exception>
        /// <remarks>
        /// The returned URL can be called with a simple HTTP GET request to update
        /// the DNS record. All parameters are URL-encoded to prevent injection attacks.
        ///
        /// Note: Cloudflare requires a PUT request with JSON body - the URL returned
        /// here is just the endpoint. Additional implementation is needed for Cloudflare.
        /// </remarks>
        /// <example>
        /// <code>
        /// string updateUrl = DDNSUpdate.DDNSUpdateURL(settings);
        /// using var client = new HttpClient();
        /// var response = await client.GetAsync(updateUrl);
        /// </code>
        /// </example>
        public static string DDNSUpdateURL(AppSettings Settings)
        {
            // Step 1: Validate required settings
            // All DDNS services require at minimum a domain and some form of authentication
            if (string.IsNullOrEmpty(Settings.DDNSDomain) ||
                string.IsNullOrEmpty(Settings.DDNSPassword) ||
                string.IsNullOrEmpty(Settings.DDNSUsername))
            {
                throw new ArgumentException("DDNS settings parameters cannot be null or empty.");
            }

            // Step 2: URL-encode all parameters to prevent injection attacks
            // This ensures special characters (like & or =) in credentials don't break the URL
            string domain = Uri.EscapeDataString(Settings.DDNSDomain);
            string username = Uri.EscapeDataString(Settings.DDNSUsername);
            string password = Uri.EscapeDataString(Settings.DDNSPassword);
            string ipAddress = Uri.EscapeDataString(Settings.IPAddress ?? "");

            // Step 3: Generate provider-specific URL format
            // Each provider has different API conventions
            return Settings.DDNSService switch
            {
                // DuckDNS: Free service, uses token-based auth in query string
                // Format: domains=subdomain (without .duckdns.org), token=your-token
                Enums.DDNSService.DuckDNS =>
                    $"http://www.duckdns.org/update?domains={domain}&token={password}&ip={ipAddress}",

                // NoIP: Popular paid/free service, uses HTTP Basic Auth
                // The username:password@ syntax is HTTP Basic Authentication
                Enums.DDNSService.NoIP =>
                    $"http://{username}:{password}@dynupdate.no-ip.com/nic/update?hostname={domain}&myip={ipAddress}",

                // Dynu: Uses standard DynDNS protocol with Basic Auth
                Enums.DDNSService.Dynu =>
                    $"http://{username}:{password}@members.dyndns.org/v3/update?hostname={domain}&myip={ipAddress}",

                // Enom: Domain registrar with custom API format
                // Zone parameter is typically the main domain, HostName is the subdomain
                Enums.DDNSService.Enom =>
                    $"http://dynamic.name-services.com/interface.asp?command=SetDnsHost&HostName={domain}&Zone={username}&DomainPassword={password}&Address={ipAddress}",

                // All-Inkl: German hosting provider, Basic Auth without hostname
                // The hostname is configured server-side based on the account
                Enums.DDNSService.AllInkl =>
                    $"http://{username}:{password}@dyndns.kasserver.com/?myip={ipAddress}",

                // DynDNS: The original dynamic DNS service
                // Note: DynDNS classic is deprecated, this uses their current API
                Enums.DDNSService.DynDNS =>
                    $"http://{username}:{password}@update.dyndns.it/nic/update?hostname={domain}",

                // STRATO: German hosting provider, standard DynDNS protocol
                Enums.DDNSService.STRATO =>
                    $"http://{username}:{password}@dyndns.strato.com/nic/update?hostname={domain}&myip={ipAddress}",

                // Freemyip: Free service with token-based auth
                // Token is provided after registering a domain
                Enums.DDNSService.Freemyip =>
                    $"http://freemyip.com/update?domain={domain}&token={username}&myip={ipAddress}",

                // FreeDNS (Afraid.org): Uses a unique hash URL for each subdomain
                // The 'username' field actually contains the update hash
                Enums.DDNSService.Afraid =>
                    $"http://sync.afraid.org/u/{username}/",

                // OVH: French hosting provider, standard DynDNS protocol
                Enums.DDNSService.OVH =>
                    $"http://{username}:{password}@www.ovh.com/nic/update?system=dyndns&hostname={domain}&myip={ipAddress}",

                // Cloudflare: Uses REST API - this returns the endpoint URL only
                // Actual update requires a PUT request with JSON body containing the new IP
                // username = Zone ID, password = DNS Record ID
                Enums.DDNSService.Cloudflare =>
                    $"https://api.cloudflare.com/client/v4/zones/{username}/dns_records/{password}",

                // Unsupported service - throw to alert the developer
                _ => throw new NotSupportedException($"The DDNS service '{Settings.DDNSService}' is not supported."),
            };
        }
    }
}
