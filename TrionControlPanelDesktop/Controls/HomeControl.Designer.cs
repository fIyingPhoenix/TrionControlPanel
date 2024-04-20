using MetroFramework.Controls;
using TrionControlPanel.UI;

namespace TrionControlPanelDesktop.Controls
{
    partial class HomeControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeControl));
            TimerWacher = new System.Windows.Forms.Timer(components);
            PNLLayoutBot = new TableLayoutPanel();
            metroPanel1 = new MetroPanel();
            label4 = new Label();
            LoginPbarCPU = new CustomProgressBar();
            LoginPbarRAM = new CustomProgressBar();
            label5 = new Label();
            label6 = new Label();
            metroPanel2 = new MetroPanel();
            metroPanel3 = new MetroPanel();
            LBLWorldsOpen = new Label();
            customButton1 = new UI.Controls.CustomButton();
            BTNMySQLOpenFolder = new UI.Controls.CustomButton();
            label7 = new Label();
            WorldPbarCPU = new CustomProgressBar();
            label9 = new Label();
            WorldPbarRAM = new CustomProgressBar();
            label8 = new Label();
            PNLLayoutTop = new TableLayoutPanel();
            PNLServerStatus = new MetroPanel();
            LBLWorldServerStatus = new Label();
            LBLLogonServerStatus = new Label();
            LBLMySQLServerStatus = new Label();
            PICMySqlServerStatus = new PictureBox();
            PICWorldServerStatus = new PictureBox();
            PICLogonServerStatus = new PictureBox();
            PNLPCResorce = new MetroPanel();
            label1 = new Label();
            PCResorcePbarCPU = new CustomProgressBar();
            PCResorcePbarRAM = new CustomProgressBar();
            label2 = new Label();
            label3 = new Label();
            TimerRam = new System.Windows.Forms.Timer(components);
            PNLLayoutBot.SuspendLayout();
            metroPanel1.SuspendLayout();
            metroPanel2.SuspendLayout();
            metroPanel3.SuspendLayout();
            PNLLayoutTop.SuspendLayout();
            PNLServerStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PICWorldServerStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PICLogonServerStatus).BeginInit();
            PNLPCResorce.SuspendLayout();
            SuspendLayout();
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // PNLLayoutBot
            // 
            PNLLayoutBot.ColumnCount = 2;
            PNLLayoutBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutBot.Controls.Add(metroPanel1, 1, 0);
            PNLLayoutBot.Controls.Add(metroPanel2, 0, 0);
            PNLLayoutBot.Dock = DockStyle.Bottom;
            PNLLayoutBot.Location = new Point(0, 186);
            PNLLayoutBot.Margin = new Padding(0);
            PNLLayoutBot.Name = "PNLLayoutBot";
            PNLLayoutBot.Padding = new Padding(2);
            PNLLayoutBot.RowCount = 1;
            PNLLayoutBot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            PNLLayoutBot.Size = new Size(845, 184);
            PNLLayoutBot.TabIndex = 3;
            // 
            // metroPanel1
            // 
            metroPanel1.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(label4);
            metroPanel1.Controls.Add(LoginPbarCPU);
            metroPanel1.Controls.Add(LoginPbarRAM);
            metroPanel1.Controls.Add(label5);
            metroPanel1.Controls.Add(label6);
            metroPanel1.CustomBackground = true;
            metroPanel1.Dock = DockStyle.Fill;
            metroPanel1.HorizontalScrollbar = false;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(425, 5);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Size = new Size(415, 174);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 3;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(0, 174, 219);
            label4.Location = new Point(14, 18);
            label4.Name = "label4";
            label4.Size = new Size(199, 21);
            label4.TabIndex = 32;
            label4.Text = "LOGIN SERVER RESOURCE";
            // 
            // LoginPbarCPU
            // 
            LoginPbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LoginPbarCPU.BackColor = Color.FromArgb(34, 34, 34);
            LoginPbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            LoginPbarCPU.FontSize = 10;
            LoginPbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            LoginPbarCPU.LabelText = "%";
            LoginPbarCPU.Location = new Point(13, 139);
            LoginPbarCPU.MaximumValue = false;
            LoginPbarCPU.Name = "LoginPbarCPU";
            LoginPbarCPU.ShowStatus = true;
            LoginPbarCPU.Size = new Size(390, 20);
            LoginPbarCPU.TabIndex = 36;
            LoginPbarCPU.TextColor = Color.White;
            // 
            // LoginPbarRAM
            // 
            LoginPbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LoginPbarRAM.BackColor = Color.FromArgb(34, 34, 34);
            LoginPbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            LoginPbarRAM.FontSize = 10;
            LoginPbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            LoginPbarRAM.LabelText = "MB";
            LoginPbarRAM.Location = new Point(13, 87);
            LoginPbarRAM.MaximumValue = true;
            LoginPbarRAM.Name = "LoginPbarRAM";
            LoginPbarRAM.ShowStatus = true;
            LoginPbarRAM.Size = new Size(390, 20);
            LoginPbarRAM.TabIndex = 33;
            LoginPbarRAM.TextColor = Color.White;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(128, 120);
            label5.Name = "label5";
            label5.Size = new Size(164, 15);
            label5.TabIndex = 35;
            label5.Text = "Central Processing Unit (CPU)";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(120, 66);
            label6.Name = "label6";
            label6.Size = new Size(181, 15);
            label6.TabIndex = 34;
            label6.Text = "Random-Access Memory  (RAM)";
            // 
            // metroPanel2
            // 
            metroPanel2.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(metroPanel3);
            metroPanel2.Controls.Add(customButton1);
            metroPanel2.Controls.Add(BTNMySQLOpenFolder);
            metroPanel2.Controls.Add(label7);
            metroPanel2.Controls.Add(WorldPbarCPU);
            metroPanel2.Controls.Add(label9);
            metroPanel2.Controls.Add(WorldPbarRAM);
            metroPanel2.Controls.Add(label8);
            metroPanel2.CustomBackground = true;
            metroPanel2.Dock = DockStyle.Fill;
            metroPanel2.HorizontalScrollbar = false;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(5, 5);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Size = new Size(414, 174);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 4;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = false;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroPanel3
            // 
            metroPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            metroPanel3.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel3.Border = true;
            metroPanel3.BorderColor = Color.FromArgb(0, 174, 219);
            metroPanel3.BorderSize = 1;
            metroPanel3.Controls.Add(LBLWorldsOpen);
            metroPanel3.CustomBackground = true;
            metroPanel3.HorizontalScrollbar = false;
            metroPanel3.HorizontalScrollbarBarColor = true;
            metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel3.HorizontalScrollbarSize = 10;
            metroPanel3.Location = new Point(320, 14);
            metroPanel3.Name = "metroPanel3";
            metroPanel3.Padding = new Padding(2);
            metroPanel3.Size = new Size(55, 25);
            metroPanel3.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel3.StyleManager = null;
            metroPanel3.TabIndex = 44;
            metroPanel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel3.VerticalScrollbar = false;
            metroPanel3.VerticalScrollbarBarColor = true;
            metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            metroPanel3.VerticalScrollbarSize = 10;
            metroPanel3.Visible = false;
            // 
            // LBLWorldsOpen
            // 
            LBLWorldsOpen.Dock = DockStyle.Fill;
            LBLWorldsOpen.ForeColor = Color.White;
            LBLWorldsOpen.Location = new Point(2, 2);
            LBLWorldsOpen.Name = "LBLWorldsOpen";
            LBLWorldsOpen.Size = new Size(51, 21);
            LBLWorldsOpen.TabIndex = 19;
            LBLWorldsOpen.Text = "0/0";
            LBLWorldsOpen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // customButton1
            // 
            customButton1.Anchor = AnchorStyles.Right;
            customButton1.BackColor = Color.FromArgb(28, 33, 40);
            customButton1.BackgroundColor = Color.FromArgb(28, 33, 40);
            customButton1.BorderColor = Color.FromArgb(0, 174, 219);
            customButton1.BorderRadius = 0;
            customButton1.BorderSize = 1;
            customButton1.Cursor = Cursors.Hand;
            customButton1.FlatAppearance.BorderSize = 0;
            customButton1.FlatStyle = FlatStyle.Flat;
            customButton1.ForeColor = Color.White;
            customButton1.Image = (Image)resources.GetObject("customButton1.Image");
            customButton1.ImageAlign = ContentAlignment.MiddleLeft;
            customButton1.Location = new Point(376, 14);
            customButton1.Name = "customButton1";
            customButton1.NotificationCount = 0;
            customButton1.RightToLeft = RightToLeft.No;
            customButton1.Size = new Size(25, 25);
            customButton1.TabIndex = 43;
            customButton1.TextColor = Color.White;
            customButton1.UseVisualStyleBackColor = false;
            customButton1.Visible = false;
            // 
            // BTNMySQLOpenFolder
            // 
            BTNMySQLOpenFolder.Anchor = AnchorStyles.Right;
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
            BTNMySQLOpenFolder.Location = new Point(293, 14);
            BTNMySQLOpenFolder.Name = "BTNMySQLOpenFolder";
            BTNMySQLOpenFolder.NotificationCount = 0;
            BTNMySQLOpenFolder.RightToLeft = RightToLeft.No;
            BTNMySQLOpenFolder.Size = new Size(25, 25);
            BTNMySQLOpenFolder.TabIndex = 42;
            BTNMySQLOpenFolder.TextColor = Color.White;
            BTNMySQLOpenFolder.UseVisualStyleBackColor = false;
            BTNMySQLOpenFolder.Visible = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(0, 174, 219);
            label7.Location = new Point(14, 18);
            label7.Name = "label7";
            label7.Size = new Size(207, 21);
            label7.TabIndex = 37;
            label7.Text = "WORLD SERVER RESOURCE";
            // 
            // WorldPbarCPU
            // 
            WorldPbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            WorldPbarCPU.BackColor = Color.FromArgb(34, 34, 34);
            WorldPbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            WorldPbarCPU.FontSize = 10;
            WorldPbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            WorldPbarCPU.LabelText = "%";
            WorldPbarCPU.Location = new Point(13, 139);
            WorldPbarCPU.MaximumValue = false;
            WorldPbarCPU.Name = "WorldPbarCPU";
            WorldPbarCPU.ShowStatus = true;
            WorldPbarCPU.Size = new Size(390, 20);
            WorldPbarCPU.TabIndex = 41;
            WorldPbarCPU.TextColor = Color.White;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(120, 66);
            label9.Name = "label9";
            label9.Size = new Size(181, 15);
            label9.TabIndex = 39;
            label9.Text = "Random-Access Memory  (RAM)";
            // 
            // WorldPbarRAM
            // 
            WorldPbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            WorldPbarRAM.BackColor = Color.FromArgb(34, 34, 34);
            WorldPbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            WorldPbarRAM.FontSize = 10;
            WorldPbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            WorldPbarRAM.LabelText = "MB";
            WorldPbarRAM.Location = new Point(13, 87);
            WorldPbarRAM.MaximumValue = true;
            WorldPbarRAM.Name = "WorldPbarRAM";
            WorldPbarRAM.ShowStatus = true;
            WorldPbarRAM.Size = new Size(390, 20);
            WorldPbarRAM.TabIndex = 38;
            WorldPbarRAM.TextColor = Color.White;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(128, 120);
            label8.Name = "label8";
            label8.Size = new Size(164, 15);
            label8.TabIndex = 40;
            label8.Text = "Central Processing Unit (CPU)";
            // 
            // PNLLayoutTop
            // 
            PNLLayoutTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PNLLayoutTop.ColumnCount = 2;
            PNLLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutTop.Controls.Add(PNLServerStatus, 0, 0);
            PNLLayoutTop.Controls.Add(PNLPCResorce, 1, 0);
            PNLLayoutTop.Location = new Point(0, 0);
            PNLLayoutTop.Margin = new Padding(0);
            PNLLayoutTop.Name = "PNLLayoutTop";
            PNLLayoutTop.Padding = new Padding(2);
            PNLLayoutTop.RowCount = 1;
            PNLLayoutTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            PNLLayoutTop.Size = new Size(845, 186);
            PNLLayoutTop.TabIndex = 2;
            // 
            // PNLServerStatus
            // 
            PNLServerStatus.BackColor = Color.FromArgb(28, 33, 40);
            PNLServerStatus.Border = true;
            PNLServerStatus.BorderColor = Color.Black;
            PNLServerStatus.BorderSize = 1;
            PNLServerStatus.Controls.Add(LBLWorldServerStatus);
            PNLServerStatus.Controls.Add(LBLLogonServerStatus);
            PNLServerStatus.Controls.Add(LBLMySQLServerStatus);
            PNLServerStatus.Controls.Add(PICMySqlServerStatus);
            PNLServerStatus.Controls.Add(PICWorldServerStatus);
            PNLServerStatus.Controls.Add(PICLogonServerStatus);
            PNLServerStatus.CustomBackground = true;
            PNLServerStatus.Dock = DockStyle.Fill;
            PNLServerStatus.HorizontalScrollbar = true;
            PNLServerStatus.HorizontalScrollbarBarColor = true;
            PNLServerStatus.HorizontalScrollbarHighlightOnWheel = false;
            PNLServerStatus.HorizontalScrollbarSize = 10;
            PNLServerStatus.Location = new Point(5, 5);
            PNLServerStatus.Name = "PNLServerStatus";
            PNLServerStatus.Padding = new Padding(2);
            PNLServerStatus.Size = new Size(414, 176);
            PNLServerStatus.Style = MetroFramework.MetroColorStyle.Blue;
            PNLServerStatus.StyleManager = null;
            PNLServerStatus.TabIndex = 0;
            PNLServerStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLServerStatus.VerticalScrollbar = true;
            PNLServerStatus.VerticalScrollbarBarColor = true;
            PNLServerStatus.VerticalScrollbarHighlightOnWheel = false;
            PNLServerStatus.VerticalScrollbarSize = 10;
            // 
            // LBLWorldServerStatus
            // 
            LBLWorldServerStatus.AutoSize = true;
            LBLWorldServerStatus.Font = new Font("Segoe UI", 12F);
            LBLWorldServerStatus.ForeColor = Color.White;
            LBLWorldServerStatus.Location = new Point(73, 115);
            LBLWorldServerStatus.Name = "LBLWorldServerStatus";
            LBLWorldServerStatus.Size = new Size(101, 21);
            LBLWorldServerStatus.TabIndex = 33;
            LBLWorldServerStatus.Text = "World Server";
            // 
            // LBLLogonServerStatus
            // 
            LBLLogonServerStatus.AutoSize = true;
            LBLLogonServerStatus.Font = new Font("Segoe UI", 12F);
            LBLLogonServerStatus.ForeColor = Color.White;
            LBLLogonServerStatus.Location = new Point(73, 74);
            LBLLogonServerStatus.Name = "LBLLogonServerStatus";
            LBLLogonServerStatus.Size = new Size(103, 21);
            LBLLogonServerStatus.TabIndex = 32;
            LBLLogonServerStatus.Text = "Logon Server";
            // 
            // LBLMySQLServerStatus
            // 
            LBLMySQLServerStatus.AutoSize = true;
            LBLMySQLServerStatus.Font = new Font("Segoe UI", 12F);
            LBLMySQLServerStatus.ForeColor = Color.White;
            LBLMySQLServerStatus.Location = new Point(73, 33);
            LBLMySQLServerStatus.Name = "LBLMySQLServerStatus";
            LBLMySQLServerStatus.Size = new Size(110, 21);
            LBLMySQLServerStatus.TabIndex = 31;
            LBLMySQLServerStatus.Text = "MySQL Server";
            // 
            // PICMySqlServerStatus
            // 
            PICMySqlServerStatus.Image = (Image)resources.GetObject("PICMySqlServerStatus.Image");
            PICMySqlServerStatus.Location = new Point(32, 26);
            PICMySqlServerStatus.Name = "PICMySqlServerStatus";
            PICMySqlServerStatus.Size = new Size(35, 35);
            PICMySqlServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICMySqlServerStatus.TabIndex = 30;
            PICMySqlServerStatus.TabStop = false;
            // 
            // PICWorldServerStatus
            // 
            PICWorldServerStatus.Image = (Image)resources.GetObject("PICWorldServerStatus.Image");
            PICWorldServerStatus.Location = new Point(32, 108);
            PICWorldServerStatus.Name = "PICWorldServerStatus";
            PICWorldServerStatus.Size = new Size(35, 35);
            PICWorldServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICWorldServerStatus.TabIndex = 29;
            PICWorldServerStatus.TabStop = false;
            // 
            // PICLogonServerStatus
            // 
            PICLogonServerStatus.Image = (Image)resources.GetObject("PICLogonServerStatus.Image");
            PICLogonServerStatus.Location = new Point(32, 67);
            PICLogonServerStatus.Name = "PICLogonServerStatus";
            PICLogonServerStatus.Size = new Size(35, 35);
            PICLogonServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICLogonServerStatus.TabIndex = 28;
            PICLogonServerStatus.TabStop = false;
            // 
            // PNLPCResorce
            // 
            PNLPCResorce.BackColor = Color.FromArgb(28, 33, 40);
            PNLPCResorce.Border = true;
            PNLPCResorce.BorderColor = Color.Black;
            PNLPCResorce.BorderSize = 1;
            PNLPCResorce.Controls.Add(label1);
            PNLPCResorce.Controls.Add(PCResorcePbarCPU);
            PNLPCResorce.Controls.Add(PCResorcePbarRAM);
            PNLPCResorce.Controls.Add(label2);
            PNLPCResorce.Controls.Add(label3);
            PNLPCResorce.CustomBackground = true;
            PNLPCResorce.Dock = DockStyle.Fill;
            PNLPCResorce.HorizontalScrollbar = false;
            PNLPCResorce.HorizontalScrollbarBarColor = true;
            PNLPCResorce.HorizontalScrollbarHighlightOnWheel = false;
            PNLPCResorce.HorizontalScrollbarSize = 10;
            PNLPCResorce.Location = new Point(424, 4);
            PNLPCResorce.Margin = new Padding(2);
            PNLPCResorce.Name = "PNLPCResorce";
            PNLPCResorce.Padding = new Padding(2);
            PNLPCResorce.Size = new Size(417, 178);
            PNLPCResorce.Style = MetroFramework.MetroColorStyle.Blue;
            PNLPCResorce.StyleManager = null;
            PNLPCResorce.TabIndex = 1;
            PNLPCResorce.Theme = MetroFramework.MetroThemeStyle.Light;
            PNLPCResorce.VerticalScrollbar = false;
            PNLPCResorce.VerticalScrollbarBarColor = true;
            PNLPCResorce.VerticalScrollbarHighlightOnWheel = false;
            PNLPCResorce.VerticalScrollbarSize = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 174, 219);
            label1.Location = new Point(14, 18);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 27;
            label1.Text = "PC RESORCE";
            // 
            // PCResorcePbarCPU
            // 
            PCResorcePbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCResorcePbarCPU.BackColor = Color.FromArgb(34, 34, 34);
            PCResorcePbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarCPU.FontSize = 10;
            PCResorcePbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarCPU.LabelText = "%";
            PCResorcePbarCPU.Location = new Point(13, 140);
            PCResorcePbarCPU.MaximumValue = false;
            PCResorcePbarCPU.Name = "PCResorcePbarCPU";
            PCResorcePbarCPU.ShowStatus = true;
            PCResorcePbarCPU.Size = new Size(390, 20);
            PCResorcePbarCPU.TabIndex = 31;
            PCResorcePbarCPU.TextColor = Color.White;
            // 
            // PCResorcePbarRAM
            // 
            PCResorcePbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCResorcePbarRAM.BackColor = Color.FromArgb(34, 34, 34);
            PCResorcePbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarRAM.FontSize = 10;
            PCResorcePbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarRAM.LabelText = "MB";
            PCResorcePbarRAM.Location = new Point(13, 86);
            PCResorcePbarRAM.MaximumValue = true;
            PCResorcePbarRAM.Name = "PCResorcePbarRAM";
            PCResorcePbarRAM.ShowStatus = true;
            PCResorcePbarRAM.Size = new Size(390, 20);
            PCResorcePbarRAM.TabIndex = 28;
            PCResorcePbarRAM.TextColor = Color.White;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(128, 120);
            label2.Name = "label2";
            label2.Size = new Size(164, 15);
            label2.TabIndex = 30;
            label2.Text = "Central Processing Unit (CPU)";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(120, 66);
            label3.Name = "label3";
            label3.Size = new Size(181, 15);
            label3.TabIndex = 29;
            label3.Text = "Random-Access Memory  (RAM)";
            // 
            // TimerRam
            // 
            TimerRam.Interval = 1000;
            TimerRam.Tick += TimerRam_Tick;
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(PNLLayoutTop);
            Controls.Add(PNLLayoutBot);
            Name = "HomeControl";
            Size = new Size(845, 370);
            Load += HomeControl_Load;
            PNLLayoutBot.ResumeLayout(false);
            metroPanel1.ResumeLayout(false);
            metroPanel1.PerformLayout();
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            metroPanel3.ResumeLayout(false);
            PNLLayoutTop.ResumeLayout(false);
            PNLServerStatus.ResumeLayout(false);
            PNLServerStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)PICWorldServerStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)PICLogonServerStatus).EndInit();
            PNLPCResorce.ResumeLayout(false);
            PNLPCResorce.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer TimerWacher;
        private TableLayoutPanel PNLLayoutBot;
        private TableLayoutPanel PNLLayoutTop;
        private MetroPanel PNLServerStatus;
        private Label LBLWorldServerStatus;
        private Label LBLLogonServerStatus;
        private Label LBLMySQLServerStatus;
        private MetroPanel PNLPCResorce;
        private Label label1;
        private CustomProgressBar PCResorcePbarCPU;
        private CustomProgressBar PCResorcePbarRAM;
        private Label label2;
        private Label label3;
        private MetroPanel metroPanel1;
        private Label label4;
        private CustomProgressBar LoginPbarCPU;
        private CustomProgressBar LoginPbarRAM;
        private Label label5;
        private Label label6;
        private MetroPanel metroPanel2;
        private Label label7;
        private CustomProgressBar WorldPbarCPU;
        private Label label9;
        private CustomProgressBar WorldPbarRAM;
        private Label label8;
        private UI.Controls.CustomButton BTNMySQLOpenFolder;
        private UI.Controls.CustomButton customButton1;
        private MetroPanel metroPanel3;
        private Label LBLWorldsOpen;
        public PictureBox PICMySqlServerStatus;
        public PictureBox PICWorldServerStatus;
        public PictureBox PICLogonServerStatus;
        private System.Windows.Forms.Timer TimerRam;
    }
}
