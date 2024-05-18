

namespace TrionControlPanelDesktop.UI.Controls.Test
{
    public partial class CustomNotificationsButton : Button
    {
        private int notificationCount = 0;

        public int NotificationCount
        {
            get { return notificationCount; }
            set
            {
                notificationCount = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the notification count
            if (notificationCount > 0)
            {
                using (Font font = new(Font.FontFamily, 9, FontStyle.Bold))
                {
                    string countText = notificationCount > 99 ? "99+" : notificationCount.ToString();
                    SizeF textSize = e.Graphics.MeasureString(countText, font);
                    PointF location = new PointF(ClientRectangle.Width - textSize.Width - 5, 2);
                    //e.Graphics.FillEllipse(Brushes.Red, new RectangleF(location.X, location.Y, textSize.Height, textSize.Height));
                    e.Graphics.DrawString(countText, font, Brushes.White, location);
                }
            }
        }
    }
}
