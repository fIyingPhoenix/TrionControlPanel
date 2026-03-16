using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public sealed class NetworkService : INetworkService
{
    private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(10) };

    // ── Internal IPs ─────────────────────────────────────────────────────────

    /// <inheritdoc />
    public IReadOnlyList<string> GetInternalIpv4Addresses()
    {
        var results = new List<string>();
        try
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus != OperationalStatus.Up)
                    continue;
                if (nic.NetworkInterfaceType is NetworkInterfaceType.Loopback
                                             or NetworkInterfaceType.Tunnel
                                             or NetworkInterfaceType.Ppp)
                    continue;

                var desc = nic.Description.ToLowerInvariant();
                if (desc.Contains("virtual") || desc.Contains("vmware") || desc.Contains("hyper-v"))
                    continue;

                foreach (var ip in nic.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        results.Add(ip.Address.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[NetworkService] GetInternalIpv4Addresses: {ex.Message}");
        }

        return results.Count > 0 ? results : ["0.0.0.0"];
    }

    // ── Public IP ─────────────────────────────────────────────────────────────

    /// <inheritdoc />
    public async Task<string> GetPublicIpv4Async(CancellationToken ct = default)
    {
        // Primary: Trion API (returns JSON { "iPv4Address": "..." })
        try
        {
            var json = await _http.GetStringAsync(AppLinks.GetExternalIpv4Url, ct).ConfigureAwait(false);
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("iPv4Address", out var prop))
                return prop.GetString() ?? "0.0.0.0";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[NetworkService] Trion API failed: {ex.Message}");
        }

        // Fallback: ipify (returns plain text)
        try
        {
            return (await _http.GetStringAsync(AppLinks.PublicIpUrl, ct).ConfigureAwait(false)).Trim();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[NetworkService] ipify fallback failed: {ex.Message}");
        }

        return "0.0.0.0";
    }
}
