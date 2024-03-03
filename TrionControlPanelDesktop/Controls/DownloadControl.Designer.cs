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
            PBARDownload = new TrionControlPanel.UI.CustomProgressBar();
            PNLDownloadStatus = new MetroFramework.Controls.MetroPanel();
            LBLQueue = new Label();
            LBLDownloadName = new Label();
            LBLDownloadSpeed = new Label();
            LBLDownloadSize = new Label();
            TimerWacher = new System.Windows.Forms.Timer(components);
            BTNTestDownload = new Button();
            PNLDownloadStatus.SuspendLayout();
            SuspendLayout();
            // 
            // PBARDownload
            // 
            PBARDownload.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PBARDownload.BackColor = Color.FromArgb(28, 33, 40);
            PBARDownload.BarColor = Color.FromArgb(0, 174, 219);
            PBARDownload.FontSize = 10;
            PBARDownload.ForeColor = Color.FromArgb(0, 174, 219);
            PBARDownload.LabelText = "MB";
            PBARDownload.Location = new Point(19, 315);
            PBARDownload.MaximumValue = true;
            PBARDownload.Name = "PBARDownload";
            PBARDownload.ShowStatus = true;
            PBARDownload.Size = new Size(808, 35);
            PBARDownload.TabIndex = 29;
            PBARDownload.TextColor = Color.White;
            // 
            // PNLDownloadStatus
            // 
            PNLDownloadStatus.BackColor = Color.FromArgb(34, 39, 46);
            PNLDownloadStatus.Border = true;
            PNLDownloadStatus.BorderColor = Color.Black;
            PNLDownloadStatus.BorderSize = 1;
            PNLDownloadStatus.Controls.Add(LBLQueue);
            PNLDownloadStatus.Controls.Add(LBLDownloadName);
            PNLDownloadStatus.Controls.Add(LBLDownloadSpeed);
            PNLDownloadStatus.Controls.Add(LBLDownloadSize);
            PNLDownloadStatus.CustomBackground = true;
            PNLDownloadStatus.HorizontalScrollbar = true;
            PNLDownloadStatus.HorizontalScrollbarBarColor = true;
            PNLDownloadStatus.HorizontalScrollbarHighlightOnWheel = false;
            PNLDownloadStatus.HorizontalScrollbarSize = 10;
            PNLDownloadStatus.Location = new Point(19, 49);
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
            // LBLQueue
            // 
            LBLQueue.AutoSize = true;
            LBLQueue.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLQueue.ForeColor = Color.White;
            LBLQueue.Location = new Point(28, 134);
            LBLQueue.Name = "LBLQueue";
            LBLQueue.Size = new Size(99, 21);
            LBLQueue.TabIndex = 35;
            LBLQueue.Text = "Queue:  0 / 0";
            // 
            // LBLDownloadName
            // 
            LBLDownloadName.AutoSize = true;
            LBLDownloadName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLDownloadName.ForeColor = Color.White;
            LBLDownloadName.Location = new Point(28, 108);
            LBLDownloadName.Name = "LBLDownloadName";
            LBLDownloadName.Size = new Size(129, 21);
            LBLDownloadName.TabIndex = 34;
            LBLDownloadName.Text = "Name:  FileName";
            // 
            // LBLDownloadSpeed
            // 
            LBLDownloadSpeed.AutoSize = true;
            LBLDownloadSpeed.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLDownloadSpeed.ForeColor = Color.White;
            LBLDownloadSpeed.Location = new Point(28, 45);
            LBLDownloadSpeed.Name = "LBLDownloadSpeed";
            LBLDownloadSpeed.Size = new Size(171, 21);
            LBLDownloadSpeed.TabIndex = 33;
            LBLDownloadSpeed.Text = "Download Speed: Mb/s";
            // 
            // LBLDownloadSize
            // 
            LBLDownloadSize.AutoSize = true;
            LBLDownloadSize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLDownloadSize.ForeColor = Color.White;
            LBLDownloadSize.Location = new Point(28, 24);
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
            // BTNTestDownload
            // 
            BTNTestDownload.Location = new Point(636, 49);
            BTNTestDownload.Name = "BTNTestDownload";
            BTNTestDownload.Size = new Size(191, 23);
            BTNTestDownload.TabIndex = 31;
            BTNTestDownload.Text = "Test Download";
            BTNTestDownload.UseVisualStyleBackColor = true;
            BTNTestDownload.Click += BTNTestDownload_ClickAsync;
            // 
            // DownloadControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(BTNTestDownload);
            Controls.Add(PNLDownloadStatus);
            Controls.Add(PBARDownload);
            Name = "DownloadControl";
            Size = new Size(845, 370);
            Load += DownloadControl_Load;
            PNLDownloadStatus.ResumeLayout(false);
            PNLDownloadStatus.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TrionControlPanel.UI.CustomProgressBar PBARDownload;
        private MetroFramework.Controls.MetroPanel PNLDownloadStatus;
        private Label LBLDownloadSize;
        private Label LBLDownloadSpeed;
        private Label LBLQueue;
        private Label LBLDownloadName;
        private System.Windows.Forms.Timer TimerWacher;
        private Button BTNTestDownload;
    }
}
