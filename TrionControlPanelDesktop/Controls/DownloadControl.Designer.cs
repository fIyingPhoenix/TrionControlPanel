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
            LBLStatus = new Label();
            LBLQueue = new Label();
            LBLDownloadName = new Label();
            LBLDownloadSpeed = new Label();
            LBLDownloadSize = new Label();
            TimerWacher = new System.Windows.Forms.Timer(components);
            LBLFIleName = new Label();
            TimerDownloadStart = new System.Windows.Forms.Timer(components);
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
            PNLDownloadStatus.Controls.Add(LBLStatus);
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
            // LBLStatus
            // 
            LBLStatus.AutoSize = true;
            LBLStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLStatus.ForeColor = Color.White;
            LBLStatus.Location = new Point(20, 149);
            LBLStatus.Name = "LBLStatus";
            LBLStatus.Size = new Size(55, 21);
            LBLStatus.TabIndex = 35;
            LBLStatus.Text = "Status:";
            // 
            // LBLQueue
            // 
            LBLQueue.AutoSize = true;
            LBLQueue.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLQueue.ForeColor = Color.White;
            LBLQueue.Location = new Point(20, 123);
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
            LBLDownloadName.Location = new Point(20, 97);
            LBLDownloadName.Name = "LBLDownloadName";
            LBLDownloadName.Size = new Size(116, 21);
            LBLDownloadName.TabIndex = 34;
            LBLDownloadName.Text = "Task:  FileName";
            // 
            // LBLDownloadSpeed
            // 
            LBLDownloadSpeed.AutoSize = true;
            LBLDownloadSpeed.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLDownloadSpeed.ForeColor = Color.White;
            LBLDownloadSpeed.Location = new Point(20, 40);
            LBLDownloadSpeed.Name = "LBLDownloadSpeed";
            LBLDownloadSpeed.Size = new Size(96, 21);
            LBLDownloadSpeed.TabIndex = 33;
            LBLDownloadSpeed.Text = "Speed: Mb/s";
            // 
            // LBLDownloadSize
            // 
            LBLDownloadSize.AutoSize = true;
            LBLDownloadSize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLDownloadSize.ForeColor = Color.White;
            LBLDownloadSize.Location = new Point(20, 19);
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
            // LBLFIleName
            // 
            LBLFIleName.AutoSize = true;
            LBLFIleName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            LBLFIleName.ForeColor = Color.White;
            LBLFIleName.Location = new Point(19, 291);
            LBLFIleName.Name = "LBLFIleName";
            LBLFIleName.Size = new Size(37, 21);
            LBLFIleName.TabIndex = 36;
            LBLFIleName.Text = "File:";
            // 
            // TimerDownloadStart
            // 
            TimerDownloadStart.Interval = 1000;
            TimerDownloadStart.Tick += TimerDownloadStart_Tick;
            // 
            // DownloadControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(LBLFIleName);
            Controls.Add(PNLDownloadStatus);
            Controls.Add(PBARDownload);
            Name = "DownloadControl";
            Size = new Size(845, 370);
            Load += DownloadControl_Load;
            PNLDownloadStatus.ResumeLayout(false);
            PNLDownloadStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrionControlPanel.UI.CustomProgressBar PBARDownload;
        private MetroFramework.Controls.MetroPanel PNLDownloadStatus;
        private Label LBLDownloadSize;
        private Label LBLDownloadSpeed;
        private Label LBLQueue;
        private Label LBLDownloadName;
        private System.Windows.Forms.Timer TimerWacher;
        private Label LBLStatus;
        private Label LBLFIleName;
        private System.Windows.Forms.Timer TimerDownloadStart;
    }
}
