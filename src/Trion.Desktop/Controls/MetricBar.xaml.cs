using System.Windows;
using System.Windows.Controls;

namespace Trion.Desktop.Controls;

public partial class MetricBar : UserControl
{
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(MetricBar),
            new PropertyMetadata("Metric"));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(MetricBar),
            new PropertyMetadata(0.0));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public MetricBar()
    {
        InitializeComponent();
    }
}
