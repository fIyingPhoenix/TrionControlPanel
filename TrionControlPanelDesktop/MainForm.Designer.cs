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
            panel1 = new Panel();
            panel2 = new Panel();
            BTNConsole = new UI.Controls.CustomButton();
            LblVersion = new Label();
            BTNSettings = new UI.Controls.CustomButton();
            BTNdatabase = new UI.Controls.CustomButton();
            BTNHome = new UI.Controls.CustomButton();
            PNLControl = new MetroPanel();
            TimerWacher = new System.Windows.Forms.Timer(components);
            PnlButtonFront = new MetroPanel();
            BTNStartMySQL = new UI.Controls.CustomButton();
            BTNStartLogin = new UI.Controls.CustomButton();
            BTNStartWorld = new UI.Controls.CustomButton();
            ContributorsPNLFront = new MetroPanel();
            BTNDownload = new UI.Controls.CustomButton();
            BTNNotification = new UI.Controls.CustomButton();
            TimerChangeControl = new System.Windows.Forms.Timer(components);
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
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            PnlButtonFront.SuspendLayout();
            ContributorsPNLFront.SuspendLayout();
            CMSNotify.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(-1, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(92, 597);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(34, 39, 46);
            panel2.Controls.Add(BTNConsole);
            panel2.Controls.Add(LblVersion);
            panel2.Controls.Add(BTNSettings);
            panel2.Controls.Add(BTNdatabase);
            panel2.Controls.Add(BTNHome);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(90, 597);
            panel2.TabIndex = 1;
            // 
            // BTNConsole
            // 
            BTNConsole.BackColor = Color.FromArgb(28, 33, 40);
            BTNConsole.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNConsole.BorderColor = Color.FromArgb(0, 174, 219);
            BTNConsole.BorderRadius = 0;
            BTNConsole.BorderSize = 1;
            BTNConsole.Cursor = Cursors.Hand;
            BTNConsole.FlatAppearance.BorderSize = 0;
            BTNConsole.FlatStyle = FlatStyle.Flat;
            BTNConsole.ForeColor = Color.White;
            BTNConsole.Image = (Image)resources.GetObject("BTNConsole.Image");
            BTNConsole.Location = new Point(10, 238);
            BTNConsole.Name = "BTNConsole";
            BTNConsole.NotificationCount = 0;
            BTNConsole.Size = new Size(70, 70);
            BTNConsole.TabIndex = 5;
            BTNConsole.TextColor = Color.White;
            BTNConsole.UseVisualStyleBackColor = false;
            BTNConsole.Visible = false;
            BTNConsole.Click += BTNConsole_Click;
            // 
            // LblVersion
            // 
            LblVersion.Anchor = AnchorStyles.Bottom;
            LblVersion.Location = new Point(0, 575);
            LblVersion.Name = "LblVersion";
            LblVersion.Size = new Size(89, 15);
            LblVersion.TabIndex = 4;
            LblVersion.Text = "Version: ";
            LblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNSettings
            // 
            BTNSettings.BackColor = Color.FromArgb(28, 33, 40);
            BTNSettings.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNSettings.BorderColor = Color.FromArgb(0, 174, 219);
            BTNSettings.BorderRadius = 0;
            BTNSettings.BorderSize = 1;
            BTNSettings.Cursor = Cursors.Hand;
            BTNSettings.FlatAppearance.BorderSize = 0;
            BTNSettings.FlatStyle = FlatStyle.Flat;
            BTNSettings.ForeColor = Color.White;
            BTNSettings.Image = (Image)resources.GetObject("BTNSettings.Image");
            BTNSettings.Location = new Point(10, 162);
            BTNSettings.Name = "BTNSettings";
            BTNSettings.NotificationCount = 0;
            BTNSettings.Size = new Size(70, 70);
            BTNSettings.TabIndex = 3;
            BTNSettings.TextColor = Color.White;
            BTNSettings.UseVisualStyleBackColor = false;
            BTNSettings.Visible = false;
            BTNSettings.Click += SettingsBTN_Click;
            // 
            // BTNdatabase
            // 
            BTNdatabase.BackColor = Color.FromArgb(28, 33, 40);
            BTNdatabase.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNdatabase.BorderColor = Color.FromArgb(0, 174, 219);
            BTNdatabase.BorderRadius = 0;
            BTNdatabase.BorderSize = 1;
            BTNdatabase.Cursor = Cursors.Hand;
            BTNdatabase.FlatAppearance.BorderSize = 0;
            BTNdatabase.FlatStyle = FlatStyle.Flat;
            BTNdatabase.ForeColor = Color.White;
            BTNdatabase.Image = (Image)resources.GetObject("BTNdatabase.Image");
            BTNdatabase.Location = new Point(10, 86);
            BTNdatabase.Name = "BTNdatabase";
            BTNdatabase.NotificationCount = 0;
            BTNdatabase.Size = new Size(70, 70);
            BTNdatabase.TabIndex = 2;
            BTNdatabase.TextColor = Color.White;
            BTNdatabase.UseVisualStyleBackColor = false;
            BTNdatabase.Visible = false;
            BTNdatabase.Click += TerminaBTN_Click;
            // 
            // BTNHome
            // 
            BTNHome.BackColor = Color.FromArgb(28, 33, 40);
            BTNHome.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNHome.BorderColor = Color.FromArgb(0, 174, 219);
            BTNHome.BorderRadius = 0;
            BTNHome.BorderSize = 1;
            BTNHome.Cursor = Cursors.Hand;
            BTNHome.FlatAppearance.BorderSize = 0;
            BTNHome.FlatStyle = FlatStyle.Flat;
            BTNHome.ForeColor = Color.White;
            BTNHome.Image = (Image)resources.GetObject("BTNHome.Image");
            BTNHome.Location = new Point(10, 10);
            BTNHome.Name = "BTNHome";
            BTNHome.NotificationCount = 0;
            BTNHome.Size = new Size(70, 70);
            BTNHome.TabIndex = 1;
            BTNHome.TextColor = Color.White;
            BTNHome.UseVisualStyleBackColor = false;
            BTNHome.Visible = false;
            BTNHome.Click += HomeBTN_Click;
            // 
            // PNLControl
            // 
            PNLControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PNLControl.BackColor = Color.FromArgb(45, 51, 59);
            PNLControl.Border = true;
            PNLControl.BorderColor = Color.Black;
            PNLControl.BorderSize = 1;
            PNLControl.CustomBackground = false;
            PNLControl.HorizontalScrollbar = false;
            PNLControl.HorizontalScrollbarBarColor = true;
            PNLControl.HorizontalScrollbarHighlightOnWheel = false;
            PNLControl.HorizontalScrollbarSize = 10;
            PNLControl.Location = new Point(120, 142);
            PNLControl.MaximumSize = new Size(0, 370);
            PNLControl.MinimumSize = new Size(845, 370);
            PNLControl.Name = "PNLControl";
            PNLControl.Padding = new Padding(2);
            PNLControl.Size = new Size(845, 370);
            PNLControl.Style = MetroFramework.MetroColorStyle.Blue;
            PNLControl.StyleManager = null;
            PNLControl.TabIndex = 4;
            PNLControl.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLControl.VerticalScrollbar = false;
            PNLControl.VerticalScrollbarBarColor = true;
            PNLControl.VerticalScrollbarHighlightOnWheel = false;
            PNLControl.VerticalScrollbarSize = 10;
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // PnlButtonFront
            // 
            PnlButtonFront.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PnlButtonFront.BackColor = Color.FromArgb(34, 39, 46);
            PnlButtonFront.Border = true;
            PnlButtonFront.BorderColor = Color.Black;
            PnlButtonFront.BorderSize = 1;
            PnlButtonFront.Controls.Add(BTNStartMySQL);
            PnlButtonFront.Controls.Add(BTNStartLogin);
            PnlButtonFront.Controls.Add(BTNStartWorld);
            PnlButtonFront.CustomBackground = true;
            PnlButtonFront.HorizontalScrollbar = false;
            PnlButtonFront.HorizontalScrollbarBarColor = true;
            PnlButtonFront.HorizontalScrollbarHighlightOnWheel = false;
            PnlButtonFront.HorizontalScrollbarSize = 10;
            PnlButtonFront.Location = new Point(120, 517);
            PnlButtonFront.Name = "PnlButtonFront";
            PnlButtonFront.Padding = new Padding(2);
            PnlButtonFront.Size = new Size(845, 55);
            PnlButtonFront.Style = MetroFramework.MetroColorStyle.Blue;
            PnlButtonFront.StyleManager = null;
            PnlButtonFront.TabIndex = 5;
            PnlButtonFront.Theme = MetroFramework.MetroThemeStyle.Dark;
            PnlButtonFront.VerticalScrollbar = false;
            PnlButtonFront.VerticalScrollbarBarColor = true;
            PnlButtonFront.VerticalScrollbarHighlightOnWheel = false;
            PnlButtonFront.VerticalScrollbarSize = 10;
            // 
            // BTNStartMySQL
            // 
            BTNStartMySQL.Anchor = AnchorStyles.Top;
            BTNStartMySQL.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartMySQL.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartMySQL.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartMySQL.BorderRadius = 0;
            BTNStartMySQL.BorderSize = 1;
            BTNStartMySQL.Cursor = Cursors.Hand;
            BTNStartMySQL.FlatAppearance.BorderSize = 0;
            BTNStartMySQL.FlatStyle = FlatStyle.Flat;
            BTNStartMySQL.ForeColor = Color.White;
            BTNStartMySQL.Image = (Image)resources.GetObject("BTNStartMySQL.Image");
            BTNStartMySQL.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartMySQL.Location = new Point(498, 8);
            BTNStartMySQL.Name = "BTNStartMySQL";
            BTNStartMySQL.NotificationCount = 0;
            BTNStartMySQL.Size = new Size(140, 40);
            BTNStartMySQL.TabIndex = 8;
            BTNStartMySQL.Text = "            Database  ";
            BTNStartMySQL.TextColor = Color.White;
            BTNStartMySQL.UseVisualStyleBackColor = false;
            BTNStartMySQL.Visible = false;
            BTNStartMySQL.Click += BTNStartMySQL_Click;
            // 
            // BTNStartLogin
            // 
            BTNStartLogin.Anchor = AnchorStyles.Top;
            BTNStartLogin.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartLogin.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartLogin.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartLogin.BorderRadius = 0;
            BTNStartLogin.BorderSize = 1;
            BTNStartLogin.Cursor = Cursors.Hand;
            BTNStartLogin.FlatAppearance.BorderSize = 0;
            BTNStartLogin.FlatStyle = FlatStyle.Flat;
            BTNStartLogin.ForeColor = Color.White;
            BTNStartLogin.Image = (Image)resources.GetObject("BTNStartLogin.Image");
            BTNStartLogin.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartLogin.Location = new Point(206, 8);
            BTNStartLogin.Name = "BTNStartLogin";
            BTNStartLogin.NotificationCount = 0;
            BTNStartLogin.Size = new Size(140, 40);
            BTNStartLogin.TabIndex = 7;
            BTNStartLogin.Text = "            Logon";
            BTNStartLogin.TextColor = Color.White;
            BTNStartLogin.UseVisualStyleBackColor = false;
            BTNStartLogin.Visible = false;
            BTNStartLogin.Click += BTNStartLogin_Click;
            // 
            // BTNStartWorld
            // 
            BTNStartWorld.Anchor = AnchorStyles.Top;
            BTNStartWorld.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartWorld.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartWorld.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartWorld.BorderRadius = 0;
            BTNStartWorld.BorderSize = 1;
            BTNStartWorld.Cursor = Cursors.Hand;
            BTNStartWorld.FlatAppearance.BorderSize = 0;
            BTNStartWorld.FlatStyle = FlatStyle.Flat;
            BTNStartWorld.ForeColor = Color.White;
            BTNStartWorld.Image = (Image)resources.GetObject("BTNStartWorld.Image");
            BTNStartWorld.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartWorld.Location = new Point(352, 8);
            BTNStartWorld.Name = "BTNStartWorld";
            BTNStartWorld.NotificationCount = 0;
            BTNStartWorld.Size = new Size(140, 40);
            BTNStartWorld.TabIndex = 6;
            BTNStartWorld.Text = "            World ";
            BTNStartWorld.TextColor = Color.White;
            BTNStartWorld.UseVisualStyleBackColor = false;
            BTNStartWorld.Visible = false;
            BTNStartWorld.Click += BTNStartWorld_Click;
            // 
            // ContributorsPNLFront
            // 
            ContributorsPNLFront.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ContributorsPNLFront.BackColor = Color.FromArgb(34, 39, 46);
            ContributorsPNLFront.Border = true;
            ContributorsPNLFront.BorderColor = Color.Black;
            ContributorsPNLFront.BorderSize = 1;
            ContributorsPNLFront.Controls.Add(BTNDownload);
            ContributorsPNLFront.Controls.Add(BTNNotification);
            ContributorsPNLFront.CustomBackground = true;
            ContributorsPNLFront.HorizontalScrollbar = false;
            ContributorsPNLFront.HorizontalScrollbarBarColor = true;
            ContributorsPNLFront.HorizontalScrollbarHighlightOnWheel = false;
            ContributorsPNLFront.HorizontalScrollbarSize = 10;
            ContributorsPNLFront.Location = new Point(120, 62);
            ContributorsPNLFront.Name = "ContributorsPNLFront";
            ContributorsPNLFront.Padding = new Padding(2);
            ContributorsPNLFront.Size = new Size(845, 73);
            ContributorsPNLFront.Style = MetroFramework.MetroColorStyle.Blue;
            ContributorsPNLFront.StyleManager = null;
            ContributorsPNLFront.TabIndex = 6;
            ContributorsPNLFront.Theme = MetroFramework.MetroThemeStyle.Dark;
            ContributorsPNLFront.VerticalScrollbar = false;
            ContributorsPNLFront.VerticalScrollbarBarColor = true;
            ContributorsPNLFront.VerticalScrollbarHighlightOnWheel = false;
            ContributorsPNLFront.VerticalScrollbarSize = 10;
            // 
            // BTNDownload
            // 
            BTNDownload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTNDownload.BackColor = Color.FromArgb(34, 39, 46);
            BTNDownload.BackgroundColor = Color.FromArgb(34, 39, 46);
            BTNDownload.BorderColor = Color.FromArgb(0, 174, 219);
            BTNDownload.BorderRadius = 0;
            BTNDownload.BorderSize = 0;
            BTNDownload.Cursor = Cursors.Hand;
            BTNDownload.FlatAppearance.BorderSize = 0;
            BTNDownload.FlatStyle = FlatStyle.Flat;
            BTNDownload.ForeColor = Color.White;
            BTNDownload.Image = (Image)resources.GetObject("BTNDownload.Image");
            BTNDownload.Location = new Point(758, 5);
            BTNDownload.Name = "BTNDownload";
            BTNDownload.NotificationCount = 0;
            BTNDownload.Size = new Size(41, 36);
            BTNDownload.TabIndex = 8;
            BTNDownload.TextColor = Color.White;
            BTNDownload.UseVisualStyleBackColor = true;
            BTNDownload.Visible = false;
            BTNDownload.Click += BTNDownload_Click;
            // 
            // BTNNotification
            // 
            BTNNotification.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BTNNotification.BackColor = Color.FromArgb(34, 39, 46);
            BTNNotification.BackgroundColor = Color.FromArgb(34, 39, 46);
            BTNNotification.BorderColor = Color.FromArgb(0, 174, 219);
            BTNNotification.BorderRadius = 0;
            BTNNotification.BorderSize = 0;
            BTNNotification.Cursor = Cursors.Hand;
            BTNNotification.FlatAppearance.BorderSize = 0;
            BTNNotification.FlatStyle = FlatStyle.Flat;
            BTNNotification.ForeColor = Color.White;
            BTNNotification.Image = (Image)resources.GetObject("BTNNotification.Image");
            BTNNotification.Location = new Point(803, 5);
            BTNNotification.Name = "BTNNotification";
            BTNNotification.NotificationCount = 0;
            BTNNotification.Size = new Size(37, 36);
            BTNNotification.TabIndex = 7;
            BTNNotification.TextColor = Color.White;
            BTNNotification.UseVisualStyleBackColor = true;
            BTNNotification.Visible = false;
            BTNNotification.Click += BTNNotification_Click;
            // 
            // TimerChangeControl
            // 
            TimerChangeControl.Interval = 10;
            TimerChangeControl.Tick += TimerChangeControl_Tick;
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 33, 40);
            ClientSize = new Size(1000, 600);
            Controls.Add(ContributorsPNLFront);
            Controls.Add(PnlButtonFront);
            Controls.Add(PNLControl);
            Controls.Add(panel1);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(0, 0);
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Shadow = false;
            Text = "    Trion Contro Panel";
            TextAlign = MetroFramework.Forms.TextAlign.Center;
            Theme = MetroFramework.MetroThemeStyle.Dark;
            TransparencyKey = Color.Magenta;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            PnlButtonFront.ResumeLayout(false);
            ContributorsPNLFront.ResumeLayout(false);
            CMSNotify.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private UI.Controls.CustomButton BTNHome;
        private UI.Controls.CustomButton BTNSettings;
        private UI.Controls.CustomButton BTNdatabase;
        private Label LblVersion;
        private MetroPanel PNLControl;
        private System.Windows.Forms.Timer TimerWacher;
        private MetroPanel PnlButtonFront;
        private UI.Controls.CustomButton BTNStartMySQL;
        private UI.Controls.CustomButton BTNStartLogin;
        private UI.Controls.CustomButton BTNStartWorld;
        private MetroPanel ContributorsPNLFront;
        private System.Windows.Forms.Timer TimerChangeControl;
        private UI.Controls.CustomButton BTNNotification;
        private UI.Controls.CustomButton BTNDownload;
        private UI.Controls.CustomButton BTNConsole;
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
    }
}