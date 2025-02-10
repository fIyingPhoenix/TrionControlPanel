using Microsoft.VisualBasic.Logging;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    public class NetworkManager
    {
        public static bool IsDomainName(string input)
        {
            // Regular expression pattern to match a domain name
            string pattern = @"^(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.[A-Za-z]{2,6}(\.[A-Za-z]{2,6})?$";
            Regex regex = new(pattern, RegexOptions.Compiled);

            return regex.IsMatch(input);
        }
        public static async Task<bool> IsPortOpen(int Port, string Host)
        {
            try
            {
                using TcpClient tcpClient = new();
                await tcpClient.ConnectAsync(Host, Port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<string> GetExternalIpAddress(string url)
        {
            try
            {
                using (HttpClient client = new())
                {
                    await TrionLogger.Log($"Getting Exter ipv4 address from {url}");
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();
                        await TrionLogger.Log($"Loaded Extern ipv4 address: {result.iPv4Address}");
                        return result.iPv4Address;
                    }
                    else
                    {
                        await TrionLogger.Log($"Error fetching IP {response.StatusCode} ,Url {url}", "ERROR");
                        return "0.0.0.0";
                    }

                }
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Error fetching IP Message {ex.Message},Url {url}", "ERROR");
                return "0.0.0.0";
            }
        }
        public static async Task<string> GetInternalIpAddress()
        {

            try
            {
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Filter for active physical network interfaces and exclude virtual ones
                    if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                        networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ppp &&
                        !networkInterface.Description.ToLower().Contains("virtual") &&
                        !networkInterface.Description.ToLower().Contains("vmware") &&
                        !networkInterface.Description.ToLower().Contains("hyper-v"))
                    {
                        foreach (UnicastIPAddressInformation ipAddressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (ipAddressInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                await TrionLogger.Log($"Loaded Intern ipv4 address {ipAddressInfo.Address}");
                                return ipAddressInfo.Address.ToString();
                            }
                        }
                    }
                }
                await TrionLogger.Log($"No active physical IPv4 address found!", "ERROR");
                return "0.0.0.0";
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"No active physical IPv4 address found! {ex.Message}", "ERROR");
                return "0.0.0.0";
            }
        }
        public static async Task<bool> IsWebsiteOnlineAsync(string url)
        {
            await TrionLogger.Log($"Website: {url} Ckecking");
            try
            {
                using HttpClient httpClient = new();
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (HttpRequestException)
            {
                // Handle web exceptions (e.g., network issues)
                await TrionLogger.Log($"Website: {url} Handle web exceptions");
                return false;
            }
            catch (TaskCanceledException)
            {
                // Handle timeout exception
                await TrionLogger.Log($"Website: {url} Timeout");
                return false;
            }
        }
        public static async Task<bool> UpdateDNSIP(AppSettings Settings)
        {
            if (!string.IsNullOrEmpty(Settings.DDNSDomain) && !string.IsNullOrEmpty(Settings.IPAddress))
            {
                try
                {
                    // Create a request for the URL
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Links.DDNSWebsits(Settings.DDNSerivce));
                    request.Method = "GET";

                    // Get the response
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // Check the status code
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            // Request succeeded
                            await TrionLogger.Log($"DNS update request succeeded! IP{Settings.IPAddress}, Domain: {Settings.DDNSDomain}");
                            return true;
                        }
                        else
                        {
                            // Request failed
                            await TrionLogger.Log($"Response status code: {response.StatusCode}", "ERROR");
                            return false;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    // Handle web exceptions, e.g., 404, 500, etc.
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        await TrionLogger.Log($"Request failed with status code: {errorResponse.StatusCode}", "ERROR");
                    }
                    else
                    {
                        await TrionLogger.Log($"Request failed: {webEx.Message}", "ERROR");
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    await TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
                    return false;
                }
            }
            return false;
        }
    }
}
