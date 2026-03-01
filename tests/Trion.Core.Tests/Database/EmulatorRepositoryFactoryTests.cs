using Microsoft.Extensions.Options;
using Trion.Core.Abstractions.Services;
using Trion.Core.Database;

namespace Trion.Core.Tests.Database;

public sealed class EmulatorRepositoryFactoryTests
{
    private static EmulatorRepositoryFactory CreateFactory(EmulatorConnectionOptions opts) =>
        new(Options.Create(opts));

    // ── No configuration ──────────────────────────────────────────────────────

    [Fact]
    public void EmptyOptions_ReturnsNullForAllTypes()
    {
        var factory = CreateFactory(new EmulatorConnectionOptions());

        Assert.Null(factory.Get(EmulatorType.TrinityCore));
        Assert.Null(factory.Get(EmulatorType.AzerothCore));
        Assert.Null(factory.Get(EmulatorType.CMaNGOS));
    }

    // ── Single-type configuration ─────────────────────────────────────────────

    [Fact]
    public void TrinityCore_Configured_ReturnsNonNull_OthersNull()
    {
        var opts = new EmulatorConnectionOptions
        {
            TrinityCore = new EmulatorDbConfig { AuthConnectionString = "Server=tc;Database=auth;" }
        };

        var factory = CreateFactory(opts);

        Assert.NotNull(factory.Get(EmulatorType.TrinityCore));
        Assert.Null(factory.Get(EmulatorType.AzerothCore));
        Assert.Null(factory.Get(EmulatorType.CMaNGOS));
    }

    [Fact]
    public void AzerothCore_Configured_ReturnsNonNull_OthersNull()
    {
        var opts = new EmulatorConnectionOptions
        {
            AzerothCore = new EmulatorDbConfig { AuthConnectionString = "Server=ac;Database=auth;" }
        };

        var factory = CreateFactory(opts);

        Assert.Null(factory.Get(EmulatorType.TrinityCore));
        Assert.NotNull(factory.Get(EmulatorType.AzerothCore));
        Assert.Null(factory.Get(EmulatorType.CMaNGOS));
    }

    [Fact]
    public void CMaNGOS_Configured_ReturnsNonNull_OthersNull()
    {
        var opts = new EmulatorConnectionOptions
        {
            CMaNGOS = new EmulatorDbConfig { AuthConnectionString = "Server=cm;Database=auth;" }
        };

        var factory = CreateFactory(opts);

        Assert.Null(factory.Get(EmulatorType.TrinityCore));
        Assert.Null(factory.Get(EmulatorType.AzerothCore));
        Assert.NotNull(factory.Get(EmulatorType.CMaNGOS));
    }

    // ── All types configured ──────────────────────────────────────────────────

    [Fact]
    public void AllConfigured_ReturnsNonNullForAllTypes()
    {
        var opts = new EmulatorConnectionOptions
        {
            TrinityCore = new EmulatorDbConfig { AuthConnectionString = "Server=tc;" },
            AzerothCore = new EmulatorDbConfig { AuthConnectionString = "Server=ac;" },
            CMaNGOS     = new EmulatorDbConfig { AuthConnectionString = "Server=cm;" }
        };

        var factory = CreateFactory(opts);

        Assert.NotNull(factory.Get(EmulatorType.TrinityCore));
        Assert.NotNull(factory.Get(EmulatorType.AzerothCore));
        Assert.NotNull(factory.Get(EmulatorType.CMaNGOS));
    }

    // ── Same call always returns same instance (singleton-per-type) ───────────

    [Fact]
    public void SameType_ReturnsSameInstance()
    {
        var opts = new EmulatorConnectionOptions
        {
            TrinityCore = new EmulatorDbConfig { AuthConnectionString = "Server=tc;" }
        };
        var factory = CreateFactory(opts);

        Assert.Same(factory.Get(EmulatorType.TrinityCore), factory.Get(EmulatorType.TrinityCore));
    }

    [Fact]
    public void DifferentTypes_ReturnDifferentInstances()
    {
        var opts = new EmulatorConnectionOptions
        {
            TrinityCore = new EmulatorDbConfig { AuthConnectionString = "Server=tc;" },
            AzerothCore = new EmulatorDbConfig { AuthConnectionString = "Server=ac;" }
        };
        var factory = CreateFactory(opts);

        Assert.NotSame(factory.Get(EmulatorType.TrinityCore), factory.Get(EmulatorType.AzerothCore));
    }

    // ── Empty / whitespace connection string treated as not configured ─────────

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyOrWhitespace_AuthConnectionString_TreatedAsNotConfigured(string cs)
    {
        var opts = new EmulatorConnectionOptions
        {
            TrinityCore = new EmulatorDbConfig { AuthConnectionString = cs }
        };

        var factory = CreateFactory(opts);

        Assert.Null(factory.Get(EmulatorType.TrinityCore));
    }
}
