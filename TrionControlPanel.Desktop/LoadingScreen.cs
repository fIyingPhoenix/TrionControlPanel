
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TrionControlPanel.Desktop.Properties;

namespace TrionControlPanel.Desktop
{
    public partial class LoadingScreen : Form
    {
        private float scaleFactor = 1.0f;
        private int direction = 1; // 1 for growing, -1 for shrinking
        private float minScale = 1.0f;
        private float maxScale = 1.2f;
        private float step = 0.02f;

        public LoadingScreen()
        {

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);
            InitializeComponent();
            timer1_Tick(null!, null!); // Manually call the first tick
            // Make the form transparent
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false; // Hide from taskbar
            TopMost = true; // Keep on top of other windows
            Size = new Size(300, 300); // Set the form size
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

            Region = new Region(path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedEdges(30); // Reapply when resized
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scaleFactor += direction * step;

            if (scaleFactor >= maxScale || scaleFactor <= minScale)
            {
                direction *= -1; // Reverse direction
            }

            int newWidth = (int)(200 * scaleFactor);
            int newHeight = (int)(200 * scaleFactor);

            pictureBox1.Size = new Size(newWidth, newHeight);
            pictureBox1.Location = new Point((this.ClientSize.Width - newWidth) / 2, (this.ClientSize.Height - newHeight) / 2);
        }
    }
}
