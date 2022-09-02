using TrionControlPanel.UI;
namespace TrionControlPanel.TabsComponents
{
    partial class SettingControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        // txtMysqlLocation
        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblWorldName;
            System.Windows.Forms.Label lblBnetName;
            System.Windows.Forms.Label lblMysqlName;
            System.Windows.Forms.Label lblAuth;
            System.Windows.Forms.Label lblPort;
            System.Windows.Forms.Label lblPassword;
            System.Windows.Forms.Label lblUser;
            System.Windows.Forms.Label lblServer;
            System.Windows.Forms.Label lblMysqlLocation;
            System.Windows.Forms.Label lblCoreLocation;
            this.txtAuthDatabase = new MetroFramework.Controls.MetroTextBox();
            this.txtMySqlPort = new MetroFramework.Controls.MetroTextBox();
            this.txtMySqlPassowrd = new MetroFramework.Controls.MetroTextBox();
            this.txtMySqlUser = new MetroFramework.Controls.MetroTextBox();
            this.txtMySqlServer = new MetroFramework.Controls.MetroTextBox();
            this.lblMySqlServerSettings = new System.Windows.Forms.Label();
            this.tglStayInTray = new TrionControlPanel.UI.CustomToggleButton();
            this.lblRestartServerOnCrash = new System.Windows.Forms.Label();
            this.tglRestartOnCrash = new TrionControlPanel.UI.CustomToggleButton();
            this.lblStartServerOnLaunch = new System.Windows.Forms.Label();
            this.tglStartServer = new TrionControlPanel.UI.CustomToggleButton();
            this.lblStartWithWindows = new System.Windows.Forms.Label();
            this.tglStartOnStartup = new TrionControlPanel.UI.CustomToggleButton();
            this.lblCores = new System.Windows.Forms.Label();
            this.lblHideControls = new System.Windows.Forms.Label();
            this.tglHideConsole = new TrionControlPanel.UI.CustomToggleButton();
            this.lblNotySound = new System.Windows.Forms.Label();
            this.tglNotySound = new TrionControlPanel.UI.CustomToggleButton();
            this.lblStayTray = new System.Windows.Forms.Label();
            this.lblControlPanelSettings = new System.Windows.Forms.Label();
            this.btnSave = new TrionControlPanel.UI.CustomButton();
            this.lblCustomNames = new System.Windows.Forms.Label();
            this.tglCustomNames = new TrionControlPanel.UI.CustomToggleButton();
            this.txtMysqlName = new MetroFramework.Controls.MetroTextBox();
            this.lblNames = new System.Windows.Forms.Label();
            this.txtWorldName = new MetroFramework.Controls.MetroTextBox();
            this.txtBnetName = new MetroFramework.Controls.MetroTextBox();
            this.TimerUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnTestMySQL = new TrionControlPanel.UI.CustomButton();
            this.panelNames = new TrionControlPanel.UI.CustomPanel();
            this.panelControSettins = new TrionControlPanel.UI.CustomPanel();
            this.btnFixMysql = new TrionControlPanel.UI.CustomButton();
            this.PanelMySelSettings = new TrionControlPanel.UI.CustomPanel();
            this.btCoreLocationSearch = new TrionControlPanel.UI.CustomButton();
            this.lblPCresource = new System.Windows.Forms.Label();
            this.txtWorldLocation = new MetroFramework.Controls.MetroTextBox();
            this.bntOpenLocation = new TrionControlPanel.UI.CustomButton();
            this.bntOpenMySqlLocation = new TrionControlPanel.UI.CustomButton();
            this.comboBoxCore = new MetroFramework.Controls.MetroComboBox();
            this.PanelServerLocation = new TrionControlPanel.UI.CustomPanel();
            this.txtCoreLocation = new MetroFramework.Controls.MetroTextBox();
            this.txtMysqlLocation = new MetroFramework.Controls.MetroTextBox();
            this.btnMySQLLocationSearch = new TrionControlPanel.UI.CustomButton();
            this.lblServerLocation = new System.Windows.Forms.Label();
            this.btnCoreLocation = new TrionControlPanel.UI.CustomButton();
            this.btnMysqlOpenLocation = new TrionControlPanel.UI.CustomButton();
            lblWorldName = new System.Windows.Forms.Label();
            lblBnetName = new System.Windows.Forms.Label();
            lblMysqlName = new System.Windows.Forms.Label();
            lblAuth = new System.Windows.Forms.Label();
            lblPort = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            lblUser = new System.Windows.Forms.Label();
            lblServer = new System.Windows.Forms.Label();
            lblMysqlLocation = new System.Windows.Forms.Label();
            lblCoreLocation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWorldName
            // 
            lblWorldName.AutoSize = true;
            lblWorldName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblWorldName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblWorldName.Location = new System.Drawing.Point(29, 268);
            lblWorldName.Name = "lblWorldName";
            lblWorldName.Size = new System.Drawing.Size(77, 15);
            lblWorldName.TabIndex = 31;
            lblWorldName.Text = "World Name:";
            // 
            // lblBnetName
            // 
            lblBnetName.AutoSize = true;
            lblBnetName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblBnetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblBnetName.Location = new System.Drawing.Point(29, 309);
            lblBnetName.Name = "lblBnetName";
            lblBnetName.Size = new System.Drawing.Size(100, 15);
            lblBnetName.TabIndex = 33;
            lblBnetName.Text = "Bnet/Auth Name:";
            // 
            // lblMysqlName
            // 
            lblMysqlName.AutoSize = true;
            lblMysqlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblMysqlName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblMysqlName.Location = new System.Drawing.Point(29, 348);
            lblMysqlName.Name = "lblMysqlName";
            lblMysqlName.Size = new System.Drawing.Size(83, 15);
            lblMysqlName.TabIndex = 36;
            lblMysqlName.Text = "MySQL Name:";
            // 
            // lblAuth
            // 
            lblAuth.AutoSize = true;
            lblAuth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblAuth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblAuth.Location = new System.Drawing.Point(237, 426);
            lblAuth.Name = "lblAuth";
            lblAuth.Size = new System.Drawing.Size(36, 15);
            lblAuth.TabIndex = 56;
            lblAuth.Text = "Auth:";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblPort.Location = new System.Drawing.Point(237, 387);
            lblPort.Name = "lblPort";
            lblPort.Size = new System.Drawing.Size(32, 15);
            lblPort.TabIndex = 57;
            lblPort.Text = "Port:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblPassword.Location = new System.Drawing.Point(237, 349);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(57, 15);
            lblPassword.TabIndex = 58;
            lblPassword.Text = "Password";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblUser.Location = new System.Drawing.Point(237, 310);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(33, 15);
            lblUser.TabIndex = 59;
            lblUser.Text = "User:";
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblServer.Location = new System.Drawing.Point(237, 269);
            lblServer.Name = "lblServer";
            lblServer.Size = new System.Drawing.Size(42, 15);
            lblServer.TabIndex = 60;
            lblServer.Text = "Server:";
            // 
            // lblMysqlLocation
            // 
            lblMysqlLocation.AutoSize = true;
            lblMysqlLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblMysqlLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblMysqlLocation.Location = new System.Drawing.Point(29, 108);
            lblMysqlLocation.Name = "lblMysqlLocation";
            lblMysqlLocation.Size = new System.Drawing.Size(94, 15);
            lblMysqlLocation.TabIndex = 66;
            lblMysqlLocation.Text = "MySQL Location";
            // 
            // lblCoreLocation
            // 
            lblCoreLocation.AutoSize = true;
            lblCoreLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblCoreLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblCoreLocation.Location = new System.Drawing.Point(30, 56);
            lblCoreLocation.Name = "lblCoreLocation";
            lblCoreLocation.Size = new System.Drawing.Size(81, 15);
            lblCoreLocation.TabIndex = 67;
            lblCoreLocation.Text = "Core Location";
            // 
            // txtAuthDatabase
            // 
            this.txtAuthDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtAuthDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtAuthDatabase.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtAuthDatabase.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtAuthDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtAuthDatabase.Location = new System.Drawing.Point(237, 441);
            this.txtAuthDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.txtAuthDatabase.Multiline = true;
            this.txtAuthDatabase.Name = "txtAuthDatabase";
            this.txtAuthDatabase.Padding = new System.Windows.Forms.Padding(1);
            this.txtAuthDatabase.SelectedText = "";
            this.txtAuthDatabase.Size = new System.Drawing.Size(206, 23);
            this.txtAuthDatabase.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAuthDatabase.StyleManager = null;
            this.txtAuthDatabase.TabIndex = 40;
            this.txtAuthDatabase.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtAuthDatabase.UseStyleColors = true;
            // 
            // txtMySqlPort
            // 
            this.txtMySqlPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlPort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPort.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMySqlPort.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMySqlPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPort.Location = new System.Drawing.Point(237, 402);
            this.txtMySqlPort.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlPort.Multiline = true;
            this.txtMySqlPort.Name = "txtMySqlPort";
            this.txtMySqlPort.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlPort.SelectedText = "";
            this.txtMySqlPort.Size = new System.Drawing.Size(206, 23);
            this.txtMySqlPort.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMySqlPort.StyleManager = null;
            this.txtMySqlPort.TabIndex = 31;
            this.txtMySqlPort.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMySqlPort.UseStyleColors = true;
            this.txtMySqlPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtMySqlPassowrd
            // 
            this.txtMySqlPassowrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlPassowrd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPassowrd.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMySqlPassowrd.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMySqlPassowrd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPassowrd.Location = new System.Drawing.Point(237, 364);
            this.txtMySqlPassowrd.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlPassowrd.Multiline = true;
            this.txtMySqlPassowrd.Name = "txtMySqlPassowrd";
            this.txtMySqlPassowrd.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlPassowrd.SelectedText = "";
            this.txtMySqlPassowrd.Size = new System.Drawing.Size(206, 23);
            this.txtMySqlPassowrd.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMySqlPassowrd.StyleManager = null;
            this.txtMySqlPassowrd.TabIndex = 30;
            this.txtMySqlPassowrd.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMySqlPassowrd.UseStyleColors = true;
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlUser.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMySqlUser.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMySqlUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlUser.Location = new System.Drawing.Point(237, 326);
            this.txtMySqlUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlUser.Multiline = true;
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlUser.SelectedText = "";
            this.txtMySqlUser.Size = new System.Drawing.Size(206, 23);
            this.txtMySqlUser.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMySqlUser.StyleManager = null;
            this.txtMySqlUser.TabIndex = 22;
            this.txtMySqlUser.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMySqlUser.UseStyleColors = true;
            // 
            // txtMySqlServer
            // 
            this.txtMySqlServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlServer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlServer.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMySqlServer.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMySqlServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlServer.Location = new System.Drawing.Point(237, 284);
            this.txtMySqlServer.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlServer.Multiline = true;
            this.txtMySqlServer.Name = "txtMySqlServer";
            this.txtMySqlServer.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlServer.SelectedText = "";
            this.txtMySqlServer.Size = new System.Drawing.Size(206, 23);
            this.txtMySqlServer.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMySqlServer.StyleManager = null;
            this.txtMySqlServer.TabIndex = 21;
            this.txtMySqlServer.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMySqlServer.UseStyleColors = true;
            // 
            // lblMySqlServerSettings
            // 
            this.lblMySqlServerSettings.AutoSize = true;
            this.lblMySqlServerSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblMySqlServerSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySqlServerSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblMySqlServerSettings.Location = new System.Drawing.Point(226, 224);
            this.lblMySqlServerSettings.Name = "lblMySqlServerSettings";
            this.lblMySqlServerSettings.Size = new System.Drawing.Size(178, 21);
            this.lblMySqlServerSettings.TabIndex = 20;
            this.lblMySqlServerSettings.Text = "MySQL Server Settings";
            // 
            // tglStayInTray
            // 
            this.tglStayInTray.AutoSize = true;
            this.tglStayInTray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglStayInTray.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglStayInTray.Location = new System.Drawing.Point(480, 323);
            this.tglStayInTray.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglStayInTray.Name = "tglStayInTray";
            this.tglStayInTray.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglStayInTray.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglStayInTray.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglStayInTray.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglStayInTray.Size = new System.Drawing.Size(45, 22);
            this.tglStayInTray.SolidStyle = false;
            this.tglStayInTray.TabIndex = 2;
            this.tglStayInTray.UseVisualStyleBackColor = false;
            this.tglStayInTray.CheckedChanged += new System.EventHandler(this.TglStayInTray_CheckedChanged);
            // 
            // lblRestartServerOnCrash
            // 
            this.lblRestartServerOnCrash.AutoSize = true;
            this.lblRestartServerOnCrash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblRestartServerOnCrash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblRestartServerOnCrash.Location = new System.Drawing.Point(531, 465);
            this.lblRestartServerOnCrash.Name = "lblRestartServerOnCrash";
            this.lblRestartServerOnCrash.Size = new System.Drawing.Size(128, 15);
            this.lblRestartServerOnCrash.TabIndex = 49;
            this.lblRestartServerOnCrash.Text = "Restart server on crash.";
            // 
            // tglRestartOnCrash
            // 
            this.tglRestartOnCrash.AutoSize = true;
            this.tglRestartOnCrash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglRestartOnCrash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglRestartOnCrash.Location = new System.Drawing.Point(480, 462);
            this.tglRestartOnCrash.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglRestartOnCrash.Name = "tglRestartOnCrash";
            this.tglRestartOnCrash.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglRestartOnCrash.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglRestartOnCrash.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglRestartOnCrash.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglRestartOnCrash.Size = new System.Drawing.Size(45, 22);
            this.tglRestartOnCrash.SolidStyle = false;
            this.tglRestartOnCrash.TabIndex = 48;
            this.tglRestartOnCrash.UseVisualStyleBackColor = false;
            this.tglRestartOnCrash.CheckedChanged += new System.EventHandler(this.TglRestartOnCrash_CheckedChanged);
            // 
            // lblStartServerOnLaunch
            // 
            this.lblStartServerOnLaunch.AutoSize = true;
            this.lblStartServerOnLaunch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblStartServerOnLaunch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblStartServerOnLaunch.Location = new System.Drawing.Point(531, 438);
            this.lblStartServerOnLaunch.Name = "lblStartServerOnLaunch";
            this.lblStartServerOnLaunch.Size = new System.Drawing.Size(124, 15);
            this.lblStartServerOnLaunch.TabIndex = 47;
            this.lblStartServerOnLaunch.Text = "Start server on launch.";
            // 
            // tglStartServer
            // 
            this.tglStartServer.AutoSize = true;
            this.tglStartServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglStartServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglStartServer.Location = new System.Drawing.Point(480, 435);
            this.tglStartServer.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglStartServer.Name = "tglStartServer";
            this.tglStartServer.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglStartServer.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglStartServer.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglStartServer.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglStartServer.Size = new System.Drawing.Size(45, 22);
            this.tglStartServer.SolidStyle = false;
            this.tglStartServer.TabIndex = 46;
            this.tglStartServer.UseVisualStyleBackColor = false;
            this.tglStartServer.CheckedChanged += new System.EventHandler(this.TglStartServer_CheckedChanged);
            // 
            // lblStartWithWindows
            // 
            this.lblStartWithWindows.AutoSize = true;
            this.lblStartWithWindows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblStartWithWindows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblStartWithWindows.Location = new System.Drawing.Point(531, 410);
            this.lblStartWithWindows.Name = "lblStartWithWindows";
            this.lblStartWithWindows.Size = new System.Drawing.Size(112, 15);
            this.lblStartWithWindows.TabIndex = 45;
            this.lblStartWithWindows.Text = "Start with Windows.";
            // 
            // tglStartOnStartup
            // 
            this.tglStartOnStartup.AutoSize = true;
            this.tglStartOnStartup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglStartOnStartup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglStartOnStartup.Location = new System.Drawing.Point(480, 407);
            this.tglStartOnStartup.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglStartOnStartup.Name = "tglStartOnStartup";
            this.tglStartOnStartup.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglStartOnStartup.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglStartOnStartup.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglStartOnStartup.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglStartOnStartup.Size = new System.Drawing.Size(45, 22);
            this.tglStartOnStartup.SolidStyle = false;
            this.tglStartOnStartup.TabIndex = 44;
            this.tglStartOnStartup.UseVisualStyleBackColor = false;
            this.tglStartOnStartup.CheckedChanged += new System.EventHandler(this.TglStartOnStartup_CheckedChanged);
            // 
            // lblCores
            // 
            this.lblCores.AutoSize = true;
            this.lblCores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblCores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblCores.Location = new System.Drawing.Point(480, 269);
            this.lblCores.Name = "lblCores";
            this.lblCores.Size = new System.Drawing.Size(40, 15);
            this.lblCores.TabIndex = 42;
            this.lblCores.Text = "Cores:";
            // 
            // lblHideControls
            // 
            this.lblHideControls.AutoSize = true;
            this.lblHideControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblHideControls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblHideControls.Location = new System.Drawing.Point(531, 382);
            this.lblHideControls.Name = "lblHideControls";
            this.lblHideControls.Size = new System.Drawing.Size(79, 15);
            this.lblHideControls.TabIndex = 41;
            this.lblHideControls.Text = "Hide console.";
            // 
            // tglHideConsole
            // 
            this.tglHideConsole.AutoSize = true;
            this.tglHideConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglHideConsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglHideConsole.Location = new System.Drawing.Point(480, 379);
            this.tglHideConsole.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglHideConsole.Name = "tglHideConsole";
            this.tglHideConsole.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglHideConsole.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglHideConsole.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglHideConsole.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglHideConsole.Size = new System.Drawing.Size(45, 22);
            this.tglHideConsole.SolidStyle = false;
            this.tglHideConsole.TabIndex = 40;
            this.tglHideConsole.UseVisualStyleBackColor = false;
            // 
            // lblNotySound
            // 
            this.lblNotySound.AutoSize = true;
            this.lblNotySound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblNotySound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblNotySound.Location = new System.Drawing.Point(531, 354);
            this.lblNotySound.Name = "lblNotySound";
            this.lblNotySound.Size = new System.Drawing.Size(109, 15);
            this.lblNotySound.TabIndex = 39;
            this.lblNotySound.Text = "Notification sound.";
            // 
            // tglNotySound
            // 
            this.tglNotySound.AutoSize = true;
            this.tglNotySound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglNotySound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglNotySound.Location = new System.Drawing.Point(480, 351);
            this.tglNotySound.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglNotySound.Name = "tglNotySound";
            this.tglNotySound.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglNotySound.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglNotySound.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglNotySound.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglNotySound.Size = new System.Drawing.Size(45, 22);
            this.tglNotySound.SolidStyle = false;
            this.tglNotySound.TabIndex = 38;
            this.tglNotySound.UseVisualStyleBackColor = false;
            // 
            // lblStayTray
            // 
            this.lblStayTray.AutoSize = true;
            this.lblStayTray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblStayTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblStayTray.Location = new System.Drawing.Point(531, 326);
            this.lblStayTray.Name = "lblStayTray";
            this.lblStayTray.Size = new System.Drawing.Size(68, 15);
            this.lblStayTray.TabIndex = 36;
            this.lblStayTray.Text = "Stay in tray.";
            // 
            // lblControlPanelSettings
            // 
            this.lblControlPanelSettings.AutoSize = true;
            this.lblControlPanelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblControlPanelSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblControlPanelSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblControlPanelSettings.Location = new System.Drawing.Point(476, 224);
            this.lblControlPanelSettings.Name = "lblControlPanelSettings";
            this.lblControlPanelSettings.Size = new System.Drawing.Size(172, 21);
            this.lblControlPanelSettings.TabIndex = 21;
            this.lblControlPanelSettings.Text = "Control Panel Settings";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnSave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSave.BorderRadius = 0;
            this.btnSave.BorderSize = 1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(606, 506);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblCustomNames
            // 
            this.lblCustomNames.AutoSize = true;
            this.lblCustomNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblCustomNames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblCustomNames.Location = new System.Drawing.Point(81, 404);
            this.lblCustomNames.Name = "lblCustomNames";
            this.lblCustomNames.Size = new System.Drawing.Size(92, 15);
            this.lblCustomNames.TabIndex = 45;
            this.lblCustomNames.Text = "Custom Names.";
            // 
            // tglCustomNames
            // 
            this.tglCustomNames.AutoSize = true;
            this.tglCustomNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglCustomNames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglCustomNames.Location = new System.Drawing.Point(30, 401);
            this.tglCustomNames.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglCustomNames.Name = "tglCustomNames";
            this.tglCustomNames.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglCustomNames.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglCustomNames.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglCustomNames.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglCustomNames.Size = new System.Drawing.Size(45, 22);
            this.tglCustomNames.SolidStyle = false;
            this.tglCustomNames.TabIndex = 44;
            this.tglCustomNames.UseVisualStyleBackColor = false;
            // 
            // txtMysqlName
            // 
            this.txtMysqlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMysqlName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMysqlName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMysqlName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMysqlName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMysqlName.Location = new System.Drawing.Point(29, 363);
            this.txtMysqlName.Margin = new System.Windows.Forms.Padding(0);
            this.txtMysqlName.Multiline = true;
            this.txtMysqlName.Name = "txtMysqlName";
            this.txtMysqlName.Padding = new System.Windows.Forms.Padding(1);
            this.txtMysqlName.SelectedText = "";
            this.txtMysqlName.Size = new System.Drawing.Size(165, 23);
            this.txtMysqlName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMysqlName.StyleManager = null;
            this.txtMysqlName.TabIndex = 37;
            this.txtMysqlName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMysqlName.UseStyleColors = true;
            // 
            // lblNames
            // 
            this.lblNames.AutoSize = true;
            this.lblNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblNames.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNames.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblNames.Location = new System.Drawing.Point(15, 224);
            this.lblNames.Name = "lblNames";
            this.lblNames.Size = new System.Drawing.Size(60, 21);
            this.lblNames.TabIndex = 20;
            this.lblNames.Text = "Names";
            // 
            // txtWorldName
            // 
            this.txtWorldName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtWorldName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtWorldName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtWorldName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldName.Location = new System.Drawing.Point(29, 283);
            this.txtWorldName.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorldName.Multiline = true;
            this.txtWorldName.Name = "txtWorldName";
            this.txtWorldName.Padding = new System.Windows.Forms.Padding(1);
            this.txtWorldName.SelectedText = "";
            this.txtWorldName.Size = new System.Drawing.Size(165, 23);
            this.txtWorldName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtWorldName.StyleManager = null;
            this.txtWorldName.TabIndex = 31;
            this.txtWorldName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtWorldName.UseStyleColors = true;
            // 
            // txtBnetName
            // 
            this.txtBnetName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtBnetName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtBnetName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtBnetName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtBnetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtBnetName.Location = new System.Drawing.Point(29, 325);
            this.txtBnetName.Margin = new System.Windows.Forms.Padding(0);
            this.txtBnetName.Multiline = true;
            this.txtBnetName.Name = "txtBnetName";
            this.txtBnetName.Padding = new System.Windows.Forms.Padding(1);
            this.txtBnetName.SelectedText = "";
            this.txtBnetName.Size = new System.Drawing.Size(165, 23);
            this.txtBnetName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtBnetName.StyleManager = null;
            this.txtBnetName.TabIndex = 32;
            this.txtBnetName.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtBnetName.UseStyleColors = true;
            // 
            // TimerUpdate
            // 
            this.TimerUpdate.Enabled = true;
            this.TimerUpdate.Interval = 1000;
            this.TimerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // btnTestMySQL
            // 
            this.btnTestMySQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnTestMySQL.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnTestMySQL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTestMySQL.BorderRadius = 0;
            this.btnTestMySQL.BorderSize = 1;
            this.btnTestMySQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestMySQL.FlatAppearance.BorderSize = 0;
            this.btnTestMySQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestMySQL.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestMySQL.ForeColor = System.Drawing.Color.White;
            this.btnTestMySQL.Location = new System.Drawing.Point(456, 506);
            this.btnTestMySQL.Name = "btnTestMySQL";
            this.btnTestMySQL.Size = new System.Drawing.Size(144, 40);
            this.btnTestMySQL.TabIndex = 6;
            this.btnTestMySQL.Text = "Test Connection";
            this.btnTestMySQL.TextColor = System.Drawing.Color.White;
            this.btnTestMySQL.UseVisualStyleBackColor = false;
            this.btnTestMySQL.Click += new System.EventHandler(this.BtnTestMySQL_Click);
            // 
            // panelNames
            // 
            this.panelNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelNames.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.panelNames.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelNames.BorderSize = 1;
            this.panelNames.Edge = 0;
            this.panelNames.Location = new System.Drawing.Point(10, 219);
            this.panelNames.Name = "panelNames";
            this.panelNames.Padding = new System.Windows.Forms.Padding(1);
            this.panelNames.Size = new System.Drawing.Size(199, 275);
            this.panelNames.TabIndex = 51;
            // 
            // panelControSettins
            // 
            this.panelControSettins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelControSettins.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.panelControSettins.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panelControSettins.BorderSize = 1;
            this.panelControSettins.Edge = 0;
            this.panelControSettins.Location = new System.Drawing.Point(470, 219);
            this.panelControSettins.Name = "panelControSettins";
            this.panelControSettins.Padding = new System.Windows.Forms.Padding(1);
            this.panelControSettins.Size = new System.Drawing.Size(218, 276);
            this.panelControSettins.TabIndex = 52;
            // 
            // btnFixMysql
            // 
            this.btnFixMysql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnFixMysql.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnFixMysql.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnFixMysql.BorderRadius = 0;
            this.btnFixMysql.BorderSize = 1;
            this.btnFixMysql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFixMysql.FlatAppearance.BorderSize = 0;
            this.btnFixMysql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFixMysql.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFixMysql.ForeColor = System.Drawing.Color.White;
            this.btnFixMysql.Location = new System.Drawing.Point(322, 506);
            this.btnFixMysql.Name = "btnFixMysql";
            this.btnFixMysql.Size = new System.Drawing.Size(129, 40);
            this.btnFixMysql.TabIndex = 54;
            this.btnFixMysql.Text = "Fix MySQL";
            this.btnFixMysql.TextColor = System.Drawing.Color.White;
            this.btnFixMysql.UseVisualStyleBackColor = false;
            this.btnFixMysql.Click += new System.EventHandler(this.BtnFixMysql_Click);
            // 
            // PanelMySelSettings
            // 
            this.PanelMySelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.PanelMySelSettings.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.PanelMySelSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.PanelMySelSettings.BorderSize = 1;
            this.PanelMySelSettings.Edge = 0;
            this.PanelMySelSettings.Location = new System.Drawing.Point(221, 219);
            this.PanelMySelSettings.Name = "PanelMySelSettings";
            this.PanelMySelSettings.Padding = new System.Windows.Forms.Padding(1);
            this.PanelMySelSettings.Size = new System.Drawing.Size(237, 276);
            this.PanelMySelSettings.TabIndex = 53;
            // 
            // btCoreLocationSearch
            // 
            this.btCoreLocationSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btCoreLocationSearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btCoreLocationSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btCoreLocationSearch.BorderRadius = 0;
            this.btCoreLocationSearch.BorderSize = 1;
            this.btCoreLocationSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCoreLocationSearch.FlatAppearance.BorderSize = 0;
            this.btCoreLocationSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCoreLocationSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btCoreLocationSearch.ForeColor = System.Drawing.Color.White;
            this.btCoreLocationSearch.Location = new System.Drawing.Point(564, 71);
            this.btCoreLocationSearch.Name = "btCoreLocationSearch";
            this.btCoreLocationSearch.Size = new System.Drawing.Size(114, 23);
            this.btCoreLocationSearch.TabIndex = 1;
            this.btCoreLocationSearch.Text = "...";
            this.btCoreLocationSearch.TextColor = System.Drawing.Color.White;
            this.btCoreLocationSearch.UseVisualStyleBackColor = false;
            this.btCoreLocationSearch.Click += new System.EventHandler(this.BntLocation_Click);
            // 
            // lblPCresource
            // 
            this.lblPCresource.AutoSize = true;
            this.lblPCresource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPCresource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPCresource.Location = new System.Drawing.Point(3, 3);
            this.lblPCresource.Name = "lblPCresource";
            this.lblPCresource.Size = new System.Drawing.Size(125, 21);
            this.lblPCresource.TabIndex = 20;
            this.lblPCresource.Text = "Server Location";
            // 
            // txtWorldLocation
            // 
            this.txtWorldLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtWorldLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtWorldLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtWorldLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldLocation.Location = new System.Drawing.Point(17, 39);
            this.txtWorldLocation.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorldLocation.Multiline = true;
            this.txtWorldLocation.Name = "txtWorldLocation";
            this.txtWorldLocation.Padding = new System.Windows.Forms.Padding(1);
            this.txtWorldLocation.SelectedText = "";
            this.txtWorldLocation.Size = new System.Drawing.Size(545, 23);
            this.txtWorldLocation.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtWorldLocation.StyleManager = null;
            this.txtWorldLocation.TabIndex = 21;
            this.txtWorldLocation.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtWorldLocation.UseStyleColors = false;
            // 
            // bntOpenLocation
            // 
            this.bntOpenLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntOpenLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntOpenLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntOpenLocation.BorderRadius = 3;
            this.bntOpenLocation.BorderSize = 1;
            this.bntOpenLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntOpenLocation.FlatAppearance.BorderSize = 0;
            this.bntOpenLocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bntOpenLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntOpenLocation.ForeColor = System.Drawing.Color.White;
            this.bntOpenLocation.Location = new System.Drawing.Point(128, 160);
            this.bntOpenLocation.Name = "bntOpenLocation";
            this.bntOpenLocation.Size = new System.Drawing.Size(105, 30);
            this.bntOpenLocation.TabIndex = 30;
            this.bntOpenLocation.Text = "Core Location";
            this.bntOpenLocation.TextColor = System.Drawing.Color.White;
            this.bntOpenLocation.UseVisualStyleBackColor = false;
            this.bntOpenLocation.Click += new System.EventHandler(this.BntOpenLocation_Click);
            // 
            // bntOpenMySqlLocation
            // 
            this.bntOpenMySqlLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntOpenMySqlLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntOpenMySqlLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntOpenMySqlLocation.BorderRadius = 3;
            this.bntOpenMySqlLocation.BorderSize = 1;
            this.bntOpenMySqlLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntOpenMySqlLocation.FlatAppearance.BorderSize = 0;
            this.bntOpenMySqlLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOpenMySqlLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntOpenMySqlLocation.ForeColor = System.Drawing.Color.White;
            this.bntOpenMySqlLocation.Location = new System.Drawing.Point(17, 160);
            this.bntOpenMySqlLocation.Name = "bntOpenMySqlLocation";
            this.bntOpenMySqlLocation.Size = new System.Drawing.Size(105, 30);
            this.bntOpenMySqlLocation.TabIndex = 7;
            this.bntOpenMySqlLocation.Text = "MySQL Location";
            this.bntOpenMySqlLocation.TextColor = System.Drawing.Color.White;
            this.bntOpenMySqlLocation.UseVisualStyleBackColor = false;
            this.bntOpenMySqlLocation.Click += new System.EventHandler(this.BntMySqlLocation_Click);
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCore.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.comboBoxCore.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.comboBoxCore.FormattingEnabled = true;
            this.comboBoxCore.ItemHeight = 23;
            this.comboBoxCore.Items.AddRange(new object[] {
            "AscEmu ",
            "AzerothCore ",
            "Continued MaNGOS ",
            "CypherCore",
            "TrinityCore ",
            "TrinityCore 4.3.4 (TCPP)",
            "Vanilla MaNGOS "});
            this.comboBoxCore.Location = new System.Drawing.Point(480, 284);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Size = new System.Drawing.Size(199, 29);
            this.comboBoxCore.Style = MetroFramework.MetroColorStyle.Blue;
            this.comboBoxCore.StyleManager = null;
            this.comboBoxCore.TabIndex = 55;
            this.comboBoxCore.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // PanelServerLocation
            // 
            this.PanelServerLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.PanelServerLocation.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.PanelServerLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.PanelServerLocation.BorderSize = 1;
            this.PanelServerLocation.Edge = 0;
            this.PanelServerLocation.Location = new System.Drawing.Point(10, 15);
            this.PanelServerLocation.Name = "PanelServerLocation";
            this.PanelServerLocation.Padding = new System.Windows.Forms.Padding(1);
            this.PanelServerLocation.Size = new System.Drawing.Size(677, 193);
            this.PanelServerLocation.TabIndex = 61;
            // 
            // txtCoreLocation
            // 
            this.txtCoreLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtCoreLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtCoreLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtCoreLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtCoreLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtCoreLocation.Location = new System.Drawing.Point(29, 71);
            this.txtCoreLocation.Margin = new System.Windows.Forms.Padding(0);
            this.txtCoreLocation.Multiline = true;
            this.txtCoreLocation.Name = "txtCoreLocation";
            this.txtCoreLocation.Padding = new System.Windows.Forms.Padding(1);
            this.txtCoreLocation.SelectedText = "";
            this.txtCoreLocation.Size = new System.Drawing.Size(532, 23);
            this.txtCoreLocation.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCoreLocation.StyleManager = null;
            this.txtCoreLocation.TabIndex = 62;
            this.txtCoreLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtCoreLocation.UseStyleColors = true;
            // 
            // txtMysqlLocation
            // 
            this.txtMysqlLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMysqlLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMysqlLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            this.txtMysqlLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            this.txtMysqlLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMysqlLocation.Location = new System.Drawing.Point(29, 123);
            this.txtMysqlLocation.Margin = new System.Windows.Forms.Padding(0);
            this.txtMysqlLocation.Multiline = true;
            this.txtMysqlLocation.Name = "txtMysqlLocation";
            this.txtMysqlLocation.Padding = new System.Windows.Forms.Padding(1);
            this.txtMysqlLocation.SelectedText = "";
            this.txtMysqlLocation.Size = new System.Drawing.Size(533, 23);
            this.txtMysqlLocation.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMysqlLocation.StyleManager = null;
            this.txtMysqlLocation.TabIndex = 63;
            this.txtMysqlLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtMysqlLocation.UseStyleColors = true;
            // 
            // btnMySQLLocationSearch
            // 
            this.btnMySQLLocationSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnMySQLLocationSearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnMySQLLocationSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnMySQLLocationSearch.BorderRadius = 0;
            this.btnMySQLLocationSearch.BorderSize = 1;
            this.btnMySQLLocationSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMySQLLocationSearch.FlatAppearance.BorderSize = 0;
            this.btnMySQLLocationSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMySQLLocationSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMySQLLocationSearch.ForeColor = System.Drawing.Color.White;
            this.btnMySQLLocationSearch.Location = new System.Drawing.Point(565, 123);
            this.btnMySQLLocationSearch.Name = "btnMySQLLocationSearch";
            this.btnMySQLLocationSearch.Size = new System.Drawing.Size(114, 23);
            this.btnMySQLLocationSearch.TabIndex = 64;
            this.btnMySQLLocationSearch.Text = "...";
            this.btnMySQLLocationSearch.TextColor = System.Drawing.Color.White;
            this.btnMySQLLocationSearch.UseVisualStyleBackColor = false;
            this.btnMySQLLocationSearch.Click += new System.EventHandler(this.BtnMysqlLocation_Click);
            // 
            // lblServerLocation
            // 
            this.lblServerLocation.AutoSize = true;
            this.lblServerLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblServerLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblServerLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblServerLocation.Location = new System.Drawing.Point(15, 20);
            this.lblServerLocation.Name = "lblServerLocation";
            this.lblServerLocation.Size = new System.Drawing.Size(125, 21);
            this.lblServerLocation.TabIndex = 65;
            this.lblServerLocation.Text = "Server Location";
            // 
            // btnCoreLocation
            // 
            this.btnCoreLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnCoreLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnCoreLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCoreLocation.BorderRadius = 0;
            this.btnCoreLocation.BorderSize = 1;
            this.btnCoreLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCoreLocation.FlatAppearance.BorderSize = 0;
            this.btnCoreLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCoreLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCoreLocation.ForeColor = System.Drawing.Color.White;
            this.btnCoreLocation.Location = new System.Drawing.Point(30, 162);
            this.btnCoreLocation.Name = "btnCoreLocation";
            this.btnCoreLocation.Size = new System.Drawing.Size(128, 29);
            this.btnCoreLocation.TabIndex = 68;
            this.btnCoreLocation.Text = "Core Location";
            this.btnCoreLocation.TextColor = System.Drawing.Color.White;
            this.btnCoreLocation.UseVisualStyleBackColor = false;
            // 
            // btnMysqlOpenLocation
            // 
            this.btnMysqlOpenLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnMysqlOpenLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnMysqlOpenLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnMysqlOpenLocation.BorderRadius = 0;
            this.btnMysqlOpenLocation.BorderSize = 1;
            this.btnMysqlOpenLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMysqlOpenLocation.FlatAppearance.BorderSize = 0;
            this.btnMysqlOpenLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMysqlOpenLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMysqlOpenLocation.ForeColor = System.Drawing.Color.White;
            this.btnMysqlOpenLocation.Location = new System.Drawing.Point(166, 162);
            this.btnMysqlOpenLocation.Name = "btnMysqlOpenLocation";
            this.btnMysqlOpenLocation.Size = new System.Drawing.Size(128, 29);
            this.btnMysqlOpenLocation.TabIndex = 69;
            this.btnMysqlOpenLocation.Text = "MySQL Location";
            this.btnMysqlOpenLocation.TextColor = System.Drawing.Color.White;
            this.btnMysqlOpenLocation.UseVisualStyleBackColor = false;
            this.btnMysqlOpenLocation.Click += new System.EventHandler(this.BtnMysqlOpenLocation_Click);
            // 
            // SettingControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Controls.Add(this.btnMysqlOpenLocation);
            this.Controls.Add(this.btnCoreLocation);
            this.Controls.Add(lblCoreLocation);
            this.Controls.Add(lblMysqlLocation);
            this.Controls.Add(this.lblServerLocation);
            this.Controls.Add(this.btnMySQLLocationSearch);
            this.Controls.Add(this.txtMysqlLocation);
            this.Controls.Add(this.txtCoreLocation);
            this.Controls.Add(lblServer);
            this.Controls.Add(lblUser);
            this.Controls.Add(lblPassword);
            this.Controls.Add(lblPort);
            this.Controls.Add(lblAuth);
            this.Controls.Add(this.comboBoxCore);
            this.Controls.Add(this.lblRestartServerOnCrash);
            this.Controls.Add(this.tglRestartOnCrash);
            this.Controls.Add(this.lblStartServerOnLaunch);
            this.Controls.Add(this.tglStartServer);
            this.Controls.Add(this.lblStartWithWindows);
            this.Controls.Add(this.tglStartOnStartup);
            this.Controls.Add(this.lblCores);
            this.Controls.Add(this.lblHideControls);
            this.Controls.Add(this.tglHideConsole);
            this.Controls.Add(this.lblNotySound);
            this.Controls.Add(this.tglNotySound);
            this.Controls.Add(this.lblStayTray);
            this.Controls.Add(this.lblControlPanelSettings);
            this.Controls.Add(this.tglStayInTray);
            this.Controls.Add(this.txtAuthDatabase);
            this.Controls.Add(this.lblCustomNames);
            this.Controls.Add(this.btnFixMysql);
            this.Controls.Add(this.tglCustomNames);
            this.Controls.Add(lblMysqlName);
            this.Controls.Add(this.txtMySqlPort);
            this.Controls.Add(this.txtMysqlName);
            this.Controls.Add(lblWorldName);
            this.Controls.Add(this.txtMySqlPassowrd);
            this.Controls.Add(this.lblNames);
            this.Controls.Add(this.panelControSettins);
            this.Controls.Add(this.txtWorldName);
            this.Controls.Add(this.btCoreLocationSearch);
            this.Controls.Add(lblBnetName);
            this.Controls.Add(this.txtMySqlUser);
            this.Controls.Add(this.txtBnetName);
            this.Controls.Add(this.panelNames);
            this.Controls.Add(this.txtMySqlServer);
            this.Controls.Add(this.lblMySqlServerSettings);
            this.Controls.Add(this.btnTestMySQL);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.PanelMySelSettings);
            this.Controls.Add(this.PanelServerLocation);
            this.Name = "SettingControl";
            this.Size = new System.Drawing.Size(700, 560);
            this.Load += new System.EventHandler(this.SettingControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion
        private Label lblMySqlServerSettings;
        private CustomToggleButton tglStayInTray;
        private Label lblControlPanelSettings;
        private Label lblStayTray;
        private Label lblNotySound;
        private CustomToggleButton tglNotySound;
        private CustomButton btnSave;
        private Label lblHideControls;
        private CustomToggleButton tglHideConsole;
        private Label lblNames;
        private MetroFramework.Controls.MetroTextBox txtMysqlName;
        private MetroFramework.Controls.MetroTextBox txtBnetName;
        private MetroFramework.Controls.MetroTextBox txtWorldName;
        private Label lblCores;
        private Label lblCustomNames;
        private CustomToggleButton tglCustomNames;
        private System.Windows.Forms.Timer TimerUpdate;
        private CustomButton btnTestMySQL;
        private CustomPanel panelNames;
        private CustomPanel panelControSettins;
        private CustomButton btnFixMysql;
        private Label lblRestartServerOnCrash;
        private CustomToggleButton tglRestartOnCrash;
        private Label lblStartServerOnLaunch;
        private CustomToggleButton tglStartServer;
        private Label lblStartWithWindows;
        private CustomToggleButton tglStartOnStartup;
        private CustomPanel PanelMySelSettings;
        private CustomButton btCoreLocationSearch;
        private Label lblPCresource;
        private MetroFramework.Controls.MetroTextBox txtWorldLocation;
        private CustomButton bntOpenLocation;
        private CustomButton bntOpenMySqlLocation;
        private MetroFramework.Controls.MetroComboBox comboBoxCore;
        private CustomPanel PanelServerLocation;
        internal MetroFramework.Controls.MetroTextBox txtCoreLocation;
        internal MetroFramework.Controls.MetroTextBox txtMysqlLocation;
        private CustomButton btnMySQLLocationSearch;
        private Label lblServerLocation;
        private CustomButton btnCoreLocation;
        private CustomButton btnMysqlOpenLocation;
        public MetroFramework.Controls.MetroTextBox txtAuthDatabase;
        public MetroFramework.Controls.MetroTextBox txtMySqlPort;
        public MetroFramework.Controls.MetroTextBox txtMySqlPassowrd;
        public MetroFramework.Controls.MetroTextBox txtMySqlUser;
        public MetroFramework.Controls.MetroTextBox txtMySqlServer;

    }
}
