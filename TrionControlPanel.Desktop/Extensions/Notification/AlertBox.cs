using MaterialSkin.Controls;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Notification
{
    public partial class AlertBox : MaterialForm
    {
        public enum NotificationType { Success, Error, Info, Warning }
        private const int AlertWidth = 400;
        private const int AlertHeight = 150;
        private const int AlertSpacing = 10;
        private Translator _translator = new();

        private static readonly List<AlertBox> ActiveAlerts = new(); // Tracks active alerts
        public AlertBox(string message, NotificationType alertType, AppSettings appSettings)
        {
            InitializeComponent();
            // Configure form properties
            Width = AlertWidth;
            Height = AlertHeight;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            Opacity = 0.0; // Start transparent for fade-in effect
            _translator.LoadLanguage(appSettings.TrionLanguage);
            Text = GetTITLeByType(alertType);
            PboxIcon.Image = GetIconByType(alertType);
            LBLMessage.Text = message;
            Refresh();
        }

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1.0)
            {
                Opacity += 0.1; // Fade-in effect
            }
            else
            {
                TimerAnimation.Interval = 5000; // Stay visible for 5 seconds
                TimerAnimation.Tick -= TimerAnimation_Tick; // Remove fade-in handler
                TimerAnimation.Tick += (_, _) => StartFadeOut(sender, e);
            }
        }

        private void StartFadeOut(object sender, EventArgs e)
        {
            TimerAnimation.Tick -= StartFadeOut; // Remove current handler
            TimerAnimation.Tick += (s, _) =>
            {
                Opacity -= 0.1; // Fade-out effect
                if (Opacity <= 0.0) CloseAlert();
            };
            TimerAnimation.Interval = 50; // Fade-out interval
        }
        private Image GetIconByType(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => NotificationIconsList.Images["success.png"]!,
                NotificationType.Error => NotificationIconsList.Images["error.png"]!,
                NotificationType.Info => NotificationIconsList.Images["info.png"]!,
                NotificationType.Warning => NotificationIconsList.Images["warning.png"]!,
                _ => SystemIcons.Application.ToBitmap()
            };
        }

        private string GetTITLeByType(NotificationType type)
        {
            return type switch
            {
                NotificationType.Success => _translator.Translate("AlertBoxSuccess"),
                NotificationType.Error => _translator.Translate("AlertBoxError"),
                NotificationType.Info => _translator.Translate("AlertBoxInfo"),
                NotificationType.Warning => _translator.Translate("AlertBoxWarning"),
                _ => "TRION CONTROL PANEL!"
            };
        }
        private void CloseAlert()
        {
            TimerAnimation.Stop();
            ActiveAlerts.Remove(this);
            Close();
            RepositionAlerts();
        }
        private static void RepositionAlerts()
        {
            for (int i = 0; i < ActiveAlerts.Count; i++)
            {
                var alert = ActiveAlerts[i];
                var startX = Screen.PrimaryScreen!.WorkingArea.Width - AlertWidth - AlertSpacing;
                var startY = Screen.PrimaryScreen.WorkingArea.Height - (AlertHeight + AlertSpacing) * (i + 1);
                alert.Location = new Point(startX, startY);
            }
        }
        public static void Show(string message, NotificationType alertType, AppSettings Settings)
        {
            var alert = new AlertBox(message, alertType, Settings);
            NotificationData.Message = message;
            // Calculate position for the new alert
            var startX = Screen.PrimaryScreen!.WorkingArea.Width - AlertWidth - AlertSpacing;
            var startY = Screen.PrimaryScreen.WorkingArea.Height - (AlertHeight + AlertSpacing) * (ActiveAlerts.Count + 1);

            alert.Location = new Point(startX, startY);
            ActiveAlerts.Add(alert);
            alert.Show();

            alert.TimerAnimation.Start();
        }
    }
}
