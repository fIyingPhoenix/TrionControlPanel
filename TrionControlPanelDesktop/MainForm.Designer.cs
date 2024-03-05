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
            LblVersion = new Label();
            SettingsBTN = new UI.Controls.CustomButton();
            TerminaBTN = new UI.Controls.CustomButton();
            HomeBTN = new UI.Controls.CustomButton();
            PNLControl = new MetroPanel();
            TimerWacher = new System.Windows.Forms.Timer(components);
            PnlButtonFront = new MetroPanel();
            BTNStartMySQL = new UI.Controls.CustomButton();
            BTNStartAll = new UI.Controls.CustomButton();
            BTNStartLogin = new UI.Controls.CustomButton();
            BTNStartWorld = new UI.Controls.CustomButton();
            ContributorsPNLFront = new MetroPanel();
            TimerChangeControl = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            PnlButtonFront.SuspendLayout();
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
            panel2.Controls.Add(LblVersion);
            panel2.Controls.Add(SettingsBTN);
            panel2.Controls.Add(TerminaBTN);
            panel2.Controls.Add(HomeBTN);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(90, 597);
            panel2.TabIndex = 1;
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
            // SettingsBTN
            // 
            SettingsBTN.BackColor = Color.FromArgb(28, 33, 40);
            SettingsBTN.BackgroundColor = Color.FromArgb(28, 33, 40);
            SettingsBTN.BorderColor = Color.FromArgb(0, 174, 219);
            SettingsBTN.BorderRadius = 0;
            SettingsBTN.BorderSize = 1;
            SettingsBTN.Cursor = Cursors.Hand;
            SettingsBTN.FlatAppearance.BorderSize = 0;
            SettingsBTN.FlatStyle = FlatStyle.Flat;
            SettingsBTN.ForeColor = Color.White;
            SettingsBTN.Image = (Image)resources.GetObject("SettingsBTN.Image");
            SettingsBTN.Location = new Point(10, 168);
            SettingsBTN.Name = "SettingsBTN";
            SettingsBTN.Size = new Size(70, 70);
            SettingsBTN.TabIndex = 3;
            SettingsBTN.TextColor = Color.White;
            SettingsBTN.UseVisualStyleBackColor = false;
            SettingsBTN.Visible = false;
            SettingsBTN.Click += SettingsBTN_Click;
            // 
            // TerminaBTN
            // 
            TerminaBTN.BackColor = Color.FromArgb(28, 33, 40);
            TerminaBTN.BackgroundColor = Color.FromArgb(28, 33, 40);
            TerminaBTN.BorderColor = Color.FromArgb(0, 174, 219);
            TerminaBTN.BorderRadius = 0;
            TerminaBTN.BorderSize = 1;
            TerminaBTN.Cursor = Cursors.Hand;
            TerminaBTN.FlatAppearance.BorderSize = 0;
            TerminaBTN.FlatStyle = FlatStyle.Flat;
            TerminaBTN.ForeColor = Color.White;
            TerminaBTN.Image = (Image)resources.GetObject("TerminaBTN.Image");
            TerminaBTN.Location = new Point(10, 89);
            TerminaBTN.Name = "TerminaBTN";
            TerminaBTN.Size = new Size(70, 70);
            TerminaBTN.TabIndex = 2;
            TerminaBTN.TextColor = Color.White;
            TerminaBTN.UseVisualStyleBackColor = false;
            TerminaBTN.Visible = false;
            TerminaBTN.Click += TerminaBTN_Click;
            // 
            // HomeBTN
            // 
            HomeBTN.BackColor = Color.FromArgb(28, 33, 40);
            HomeBTN.BackgroundColor = Color.FromArgb(28, 33, 40);
            HomeBTN.BorderColor = Color.FromArgb(0, 174, 219);
            HomeBTN.BorderRadius = 0;
            HomeBTN.BorderSize = 1;
            HomeBTN.Cursor = Cursors.Hand;
            HomeBTN.FlatAppearance.BorderSize = 0;
            HomeBTN.FlatStyle = FlatStyle.Flat;
            HomeBTN.ForeColor = Color.White;
            HomeBTN.Image = (Image)resources.GetObject("HomeBTN.Image");
            HomeBTN.Location = new Point(10, 10);
            HomeBTN.Name = "HomeBTN";
            HomeBTN.Size = new Size(70, 70);
            HomeBTN.TabIndex = 1;
            HomeBTN.TextColor = Color.White;
            HomeBTN.UseVisualStyleBackColor = false;
            HomeBTN.Visible = false;
            HomeBTN.Click += HomeBTN_Click;
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
            PnlButtonFront.Controls.Add(BTNStartAll);
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
            BTNStartMySQL.Location = new Point(423, 8);
            BTNStartMySQL.Name = "BTNStartMySQL";
            BTNStartMySQL.Size = new Size(140, 40);
            BTNStartMySQL.TabIndex = 8;
            BTNStartMySQL.Text = "            Start  MySQL  ";
            BTNStartMySQL.TextColor = Color.White;
            BTNStartMySQL.UseVisualStyleBackColor = false;
            BTNStartMySQL.Visible = false;
            BTNStartMySQL.Click += BTNStartMySQL_Click;
            // 
            // BTNStartAll
            // 
            BTNStartAll.Anchor = AnchorStyles.Top;
            BTNStartAll.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartAll.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartAll.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartAll.BorderRadius = 0;
            BTNStartAll.BorderSize = 1;
            BTNStartAll.Cursor = Cursors.Hand;
            BTNStartAll.FlatAppearance.BorderSize = 0;
            BTNStartAll.FlatStyle = FlatStyle.Flat;
            BTNStartAll.ForeColor = Color.White;
            BTNStartAll.Image = (Image)resources.GetObject("BTNStartAll.Image");
            BTNStartAll.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartAll.Location = new Point(569, 8);
            BTNStartAll.Name = "BTNStartAll";
            BTNStartAll.Size = new Size(140, 40);
            BTNStartAll.TabIndex = 9;
            BTNStartAll.Text = "           Start All";
            BTNStartAll.TextColor = Color.White;
            BTNStartAll.UseVisualStyleBackColor = false;
            BTNStartAll.Visible = false;
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
            BTNStartLogin.Location = new Point(131, 8);
            BTNStartLogin.Name = "BTNStartLogin";
            BTNStartLogin.Size = new Size(140, 40);
            BTNStartLogin.TabIndex = 7;
            BTNStartLogin.Text = "            Start  Login  ";
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
            BTNStartWorld.Location = new Point(277, 8);
            BTNStartWorld.Name = "BTNStartWorld";
            BTNStartWorld.Size = new Size(140, 40);
            BTNStartWorld.TabIndex = 6;
            BTNStartWorld.Text = "            Start  World ";
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
            // TimerChangeControl
            // 
            TimerChangeControl.Interval = 10;
            TimerChangeControl.Tick += TimerChangeControl_Tick;
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
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Location = new Point(0, 0);
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Shadow = false;
            Text = "    Trion Contro Panel";
            TextAlign = MetroFramework.Forms.TextAlign.Center;
            Theme = MetroFramework.MetroThemeStyle.Dark;
            TransparencyKey = Color.Magenta;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            PnlButtonFront.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private UI.Controls.CustomButton HomeBTN;
        private UI.Controls.CustomButton SettingsBTN;
        private UI.Controls.CustomButton TerminaBTN;
        private Label LblVersion;
        private MetroPanel PNLControl;
        private System.Windows.Forms.Timer TimerWacher;
        private MetroPanel PnlButtonFront;
        private UI.Controls.CustomButton BTNStartMySQL;
        private UI.Controls.CustomButton BTNStartAll;
        private UI.Controls.CustomButton BTNStartLogin;
        private UI.Controls.CustomButton BTNStartWorld;
        private MetroPanel ContributorsPNLFront;
        private System.Windows.Forms.Timer TimerChangeControl;
    }
}