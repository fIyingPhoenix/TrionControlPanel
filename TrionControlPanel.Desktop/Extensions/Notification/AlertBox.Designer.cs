namespace TrionControlPanel.Desktop.Extensions.Notification
{
    partial class AlertBox
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertBox));
            TimerAnimation = new System.Windows.Forms.Timer(components);
            PboxIcon = new PictureBox();
            LBLMessage = new Label();
            NotificationIconsList = new ImageList(components);
            ((System.ComponentModel.ISupportInitialize)PboxIcon).BeginInit();
            SuspendLayout();
            // 
            // TimerAnimation
            // 
            TimerAnimation.Enabled = true;
            TimerAnimation.Interval = 1;
            TimerAnimation.Tick += TimerAnimation_Tick;
            // 
            // PboxIcon
            // 
            PboxIcon.BackColor = Color.Transparent;
            PboxIcon.Location = new Point(7, 71);
            PboxIcon.Name = "PboxIcon";
            PboxIcon.Size = new Size(70, 70);
            PboxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PboxIcon.TabIndex = 2;
            PboxIcon.TabStop = false;
            // 
            // LBLMessage
            // 
            LBLMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LBLMessage.BackColor = Color.Transparent;
            LBLMessage.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LBLMessage.ForeColor = Color.White;
            LBLMessage.Location = new Point(100, 71);
            LBLMessage.Name = "LBLMessage";
            LBLMessage.Size = new Size(294, 70);
            LBLMessage.TabIndex = 3;
            LBLMessage.Text = "label1";
            // 
            // NotificationIconsList
            // 
            NotificationIconsList.ColorDepth = ColorDepth.Depth32Bit;
            NotificationIconsList.ImageStream = (ImageListStreamer)resources.GetObject("NotificationIconsList.ImageStream");
            NotificationIconsList.TransparentColor = Color.Transparent;
            NotificationIconsList.Images.SetKeyName(0, "error.png");
            NotificationIconsList.Images.SetKeyName(1, "info.png");
            NotificationIconsList.Images.SetKeyName(2, "success.png");
            NotificationIconsList.Images.SetKeyName(3, "warning.png");
            // 
            // AlertBox
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(400, 150);
            Controls.Add(LBLMessage);
            Controls.Add(PboxIcon);
            MaximizeBox = false;
            MaximumSize = new Size(400, 150);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            MinimumSize = new Size(400, 150);
            Name = "AlertBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "AlertBox";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PboxIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer TimerAnimation;
        private PictureBox PboxIcon;
        private Label LBLMessage;
        private ImageList NotificationIconsList;
    }
}