namespace Trion.Core.Database;

public sealed class EmulatorConnectionOptions
{
    public EmulatorDbConfig? TrinityCore { get; set; }
    public EmulatorDbConfig? AzerothCore { get; set; }
    public EmulatorDbConfig? CMaNGOS     { get; set; }
}

public sealed class EmulatorDbConfig
{
    public string  AuthConnectionString      { get; set; } = string.Empty;
    public string? CharacterConnectionString { get; set; }
    public string? WorldConnectionString     { get; set; }
}
