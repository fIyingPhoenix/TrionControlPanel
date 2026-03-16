namespace Trion.Desktop.Models;

public class NotificationEntry
{
    public string   Message       { get; init; } = string.Empty;
    public DateTime Timestamp     { get; init; } = DateTime.Now;

    public string FormattedTime => Timestamp.ToString("HH:mm:ss");
    public string FormattedDate => Timestamp.ToString("yyyy-MM-dd");
}
