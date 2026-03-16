using CommunityToolkit.Mvvm.ComponentModel;

namespace Trion.Desktop.Services;

public interface INavigationService
{
    ObservableObject? CurrentViewModel { get; }
    void NavigateTo<TViewModel>() where TViewModel : ObservableObject;
    void NavigateTo(Type viewModelType);
}
