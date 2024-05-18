using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace TrionControlPanelDesktop.UI.Controls
{
    public class CustomButton : Button
    {
        private int notificationCount = 0;
        //Fields
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderColor = Color.PaleVioletRed;

        //Properties
        [Category("1 CustomButton Advance")]
        public int NotificationCount
        {
            get { return notificationCount; }
            set
            {
                notificationCount = value;
                Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public int BorderSize
        {
            get => borderSize; 
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        [Category("1 CustomButton Advance")]
        public int BorderRadius
        {
            get => borderRadius; 
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        [Category("1 CustomButton Advance")]
        public Color BorderColor
        {
            get => borderColor;
            set
            { 
                borderColor = value;
                Invalidate();
            }
        }

        [Category("1 CustomButton Advance")]
        public Color BackgroundColor
        {
            get => BackColor; 
            set => BackColor = value; 
        }

        [Category("1 CustomButton Advance")]
        public Color TextColor
        {
            get => ForeColor; 
            set => ForeColor = value;
        }

        //Constructor
        public CustomButton()
        {

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = Color.MediumSlateBlue;
            ForeColor = Color.White;
            Resize += new EventHandler(Button_Resize);
        }

        //Methods
        private static GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            // Draw the notification count
            if (notificationCount > 0)
            {
                Graphics g = pevent.Graphics;
                // Set smoothing mode to AntiAlias for smoother drawing
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Draw the red ellipse
                using (SolidBrush brush = new(Color.FromArgb(0, 174, 219)))
                {
                    g.FillEllipse(brush, Width - 15, 4, 14,14);
                }
                // Draw the notification count text
                using (Font font = new("Segoe UI", notificationCount >= 10 ? 6 : 10, FontStyle.Bold)) // Adjust font size
                { // notification count 
                    string countText = notificationCount > 99 ? "99+" : notificationCount.ToString();
                    SizeF textSize = g.MeasureString(countText, font);
                    PointF textPosition = new(Width - 14 + (13 - textSize.Width) / 2, 5 + (14 - textSize.Height) / 2);
                    g.DrawString(countText, font, Brushes.White, textPosition);
                }
            }
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius);
                using GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize);
                using Pen penSurface = new(Parent!.BackColor, smoothSize);
                using Pen penBorder = new(borderColor, borderSize);
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);

                }
               
            }
            else //Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {

                    using (Pen penBorder = new(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent!.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        private void Container_BackColorChanged(object? sender, EventArgs e)
        {
            Invalidate();
        }
        private void Button_Resize(object? sender, EventArgs e)
        {
            if (borderRadius > Height)
                borderRadius = Height;
        }
    }
}
