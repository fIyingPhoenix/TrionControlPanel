using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Trion.Core.Abstractions.Settings;
using Trion.Core.Logging;

namespace Trion.Platform.Windows;

[SupportedOSPlatform("windows")]
public sealed class WindowsSecretStore : ISecretStore
{
    private const string KeyPrefix = "Trion/";

    private readonly ILogger _log;

    public WindowsSecretStore(TrionLogger trionLogger)
    {
        _log = trionLogger.CreateLogger(nameof(WindowsSecretStore));
    }

    public string? GetSecret(string key)
    {
        try
        {
            return CredentialManagerGet(KeyPrefix + key);
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "Credential Manager unavailable for key {Key}, trying fallback.", key);
            return DpapiFileGet(key);
        }
    }

    public void SetSecret(string key, string value)
    {
        try
        {
            CredentialManagerSet(KeyPrefix + key, value);
        }
        catch (Exception ex)
        {
            _log.LogWarning(ex, "Credential Manager unavailable for key {Key}, using fallback.", key);
            DpapiFileSet(key, value);
        }
    }

    public void DeleteSecret(string key)
    {
        try { CredentialManagerDelete(KeyPrefix + key); }
        catch { /* best effort */ }

        var fallbackPath = GetFallbackPath(key);
        if (File.Exists(fallbackPath))
            File.Delete(fallbackPath);
    }

    // ── Windows Credential Manager (P/Invoke) ─────────────────────────────

    [DllImport("advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CredRead(string target, uint type, int flags, out IntPtr credential);

    [DllImport("advapi32.dll", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CredWrite(ref CREDENTIAL credential, uint flags);

    [DllImport("advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CredDelete(string target, uint type, int flags);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool CredFree(IntPtr buffer);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CREDENTIAL
    {
        public uint    Flags;
        public uint    Type;           // CRED_TYPE_GENERIC = 1
        public string  TargetName;
        public string? Comment;
        public long    LastWritten;
        public uint    CredentialBlobSize;
        public IntPtr  CredentialBlob;
        public uint    Persist;        // CRED_PERSIST_LOCAL_MACHINE = 2
        public uint    AttributeCount;
        public IntPtr  Attributes;
        public string? TargetAlias;
        public string? UserName;
    }

    private static string? CredentialManagerGet(string target)
    {
        if (!CredRead(target, 1 /*GENERIC*/, 0, out var ptr))
            return null;
        try
        {
            var cred = Marshal.PtrToStructure<CREDENTIAL>(ptr);
            if (cred.CredentialBlobSize == 0) return null;
            var bytes = new byte[cred.CredentialBlobSize];
            Marshal.Copy(cred.CredentialBlob, bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes);
        }
        finally { CredFree(ptr); }
    }

    private static void CredentialManagerSet(string target, string value)
    {
        var blob = Encoding.UTF8.GetBytes(value);
        var handle = GCHandle.Alloc(blob, GCHandleType.Pinned);
        try
        {
            var cred = new CREDENTIAL
            {
                Flags             = 0,
                Type              = 1,   // CRED_TYPE_GENERIC
                TargetName        = target,
                CredentialBlobSize = (uint)blob.Length,
                CredentialBlob    = handle.AddrOfPinnedObject(),
                Persist           = 2,   // CRED_PERSIST_LOCAL_MACHINE
                UserName          = Environment.UserName
            };
            if (!CredWrite(ref cred, 0))
                throw new InvalidOperationException($"CredWrite failed: {Marshal.GetLastWin32Error()}");
        }
        finally { handle.Free(); }
    }

    private static void CredentialManagerDelete(string target)
    {
        CredDelete(target, 1 /*GENERIC*/, 0);
    }

    // ── DPAPI file fallback ───────────────────────────────────────────────

    private static string GetFallbackPath(string key)
    {
        var dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Trion");
        Directory.CreateDirectory(dir);
        var safe = Uri.EscapeDataString(key);
        return Path.Combine(dir, $"{safe}.secret");
    }

    private static string? DpapiFileGet(string key)
    {
        var path = GetFallbackPath(key);
        if (!File.Exists(path)) return null;
        var encrypted = File.ReadAllBytes(path);
        var decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.LocalMachine);
        return Encoding.UTF8.GetString(decrypted);
    }

    private static void DpapiFileSet(string key, string value)
    {
        var plain     = Encoding.UTF8.GetBytes(value);
        var encrypted = ProtectedData.Protect(plain, null, DataProtectionScope.LocalMachine);
        File.WriteAllBytes(GetFallbackPath(key), encrypted);
    }
}
