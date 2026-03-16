namespace Trion.Desktop.Models;

public sealed record InstallProgress(
    string Phase,       // "Downloading" | "Extracting" | "Repairing" | "Verifying" | "Cleaning up"
    int    Percent,
    double SpeedMbps,
    string StatusText);
