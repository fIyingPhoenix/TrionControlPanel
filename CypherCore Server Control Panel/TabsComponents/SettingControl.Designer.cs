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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblWorldLocation;
            System.Windows.Forms.Label lblWorldName;
            System.Windows.Forms.Label lblBnetName;
            System.Windows.Forms.Label lblApacheName;
            System.Windows.Forms.Label lblMysqlName;
            this.roundPanel1 = new CypherCore_Server_Laucher.UI.CustomPanelPanel();
            this.bntOpenLocation = new CypherCore_Server_Laucher.UI.CustomButton();
            this.bntLocation = new CypherCore_Server_Laucher.UI.CustomButton();
            this.lblBnetLocation = new System.Windows.Forms.Label();
            this.txtBnetLocation = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtWorldLocation = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.lblPCresource = new System.Windows.Forms.Label();
            this.roundPanel2 = new CypherCore_Server_Laucher.UI.CustomPanelPanel();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtMySqlPort = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtMySqlPassowrd = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtMySqlUser = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtMySqlServer = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.lblMySqlServerSettings = new System.Windows.Forms.Label();
            this.tglStayInTray = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.roundPanel3 = new CypherCore_Server_Laucher.UI.CustomPanelPanel();
            this.comboBoxCore = new CypherCoreServerLaucher.UI.CustomComboBox();
            this.lblCores = new System.Windows.Forms.Label();
            this.lblHideControls = new System.Windows.Forms.Label();
            this.tglHideConsole = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.lblNotySound = new System.Windows.Forms.Label();
            this.tglNotySound = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.lblStayTray = new System.Windows.Forms.Label();
            this.lblControlPanelSettings = new System.Windows.Forms.Label();
            this.roundPanel4 = new CypherCore_Server_Laucher.UI.CustomPanelPanel();
            this.lblAuth = new System.Windows.Forms.Label();
            this.lblDatabaseSettings = new System.Windows.Forms.Label();
            this.txtAuthDatabase = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtWorldDatabase = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.lblCharacters = new System.Windows.Forms.Label();
            this.txtCharactersDatabase = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.lblWorld = new System.Windows.Forms.Label();
            this.btnSave = new CypherCore_Server_Laucher.UI.CustomButton();
            this.customPanelPanel1 = new CypherCore_Server_Laucher.UI.CustomPanelPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tglCustomNames = new RJCodeAdvance.RJControls.CustomToggleButton();
            this.txtMysqlName = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtApacheName = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.txtBnetName = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWorldName = new CypherCore_Server_Laucher.UI.CustomTextBox();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.btnTestMySQL = new CypherCore_Server_Laucher.UI.CustomButton();
            lblWorldLocation = new System.Windows.Forms.Label();
            lblWorldName = new System.Windows.Forms.Label();
            lblBnetName = new System.Windows.Forms.Label();
            lblApacheName = new System.Windows.Forms.Label();
            lblMysqlName = new System.Windows.Forms.Label();
            this.roundPanel1.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.roundPanel3.SuspendLayout();
            this.roundPanel4.SuspendLayout();
            this.customPanelPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWorldLocation
            // 
            lblWorldLocation.AutoSize = true;
            lblWorldLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblWorldLocation.Location = new System.Drawing.Point(17, 29);
            lblWorldLocation.Name = "lblWorldLocation";
            lblWorldLocation.Size = new System.Drawing.Size(91, 15);
            lblWorldLocation.TabIndex = 29;
            lblWorldLocation.Text = "World Lcoation:";
            // 
            // lblWorldName
            // 
            lblWorldName.AutoSize = true;
            lblWorldName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblWorldName.Location = new System.Drawing.Point(19, 35);
            lblWorldName.Name = "lblWorldName";
            lblWorldName.Size = new System.Drawing.Size(77, 15);
            lblWorldName.TabIndex = 31;
            lblWorldName.Text = "World Name:";
            // 
            // lblBnetName
            // 
            lblBnetName.AutoSize = true;
            lblBnetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblBnetName.Location = new System.Drawing.Point(176, 35);
            lblBnetName.Name = "lblBnetName";
            lblBnetName.Size = new System.Drawing.Size(100, 15);
            lblBnetName.TabIndex = 33;
            lblBnetName.Text = "Bnet/Auth Name:";
            // 
            // lblApacheName
            // 
            lblApacheName.AutoSize = true;
            lblApacheName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblApacheName.Location = new System.Drawing.Point(19, 81);
            lblApacheName.Name = "lblApacheName";
            lblApacheName.Size = new System.Drawing.Size(85, 15);
            lblApacheName.TabIndex = 34;
            lblApacheName.Text = "Apache Name:";
            // 
            // lblMysqlName
            // 
            lblMysqlName.AutoSize = true;
            lblMysqlName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblMysqlName.Location = new System.Drawing.Point(176, 81);
            lblMysqlName.Name = "lblMysqlName";
            lblMysqlName.Size = new System.Drawing.Size(83, 15);
            lblMysqlName.TabIndex = 36;
            lblMysqlName.Text = "MySQL Name:";
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
            this.roundPanel1.Edge = 5;
            this.roundPanel1.Location = new System.Drawing.Point(11, 13);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Size = new System.Drawing.Size(342, 180);
            this.roundPanel1.TabIndex = 0;
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
            this.bntOpenLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOpenLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntOpenLocation.ForeColor = System.Drawing.Color.White;
            this.bntOpenLocation.Location = new System.Drawing.Point(17, 135);
            this.bntOpenLocation.Name = "bntOpenLocation";
            this.bntOpenLocation.Size = new System.Drawing.Size(120, 35);
            this.bntOpenLocation.TabIndex = 30;
            this.bntOpenLocation.Text = "Open Location";
            this.bntOpenLocation.TextColor = System.Drawing.Color.White;
            this.bntOpenLocation.UseVisualStyleBackColor = false;
            this.bntOpenLocation.Click += new System.EventHandler(this.BntOpenLocation_Click);
            // 
            // bntLocation
            // 
            this.bntLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntLocation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntLocation.BorderRadius = 3;
            this.bntLocation.BorderSize = 1;
            this.bntLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntLocation.FlatAppearance.BorderSize = 0;
            this.bntLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntLocation.ForeColor = System.Drawing.Color.White;
            this.bntLocation.Location = new System.Drawing.Point(190, 133);
            this.bntLocation.Name = "bntLocation";
            this.bntLocation.Size = new System.Drawing.Size(120, 35);
            this.bntLocation.TabIndex = 1;
            this.bntLocation.Text = "Search Location";
            this.bntLocation.TextColor = System.Drawing.Color.White;
            this.bntLocation.UseVisualStyleBackColor = false;
            this.bntLocation.Click += new System.EventHandler(this.BntLocation_Click);
            // 
            // lblBnetLocation
            // 
            this.lblBnetLocation.AutoSize = true;
            this.lblBnetLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnetLocation.Location = new System.Drawing.Point(17, 81);
            this.lblBnetLocation.Name = "lblBnetLocation";
            this.lblBnetLocation.Size = new System.Drawing.Size(114, 15);
            this.lblBnetLocation.TabIndex = 29;
            this.lblBnetLocation.Text = "Bnet/Auth Location:";
            // 
            // txtBnetLocation
            // 
            this.txtBnetLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtBnetLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtBnetLocation.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtBnetLocation.BorderSize = 1;
            this.txtBnetLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtBnetLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtBnetLocation.Location = new System.Drawing.Point(17, 99);
            this.txtBnetLocation.Margin = new System.Windows.Forms.Padding(0);
            this.txtBnetLocation.Multiline = true;
            this.txtBnetLocation.Name = "txtBnetLocation";
            this.txtBnetLocation.Padding = new System.Windows.Forms.Padding(1);
            this.txtBnetLocation.PasswordChar = false;
            this.txtBnetLocation.ReadOnly = true;
            this.txtBnetLocation.Size = new System.Drawing.Size(293, 23);
            this.txtBnetLocation.TabIndex = 22;
            this.txtBnetLocation.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBnetLocation.Texts = "";
            this.txtBnetLocation.UnderlinedStyle = false;
            // 
            // txtWorldLocation
            // 
            this.txtWorldLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtWorldLocation.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtWorldLocation.BorderSize = 1;
            this.txtWorldLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtWorldLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldLocation.Location = new System.Drawing.Point(17, 44);
            this.txtWorldLocation.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorldLocation.Multiline = true;
            this.txtWorldLocation.Name = "txtWorldLocation";
            this.txtWorldLocation.Padding = new System.Windows.Forms.Padding(1);
            this.txtWorldLocation.PasswordChar = false;
            this.txtWorldLocation.ReadOnly = true;
            this.txtWorldLocation.Size = new System.Drawing.Size(293, 23);
            this.txtWorldLocation.TabIndex = 21;
            this.txtWorldLocation.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWorldLocation.Texts = "";
            this.txtWorldLocation.UnderlinedStyle = false;
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
            this.roundPanel2.Edge = 5;
            this.roundPanel2.Location = new System.Drawing.Point(11, 199);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Size = new System.Drawing.Size(208, 296);
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
            this.txtMySqlPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtMySqlPort.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtMySqlPort.BorderSize = 1;
            this.txtMySqlPort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPort.Location = new System.Drawing.Point(17, 206);
            this.txtMySqlPort.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlPort.Multiline = true;
            this.txtMySqlPort.Name = "txtMySqlPort";
            this.txtMySqlPort.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlPort.PasswordChar = false;
            this.txtMySqlPort.ReadOnly = false;
            this.txtMySqlPort.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlPort.TabIndex = 31;
            this.txtMySqlPort.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMySqlPort.Texts = "";
            this.txtMySqlPort.UnderlinedStyle = false;
            this.txtMySqlPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMySqlPort_KeyPress);
            // 
            // txtMySqlPassowrd
            // 
            this.txtMySqlPassowrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlPassowrd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtMySqlPassowrd.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtMySqlPassowrd.BorderSize = 1;
            this.txtMySqlPassowrd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlPassowrd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlPassowrd.Location = new System.Drawing.Point(17, 160);
            this.txtMySqlPassowrd.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlPassowrd.Multiline = true;
            this.txtMySqlPassowrd.Name = "txtMySqlPassowrd";
            this.txtMySqlPassowrd.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlPassowrd.PasswordChar = false;
            this.txtMySqlPassowrd.ReadOnly = false;
            this.txtMySqlPassowrd.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlPassowrd.TabIndex = 30;
            this.txtMySqlPassowrd.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMySqlPassowrd.Texts = "";
            this.txtMySqlPassowrd.UnderlinedStyle = false;
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
            this.txtMySqlUser.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtMySqlUser.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtMySqlUser.BorderSize = 1;
            this.txtMySqlUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlUser.Location = new System.Drawing.Point(17, 116);
            this.txtMySqlUser.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlUser.Multiline = true;
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlUser.PasswordChar = false;
            this.txtMySqlUser.ReadOnly = false;
            this.txtMySqlUser.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlUser.TabIndex = 22;
            this.txtMySqlUser.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMySqlUser.Texts = "";
            this.txtMySqlUser.UnderlinedStyle = false;
            // 
            // txtMySqlServer
            // 
            this.txtMySqlServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtMySqlServer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtMySqlServer.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtMySqlServer.BorderSize = 1;
            this.txtMySqlServer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMySqlServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMySqlServer.Location = new System.Drawing.Point(17, 70);
            this.txtMySqlServer.Margin = new System.Windows.Forms.Padding(0);
            this.txtMySqlServer.Multiline = true;
            this.txtMySqlServer.Name = "txtMySqlServer";
            this.txtMySqlServer.Padding = new System.Windows.Forms.Padding(1);
            this.txtMySqlServer.PasswordChar = false;
            this.txtMySqlServer.ReadOnly = false;
            this.txtMySqlServer.Size = new System.Drawing.Size(167, 23);
            this.txtMySqlServer.TabIndex = 21;
            this.txtMySqlServer.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMySqlServer.Texts = "";
            this.txtMySqlServer.UnderlinedStyle = false;
            // 
            // lblMySqlServerSettings
            // 
            this.lblMySqlServerSettings.AutoSize = true;
            this.lblMySqlServerSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySqlServerSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblMySqlServerSettings.Location = new System.Drawing.Point(14, 7);
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
            this.tglStayInTray.Location = new System.Drawing.Point(17, 71);
            this.tglStayInTray.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglStayInTray.Name = "tglStayInTray";
            this.tglStayInTray.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglStayInTray.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglStayInTray.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglStayInTray.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglStayInTray.Size = new System.Drawing.Size(45, 22);
            this.tglStayInTray.SolidStyle = false;
            this.tglStayInTray.TabIndex = 2;
            this.tglStayInTray.UseVisualStyleBackColor = false;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.roundPanel3.Controls.Add(this.comboBoxCore);
            this.roundPanel3.Controls.Add(this.lblCores);
            this.roundPanel3.Controls.Add(this.lblHideControls);
            this.roundPanel3.Controls.Add(this.tglHideConsole);
            this.roundPanel3.Controls.Add(this.lblNotySound);
            this.roundPanel3.Controls.Add(this.tglNotySound);
            this.roundPanel3.Controls.Add(this.lblStayTray);
            this.roundPanel3.Controls.Add(this.lblControlPanelSettings);
            this.roundPanel3.Controls.Add(this.tglStayInTray);
            this.roundPanel3.Edge = 5;
            this.roundPanel3.Location = new System.Drawing.Point(455, 199);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Size = new System.Drawing.Size(234, 296);
            this.roundPanel3.TabIndex = 3;
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.comboBoxCore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.comboBoxCore.BorderSize = 1;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxCore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.comboBoxCore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.comboBoxCore.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.comboBoxCore.Items.AddRange(new object[] {
            "AscEmu ",
            "AzerothCore ",
            "Continued MaNGOS ",
            "CypherCore",
            "TrinityCore ",
            "TrinityCore 4.3.4 (TCPP)",
            "Vanilla MaNGOS "});
            this.comboBoxCore.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.comboBoxCore.ListTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.comboBoxCore.Location = new System.Drawing.Point(14, 193);
            this.comboBoxCore.MinimumSize = new System.Drawing.Size(200, 30);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Padding = new System.Windows.Forms.Padding(1);
            this.comboBoxCore.Size = new System.Drawing.Size(201, 30);
            this.comboBoxCore.TabIndex = 43;
            this.comboBoxCore.Texts = "";
            this.comboBoxCore.OnSelectedIndexChanged += new System.EventHandler(this.comboBoxCore_OnSelectedIndexChanged);
            // 
            // lblCores
            // 
            this.lblCores.AutoSize = true;
            this.lblCores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblCores.Location = new System.Drawing.Point(14, 175);
            this.lblCores.Name = "lblCores";
            this.lblCores.Size = new System.Drawing.Size(40, 15);
            this.lblCores.TabIndex = 42;
            this.lblCores.Text = "Cores:";
            // 
            // lblHideControls
            // 
            this.lblHideControls.AutoSize = true;
            this.lblHideControls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblHideControls.Location = new System.Drawing.Point(68, 131);
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
            this.tglHideConsole.Location = new System.Drawing.Point(17, 127);
            this.tglHideConsole.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglHideConsole.Name = "tglHideConsole";
            this.tglHideConsole.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglHideConsole.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglHideConsole.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglHideConsole.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglHideConsole.Size = new System.Drawing.Size(45, 22);
            this.tglHideConsole.SolidStyle = false;
            this.tglHideConsole.TabIndex = 40;
            this.tglHideConsole.UseVisualStyleBackColor = false;
            // 
            // lblNotySound
            // 
            this.lblNotySound.AutoSize = true;
            this.lblNotySound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblNotySound.Location = new System.Drawing.Point(68, 103);
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
            this.tglNotySound.Location = new System.Drawing.Point(17, 99);
            this.tglNotySound.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglNotySound.Name = "tglNotySound";
            this.tglNotySound.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglNotySound.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglNotySound.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.tglNotySound.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglNotySound.Size = new System.Drawing.Size(45, 22);
            this.tglNotySound.SolidStyle = false;
            this.tglNotySound.TabIndex = 38;
            this.tglNotySound.UseVisualStyleBackColor = false;
            // 
            // lblStayTray
            // 
            this.lblStayTray.AutoSize = true;
            this.lblStayTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblStayTray.Location = new System.Drawing.Point(68, 74);
            this.lblStayTray.Name = "lblStayTray";
            this.lblStayTray.Size = new System.Drawing.Size(68, 15);
            this.lblStayTray.TabIndex = 36;
            this.lblStayTray.Text = "Stay in tray.";
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
            this.roundPanel4.Edge = 5;
            this.roundPanel4.Location = new System.Drawing.Point(225, 199);
            this.roundPanel4.Name = "roundPanel4";
            this.roundPanel4.Size = new System.Drawing.Size(224, 296);
            this.roundPanel4.TabIndex = 4;
            // 
            // lblAuth
            // 
            this.lblAuth.AutoSize = true;
            this.lblAuth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblAuth.Location = new System.Drawing.Point(26, 142);
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
            this.txtAuthDatabase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtAuthDatabase.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtAuthDatabase.BorderSize = 1;
            this.txtAuthDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtAuthDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtAuthDatabase.Location = new System.Drawing.Point(26, 160);
            this.txtAuthDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.txtAuthDatabase.Multiline = true;
            this.txtAuthDatabase.Name = "txtAuthDatabase";
            this.txtAuthDatabase.Padding = new System.Windows.Forms.Padding(1);
            this.txtAuthDatabase.PasswordChar = false;
            this.txtAuthDatabase.ReadOnly = false;
            this.txtAuthDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtAuthDatabase.TabIndex = 40;
            this.txtAuthDatabase.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAuthDatabase.Texts = "";
            this.txtAuthDatabase.UnderlinedStyle = false;
            // 
            // txtWorldDatabase
            // 
            this.txtWorldDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldDatabase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtWorldDatabase.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtWorldDatabase.BorderSize = 1;
            this.txtWorldDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtWorldDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldDatabase.Location = new System.Drawing.Point(26, 70);
            this.txtWorldDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorldDatabase.Multiline = true;
            this.txtWorldDatabase.Name = "txtWorldDatabase";
            this.txtWorldDatabase.Padding = new System.Windows.Forms.Padding(1);
            this.txtWorldDatabase.PasswordChar = false;
            this.txtWorldDatabase.ReadOnly = false;
            this.txtWorldDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtWorldDatabase.TabIndex = 36;
            this.txtWorldDatabase.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWorldDatabase.Texts = "";
            this.txtWorldDatabase.UnderlinedStyle = false;
            // 
            // lblCharacters
            // 
            this.lblCharacters.AutoSize = true;
            this.lblCharacters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblCharacters.Location = new System.Drawing.Point(26, 98);
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(63, 15);
            this.lblCharacters.TabIndex = 38;
            this.lblCharacters.Text = "Characters";
            // 
            // txtCharactersDatabase
            // 
            this.txtCharactersDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtCharactersDatabase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtCharactersDatabase.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtCharactersDatabase.BorderSize = 1;
            this.txtCharactersDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtCharactersDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtCharactersDatabase.Location = new System.Drawing.Point(26, 116);
            this.txtCharactersDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.txtCharactersDatabase.Multiline = true;
            this.txtCharactersDatabase.Name = "txtCharactersDatabase";
            this.txtCharactersDatabase.Padding = new System.Windows.Forms.Padding(1);
            this.txtCharactersDatabase.PasswordChar = false;
            this.txtCharactersDatabase.ReadOnly = false;
            this.txtCharactersDatabase.Size = new System.Drawing.Size(167, 23);
            this.txtCharactersDatabase.TabIndex = 37;
            this.txtCharactersDatabase.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCharactersDatabase.Texts = "";
            this.txtCharactersDatabase.UnderlinedStyle = false;
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorld.Location = new System.Drawing.Point(26, 52);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(42, 15);
            this.lblWorld.TabIndex = 39;
            this.lblWorld.Text = "World:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.btnSave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnSave.BorderRadius = 3;
            this.btnSave.BorderSize = 1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(618, 508);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // customPanelPanel1
            // 
            this.customPanelPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.customPanelPanel1.BorderColor = System.Drawing.Color.White;
            this.customPanelPanel1.Controls.Add(this.label1);
            this.customPanelPanel1.Controls.Add(this.tglCustomNames);
            this.customPanelPanel1.Controls.Add(lblMysqlName);
            this.customPanelPanel1.Controls.Add(this.txtMysqlName);
            this.customPanelPanel1.Controls.Add(lblApacheName);
            this.customPanelPanel1.Controls.Add(this.txtApacheName);
            this.customPanelPanel1.Controls.Add(lblBnetName);
            this.customPanelPanel1.Controls.Add(lblWorldName);
            this.customPanelPanel1.Controls.Add(this.txtBnetName);
            this.customPanelPanel1.Controls.Add(this.label3);
            this.customPanelPanel1.Controls.Add(this.txtWorldName);
            this.customPanelPanel1.Edge = 5;
            this.customPanelPanel1.Location = new System.Drawing.Point(359, 13);
            this.customPanelPanel1.Name = "customPanelPanel1";
            this.customPanelPanel1.Size = new System.Drawing.Size(330, 180);
            this.customPanelPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label1.Location = new System.Drawing.Point(70, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 45;
            this.label1.Text = "Custom Names.";
            // 
            // tglCustomNames
            // 
            this.tglCustomNames.AutoSize = true;
            this.tglCustomNames.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglCustomNames.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglCustomNames.Location = new System.Drawing.Point(19, 138);
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
            this.txtMysqlName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtMysqlName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtMysqlName.BorderSize = 1;
            this.txtMysqlName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtMysqlName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtMysqlName.Location = new System.Drawing.Point(176, 99);
            this.txtMysqlName.Margin = new System.Windows.Forms.Padding(0);
            this.txtMysqlName.Multiline = true;
            this.txtMysqlName.Name = "txtMysqlName";
            this.txtMysqlName.Padding = new System.Windows.Forms.Padding(1);
            this.txtMysqlName.PasswordChar = false;
            this.txtMysqlName.ReadOnly = true;
            this.txtMysqlName.Size = new System.Drawing.Size(135, 23);
            this.txtMysqlName.TabIndex = 37;
            this.txtMysqlName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMysqlName.Texts = "";
            this.txtMysqlName.UnderlinedStyle = false;
            // 
            // txtApacheName
            // 
            this.txtApacheName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtApacheName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtApacheName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtApacheName.BorderSize = 1;
            this.txtApacheName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtApacheName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtApacheName.Location = new System.Drawing.Point(19, 99);
            this.txtApacheName.Margin = new System.Windows.Forms.Padding(0);
            this.txtApacheName.Multiline = true;
            this.txtApacheName.Name = "txtApacheName";
            this.txtApacheName.Padding = new System.Windows.Forms.Padding(1);
            this.txtApacheName.PasswordChar = false;
            this.txtApacheName.ReadOnly = true;
            this.txtApacheName.Size = new System.Drawing.Size(135, 23);
            this.txtApacheName.TabIndex = 35;
            this.txtApacheName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtApacheName.Texts = "";
            this.txtApacheName.UnderlinedStyle = false;
            // 
            // txtBnetName
            // 
            this.txtBnetName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtBnetName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtBnetName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtBnetName.BorderSize = 1;
            this.txtBnetName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtBnetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtBnetName.Location = new System.Drawing.Point(176, 53);
            this.txtBnetName.Margin = new System.Windows.Forms.Padding(0);
            this.txtBnetName.Multiline = true;
            this.txtBnetName.Name = "txtBnetName";
            this.txtBnetName.Padding = new System.Windows.Forms.Padding(1);
            this.txtBnetName.PasswordChar = false;
            this.txtBnetName.ReadOnly = true;
            this.txtBnetName.Size = new System.Drawing.Size(135, 23);
            this.txtBnetName.TabIndex = 32;
            this.txtBnetName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBnetName.Texts = "";
            this.txtBnetName.UnderlinedStyle = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 21);
            this.label3.TabIndex = 20;
            this.label3.Text = "Names";
            // 
            // txtWorldName
            // 
            this.txtWorldName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtWorldName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.txtWorldName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtWorldName.BorderSize = 1;
            this.txtWorldName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtWorldName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtWorldName.Location = new System.Drawing.Point(19, 53);
            this.txtWorldName.Margin = new System.Windows.Forms.Padding(0);
            this.txtWorldName.Multiline = true;
            this.txtWorldName.Name = "txtWorldName";
            this.txtWorldName.Padding = new System.Windows.Forms.Padding(1);
            this.txtWorldName.PasswordChar = false;
            this.txtWorldName.ReadOnly = true;
            this.txtWorldName.Size = new System.Drawing.Size(135, 23);
            this.txtWorldName.TabIndex = 31;
            this.txtWorldName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWorldName.Texts = "";
            this.txtWorldName.UnderlinedStyle = false;
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
            // 
            // btnTestMySQL
            // 
            this.btnTestMySQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.btnTestMySQL.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.btnTestMySQL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnTestMySQL.BorderRadius = 3;
            this.btnTestMySQL.BorderSize = 1;
            this.btnTestMySQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestMySQL.FlatAppearance.BorderSize = 0;
            this.btnTestMySQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestMySQL.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestMySQL.ForeColor = System.Drawing.Color.White;
            this.btnTestMySQL.Location = new System.Drawing.Point(469, 508);
            this.btnTestMySQL.Name = "btnTestMySQL";
            this.btnTestMySQL.Size = new System.Drawing.Size(144, 40);
            this.btnTestMySQL.TabIndex = 6;
            this.btnTestMySQL.Text = "Test Connection";
            this.btnTestMySQL.TextColor = System.Drawing.Color.White;
            this.btnTestMySQL.UseVisualStyleBackColor = false;
            this.btnTestMySQL.Click += new System.EventHandler(this.btnTestMySQL_Click);
            // 
            // SettingControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.btnTestMySQL);
            this.Controls.Add(this.customPanelPanel1);
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
            this.customPanelPanel1.ResumeLayout(false);
            this.customPanelPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.CustomPanelPanel roundPanel1;
        private Label lblPCresource;
        private UI.CustomTextBox txtBnetLocation;
        private UI.CustomTextBox txtWorldLocation;
        private UI.CustomButton bntLocation;
        private Label lblBnetLocation;
        private Label lblWorldLocation;
        private UI.CustomPanelPanel roundPanel2;
        private Label lblPort;
        private Label lblPassword;
        private UI.CustomTextBox txtMySqlPort;
        private UI.CustomTextBox txtMySqlPassowrd;
        private Label lblUser;
        private Label lblServer;
        private UI.CustomTextBox txtMySqlUser;
        private UI.CustomTextBox txtMySqlServer;
        private Label lblMySqlServerSettings;
        private RJCodeAdvance.RJControls.CustomToggleButton tglStayInTray;
        private UI.CustomPanelPanel roundPanel3;
        private Label lblControlPanelSettings;
        private Label lblStayTray;
        private Label lblNotySound;
        private RJCodeAdvance.RJControls.CustomToggleButton tglNotySound;
        private UI.CustomPanelPanel roundPanel4;
        private Label lblDatabaseSettings;
        private Label lblAuth;
        private UI.CustomTextBox txtAuthDatabase;
        private UI.CustomTextBox txtWorldDatabase;
        private Label lblCharacters;
        private UI.CustomTextBox txtCharactersDatabase;
        private Label lblWorld;
        private UI.CustomButton btnSave;
        private Label lblHideControls;
        private RJCodeAdvance.RJControls.CustomToggleButton tglHideConsole;
        private UI.CustomButton bntOpenLocation;
        private UI.CustomPanelPanel customPanelPanel1;
        private Label label3;
        private UI.CustomTextBox txtMysqlName;
        private UI.CustomTextBox txtApacheName;
        private UI.CustomTextBox txtBnetName;
        private UI.CustomTextBox txtWorldName;
        private Label lblCores;
        private CypherCoreServerLaucher.UI.CustomComboBox comboBoxCore;
        private Label label1;
        private RJCodeAdvance.RJControls.CustomToggleButton tglCustomNames;
        private System.Windows.Forms.Timer timerCheck;
        private UI.CustomButton btnTestMySQL;
    }
}
