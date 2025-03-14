
using System.Drawing.Drawing2D;
using TrionControlPanel.Desktop.Properties;

namespace TrionControlPanel.Desktop
{
    public partial class LoadingScreen : Form
    {
        private float _angle; // Used for spinner rotation

        private const int SpinnerRadius = 15; // Size of the spinner
        private const int SpinnerThickness = 14; // Thickness of the spinner stroke
        public LoadingScreen()
        {

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);
            InitializeComponent();
            // Make the form transparent
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false; // Hide from taskbar
            TopMost = true; // Keep on top of other windows
            Size = new Size(400, 300); // Set the form size

            ApplyRoundedEdges(30); // Adjust the corner radius (30px)
        }
        private void ApplyRoundedEdges(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90); // Top-left
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90); // Top-right
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90); // Bottom-right
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90); // Bottom-left
            path.CloseFigure();

            this.Region = new Region(path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedEdges(30); // Reapply when resized
        }

    }
}
