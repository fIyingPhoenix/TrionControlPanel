using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TrionUI.Controls
{
    public class CustomLoadingSpinner : Control
    {
        private System.Windows.Forms.Timer timer;
        private int angle = 0;
        private int numberOfSegments = 12;

        public CustomLoadingSpinner()
        {
            // Set the control size
            this.Size = new Size(100, 100);

            // Initialize the animation timer
            timer = new()
            {
                Interval = 50 // Adjust speed (lower = faster)
            };
            timer.Tick += (s, e) =>
            {
                angle = (angle + 30) % 360; // Rotate the spinner
                this.Invalidate(); // Redraw the control
            };
            timer.Start();

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float centerX = Width / 2f;
            float centerY = Height / 2f;
            float radius = Math.Min(centerX, centerY) - 10;

            for (int i = 0; i < numberOfSegments; i++)
            {
                int alpha = (int)(255 * (i + 1) / (float)numberOfSegments);
                Color color = Color.FromArgb(alpha, Color.Gray);
                using (Pen pen = new(color, 4))
                {
                    float angleStep = (360f / numberOfSegments) * i;
                    float startAngle = angle + angleStep;
                    float x1 = centerX + (float)Math.Cos(startAngle * Math.PI / 180) * radius;
                    float y1 = centerY + (float)Math.Sin(startAngle * Math.PI / 180) * radius;
                    float x2 = centerX + (float)Math.Cos(startAngle * Math.PI / 180) * (radius - 10);
                    float y2 = centerY + (float)Math.Sin(startAngle * Math.PI / 180) * (radius - 10);

                    e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }
    }
}