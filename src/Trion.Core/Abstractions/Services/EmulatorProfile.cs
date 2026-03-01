namespace Trion.Core.Abstractions.Services;

public enum EmulatorType { TrinityCore, AzerothCore, CMaNGOS }

public sealed record EmulatorProfile(
    string Id,
    string DisplayName,
    EmulatorType Type,
    string ExecutablePath,
    string WorkingDirectory,
    bool AutoRestart = true,
    int MaxRestartAttempts = 3);
