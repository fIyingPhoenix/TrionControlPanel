using System.Security.Cryptography;

namespace Trion.Agent.Security;

public static class AgentIntegrityGuard
{
    // Injected at build time by CI pipeline via sed replacement.
    // Empty string = check disabled (development builds).
    private const string ExpectedSha256 = "";

    public static void Check()
    {
#if DEBUG
        // Skip integrity check in debug builds
        return;
#else
        if (string.IsNullOrEmpty(ExpectedSha256))
            return;

        var processPath = Environment.ProcessPath;
        if (processPath is null)
            return;

        try
        {
            var bytes    = File.ReadAllBytes(processPath);
            var hash     = SHA256.HashData(bytes);
            var hexHash  = Convert.ToHexString(hash).ToLowerInvariant();
            var expected = ExpectedSha256.ToLowerInvariant();

            var hashBytes     = Convert.FromHexString(hexHash);
            var expectedBytes = Convert.FromHexString(expected);

            if (!CryptographicOperations.FixedTimeEquals(hashBytes, expectedBytes))
            {
                // Silent exit — give no information to attacker
                Environment.Exit(0);
            }
        }
        catch
        {
            // If we cannot verify, do not start
            Environment.Exit(0);
        }
#endif
    }
}
