
using TrionControlPanelDesktop.Extensions.Modules;

namespace TrionControlPanelDesktop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TimerWacher = new System.Windows.Forms.Timer(components);
            NIcon = new NotifyIcon(components);
            CMSNotify = new ContextMenuStrip(components);
            OpenTSMItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            StartLogonTSMItem = new ToolStripMenuItem();
            StartWorldTSMItem = new ToolStripMenuItem();
            StartDatabaseTSMItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            ExitTSMItem = new ToolStripMenuItem();
            TimerLoadingCheck = new System.Windows.Forms.Timer(components);
            TLTHome = new TrionUI.Controls.CustomToolTip();
            BTNStartDatabase = new MaterialSkin.Controls.MaterialFloatingActionButton();
            BTNStartWorld = new MaterialSkin.Controls.MaterialFloatingActionButton();
            BTNStartLogon = new MaterialSkin.Controls.MaterialFloatingActionButton();
            TimerCrashDetected = new System.Windows.Forms.Timer(components);
            LayoutPanelMain = new TableLayoutPanel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            MainFormTabControler = new MaterialSkin.Controls.MaterialTabControl();
            TabHome = new TabPage();
            LayoutPanelHome = new TableLayoutPanel();
            CardLogonResorce = new MaterialSkin.Controls.MaterialCard();
            pictureBox3 = new PictureBox();
            materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar3 = new MaterialSkin.Controls.MaterialProgressBar();
            materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar4 = new MaterialSkin.Controls.MaterialProgressBar();
            CardWorldResources = new MaterialSkin.Controls.MaterialCard();
            pictureBox2 = new PictureBox();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar1 = new MaterialSkin.Controls.MaterialProgressBar();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar2 = new MaterialSkin.Controls.MaterialProgressBar();
            CardServerStatus = new MaterialSkin.Controls.MaterialCard();
            materialDrawer1 = new MaterialSkin.Controls.MaterialDrawer();
            CardMachineResoruces = new MaterialSkin.Controls.MaterialCard();
            pictureBox1 = new PictureBox();
            LBLCardMachineResourcesTitle = new MaterialSkin.Controls.MaterialLabel();
            LBLCPUTextMachineResources = new MaterialSkin.Controls.MaterialLabel();
            PbarRAMMachineResources = new MaterialSkin.Controls.MaterialProgressBar();
            LBLRAMTextMachineResources = new MaterialSkin.Controls.MaterialLabel();
            PbarCPUMachineResources = new MaterialSkin.Controls.MaterialProgressBar();
            TabDatabaseEditor = new TabPage();
            TabSettings = new TabPage();
            IMGListTabControler = new ImageList(components);
            LayerPNLCardServerREsorce = new TableLayoutPanel();
            PNLDatanasServerResorce = new MetroFramework.Controls.MetroPanel();
            metroPanel1 = new MetroFramework.Controls.MetroPanel();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            CMSNotify.SuspendLayout();
            LayoutPanelMain.SuspendLayout();
            materialCard1.SuspendLayout();
            MainFormTabControler.SuspendLayout();
            TabHome.SuspendLayout();
            LayoutPanelHome.SuspendLayout();
            CardLogonResorce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            CardWorldResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            CardServerStatus.SuspendLayout();
            CardMachineResoruces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            LayerPNLCardServerREsorce.SuspendLayout();
            SuspendLayout();
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // NIcon
            // 
            NIcon.BalloonTipIcon = ToolTipIcon.Info;
            NIcon.BalloonTipText = "Control Panel for World of Warcraft Emulators!";
            NIcon.BalloonTipTitle = "Trion Control Panel";
            NIcon.ContextMenuStrip = CMSNotify;
            NIcon.Icon = (Icon)resources.GetObject("NIcon.Icon");
            NIcon.Text = "Trion Control Panel";
            // 
            // CMSNotify
            // 
            CMSNotify.BackColor = Color.FromArgb(28, 33, 40);
            CMSNotify.ImageScalingSize = new Size(20, 20);
            CMSNotify.Items.AddRange(new ToolStripItem[] { OpenTSMItem, toolStripSeparator1, StartLogonTSMItem, StartWorldTSMItem, StartDatabaseTSMItem, toolStripSeparator2, ExitTSMItem });
            CMSNotify.Name = "contextMenuStrip1";
            CMSNotify.RenderMode = ToolStripRenderMode.System;
            CMSNotify.Size = new Size(138, 196);
            // 
            // OpenTSMItem
            // 
            OpenTSMItem.BackgroundImageLayout = ImageLayout.Stretch;
            OpenTSMItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            OpenTSMItem.ForeColor = Color.White;
            OpenTSMItem.ImageAlign = ContentAlignment.MiddleLeft;
            OpenTSMItem.Name = "OpenTSMItem";
            OpenTSMItem.Size = new Size(137, 36);
            OpenTSMItem.Text = "Open";
            OpenTSMItem.Click += OpenTSMItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(134, 6);
            // 
            // StartLogonTSMItem
            // 
            StartLogonTSMItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StartLogonTSMItem.ForeColor = Color.White;
            StartLogonTSMItem.Image = (Image)resources.GetObject("StartLogonTSMItem.Image");
            StartLogonTSMItem.ImageScaling = ToolStripItemImageScaling.None;
            StartLogonTSMItem.Name = "StartLogonTSMItem";
            StartLogonTSMItem.Size = new Size(137, 36);
            StartLogonTSMItem.Text = "Logon";
            StartLogonTSMItem.Click += StartLogonTSMItem_Click;
            // 
            // StartWorldTSMItem
            // 
            StartWorldTSMItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StartWorldTSMItem.ForeColor = Color.White;
            StartWorldTSMItem.Image = Properties.Resources.power_on_30;
            StartWorldTSMItem.ImageScaling = ToolStripItemImageScaling.None;
            StartWorldTSMItem.Name = "StartWorldTSMItem";
            StartWorldTSMItem.Size = new Size(137, 36);
            StartWorldTSMItem.Text = "World";
            StartWorldTSMItem.Click += StartWorldTSMItem_Click;
            // 
            // StartDatabaseTSMItem
            // 
            StartDatabaseTSMItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StartDatabaseTSMItem.ForeColor = Color.White;
            StartDatabaseTSMItem.Image = (Image)resources.GetObject("StartDatabaseTSMItem.Image");
            StartDatabaseTSMItem.ImageScaling = ToolStripItemImageScaling.None;
            StartDatabaseTSMItem.Name = "StartDatabaseTSMItem";
            StartDatabaseTSMItem.Size = new Size(137, 36);
            StartDatabaseTSMItem.Text = "Database";
            StartDatabaseTSMItem.Click += StartDatabaseTSMItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(134, 6);
            // 
            // ExitTSMItem
            // 
            ExitTSMItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExitTSMItem.ForeColor = Color.White;
            ExitTSMItem.Name = "ExitTSMItem";
            ExitTSMItem.Size = new Size(137, 36);
            ExitTSMItem.Text = "Exit";
            ExitTSMItem.Click += ExitTSMItem_ClickAsync;
            // 
            // TimerLoadingCheck
            // 
            TimerLoadingCheck.Enabled = true;
            TimerLoadingCheck.Interval = 15000;
            TimerLoadingCheck.Tick += TimerLoadingCheck_Tick;
            // 
            // TLTHome
            // 
            TLTHome.BackColor = Color.White;
            TLTHome.BackgroundColor = Color.FromArgb(34, 39, 46);
            TLTHome.BorderColor = Color.FromArgb(0, 174, 219);
            TLTHome.BorderSize = 1;
            TLTHome.ForeColor = Color.WhiteSmoke;
            TLTHome.LinkColor = Color.DodgerBlue;
            TLTHome.LinkEnabled = false;
            TLTHome.LinkText = "";
            TLTHome.OwnerDraw = true;
            TLTHome.StripAmpersands = true;
            TLTHome.TextColor = Color.White;
            TLTHome.TextFont = new Font("Segoe UI Semibold", 10F);
            TLTHome.TitleBackgroundColor = Color.FromArgb(28, 33, 40);
            TLTHome.TitleColor = Color.FromArgb(0, 174, 219);
            TLTHome.ToolTipIcon = ToolTipIcon.Info;
            TLTHome.ToolTipTitle = "Information!";
            // 
            // BTNStartDatabase
            // 
            BTNStartDatabase.Anchor = AnchorStyles.Top;
            BTNStartDatabase.AutoSize = true;
            BTNStartDatabase.Cursor = Cursors.Hand;
            BTNStartDatabase.Depth = 0;
            BTNStartDatabase.Icon = Properties.Resources.cloud_online_50;
            BTNStartDatabase.Location = new Point(423, 9);
            BTNStartDatabase.MouseState = MaterialSkin.MouseState.HOVER;
            BTNStartDatabase.Name = "BTNStartDatabase";
            BTNStartDatabase.Size = new Size(46, 46);
            BTNStartDatabase.TabIndex = 3;
            TLTHome.SetToolTip(BTNStartDatabase, "Start Database Server ");
            BTNStartDatabase.UseVisualStyleBackColor = true;
            // 
            // BTNStartWorld
            // 
            BTNStartWorld.Anchor = AnchorStyles.Top;
            BTNStartWorld.AutoSize = true;
            BTNStartWorld.Cursor = Cursors.Hand;
            BTNStartWorld.Depth = 0;
            BTNStartWorld.Icon = Properties.Resources.cloud_online_50;
            BTNStartWorld.Location = new Point(475, 9);
            BTNStartWorld.MouseState = MaterialSkin.MouseState.HOVER;
            BTNStartWorld.Name = "BTNStartWorld";
            BTNStartWorld.Size = new Size(46, 46);
            BTNStartWorld.TabIndex = 5;
            BTNStartWorld.Text = "g";
            BTNStartWorld.UseVisualStyleBackColor = true;
            // 
            // BTNStartLogon
            // 
            BTNStartLogon.Anchor = AnchorStyles.Top;
            BTNStartLogon.AutoSize = true;
            BTNStartLogon.Cursor = Cursors.Hand;
            BTNStartLogon.Depth = 0;
            BTNStartLogon.Icon = Properties.Resources.cloud_online_50;
            BTNStartLogon.Location = new Point(527, 9);
            BTNStartLogon.MouseState = MaterialSkin.MouseState.HOVER;
            BTNStartLogon.Name = "BTNStartLogon";
            BTNStartLogon.Size = new Size(46, 46);
            BTNStartLogon.TabIndex = 4;
            BTNStartLogon.UseVisualStyleBackColor = true;
            // 
            // TimerCrashDetected
            // 
            TimerCrashDetected.Enabled = true;
            TimerCrashDetected.Interval = 5000;
            TimerCrashDetected.Tick += TimerCrashDetected_Tick;
            // 
            // LayoutPanelMain
            // 
            LayoutPanelMain.ColumnCount = 1;
            LayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            LayoutPanelMain.Controls.Add(materialCard1, 0, 1);
            LayoutPanelMain.Controls.Add(MainFormTabControler, 0, 0);
            LayoutPanelMain.Dock = DockStyle.Fill;
            LayoutPanelMain.Location = new Point(0, 72);
            LayoutPanelMain.Name = "LayoutPanelMain";
            LayoutPanelMain.RowCount = 2;
            LayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            LayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            LayoutPanelMain.Size = new Size(997, 525);
            LayoutPanelMain.TabIndex = 9;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(BTNStartWorld);
            materialCard1.Controls.Add(BTNStartLogon);
            materialCard1.Controls.Add(BTNStartDatabase);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Fill;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(10, 449);
            materialCard1.Margin = new Padding(10, 4, 10, 10);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(4);
            materialCard1.Size = new Size(977, 66);
            materialCard1.TabIndex = 0;
            // 
            // MainFormTabControler
            // 
            MainFormTabControler.Alignment = TabAlignment.Left;
            MainFormTabControler.Controls.Add(TabHome);
            MainFormTabControler.Controls.Add(TabDatabaseEditor);
            MainFormTabControler.Controls.Add(TabSettings);
            MainFormTabControler.Depth = 0;
            MainFormTabControler.Dock = DockStyle.Fill;
            MainFormTabControler.ImageList = IMGListTabControler;
            MainFormTabControler.Location = new Point(3, 3);
            MainFormTabControler.MouseState = MaterialSkin.MouseState.HOVER;
            MainFormTabControler.Multiline = true;
            MainFormTabControler.Name = "MainFormTabControler";
            MainFormTabControler.SelectedIndex = 0;
            MainFormTabControler.Size = new Size(991, 439);
            MainFormTabControler.TabIndex = 1;
            // 
            // TabHome
            // 
            TabHome.BackColor = Color.White;
            TabHome.Controls.Add(LayoutPanelHome);
            TabHome.ImageKey = "menu-35.png";
            TabHome.Location = new Point(39, 4);
            TabHome.Name = "TabHome";
            TabHome.Padding = new Padding(3);
            TabHome.Size = new Size(948, 431);
            TabHome.TabIndex = 0;
            TabHome.Text = "Home";
            // 
            // LayoutPanelHome
            // 
            LayoutPanelHome.ColumnCount = 2;
            LayoutPanelHome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LayoutPanelHome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LayoutPanelHome.Controls.Add(CardLogonResorce, 1, 1);
            LayoutPanelHome.Controls.Add(CardWorldResources, 0, 1);
            LayoutPanelHome.Controls.Add(CardServerStatus, 0, 0);
            LayoutPanelHome.Controls.Add(CardMachineResoruces, 1, 0);
            LayoutPanelHome.Dock = DockStyle.Fill;
            LayoutPanelHome.ForeColor = SystemColors.Control;
            LayoutPanelHome.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            LayoutPanelHome.Location = new Point(3, 3);
            LayoutPanelHome.Name = "LayoutPanelHome";
            LayoutPanelHome.RowCount = 2;
            LayoutPanelHome.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            LayoutPanelHome.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            LayoutPanelHome.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            LayoutPanelHome.Size = new Size(942, 425);
            LayoutPanelHome.TabIndex = 0;
            // 
            // CardLogonResorce
            // 
            CardLogonResorce.BackColor = Color.FromArgb(255, 255, 255);
            CardLogonResorce.Controls.Add(pictureBox3);
            CardLogonResorce.Controls.Add(materialLabel4);
            CardLogonResorce.Controls.Add(materialLabel5);
            CardLogonResorce.Controls.Add(materialProgressBar3);
            CardLogonResorce.Controls.Add(materialLabel6);
            CardLogonResorce.Controls.Add(materialProgressBar4);
            CardLogonResorce.Depth = 0;
            CardLogonResorce.Dock = DockStyle.Fill;
            CardLogonResorce.ForeColor = Color.FromArgb(222, 0, 0, 0);
            CardLogonResorce.Location = new Point(475, 216);
            CardLogonResorce.Margin = new Padding(4);
            CardLogonResorce.MouseState = MaterialSkin.MouseState.HOVER;
            CardLogonResorce.Name = "CardLogonResorce";
            CardLogonResorce.Padding = new Padding(4);
            CardLogonResorce.Size = new Size(463, 205);
            CardLogonResorce.TabIndex = 7;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(440, 10);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(16, 16);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel4.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel4.HighEmphasis = true;
            materialLabel4.Location = new Point(10, 10);
            materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new Size(259, 24);
            materialLabel4.TabIndex = 1;
            materialLabel4.Text = "LOGON SERVER RESOURCES";
            // 
            // materialLabel5
            // 
            materialLabel5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel5.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel5.Location = new Point(30, 140);
            materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new Size(401, 15);
            materialLabel5.TabIndex = 5;
            materialLabel5.Text = "Central Processing Unit (CPU)";
            materialLabel5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar3
            // 
            materialProgressBar3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar3.Depth = 0;
            materialProgressBar3.Location = new Point(30, 95);
            materialProgressBar3.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar3.Name = "materialProgressBar3";
            materialProgressBar3.PbarHeight = 15;
            materialProgressBar3.Size = new Size(401, 15);
            materialProgressBar3.Style = ProgressBarStyle.Continuous;
            materialProgressBar3.TabIndex = 1;
            materialProgressBar3.Value = 10;
            // 
            // materialLabel6
            // 
            materialLabel6.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialLabel6.Depth = 0;
            materialLabel6.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel6.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel6.Location = new Point(30, 75);
            materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel6.Name = "materialLabel6";
            materialLabel6.Size = new Size(401, 15);
            materialLabel6.TabIndex = 3;
            materialLabel6.Text = "Random-Acess Memory  (RAM)";
            materialLabel6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar4
            // 
            materialProgressBar4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar4.Depth = 0;
            materialProgressBar4.Location = new Point(30, 160);
            materialProgressBar4.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar4.Name = "materialProgressBar4";
            materialProgressBar4.PbarHeight = 15;
            materialProgressBar4.Size = new Size(401, 15);
            materialProgressBar4.TabIndex = 4;
            materialProgressBar4.Value = 10;
            // 
            // CardWorldResources
            // 
            CardWorldResources.BackColor = Color.FromArgb(255, 255, 255);
            CardWorldResources.Controls.Add(pictureBox2);
            CardWorldResources.Controls.Add(materialLabel1);
            CardWorldResources.Controls.Add(materialLabel2);
            CardWorldResources.Controls.Add(materialProgressBar1);
            CardWorldResources.Controls.Add(materialLabel3);
            CardWorldResources.Controls.Add(materialProgressBar2);
            CardWorldResources.Depth = 0;
            CardWorldResources.Dock = DockStyle.Fill;
            CardWorldResources.ForeColor = Color.FromArgb(222, 0, 0, 0);
            CardWorldResources.Location = new Point(4, 216);
            CardWorldResources.Margin = new Padding(4);
            CardWorldResources.MouseState = MaterialSkin.MouseState.HOVER;
            CardWorldResources.Name = "CardWorldResources";
            CardWorldResources.Padding = new Padding(4);
            CardWorldResources.Size = new Size(463, 205);
            CardWorldResources.TabIndex = 6;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(440, 10);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(16, 16);
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel1.HighEmphasis = true;
            materialLabel1.Location = new Point(10, 10);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(260, 24);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "WORLD SERVER RESOURCES";
            // 
            // materialLabel2
            // 
            materialLabel2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel2.Location = new Point(30, 140);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(401, 15);
            materialLabel2.TabIndex = 5;
            materialLabel2.Text = "Central Processing Unit (CPU)";
            materialLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar1
            // 
            materialProgressBar1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar1.Depth = 0;
            materialProgressBar1.Location = new Point(30, 95);
            materialProgressBar1.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar1.Name = "materialProgressBar1";
            materialProgressBar1.PbarHeight = 15;
            materialProgressBar1.Size = new Size(401, 15);
            materialProgressBar1.Style = ProgressBarStyle.Continuous;
            materialProgressBar1.TabIndex = 1;
            materialProgressBar1.Value = 10;
            // 
            // materialLabel3
            // 
            materialLabel3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel3.Location = new Point(30, 75);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(401, 15);
            materialLabel3.TabIndex = 3;
            materialLabel3.Text = "Random-Access Memory  (RAM)";
            materialLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar2
            // 
            materialProgressBar2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar2.Depth = 0;
            materialProgressBar2.Location = new Point(30, 160);
            materialProgressBar2.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar2.Name = "materialProgressBar2";
            materialProgressBar2.PbarHeight = 15;
            materialProgressBar2.Size = new Size(401, 15);
            materialProgressBar2.TabIndex = 4;
            materialProgressBar2.Value = 10;
            // 
            // CardServerStatus
            // 
            CardServerStatus.BackColor = Color.FromArgb(255, 255, 255);
            CardServerStatus.Controls.Add(LayerPNLCardServerREsorce);
            CardServerStatus.Controls.Add(materialDrawer1);
            CardServerStatus.Depth = 0;
            CardServerStatus.Dock = DockStyle.Fill;
            CardServerStatus.ForeColor = Color.FromArgb(222, 0, 0, 0);
            CardServerStatus.Location = new Point(4, 4);
            CardServerStatus.Margin = new Padding(4);
            CardServerStatus.MouseState = MaterialSkin.MouseState.HOVER;
            CardServerStatus.Name = "CardServerStatus";
            CardServerStatus.Padding = new Padding(4);
            CardServerStatus.Size = new Size(463, 204);
            CardServerStatus.TabIndex = 5;
            // 
            // materialDrawer1
            // 
            materialDrawer1.AutoHide = false;
            materialDrawer1.AutoShow = false;
            materialDrawer1.BackgroundWithAccent = false;
            materialDrawer1.BaseTabControl = null;
            materialDrawer1.Depth = 0;
            materialDrawer1.HighlightWithAccent = true;
            materialDrawer1.IndicatorWidth = 0;
            materialDrawer1.IsOpen = false;
            materialDrawer1.Location = new Point(-432, 10);
            materialDrawer1.MouseState = MaterialSkin.MouseState.HOVER;
            materialDrawer1.Name = "materialDrawer1";
            materialDrawer1.ShowIconsWhenHidden = false;
            materialDrawer1.Size = new Size(432, 74);
            materialDrawer1.TabIndex = 0;
            materialDrawer1.Text = "materialDrawer1";
            materialDrawer1.UseColors = false;
            // 
            // CardMachineResoruces
            // 
            CardMachineResoruces.BackColor = Color.FromArgb(255, 255, 255);
            CardMachineResoruces.Controls.Add(pictureBox1);
            CardMachineResoruces.Controls.Add(LBLCardMachineResourcesTitle);
            CardMachineResoruces.Controls.Add(LBLCPUTextMachineResources);
            CardMachineResoruces.Controls.Add(PbarRAMMachineResources);
            CardMachineResoruces.Controls.Add(LBLRAMTextMachineResources);
            CardMachineResoruces.Controls.Add(PbarCPUMachineResources);
            CardMachineResoruces.Depth = 0;
            CardMachineResoruces.Dock = DockStyle.Fill;
            CardMachineResoruces.ForeColor = Color.FromArgb(222, 0, 0, 0);
            CardMachineResoruces.Location = new Point(475, 4);
            CardMachineResoruces.Margin = new Padding(4);
            CardMachineResoruces.MouseState = MaterialSkin.MouseState.HOVER;
            CardMachineResoruces.Name = "CardMachineResoruces";
            CardMachineResoruces.Padding = new Padding(4);
            CardMachineResoruces.Size = new Size(463, 204);
            CardMachineResoruces.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(440, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(16, 16);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // LBLCardMachineResourcesTitle
            // 
            LBLCardMachineResourcesTitle.AutoSize = true;
            LBLCardMachineResourcesTitle.Depth = 0;
            LBLCardMachineResourcesTitle.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            LBLCardMachineResourcesTitle.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            LBLCardMachineResourcesTitle.HighEmphasis = true;
            LBLCardMachineResourcesTitle.Location = new Point(10, 10);
            LBLCardMachineResourcesTitle.MouseState = MaterialSkin.MouseState.HOVER;
            LBLCardMachineResourcesTitle.Name = "LBLCardMachineResourcesTitle";
            LBLCardMachineResourcesTitle.Size = new Size(205, 24);
            LBLCardMachineResourcesTitle.TabIndex = 1;
            LBLCardMachineResourcesTitle.Text = "MACHINE RESOURCES";
            // 
            // LBLCPUTextMachineResources
            // 
            LBLCPUTextMachineResources.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LBLCPUTextMachineResources.Depth = 0;
            LBLCPUTextMachineResources.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            LBLCPUTextMachineResources.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            LBLCPUTextMachineResources.Location = new Point(30, 140);
            LBLCPUTextMachineResources.MouseState = MaterialSkin.MouseState.HOVER;
            LBLCPUTextMachineResources.Name = "LBLCPUTextMachineResources";
            LBLCPUTextMachineResources.Size = new Size(400, 17);
            LBLCPUTextMachineResources.TabIndex = 5;
            LBLCPUTextMachineResources.Text = "Central Processing Unit (CPU)";
            LBLCPUTextMachineResources.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PbarRAMMachineResources
            // 
            PbarRAMMachineResources.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PbarRAMMachineResources.Depth = 0;
            PbarRAMMachineResources.Location = new Point(30, 95);
            PbarRAMMachineResources.MouseState = MaterialSkin.MouseState.HOVER;
            PbarRAMMachineResources.Name = "PbarRAMMachineResources";
            PbarRAMMachineResources.PbarHeight = 15;
            PbarRAMMachineResources.Size = new Size(401, 15);
            PbarRAMMachineResources.Style = ProgressBarStyle.Continuous;
            PbarRAMMachineResources.TabIndex = 1;
            PbarRAMMachineResources.Value = 10;
            // 
            // LBLRAMTextMachineResources
            // 
            LBLRAMTextMachineResources.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            LBLRAMTextMachineResources.Depth = 0;
            LBLRAMTextMachineResources.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            LBLRAMTextMachineResources.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            LBLRAMTextMachineResources.Location = new Point(30, 75);
            LBLRAMTextMachineResources.MouseState = MaterialSkin.MouseState.HOVER;
            LBLRAMTextMachineResources.Name = "LBLRAMTextMachineResources";
            LBLRAMTextMachineResources.Size = new Size(400, 17);
            LBLRAMTextMachineResources.TabIndex = 3;
            LBLRAMTextMachineResources.Text = "Random-Access Memory  (RAM)";
            LBLRAMTextMachineResources.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PbarCPUMachineResources
            // 
            PbarCPUMachineResources.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PbarCPUMachineResources.Depth = 0;
            PbarCPUMachineResources.Location = new Point(30, 160);
            PbarCPUMachineResources.MouseState = MaterialSkin.MouseState.HOVER;
            PbarCPUMachineResources.Name = "PbarCPUMachineResources";
            PbarCPUMachineResources.PbarHeight = 15;
            PbarCPUMachineResources.Size = new Size(401, 15);
            PbarCPUMachineResources.TabIndex = 4;
            PbarCPUMachineResources.Value = 10;
            // 
            // TabDatabaseEditor
            // 
            TabDatabaseEditor.BackColor = Color.White;
            TabDatabaseEditor.ImageKey = "database-35.png";
            TabDatabaseEditor.Location = new Point(39, 4);
            TabDatabaseEditor.Name = "TabDatabaseEditor";
            TabDatabaseEditor.Padding = new Padding(3);
            TabDatabaseEditor.Size = new Size(948, 431);
            TabDatabaseEditor.TabIndex = 1;
            TabDatabaseEditor.Text = "Database Editor";
            // 
            // TabSettings
            // 
            TabSettings.BackColor = Color.White;
            TabSettings.ImageKey = "settings-35.png";
            TabSettings.Location = new Point(39, 4);
            TabSettings.Name = "TabSettings";
            TabSettings.Padding = new Padding(3);
            TabSettings.Size = new Size(948, 431);
            TabSettings.TabIndex = 2;
            TabSettings.Text = "Settings";
            // 
            // IMGListTabControler
            // 
            IMGListTabControler.ColorDepth = ColorDepth.Depth32Bit;
            IMGListTabControler.ImageStream = (ImageListStreamer)resources.GetObject("IMGListTabControler.ImageStream");
            IMGListTabControler.TransparentColor = Color.Transparent;
            IMGListTabControler.Images.SetKeyName(0, "menu-35.png");
            IMGListTabControler.Images.SetKeyName(1, "settings-35.png");
            IMGListTabControler.Images.SetKeyName(2, "database-35.png");
            // 
            // LayerPNLCardServerREsorce
            // 
            LayerPNLCardServerREsorce.ColumnCount = 1;
            LayerPNLCardServerREsorce.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            LayerPNLCardServerREsorce.Controls.Add(metroPanel2, 0, 2);
            LayerPNLCardServerREsorce.Controls.Add(metroPanel1, 0, 1);
            LayerPNLCardServerREsorce.Controls.Add(PNLDatanasServerResorce, 0, 0);
            LayerPNLCardServerREsorce.Dock = DockStyle.Fill;
            LayerPNLCardServerREsorce.Location = new Point(4, 4);
            LayerPNLCardServerREsorce.Name = "LayerPNLCardServerREsorce";
            LayerPNLCardServerREsorce.RowCount = 3;
            LayerPNLCardServerREsorce.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            LayerPNLCardServerREsorce.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            LayerPNLCardServerREsorce.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            LayerPNLCardServerREsorce.Size = new Size(455, 196);
            LayerPNLCardServerREsorce.TabIndex = 1;
            // 
            // PNLDatanasServerResorce
            // 
            PNLDatanasServerResorce.BackColor = Color.FromArgb(28, 33, 40);
            PNLDatanasServerResorce.Border = true;
            PNLDatanasServerResorce.BorderColor = Color.Black;
            PNLDatanasServerResorce.BorderSize = 1;
            PNLDatanasServerResorce.CustomBackground = true;
            PNLDatanasServerResorce.Dock = DockStyle.Fill;
            PNLDatanasServerResorce.HorizontalScrollbar = true;
            PNLDatanasServerResorce.HorizontalScrollbarBarColor = true;
            PNLDatanasServerResorce.HorizontalScrollbarHighlightOnWheel = false;
            PNLDatanasServerResorce.HorizontalScrollbarSize = 10;
            PNLDatanasServerResorce.Location = new Point(3, 3);
            PNLDatanasServerResorce.Name = "PNLDatanasServerResorce";
            PNLDatanasServerResorce.Padding = new Padding(2);
            PNLDatanasServerResorce.Size = new Size(449, 59);
            PNLDatanasServerResorce.Style = MetroFramework.MetroColorStyle.Blue;
            PNLDatanasServerResorce.StyleManager = null;
            PNLDatanasServerResorce.TabIndex = 35;
            PNLDatanasServerResorce.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLDatanasServerResorce.VerticalScrollbar = true;
            PNLDatanasServerResorce.VerticalScrollbarBarColor = true;
            PNLDatanasServerResorce.VerticalScrollbarHighlightOnWheel = false;
            PNLDatanasServerResorce.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            metroPanel1.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.CustomBackground = true;
            metroPanel1.Dock = DockStyle.Fill;
            metroPanel1.HorizontalScrollbar = true;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(3, 68);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Padding = new Padding(2);
            metroPanel1.Size = new Size(449, 59);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 36;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = true;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            metroPanel2.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.CustomBackground = true;
            metroPanel2.Dock = DockStyle.Fill;
            metroPanel2.HorizontalScrollbar = true;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(3, 133);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(449, 60);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 37;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = true;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1000, 600);
            Controls.Add(LayoutPanelMain);
            DrawerAutoShow = true;
            DrawerBackgroundWithAccent = true;
            DrawerIndicatorWidth = 2;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = MainFormTabControler;
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            FormStyle = FormStyles.ActionBar_48;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Padding = new Padding(0, 72, 3, 3);
            Text = "Trion Control Panel";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            CMSNotify.ResumeLayout(false);
            LayoutPanelMain.ResumeLayout(false);
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            MainFormTabControler.ResumeLayout(false);
            TabHome.ResumeLayout(false);
            LayoutPanelHome.ResumeLayout(false);
            CardLogonResorce.ResumeLayout(false);
            CardLogonResorce.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            CardWorldResources.ResumeLayout(false);
            CardWorldResources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            CardServerStatus.ResumeLayout(false);
            CardMachineResoruces.ResumeLayout(false);
            CardMachineResoruces.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            LayerPNLCardServerREsorce.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer TimerWacher;
        private NotifyIcon NIcon;
        private ContextMenuStrip CMSNotify;
        private ToolStripMenuItem OpenTSMItem;
        private ToolStripMenuItem StartWorldTSMItem;
        private ToolStripMenuItem StartLogonTSMItem;
        private ToolStripMenuItem StartDatabaseTSMItem;
        private ToolStripMenuItem ExitTSMItem;
        private System.Windows.Forms.Timer TimerLoadingCheck;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private TrionUI.Controls.CustomToolTip TLTHome;
        private System.Windows.Forms.Timer TimerCrashDetected;
        private TableLayoutPanel LayoutPanelMain;
        private MaterialSkin.Controls.MaterialTabControl MainFormTabControler;
        private TabPage TabHome;
        private TabPage TabDatabaseEditor;
        private TabPage TabSettings;
        private ImageList IMGListTabControler;
        private TableLayoutPanel LayoutPanelHome;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialCard CardMachineResoruces;
        private MaterialSkin.Controls.MaterialLabel LBLCPUTextMachineResources;
        private MaterialSkin.Controls.MaterialProgressBar PbarCPUMachineResources;
        private MaterialSkin.Controls.MaterialLabel LBLRAMTextMachineResources;
        private MaterialSkin.Controls.MaterialProgressBar PbarRAMMachineResources;
        private MaterialSkin.Controls.MaterialCard CardServerStatus;
        private MaterialSkin.Controls.MaterialLabel LBLCardMachineResourcesTitle;
        private MaterialSkin.Controls.MaterialFloatingActionButton BTNStartWorld;
        private MaterialSkin.Controls.MaterialFloatingActionButton BTNStartLogon;
        private MaterialSkin.Controls.MaterialFloatingActionButton BTNStartDatabase;
        private PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialCard CardWorldResources;
        private PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar2;
        private MaterialSkin.Controls.MaterialCard CardLogonResorce;
        private PictureBox pictureBox3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar3;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar4;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialDrawer materialDrawer1;
        private TableLayoutPanel LayerPNLCardServerREsorce;
        private MetroFramework.Controls.MetroPanel PNLDatanasServerResorce;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
    }
}