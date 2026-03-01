using Trion.Agent.Security;
using Xunit;

namespace Trion.Agent.Tests.Security;

public sealed class HmacValidatorTests
{
    private readonly HmacValidator _sut = new();
    private const string Key     = "super-secret-test-key";
    private const string Payload = """{"commandType":"ping","payload":"{}"}""";

    [Fact]
    public void ValidSignature_Accepted()
    {
        var hmac   = _sut.Sign(Payload, Key);
        var result = _sut.Validate(Payload, hmac, Key);
        Assert.True(result);
    }

    [Fact]
    public void WrongKey_Rejected()
    {
        var hmac   = _sut.Sign(Payload, Key);
        var result = _sut.Validate(Payload, hmac, "wrong-key");
        Assert.False(result);
    }

    [Fact]
    public void TamperedPayload_Rejected()
    {
        var hmac          = _sut.Sign(Payload, Key);
        var tampered      = Payload + " ";
        var result        = _sut.Validate(tampered, hmac, Key);
        Assert.False(result);
    }

    [Fact]
    public void EmptyPayload_Rejected()
    {
        var result = _sut.Validate(string.Empty, "abc123", Key);
        Assert.False(result);
    }

    [Fact]
    public void EmptyKey_Rejected()
    {
        var hmac   = _sut.Sign(Payload, Key);
        var result = _sut.Validate(Payload, hmac, string.Empty);
        Assert.False(result);
    }

    [Fact]
    public void SignAndValidate_AreSymmetric()
    {
        var payload2 = """{"commandType":"launch_process","payload":"{}"}""";
        var hmac     = _sut.Sign(payload2, Key);
        Assert.True(_sut.Validate(payload2, hmac, Key));
    }
}
