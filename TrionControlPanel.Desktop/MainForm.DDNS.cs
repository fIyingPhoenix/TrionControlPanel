// =============================================================================
// MainForm.DDNS.cs
// Purpose: Dynamic DNS timer and update logic
// Related UI: TabDDNS, DDNS configuration fields, BTNDDNSTimerStart
// Dependencies: NetworkManager, DDNSUpdate
// =============================================================================

using System.Diagnostics;
using TrionControlPanel.Desktop.Extensions.Classes.Network;
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing Dynamic DNS functionality.
    /// Handles automatic IP updates to DDNS providers.
    /// </summary>
    public partial class MainForm
    {
        #region DDNS Timer and Updates
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Starts the DDNS update timer.
        /// </summary>
        private void BTNDDNSTimerStart_Click(object sender, EventArgs e)
        {
            TimerWacher.Enabled = true;
        }

        /// <summary>
        /// DDNS timer tick handler.
        /// Checks if IP has changed and updates the DDNS provider if needed.
        /// </summary>
        /// <remarks>
        /// Only sends an update if the current public IP differs from the last known IP.
        /// This prevents unnecessary API calls to the DDNS provider.
        /// </remarks>
        private void TImerDinamicDNS_Tick(object sender, EventArgs e)
        {
            // Only update if IP has changed
            if (settings.IPAddress != TXTPublicIP.Text)
            {
                settings.IPAddress = TXTPublicIP.Text;
                NetworkManager.UpdateDNSIP(settings);
            }
        }

        /// <summary>
        /// Opens the selected DDNS provider's website in the default browser.
        /// </summary>
        private void BTNDDNSServiceWebiste_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Links.DDNSWebsites(settings.DDNSService));
        }

        /// <summary>
        /// Handles changes to the DDNS Run on Startup toggle.
        /// </summary>
        private void TGLDDNSRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            settings.DDNSRunOnStartup = TGLDDNSRunOnStartup.Checked;
            TimerDinamicDNS.Enabled = settings.DDNSRunOnStartup;
        }

        #endregion
    }
}
