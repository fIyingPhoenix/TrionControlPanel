namespace TrionControlPanelDesktop.Controls
{
    partial class DownloadControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadControl));
            PBARDownload = new TrionControlPanel.UI.CustomProgressBar();
            PNLDownloadStatus = new MetroFramework.Controls.MetroPanel();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            LBLQueue = new Label();
            LBLDownloadName = new Label();
            LBLDownloadSpeed = new Label();
            LBLDownloadSize = new Label();
            TimerWacher = new System.Windows.Forms.Timer(components);
            TimerDownloadStart = new System.Windows.Forms.Timer(components);
            LBLTitle = new Label();
            metroPanel1 = new MetroFramework.Controls.MetroPanel();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            PBoxWebServer = new PictureBox();
            PBoxAPIServer = new PictureBox();
            PBoxBackupCDN = new PictureBox();
            PBoxMainCDN = new PictureBox();
            PNLDownloadStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            metroPanel1.SuspendLayout();
            metroPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PBoxWebServer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PBoxAPIServer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PBoxBackupCDN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PBoxMainCDN).BeginInit();
            SuspendLayout();
            // 
            // PBARDownload
            // 
            PBARDownload.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PBARDownload.BackColor = Color.FromArgb(34, 39, 46);
            PBARDownload.BarColor = Color.FromArgb(0, 174, 219);
            PBARDownload.FontSize = 10;
            PBARDownload.ForeColor = Color.FromArgb(0, 174, 219);
            PBARDownload.LabelText = "MB";
            PBARDownload.Location = new Point(19, 327);
            PBARDownload.MaximumValue = true;
            PBARDownload.Name = "PBARDownload";
            PBARDownload.ShowStatus = true;
            PBARDownload.Size = new Size(808, 30);
            PBARDownload.TabIndex = 29;
            PBARDownload.TextColor = Color.White;
            // 
            // PNLDownloadStatus
            // 
            PNLDownloadStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PNLDownloadStatus.BackColor = Color.FromArgb(34, 39, 46);
            PNLDownloadStatus.Border = true;
            PNLDownloadStatus.BorderColor = Color.Black;
            PNLDownloadStatus.BorderSize = 1;
            PNLDownloadStatus.Controls.Add(pictureBox4);
            PNLDownloadStatus.Controls.Add(pictureBox3);
            PNLDownloadStatus.Controls.Add(pictureBox2);
            PNLDownloadStatus.Controls.Add(pictureBox1);
            PNLDownloadStatus.Controls.Add(LBLQueue);
            PNLDownloadStatus.Controls.Add(LBLDownloadName);
            PNLDownloadStatus.Controls.Add(LBLDownloadSpeed);
            PNLDownloadStatus.Controls.Add(LBLDownloadSize);
            PNLDownloadStatus.CustomBackground = true;
            PNLDownloadStatus.HorizontalScrollbar = true;
            PNLDownloadStatus.HorizontalScrollbarBarColor = true;
            PNLDownloadStatus.HorizontalScrollbarHighlightOnWheel = false;
            PNLDownloadStatus.HorizontalScrollbarSize = 10;
            PNLDownloadStatus.Location = new Point(19, 104);
            PNLDownloadStatus.Name = "PNLDownloadStatus";
            PNLDownloadStatus.Padding = new Padding(2);
            PNLDownloadStatus.Size = new Size(392, 185);
            PNLDownloadStatus.Style = MetroFramework.MetroColorStyle.Blue;
            PNLDownloadStatus.StyleManager = null;
            PNLDownloadStatus.TabIndex = 30;
            PNLDownloadStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLDownloadStatus.VerticalScrollbar = true;
            PNLDownloadStatus.VerticalScrollbarBarColor = true;
            PNLDownloadStatus.VerticalScrollbarHighlightOnWheel = false;
            PNLDownloadStatus.VerticalScrollbarSize = 10;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(15, 97);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(35, 35);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 39;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(15, 138);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(35, 35);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 38;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 56);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 35);
            pictureBox2.TabIndex = 37;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(15, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.TabIndex = 36;
            pictureBox1.TabStop = false;
            // 
            // LBLQueue
            // 
            LBLQueue.AutoSize = true;
            LBLQueue.Font = new Font("Segoe UI", 12F);
            LBLQueue.ForeColor = Color.White;
            LBLQueue.Location = new Point(53, 145);
            LBLQueue.Name = "LBLQueue";
            LBLQueue.Size = new Size(99, 21);
            LBLQueue.TabIndex = 35;
            LBLQueue.Text = "Queue:  0 / 0";
            // 
            // LBLDownloadName
            // 
            LBLDownloadName.AutoSize = true;
            LBLDownloadName.Font = new Font("Segoe UI", 12F);
            LBLDownloadName.ForeColor = Color.White;
            LBLDownloadName.Location = new Point(53, 104);
            LBLDownloadName.Name = "LBLDownloadName";
            LBLDownloadName.Size = new Size(116, 21);
            LBLDownloadName.TabIndex = 34;
            LBLDownloadName.Text = "Task:  FileName";
            // 
            // LBLDownloadSpeed
            // 
            LBLDownloadSpeed.AutoSize = true;
            LBLDownloadSpeed.Font = new Font("Segoe UI", 12F);
            LBLDownloadSpeed.ForeColor = Color.White;
            LBLDownloadSpeed.Location = new Point(53, 63);
            LBLDownloadSpeed.Name = "LBLDownloadSpeed";
            LBLDownloadSpeed.Size = new Size(96, 21);
            LBLDownloadSpeed.TabIndex = 33;
            LBLDownloadSpeed.Text = "Speed: Mb/s";
            // 
            // LBLDownloadSize
            // 
            LBLDownloadSize.AutoSize = true;
            LBLDownloadSize.Font = new Font("Segoe UI", 12F);
            LBLDownloadSize.ForeColor = Color.White;
            LBLDownloadSize.Location = new Point(53, 22);
            LBLDownloadSize.Name = "LBLDownloadSize";
            LBLDownloadSize.Size = new Size(68, 21);
            LBLDownloadSize.TabIndex = 32;
            LBLDownloadSize.Text = "Size: MB";
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1800000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // TimerDownloadStart
            // 
            TimerDownloadStart.Enabled = true;
            TimerDownloadStart.Interval = 10;
            TimerDownloadStart.Tick += TimerDownloadStart_Tick;
            // 
            // LBLTitle
            // 
            LBLTitle.BackColor = Color.FromArgb(34, 39, 46);
            LBLTitle.Dock = DockStyle.Fill;
            LBLTitle.Font = new Font("Segoe UI", 12F);
            LBLTitle.ForeColor = Color.White;
            LBLTitle.Location = new Point(2, 2);
            LBLTitle.Name = "LBLTitle";
            LBLTitle.Size = new Size(796, 58);
            LBLTitle.TabIndex = 37;
            LBLTitle.Text = "Title";
            LBLTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroPanel1
            // 
            metroPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroPanel1.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(LBLTitle);
            metroPanel1.CustomBackground = true;
            metroPanel1.HorizontalScrollbar = true;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(19, 14);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Padding = new Padding(2);
            metroPanel1.Size = new Size(800, 62);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 38;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = true;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            metroPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            metroPanel2.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(label4);
            metroPanel2.Controls.Add(label3);
            metroPanel2.Controls.Add(label2);
            metroPanel2.Controls.Add(label1);
            metroPanel2.Controls.Add(PBoxWebServer);
            metroPanel2.Controls.Add(PBoxAPIServer);
            metroPanel2.Controls.Add(PBoxBackupCDN);
            metroPanel2.Controls.Add(PBoxMainCDN);
            metroPanel2.CustomBackground = true;
            metroPanel2.HorizontalScrollbar = true;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(427, 104);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(392, 185);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 39;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = true;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(56, 145);
            label4.Name = "label4";
            label4.Size = new Size(129, 21);
            label4.TabIndex = 47;
            label4.Text = "Main Web Server";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(56, 104);
            label3.Name = "label3";
            label3.Size = new Size(82, 21);
            label3.TabIndex = 46;
            label3.Text = "API Server";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(56, 63);
            label2.Name = "label2";
            label2.Size = new Size(244, 21);
            label2.TabIndex = 45;
            label2.Text = "Backup Content Delivery Network";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(56, 22);
            label1.Name = "label1";
            label1.Size = new Size(229, 21);
            label1.TabIndex = 44;
            label1.Text = "Main Content Delivery Network";
            // 
            // PBoxWebServer
            // 
            PBoxWebServer.Image = Properties.Resources.cloud_offline_50;
            PBoxWebServer.InitialImage = Properties.Resources.cloud_offline_50;
            PBoxWebServer.Location = new Point(15, 138);
            PBoxWebServer.Name = "PBoxWebServer";
            PBoxWebServer.Size = new Size(35, 35);
            PBoxWebServer.SizeMode = PictureBoxSizeMode.StretchImage;
            PBoxWebServer.TabIndex = 43;
            PBoxWebServer.TabStop = false;
            // 
            // PBoxAPIServer
            // 
            PBoxAPIServer.Image = Properties.Resources.cloud_offline_50;
            PBoxAPIServer.InitialImage = Properties.Resources.cloud_offline_50;
            PBoxAPIServer.Location = new Point(15, 97);
            PBoxAPIServer.Name = "PBoxAPIServer";
            PBoxAPIServer.Size = new Size(35, 35);
            PBoxAPIServer.SizeMode = PictureBoxSizeMode.StretchImage;
            PBoxAPIServer.TabIndex = 42;
            PBoxAPIServer.TabStop = false;
            // 
            // PBoxBackupCDN
            // 
            PBoxBackupCDN.Image = Properties.Resources.cloud_offline_50;
            PBoxBackupCDN.InitialImage = Properties.Resources.cloud_offline_50;
            PBoxBackupCDN.Location = new Point(15, 56);
            PBoxBackupCDN.Name = "PBoxBackupCDN";
            PBoxBackupCDN.Size = new Size(35, 35);
            PBoxBackupCDN.SizeMode = PictureBoxSizeMode.StretchImage;
            PBoxBackupCDN.TabIndex = 41;
            PBoxBackupCDN.TabStop = false;
            // 
            // PBoxMainCDN
            // 
            PBoxMainCDN.Image = Properties.Resources.cloud_offline_50;
            PBoxMainCDN.Location = new Point(15, 15);
            PBoxMainCDN.Name = "PBoxMainCDN";
            PBoxMainCDN.Size = new Size(35, 35);
            PBoxMainCDN.SizeMode = PictureBoxSizeMode.StretchImage;
            PBoxMainCDN.TabIndex = 40;
            PBoxMainCDN.TabStop = false;
            // 
            // DownloadControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(metroPanel2);
            Controls.Add(metroPanel1);
            Controls.Add(PNLDownloadStatus);
            Controls.Add(PBARDownload);
            Name = "DownloadControl";
            Size = new Size(845, 370);
            Load += DownloadControl_Load;
            PNLDownloadStatus.ResumeLayout(false);
            PNLDownloadStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            metroPanel1.ResumeLayout(false);
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PBoxWebServer).EndInit();
            ((System.ComponentModel.ISupportInitialize)PBoxAPIServer).EndInit();
            ((System.ComponentModel.ISupportInitialize)PBoxBackupCDN).EndInit();
            ((System.ComponentModel.ISupportInitialize)PBoxMainCDN).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TrionControlPanel.UI.CustomProgressBar PBARDownload;
        private MetroFramework.Controls.MetroPanel PNLDownloadStatus;
        private Label LBLDownloadSize;
        private Label LBLDownloadSpeed;
        private Label LBLDownloadName;
        private System.Windows.Forms.Timer TimerWacher;
        private System.Windows.Forms.Timer TimerDownloadStart;
        private Label LBLTitle;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private Label LBLQueue;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox PBoxWebServer;
        private PictureBox PBoxAPIServer;
        private PictureBox PBoxBackupCDN;
        private PictureBox PBoxMainCDN;
        private Label label2;
        private Label label1;
        private Label label4;
        private Label label3;
    }
}
