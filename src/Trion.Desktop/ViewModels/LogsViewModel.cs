using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Trion.Desktop.Models;
using Trion.Desktop.Services;

namespace Trion.Desktop.ViewModels;

public partial class LogsViewModel : ObservableObject
{
    private readonly INotificationService  _notifications;
    private readonly ILocalizationService  _loc;

    public ILocalizationService Loc => _loc;

    public ObservableCollection<NotificationEntry> Entries => _notifications.Entries;

    public LogsViewModel(INotificationService notifications, ILocalizationService loc)
    {
        _notifications = notifications;
        _loc           = loc;
    }

    [RelayCommand]
    private void ClearAll()
    {
        _notifications.Entries.Clear();
        _notifications.MarkAllRead();
    }
}
