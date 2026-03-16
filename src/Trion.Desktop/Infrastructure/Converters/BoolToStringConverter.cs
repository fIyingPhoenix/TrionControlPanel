using System.Globalization;
using System.Windows.Data;

namespace Trion.Desktop.Infrastructure.Converters;

/// <summary>
/// Returns TrueValue when bool is true, FalseValue otherwise.
/// Used for status text like "Running" / "Stopped".
/// </summary>
public class BoolToStringConverter : IValueConverter
{
    public string TrueValue  { get; set; } = "True";
    public string FalseValue { get; set; } = "False";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is true ? TrueValue : FalseValue;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
