// ── WPF vs WinForms type disambiguation ───────────────────────────────────────
// UseWindowsForms=true (needed for the system-tray NotifyIcon) pulls in
// System.Windows.Forms and System.Drawing globally, which conflicts with
// several WPF / System.Windows.Media types.
// These global aliases restore the correct WPF types project-wide so every
// other file compiles unchanged.

global using Application    = System.Windows.Application;
global using Binding        = System.Windows.Data.Binding;
global using Brush          = System.Windows.Media.Brush;
global using Brushes        = System.Windows.Media.Brushes;
global using Color          = System.Windows.Media.Color;
global using ColorConverter = System.Windows.Media.ColorConverter;
global using Pen            = System.Windows.Media.Pen;
global using Point          = System.Windows.Point;
global using UserControl    = System.Windows.Controls.UserControl;
