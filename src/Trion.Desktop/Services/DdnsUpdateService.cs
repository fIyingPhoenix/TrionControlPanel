using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Trion.Desktop.Infrastructure.Constants;

namespace Trion.Desktop.Services;

/// <summary>
/// Sends Dynamic DNS IP-update requests to all supported providers.
/// URL templates live in <see cref="DdnsProviderUrls"/>; edit there when endpoints change.
/// </summary>
public sealed class DdnsUpdateService : IDdnsUpdateService
{
    private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(15) };

    static DdnsUpdateService()
    {
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("TrionControlPanel/1.0");
    }

    public async Task<DdnsResult> UpdateAsync(
        string            service,
        string            domain,
        string            username,
        string            password,
        string            ip,
        CancellationToken ct = default)
    {
        try
        {
            return service == "Cloudflare"
                ? await UpdateCloudflareAsync(domain, username, password, ip, ct)
                : await UpdateStandardAsync(service, domain, username, password, ip, ct);
        }
        catch (Exception ex)
        {
            return new DdnsResult(false, ex.Message);
        }
    }

    // ── Standard GET-based providers ─────────────────────────────────────────

    private async Task<DdnsResult> UpdateStandardAsync(
        string service, string domain, string username, string password, string ip,
        CancellationToken ct)
    {
        string url = BuildUrl(service, domain, username, password, ip);
        using var response = await _http.GetAsync(url, ct);
        string body = (await response.Content.ReadAsStringAsync(ct)).Trim();
        return new DdnsResult(response.IsSuccessStatusCode, body);
    }

    // ── Cloudflare REST API (PUT + JSON) ──────────────────────────────────────
    // username = Zone ID  |  password = API Token (Bearer)
    //
    // Step 1 — GET list of A records to resolve the record ID by domain name.
    // Step 2 — PUT the updated IP to that specific record.

    private async Task<DdnsResult> UpdateCloudflareAsync(
        string domain, string zoneId, string apiToken, string ip,
        CancellationToken ct)
    {
        // ── Step 1: resolve the record ID ────────────────────────────────────
        using var listReq = new HttpRequestMessage(
            HttpMethod.Get,
            DdnsProviderUrls.CloudflareListRecords(zoneId, domain));
        listReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

        using var listRes = await _http.SendAsync(listReq, ct);
        string listJson = await listRes.Content.ReadAsStringAsync(ct);

        using var listDoc = JsonDocument.Parse(listJson);
        var listRoot = listDoc.RootElement;

        if (!listRoot.TryGetProperty("success", out var listOk) || !listOk.GetBoolean())
            return new DdnsResult(false, "Cloudflare: API returned failure while listing records.");

        var records = listRoot.GetProperty("result");
        if (records.GetArrayLength() == 0)
            return new DdnsResult(false, $"Cloudflare: no A record found for '{domain}'.");

        string recordId = records[0].GetProperty("id").GetString()!;

        // ── Step 2: update the record ─────────────────────────────────────────
        string body = JsonSerializer.Serialize(new
        {
            type    = "A",
            name    = domain,
            content = ip,
            ttl     = 1,
            proxied = false
        });

        using var putReq = new HttpRequestMessage(
            HttpMethod.Put,
            DdnsProviderUrls.CloudflareUpdateRecord(zoneId, recordId));
        putReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
        putReq.Content = new StringContent(body, Encoding.UTF8, "application/json");

        using var putRes = await _http.SendAsync(putReq, ct);
        string putJson = await putRes.Content.ReadAsStringAsync(ct);

        using var putDoc = JsonDocument.Parse(putJson);
        bool success = putDoc.RootElement.TryGetProperty("success", out var putOk) && putOk.GetBoolean();
        return new DdnsResult(success,
            success ? $"Record updated → {ip}" : "Cloudflare: update request failed.");
    }

    // ── URL dispatch for GET-based providers ──────────────────────────────────

    private static string BuildUrl(
        string service, string domain, string username, string password, string ip) =>
        service switch
        {
            "Afraid"   => DdnsProviderUrls.Afraid (username),
            "AllInkl"  => DdnsProviderUrls.AllInkl(username, password, ip),
            "DuckDNS"  => DdnsProviderUrls.DuckDns(domain, password, ip),
            "DynDNS"   => DdnsProviderUrls.DynDns(username, password, domain, ip),
            "Dynu"     => DdnsProviderUrls.Dynu(username, password, domain, ip),
            "Enom"     => DdnsProviderUrls.Enom(domain, username, password, ip),
            "Freemyip" => DdnsProviderUrls.Freemyip(domain, username, ip),
            "NoIP"     => DdnsProviderUrls.NoIp(username, password, domain, ip),
            "OVH"      => DdnsProviderUrls.Ovh(username, password, domain, ip),
            "STRATO"   => DdnsProviderUrls.Strato(username, password, domain, ip),
            _          => throw new NotSupportedException($"Provider '{service}' is not supported."),
        };
}
