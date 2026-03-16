using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Trion.Desktop.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private ObservableObject? _currentViewModel;

    public ObservableObject? CurrentViewModel
    {
        get => _currentViewModel;
        private set => SetProperty(ref _currentViewModel, value);
    }

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ObservableObject =>
        NavigateTo(typeof(TViewModel));

    public void NavigateTo(Type viewModelType)
    {
        var vm = _serviceProvider.GetRequiredService(viewModelType) as ObservableObject;
        CurrentViewModel = vm;
    }
}
