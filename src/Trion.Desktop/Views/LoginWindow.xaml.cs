using System.Windows;
using Trion.Desktop.ViewModels;

namespace Trion.Desktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow(LoginViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        vm.RequestClose += () => Dispatcher.InvokeAsync(Close);
        PasswordInput.PasswordChanged += (_, _) => vm.Password = PasswordInput.Password;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
}
