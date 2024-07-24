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
            metroPanel7 = new MetroPanel();
            PNLLoginCount = new MetroPanel();
            LBLLogonOpen = new Label();
            label4 = new Label();
            BTNLoginFW = new UI.Controls.CustomButton();
            BTNLoginBC = new UI.Controls.CustomButton();
            LoginPbarCPU = new CustomProgressBar();
            LoginPbarRAM = new CustomProgressBar();
            label5 = new Label();
            label6 = new Label();
            metroPanel2 = new MetroPanel();
            metroPanel8 = new MetroPanel();
            label7 = new Label();
            PNLWorldCount = new MetroPanel();
            LBLWorldsOpen = new Label();
            BTNWorldFW = new UI.Controls.CustomButton();
            BTNWorldBC = new UI.Controls.CustomButton();
            WorldPbarCPU = new CustomProgressBar();
            label9 = new Label();
            WorldPbarRAM = new CustomProgressBar();
            label8 = new Label();
            PNLLayoutTop = new TableLayoutPanel();
            PNLServerStatus = new MetroPanel();
            metroPanel6 = new MetroPanel();
            LBLWordPort = new Label();
            LBLUpTimeWorld = new Label();
            PICWorldServerStatus = new PictureBox();
            LBLWorldServerStatus = new Label();
            metroPanel5 = new MetroPanel();
            LBLLogonPort = new Label();
            LBLUpTimeLogon = new Label();
            PICLogonServerStatus = new PictureBox();
            LBLLogonServerStatus = new Label();
            metroPanel4 = new MetroPanel();
            LBLMysqlPort = new Label();
            LBLUpTimeDatabase = new Label();
            PICMySqlServerStatus = new PictureBox();
            LBLMySQLServerStatus = new Label();
            PNLPCResorce = new MetroPanel();
            metroPanel20 = new MetroPanel();
            label1 = new Label();
            PCResorcePbarCPU = new CustomProgressBar();
            PCResorcePbarRAM = new CustomProgressBar();
            label2 = new Label();
            label3 = new Label();
            TimerRam = new System.Windows.Forms.Timer(components);
            TimerStopWatch = new System.Windows.Forms.Timer(components);
            PNLLayoutBot.SuspendLayout();
            metroPanel1.SuspendLayout();
            metroPanel7.SuspendLayout();
            PNLLoginCount.SuspendLayout();
            metroPanel2.SuspendLayout();
            metroPanel8.SuspendLayout();
            PNLWorldCount.SuspendLayout();
            PNLLayoutTop.SuspendLayout();
            PNLServerStatus.SuspendLayout();
            metroPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICWorldServerStatus).BeginInit();
            metroPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICLogonServerStatus).BeginInit();
            metroPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).BeginInit();
            PNLPCResorce.SuspendLayout();
            metroPanel20.SuspendLayout();
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
            metroPanel1.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(metroPanel7);
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
            // metroPanel7
            // 
            metroPanel7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel7.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel7.Border = true;
            metroPanel7.BorderColor = Color.Black;
            metroPanel7.BorderSize = 1;
            metroPanel7.Controls.Add(PNLLoginCount);
            metroPanel7.Controls.Add(label4);
            metroPanel7.Controls.Add(BTNLoginFW);
            metroPanel7.Controls.Add(BTNLoginBC);
            metroPanel7.CustomBackground = true;
            metroPanel7.HorizontalScrollbar = true;
            metroPanel7.HorizontalScrollbarBarColor = true;
            metroPanel7.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel7.HorizontalScrollbarSize = 10;
            metroPanel7.Location = new Point(0, 0);
            metroPanel7.Name = "metroPanel7";
            metroPanel7.Padding = new Padding(2);
            metroPanel7.Size = new Size(415, 30);
            metroPanel7.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel7.StyleManager = null;
            metroPanel7.TabIndex = 57;
            metroPanel7.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel7.VerticalScrollbar = true;
            metroPanel7.VerticalScrollbarBarColor = true;
            metroPanel7.VerticalScrollbarHighlightOnWheel = false;
            metroPanel7.VerticalScrollbarSize = 10;
            // 
            // PNLLoginCount
            // 
            PNLLoginCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PNLLoginCount.BackColor = Color.FromArgb(28, 33, 40);
            PNLLoginCount.Border = true;
            PNLLoginCount.BorderColor = Color.FromArgb(0, 174, 219);
            PNLLoginCount.BorderSize = 1;
            PNLLoginCount.Controls.Add(LBLLogonOpen);
            PNLLoginCount.CustomBackground = true;
            PNLLoginCount.HorizontalScrollbar = false;
            PNLLoginCount.HorizontalScrollbarBarColor = true;
            PNLLoginCount.HorizontalScrollbarHighlightOnWheel = false;
            PNLLoginCount.HorizontalScrollbarSize = 10;
            PNLLoginCount.Location = new Point(352, 3);
            PNLLoginCount.Name = "PNLLoginCount";
            PNLLoginCount.Padding = new Padding(2);
            PNLLoginCount.Size = new Size(36, 25);
            PNLLoginCount.Style = MetroFramework.MetroColorStyle.Blue;
            PNLLoginCount.StyleManager = null;
            PNLLoginCount.TabIndex = 60;
            PNLLoginCount.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLLoginCount.VerticalScrollbar = false;
            PNLLoginCount.VerticalScrollbarBarColor = true;
            PNLLoginCount.VerticalScrollbarHighlightOnWheel = false;
            PNLLoginCount.VerticalScrollbarSize = 10;
            // 
            // LBLLogonOpen
            // 
            LBLLogonOpen.Dock = DockStyle.Fill;
            LBLLogonOpen.ForeColor = Color.White;
            LBLLogonOpen.Location = new Point(2, 2);
            LBLLogonOpen.Name = "LBLLogonOpen";
            LBLLogonOpen.Size = new Size(32, 21);
            LBLLogonOpen.TabIndex = 19;
            LBLLogonOpen.Text = "0/0";
            LBLLogonOpen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Dock = DockStyle.Left;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(0, 174, 219);
            label4.Location = new Point(2, 2);
            label4.Name = "label4";
            label4.Size = new Size(215, 26);
            label4.TabIndex = 32;
            label4.Text = "LOGIN SERVER RESOURCE";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNLoginFW
            // 
            BTNLoginFW.Anchor = AnchorStyles.Right;
            BTNLoginFW.BackColor = Color.FromArgb(28, 33, 40);
            BTNLoginFW.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNLoginFW.BorderColor = Color.FromArgb(0, 174, 219);
            BTNLoginFW.BorderRadius = 0;
            BTNLoginFW.BorderSize = 1;
            BTNLoginFW.Cursor = Cursors.Hand;
            BTNLoginFW.FlatAppearance.BorderSize = 0;
            BTNLoginFW.FlatStyle = FlatStyle.Flat;
            BTNLoginFW.ForeColor = Color.White;
            BTNLoginFW.Image = (Image)resources.GetObject("BTNLoginFW.Image");
            BTNLoginFW.ImageAlign = ContentAlignment.MiddleLeft;
            BTNLoginFW.Location = new Point(387, 3);
            BTNLoginFW.Name = "BTNLoginFW";
            BTNLoginFW.NotificationCount = 0;
            BTNLoginFW.RightToLeft = RightToLeft.No;
            BTNLoginFW.Size = new Size(25, 25);
            BTNLoginFW.TabIndex = 59;
            BTNLoginFW.TextColor = Color.White;
            BTNLoginFW.UseVisualStyleBackColor = false;
            // 
            // BTNLoginBC
            // 
            BTNLoginBC.Anchor = AnchorStyles.Right;
            BTNLoginBC.BackColor = Color.FromArgb(28, 33, 40);
            BTNLoginBC.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNLoginBC.BorderColor = Color.FromArgb(0, 174, 219);
            BTNLoginBC.BorderRadius = 0;
            BTNLoginBC.BorderSize = 1;
            BTNLoginBC.Cursor = Cursors.Hand;
            BTNLoginBC.FlatAppearance.BorderSize = 0;
            BTNLoginBC.FlatStyle = FlatStyle.Flat;
            BTNLoginBC.ForeColor = Color.White;
            BTNLoginBC.Image = (Image)resources.GetObject("BTNLoginBC.Image");
            BTNLoginBC.ImageAlign = ContentAlignment.MiddleLeft;
            BTNLoginBC.Location = new Point(328, 3);
            BTNLoginBC.Name = "BTNLoginBC";
            BTNLoginBC.NotificationCount = 0;
            BTNLoginBC.RightToLeft = RightToLeft.No;
            BTNLoginBC.Size = new Size(25, 25);
            BTNLoginBC.TabIndex = 58;
            BTNLoginBC.TextColor = Color.White;
            BTNLoginBC.UseVisualStyleBackColor = false;
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
            metroPanel2.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(metroPanel8);
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
            // metroPanel8
            // 
            metroPanel8.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel8.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel8.Border = true;
            metroPanel8.BorderColor = Color.Black;
            metroPanel8.BorderSize = 1;
            metroPanel8.Controls.Add(label7);
            metroPanel8.Controls.Add(PNLWorldCount);
            metroPanel8.Controls.Add(BTNWorldFW);
            metroPanel8.Controls.Add(BTNWorldBC);
            metroPanel8.CustomBackground = true;
            metroPanel8.HorizontalScrollbar = true;
            metroPanel8.HorizontalScrollbarBarColor = true;
            metroPanel8.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel8.HorizontalScrollbarSize = 10;
            metroPanel8.Location = new Point(0, 0);
            metroPanel8.Name = "metroPanel8";
            metroPanel8.Padding = new Padding(2);
            metroPanel8.Size = new Size(414, 30);
            metroPanel8.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel8.StyleManager = null;
            metroPanel8.TabIndex = 58;
            metroPanel8.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel8.VerticalScrollbar = true;
            metroPanel8.VerticalScrollbarBarColor = true;
            metroPanel8.VerticalScrollbarHighlightOnWheel = false;
            metroPanel8.VerticalScrollbarSize = 10;
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Dock = DockStyle.Left;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(0, 174, 219);
            label7.Location = new Point(2, 2);
            label7.Name = "label7";
            label7.Size = new Size(215, 26);
            label7.TabIndex = 37;
            label7.Text = "WORLD SERVER RESOURCE";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PNLWorldCount
            // 
            PNLWorldCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PNLWorldCount.BackColor = Color.FromArgb(28, 33, 40);
            PNLWorldCount.Border = true;
            PNLWorldCount.BorderColor = Color.FromArgb(0, 174, 219);
            PNLWorldCount.BorderSize = 1;
            PNLWorldCount.Controls.Add(LBLWorldsOpen);
            PNLWorldCount.CustomBackground = true;
            PNLWorldCount.HorizontalScrollbar = false;
            PNLWorldCount.HorizontalScrollbarBarColor = true;
            PNLWorldCount.HorizontalScrollbarHighlightOnWheel = false;
            PNLWorldCount.HorizontalScrollbarSize = 10;
            PNLWorldCount.Location = new Point(351, 3);
            PNLWorldCount.Name = "PNLWorldCount";
            PNLWorldCount.Padding = new Padding(2);
            PNLWorldCount.Size = new Size(36, 25);
            PNLWorldCount.Style = MetroFramework.MetroColorStyle.Blue;
            PNLWorldCount.StyleManager = null;
            PNLWorldCount.TabIndex = 44;
            PNLWorldCount.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLWorldCount.VerticalScrollbar = false;
            PNLWorldCount.VerticalScrollbarBarColor = true;
            PNLWorldCount.VerticalScrollbarHighlightOnWheel = false;
            PNLWorldCount.VerticalScrollbarSize = 10;
            // 
            // LBLWorldsOpen
            // 
            LBLWorldsOpen.Dock = DockStyle.Fill;
            LBLWorldsOpen.ForeColor = Color.White;
            LBLWorldsOpen.Location = new Point(2, 2);
            LBLWorldsOpen.Name = "LBLWorldsOpen";
            LBLWorldsOpen.Size = new Size(32, 21);
            LBLWorldsOpen.TabIndex = 19;
            LBLWorldsOpen.Text = "0/0";
            LBLWorldsOpen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNWorldFW
            // 
            BTNWorldFW.Anchor = AnchorStyles.Right;
            BTNWorldFW.BackColor = Color.FromArgb(28, 33, 40);
            BTNWorldFW.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNWorldFW.BorderColor = Color.FromArgb(0, 174, 219);
            BTNWorldFW.BorderRadius = 0;
            BTNWorldFW.BorderSize = 1;
            BTNWorldFW.Cursor = Cursors.Hand;
            BTNWorldFW.FlatAppearance.BorderSize = 0;
            BTNWorldFW.FlatStyle = FlatStyle.Flat;
            BTNWorldFW.ForeColor = Color.White;
            BTNWorldFW.Image = (Image)resources.GetObject("BTNWorldFW.Image");
            BTNWorldFW.ImageAlign = ContentAlignment.MiddleLeft;
            BTNWorldFW.Location = new Point(386, 3);
            BTNWorldFW.Name = "BTNWorldFW";
            BTNWorldFW.NotificationCount = 0;
            BTNWorldFW.RightToLeft = RightToLeft.No;
            BTNWorldFW.Size = new Size(25, 25);
            BTNWorldFW.TabIndex = 43;
            BTNWorldFW.TextColor = Color.White;
            BTNWorldFW.UseVisualStyleBackColor = false;
            BTNWorldFW.Click += BTNWorldFW_Click;
            // 
            // BTNWorldBC
            // 
            BTNWorldBC.Anchor = AnchorStyles.Right;
            BTNWorldBC.BackColor = Color.FromArgb(28, 33, 40);
            BTNWorldBC.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNWorldBC.BorderColor = Color.FromArgb(0, 174, 219);
            BTNWorldBC.BorderRadius = 0;
            BTNWorldBC.BorderSize = 1;
            BTNWorldBC.Cursor = Cursors.Hand;
            BTNWorldBC.FlatAppearance.BorderSize = 0;
            BTNWorldBC.FlatStyle = FlatStyle.Flat;
            BTNWorldBC.ForeColor = Color.White;
            BTNWorldBC.Image = (Image)resources.GetObject("BTNWorldBC.Image");
            BTNWorldBC.ImageAlign = ContentAlignment.MiddleLeft;
            BTNWorldBC.Location = new Point(327, 3);
            BTNWorldBC.Name = "BTNWorldBC";
            BTNWorldBC.NotificationCount = 0;
            BTNWorldBC.RightToLeft = RightToLeft.No;
            BTNWorldBC.Size = new Size(25, 25);
            BTNWorldBC.TabIndex = 42;
            BTNWorldBC.TextColor = Color.White;
            BTNWorldBC.UseVisualStyleBackColor = false;
            BTNWorldBC.Click += BTNWorldBC_Click;
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
            PNLServerStatus.BackColor = Color.FromArgb(34, 39, 46);
            PNLServerStatus.Border = true;
            PNLServerStatus.BorderColor = Color.Black;
            PNLServerStatus.BorderSize = 1;
            PNLServerStatus.Controls.Add(metroPanel6);
            PNLServerStatus.Controls.Add(metroPanel5);
            PNLServerStatus.Controls.Add(metroPanel4);
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
            // metroPanel6
            // 
            metroPanel6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel6.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel6.Border = true;
            metroPanel6.BorderColor = Color.Black;
            metroPanel6.BorderSize = 1;
            metroPanel6.Controls.Add(LBLWordPort);
            metroPanel6.Controls.Add(LBLUpTimeWorld);
            metroPanel6.Controls.Add(PICWorldServerStatus);
            metroPanel6.Controls.Add(LBLWorldServerStatus);
            metroPanel6.CustomBackground = true;
            metroPanel6.HorizontalScrollbar = true;
            metroPanel6.HorizontalScrollbarBarColor = true;
            metroPanel6.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel6.HorizontalScrollbarSize = 10;
            metroPanel6.Location = new Point(7, 118);
            metroPanel6.Name = "metroPanel6";
            metroPanel6.Padding = new Padding(2);
            metroPanel6.Size = new Size(400, 50);
            metroPanel6.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel6.StyleManager = null;
            metroPanel6.TabIndex = 36;
            metroPanel6.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel6.VerticalScrollbar = true;
            metroPanel6.VerticalScrollbarBarColor = true;
            metroPanel6.VerticalScrollbarHighlightOnWheel = false;
            metroPanel6.VerticalScrollbarSize = 10;
            // 
            // LBLWordPort
            // 
            LBLWordPort.Anchor = AnchorStyles.Top;
            LBLWordPort.AutoSize = true;
            LBLWordPort.ForeColor = Color.White;
            LBLWordPort.Location = new Point(163, 27);
            LBLWordPort.Name = "LBLWordPort";
            LBLWordPort.Size = new Size(61, 15);
            LBLWordPort.TabIndex = 43;
            LBLWordPort.Text = "ProcessID:";
            // 
            // LBLUpTimeWorld
            // 
            LBLUpTimeWorld.Anchor = AnchorStyles.Top;
            LBLUpTimeWorld.AutoSize = true;
            LBLUpTimeWorld.ForeColor = Color.White;
            LBLUpTimeWorld.Location = new Point(163, 7);
            LBLUpTimeWorld.Name = "LBLUpTimeWorld";
            LBLUpTimeWorld.Size = new Size(54, 15);
            LBLUpTimeWorld.TabIndex = 42;
            LBLUpTimeWorld.Text = "Up Time:";
            // 
            // PICWorldServerStatus
            // 
            PICWorldServerStatus.Anchor = AnchorStyles.Left;
            PICWorldServerStatus.Image = (Image)resources.GetObject("PICWorldServerStatus.Image");
            PICWorldServerStatus.Location = new Point(7, 7);
            PICWorldServerStatus.Name = "PICWorldServerStatus";
            PICWorldServerStatus.Size = new Size(35, 35);
            PICWorldServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICWorldServerStatus.TabIndex = 29;
            PICWorldServerStatus.TabStop = false;
            // 
            // LBLWorldServerStatus
            // 
            LBLWorldServerStatus.AutoSize = true;
            LBLWorldServerStatus.Font = new Font("Segoe UI", 12F);
            LBLWorldServerStatus.ForeColor = Color.White;
            LBLWorldServerStatus.Location = new Point(50, 15);
            LBLWorldServerStatus.Name = "LBLWorldServerStatus";
            LBLWorldServerStatus.Size = new Size(56, 21);
            LBLWorldServerStatus.TabIndex = 33;
            LBLWorldServerStatus.Text = "World ";
            // 
            // metroPanel5
            // 
            metroPanel5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel5.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel5.Border = true;
            metroPanel5.BorderColor = Color.Black;
            metroPanel5.BorderSize = 1;
            metroPanel5.Controls.Add(LBLLogonPort);
            metroPanel5.Controls.Add(LBLUpTimeLogon);
            metroPanel5.Controls.Add(PICLogonServerStatus);
            metroPanel5.Controls.Add(LBLLogonServerStatus);
            metroPanel5.CustomBackground = true;
            metroPanel5.HorizontalScrollbar = true;
            metroPanel5.HorizontalScrollbarBarColor = true;
            metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel5.HorizontalScrollbarSize = 10;
            metroPanel5.Location = new Point(7, 63);
            metroPanel5.Name = "metroPanel5";
            metroPanel5.Padding = new Padding(2);
            metroPanel5.Size = new Size(400, 50);
            metroPanel5.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel5.StyleManager = null;
            metroPanel5.TabIndex = 35;
            metroPanel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel5.VerticalScrollbar = true;
            metroPanel5.VerticalScrollbarBarColor = true;
            metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            metroPanel5.VerticalScrollbarSize = 10;
            // 
            // LBLLogonPort
            // 
            LBLLogonPort.Anchor = AnchorStyles.Top;
            LBLLogonPort.AutoSize = true;
            LBLLogonPort.ForeColor = Color.White;
            LBLLogonPort.Location = new Point(163, 27);
            LBLLogonPort.Name = "LBLLogonPort";
            LBLLogonPort.Size = new Size(61, 15);
            LBLLogonPort.TabIndex = 43;
            LBLLogonPort.Text = "ProcessID:";
            // 
            // LBLUpTimeLogon
            // 
            LBLUpTimeLogon.Anchor = AnchorStyles.Top;
            LBLUpTimeLogon.AutoSize = true;
            LBLUpTimeLogon.ForeColor = Color.White;
            LBLUpTimeLogon.Location = new Point(163, 7);
            LBLUpTimeLogon.Name = "LBLUpTimeLogon";
            LBLUpTimeLogon.Size = new Size(54, 15);
            LBLUpTimeLogon.TabIndex = 42;
            LBLUpTimeLogon.Text = "Up Time:";
            // 
            // PICLogonServerStatus
            // 
            PICLogonServerStatus.Anchor = AnchorStyles.Left;
            PICLogonServerStatus.Image = (Image)resources.GetObject("PICLogonServerStatus.Image");
            PICLogonServerStatus.Location = new Point(7, 7);
            PICLogonServerStatus.Name = "PICLogonServerStatus";
            PICLogonServerStatus.Size = new Size(35, 35);
            PICLogonServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICLogonServerStatus.TabIndex = 28;
            PICLogonServerStatus.TabStop = false;
            // 
            // LBLLogonServerStatus
            // 
            LBLLogonServerStatus.AutoSize = true;
            LBLLogonServerStatus.Font = new Font("Segoe UI", 12F);
            LBLLogonServerStatus.ForeColor = Color.White;
            LBLLogonServerStatus.Location = new Point(50, 15);
            LBLLogonServerStatus.Name = "LBLLogonServerStatus";
            LBLLogonServerStatus.Size = new Size(54, 21);
            LBLLogonServerStatus.TabIndex = 32;
            LBLLogonServerStatus.Text = "Logon";
            // 
            // metroPanel4
            // 
            metroPanel4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel4.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel4.Border = true;
            metroPanel4.BorderColor = Color.Black;
            metroPanel4.BorderSize = 1;
            metroPanel4.Controls.Add(LBLMysqlPort);
            metroPanel4.Controls.Add(LBLUpTimeDatabase);
            metroPanel4.Controls.Add(PICMySqlServerStatus);
            metroPanel4.Controls.Add(LBLMySQLServerStatus);
            metroPanel4.CustomBackground = true;
            metroPanel4.HorizontalScrollbar = true;
            metroPanel4.HorizontalScrollbarBarColor = true;
            metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel4.HorizontalScrollbarSize = 10;
            metroPanel4.Location = new Point(7, 8);
            metroPanel4.Name = "metroPanel4";
            metroPanel4.Padding = new Padding(2);
            metroPanel4.Size = new Size(400, 50);
            metroPanel4.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel4.StyleManager = null;
            metroPanel4.TabIndex = 34;
            metroPanel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel4.VerticalScrollbar = true;
            metroPanel4.VerticalScrollbarBarColor = true;
            metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            metroPanel4.VerticalScrollbarSize = 10;
            // 
            // LBLMysqlPort
            // 
            LBLMysqlPort.Anchor = AnchorStyles.Top;
            LBLMysqlPort.AutoSize = true;
            LBLMysqlPort.ForeColor = Color.White;
            LBLMysqlPort.Location = new Point(163, 27);
            LBLMysqlPort.Name = "LBLMysqlPort";
            LBLMysqlPort.Size = new Size(61, 15);
            LBLMysqlPort.TabIndex = 41;
            LBLMysqlPort.Text = "ProcessID:";
            // 
            // LBLUpTimeDatabase
            // 
            LBLUpTimeDatabase.Anchor = AnchorStyles.Top;
            LBLUpTimeDatabase.AutoSize = true;
            LBLUpTimeDatabase.ForeColor = Color.White;
            LBLUpTimeDatabase.Location = new Point(163, 7);
            LBLUpTimeDatabase.Name = "LBLUpTimeDatabase";
            LBLUpTimeDatabase.Size = new Size(54, 15);
            LBLUpTimeDatabase.TabIndex = 40;
            LBLUpTimeDatabase.Text = "Up Time:";
            // 
            // PICMySqlServerStatus
            // 
            PICMySqlServerStatus.Anchor = AnchorStyles.Left;
            PICMySqlServerStatus.Image = (Image)resources.GetObject("PICMySqlServerStatus.Image");
            PICMySqlServerStatus.Location = new Point(7, 7);
            PICMySqlServerStatus.Name = "PICMySqlServerStatus";
            PICMySqlServerStatus.Size = new Size(35, 35);
            PICMySqlServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICMySqlServerStatus.TabIndex = 30;
            PICMySqlServerStatus.TabStop = false;
            // 
            // LBLMySQLServerStatus
            // 
            LBLMySQLServerStatus.AutoSize = true;
            LBLMySQLServerStatus.Font = new Font("Segoe UI", 12F);
            LBLMySQLServerStatus.ForeColor = Color.White;
            LBLMySQLServerStatus.Location = new Point(50, 15);
            LBLMySQLServerStatus.Name = "LBLMySQLServerStatus";
            LBLMySQLServerStatus.Size = new Size(61, 21);
            LBLMySQLServerStatus.TabIndex = 31;
            LBLMySQLServerStatus.Text = "MySQL";
            // 
            // PNLPCResorce
            // 
            PNLPCResorce.BackColor = Color.FromArgb(34, 39, 46);
            PNLPCResorce.Border = true;
            PNLPCResorce.BorderColor = Color.Black;
            PNLPCResorce.BorderSize = 1;
            PNLPCResorce.Controls.Add(metroPanel20);
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
            // metroPanel20
            // 
            metroPanel20.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel20.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel20.Border = true;
            metroPanel20.BorderColor = Color.Black;
            metroPanel20.BorderSize = 1;
            metroPanel20.Controls.Add(label1);
            metroPanel20.CustomBackground = true;
            metroPanel20.HorizontalScrollbar = true;
            metroPanel20.HorizontalScrollbarBarColor = true;
            metroPanel20.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel20.HorizontalScrollbarSize = 10;
            metroPanel20.Location = new Point(0, 0);
            metroPanel20.Name = "metroPanel20";
            metroPanel20.Padding = new Padding(2);
            metroPanel20.Size = new Size(417, 30);
            metroPanel20.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel20.StyleManager = null;
            metroPanel20.TabIndex = 56;
            metroPanel20.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel20.VerticalScrollbar = true;
            metroPanel20.VerticalScrollbarBarColor = true;
            metroPanel20.VerticalScrollbarHighlightOnWheel = false;
            metroPanel20.VerticalScrollbarSize = 10;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 174, 219);
            label1.Location = new Point(2, 2);
            label1.Name = "label1";
            label1.Size = new Size(413, 26);
            label1.TabIndex = 27;
            label1.Text = "PC RESOURCE";
            label1.TextAlign = ContentAlignment.MiddleCenter;
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
            // TimerStopWatch
            // 
            TimerStopWatch.Enabled = true;
            TimerStopWatch.Interval = 1000;
            TimerStopWatch.Tick += TimerStopWatch_Tick;
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
            metroPanel7.ResumeLayout(false);
            PNLLoginCount.ResumeLayout(false);
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            metroPanel8.ResumeLayout(false);
            PNLWorldCount.ResumeLayout(false);
            PNLLayoutTop.ResumeLayout(false);
            PNLServerStatus.ResumeLayout(false);
            metroPanel6.ResumeLayout(false);
            metroPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PICWorldServerStatus).EndInit();
            metroPanel5.ResumeLayout(false);
            metroPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PICLogonServerStatus).EndInit();
            metroPanel4.ResumeLayout(false);
            metroPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).EndInit();
            PNLPCResorce.ResumeLayout(false);
            PNLPCResorce.PerformLayout();
            metroPanel20.ResumeLayout(false);
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
        private UI.Controls.CustomButton BTNWorldBC;
        private UI.Controls.CustomButton BTNWorldFW;
        private MetroPanel PNLWorldCount;
        private Label LBLWorldsOpen;
        public PictureBox PICMySqlServerStatus;
        public PictureBox PICWorldServerStatus;
        public PictureBox PICLogonServerStatus;
        private System.Windows.Forms.Timer TimerRam;
        private MetroPanel metroPanel4;
        private MetroPanel metroPanel6;
        private MetroPanel metroPanel5;
        private Label LBLUpTimeDatabase;
        private Label LBLWordPort;
        private Label LBLUpTimeWorld;
        private Label LBLLogonPort;
        private Label LBLUpTimeLogon;
        private Label LBLMysqlPort;
        private System.Windows.Forms.Timer TimerStopWatch;
        private MetroPanel metroPanel20;
        private MetroPanel metroPanel7;
        private MetroPanel metroPanel8;
        private MetroPanel PNLLoginCount;
        private Label LBLLogonOpen;
        private UI.Controls.CustomButton BTNLoginFW;
        private UI.Controls.CustomButton BTNLoginBC;
    }
}
