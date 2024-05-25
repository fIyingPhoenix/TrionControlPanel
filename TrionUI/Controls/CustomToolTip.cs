using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrionUI.Controls
{
    public class CustomToolTip : ToolTip
    {
        public Color BackgroundColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Red;
        public Color TextColor { get; set; } = Color.Blue;
        public Color TitleColor { get; set; } = Color.Green;

        public CustomToolTip()
        {
            this.OwnerDraw = true;
            this.Draw += new DrawToolTipEventHandler(OnDraw);
            this.Popup += new PopupEventHandler(OnPopup);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (this.IsBalloon)
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
            // Customize size if needed
            e.ToolTipSize = new Size(e.ToolTipSize.Width + 10, e.ToolTipSize.Height + 10);
        }

        private void DrawStandardToolTip(DrawToolTipEventArgs e)
        {
            // Draw the background
            using (SolidBrush backgroundBrush = new(BackgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Draw the border
            using (Pen borderPen = new(BorderColor))
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1));
            }

            // Draw the text
            using SolidBrush textBrush = new(TextColor);
            e.Graphics.DrawString(e.ToolTipText, e.Font!, textBrush, e.Bounds);
        }

        private void DrawBalloonToolTip(DrawToolTipEventArgs e)
        {
            // Calculate the balloon area
            Rectangle balloonArea = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

            // Draw the balloon background
            using (SolidBrush backgroundBrush = new (BackgroundColor))
            {
                e.Graphics.FillEllipse(backgroundBrush, balloonArea);
            }

            // Draw the balloon border
            using (Pen borderPen = new(BorderColor))
            {
                e.Graphics.DrawEllipse(borderPen, balloonArea);
            }

            // Draw the title if present
            if (!string.IsNullOrEmpty(this.ToolTipTitle))
            {
                using SolidBrush titleBrush = new(TitleColor);
                e.Graphics.DrawString(this.ToolTipTitle, new Font(e.Font!, FontStyle.Bold), titleBrush, new PointF(10, 5));
            }

            // Draw the text
            using SolidBrush textBrush = new(TextColor);
            e.Graphics.DrawString(e.ToolTipText, e.Font!, textBrush, new RectangleF(new PointF(10, 20), e.Bounds.Size));
        }
    }
}
