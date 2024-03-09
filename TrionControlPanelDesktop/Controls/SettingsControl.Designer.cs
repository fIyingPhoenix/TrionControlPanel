namespace TrionControlPanelDesktop.Controls
{
    partial class SettingsControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            TabPageCore = new TabPage();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            label19 = new Label();
            ComboBoxCores = new TrionControlPanel.UI.CustomComboBox();
            label11 = new Label();
            label1 = new Label();
            TGLCustomNames = new TrionControlPanel.UI.CustomToggleButton();
            TXTBoxWorldExecName = new MetroFramework.Controls.MetroTextBox();
            label10 = new Label();
            label8 = new Label();
            TXTBoxMySQLExecName = new MetroFramework.Controls.MetroTextBox();
            TXTBoxLoginExecName = new MetroFramework.Controls.MetroTextBox();
            label9 = new Label();
            metroPanel1 = new MetroFramework.Controls.MetroPanel();
            LBLMySQLVersion = new Label();
            LBLCoreVersion = new Label();
            PNLControl = new MetroFramework.Controls.MetroPanel();
            BTNDownlaodMySQL = new UI.Controls.CustomButton();
            BTNModsConfig = new UI.Controls.CustomButton();
            BTNAuthConfig = new UI.Controls.CustomButton();
            BTNWorldConfig = new UI.Controls.CustomButton();
            BTNDiscord = new UI.Controls.CustomButton();
            BtnDownloadSPP = new UI.Controls.CustomButton();
            TabPageTrion = new TabPage();
            BTNFixMysql = new UI.Controls.CustomButton();
            metroPanel4 = new MetroFramework.Controls.MetroPanel();
            LBLTrionVersion = new Label();
            BTNTrionUpdate = new UI.Controls.CustomButton();
            metroPanel3 = new MetroFramework.Controls.MetroPanel();
            LBLAutoUpdateMySQL = new Label();
            TGLAutoUpdateMySQL = new TrionControlPanel.UI.CustomToggleButton();
            LBLAutoUpdateSPP = new Label();
            TGLAutoUpdateCore = new TrionControlPanel.UI.CustomToggleButton();
            LBLAutoUpdateTrion = new Label();
            TGLAutoUpdateTrion = new TrionControlPanel.UI.CustomToggleButton();
            TGLStayInTrey = new TrionControlPanel.UI.CustomToggleButton();
            TGLNotificationSound = new TrionControlPanel.UI.CustomToggleButton();
            TGLHideConsole = new TrionControlPanel.UI.CustomToggleButton();
            LBLHideConsole = new Label();
            LBLStayInTray = new Label();
            LBLNotiSound = new Label();
            BTNMySQLOpenFolder = new UI.Controls.CustomButton();
            BTNCoreOpenFolder = new UI.Controls.CustomButton();
            BTNMySQLExecLovation = new UI.Controls.CustomButton();
            BTNCoreExecLovation = new UI.Controls.CustomButton();
            LBLMysqlWorkingDIr = new Label();
            LBLCorelWorkingDIr = new Label();
            TXTBoxMySQLLocation = new MetroFramework.Controls.MetroTextBox();
            TXTBoxCoreLocation = new MetroFramework.Controls.MetroTextBox();
            TabPageDatabase = new TabPage();
            panel1 = new MetroFramework.Controls.MetroPanel();
            label21 = new Label();
            TXTWorldDatabase = new MetroFramework.Controls.MetroTextBox();
            label22 = new Label();
            TXTCharDatabase = new MetroFramework.Controls.MetroTextBox();
            label23 = new Label();
            TXTAuthDatabase = new MetroFramework.Controls.MetroTextBox();
            panel3 = new MetroFramework.Controls.MetroPanel();
            BTNTestConnection = new UI.Controls.CustomButton();
            label18 = new Label();
            TXTMysqlPassword = new MetroFramework.Controls.MetroTextBox();
            label15 = new Label();
            TXTMysqlUser = new MetroFramework.Controls.MetroTextBox();
            label13 = new Label();
            TXTMysqlPort = new MetroFramework.Controls.MetroTextBox();
            label12 = new Label();
            TXTMysqlHost = new MetroFramework.Controls.MetroTextBox();
            TimerWacher = new System.Windows.Forms.Timer(components);
            metroTabControl1.SuspendLayout();
            TabPageCore.SuspendLayout();
            metroPanel2.SuspendLayout();
            metroPanel1.SuspendLayout();
            PNLControl.SuspendLayout();
            TabPageTrion.SuspendLayout();
            metroPanel4.SuspendLayout();
            metroPanel3.SuspendLayout();
            TabPageDatabase.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // metroTabControl1
            // 
            metroTabControl1.Controls.Add(TabPageTrion);
            metroTabControl1.Controls.Add(TabPageCore);
            metroTabControl1.Controls.Add(TabPageDatabase);
            metroTabControl1.Dock = DockStyle.Fill;
            metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            metroTabControl1.Location = new Point(0, 0);
            metroTabControl1.Name = "metroTabControl1";
            metroTabControl1.Padding = new Point(6, 8);
            metroTabControl1.SelectedIndex = 0;
            metroTabControl1.Size = new Size(845, 370);
            metroTabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            metroTabControl1.TabIndex = 2;
            metroTabControl1.TextAlign = ContentAlignment.MiddleCenter;
            metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTabControl1.UseSelectable = true;
            metroTabControl1.UseStyleColors = true;
            // 
            // TabPageCore
            // 
            TabPageCore.BackColor = Color.FromArgb(45, 51, 59);
            TabPageCore.Controls.Add(metroPanel2);
            TabPageCore.Controls.Add(metroPanel1);
            TabPageCore.Controls.Add(PNLControl);
            TabPageCore.Location = new Point(4, 38);
            TabPageCore.Name = "TabPageCore";
            TabPageCore.Size = new Size(837, 328);
            TabPageCore.TabIndex = 1;
            TabPageCore.Text = "Core";
            // 
            // metroPanel2
            // 
            metroPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroPanel2.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(label19);
            metroPanel2.Controls.Add(ComboBoxCores);
            metroPanel2.Controls.Add(label11);
            metroPanel2.Controls.Add(label1);
            metroPanel2.Controls.Add(TGLCustomNames);
            metroPanel2.Controls.Add(TXTBoxWorldExecName);
            metroPanel2.Controls.Add(label10);
            metroPanel2.Controls.Add(label8);
            metroPanel2.Controls.Add(TXTBoxMySQLExecName);
            metroPanel2.Controls.Add(TXTBoxLoginExecName);
            metroPanel2.Controls.Add(label9);
            metroPanel2.CustomBackground = false;
            metroPanel2.HorizontalScrollbar = true;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(235, 20);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Size = new Size(290, 290);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 40;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = true;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label19.BackColor = Color.Transparent;
            label19.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label19.ForeColor = Color.FromArgb(0, 174, 219);
            label19.Location = new Point(19, 10);
            label19.Name = "label19";
            label19.Size = new Size(252, 21);
            label19.TabIndex = 38;
            label19.Text = "SERVER FRAMEWORKS";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ComboBoxCores
            // 
            ComboBoxCores.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ComboBoxCores.BackColor = Color.FromArgb(34, 34, 34);
            ComboBoxCores.BorderColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.BorderSize = 1;
            ComboBoxCores.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxCores.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ComboBoxCores.ForeColor = Color.White;
            ComboBoxCores.IconColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.ListBackColor = Color.FromArgb(34, 34, 34);
            ComboBoxCores.ListTextColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.Location = new Point(16, 65);
            ComboBoxCores.MinimumSize = new Size(200, 27);
            ComboBoxCores.Name = "ComboBoxCores";
            ComboBoxCores.Padding = new Padding(1);
            ComboBoxCores.Size = new Size(255, 27);
            ComboBoxCores.TabIndex = 28;
            ComboBoxCores.Texts = "";
            ComboBoxCores.OnSelectedIndexChanged += ComboBoxCores_OnSelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(28, 33, 40);
            label11.ForeColor = Color.White;
            label11.Location = new Point(62, 257);
            label11.Name = "label11";
            label11.Size = new Size(92, 15);
            label11.TabIndex = 37;
            label11.Text = "Custom Names.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(19, 43);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 29;
            label1.Text = "Core:";
            // 
            // TGLCustomNames
            // 
            TGLCustomNames.AutoSize = true;
            TGLCustomNames.BackColor = Color.FromArgb(28, 33, 40);
            TGLCustomNames.Location = new Point(17, 253);
            TGLCustomNames.MinimumSize = new Size(45, 22);
            TGLCustomNames.Name = "TGLCustomNames";
            TGLCustomNames.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLCustomNames.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLCustomNames.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLCustomNames.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLCustomNames.Size = new Size(45, 22);
            TGLCustomNames.SolidStyle = false;
            TGLCustomNames.TabIndex = 36;
            TGLCustomNames.UseVisualStyleBackColor = false;
            TGLCustomNames.CheckedChanged += TGLCustomNames_CheckedChanged;
            // 
            // TXTBoxWorldExecName
            // 
            TXTBoxWorldExecName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxWorldExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxWorldExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxWorldExecName.Location = new Point(16, 113);
            TXTBoxWorldExecName.Multiline = false;
            TXTBoxWorldExecName.Name = "TXTBoxWorldExecName";
            TXTBoxWorldExecName.PasswordChar = '\0';
            TXTBoxWorldExecName.ReadOnly = true;
            TXTBoxWorldExecName.SelectedText = "";
            TXTBoxWorldExecName.Size = new Size(255, 25);
            TXTBoxWorldExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxWorldExecName.StyleManager = null;
            TXTBoxWorldExecName.TabIndex = 30;
            TXTBoxWorldExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxWorldExecName.UseStyleColors = true;
            TXTBoxWorldExecName.TextChanged += TXTBox_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(19, 189);
            label10.Name = "label10";
            label10.Size = new Size(146, 15);
            label10.TabIndex = 35;
            label10.Text = "MySQLl Executable Name:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(19, 96);
            label8.Name = "label8";
            label8.Size = new Size(134, 15);
            label8.TabIndex = 31;
            label8.Text = "World Executable Name";
            // 
            // TXTBoxMySQLExecName
            // 
            TXTBoxMySQLExecName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxMySQLExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxMySQLExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxMySQLExecName.Location = new Point(16, 206);
            TXTBoxMySQLExecName.Multiline = false;
            TXTBoxMySQLExecName.Name = "TXTBoxMySQLExecName";
            TXTBoxMySQLExecName.PasswordChar = '\0';
            TXTBoxMySQLExecName.ReadOnly = true;
            TXTBoxMySQLExecName.SelectedText = "";
            TXTBoxMySQLExecName.Size = new Size(255, 25);
            TXTBoxMySQLExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxMySQLExecName.StyleManager = null;
            TXTBoxMySQLExecName.TabIndex = 34;
            TXTBoxMySQLExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxMySQLExecName.UseStyleColors = true;
            TXTBoxMySQLExecName.TextChanged += TXTBox_TextChanged;
            // 
            // TXTBoxLoginExecName
            // 
            TXTBoxLoginExecName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxLoginExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxLoginExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxLoginExecName.Location = new Point(16, 160);
            TXTBoxLoginExecName.Multiline = false;
            TXTBoxLoginExecName.Name = "TXTBoxLoginExecName";
            TXTBoxLoginExecName.PasswordChar = '\0';
            TXTBoxLoginExecName.ReadOnly = true;
            TXTBoxLoginExecName.SelectedText = "";
            TXTBoxLoginExecName.Size = new Size(255, 25);
            TXTBoxLoginExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxLoginExecName.StyleManager = null;
            TXTBoxLoginExecName.TabIndex = 32;
            TXTBoxLoginExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxLoginExecName.UseStyleColors = true;
            TXTBoxLoginExecName.TextChanged += TXTBox_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(18, 143);
            label9.Name = "label9";
            label9.Size = new Size(132, 15);
            label9.TabIndex = 33;
            label9.Text = "Login Executable Name";
            // 
            // metroPanel1
            // 
            metroPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            metroPanel1.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(LBLMySQLVersion);
            metroPanel1.Controls.Add(LBLCoreVersion);
            metroPanel1.CustomBackground = true;
            metroPanel1.HorizontalScrollbar = false;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(535, 20);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Padding = new Padding(2);
            metroPanel1.Size = new Size(290, 122);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 39;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // LBLMySQLVersion
            // 
            LBLMySQLVersion.AutoSize = true;
            LBLMySQLVersion.ForeColor = Color.FromArgb(0, 174, 219);
            LBLMySQLVersion.Location = new Point(7, 65);
            LBLMySQLVersion.Name = "LBLMySQLVersion";
            LBLMySQLVersion.Size = new Size(89, 45);
            LBLMySQLVersion.TabIndex = 36;
            LBLMySQLVersion.Text = "MySQL Version:\r\nLocal:\r\nOnline:";
            // 
            // LBLCoreVersion
            // 
            LBLCoreVersion.AutoSize = true;
            LBLCoreVersion.ForeColor = Color.FromArgb(0, 174, 219);
            LBLCoreVersion.Location = new Point(7, 14);
            LBLCoreVersion.Name = "LBLCoreVersion";
            LBLCoreVersion.Size = new Size(80, 45);
            LBLCoreVersion.TabIndex = 32;
            LBLCoreVersion.Text = "S.P.P. Version:\r\nLocal:\r\nOnline:";
            // 
            // PNLControl
            // 
            PNLControl.BackColor = Color.FromArgb(28, 33, 40);
            PNLControl.Border = true;
            PNLControl.BorderColor = Color.Black;
            PNLControl.BorderSize = 1;
            PNLControl.Controls.Add(BTNDownlaodMySQL);
            PNLControl.Controls.Add(BTNModsConfig);
            PNLControl.Controls.Add(BTNAuthConfig);
            PNLControl.Controls.Add(BTNWorldConfig);
            PNLControl.Controls.Add(BTNDiscord);
            PNLControl.Controls.Add(BtnDownloadSPP);
            PNLControl.CustomBackground = false;
            PNLControl.HorizontalScrollbar = false;
            PNLControl.HorizontalScrollbarBarColor = true;
            PNLControl.HorizontalScrollbarHighlightOnWheel = false;
            PNLControl.HorizontalScrollbarSize = 10;
            PNLControl.Location = new Point(15, 20);
            PNLControl.Name = "PNLControl";
            PNLControl.Padding = new Padding(2);
            PNLControl.Size = new Size(210, 290);
            PNLControl.Style = MetroFramework.MetroColorStyle.Blue;
            PNLControl.StyleManager = null;
            PNLControl.TabIndex = 38;
            PNLControl.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLControl.VerticalScrollbar = false;
            PNLControl.VerticalScrollbarBarColor = true;
            PNLControl.VerticalScrollbarHighlightOnWheel = false;
            PNLControl.VerticalScrollbarSize = 10;
            // 
            // BTNDownlaodMySQL
            // 
            BTNDownlaodMySQL.Anchor = AnchorStyles.Top;
            BTNDownlaodMySQL.BackColor = Color.FromArgb(28, 33, 40);
            BTNDownlaodMySQL.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNDownlaodMySQL.BorderColor = Color.FromArgb(0, 174, 219);
            BTNDownlaodMySQL.BorderRadius = 0;
            BTNDownlaodMySQL.BorderSize = 1;
            BTNDownlaodMySQL.Cursor = Cursors.Hand;
            BTNDownlaodMySQL.FlatAppearance.BorderSize = 0;
            BTNDownlaodMySQL.FlatStyle = FlatStyle.Flat;
            BTNDownlaodMySQL.ForeColor = Color.White;
            BTNDownlaodMySQL.Image = (Image)resources.GetObject("BTNDownlaodMySQL.Image");
            BTNDownlaodMySQL.ImageAlign = ContentAlignment.MiddleLeft;
            BTNDownlaodMySQL.Location = new Point(12, 44);
            BTNDownlaodMySQL.Name = "BTNDownlaodMySQL";
            BTNDownlaodMySQL.RightToLeft = RightToLeft.No;
            BTNDownlaodMySQL.Size = new Size(186, 25);
            BTNDownlaodMySQL.TabIndex = 42;
            BTNDownlaodMySQL.Text = "   Download MySQL";
            BTNDownlaodMySQL.TextColor = Color.White;
            BTNDownlaodMySQL.UseVisualStyleBackColor = false;
            BTNDownlaodMySQL.Click += BTNDownloadMySQL_Click;
            // 
            // BTNModsConfig
            // 
            BTNModsConfig.Anchor = AnchorStyles.Top;
            BTNModsConfig.BackColor = Color.FromArgb(28, 33, 40);
            BTNModsConfig.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNModsConfig.BorderColor = Color.FromArgb(0, 174, 219);
            BTNModsConfig.BorderRadius = 0;
            BTNModsConfig.BorderSize = 1;
            BTNModsConfig.Cursor = Cursors.Hand;
            BTNModsConfig.FlatAppearance.BorderSize = 0;
            BTNModsConfig.FlatStyle = FlatStyle.Flat;
            BTNModsConfig.ForeColor = Color.White;
            BTNModsConfig.Image = (Image)resources.GetObject("BTNModsConfig.Image");
            BTNModsConfig.ImageAlign = ContentAlignment.MiddleLeft;
            BTNModsConfig.Location = new Point(12, 253);
            BTNModsConfig.Name = "BTNModsConfig";
            BTNModsConfig.RightToLeft = RightToLeft.No;
            BTNModsConfig.Size = new Size(186, 25);
            BTNModsConfig.TabIndex = 41;
            BTNModsConfig.Text = "   Mod's Config";
            BTNModsConfig.TextColor = Color.White;
            BTNModsConfig.UseVisualStyleBackColor = false;
            // 
            // BTNAuthConfig
            // 
            BTNAuthConfig.Anchor = AnchorStyles.Top;
            BTNAuthConfig.BackColor = Color.FromArgb(28, 33, 40);
            BTNAuthConfig.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNAuthConfig.BorderColor = Color.FromArgb(0, 174, 219);
            BTNAuthConfig.BorderRadius = 0;
            BTNAuthConfig.BorderSize = 1;
            BTNAuthConfig.Cursor = Cursors.Hand;
            BTNAuthConfig.FlatAppearance.BorderSize = 0;
            BTNAuthConfig.FlatStyle = FlatStyle.Flat;
            BTNAuthConfig.ForeColor = Color.White;
            BTNAuthConfig.Image = (Image)resources.GetObject("BTNAuthConfig.Image");
            BTNAuthConfig.ImageAlign = ContentAlignment.MiddleLeft;
            BTNAuthConfig.Location = new Point(12, 191);
            BTNAuthConfig.Name = "BTNAuthConfig";
            BTNAuthConfig.RightToLeft = RightToLeft.No;
            BTNAuthConfig.Size = new Size(186, 25);
            BTNAuthConfig.TabIndex = 38;
            BTNAuthConfig.Text = "   Auth Server Config";
            BTNAuthConfig.TextColor = Color.White;
            BTNAuthConfig.UseVisualStyleBackColor = false;
            BTNAuthConfig.Click += BTNAuthConfig_Click;
            // 
            // BTNWorldConfig
            // 
            BTNWorldConfig.Anchor = AnchorStyles.Top;
            BTNWorldConfig.BackColor = Color.FromArgb(28, 33, 40);
            BTNWorldConfig.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNWorldConfig.BorderColor = Color.FromArgb(0, 174, 219);
            BTNWorldConfig.BorderRadius = 0;
            BTNWorldConfig.BorderSize = 1;
            BTNWorldConfig.Cursor = Cursors.Hand;
            BTNWorldConfig.FlatAppearance.BorderSize = 0;
            BTNWorldConfig.FlatStyle = FlatStyle.Flat;
            BTNWorldConfig.ForeColor = Color.White;
            BTNWorldConfig.Image = (Image)resources.GetObject("BTNWorldConfig.Image");
            BTNWorldConfig.ImageAlign = ContentAlignment.MiddleLeft;
            BTNWorldConfig.Location = new Point(12, 222);
            BTNWorldConfig.Name = "BTNWorldConfig";
            BTNWorldConfig.RightToLeft = RightToLeft.No;
            BTNWorldConfig.Size = new Size(186, 25);
            BTNWorldConfig.TabIndex = 39;
            BTNWorldConfig.Text = "   World Server Config";
            BTNWorldConfig.TextColor = Color.White;
            BTNWorldConfig.UseVisualStyleBackColor = false;
            // 
            // BTNDiscord
            // 
            BTNDiscord.Anchor = AnchorStyles.Top;
            BTNDiscord.BackColor = Color.FromArgb(28, 33, 40);
            BTNDiscord.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNDiscord.BorderColor = Color.FromArgb(0, 174, 219);
            BTNDiscord.BorderRadius = 0;
            BTNDiscord.BorderSize = 1;
            BTNDiscord.Cursor = Cursors.Hand;
            BTNDiscord.FlatAppearance.BorderSize = 0;
            BTNDiscord.FlatStyle = FlatStyle.Flat;
            BTNDiscord.ForeColor = Color.White;
            BTNDiscord.Image = (Image)resources.GetObject("BTNDiscord.Image");
            BTNDiscord.ImageAlign = ContentAlignment.MiddleLeft;
            BTNDiscord.Location = new Point(12, 75);
            BTNDiscord.Name = "BTNDiscord";
            BTNDiscord.RightToLeft = RightToLeft.No;
            BTNDiscord.Size = new Size(186, 25);
            BTNDiscord.TabIndex = 34;
            BTNDiscord.Text = "   Discord";
            BTNDiscord.TextColor = Color.White;
            BTNDiscord.UseVisualStyleBackColor = false;
            BTNDiscord.Click += BTNDiscord_Click;
            // 
            // BtnDownloadSPP
            // 
            BtnDownloadSPP.Anchor = AnchorStyles.Top;
            BtnDownloadSPP.BackColor = Color.FromArgb(28, 33, 40);
            BtnDownloadSPP.BackgroundColor = Color.FromArgb(28, 33, 40);
            BtnDownloadSPP.BorderColor = Color.FromArgb(0, 174, 219);
            BtnDownloadSPP.BorderRadius = 0;
            BtnDownloadSPP.BorderSize = 1;
            BtnDownloadSPP.Cursor = Cursors.Hand;
            BtnDownloadSPP.FlatAppearance.BorderSize = 0;
            BtnDownloadSPP.FlatStyle = FlatStyle.Flat;
            BtnDownloadSPP.ForeColor = Color.White;
            BtnDownloadSPP.Image = (Image)resources.GetObject("BtnDownloadSPP.Image");
            BtnDownloadSPP.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDownloadSPP.Location = new Point(12, 13);
            BtnDownloadSPP.Name = "BtnDownloadSPP";
            BtnDownloadSPP.RightToLeft = RightToLeft.No;
            BtnDownloadSPP.Size = new Size(186, 25);
            BtnDownloadSPP.TabIndex = 35;
            BtnDownloadSPP.Text = "   Download S.P.P.";
            BtnDownloadSPP.TextColor = Color.White;
            BtnDownloadSPP.UseVisualStyleBackColor = false;
            BtnDownloadSPP.Click += BtnDownloadSPP_ClickAsync;
            // 
            // TabPageTrion
            // 
            TabPageTrion.AccessibleDescription = "";
            TabPageTrion.AccessibleName = "";
            TabPageTrion.AccessibleRole = AccessibleRole.Window;
            TabPageTrion.BackColor = Color.FromArgb(45, 51, 59);
            TabPageTrion.Controls.Add(BTNFixMysql);
            TabPageTrion.Controls.Add(metroPanel4);
            TabPageTrion.Controls.Add(BTNTrionUpdate);
            TabPageTrion.Controls.Add(metroPanel3);
            TabPageTrion.Controls.Add(BTNMySQLOpenFolder);
            TabPageTrion.Controls.Add(BTNCoreOpenFolder);
            TabPageTrion.Controls.Add(BTNMySQLExecLovation);
            TabPageTrion.Controls.Add(BTNCoreExecLovation);
            TabPageTrion.Controls.Add(LBLMysqlWorkingDIr);
            TabPageTrion.Controls.Add(LBLCorelWorkingDIr);
            TabPageTrion.Controls.Add(TXTBoxMySQLLocation);
            TabPageTrion.Controls.Add(TXTBoxCoreLocation);
            TabPageTrion.ForeColor = Color.FromArgb(0, 174, 219);
            TabPageTrion.Location = new Point(4, 38);
            TabPageTrion.Name = "TabPageTrion";
            TabPageTrion.Size = new Size(837, 328);
            TabPageTrion.TabIndex = 0;
            TabPageTrion.Text = "Trion ";
            // 
            // BTNFixMysql
            // 
            BTNFixMysql.Anchor = AnchorStyles.Right;
            BTNFixMysql.BackColor = Color.FromArgb(28, 33, 40);
            BTNFixMysql.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNFixMysql.BorderColor = Color.FromArgb(0, 174, 219);
            BTNFixMysql.BorderRadius = 0;
            BTNFixMysql.BorderSize = 1;
            BTNFixMysql.Cursor = Cursors.Hand;
            BTNFixMysql.FlatAppearance.BorderSize = 0;
            BTNFixMysql.FlatStyle = FlatStyle.Flat;
            BTNFixMysql.ForeColor = Color.White;
            BTNFixMysql.Image = (Image)resources.GetObject("BTNFixMysql.Image");
            BTNFixMysql.ImageAlign = ContentAlignment.MiddleLeft;
            BTNFixMysql.Location = new Point(711, 259);
            BTNFixMysql.Name = "BTNFixMysql";
            BTNFixMysql.RightToLeft = RightToLeft.No;
            BTNFixMysql.Size = new Size(104, 25);
            BTNFixMysql.TabIndex = 43;
            BTNFixMysql.Text = "   Fix MySQL";
            BTNFixMysql.TextColor = Color.White;
            BTNFixMysql.UseVisualStyleBackColor = false;
            BTNFixMysql.Click += BTNFixMysql_Click;
            // 
            // metroPanel4
            // 
            metroPanel4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel4.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel4.Border = true;
            metroPanel4.BorderColor = Color.Black;
            metroPanel4.BorderSize = 1;
            metroPanel4.Controls.Add(LBLTrionVersion);
            metroPanel4.CustomBackground = true;
            metroPanel4.HorizontalScrollbar = false;
            metroPanel4.HorizontalScrollbarBarColor = true;
            metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel4.HorizontalScrollbarSize = 10;
            metroPanel4.Location = new Point(403, 287);
            metroPanel4.Name = "metroPanel4";
            metroPanel4.Padding = new Padding(2);
            metroPanel4.Size = new Size(302, 25);
            metroPanel4.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel4.StyleManager = null;
            metroPanel4.TabIndex = 42;
            metroPanel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel4.VerticalScrollbar = false;
            metroPanel4.VerticalScrollbarBarColor = true;
            metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            metroPanel4.VerticalScrollbarSize = 10;
            // 
            // LBLTrionVersion
            // 
            LBLTrionVersion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LBLTrionVersion.ForeColor = Color.FromArgb(0, 174, 219);
            LBLTrionVersion.Location = new Point(2, 2);
            LBLTrionVersion.Name = "LBLTrionVersion";
            LBLTrionVersion.Size = new Size(298, 21);
            LBLTrionVersion.TabIndex = 32;
            LBLTrionVersion.Text = "Trion Version: Local / Online";
            LBLTrionVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNTrionUpdate
            // 
            BTNTrionUpdate.Anchor = AnchorStyles.Right;
            BTNTrionUpdate.BackColor = Color.FromArgb(28, 33, 40);
            BTNTrionUpdate.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNTrionUpdate.BorderColor = Color.FromArgb(0, 174, 219);
            BTNTrionUpdate.BorderRadius = 0;
            BTNTrionUpdate.BorderSize = 1;
            BTNTrionUpdate.Cursor = Cursors.Hand;
            BTNTrionUpdate.FlatAppearance.BorderSize = 0;
            BTNTrionUpdate.FlatStyle = FlatStyle.Flat;
            BTNTrionUpdate.ForeColor = Color.White;
            BTNTrionUpdate.Image = (Image)resources.GetObject("BTNTrionUpdate.Image");
            BTNTrionUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            BTNTrionUpdate.Location = new Point(711, 287);
            BTNTrionUpdate.Name = "BTNTrionUpdate";
            BTNTrionUpdate.RightToLeft = RightToLeft.No;
            BTNTrionUpdate.Size = new Size(104, 25);
            BTNTrionUpdate.TabIndex = 41;
            BTNTrionUpdate.Text = "   Update";
            BTNTrionUpdate.TextColor = Color.White;
            BTNTrionUpdate.UseVisualStyleBackColor = false;
            // 
            // metroPanel3
            // 
            metroPanel3.Anchor = AnchorStyles.Left;
            metroPanel3.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel3.Border = true;
            metroPanel3.BorderColor = Color.Black;
            metroPanel3.BorderSize = 1;
            metroPanel3.Controls.Add(LBLAutoUpdateMySQL);
            metroPanel3.Controls.Add(TGLAutoUpdateMySQL);
            metroPanel3.Controls.Add(LBLAutoUpdateSPP);
            metroPanel3.Controls.Add(TGLAutoUpdateCore);
            metroPanel3.Controls.Add(LBLAutoUpdateTrion);
            metroPanel3.Controls.Add(TGLAutoUpdateTrion);
            metroPanel3.Controls.Add(TGLStayInTrey);
            metroPanel3.Controls.Add(TGLNotificationSound);
            metroPanel3.Controls.Add(TGLHideConsole);
            metroPanel3.Controls.Add(LBLHideConsole);
            metroPanel3.Controls.Add(LBLStayInTray);
            metroPanel3.Controls.Add(LBLNotiSound);
            metroPanel3.CustomBackground = true;
            metroPanel3.HorizontalScrollbar = false;
            metroPanel3.HorizontalScrollbarBarColor = true;
            metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel3.HorizontalScrollbarSize = 10;
            metroPanel3.Location = new Point(19, 116);
            metroPanel3.Name = "metroPanel3";
            metroPanel3.Padding = new Padding(2);
            metroPanel3.Size = new Size(186, 196);
            metroPanel3.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel3.StyleManager = null;
            metroPanel3.TabIndex = 40;
            metroPanel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel3.VerticalScrollbar = false;
            metroPanel3.VerticalScrollbarBarColor = true;
            metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            metroPanel3.VerticalScrollbarSize = 10;
            // 
            // LBLAutoUpdateMySQL
            // 
            LBLAutoUpdateMySQL.AutoSize = true;
            LBLAutoUpdateMySQL.ForeColor = Color.White;
            LBLAutoUpdateMySQL.Location = new Point(63, 157);
            LBLAutoUpdateMySQL.Name = "LBLAutoUpdateMySQL";
            LBLAutoUpdateMySQL.Size = new Size(115, 15);
            LBLAutoUpdateMySQL.TabIndex = 37;
            LBLAutoUpdateMySQL.Text = "Auto Update MySQL";
            // 
            // TGLAutoUpdateMySQL
            // 
            TGLAutoUpdateMySQL.AutoSize = true;
            TGLAutoUpdateMySQL.Cursor = Cursors.Hand;
            TGLAutoUpdateMySQL.Location = new Point(12, 154);
            TGLAutoUpdateMySQL.MinimumSize = new Size(45, 22);
            TGLAutoUpdateMySQL.Name = "TGLAutoUpdateMySQL";
            TGLAutoUpdateMySQL.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateMySQL.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLAutoUpdateMySQL.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateMySQL.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLAutoUpdateMySQL.Size = new Size(45, 22);
            TGLAutoUpdateMySQL.SolidStyle = false;
            TGLAutoUpdateMySQL.TabIndex = 36;
            TGLAutoUpdateMySQL.UseVisualStyleBackColor = true;
            TGLAutoUpdateMySQL.CheckedChanged += TGLAutoUpdateMySQL_CheckedChanged;
            // 
            // LBLAutoUpdateSPP
            // 
            LBLAutoUpdateSPP.AutoSize = true;
            LBLAutoUpdateSPP.ForeColor = Color.White;
            LBLAutoUpdateSPP.Location = new Point(63, 129);
            LBLAutoUpdateSPP.Name = "LBLAutoUpdateSPP";
            LBLAutoUpdateSPP.Size = new Size(106, 15);
            LBLAutoUpdateSPP.TabIndex = 35;
            LBLAutoUpdateSPP.Text = "Auto Update S.P.P.";
            // 
            // TGLAutoUpdateCore
            // 
            TGLAutoUpdateCore.AutoSize = true;
            TGLAutoUpdateCore.Cursor = Cursors.Hand;
            TGLAutoUpdateCore.Location = new Point(12, 126);
            TGLAutoUpdateCore.MinimumSize = new Size(45, 22);
            TGLAutoUpdateCore.Name = "TGLAutoUpdateCore";
            TGLAutoUpdateCore.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateCore.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLAutoUpdateCore.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateCore.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLAutoUpdateCore.Size = new Size(45, 22);
            TGLAutoUpdateCore.SolidStyle = false;
            TGLAutoUpdateCore.TabIndex = 34;
            TGLAutoUpdateCore.UseVisualStyleBackColor = true;
            TGLAutoUpdateCore.CheckedChanged += TGLAutoUpdateCore_CheckedChanged;
            // 
            // LBLAutoUpdateTrion
            // 
            LBLAutoUpdateTrion.AutoSize = true;
            LBLAutoUpdateTrion.ForeColor = Color.White;
            LBLAutoUpdateTrion.Location = new Point(63, 101);
            LBLAutoUpdateTrion.Name = "LBLAutoUpdateTrion";
            LBLAutoUpdateTrion.Size = new Size(106, 15);
            LBLAutoUpdateTrion.TabIndex = 33;
            LBLAutoUpdateTrion.Text = "Auto Update Trion.";
            // 
            // TGLAutoUpdateTrion
            // 
            TGLAutoUpdateTrion.AutoSize = true;
            TGLAutoUpdateTrion.Cursor = Cursors.Hand;
            TGLAutoUpdateTrion.Location = new Point(12, 98);
            TGLAutoUpdateTrion.MinimumSize = new Size(45, 22);
            TGLAutoUpdateTrion.Name = "TGLAutoUpdateTrion";
            TGLAutoUpdateTrion.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateTrion.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLAutoUpdateTrion.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLAutoUpdateTrion.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLAutoUpdateTrion.Size = new Size(45, 22);
            TGLAutoUpdateTrion.SolidStyle = false;
            TGLAutoUpdateTrion.TabIndex = 32;
            TGLAutoUpdateTrion.UseVisualStyleBackColor = true;
            TGLAutoUpdateTrion.CheckedChanged += TGLAutoUpdateTrion_CheckedChanged;
            // 
            // TGLStayInTrey
            // 
            TGLStayInTrey.AutoSize = true;
            TGLStayInTrey.Cursor = Cursors.Hand;
            TGLStayInTrey.Location = new Point(12, 14);
            TGLStayInTrey.MinimumSize = new Size(45, 22);
            TGLStayInTrey.Name = "TGLStayInTrey";
            TGLStayInTrey.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLStayInTrey.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLStayInTrey.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLStayInTrey.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLStayInTrey.Size = new Size(45, 22);
            TGLStayInTrey.SolidStyle = false;
            TGLStayInTrey.TabIndex = 16;
            TGLStayInTrey.UseVisualStyleBackColor = true;
            TGLStayInTrey.CheckedChanged += TGLStayInTrey_CheckedChanged;
            // 
            // TGLNotificationSound
            // 
            TGLNotificationSound.AutoSize = true;
            TGLNotificationSound.Cursor = Cursors.Hand;
            TGLNotificationSound.Location = new Point(12, 42);
            TGLNotificationSound.MinimumSize = new Size(45, 22);
            TGLNotificationSound.Name = "TGLNotificationSound";
            TGLNotificationSound.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLNotificationSound.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLNotificationSound.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLNotificationSound.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLNotificationSound.Size = new Size(45, 22);
            TGLNotificationSound.SolidStyle = false;
            TGLNotificationSound.TabIndex = 17;
            TGLNotificationSound.UseVisualStyleBackColor = true;
            TGLNotificationSound.CheckedChanged += TGLNotificationSound_CheckedChanged;
            // 
            // TGLHideConsole
            // 
            TGLHideConsole.AutoSize = true;
            TGLHideConsole.Cursor = Cursors.Hand;
            TGLHideConsole.Location = new Point(12, 70);
            TGLHideConsole.MinimumSize = new Size(45, 22);
            TGLHideConsole.Name = "TGLHideConsole";
            TGLHideConsole.OffBackColor = Color.FromArgb(0, 174, 219);
            TGLHideConsole.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLHideConsole.OnBackColor = Color.FromArgb(0, 174, 219);
            TGLHideConsole.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLHideConsole.Size = new Size(45, 22);
            TGLHideConsole.SolidStyle = false;
            TGLHideConsole.TabIndex = 18;
            TGLHideConsole.UseVisualStyleBackColor = true;
            TGLHideConsole.CheckedChanged += TGLHideConsole_CheckedChanged;
            // 
            // LBLHideConsole
            // 
            LBLHideConsole.AutoSize = true;
            LBLHideConsole.ForeColor = Color.White;
            LBLHideConsole.Location = new Point(63, 73);
            LBLHideConsole.Name = "LBLHideConsole";
            LBLHideConsole.Size = new Size(81, 15);
            LBLHideConsole.TabIndex = 21;
            LBLHideConsole.Text = "Hide Console.";
            // 
            // LBLStayInTray
            // 
            LBLStayInTray.AutoSize = true;
            LBLStayInTray.ForeColor = Color.White;
            LBLStayInTray.Location = new Point(63, 17);
            LBLStayInTray.Name = "LBLStayInTray";
            LBLStayInTray.Size = new Size(68, 15);
            LBLStayInTray.TabIndex = 19;
            LBLStayInTray.Text = "Stay in tray.";
            // 
            // LBLNotiSound
            // 
            LBLNotiSound.AutoSize = true;
            LBLNotiSound.ForeColor = Color.White;
            LBLNotiSound.Location = new Point(63, 45);
            LBLNotiSound.Name = "LBLNotiSound";
            LBLNotiSound.Size = new Size(109, 15);
            LBLNotiSound.TabIndex = 20;
            LBLNotiSound.Text = "Notification sound.";
            // 
            // BTNMySQLOpenFolder
            // 
            BTNMySQLOpenFolder.Anchor = AnchorStyles.Left;
            BTNMySQLOpenFolder.BackColor = Color.FromArgb(28, 33, 40);
            BTNMySQLOpenFolder.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNMySQLOpenFolder.BorderColor = Color.FromArgb(0, 174, 219);
            BTNMySQLOpenFolder.BorderRadius = 0;
            BTNMySQLOpenFolder.BorderSize = 1;
            BTNMySQLOpenFolder.Cursor = Cursors.Hand;
            BTNMySQLOpenFolder.FlatAppearance.BorderSize = 0;
            BTNMySQLOpenFolder.FlatStyle = FlatStyle.Flat;
            BTNMySQLOpenFolder.ForeColor = Color.White;
            BTNMySQLOpenFolder.Image = (Image)resources.GetObject("BTNMySQLOpenFolder.Image");
            BTNMySQLOpenFolder.ImageAlign = ContentAlignment.MiddleLeft;
            BTNMySQLOpenFolder.Location = new Point(211, 287);
            BTNMySQLOpenFolder.Name = "BTNMySQLOpenFolder";
            BTNMySQLOpenFolder.RightToLeft = RightToLeft.No;
            BTNMySQLOpenFolder.Size = new Size(186, 25);
            BTNMySQLOpenFolder.TabIndex = 31;
            BTNMySQLOpenFolder.Text = "   MySQL Working Directory";
            BTNMySQLOpenFolder.TextColor = Color.White;
            BTNMySQLOpenFolder.UseVisualStyleBackColor = false;
            // 
            // BTNCoreOpenFolder
            // 
            BTNCoreOpenFolder.Anchor = AnchorStyles.Left;
            BTNCoreOpenFolder.BackColor = Color.FromArgb(28, 33, 40);
            BTNCoreOpenFolder.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNCoreOpenFolder.BorderColor = Color.FromArgb(0, 174, 219);
            BTNCoreOpenFolder.BorderRadius = 0;
            BTNCoreOpenFolder.BorderSize = 1;
            BTNCoreOpenFolder.Cursor = Cursors.Hand;
            BTNCoreOpenFolder.FlatAppearance.BorderSize = 0;
            BTNCoreOpenFolder.FlatStyle = FlatStyle.Flat;
            BTNCoreOpenFolder.ForeColor = Color.White;
            BTNCoreOpenFolder.Image = (Image)resources.GetObject("BTNCoreOpenFolder.Image");
            BTNCoreOpenFolder.ImageAlign = ContentAlignment.MiddleLeft;
            BTNCoreOpenFolder.Location = new Point(211, 257);
            BTNCoreOpenFolder.Name = "BTNCoreOpenFolder";
            BTNCoreOpenFolder.RightToLeft = RightToLeft.No;
            BTNCoreOpenFolder.Size = new Size(186, 25);
            BTNCoreOpenFolder.TabIndex = 28;
            BTNCoreOpenFolder.Text = "   Core Working Directory";
            BTNCoreOpenFolder.TextColor = Color.White;
            BTNCoreOpenFolder.UseVisualStyleBackColor = false;
            // 
            // BTNMySQLExecLovation
            // 
            BTNMySQLExecLovation.Anchor = AnchorStyles.Right;
            BTNMySQLExecLovation.BackColor = Color.FromArgb(28, 33, 40);
            BTNMySQLExecLovation.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNMySQLExecLovation.BorderColor = Color.FromArgb(0, 174, 219);
            BTNMySQLExecLovation.BorderRadius = 0;
            BTNMySQLExecLovation.BorderSize = 1;
            BTNMySQLExecLovation.Cursor = Cursors.Hand;
            BTNMySQLExecLovation.FlatAppearance.BorderSize = 0;
            BTNMySQLExecLovation.FlatStyle = FlatStyle.Flat;
            BTNMySQLExecLovation.ForeColor = Color.White;
            BTNMySQLExecLovation.Image = (Image)resources.GetObject("BTNMySQLExecLovation.Image");
            BTNMySQLExecLovation.ImageAlign = ContentAlignment.MiddleLeft;
            BTNMySQLExecLovation.Location = new Point(743, 85);
            BTNMySQLExecLovation.Name = "BTNMySQLExecLovation";
            BTNMySQLExecLovation.RightToLeft = RightToLeft.No;
            BTNMySQLExecLovation.Size = new Size(72, 25);
            BTNMySQLExecLovation.TabIndex = 13;
            BTNMySQLExecLovation.Text = "   ...";
            BTNMySQLExecLovation.TextColor = Color.White;
            BTNMySQLExecLovation.UseVisualStyleBackColor = false;
            BTNMySQLExecLovation.Click += BTNMySQLExecLovation_Click;
            // 
            // BTNCoreExecLovation
            // 
            BTNCoreExecLovation.Anchor = AnchorStyles.Right;
            BTNCoreExecLovation.BackColor = Color.FromArgb(28, 33, 40);
            BTNCoreExecLovation.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNCoreExecLovation.BorderColor = Color.FromArgb(0, 174, 219);
            BTNCoreExecLovation.BorderRadius = 0;
            BTNCoreExecLovation.BorderSize = 1;
            BTNCoreExecLovation.Cursor = Cursors.Hand;
            BTNCoreExecLovation.FlatAppearance.BorderSize = 0;
            BTNCoreExecLovation.FlatStyle = FlatStyle.Flat;
            BTNCoreExecLovation.ForeColor = Color.White;
            BTNCoreExecLovation.Image = (Image)resources.GetObject("BTNCoreExecLovation.Image");
            BTNCoreExecLovation.ImageAlign = ContentAlignment.MiddleLeft;
            BTNCoreExecLovation.Location = new Point(743, 30);
            BTNCoreExecLovation.Name = "BTNCoreExecLovation";
            BTNCoreExecLovation.RightToLeft = RightToLeft.No;
            BTNCoreExecLovation.Size = new Size(72, 25);
            BTNCoreExecLovation.TabIndex = 11;
            BTNCoreExecLovation.Text = "   ...";
            BTNCoreExecLovation.TextColor = Color.White;
            BTNCoreExecLovation.UseVisualStyleBackColor = false;
            BTNCoreExecLovation.Click += BTNCoreExecLovation_Click;
            // 
            // LBLMysqlWorkingDIr
            // 
            LBLMysqlWorkingDIr.Anchor = AnchorStyles.Left;
            LBLMysqlWorkingDIr.AutoSize = true;
            LBLMysqlWorkingDIr.ForeColor = Color.White;
            LBLMysqlWorkingDIr.Location = new Point(19, 66);
            LBLMysqlWorkingDIr.Name = "LBLMysqlWorkingDIr";
            LBLMysqlWorkingDIr.Size = new Size(144, 15);
            LBLMysqlWorkingDIr.TabIndex = 5;
            LBLMysqlWorkingDIr.Text = "MySQL Working Directory";
            // 
            // LBLCorelWorkingDIr
            // 
            LBLCorelWorkingDIr.Anchor = AnchorStyles.Left;
            LBLCorelWorkingDIr.AutoSize = true;
            LBLCorelWorkingDIr.ForeColor = Color.White;
            LBLCorelWorkingDIr.Location = new Point(19, 11);
            LBLCorelWorkingDIr.Name = "LBLCorelWorkingDIr";
            LBLCorelWorkingDIr.Size = new Size(131, 15);
            LBLCorelWorkingDIr.TabIndex = 4;
            LBLCorelWorkingDIr.Text = "Core Working Directory";
            // 
            // TXTBoxMySQLLocation
            // 
            TXTBoxMySQLLocation.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxMySQLLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxMySQLLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxMySQLLocation.ForeColor = Color.White;
            TXTBoxMySQLLocation.Location = new Point(19, 85);
            TXTBoxMySQLLocation.Multiline = false;
            TXTBoxMySQLLocation.Name = "TXTBoxMySQLLocation";
            TXTBoxMySQLLocation.PasswordChar = '\0';
            TXTBoxMySQLLocation.ReadOnly = true;
            TXTBoxMySQLLocation.SelectedText = "";
            TXTBoxMySQLLocation.Size = new Size(718, 25);
            TXTBoxMySQLLocation.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxMySQLLocation.StyleManager = null;
            TXTBoxMySQLLocation.TabIndex = 2;
            TXTBoxMySQLLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxMySQLLocation.UseStyleColors = true;
            // 
            // TXTBoxCoreLocation
            // 
            TXTBoxCoreLocation.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxCoreLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxCoreLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxCoreLocation.ForeColor = Color.White;
            TXTBoxCoreLocation.Location = new Point(19, 30);
            TXTBoxCoreLocation.Multiline = false;
            TXTBoxCoreLocation.Name = "TXTBoxCoreLocation";
            TXTBoxCoreLocation.PasswordChar = '\0';
            TXTBoxCoreLocation.ReadOnly = true;
            TXTBoxCoreLocation.SelectedText = "";
            TXTBoxCoreLocation.Size = new Size(718, 25);
            TXTBoxCoreLocation.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxCoreLocation.StyleManager = null;
            TXTBoxCoreLocation.TabIndex = 0;
            TXTBoxCoreLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxCoreLocation.UseStyleColors = true;
            // 
            // TabPageDatabase
            // 
            TabPageDatabase.AccessibleDescription = "";
            TabPageDatabase.AccessibleName = "";
            TabPageDatabase.BackColor = Color.FromArgb(45, 51, 59);
            TabPageDatabase.Controls.Add(panel1);
            TabPageDatabase.Controls.Add(panel3);
            TabPageDatabase.Location = new Point(4, 38);
            TabPageDatabase.Name = "TabPageDatabase";
            TabPageDatabase.Size = new Size(837, 328);
            TabPageDatabase.TabIndex = 2;
            TabPageDatabase.Text = "Database";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(28, 33, 40);
            panel1.Border = true;
            panel1.BorderColor = Color.Black;
            panel1.BorderSize = 1;
            panel1.Controls.Add(label21);
            panel1.Controls.Add(TXTWorldDatabase);
            panel1.Controls.Add(label22);
            panel1.Controls.Add(TXTCharDatabase);
            panel1.Controls.Add(label23);
            panel1.Controls.Add(TXTAuthDatabase);
            panel1.CustomBackground = false;
            panel1.HorizontalScrollbar = false;
            panel1.HorizontalScrollbarBarColor = true;
            panel1.HorizontalScrollbarHighlightOnWheel = false;
            panel1.HorizontalScrollbarSize = 10;
            panel1.Location = new Point(427, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(383, 289);
            panel1.Style = MetroFramework.MetroColorStyle.Blue;
            panel1.StyleManager = null;
            panel1.TabIndex = 14;
            panel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            panel1.VerticalScrollbar = false;
            panel1.VerticalScrollbarBarColor = true;
            panel1.VerticalScrollbarHighlightOnWheel = false;
            panel1.VerticalScrollbarSize = 10;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.ForeColor = Color.White;
            label21.Location = new Point(18, 113);
            label21.Name = "label21";
            label21.Size = new Size(39, 15);
            label21.TabIndex = 24;
            label21.Text = "Word:";
            // 
            // TXTWorldDatabase
            // 
            TXTWorldDatabase.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTWorldDatabase.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTWorldDatabase.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTWorldDatabase.ForeColor = Color.White;
            TXTWorldDatabase.Location = new Point(18, 131);
            TXTWorldDatabase.Multiline = false;
            TXTWorldDatabase.Name = "TXTWorldDatabase";
            TXTWorldDatabase.PasswordChar = '\0';
            TXTWorldDatabase.ReadOnly = false;
            TXTWorldDatabase.SelectedText = "";
            TXTWorldDatabase.Size = new Size(347, 23);
            TXTWorldDatabase.Style = MetroFramework.MetroColorStyle.Blue;
            TXTWorldDatabase.StyleManager = null;
            TXTWorldDatabase.TabIndex = 23;
            TXTWorldDatabase.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTWorldDatabase.UseStyleColors = true;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.ForeColor = Color.White;
            label22.Location = new Point(18, 69);
            label22.Name = "label22";
            label22.Size = new Size(58, 15);
            label22.TabIndex = 22;
            label22.Text = "Character";
            // 
            // TXTCharDatabase
            // 
            TXTCharDatabase.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTCharDatabase.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTCharDatabase.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTCharDatabase.ForeColor = Color.White;
            TXTCharDatabase.Location = new Point(18, 87);
            TXTCharDatabase.Multiline = false;
            TXTCharDatabase.Name = "TXTCharDatabase";
            TXTCharDatabase.PasswordChar = '\0';
            TXTCharDatabase.ReadOnly = false;
            TXTCharDatabase.SelectedText = "";
            TXTCharDatabase.Size = new Size(347, 23);
            TXTCharDatabase.Style = MetroFramework.MetroColorStyle.Blue;
            TXTCharDatabase.StyleManager = null;
            TXTCharDatabase.TabIndex = 21;
            TXTCharDatabase.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTCharDatabase.UseStyleColors = true;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = Color.White;
            label23.Location = new Point(18, 25);
            label23.Name = "label23";
            label23.Size = new Size(36, 15);
            label23.TabIndex = 20;
            label23.Text = "Auth:";
            // 
            // TXTAuthDatabase
            // 
            TXTAuthDatabase.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTAuthDatabase.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTAuthDatabase.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTAuthDatabase.ForeColor = Color.White;
            TXTAuthDatabase.Location = new Point(18, 43);
            TXTAuthDatabase.Multiline = false;
            TXTAuthDatabase.Name = "TXTAuthDatabase";
            TXTAuthDatabase.PasswordChar = '\0';
            TXTAuthDatabase.ReadOnly = false;
            TXTAuthDatabase.SelectedText = "";
            TXTAuthDatabase.Size = new Size(347, 23);
            TXTAuthDatabase.Style = MetroFramework.MetroColorStyle.Blue;
            TXTAuthDatabase.StyleManager = null;
            TXTAuthDatabase.TabIndex = 14;
            TXTAuthDatabase.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTAuthDatabase.UseStyleColors = true;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(28, 33, 40);
            panel3.Border = true;
            panel3.BorderColor = Color.Black;
            panel3.BorderSize = 1;
            panel3.Controls.Add(BTNTestConnection);
            panel3.Controls.Add(label18);
            panel3.Controls.Add(TXTMysqlPassword);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(TXTMysqlUser);
            panel3.Controls.Add(label13);
            panel3.Controls.Add(TXTMysqlPort);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(TXTMysqlHost);
            panel3.CustomBackground = false;
            panel3.HorizontalScrollbar = false;
            panel3.HorizontalScrollbarBarColor = true;
            panel3.HorizontalScrollbarHighlightOnWheel = false;
            panel3.HorizontalScrollbarSize = 10;
            panel3.Location = new Point(23, 22);
            panel3.Name = "panel3";
            panel3.Size = new Size(383, 289);
            panel3.Style = MetroFramework.MetroColorStyle.Blue;
            panel3.StyleManager = null;
            panel3.TabIndex = 13;
            panel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            panel3.VerticalScrollbar = false;
            panel3.VerticalScrollbarBarColor = true;
            panel3.VerticalScrollbarHighlightOnWheel = false;
            panel3.VerticalScrollbarSize = 10;
            // 
            // BTNTestConnection
            // 
            BTNTestConnection.Anchor = AnchorStyles.Top;
            BTNTestConnection.BackColor = Color.FromArgb(28, 33, 40);
            BTNTestConnection.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNTestConnection.BorderColor = Color.FromArgb(0, 174, 219);
            BTNTestConnection.BorderRadius = 0;
            BTNTestConnection.BorderSize = 1;
            BTNTestConnection.Cursor = Cursors.Hand;
            BTNTestConnection.FlatAppearance.BorderSize = 0;
            BTNTestConnection.FlatStyle = FlatStyle.Flat;
            BTNTestConnection.ForeColor = Color.White;
            BTNTestConnection.Image = (Image)resources.GetObject("BTNTestConnection.Image");
            BTNTestConnection.ImageAlign = ContentAlignment.MiddleLeft;
            BTNTestConnection.Location = new Point(18, 244);
            BTNTestConnection.Name = "BTNTestConnection";
            BTNTestConnection.RightToLeft = RightToLeft.No;
            BTNTestConnection.Size = new Size(133, 30);
            BTNTestConnection.TabIndex = 27;
            BTNTestConnection.Text = "   Test Connection";
            BTNTestConnection.TextColor = Color.White;
            BTNTestConnection.UseVisualStyleBackColor = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = Color.White;
            label18.Location = new Point(18, 157);
            label18.Name = "label18";
            label18.Size = new Size(60, 15);
            label18.TabIndex = 26;
            label18.Text = "Password:";
            // 
            // TXTMysqlPassword
            // 
            TXTMysqlPassword.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTMysqlPassword.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTMysqlPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTMysqlPassword.ForeColor = Color.White;
            TXTMysqlPassword.Location = new Point(18, 175);
            TXTMysqlPassword.Multiline = false;
            TXTMysqlPassword.Name = "TXTMysqlPassword";
            TXTMysqlPassword.PasswordChar = '*';
            TXTMysqlPassword.ReadOnly = false;
            TXTMysqlPassword.SelectedText = "";
            TXTMysqlPassword.Size = new Size(347, 23);
            TXTMysqlPassword.Style = MetroFramework.MetroColorStyle.Blue;
            TXTMysqlPassword.StyleManager = null;
            TXTMysqlPassword.TabIndex = 25;
            TXTMysqlPassword.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTMysqlPassword.UseStyleColors = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(18, 113);
            label15.Name = "label15";
            label15.Size = new Size(63, 15);
            label15.TabIndex = 24;
            label15.Text = "Username:";
            // 
            // TXTMysqlUser
            // 
            TXTMysqlUser.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTMysqlUser.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTMysqlUser.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTMysqlUser.ForeColor = Color.White;
            TXTMysqlUser.Location = new Point(18, 131);
            TXTMysqlUser.Multiline = false;
            TXTMysqlUser.Name = "TXTMysqlUser";
            TXTMysqlUser.PasswordChar = '\0';
            TXTMysqlUser.ReadOnly = false;
            TXTMysqlUser.SelectedText = "";
            TXTMysqlUser.Size = new Size(347, 23);
            TXTMysqlUser.Style = MetroFramework.MetroColorStyle.Blue;
            TXTMysqlUser.StyleManager = null;
            TXTMysqlUser.TabIndex = 23;
            TXTMysqlUser.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTMysqlUser.UseStyleColors = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(18, 69);
            label13.Name = "label13";
            label13.Size = new Size(32, 15);
            label13.TabIndex = 22;
            label13.Text = "Port:";
            // 
            // TXTMysqlPort
            // 
            TXTMysqlPort.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTMysqlPort.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTMysqlPort.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTMysqlPort.ForeColor = Color.White;
            TXTMysqlPort.Location = new Point(18, 87);
            TXTMysqlPort.Multiline = false;
            TXTMysqlPort.Name = "TXTMysqlPort";
            TXTMysqlPort.PasswordChar = '\0';
            TXTMysqlPort.ReadOnly = false;
            TXTMysqlPort.SelectedText = "";
            TXTMysqlPort.Size = new Size(347, 23);
            TXTMysqlPort.Style = MetroFramework.MetroColorStyle.Blue;
            TXTMysqlPort.StyleManager = null;
            TXTMysqlPort.TabIndex = 21;
            TXTMysqlPort.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTMysqlPort.UseStyleColors = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(18, 25);
            label12.Name = "label12";
            label12.Size = new Size(35, 15);
            label12.TabIndex = 20;
            label12.Text = "Host:";
            // 
            // TXTMysqlHost
            // 
            TXTMysqlHost.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TXTMysqlHost.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTMysqlHost.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTMysqlHost.ForeColor = Color.White;
            TXTMysqlHost.Location = new Point(18, 43);
            TXTMysqlHost.Multiline = false;
            TXTMysqlHost.Name = "TXTMysqlHost";
            TXTMysqlHost.PasswordChar = '\0';
            TXTMysqlHost.ReadOnly = false;
            TXTMysqlHost.SelectedText = "";
            TXTMysqlHost.Size = new Size(347, 23);
            TXTMysqlHost.Style = MetroFramework.MetroColorStyle.Blue;
            TXTMysqlHost.StyleManager = null;
            TXTMysqlHost.TabIndex = 14;
            TXTMysqlHost.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTMysqlHost.UseStyleColors = true;
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(metroTabControl1);
            ForeColor = Color.White;
            Name = "SettingsControl";
            Size = new Size(845, 370);
            Load += SettingsControl_LoadAsync;
            metroTabControl1.ResumeLayout(false);
            TabPageCore.ResumeLayout(false);
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            metroPanel1.ResumeLayout(false);
            metroPanel1.PerformLayout();
            PNLControl.ResumeLayout(false);
            TabPageTrion.ResumeLayout(false);
            TabPageTrion.PerformLayout();
            metroPanel4.ResumeLayout(false);
            metroPanel3.ResumeLayout(false);
            metroPanel3.PerformLayout();
            TabPageDatabase.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private TabPage TabPageTrion;
        private TabPage TabPageDatabase;
        private MetroFramework.Controls.MetroPanel panel3;
        private MetroFramework.Controls.MetroTextBox TXTBoxCoreLocation;
        private MetroFramework.Controls.MetroTextBox TXTBoxMySQLLocation;
        private Label LBLMysqlWorkingDIr;
        private Label LBLCorelWorkingDIr;
        private UI.Controls.CustomButton BTNMySQLExecLovation;
        private UI.Controls.CustomButton BTNCoreExecLovation;
        private TrionControlPanel.UI.CustomToggleButton TGLStayInTrey;
        private Label LBLHideConsole;
        private Label LBLNotiSound;
        private Label LBLStayInTray;
        private TrionControlPanel.UI.CustomToggleButton TGLHideConsole;
        private TrionControlPanel.UI.CustomToggleButton TGLNotificationSound;
        private UI.Controls.CustomButton BTNCoreOpenFolder;
        private UI.Controls.CustomButton BTNMySQLOpenFolder;
        private Label LBLAutoUpdateTrion;
        private TrionControlPanel.UI.CustomToggleButton TGLAutoUpdateTrion;
        private TabPage TabPageCore;
        private MetroFramework.Controls.MetroPanel PNLControl;
        private UI.Controls.CustomButton BTNModsConfig;
        private UI.Controls.CustomButton BTNAuthConfig;
        private UI.Controls.CustomButton BTNWorldConfig;
        private UI.Controls.CustomButton BTNDiscord;
        private UI.Controls.CustomButton BtnDownloadSPP;
        private Label label11;
        private TrionControlPanel.UI.CustomToggleButton TGLCustomNames;
        private Label label10;
        private MetroFramework.Controls.MetroTextBox TXTBoxMySQLExecName;
        private Label label9;
        private MetroFramework.Controls.MetroTextBox TXTBoxLoginExecName;
        private Label label8;
        private MetroFramework.Controls.MetroTextBox TXTBoxWorldExecName;
        private Label label1;
        private TrionControlPanel.UI.CustomComboBox ComboBoxCores;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private Label LBLMySQLVersion;
        private Label LBLCoreVersion;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private Label label19;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private Label label18;
        private MetroFramework.Controls.MetroTextBox TXTMysqlPassword;
        private Label label15;
        private MetroFramework.Controls.MetroTextBox TXTMysqlUser;
        private Label label13;
        private MetroFramework.Controls.MetroTextBox TXTMysqlPort;
        private Label label12;
        private MetroFramework.Controls.MetroTextBox TXTMysqlHost;
        private Label label21;
        private MetroFramework.Controls.MetroTextBox TXTWorldDatabase;
        private Label label22;
        private MetroFramework.Controls.MetroTextBox TXTCharDatabase;
        private Label label23;
        private MetroFramework.Controls.MetroTextBox TXTAuthDatabase;
        private UI.Controls.CustomButton BTNTestConnection;
        private UI.Controls.CustomButton BTNDownlaodMySQL;
        private UI.Controls.CustomButton BTNTrionUpdate;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private Label LBLTrionVersion;
        private MetroFramework.Controls.MetroPanel panel1;
        private Label LBLAutoUpdateSPP;
        private TrionControlPanel.UI.CustomToggleButton TGLAutoUpdateCore;
        private System.Windows.Forms.Timer TimerWacher;
        private UI.Controls.CustomButton BTNFixMysql;
        private Label LBLAutoUpdateMySQL;
        private TrionControlPanel.UI.CustomToggleButton TGLAutoUpdateMySQL;
    }
}
