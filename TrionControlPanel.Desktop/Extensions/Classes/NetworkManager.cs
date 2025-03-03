using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
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
        public static async Task GetAPIServer()
        {
            if (await IsWebsiteOnlineAsync($"{Links.MainHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.MainHost; }
            if (await IsWebsiteOnlineAsync($"{Links.BackupHost}/Trion/GetWebsitePing")) { Links.APIServer = Links.BackupHost; }
            else { Links.APIServer = Links.MainHost; }
        }
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
                    TrionLogger.Log($"Getting Exter ipv4 address from {url}");
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();
                        TrionLogger.Log($"Loaded Extern ipv4 address: {result.iPv4Address}");
                        return result.iPv4Address;
                    }
                    else
                    {
                        TrionLogger.Log($"Error fetching IP {response.StatusCode} ,Url {url}", "ERROR");
                        return "0.0.0.0";
                    }

                }
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error fetching IP Message {ex.Message},Url {url}", "ERROR");
                return "0.0.0.0";
            }
        }
        public static string GetInternalIpAddress()
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
                                TrionLogger.Log($"Loaded Intern ipv4 address {ipAddressInfo.Address}");
                                return ipAddressInfo.Address.ToString();
                            }
                        }
                    }
                }
                TrionLogger.Log($"No active physical IPv4 address found!", "ERROR");
                return "0.0.0.0";
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"No active physical IPv4 address found! {ex.Message}", "ERROR");
                return "0.0.0.0";
            }
        }
        public static async Task<bool> IsWebsiteOnlineAsync(string url)
        {
            TrionLogger.Log($"Website: {url} Ckecking");
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
                TrionLogger.Log($"Website: {url} Handle web exceptions");
                return false;
            }
            catch (TaskCanceledException)
            {
                // Handle timeout exception
                TrionLogger.Log($"Website: {url} Timeout");
                return false;
            }
        }
        public static bool UpdateDNSIP(AppSettings Settings)
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
                            TrionLogger.Log($"DNS update request succeeded! IP{Settings.IPAddress}, Domain: {Settings.DDNSDomain}");
                            return true;
                        }
                        else
                        {
                            // Request failed
                            TrionLogger.Log($"Response status code: {response.StatusCode}", "ERROR");
                            return false;
                        }
                    }
                }
                catch (WebException webEx)
                {
                    // Handle web exceptions, e.g., 404, 500, etc.
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        TrionLogger.Log($"Request failed with status code: {errorResponse.StatusCode}", "ERROR");
                    }
                    else
                    {
                        TrionLogger.Log($"Request failed: {webEx.Message}", "ERROR");
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    TrionLogger.Log($"An error occurred: {ex.Message}", "ERROR");
                    return false;
                }
            }
            return false;
        }
        public static async void DownlaodSeppd() //work in progress
        {
            string apiUrl = "https://localhost:7107/Trion/DownloadSpeed"; // Replace with your actual server URL

            using HttpClient client = new();
            client.Timeout = TimeSpan.FromMinutes(1); // Ensure enough time

            byte[] buffer = new byte[4 * 1024 * 1024]; // 4 MB buffer
            long totalBytesRead = 0;
            int bytesRead;

            try
            {
                using HttpResponseMessage response = await client.GetAsync(apiUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                Stopwatch stopwatch = Stopwatch.StartNew();

                CancellationTokenSource cts = new();
                cts.CancelAfter(TimeSpan.FromSeconds(4)); // Stop download after 4 seconds

                while (!cts.Token.IsCancellationRequested && (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cts.Token)) > 0)
                {
                    totalBytesRead += bytesRead;
                }

                stopwatch.Stop();
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double speedMbps = (totalBytesRead * 8) / (elapsedSeconds * 1_000_000); // Convert to Mbps

                TrionLogger.Log($"Downloaded {totalBytesRead / (1024.0 * 1024.0):F2} MB in {elapsedSeconds:F2} seconds.");
                TrionLogger.Log($"Estimated Download Speed: {speedMbps:F2} Mbps");
            }
            catch (OperationCanceledException)
            {
                TrionLogger.Log("Speed test stopped after 4 seconds.");
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Error: {ex.Message}", "ERROR");
            }
        }
        public static async Task<List<FileList>> GetServerFiles(string URL, IProgress<string>? Count = null)
        {
            try
            {
                // Use Task.Run to offload work to a background thread
                var task = Task.Run(async () =>
                {
                    using HttpClient httpClient = new();
                    HttpResponseMessage response = await httpClient.GetAsync(URL).ConfigureAwait(false); // Don't capture the UI context here
                    TrionLogger.Log($"Getting data from {URL}, Response code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false); // Don't capture UI context
                        var filesObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                        List<FileList> fileList = new List<FileList>();
                        // Offload the file processing to a background thread using Task.Run
                        await Task.Run(() =>
                        {
                            foreach (var file in filesObject!.files)
                            {
                                FileList fileItem = new()
                                {
                                    Name = file.name,
                                    Size = file.size,
                                    Hash = file.hash,
                                    Path = file.path
                                };

                                // Add the file to the list (no UI interaction here, so it's thread-safe)
                                lock (fileList)
                                {
                                    fileList.Add(fileItem);
                                }

                                // Report progress to the UI thread safely
                                Count?.Report($"{fileList.Count} / {filesObject!.files.Count}");
                            }
                        });

                        return fileList;
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        TrionLogger.Log($"GetServerFiles API Error: {response.StatusCode} - {error}", "ERROR");
                        return null!;
                    }
                });

                // Await the task and return the result
                return await task.ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                TrionLogger.Log($"GetServerFiles Network error: {ex.Message}", "ERROR");
                return null!;
            }
            catch (Exception ex)
            {
                TrionLogger.Log($"Unexpected error: {ex.Message}", "ERROR");
                return null!;
            }
        }
    }
}
