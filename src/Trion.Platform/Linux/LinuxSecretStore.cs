using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Logging;

namespace Trion.Platform.Linux;

[SupportedOSPlatform("linux")]
public sealed class LinuxSecretStore : ISecretStore
{
    private const string FallbackPath = "/var/lib/trion/.secrets";
    private const int    AesKeySize   = 32; // 256-bit
    private const int    NonceSIze    = 12; // AES-GCM standard nonce
    private const int    TagSize      = 16; // AES-GCM authentication tag

    private readonly ILogger       _log;
    private readonly Lazy<byte[]>  _machineKey;

    // In-memory cache so we don't open the file on every read
    private Dictionary<string, string>? _cache;
    private readonly object              _lock = new();

    public LinuxSecretStore(TrionLogger trionLogger)
    {
        _log        = trionLogger.CreateLogger(nameof(LinuxSecretStore));
        _machineKey = new Lazy<byte[]>(DeriveMachineKey);
    }

    public string? GetSecret(string key)
    {
        lock (_lock)
        {
            var store = LoadStore();
            return store.TryGetValue(key, out var v) ? v : null;
        }
    }

    public void SetSecret(string key, string value)
    {
        lock (_lock)
        {
            var store = LoadStore();
            store[key] = value;
            SaveStore(store);
        }
    }

    public void DeleteSecret(string key)
    {
        lock (_lock)
        {
            var store = LoadStore();
            if (store.Remove(key))
                SaveStore(store);
        }
    }

    // ── Machine-key derivation ────────────────────────────────────────────

    private static byte[] DeriveMachineKey()
    {
        // Use /etc/machine-id as the base material — unique per Linux installation
        var machineId = File.Exists("/etc/machine-id")
            ? File.ReadAllText("/etc/machine-id").Trim()
            : Guid.NewGuid().ToString(); // fallback for containers

        var ikmBytes = Encoding.UTF8.GetBytes(machineId);
        // Application-specific salt so keys differ per product
        var salt     = "TrionControlPanel-SecretStore-v1"u8.ToArray();

        // HKDF-SHA256: extract + expand
        var prk = HKDF.Extract(HashAlgorithmName.SHA256, ikmBytes, salt);
        return HKDF.Expand(HashAlgorithmName.SHA256, prk, AesKeySize, "AES-256-GCM-Key"u8.ToArray());
    }

    // ── Encrypted file store ─────────────────────────────────────────────

    private Dictionary<string, string> LoadStore()
    {
        if (_cache is not null) return _cache;

        if (!File.Exists(FallbackPath))
        {
            _log.LogWarning("Linux secret store file not found — starting empty.");
            _cache = [];
            return _cache;
        }

        try
        {
            var blob    = File.ReadAllBytes(FallbackPath);
            var json    = Decrypt(blob);
            _cache = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? [];
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "Failed to load Linux secret store — starting empty.");
            _cache = [];
        }

        return _cache;
    }

    private void SaveStore(Dictionary<string, string> store)
    {
        var json      = System.Text.Json.JsonSerializer.Serialize(store);
        var encrypted = Encrypt(json);

        var dir = Path.GetDirectoryName(FallbackPath)!;
        Directory.CreateDirectory(dir);

        // Write to temp file then rename (atomic on Linux)
        var tmp = FallbackPath + ".tmp";
        File.WriteAllBytes(tmp, encrypted);

        // Mode 0600 — owner read/write only
        File.SetUnixFileMode(tmp,
            UnixFileMode.UserRead | UnixFileMode.UserWrite);

        File.Move(tmp, FallbackPath, overwrite: true);
        _cache = store;
    }

    private byte[] Encrypt(string plaintext)
    {
        var data  = Encoding.UTF8.GetBytes(plaintext);
        var nonce = new byte[NonceSIze];
        var tag   = new byte[TagSize];
        var cipher = new byte[data.Length];

        RandomNumberGenerator.Fill(nonce);

        using var aes = new AesGcm(_machineKey.Value, TagSize);
        aes.Encrypt(nonce, data, cipher, tag);

        // Layout: [nonce (12)] [tag (16)] [ciphertext]
        var result = new byte[NonceSIze + TagSize + cipher.Length];
        nonce.CopyTo(result, 0);
        tag.CopyTo(result, NonceSIze);
        cipher.CopyTo(result, NonceSIze + TagSize);
        return result;
    }

    private string Decrypt(byte[] blob)
    {
        if (blob.Length < NonceSIze + TagSize)
            throw new InvalidDataException("Secret store blob is too short.");

        var nonce  = blob[..NonceSIze];
        var tag    = blob[NonceSIze..(NonceSIze + TagSize)];
        var cipher = blob[(NonceSIze + TagSize)..];
        var plain  = new byte[cipher.Length];

        using var aes = new AesGcm(_machineKey.Value, TagSize);
        aes.Decrypt(nonce, cipher, tag, plain);
        return Encoding.UTF8.GetString(plain);
    }
}
