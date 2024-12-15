using MetroFramework.Controls;
using TrionControlPanel.UI;

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
            TimerCrashDetected = new System.Windows.Forms.Timer(components);
            LayoutPanelMain = new TableLayoutPanel();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            MainFormTabControler = new MaterialSkin.Controls.MaterialTabControl();
            TabHome = new TabPage();
            LayoutPanelHome = new TableLayoutPanel();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            CardMachineResoruces = new MaterialSkin.Controls.MaterialCard();
            LBLCardMachineResourcesTitle = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar1 = new MaterialSkin.Controls.MaterialProgressBar();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialProgressBar2 = new MaterialSkin.Controls.MaterialProgressBar();
            TabDatabaseEditor = new TabPage();
            TabSettings = new TabPage();
            IMGListTabControler = new ImageList(components);
            CMSNotify.SuspendLayout();
            LayoutPanelMain.SuspendLayout();
            MainFormTabControler.SuspendLayout();
            TabHome.SuspendLayout();
            LayoutPanelHome.SuspendLayout();
            CardMachineResoruces.SuspendLayout();
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
            // TimerCrashDetected
            // 
            TimerCrashDetected.Enabled = true;
            TimerCrashDetected.Interval = 5000;
            TimerCrashDetected.Tick += TimerCrashDetected_Tick;
            // 
            // LayoutPanelMain
            // 
            LayoutPanelMain.ColumnCount = 1;
            LayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LayoutPanelMain.Controls.Add(materialCard1, 0, 1);
            LayoutPanelMain.Controls.Add(MainFormTabControler, 0, 0);
            LayoutPanelMain.Dock = DockStyle.Fill;
            LayoutPanelMain.Location = new Point(3, 64);
            LayoutPanelMain.Name = "LayoutPanelMain";
            LayoutPanelMain.RowCount = 2;
            LayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 86.30394F));
            LayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 13.69606F));
            LayoutPanelMain.Size = new Size(994, 533);
            LayoutPanelMain.TabIndex = 9;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Fill;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(10, 464);
            materialCard1.Margin = new Padding(10, 4, 10, 10);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(4);
            materialCard1.Size = new Size(974, 59);
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
            MainFormTabControler.Size = new Size(988, 454);
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
            TabHome.Size = new Size(945, 446);
            TabHome.TabIndex = 0;
            TabHome.Text = "Home";
            // 
            // LayoutPanelHome
            // 
            LayoutPanelHome.ColumnCount = 2;
            LayoutPanelHome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LayoutPanelHome.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LayoutPanelHome.Controls.Add(materialCard2, 0, 0);
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
            LayoutPanelHome.Size = new Size(939, 440);
            LayoutPanelHome.TabIndex = 0;
            // 
            // materialCard2
            // 
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Depth = 0;
            materialCard2.Dock = DockStyle.Fill;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(4, 4);
            materialCard2.Margin = new Padding(4);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(4);
            materialCard2.Size = new Size(461, 212);
            materialCard2.TabIndex = 5;
            // 
            // CardMachineResoruces
            // 
            CardMachineResoruces.BackColor = Color.FromArgb(255, 255, 255);
            CardMachineResoruces.Controls.Add(LBLCardMachineResourcesTitle);
            CardMachineResoruces.Controls.Add(materialLabel2);
            CardMachineResoruces.Controls.Add(materialProgressBar1);
            CardMachineResoruces.Controls.Add(materialLabel1);
            CardMachineResoruces.Controls.Add(materialProgressBar2);
            CardMachineResoruces.Depth = 0;
            CardMachineResoruces.Dock = DockStyle.Fill;
            CardMachineResoruces.ForeColor = Color.FromArgb(222, 0, 0, 0);
            CardMachineResoruces.Location = new Point(473, 4);
            CardMachineResoruces.Margin = new Padding(4);
            CardMachineResoruces.MouseState = MaterialSkin.MouseState.HOVER;
            CardMachineResoruces.Name = "CardMachineResoruces";
            CardMachineResoruces.Padding = new Padding(4);
            CardMachineResoruces.Size = new Size(462, 212);
            CardMachineResoruces.TabIndex = 4;
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
            // materialLabel2
            // 
            materialLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel2.Location = new Point(29, 130);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(400, 17);
            materialLabel2.TabIndex = 5;
            materialLabel2.Text = "Central Processing Unit (CPU)";
            materialLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar1
            // 
            materialProgressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar1.Depth = 0;
            materialProgressBar1.Location = new Point(30, 100);
            materialProgressBar1.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar1.Name = "materialProgressBar1";
            materialProgressBar1.PbarHeight = 15;
            materialProgressBar1.Size = new Size(400, 15);
            materialProgressBar1.Style = ProgressBarStyle.Continuous;
            materialProgressBar1.TabIndex = 1;
            materialProgressBar1.Value = 10;
            // 
            // materialLabel1
            // 
            materialLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.Button;
            materialLabel1.Location = new Point(29, 71);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(400, 17);
            materialLabel1.TabIndex = 3;
            materialLabel1.Text = "Random-Access Memory  (RAM)";
            materialLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // materialProgressBar2
            // 
            materialProgressBar2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            materialProgressBar2.Depth = 0;
            materialProgressBar2.Location = new Point(30, 165);
            materialProgressBar2.MouseState = MaterialSkin.MouseState.HOVER;
            materialProgressBar2.Name = "materialProgressBar2";
            materialProgressBar2.PbarHeight = 5;
            materialProgressBar2.Size = new Size(400, 5);
            materialProgressBar2.TabIndex = 4;
            materialProgressBar2.Value = 10;
            // 
            // TabDatabaseEditor
            // 
            TabDatabaseEditor.BackColor = Color.White;
            TabDatabaseEditor.ImageKey = "database-35.png";
            TabDatabaseEditor.Location = new Point(39, 4);
            TabDatabaseEditor.Name = "TabDatabaseEditor";
            TabDatabaseEditor.Padding = new Padding(3);
            TabDatabaseEditor.Size = new Size(945, 446);
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
            TabSettings.Size = new Size(945, 446);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1000, 600);
            Controls.Add(LayoutPanelMain);
            DrawerAutoShow = true;
            DrawerBackgroundWithAccent = true;
            DrawerIndicatorWidth = 2;
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = MainFormTabControler;
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Text = "Trion Control Panel";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            CMSNotify.ResumeLayout(false);
            LayoutPanelMain.ResumeLayout(false);
            MainFormTabControler.ResumeLayout(false);
            TabHome.ResumeLayout(false);
            LayoutPanelHome.ResumeLayout(false);
            CardMachineResoruces.ResumeLayout(false);
            CardMachineResoruces.PerformLayout();
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
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialProgressBar materialProgressBar1;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialLabel LBLCardMachineResourcesTitle;
    }
}