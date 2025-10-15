using System.Diagnostics;

namespace Trion.Core.Monitoring;

public static class ProcessLauncher
{
    public static Process Start(string fileName, string arguments = "")
    {
        var psi = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        var p = Process.Start(psi)
                 ?? throw new InvalidOperationException($"Failed to start {fileName}");
        return p;   // PID inside p.Id
    }
}