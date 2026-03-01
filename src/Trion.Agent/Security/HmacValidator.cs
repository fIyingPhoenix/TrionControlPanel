using System.Security.Cryptography;
using System.Text;

namespace Trion.Agent.Security;

public sealed class HmacValidator
{
    public bool Validate(string payload, string hmac, string sharedKey)
    {
        if (string.IsNullOrEmpty(payload) || string.IsNullOrEmpty(hmac) || string.IsNullOrEmpty(sharedKey))
            return false;

        var expected = Compute(payload, sharedKey);
        var expectedBytes = Convert.FromHexString(expected);
        var actualBytes   = Convert.FromHexString(hmac);

        // Constant-time comparison prevents timing attacks
        return CryptographicOperations.FixedTimeEquals(expectedBytes, actualBytes);
    }

    public string Sign(string payload, string sharedKey)
        => Compute(payload, sharedKey);

    private static string Compute(string payload, string sharedKey)
    {
        var keyBytes     = Encoding.UTF8.GetBytes(sharedKey);
        var payloadBytes = Encoding.UTF8.GetBytes(payload);
        var hash         = HMACSHA256.HashData(keyBytes, payloadBytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
