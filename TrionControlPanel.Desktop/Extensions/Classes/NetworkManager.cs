using System.Net.NetworkInformation;
using System.Net.Sockets;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    public class NetworkManager
    {
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
                    string apiUrl = url; // Replace with your actual API URL
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();
                        await TrionLogger.Log($"Loaded Extern ipv4 address {result.IPv4Address}");
                        return result.IPv4Address;

                    }
                    else
                    {
                        await TrionLogger.Log($"Error fetching IP {response.StatusCode}", "ERROR");
                        return "0.0.0.0";
                    }

                }
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Error fetching IP Message {ex.Message}", "ERROR");
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
                                await TrionLogger.Log($"Loaded Intern ipv4 address{ipAddressInfo.Address}");
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
                await TrionLogger.Log($"No active physical IPv4 address found! {ex.Message} ", "ERROR");
                return "0.0.0.0";
            }
        }
        public static async Task<bool> IsWebsiteOnlineAsync(string url)
        {
            try
            {
                using (HttpClient client = new())
                {
                    // Set a timeout to avoid hanging
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Send a GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check if the status code indicates success (200-299)
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException)
            {
                // Handle web exceptions (e.g., network issues)
                return false;
            }
            catch (TaskCanceledException)
            {
                // Handle timeout exception
                return false;
            }
        }

    }
}
