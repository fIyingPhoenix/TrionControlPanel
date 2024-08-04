using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TrionLibrary.Sys;

namespace TrionLibrary.Network
{
    public class Helper
    {
        public static async Task<string> GetExternalIpAddress()
        {
            string externalIpAddress = string.Empty;
            try
            {
                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync("https://checkip.amazonaws.com/");
                    if (response.IsSuccessStatusCode)
                    {
                        externalIpAddress = await response.Content.ReadAsStringAsync();
                        externalIpAddress = externalIpAddress.Trim();
                    }
                    else
                    {
                        Infos.Message = "Failed to retrieve IP address. Status code: " + response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Infos.Message = "Error getting external IP address: " + ex.Message;
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
                            if (ipAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
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
                Infos.Message = "Error getting IPv4 addresses: " + ex.Message;
                return internalIpAddress;
            }
        }
    }
}
