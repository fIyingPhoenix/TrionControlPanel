using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Trion.Desktop.Controls;

public partial class ServerStatusCard : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(ServerStatusCard),
            new PropertyMetadata("Server"));

    public static readonly DependencyProperty IsRunningProperty =
        DependencyProperty.Register(nameof(IsRunning), typeof(bool), typeof(ServerStatusCard),
            new PropertyMetadata(false));

    public static readonly DependencyProperty UptimeProperty =
        DependencyProperty.Register(nameof(Uptime), typeof(string), typeof(ServerStatusCard),
            new PropertyMetadata("00:00:00"));

    public static readonly DependencyProperty ProcessIdProperty =
        DependencyProperty.Register(nameof(ProcessId), typeof(int), typeof(ServerStatusCard),
            new PropertyMetadata(0));

    public static readonly DependencyProperty IconDataProperty =
        DependencyProperty.Register(nameof(IconData), typeof(Geometry), typeof(ServerStatusCard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconAccentProperty =
        DependencyProperty.Register(nameof(IconAccent), typeof(Brush), typeof(ServerStatusCard),
            new PropertyMetadata(null));

    public string   Title     { get => (string)GetValue(TitleProperty);     set => SetValue(TitleProperty, value); }
    public bool     IsRunning { get => (bool)GetValue(IsRunningProperty);   set => SetValue(IsRunningProperty, value); }
    public string   Uptime    { get => (string)GetValue(UptimeProperty);    set => SetValue(UptimeProperty, value); }
    public int      ProcessId { get => (int)GetValue(ProcessIdProperty);    set => SetValue(ProcessIdProperty, value); }
    public Geometry IconData  { get => (Geometry)GetValue(IconDataProperty);set => SetValue(IconDataProperty, value); }
    public Brush    IconAccent{ get => (Brush)GetValue(IconAccentProperty); set => SetValue(IconAccentProperty, value); }

    public ServerStatusCard() => InitializeComponent();
}
