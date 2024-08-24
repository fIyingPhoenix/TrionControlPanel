

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace TrionUI.Controls
{
    public class CustomToolTip : ToolTip
    {
        private Rectangle linkRectangle;
        public Color BackgroundColor { get; set; } = Color.FromArgb(34, 39, 46);
        public Color BorderColor { get; set; } = Color.FromArgb(0, 174, 219);
        public Color TextColor { get; set; } = Color.White;
        public Color TitleColor { get; set; } = Color.Green;
        public Color TitleBackgroundColor { get; set; } = Color.FromArgb(28, 33, 40);
        public int BorderSize { get; set; } = 1;
        public bool LinkEnabled { get; set; } = true;
        public string LinkText { get; set; } = "Read More";
        public Color LinkColor { get; set; } = Color.DodgerBlue; 
        public Font TextFont { get; set; } = new("Segoe UI Semibold", 10);
        public CustomToolTip()
        {
            OwnerDraw = true;
            Draw += new DrawToolTipEventHandler(OnDraw);
            Popup += new PopupEventHandler(OnPopup);
            // Attach the mouse click event to handle link clicks
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (IsBalloon)
            {
                DrawBalloonToolTip(e);
            }
            else
            {
                DrawStandardToolTip(e);
            }
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            // Adjust the size of the tooltip to fit the text if necessary

            using (Graphics g = e.AssociatedControl!.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(GetToolTip(e.AssociatedControl), TextFont);
                SizeF linkSize = LinkEnabled ? g.MeasureString(LinkText, TextFont) : new(0f, 0f);
                SizeF titleSize = g.MeasureString(ToolTipTitle, TextFont);
                // Add some padding to the text size
                e.ToolTipSize = new Size((int)textSize.Width + 10, (int)textSize.Height + 10 + (int)titleSize.Height + (int)linkSize.Height);
            }
        }
        private void DrawStandardToolTip(DrawToolTipEventArgs e)
        {
            SizeF textSize = e.Graphics.MeasureString(e.ToolTipText, TextFont);
            SizeF titleSize = e.Graphics.MeasureString(ToolTipTitle, TextFont);
            SizeF linkSize = e.Graphics.MeasureString(LinkText, TextFont);

            // Draw the background for the title area
            using (SolidBrush titleBackgroundBrush = new(TitleBackgroundColor))
            {
                e.Graphics.FillRectangle(titleBackgroundBrush, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, (int)titleSize.Height + 5));

            }
            // Draw the bottom border of the title background
            using (Pen borderPen = new(Color.FromArgb(28, 33, 40), 1))
            {
                if (ToolTipTitle != string.Empty)
                {
                    var Xsize = e.Bounds.X;
                    var Ysize = e.Bounds.Y + ((int)titleSize.Height + 4);
                    e.Graphics.DrawLine(borderPen, Xsize, Ysize, e.Bounds.Right, Ysize);
                }
            }
            // Draw the background for the text area
            using (SolidBrush backgroundBrush = new(BackgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, new Rectangle(e.Bounds.X, e.Bounds.Y + (int)titleSize.Height + 5, e.Bounds.Width, e.Bounds.Height - (int)titleSize.Height - 5));
            }
            if (BorderSize != 0)
            {
                // Draw the border around the tooltip
                using (Pen borderPen = new(BorderColor, BorderSize)) // Border width = BorderSize >= 1
                {
                    e.Graphics.DrawRectangle(borderPen, new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                }
            }
            // Draw the title if present
            if (!string.IsNullOrEmpty(ToolTipTitle))
            {
                using SolidBrush titleBrush = new(TitleColor);
                e.Graphics.DrawString(ToolTipTitle, new Font(e.Font!, FontStyle.Bold), titleBrush, new PointF((textSize.Width / 2)-(titleSize.Width /2) + 5, 5));
            }
            // Draw the regular text
            using (SolidBrush textBrush = new(TextColor))
            {
                // Draw the main text
                e.Graphics.DrawString(e.ToolTipText, TextFont, textBrush, e.Bounds.X + 5, e.Bounds.Y + titleSize.Height + 5 );

                // Measure the height of the first line of text

                if (LinkEnabled)
                {
                    // Draw the "Read more" link on the new line
                    linkRectangle = new(e.Bounds.X + (int)textSize.Width - (int)linkSize.Width - 5, e.Bounds.Y + (int)titleSize.Height +(int)textSize.Height, (int)linkSize.Width + 5, (int)linkSize.Height);
                    using (SolidBrush linkBrush = new(LinkColor))
                    {
                        e.Graphics.DrawString(LinkText, TextFont, linkBrush, linkRectangle.Location);
                    }
                    // Optionally, underline the "link" text if enabled
                    if (LinkEnabled)
                    {
                        using (Pen underlinePen = new(LinkColor))
                        {
                            e.Graphics.DrawLine(underlinePen, linkRectangle.X - 5, linkRectangle.Bottom, linkRectangle.Right -5 , linkRectangle.Bottom);
                        }
                    }

                }
            }
        }

        private void DrawBalloonToolTip(DrawToolTipEventArgs e)
        {
            // Calculate the balloon area
            Rectangle balloonArea = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

            // Draw the balloon background
            using (SolidBrush backgroundBrush = new(BackgroundColor))
            {
                e.Graphics.FillEllipse(backgroundBrush, balloonArea);
            }

            // Draw the balloon border
            using (Pen borderPen = new(BorderColor))
            {
                e.Graphics.DrawEllipse(borderPen, balloonArea);
            }

            // Draw the title if present
            if (!string.IsNullOrEmpty(ToolTipTitle))
            {
                using SolidBrush titleBrush = new(TitleColor);
                e.Graphics.DrawString(ToolTipTitle, new Font(e.Font!, FontStyle.Bold), titleBrush, new PointF(10, 5));
            }

            // Draw the text
            using SolidBrush textBrush = new(TextColor);
            e.Graphics.DrawString(e.ToolTipText, e.Font!, textBrush, new RectangleF(new PointF(10, 20), e.Bounds.Size));
        }
    }
}
