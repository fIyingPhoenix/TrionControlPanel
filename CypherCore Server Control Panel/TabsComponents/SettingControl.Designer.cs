namespace CypherCore_Server_Laucher.TabsComponents
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblWorldLocation;
            this.roundPanel1 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.bntOpenLocation = new CypherCore_Server_Laucher.UI.CustomButton();
            this.bntLocation = new CypherCore_Server_Laucher.UI.CustomButton();
            this.lblBnetLocation = new System.Windows.Forms.Label();
            this.txtBnetLocation = new System.Windows.Forms.TextBox();
            this.txtWorldLocation = new System.Windows.Forms.TextBox();
            this.lblPCresource = new System.Windows.Forms.Label();
            this.roundPanel2 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtMySqlPort = new System.Windows.Forms.TextBox();
            this.txtMySqlPassowrd = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtMySqlUser = new System.Windows.Forms.TextBox();
            this.txtMySqlServer = new System.Windows.Forms.TextBox();
            this.lblMySqlServerSettings = new System.Windows.Forms.Label();
            this.tglStayInTray = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.roundPanel3 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.tglHideConsole = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.label11 = new System.Windows.Forms.Label();
            this.tglNotySound = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.label10 = new System.Windows.Forms.Label();
            this.lblControlPanelSettings = new System.Windows.Forms.Label();
            this.roundPanel4 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.lblAuth = new System.Windows.Forms.Label();
            this.lblDatabaseSettings = new System.Windows.Forms.Label();
            this.txtAuthDatabase = new System.Windows.Forms.TextBox();
            this.txtWorldDatabase = new System.Windows.Forms.TextBox();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.txtCharactersDatabase = new System.Windows.Forms.TextBox();
            this.lblWorld = new System.Windows.Forms.Label();
            this.btnSave = new CypherCore_Server_Laucher.UI.CustomButton();
            lblWorldLocation = new System.Windows.Forms.Label();
            this.roundPanel1.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.roundPanel3.SuspendLayout();
            this.roundPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWorldLocation
            // 
            lblWorldLocation.AutoSize = true;
            lblWorldLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblWorldLocation.Location = new System.Drawing.Point(20, 35);
            lblWorldLocation.Name = "lblWorldLocation";
            lblWorldLocation.Size = new System.Drawing.Size(91, 15);
            lblWorldLocation.TabIndex = 29;
            lblWorldLocation.Text = "World Lcoation:";
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel1.BorderColor = System.Drawing.Color.White;
            this.roundPanel1.Controls.Add(this.bntOpenLocation);
            this.roundPanel1.Controls.Add(this.bntLocation);
            this.roundPanel1.Controls.Add(this.lblBnetLocation);
            this.roundPanel1.Controls.Add(lblWorldLocation);
            this.roundPanel1.Controls.Add(this.txtBnetLocation);
            this.roundPanel1.Controls.Add(this.txtWorldLocation);
            this.roundPanel1.Controls.Add(this.lblPCresource);
            this.roundPanel1.Edge = 20;
            this.roundPanel1.Location = new System.Drawing.Point(14, 12);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Size = new System.Drawing.Size(667, 178);
            this.roundPanel1.TabIndex = 0;
            // 
            // bntOpenLocation
            // 
            this.bntOpenLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntOpenLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntOpenLocation.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.bntOpenLocation.BorderRadius = 5;
            this.bntOpenLocation.BorderSize = 0;
            this.bntOpenLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntOpenLocation.FlatAppearance.BorderSize = 0;
            this.bntOpenLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOpenLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntOpenLocation.ForeColor = System.Drawing.Color.White;
            this.bntOpenLocation.Location = new System.Drawing.Point(410, 130);
            this.bntOpenLocation.Name = "bntOpenLocation";
            this.bntOpenLocation.Size = new System.Drawing.Size(120, 35);
            this.bntOpenLocation.TabIndex = 30;
            this.bntOpenLocation.Text = "Open Location";
            this.bntOpenLocation.TextColor = System.Drawing.Color.White;
            this.bntOpenLocation.UseVisualStyleBackColor = false;
            this.bntOpenLocation.Click += new System.EventHandler(this.bntOpenLocation_Click);
            // 
            // bntLocation
            // 
            this.bntLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntLocation.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.bntLocation.BorderRadius = 5;
            this.bntLocation.BorderSize = 0;
            this.bntLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntLocation.FlatAppearance.BorderSize = 0;
            this.bntLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntLocation.ForeColor = System.Drawing.Color.White;
            this.bntLocation.Location = new System.Drawing.Point(536, 130);
            this.bntLocation.Name = "bntLocation";
            this.bntLocation.Size = new System.Drawing.Size(120, 35);
            this.bntLocation.TabIndex = 1;
            this.bntLocation.Text = "Search Location";
            this.bntLocation.TextColor = System.Drawing.Color.White;
            this.bntLocation.UseVisualStyleBackColor = false;
            this.bntLocation.Click += new System.EventHandler(this.bntLocation_Click);
            // 
            // lblBnetLocation
            // 
            this.lblBnetLocation.AutoSize = true;
            this.lblBnetLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnetLocation.Location = new System.Drawing.Point(20, 81);
            this.lblBnetLocation.Name = "lblBnetLocation";
            this.lblBnetLocation.Size = new System.Drawing.Size(114, 15);
            this.lblBnetLocation.TabIndex = 29;
            this.lblBnetLocation.Text = "Bnet/Auth Location:";
            // 
            // txtBnetLocation
            // 
            this.txtBnetLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtBnetLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBnetLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtBnetLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtBnetLocation.Location = new System.Drawing.Point(20, 99);
            this.txtBnetLocation.Multiline = true;
            this.txtBnetLocation.Name = "txtBnetLocation";
            this.txtBnetLocation.ReadOnly = true;
            this.txtBnetLocation.Size = new System.Drawing.Size(636, 23);
            this.txtBnetLocation.TabIndex = 22;
            this.txtBnetLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWorldLocation
            // 
            this.txtWorldLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWorldLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtWorldLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldLocation.Location = new System.Drawing.Point(20, 53);
            this.txtWorldLocation.Multiline = true;
            this.txtWorldLocation.Name = "txtWorldLocation";
            this.txtWorldLocation.ReadOnly = true;
            this.txtWorldLocation.Size = new System.Drawing.Size(636, 23);
            this.txtWorldLocation.TabIndex = 21;
            this.txtWorldLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPCresource
            // 
            this.lblPCresource.AutoSize = true;
            this.lblPCresource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPCresource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPCresource.Location = new System.Drawing.Point(7, 7);
            this.lblPCresource.Name = "lblPCresource";
            this.lblPCresource.Size = new System.Drawing.Size(125, 21);
            this.lblPCresource.TabIndex = 20;
            this.lblPCresource.Text = "Server Location";
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel2.BorderColor = System.Drawing.Color.White;
            this.roundPanel2.Controls.Add(this.lblPort);
            this.roundPanel2.Controls.Add(this.lblPassword);
            this.roundPanel2.Controls.Add(this.txtMySqlPort);
            this.roundPanel2.Controls.Add(this.txtMySqlPassowrd);
            this.roundPanel2.Controls.Add(this.lblUser);
            this.roundPanel2.Controls.Add(this.lblServer);
            this.roundPanel2.Controls.Add(this.txtMySqlUser);
            this.roundPanel2.Controls.Add(this.txtMySqlServer);
            this.roundPanel2.Controls.Add(this.lblMySqlServerSettings);
            this.roundPanel2.Edge = 20;
            this.roundPanel2.Location = new System.Drawing.Point(14, 208);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Size = new System.Drawing.Size(202, 287);
            this.roundPanel2.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPort.Location = new System.Drawing.Point(17, 188);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 15);
            this.lblPort.TabIndex = 32;
            this.lblPort.Text = "Port:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPassword.Location = new System.Drawing.Point(17, 142);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(60, 15);
            this.lblPassword.TabIndex = 33;
            this.lblPassword.Text = "Password:";
            // 
            // txtMySqlPort
            // 
            this.txtMySqlPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMySqlPort.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPort.Location = new System.Drawing.Point(17, 206);
            this.txtMySqlPort.Multiline = true;
            this.txtMySqlPort.Name = "txtMySqlPort";
            this.txtMySqlPort.PlaceholderText = "3306";
            this.txtMySqlPort.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlPort.TabIndex = 31;
            this.txtMySqlPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMySqlPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMySqlPort_KeyPress);
            // 
            // txtMySqlPassowrd
            // 
            this.txtMySqlPassowrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlPassowrd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMySqlPassowrd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPassowrd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPassowrd.Location = new System.Drawing.Point(17, 160);
            this.txtMySqlPassowrd.Multiline = true;
            this.txtMySqlPassowrd.Name = "txtMySqlPassowrd";
            this.txtMySqlPassowrd.PlaceholderText = "CypherCore";
            this.txtMySqlPassowrd.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlPassowrd.TabIndex = 30;
            this.txtMySqlPassowrd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblUser.Location = new System.Drawing.Point(17, 98);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(33, 15);
            this.lblUser.TabIndex = 29;
            this.lblUser.Text = "User:";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblServer.Location = new System.Drawing.Point(17, 52);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(42, 15);
            this.lblServer.TabIndex = 29;
            this.lblServer.Text = "Server:";
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMySqlUser.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMySqlUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlUser.Location = new System.Drawing.Point(17, 116);
            this.txtMySqlUser.Multiline = true;
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.PlaceholderText = "CypherCore";
            this.txtMySqlUser.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlUser.TabIndex = 22;
            this.txtMySqlUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMySqlServer
            // 
            this.txtMySqlServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMySqlServer.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMySqlServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlServer.Location = new System.Drawing.Point(17, 70);
            this.txtMySqlServer.Multiline = true;
            this.txtMySqlServer.Name = "txtMySqlServer";
            this.txtMySqlServer.PlaceholderText = "localhost";
            this.txtMySqlServer.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlServer.TabIndex = 21;
            this.txtMySqlServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMySqlServerSettings
            // 
            this.lblMySqlServerSettings.AutoSize = true;
            this.lblMySqlServerSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySqlServerSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblMySqlServerSettings.Location = new System.Drawing.Point(14, 7);
            this.lblMySqlServerSettings.Name = "lblMySqlServerSettings";
            this.lblMySqlServerSettings.Size = new System.Drawing.Size(172, 21);
            this.lblMySqlServerSettings.TabIndex = 20;
            this.lblMySqlServerSettings.Text = "MySql Server Settings";
            // 
            // tglStayInTray
            // 
            this.tglStayInTray.AutoSize = true;
            this.tglStayInTray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglStayInTray.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglStayInTray.Location = new System.Drawing.Point(17, 71);
            this.tglStayInTray.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglStayInTray.Name = "tglStayInTray";
            this.tglStayInTray.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglStayInTray.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglStayInTray.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglStayInTray.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglStayInTray.Size = new System.Drawing.Size(45, 22);
            this.tglStayInTray.TabIndex = 2;
            this.tglStayInTray.UseVisualStyleBackColor = false;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel3.BorderColor = System.Drawing.Color.White;
            this.roundPanel3.Controls.Add(this.label16);
            this.roundPanel3.Controls.Add(this.tglHideConsole);
            this.roundPanel3.Controls.Add(this.label11);
            this.roundPanel3.Controls.Add(this.tglNotySound);
            this.roundPanel3.Controls.Add(this.label10);
            this.roundPanel3.Controls.Add(this.lblControlPanelSettings);
            this.roundPanel3.Controls.Add(this.tglStayInTray);
            this.roundPanel3.Edge = 20;
            this.roundPanel3.Location = new System.Drawing.Point(452, 208);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Size = new System.Drawing.Size(229, 287);
            this.roundPanel3.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label16.Location = new System.Drawing.Point(68, 131);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 15);
            this.label16.TabIndex = 41;
            this.label16.Text = "Hide console.";
            // 
            // tglHideConsole
            // 
            this.tglHideConsole.AutoSize = true;
            this.tglHideConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglHideConsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglHideConsole.Location = new System.Drawing.Point(17, 127);
            this.tglHideConsole.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglHideConsole.Name = "tglHideConsole";
            this.tglHideConsole.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglHideConsole.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglHideConsole.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglHideConsole.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglHideConsole.Size = new System.Drawing.Size(45, 22);
            this.tglHideConsole.TabIndex = 40;
            this.tglHideConsole.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label11.Location = new System.Drawing.Point(68, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 15);
            this.label11.TabIndex = 39;
            this.label11.Text = "Notification sound.";
            // 
            // tglNotySound
            // 
            this.tglNotySound.AutoSize = true;
            this.tglNotySound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglNotySound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglNotySound.Location = new System.Drawing.Point(17, 99);
            this.tglNotySound.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglNotySound.Name = "tglNotySound";
            this.tglNotySound.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglNotySound.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglNotySound.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglNotySound.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglNotySound.Size = new System.Drawing.Size(45, 22);
            this.tglNotySound.TabIndex = 38;
            this.tglNotySound.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label10.Location = new System.Drawing.Point(68, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 15);
            this.label10.TabIndex = 36;
            this.label10.Text = "Stay in tray.";
            // 
            // lblControlPanelSettings
            // 
            this.lblControlPanelSettings.AutoSize = true;
            this.lblControlPanelSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblControlPanelSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblControlPanelSettings.Location = new System.Drawing.Point(7, 7);
            this.lblControlPanelSettings.Name = "lblControlPanelSettings";
            this.lblControlPanelSettings.Size = new System.Drawing.Size(172, 21);
            this.lblControlPanelSettings.TabIndex = 21;
            this.lblControlPanelSettings.Text = "Control Panel Settings";
            // 
            // roundPanel4
            // 
            this.roundPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel4.BorderColor = System.Drawing.Color.White;
            this.roundPanel4.Controls.Add(this.lblAuth);
            this.roundPanel4.Controls.Add(this.lblDatabaseSettings);
            this.roundPanel4.Controls.Add(this.txtAuthDatabase);
            this.roundPanel4.Controls.Add(this.txtWorldDatabase);
            this.roundPanel4.Controls.Add(this.lblCharacters);
            this.roundPanel4.Controls.Add(this.txtCharactersDatabase);
            this.roundPanel4.Controls.Add(this.lblWorld);
            this.roundPanel4.Edge = 20;
            this.roundPanel4.Location = new System.Drawing.Point(234, 208);
            this.roundPanel4.Name = "roundPanel4";
            this.roundPanel4.Size = new System.Drawing.Size(202, 287);
            this.roundPanel4.TabIndex = 4;
            // 
            // lblAuth
            // 
            this.lblAuth.AutoSize = true;
            this.lblAuth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblAuth.Location = new System.Drawing.Point(17, 142);
            this.lblAuth.Name = "lblAuth";
            this.lblAuth.Size = new System.Drawing.Size(36, 15);
            this.lblAuth.TabIndex = 41;
            this.lblAuth.Text = "Auth:";
            this.lblAuth.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDatabaseSettings
            // 
            this.lblDatabaseSettings.AutoSize = true;
            this.lblDatabaseSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDatabaseSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblDatabaseSettings.Location = new System.Drawing.Point(7, 7);
            this.lblDatabaseSettings.Name = "lblDatabaseSettings";
            this.lblDatabaseSettings.Size = new System.Drawing.Size(141, 21);
            this.lblDatabaseSettings.TabIndex = 21;
            this.lblDatabaseSettings.Text = "Database Settings";
            // 
            // txtAuthDatabase
            // 
            this.txtAuthDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtAuthDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAuthDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtAuthDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtAuthDatabase.Location = new System.Drawing.Point(17, 160);
            this.txtAuthDatabase.Multiline = true;
            this.txtAuthDatabase.Name = "txtAuthDatabase";
            this.txtAuthDatabase.PlaceholderText = "Auth";
            this.txtAuthDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtAuthDatabase.TabIndex = 40;
            this.txtAuthDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWorldDatabase
            // 
            this.txtWorldDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWorldDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtWorldDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldDatabase.Location = new System.Drawing.Point(17, 70);
            this.txtWorldDatabase.Multiline = true;
            this.txtWorldDatabase.Name = "txtWorldDatabase";
            this.txtWorldDatabase.PlaceholderText = "World";
            this.txtWorldDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtWorldDatabase.TabIndex = 36;
            this.txtWorldDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblCharacters.Location = new System.Drawing.Point(17, 98);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(63, 15);
            this.lblCharacters.TabIndex = 38;
            this.lblCharacters.Text = "Characters";
            // 
            // txtCharactersDatabase
            // 
            this.txtCharactersDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtCharactersDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharactersDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtCharactersDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtCharactersDatabase.Location = new System.Drawing.Point(17, 116);
            this.txtCharactersDatabase.Multiline = true;
            this.txtCharactersDatabase.Name = "txtCharactersDatabase";
            this.txtCharactersDatabase.PlaceholderText = "Characters";
            this.txtCharactersDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtCharactersDatabase.TabIndex = 37;
            this.txtCharactersDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorld.Location = new System.Drawing.Point(17, 52);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(42, 15);
            this.lblWorld.TabIndex = 39;
            this.lblWorld.Text = "World:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSave.BorderRadius = 5;
            this.btnSave.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(561, 508);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.roundPanel4);
            this.Controls.Add(this.roundPanel3);
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.btnSave);
            this.Name = "SettingControl";
            this.Size = new System.Drawing.Size(697, 561);
            this.Load += new System.EventHandler(this.SettingControl_Load);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.roundPanel2.ResumeLayout(false);
            this.roundPanel2.PerformLayout();
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel3.PerformLayout();
            this.roundPanel4.ResumeLayout(false);
            this.roundPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.RoundPanel roundPanel1;
        private Label lblPCresource;
        private TextBox txtBnetLocation;
        private TextBox txtWorldLocation;
        private UI.CustomButton bntLocation;
        private Label lblBnetLocation;
        private Label lblWorldLocation;
        private UI.RoundPanel roundPanel2;
        private Label lblPort;
        private Label lblPassword;
        private TextBox txtMySqlPort;
        private TextBox txtMySqlPassowrd;
        private Label lblUser;
        private Label lblServer;
        private TextBox txtMySqlUser;
        private TextBox txtMySqlServer;
        private Label lblMySqlServerSettings;
        private RJCodeAdvance.RJControls.CustomToggleButton tglStayInTray;
        private UI.RoundPanel roundPanel3;
        private Label lblControlPanelSettings;
        private Label label10;
        private Label label11;
        private RJCodeAdvance.RJControls.CustomToggleButton tglNotySound;
        private UI.RoundPanel roundPanel4;
        private Label lblDatabaseSettings;
        private Label lblAuth;
        private TextBox txtAuthDatabase;
        private TextBox txtWorldDatabase;
        private Label lblCharacters;
        private TextBox txtCharactersDatabase;
        private Label lblWorld;
        private UI.CustomButton btnSave;
        private Label label16;
        private RJCodeAdvance.RJControls.CustomToggleButton tglHideConsole;
        private UI.CustomButton bntOpenLocation;
    }
}
