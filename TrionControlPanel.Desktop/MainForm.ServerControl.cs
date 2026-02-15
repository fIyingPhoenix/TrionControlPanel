// =============================================================================
// MainForm.ServerControl.cs
// Purpose: Handles starting/stopping of Database, World, and Logon servers
// Related UI: BTNStartDatabase, BTNStartWorld, BTNStartLogon, status panels
// Dependencies: ServerController, AppExecuteManager, ServerMonitor, SystemData
// Step 8 of IMPROVEMENTS.md - Uses ServerController for unified server management
// =============================================================================

using MaterialSkin.Controls;
using MetroFramework.Controls;
using TrionControlPanel.Desktop.Extensions.Application;
using TrionControlPanel.Desktop.Extensions.Classes;
using TrionControlPanel.Desktop.Extensions.Classes.Data.Form;
using TrionControlPanel.Desktop.Extensions.Classes.Monitor;
using TrionControlPanel.Desktop.Extensions.Modules;
using TrionControlPanel.Desktop.Properties;

namespace TrionControlPanelDesktop
{
    /// <summary>
    /// Partial class containing server control logic.
    /// Manages starting, stopping, and monitoring of database, world, and logon servers.
    /// </summary>
    public partial class MainForm
    {
        #region Main Server Control Buttons
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Handles the Database server start/stop button click.
        /// Creates MySQL config file before starting, prompts for confirmation before stopping
        /// if other servers are running.
        /// </summary>
        private async void BTNStartDatabase_Click(object sender, EventArgs e)
        {
            var stopDatabase = false;

            // Create MySQL configuration file with current settings
            Settings.CreateMySQLConfigFile(Directory.GetCurrentDirectory(), settings.DBLocation);
            SystemData.DatabaseStartTime = DateTime.Now;

            // Start database if not running and not already started
            if (!FormData.UI.Form.DBRunning && !FormData.UI.Form.DBStarted)
            {
                string arg = $"--defaults-file=\"{Directory.GetCurrentDirectory()}/my.ini\" --console";
                await AppExecuteManager.StartDatabase(settings, arg);
            }
            else
            {
                stopDatabase = true;

                // Warn user if other servers are still running
                if (SystemData.GetWorldProcessesID().Count > 0 || SystemData.GetLogonProcessesID().Count > 0)
                {
                    var result = MaterialMessageBox.Show(
                        this,
                        translator.Translate("UnsafeMysqlTurnOffQuestionMbox"),
                        translator.Translate("UnsafeMysqlTurnOffQuestionMboxTitle"),
                        MessageBoxButtons.YesNo,
                        true,
                        FlexibleMaterialForm.ButtonsPosition.Center
                    );

                    if (result == DialogResult.No)
                    {
                        stopDatabase = false;
                    }
                }
            }

            if (stopDatabase)
            {
                await AppExecuteManager.StopDatabase();
            }
        }

        /// <summary>
        /// Handles the Logon server start/stop button click.
        /// Starts all configured logon servers or stops them if already running.
        /// </summary>
        private async void BTNStartLogon_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedLogon() && !ServerMonitor.ServerRunningLogon())
            {
                await AppExecuteManager.StartLogon(settings);
            }
            else
            {
                await AppExecuteManager.StopLogon();
            }
        }

        /// <summary>
        /// Handles the World server start/stop button click.
        /// Starts all configured world servers or stops them if already running.
        /// </summary>
        private async void BTNStartWorld_Click(object sender, EventArgs e)
        {
            if (!ServerMonitor.ServerStartedWorld() && !ServerMonitor.ServerRunningWorld())
            {
                await AppExecuteManager.StartWorld(settings);
            }
            else
            {
                await AppExecuteManager.StopWorld();
            }
        }

        #endregion

        #region Context Menu Server Controls
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// System tray context menu handler for starting/stopping World server.
        /// </summary>
        private void StartWorldTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartWorld_Click(sender, e);
        }

        /// <summary>
        /// System tray context menu handler for starting/stopping Logon server.
        /// </summary>
        private void StartLogonTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartLogon_Click(sender, e);
        }

        /// <summary>
        /// System tray context menu handler for starting/stopping Database server.
        /// </summary>
        private void StartDatabaseTSMItem_Click(object sender, EventArgs e)
        {
            BTNStartDatabase_Click(sender, e);
        }

        /// <summary>
        /// System tray context menu handler for exiting the application.
        /// </summary>
        private void ExitTSMItem_ClickAsync(object sender, EventArgs e)
        {
            NIcon.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// System tray context menu handler for showing the main form.
        /// </summary>
        private void OpenTSMItem_Click(object sender, EventArgs e)
        {
            NIcon.Visible = false;
            Show();
        }

        #endregion

        #region Server Status UI Updates
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Updates the UI to reflect current server running states.
        /// Updates uptime displays, status panel colors, icons, and button text.
        /// </summary>
        /// <remarks>
        /// Called periodically by TimerWacher_Tick to keep the UI synchronized
        /// with actual server states.
        /// </remarks>
        private void RunServerUpdate()
        {
            // ── World Server Status ──
            if (ServerMonitor.ServerStartedWorld() && ServerMonitor.ServerRunningWorld())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.WorldStartTime;
                LBLUpTimeWorld.Text = FormatUptime(elapsedTime);
                SetServerPanelOnline(PNLWorldServerStatus, PICWorldServerStatus);
                BTNStartWorld.Text = translator.Translate("BTNStartWorldTextON");
            }
            else
            {
                LBLUpTimeWorld.Text = FormatUptime(TimeSpan.Zero);
                SetServerPanelOffline(PNLWorldServerStatus, PICWorldServerStatus);
                BTNStartWorld.Text = translator.Translate("BTNStartWorldTextOFF");
            }

            // ── Logon Server Status ──
            if (ServerMonitor.ServerStartedLogon() && ServerMonitor.ServerRunningLogon())
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.LogonStartTime;
                LBLUpTimeLogon.Text = FormatUptime(elapsedTime);
                SetServerPanelOnline(PNLLogonServerStatus, PICLogonServerStatus);
                BTNStartLogon.Text = translator.Translate("BTNStartLogonTextON");
            }
            else
            {
                LBLUpTimeLogon.Text = FormatUptime(TimeSpan.Zero);
                SetServerPanelOffline(PNLLogonServerStatus, PICLogonServerStatus);
                BTNStartLogon.Text = translator.Translate("BTNStartLogonTextOFF");
            }

            // ── Database Server Status ──
            if (FormData.UI.Form.DBRunning && FormData.UI.Form.DBStarted)
            {
                TimeSpan elapsedTime = DateTime.Now - SystemData.DatabaseStartTime;
                LBLUpTimeDatabase.Text = FormatUptime(elapsedTime);
                SetServerPanelOnline(PNLDatanasServerStatus, PICDatabaseServerStatus);
                BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextON");
            }
            else
            {
                LBLUpTimeDatabase.Text = FormatUptime(TimeSpan.Zero);
                SetServerPanelOffline(PNLDatanasServerStatus, PICDatabaseServerStatus);
                BTNStartDatabase.Text = translator.Translate("BTNStartDatabaseTextOFF");
            }
        }

        /// <summary>
        /// Formats a TimeSpan as an uptime string in format "D : H : M : S".
        /// </summary>
        /// <param name="elapsed">The elapsed time to format</param>
        /// <returns>Formatted uptime string with translated label</returns>
        private string FormatUptime(TimeSpan elapsed)
        {
            return $"{translator.Translate("LBLUpTime")}: {elapsed.Days}D : {elapsed.Hours}H : {elapsed.Minutes}M : {elapsed.Seconds}S";
        }

        /// <summary>
        /// Sets a server status panel to the "online" visual state.
        /// </summary>
        /// <param name="panel">The MetroPanel to update</param>
        /// <param name="pictureBox">The PictureBox showing the status icon</param>
        private void SetServerPanelOnline(MetroPanel panel, PictureBox pictureBox)
        {
            panel.BorderColor = Color.LimeGreen;
            panel.Refresh();
            pictureBox.Image = Resources.cloud_online_64;
        }

        /// <summary>
        /// Sets a server status panel to the "offline" visual state.
        /// </summary>
        /// <param name="panel">The MetroPanel to update</param>
        /// <param name="pictureBox">The PictureBox showing the status icon</param>
        private void SetServerPanelOffline(MetroPanel panel, PictureBox pictureBox)
        {
            panel.BorderColor = Color.DarkRed;
            panel.Refresh();
            pictureBox.Image = Resources.cloud_offline_64;
        }

        #endregion

        #region Individual Expansion Server Controls
        // ─────────────────────────────────────────────────────────────────────
        // These buttons start individual expansion servers separately from the
        // main start buttons, allowing fine-grained control over which servers run.
        // Updated in Step 8 to use ServerController for cleaner code.

        /// <summary>
        /// Starts the Classic expansion World server independently.
        /// </summary>
        private async void BTNWorldClassicStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartWorldServerAsync(Enums.SPP.Classic);
        }

        /// <summary>
        /// Starts the Classic expansion Logon server independently.
        /// </summary>
        private async void BTNLogonClassicStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartLogonServerAsync(Enums.SPP.Classic);
        }

        /// <summary>
        /// Starts the TBC expansion World server independently.
        /// </summary>
        private async void BTNWorldTBCStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartWorldServerAsync(Enums.SPP.TheBurningCrusade);
        }

        /// <summary>
        /// Starts the TBC expansion Logon server independently.
        /// </summary>
        private async void BTNLogonTBCStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartLogonServerAsync(Enums.SPP.TheBurningCrusade);
        }

        /// <summary>
        /// Starts the WotLK expansion World server independently.
        /// </summary>
        private async void BTNWorldWotLKStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartWorldServerAsync(Enums.SPP.WrathOfTheLichKing);
        }

        /// <summary>
        /// Starts the WotLK expansion Logon server independently.
        /// </summary>
        private async void BTNLogonWotLKStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartLogonServerAsync(Enums.SPP.WrathOfTheLichKing);
        }

        /// <summary>
        /// Starts the Cataclysm expansion World server independently.
        /// </summary>
        private async void BTNWorldCataStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartWorldServerAsync(Enums.SPP.Cataclysm);
        }

        /// <summary>
        /// Starts the Cataclysm expansion Logon server independently.
        /// </summary>
        private async void BTNLogonCataStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartLogonServerAsync(Enums.SPP.Cataclysm);
        }

        /// <summary>
        /// Starts the MoP expansion World server independently.
        /// </summary>
        private async void BTNWorldMoPStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartWorldServerAsync(Enums.SPP.MistsOfPandaria);
        }

        /// <summary>
        /// Starts the MoP expansion Logon server independently.
        /// </summary>
        private async void BTNLogonMoPStart_Click(object sender, EventArgs e)
        {
            if (_serverController != null)
                await _serverController.StartLogonServerAsync(Enums.SPP.MistsOfPandaria);
        }

        #endregion

        #region Launch Toggle Event Handlers
        // ─────────────────────────────────────────────────────────────────────
        // These handlers update settings when the user toggles expansion launch options.

        /// <summary>
        /// Enables/disables launch toggle buttons based on installed status.
        /// </summary>
        private void EnableLaunchButtonInstalled()
        {
            TGLClassicLaunch.Enabled = ChecCLASSICInstalled.Checked;
            TGLTBCLaunch.Enabled = ChecTBCInstalled.Checked;
            TGLWotLKLaunch.Enabled = ChecWOTLKInstalled.Checked;
            TGLCataLaunch.Enabled = ChecCATAInstalled.Checked;
            TGLMoPLaunch.Enabled = ChecMOPInstalled.Checked;
        }

        private void TGLClassicLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchClassicCore = TGLClassicLaunch.Checked;
        }

        private void TGLTBCLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchTBCCore = TGLTBCLaunch.Checked;
        }

        private void TGLWotLKLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchWotLKCore = TGLWotLKLaunch.Checked;
        }

        private void TGLCataLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchCataCore = TGLCataLaunch.Checked;
        }

        private void TGLMoPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            settings.LaunchMoPCore = TGLMoPLaunch.Checked;
        }

        #endregion
    }
}
