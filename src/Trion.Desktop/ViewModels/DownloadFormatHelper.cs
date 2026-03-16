namespace Trion.Desktop.ViewModels;

internal static class DownloadFormatHelper
{
    public static string FormatBytes(long bytes) => bytes switch
    {
        >= 1_073_741_824 => $"{bytes / 1_073_741_824.0:F1} GB",
        >= 1_048_576     => $"{bytes / 1_048_576.0:F1} MB",
        >= 1_024         => $"{bytes / 1_024.0:F0} KB",
        _                => $"{bytes} B",
    };

    public static string FormatSize(long downloaded, long total) =>
        total > 0
            ? $"{FormatBytes(downloaded)} / {FormatBytes(total)}"
            : FormatBytes(downloaded);
}
