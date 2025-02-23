using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
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

                await TrionLogger.Log($"Downloaded {totalBytesRead / (1024.0 * 1024.0):F2} MB in {elapsedSeconds:F2} seconds.");
                await TrionLogger.Log($"Estimated Download Speed: {speedMbps:F2} Mbps");
            }
            catch (OperationCanceledException)
            {
                await TrionLogger.Log("Speed test stopped after 4 seconds.");
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Error: {ex.Message}", "ERROR");
            }
        }
        public static async Task<List<FileList>> GetServerFiles(string URL, IProgress<string>? progress = null)
        {
            try
            {
                HttpClient _httpClient = new();
                _httpClient.Timeout = TimeSpan.FromMinutes(5); // Set a longer timeout for larger responses

                progress?.Report("Requesting Server Files...");
                await TrionLogger.Log($"Get List from: {URL}");

                // Retry mechanism
                int retryCount = 0;
                int maxRetries = 5; // Number of retries before failing
                TimeSpan delayBetweenRetries = TimeSpan.FromSeconds(15); // Delay between retries

                while (retryCount < maxRetries)
                {
                    using (var response = await _httpClient.GetAsync(URL, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            progress?.Report("Downloading file list...");
                            await TrionLogger.Log($"Received response. Status: {response.StatusCode}");

                            // Ensure the entire response stream is consumed
                            await using var stream = await response.Content.ReadAsStreamAsync();

                            progress?.Report("Processing data...");
                            await TrionLogger.Log("Deserializing response...");

                            var result = await JsonSerializer.DeserializeAsync<FileResponse>(stream);

                            if (result == null || result.Files == null || result.Files.Count == 0)
                            {
                                progress?.Report("No files found.");
                                await TrionLogger.Log("No files found in response.");
                            }
                            else
                            {
                                progress?.Report("File list successfully retrieved.");
                                await TrionLogger.Log($"Retrieved {result.Files.Count} files.");
                            }

                            return result?.Files ?? new List<FileList>();
                        }
                        else
                        {
                            // If the response status is not OK, retry after a delay
                            progress?.Report($"Error: Received {response.StatusCode} from the server. Retrying...");
                            await TrionLogger.Log($"Error: Received {response.StatusCode} from the server. Retrying...");
                            retryCount++;

                            if (retryCount < maxRetries)
                            {
                                await Task.Delay(delayBetweenRetries); // Wait before retrying
                            }
                            else
                            {
                                progress?.Report($"Max retries reached. Unable to fetch files.");
                                await TrionLogger.Log($"Max retries reached. Unable to fetch files.");
                                return new List<FileList>();
                            }
                        }
                    }
                }

                // If all retries fail
                progress?.Report("Unable to retrieve files after retries.");
                return new List<FileList>();
            }
            catch (TimeoutException ex)
            {
                await TrionLogger.Log($"Request timed out: {ex.Message}");
                progress?.Report($"Error: Request timed out.");
                return new List<FileList>();
            }
            catch (Exception ex)
            {
                await TrionLogger.Log($"Error fetching files: {ex.Message}");
                progress?.Report($"Error: {ex.Message}");
                return new List<FileList>();
            }

        }
    }

}
