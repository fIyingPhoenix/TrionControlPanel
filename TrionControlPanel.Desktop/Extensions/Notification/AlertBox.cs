using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace NotificationSystem
{
    public enum NotificationType { Success, Error, Info, Warning }

    public class AlertBox : MaterialForm
    {
        private static readonly List<AlertBox> ActiveAlerts = new(); // Tracks active alerts
        private const int AlertWidth = 300;
        private const int AlertHeight = 180;
        private const int AlertSpacing = 10;

        private readonly Timer _timer = new();
        private PictureBox iconBox;
        private MaterialLabel messageLabel;
        private NotificationType _alertType;

        public AlertBox(string message, NotificationType alertType)
        {
            // Configure form properties
            Width = AlertWidth;
            Height = AlertHeight;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            Opacity = 0.0; // Start transparent for fade-in effect

            _alertType = alertType;


            // Configure timer for fade-in, display, and fade-out
            _timer.Interval = 1;
            _timer.Tick += Timer_Tick;
        }

        private Image GetIconByType(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => SystemIcons.Information.ToBitmap(),
                NotificationType.Error => SystemIcons.Error.ToBitmap(),
                NotificationType.Info => SystemIcons.Question.ToBitmap(),
                NotificationType.Warning => SystemIcons.Warning.ToBitmap(),
                _ => SystemIcons.Application.ToBitmap()
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1.0)
            {
                Opacity += 0.1; // Fade-in effect
            }
            else
            {
                _timer.Interval = 5000; // Stay visible for 5 seconds
                _timer.Tick -= Timer_Tick; // Remove fade-in handler
                _timer.Tick += (_, _) => StartFadeOut(sender, e);
            }
        }

        private void StartFadeOut(object sender, EventArgs e)
        {
            _timer.Tick -= StartFadeOut; // Remove current handler
            _timer.Tick += (s, _) =>
            {
                Opacity -= 0.1; // Fade-out effect
                if (Opacity <= 0.0) CloseAlert();
            };
            _timer.Interval = 50; // Fade-out interval
        }

        private void CloseAlert()
        {
            _timer.Stop();
            ActiveAlerts.Remove(this);
            Close();
            RepositionAlerts();
        }

        public static void ShowAlert(string message, NotificationType alertType)
        {
            var alert = new AlertBox(message, alertType);

            // Calculate position for the new alert
            var startX = Screen.PrimaryScreen.WorkingArea.Width - AlertWidth - AlertSpacing;
            var startY = Screen.PrimaryScreen.WorkingArea.Height - (AlertHeight + AlertSpacing) * (ActiveAlerts.Count + 1);

            alert.Location = new Point(startX, startY);
            ActiveAlerts.Add(alert);
            alert.Show();

            alert._timer.Start();
        }

        private void InitializeComponent()
        {
            iconBox = new PictureBox();
            messageLabel = new MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)iconBox).BeginInit();
            SuspendLayout();
            // 
            // iconBox
            // 
            iconBox.Location = new Point(5, 75);
            iconBox.Name = "iconBox";
            iconBox.Size = new Size(65, 65);
            iconBox.TabIndex = 0;
            iconBox.TabStop = false;
            // 
            // messageLabel
            // 
            messageLabel.Depth = 0;
            messageLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            messageLabel.Location = new Point(75, 75);
            messageLabel.MouseState = MaterialSkin.MouseState.HOVER;
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(318, 65);
            messageLabel.TabIndex = 1;
            messageLabel.Text = "materialLabel1";
            // 
            // AlertBox
            // 
            ClientSize = new Size(400, 150);
            Controls.Add(messageLabel);
            Controls.Add(iconBox);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "AlertBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)iconBox).EndInit();
            ResumeLayout(false);
        }

        private static void RepositionAlerts()
        {
            for (int i = 0; i < ActiveAlerts.Count; i++)
            {
                var alert = ActiveAlerts[i];
                var startX = Screen.PrimaryScreen.WorkingArea.Width - AlertWidth - AlertSpacing;
                var startY = Screen.PrimaryScreen.WorkingArea.Height - (AlertHeight + AlertSpacing) * (i + 1);
                alert.Location = new Point(startX, startY);
            }
        }
    }
}
