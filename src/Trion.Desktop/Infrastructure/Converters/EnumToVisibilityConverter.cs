using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Trion.Desktop.Infrastructure.Converters;

/// <summary>
/// Shows the element when the bound enum value equals the ConverterParameter.
/// Usage: Visibility="{Binding ActiveTab, Converter={StaticResource EnumToVisibility},
///                             ConverterParameter={x:Static vm:SettingsTab.Trion}}"
/// </summary>
public class EnumToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value?.Equals(parameter) == true ? Visibility.Visible : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
