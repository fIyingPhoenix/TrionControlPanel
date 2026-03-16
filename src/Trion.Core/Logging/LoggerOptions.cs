namespace Trion.Core.Logging;

public sealed class LoggerOptions
{
    /// <summary>Absolute path to the log folder. Empty = AppContext.BaseDirectory/Logs</summary>
    public string Folder         { get; set; } = string.Empty;

    public bool   WriteToConsole { get; set; } = true;
    public bool   WriteToFile    { get; set; } = true;

    /// <summary>Days before a log file is GZip-compressed. Default: 7.</summary>
    public int    DaysToCompress { get; set; } = 7;

    /// <summary>Days before a log file (or its .gz) is deleted. Default: 30.</summary>
    public int    DaysToKeep     { get; set; } = 30;

    // Per-level visibility — configurable at runtime via appsettings / host config
    public bool ShowDebug   { get; set; } = false;
    public bool ShowInfo    { get; set; } = true;
    public bool ShowWarning { get; set; } = true;
    public bool ShowError   { get; set; } = true;
}
