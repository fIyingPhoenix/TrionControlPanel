using System.Net.NetworkInformation;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace TrionControlPanel.Desktop.Extensions.Classes
{
    public class Network
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
            string externalIpAddress = string.Empty;
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        // Parse the JSON response
                        JObject json = JObject.Parse(responseBody);
                        externalIpAddress = json["ip"]!.ToString();
                    }
                    else
                    {
                       //Message 
                    }
                }
            }
            catch (Exception ex)
            {
                //message
            }
            return externalIpAddress;
        }
        public static string GetInternalIpAddress()
        {
            string internalIpAddress = string.Empty;
            try
            {
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    // Skip loopback and virtual network interfaces
                    if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        !networkInterface.Description.Contains("virtual", StringComparison.InvariantCultureIgnoreCase) && // Exclude virtual adapters
                        networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                        foreach (UnicastIPAddressInformation ipAddress in ipProperties.UnicastAddresses)
                        {
                            if (ipAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                internalIpAddress = ipAddress.Address.ToString();
                            }
                        }
                    }
                }
                return internalIpAddress;
            }
            catch (Exception ex)
            {
                //Message
                return internalIpAddress;
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
