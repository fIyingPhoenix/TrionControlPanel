using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TrionLibrary;

namespace TrionDatabase
{
    public static class NetworkHelper
    {
        public static async Task<string> GetExternalIpAddress()
        {
            string externalIpAddress = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://checkip.amazonaws.com/");
                    if (response.IsSuccessStatusCode)
                    {
                        externalIpAddress = await response.Content.ReadAsStringAsync();
                        externalIpAddress = externalIpAddress.Trim();
                    }
                    else
                    {
                        Data.Message = "Failed to retrieve IP address. Status code: " + response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.Message =  "Error getting external IP address: " + ex.Message;
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
                Data.Message = "Error getting IPv4 addresses: " + ex.Message;
                return internalIpAddress;
            }
        }
    }
}
