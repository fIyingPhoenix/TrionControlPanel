using CommunityToolkit.Mvvm.ComponentModel;
using Trion.Desktop.Services;

namespace Trion.Desktop.Models;

public partial class NavigationItem : ObservableObject
{
    private readonly ILocalizationService _loc;

    public string Label        => _loc[LocKey];
    public string IconKey      { get; init; } = string.Empty;
    public string LocKey       { get; init; } = string.Empty;
    public Type   ViewModelType { get; init; } = typeof(object);

    [ObservableProperty]
    private bool _isSelected;

    public NavigationItem(ILocalizationService loc)
    {
        _loc = loc;
        _loc.PropertyChanged += (_, _) => OnPropertyChanged(nameof(Label));
    }
}
