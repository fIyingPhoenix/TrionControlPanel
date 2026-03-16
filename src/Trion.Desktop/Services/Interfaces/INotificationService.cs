using System.Collections.ObjectModel;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

public interface INotificationService
{
    /// <summary>All entries, newest first. Bound directly to the Logs view and popup.</summary>
    ObservableCollection<NotificationEntry> Entries { get; }

    /// <summary>Number of entries added since the last MarkAllRead() call.</summary>
    int UnreadCount { get; }

    /// <summary>Raised on the UI thread whenever UnreadCount changes.</summary>
    event Action? UnreadCountChanged;

    /// <summary>Adds an entry and increments UnreadCount.</summary>
    void Add(string message);

    /// <summary>Resets UnreadCount to 0.</summary>
    void MarkAllRead();
}
