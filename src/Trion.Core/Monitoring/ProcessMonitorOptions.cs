namespace Trion.Core.Monitoring;

public sealed class ProcessMonitorOptions
{
    public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromSeconds(5);
    public bool EnableDisk { get; set; } = true;
    public bool EnableNetwork { get; set; } = true;

    //discover by name (empty = all) 
    public string[] ProcessNameFilter { get; set; } = Array.Empty<string>();

    // track by PID (set in code) 
    public int[] ExplicitPids { get; set; } = Array.Empty<int>();
}