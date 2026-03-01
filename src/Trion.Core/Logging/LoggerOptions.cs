namespace Trion.Core.Logging;

public sealed class LoggerOptions
{
    public string Folder         { get; set; } = string.Empty; // empty → AppContext.BaseDirectory/Logs
    public bool   WriteToConsole { get; set; } = true;
    public bool   WriteToFile    { get; set; } = true;
    public int    DaysToCompress { get; set; } = 7;
    public int    DaysToKeep     { get; set; } = 30;

    // Runtime-configurable (no #if DEBUG — these are set by the host configuration)
    public bool ShowInfo    { get; set; } = false;
    public bool ShowSuccess { get; set; } = false;
    public bool ShowWarning { get; set; } = true;
    public bool ShowError   { get; set; } = true;
}
