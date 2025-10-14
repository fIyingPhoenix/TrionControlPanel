namespace Trion.Core.Logging;

public sealed class LoggerOptions
{
    public string Folder { get; set; } = "Logs";
    public bool WriteToConsole { get; set; } = true;
    public bool WriteToFile { get; set; } = true;
    public int DaysToCompress { get; set; } = 7;
    public int DaysToKeep { get; set; } = 30;

#if DEBUG
    public bool ShowInfo { get; set; } = true;
    public bool ShowSuccess { get; set; } = true;
    public bool ShowWarning { get; set; } = true;
    public bool ShowError { get; set; } = true;
#else
    public bool ShowInfo    { get; set; } = false;
    public bool ShowSuccess { get; set; } = false;
    public bool ShowWarning { get; set; } = true;
    public bool ShowError   { get; set; } = true;
#endif
}