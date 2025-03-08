using MaterialSkin.Controls;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules.Lists;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanel.Desktop.Extensions.Notification
{
    public partial class AlertBox : MaterialForm
    {
        // Enum for different notification types
        public enum NotificationType { Success, Error, Info, Warning }

        // Constants for alert size and spacing
        private const int AlertWidth = 400;
        private const int AlertHeight = 150;
        private const int AlertSpacing = 10;

        // Translator for localization
        private readonly Translator _translator = new();

        // Track active alerts on the screen
        private static readonly List<AlertBox> ActiveAlerts = new();

        // Constructor initializes alert box with message, type, and settings
        public AlertBox(string message, NotificationType alertType, AppSettings appSettings)
        {
            InitializeComponent();

            // Configure form properties for the alert box
            Width = AlertWidth;
            Height = AlertHeight;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ShowInTaskbar = false;
            Opacity = 0.0; // Start transparent for fade-in effect

            // Load language settings and set alert title and icon based on type
            _translator.LoadLanguage(appSettings.TrionLanguage);
            Text = GetTitleByType(alertType);
            PboxIcon.Image = GetIconByType(alertType);
            LBLMessage.Text = message;
            Refresh();
        }

        // Timer tick event to handle the fade-in effect
        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1.0)
            {
                Opacity += 0.1; // Fade-in effect
            }
            else
            {
                // After fading in, set a delay before starting fade-out
                TimerAnimation.Interval = 5000; // Stay visible for 5 seconds
                TimerAnimation.Tick -= TimerAnimation_Tick; // Remove fade-in handler
                TimerAnimation.Tick += (_, _) => StartFadeOut(sender, e);
            }
        }

        // Start the fade-out effect
        private void StartFadeOut(object sender, EventArgs e)
        {
            TimerAnimation.Tick -= StartFadeOut; // Remove fade-out handler
            TimerAnimation.Tick += (s, _) =>
            {
                Opacity -= 0.1; // Fade-out effect
                if (Opacity <= 0.0) CloseAlert();
            };

            TimerAnimation.Interval = 50; // Fade-out interval
        }

        // Get the icon for the alert based on its type
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

        // Get the title for the alert based on its type
        private string GetTitleByType(NotificationType type)
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

        // Close the alert and remove it from the active alerts list
        private void CloseAlert()
        {
            TimerAnimation.Stop();
            ActiveAlerts.Remove(this);
            Close();
            RepositionAlerts(); // Reposition remaining alerts
        }

        // Reposition alerts to prevent overlap
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
        // Checking if the DPI is Upsacled
        private static int IsDpiUpscaled()
        {
            // Create a Graphics object to get the DPI
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                // Get the system DPI (DPI of the screen)
                float dpi = g.DpiX;
                //Log the dpi for Debuging
                TrionLogger.Log($"The Screen DPI is:{dpi}");
                // Check if the DPI is above the default 96 DPI (which is the baseline for 100% scaling)
                return ((int)(dpi) - 96) * 2;
               
            }
        }

        // Public method to display a new alert
        public static void Show(string message, NotificationType alertType, AppSettings settings)
        {
            var alert = new AlertBox(message, alertType, settings);

            // Update notification data (assuming FormData is globally accessible)
            NotificationData.Message = message;

            // Get the primary screen information
            var screen = Screen.PrimaryScreen;

            // Get the scaling factor of the screen (this handles different DPI settings)
            float dpiScale = screen.Bounds.Width / screen.WorkingArea.Width;

            // Calculate position for the new alert considering screen scaling
            var startX = screen.WorkingArea.Width - AlertWidth - AlertSpacing - IsDpiUpscaled();
            var startY = screen.WorkingArea.Height - (AlertHeight  + AlertSpacing + IsDpiUpscaled()) * (ActiveAlerts.Count + 1);

            // Ensure the alert is within screen bounds, considering DPI scaling
            startX = (int)(startX / dpiScale);
            startY = (int)(startY / dpiScale);

            // Ensure alert position does not go out of the screen
            startX = Math.Max(0, startX);
            startY = Math.Max(0, startY);
            //check the Localtio
            alert.Location = new Point(startX, startY);

            // Add the alert to the active alerts list and show it
            ActiveAlerts.Add(alert);
            alert.Show();
            alert.TimerAnimation.Start();
        }
    }
}
