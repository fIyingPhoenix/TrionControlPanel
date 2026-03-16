using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Trion.Desktop.Infrastructure.Converters;

/// <summary>
/// Converts a bool (IsRunning) to a SolidColorBrush: Green for true, Red/gray for false.
/// Uses DynamicResource-aware fallback colors.
/// </summary>
[ValueConversion(typeof(bool), typeof(Brush))]
public class StatusToColorConverter : IValueConverter
{
    private static readonly SolidColorBrush Running = new(Color.FromRgb(0x4C, 0xAF, 0x50)); // Material Green 600
    private static readonly SolidColorBrush Stopped = new(Color.FromRgb(0x75, 0x75, 0x75)); // Gray 600

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var key = value is true ? "Status.Running" : "Status.Stopped";
        return Application.Current.TryFindResource(key) as Brush
               ?? (value is true ? Running : Stopped);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
