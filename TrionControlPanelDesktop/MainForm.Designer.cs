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
            ContributorsPNLBack = new Panel();
            ContributorsPNLFront = new Panel();
            QualityWEB = new UI.CustomWebBrowser();
            IssuesWEB = new UI.CustomWebBrowser();
            StarsWEB = new UI.CustomWebBrowser();
            ForkWEB = new UI.CustomWebBrowser();
            ContiWEB = new UI.CustomWebBrowser();
            PNLControl = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            BTNStartMySQL = new UI.Controls.CustomButton();
            BTNStartAll = new UI.Controls.CustomButton();
            BTNStartLOgin = new UI.Controls.CustomButton();
            BTNStartWorld = new UI.Controls.CustomButton();
            TimerWacher = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ContributorsPNLBack.SuspendLayout();
            ContributorsPNLFront.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
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
            LblVersion.Location = new Point(-4, 574);
            LblVersion.Name = "LblVersion";
            LblVersion.Size = new Size(101, 15);
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
            SettingsBTN.FlatAppearance.BorderSize = 0;
            SettingsBTN.FlatStyle = FlatStyle.Flat;
            SettingsBTN.ForeColor = Color.White;
            SettingsBTN.Image = (Image)resources.GetObject("SettingsBTN.Image");
            SettingsBTN.Location = new Point(9, 187);
            SettingsBTN.Name = "SettingsBTN";
            SettingsBTN.Size = new Size(70, 70);
            SettingsBTN.TabIndex = 3;
            SettingsBTN.TextColor = Color.White;
            SettingsBTN.UseVisualStyleBackColor = false;
            SettingsBTN.Click += SettingsBTN_Click;
            // 
            // TerminaBTN
            // 
            TerminaBTN.BackColor = Color.FromArgb(28, 33, 40);
            TerminaBTN.BackgroundColor = Color.FromArgb(28, 33, 40);
            TerminaBTN.BorderColor = Color.FromArgb(0, 174, 219);
            TerminaBTN.BorderRadius = 0;
            TerminaBTN.BorderSize = 1;
            TerminaBTN.FlatAppearance.BorderSize = 0;
            TerminaBTN.FlatStyle = FlatStyle.Flat;
            TerminaBTN.ForeColor = Color.White;
            TerminaBTN.Image = (Image)resources.GetObject("TerminaBTN.Image");
            TerminaBTN.Location = new Point(9, 98);
            TerminaBTN.Name = "TerminaBTN";
            TerminaBTN.Size = new Size(70, 70);
            TerminaBTN.TabIndex = 2;
            TerminaBTN.TextColor = Color.White;
            TerminaBTN.UseVisualStyleBackColor = false;
            // 
            // HomeBTN
            // 
            HomeBTN.BackColor = Color.FromArgb(28, 33, 40);
            HomeBTN.BackgroundColor = Color.FromArgb(28, 33, 40);
            HomeBTN.BorderColor = Color.FromArgb(0, 174, 219);
            HomeBTN.BorderRadius = 0;
            HomeBTN.BorderSize = 1;
            HomeBTN.FlatAppearance.BorderSize = 0;
            HomeBTN.FlatStyle = FlatStyle.Flat;
            HomeBTN.ForeColor = Color.White;
            HomeBTN.Image = (Image)resources.GetObject("HomeBTN.Image");
            HomeBTN.Location = new Point(9, 10);
            HomeBTN.Name = "HomeBTN";
            HomeBTN.Size = new Size(70, 70);
            HomeBTN.TabIndex = 1;
            HomeBTN.TextColor = Color.White;
            HomeBTN.UseVisualStyleBackColor = false;
            HomeBTN.Click += HomeBTN_Click;
            // 
            // ContributorsPNLBack
            // 
            ContributorsPNLBack.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ContributorsPNLBack.BackColor = Color.Black;
            ContributorsPNLBack.Controls.Add(ContributorsPNLFront);
            ContributorsPNLBack.Location = new Point(120, 70);
            ContributorsPNLBack.Name = "ContributorsPNLBack";
            ContributorsPNLBack.Padding = new Padding(2);
            ContributorsPNLBack.Size = new Size(845, 55);
            ContributorsPNLBack.TabIndex = 3;
            // 
            // ContributorsPNLFront
            // 
            ContributorsPNLFront.BackColor = Color.FromArgb(45, 51, 59);
            ContributorsPNLFront.Controls.Add(QualityWEB);
            ContributorsPNLFront.Controls.Add(IssuesWEB);
            ContributorsPNLFront.Controls.Add(StarsWEB);
            ContributorsPNLFront.Controls.Add(ForkWEB);
            ContributorsPNLFront.Controls.Add(ContiWEB);
            ContributorsPNLFront.Dock = DockStyle.Fill;
            ContributorsPNLFront.Location = new Point(2, 2);
            ContributorsPNLFront.Name = "ContributorsPNLFront";
            ContributorsPNLFront.Size = new Size(841, 51);
            ContributorsPNLFront.TabIndex = 0;
            // 
            // QualityWEB
            // 
            QualityWEB.Anchor = AnchorStyles.Top;
            QualityWEB.BackColor = Color.FromArgb(45, 51, 59);
            QualityWEB.Location = new Point(618, 11);
            QualityWEB.Name = "QualityWEB";
            QualityWEB.Size = new Size(147, 28);
            QualityWEB.TabIndex = 7;
            QualityWEB.Url = "";
            // 
            // IssuesWEB
            // 
            IssuesWEB.Anchor = AnchorStyles.Top;
            IssuesWEB.BackColor = Color.FromArgb(45, 51, 59);
            IssuesWEB.Location = new Point(465, 11);
            IssuesWEB.Name = "IssuesWEB";
            IssuesWEB.Size = new Size(142, 28);
            IssuesWEB.TabIndex = 6;
            IssuesWEB.Url = "";
            // 
            // StarsWEB
            // 
            StarsWEB.Anchor = AnchorStyles.Top;
            StarsWEB.BackColor = Color.FromArgb(45, 51, 59);
            StarsWEB.Location = new Point(350, 11);
            StarsWEB.Name = "StarsWEB";
            StarsWEB.Size = new Size(104, 28);
            StarsWEB.TabIndex = 5;
            StarsWEB.Url = "";
            // 
            // ForkWEB
            // 
            ForkWEB.Anchor = AnchorStyles.Top;
            ForkWEB.Location = new Point(234, 11);
            ForkWEB.Name = "ForkWEB";
            ForkWEB.Size = new Size(105, 28);
            ForkWEB.TabIndex = 5;
            ForkWEB.Url = "";
            // 
            // ContiWEB
            // 
            ContiWEB.Anchor = AnchorStyles.Top;
            ContiWEB.Location = new Point(71, 11);
            ContiWEB.Name = "ContiWEB";
            ContiWEB.Size = new Size(152, 28);
            ContiWEB.TabIndex = 4;
            ContiWEB.Url = "";
            // 
            // PNLControl
            // 
            PNLControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PNLControl.BackColor = Color.Black;
            PNLControl.Location = new Point(120, 138);
            PNLControl.MaximumSize = new Size(0, 370);
            PNLControl.MinimumSize = new Size(845, 370);
            PNLControl.Name = "PNLControl";
            PNLControl.Padding = new Padding(2);
            PNLControl.Size = new Size(845, 370);
            PNLControl.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = Color.Black;
            panel3.Controls.Add(panel4);
            panel3.Location = new Point(121, 522);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(2);
            panel3.Size = new Size(845, 55);
            panel3.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(45, 51, 59);
            panel4.Controls.Add(BTNStartMySQL);
            panel4.Controls.Add(BTNStartAll);
            panel4.Controls.Add(BTNStartLOgin);
            panel4.Controls.Add(BTNStartWorld);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(2, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(841, 51);
            panel4.TabIndex = 0;
            // 
            // BTNStartMySQL
            // 
            BTNStartMySQL.Anchor = AnchorStyles.Top;
            BTNStartMySQL.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartMySQL.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartMySQL.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartMySQL.BorderRadius = 0;
            BTNStartMySQL.BorderSize = 1;
            BTNStartMySQL.FlatAppearance.BorderSize = 0;
            BTNStartMySQL.FlatStyle = FlatStyle.Flat;
            BTNStartMySQL.ForeColor = Color.White;
            BTNStartMySQL.Image = (Image)resources.GetObject("BTNStartMySQL.Image");
            BTNStartMySQL.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartMySQL.Location = new Point(423, 7);
            BTNStartMySQL.Name = "BTNStartMySQL";
            BTNStartMySQL.RightToLeft = RightToLeft.No;
            BTNStartMySQL.Size = new Size(119, 40);
            BTNStartMySQL.TabIndex = 8;
            BTNStartMySQL.Text = "           Start  Mysql";
            BTNStartMySQL.TextColor = Color.White;
            BTNStartMySQL.UseVisualStyleBackColor = false;
            // 
            // BTNStartAll
            // 
            BTNStartAll.Anchor = AnchorStyles.Top;
            BTNStartAll.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartAll.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartAll.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartAll.BorderRadius = 0;
            BTNStartAll.BorderSize = 1;
            BTNStartAll.FlatAppearance.BorderSize = 0;
            BTNStartAll.FlatStyle = FlatStyle.Flat;
            BTNStartAll.ForeColor = Color.White;
            BTNStartAll.Image = (Image)resources.GetObject("BTNStartAll.Image");
            BTNStartAll.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartAll.Location = new Point(548, 7);
            BTNStartAll.Name = "BTNStartAll";
            BTNStartAll.RightToLeft = RightToLeft.No;
            BTNStartAll.Size = new Size(119, 40);
            BTNStartAll.TabIndex = 9;
            BTNStartAll.Text = "           Start All";
            BTNStartAll.TextColor = Color.White;
            BTNStartAll.UseVisualStyleBackColor = false;
            // 
            // BTNStartLOgin
            // 
            BTNStartLOgin.Anchor = AnchorStyles.Top;
            BTNStartLOgin.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartLOgin.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartLOgin.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartLOgin.BorderRadius = 0;
            BTNStartLOgin.BorderSize = 1;
            BTNStartLOgin.FlatAppearance.BorderSize = 0;
            BTNStartLOgin.FlatStyle = FlatStyle.Flat;
            BTNStartLOgin.ForeColor = Color.White;
            BTNStartLOgin.Image = (Image)resources.GetObject("BTNStartLOgin.Image");
            BTNStartLOgin.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartLOgin.Location = new Point(173, 7);
            BTNStartLOgin.Name = "BTNStartLOgin";
            BTNStartLOgin.RightToLeft = RightToLeft.No;
            BTNStartLOgin.Size = new Size(119, 40);
            BTNStartLOgin.TabIndex = 7;
            BTNStartLOgin.Text = "          Start Login";
            BTNStartLOgin.TextColor = Color.White;
            BTNStartLOgin.UseVisualStyleBackColor = false;
            BTNStartLOgin.Click += BTNStartLOgin_Click;
            // 
            // BTNStartWorld
            // 
            BTNStartWorld.Anchor = AnchorStyles.Top;
            BTNStartWorld.BackColor = Color.FromArgb(28, 33, 40);
            BTNStartWorld.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNStartWorld.BorderColor = Color.FromArgb(0, 174, 219);
            BTNStartWorld.BorderRadius = 0;
            BTNStartWorld.BorderSize = 1;
            BTNStartWorld.FlatAppearance.BorderSize = 0;
            BTNStartWorld.FlatStyle = FlatStyle.Flat;
            BTNStartWorld.ForeColor = Color.White;
            BTNStartWorld.Image = (Image)resources.GetObject("BTNStartWorld.Image");
            BTNStartWorld.ImageAlign = ContentAlignment.MiddleLeft;
            BTNStartWorld.Location = new Point(298, 7);
            BTNStartWorld.Name = "BTNStartWorld";
            BTNStartWorld.RightToLeft = RightToLeft.No;
            BTNStartWorld.Size = new Size(119, 40);
            BTNStartWorld.TabIndex = 6;
            BTNStartWorld.Text = "           Start  World";
            BTNStartWorld.TextColor = Color.White;
            BTNStartWorld.UseVisualStyleBackColor = false;
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 33, 40);
            ClientSize = new Size(1000, 600);
            Controls.Add(panel3);
            Controls.Add(PNLControl);
            Controls.Add(ContributorsPNLBack);
            Controls.Add(panel1);
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Location = new Point(0, 0);
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Text = "Trion Contro Panel";
            TextAlign = MetroFramework.Forms.TextAlign.Center;
            Theme = MetroFramework.MetroThemeStyle.Dark;
            TransparencyKey = Color.Magenta;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ContributorsPNLBack.ResumeLayout(false);
            ContributorsPNLFront.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private UI.Controls.CustomButton HomeBTN;
        private UI.Controls.CustomButton SettingsBTN;
        private UI.Controls.CustomButton TerminaBTN;
        private Panel ContributorsPNLBack;
        private Panel ContributorsPNLFront;
        private UI.CustomWebBrowser ContiWEB;
        private UI.CustomWebBrowser QualityWEB;
        private UI.CustomWebBrowser IssuesWEB;
        private UI.CustomWebBrowser StarsWEB;
        private UI.CustomWebBrowser ForkWEB;
        private Label LblVersion;
        private Panel PNLControl;
        private Panel panel3;
        private Panel panel4;
        private UI.Controls.CustomButton BTNStartWorld;
        private UI.Controls.CustomButton BTNStartAll;
        private UI.Controls.CustomButton BTNStartMySQL;
        private UI.Controls.CustomButton BTNStartLOgin;
        private System.Windows.Forms.Timer TimerWacher;
    }
}