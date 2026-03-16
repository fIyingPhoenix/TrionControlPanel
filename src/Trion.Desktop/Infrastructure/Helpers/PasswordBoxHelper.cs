using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Trion.Desktop.Infrastructure.Helpers;

/// <summary>
/// Attached property that enables two-way binding on <see cref="PasswordBox.Password"/>,
/// which is intentionally not a DependencyProperty in WPF.
/// Usage: helpers:PasswordBoxHelper.BoundPassword="{Binding MyPassword, Mode=TwoWay}"
/// </summary>
public static class PasswordBoxHelper
{
    private static readonly DependencyProperty IsUpdatingProperty =
        DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper));

    public static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached(
            "BoundPassword",
            typeof(string),
            typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnBoundPasswordChanged));

    public static string GetBoundPassword(PasswordBox box) =>
        (string)box.GetValue(BoundPasswordProperty);

    public static void SetBoundPassword(PasswordBox box, string value) =>
        box.SetCurrentValue(BoundPasswordProperty, value);

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox box) return;

        box.PasswordChanged -= OnPasswordBoxChanged;

        if (!(bool)box.GetValue(IsUpdatingProperty))
            box.Password = (string?)e.NewValue ?? string.Empty;

        box.PasswordChanged += OnPasswordBoxChanged;
    }

    private static void OnPasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox box) return;
        box.SetValue(IsUpdatingProperty, true);
        var expr = BindingOperations.GetBindingExpression(box, BoundPasswordProperty);
        box.SetCurrentValue(BoundPasswordProperty, box.Password);
        expr?.UpdateSource();
        box.SetValue(IsUpdatingProperty, false);
    }
}
