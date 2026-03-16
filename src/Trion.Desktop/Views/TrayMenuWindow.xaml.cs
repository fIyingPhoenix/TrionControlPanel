using System.Windows;
using System.Windows.Media;

namespace Trion.Desktop.Views;

public partial class TrayMenuWindow : Window
{
    private readonly Action _restore;
    private readonly Action _exit;
    private readonly int    _cursorX;
    private readonly int    _cursorY;

    // Prevents the Deactivated auto-close from firing a second Close()
    // after a button already initiated it.
    private bool _closedByButton;

    public TrayMenuWindow(Action restore, Action exit, int cursorX, int cursorY)
    {
        _restore = restore;
        _exit    = exit;
        _cursorX = cursorX;
        _cursorY = cursorY;

        InitializeComponent();

        Loaded      += OnLoaded;
        Deactivated += (_, _) => { if (!_closedByButton) Close(); };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        PositionNearCursor();
        Activate();
    }

    private void PositionNearCursor()
    {
        var source = PresentationSource.FromVisual(this);
        var matrix = source?.CompositionTarget?.TransformFromDevice ?? Matrix.Identity;

        double wpfX = _cursorX * matrix.M11;
        double wpfY = _cursorY * matrix.M22;

        var work = SystemParameters.WorkArea;

        double x = wpfX - ActualWidth  / 2;
        double y = wpfY - ActualHeight - 4;

        x = Math.Max(work.Left, Math.Min(x, work.Right  - ActualWidth));
        y = Math.Max(work.Top,  Math.Min(y, work.Bottom - ActualHeight));

        Left = x;
        Top  = y;
    }

    private void OpenButton_Click(object sender, RoutedEventArgs e)
    {
        _closedByButton = true;
        Close();
        // BeginInvoke so the window fully closes before the main window is shown
        Dispatcher.BeginInvoke(_restore);
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        _closedByButton = true;
        Close();
        Dispatcher.BeginInvoke(_exit);
    }
}
