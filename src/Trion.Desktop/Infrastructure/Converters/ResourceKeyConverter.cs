using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Trion.Desktop.Infrastructure.Converters;

/// <summary>
/// Converts a resource key string (e.g. "Icon.Home") to the corresponding
/// application resource (e.g. a Geometry stored in AppIcons.xaml).
/// </summary>
[ValueConversion(typeof(string), typeof(object))]
public sealed class ResourceKeyConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string key || string.IsNullOrEmpty(key))
            return null;

        return Application.Current.TryFindResource(key);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
