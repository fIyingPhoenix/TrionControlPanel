using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Trion.Desktop.Infrastructure.Converters;

/// <summary>Collapses the element when the bound bool is true (inverse of BoolToVisibilityConverter).</summary>
public sealed class InverseBoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is true ? Visibility.Collapsed : Visibility.Visible;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
