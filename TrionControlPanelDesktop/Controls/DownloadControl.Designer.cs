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
            pictureBox8 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox5 = new PictureBox();
            PNLDownloadStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            metroPanel1.SuspendLayout();
            metroPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // PBARDownload
            // 
            PBARDownload.Anchor = AnchorStyles.Left | AnchorStyles.Right;
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
            PNLDownloadStatus.Size = new Size(348, 186);
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
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // TimerDownloadStart
            // 
            TimerDownloadStart.Interval = 1000;
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
            metroPanel2.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(pictureBox8);
            metroPanel2.Controls.Add(pictureBox7);
            metroPanel2.Controls.Add(pictureBox6);
            metroPanel2.Controls.Add(pictureBox5);
            metroPanel2.CustomBackground = true;
            metroPanel2.HorizontalScrollbar = true;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(469, 104);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(348, 186);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 39;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = true;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // pictureBox8
            // 
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(15, 138);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(35, 35);
            pictureBox8.TabIndex = 43;
            pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(15, 97);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(35, 35);
            pictureBox7.TabIndex = 42;
            pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(15, 56);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(35, 35);
            pictureBox6.TabIndex = 41;
            pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(15, 15);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(35, 35);
            pictureBox5.TabIndex = 40;
            pictureBox5.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
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
        private PictureBox pictureBox8;
        private PictureBox pictureBox7;
        private PictureBox pictureBox6;
        private PictureBox pictureBox5;
    }
}
