using System;
using System.Globalization;
using System.Windows.Forms;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanel.Desktop.Extensions.Notification;
using static TrionControlPanel.Desktop.Extensions.Notification.AlertBox;

namespace TrionControlPanelDesktop
{
    public partial class MainForm
    {
        private System.Windows.Forms.Timer offlineRetryTimer;

        private void InitializeOfflineMode()
        {
            if (offlineRetryTimer == null)
            {
                offlineRetryTimer = new System.Windows.Forms.Timer();
                offlineRetryTimer.Interval = 30000; // 30 seconds
                offlineRetryTimer.Tick += OfflineRetryTimer_Tick;
            }
            offlineRetryTimer.Start();
        }

        private void SetOfflineState()
        {
            AlertBox.Show(translator.Translate("Server unreachable. Starting in Offline Mode."), NotificationType.Warning, settings);
            
            if (!this.Text.Contains("(Offline Mode)"))
            {
                this.Text += " (Offline Mode)";
            }

            // Only load local versions
            AppUpdateManager.GetSPPVersionOffline(settings);

            // Update labels with local info
            UpdateVersionLabelsOffline();

            // Disable Online-Only Actions
            ToggleOnlineButtons(false);

            // Start background retry
            InitializeOfflineMode();
        }

        private void ToggleOnlineButtons(bool enabled)
        {
            BTNInstallSPP.Enabled = enabled;
            BTNRepairSPP.Enabled = enabled;
            BTNDownloadUpdates.Enabled = enabled;
            BTNReviveIP.Enabled = enabled;
            BTNReviveSupporterKey.Enabled = enabled;
        }

        private void UpdateVersionLabelsOffline()
        {
            LBLTrionVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLTrionVersion"), FormData.UI.Version.Local.Trion, "Offline");
            LBLDBVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLDBVersion"), FormData.UI.Version.Local.Database, "Offline");
            LBLClassicVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLClassicVersion"), FormData.UI.Version.Local.Classic, "Offline");
            LBLTBCVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLTBCVersion"), FormData.UI.Version.Local.TBC, "Offline");
            LBLWotLKVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLWotLKVersion"), FormData.UI.Version.Local.WotLK, "Offline");
            LBLCataVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLCataVersion"), FormData.UI.Version.Local.Cata, "Offline");
            LBLMoPVersion.Text = string.Format(CultureInfo.InvariantCulture, translator.Translate("LBLMoPVersion"), FormData.UI.Version.Local.Mop, "Offline");
        }

        private async void OfflineRetryTimer_Tick(object sender, EventArgs e)
        {
            // Check connectivity silently
            await NetworkManager.GetAPIServer();
            
            if (!NetworkManager.IsOffline)
            {
                offlineRetryTimer.Stop();
                await GoOnline();
            }
        }

        private async Task GoOnline()
        {
            AlertBox.Show("Connection restored. Switching to Online Mode.", NotificationType.Success, settings);
            this.Text = this.Text.Replace(" (Offline Mode)", "");
            
            ToggleOnlineButtons(true);

            // Refresh versions and start updates
            await UpdateSppVersion();
            await StartAutoUpdate();
        }
    }
}
