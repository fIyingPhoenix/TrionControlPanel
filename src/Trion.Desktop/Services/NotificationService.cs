using System.Collections.ObjectModel;
using System.Windows;
using Trion.Desktop.Models;

namespace Trion.Desktop.Services;

/// <summary>
/// Thread-safe notification store. All collection and count mutations are
/// marshalled to the UI thread so ListBox/ItemsControl bindings always work.
/// </summary>
public sealed class NotificationService : INotificationService
{
    private const int MaxEntries = 500;

    public ObservableCollection<NotificationEntry> Entries { get; } = [];

    public int UnreadCount { get; private set; }

    public event Action? UnreadCountChanged;

    public void Add(string message)
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Entries.Insert(0, new NotificationEntry { Message = message });

            if (Entries.Count > MaxEntries)
                Entries.RemoveAt(Entries.Count - 1);

            UnreadCount++;
            UnreadCountChanged?.Invoke();
        });
    }

    public void MarkAllRead()
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            UnreadCount = 0;
            UnreadCountChanged?.Invoke();
        });
    }
}
