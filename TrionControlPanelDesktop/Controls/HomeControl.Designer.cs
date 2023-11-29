using MetroFramework.Controls;
using TrionControlPanel.UI;

namespace TrionControlPanelDesktop.Controls
{
    partial class HomeControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeControl));
            TimerWacher = new System.Windows.Forms.Timer(components);
            PNLLayoutBot = new TableLayoutPanel();
            metroPanel1 = new MetroPanel();
            label4 = new Label();
            PCLoginPbarCPU = new CustomProgressBar();
            PCLoginPbarRAM = new CustomProgressBar();
            label5 = new Label();
            label6 = new Label();
            metroPanel2 = new MetroPanel();
            label7 = new Label();
            PCWorldPbarCPU = new CustomProgressBar();
            label9 = new Label();
            PCWorldPbarRAM = new CustomProgressBar();
            label8 = new Label();
            PNLLayoutTop = new TableLayoutPanel();
            PNLServerStatus = new MetroPanel();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            PICMySqlServerStatus = new PictureBox();
            PicWorldServerStatus = new PictureBox();
            PICLoginServerStatus = new PictureBox();
            PNLPCResorce = new MetroPanel();
            label1 = new Label();
            PCResorcePbarCPU = new CustomProgressBar();
            PCResorcePbarRAM = new CustomProgressBar();
            label2 = new Label();
            label3 = new Label();
            PNLLayoutBot.SuspendLayout();
            metroPanel1.SuspendLayout();
            metroPanel2.SuspendLayout();
            PNLLayoutTop.SuspendLayout();
            PNLServerStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PicWorldServerStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PICLoginServerStatus).BeginInit();
            PNLPCResorce.SuspendLayout();
            SuspendLayout();
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // PNLLayoutBot
            // 
            PNLLayoutBot.ColumnCount = 2;
            PNLLayoutBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutBot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutBot.Controls.Add(metroPanel1, 1, 0);
            PNLLayoutBot.Controls.Add(metroPanel2, 0, 0);
            PNLLayoutBot.Dock = DockStyle.Bottom;
            PNLLayoutBot.Location = new Point(0, 186);
            PNLLayoutBot.Margin = new Padding(0);
            PNLLayoutBot.Name = "PNLLayoutBot";
            PNLLayoutBot.Padding = new Padding(2);
            PNLLayoutBot.RowCount = 1;
            PNLLayoutBot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            PNLLayoutBot.Size = new Size(845, 184);
            PNLLayoutBot.TabIndex = 3;
            // 
            // metroPanel1
            // 
            metroPanel1.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(label4);
            metroPanel1.Controls.Add(PCLoginPbarCPU);
            metroPanel1.Controls.Add(PCLoginPbarRAM);
            metroPanel1.Controls.Add(label5);
            metroPanel1.Controls.Add(label6);
            metroPanel1.CustomBackground = true;
            metroPanel1.Dock = DockStyle.Fill;
            metroPanel1.HorizontalScrollbar = false;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(425, 5);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Size = new Size(415, 174);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 3;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(0, 174, 219);
            label4.Location = new Point(14, 18);
            label4.Name = "label4";
            label4.Size = new Size(175, 21);
            label4.TabIndex = 32;
            label4.Text = "Login Server Resource";
            // 
            // PCLoginPbarCPU
            // 
            PCLoginPbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCLoginPbarCPU.BackColor = Color.FromArgb(28, 33, 40);
            PCLoginPbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            PCLoginPbarCPU.FontSize = 10;
            PCLoginPbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            PCLoginPbarCPU.LabelText = "%";
            PCLoginPbarCPU.Location = new Point(13, 139);
            PCLoginPbarCPU.MaximumValue = false;
            PCLoginPbarCPU.Name = "PCLoginPbarCPU";
            PCLoginPbarCPU.ShowStatus = true;
            PCLoginPbarCPU.Size = new Size(390, 20);
            PCLoginPbarCPU.TabIndex = 36;
            PCLoginPbarCPU.TextColor = Color.White;
            // 
            // PCLoginPbarRAM
            // 
            PCLoginPbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCLoginPbarRAM.BackColor = Color.FromArgb(28, 33, 40);
            PCLoginPbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            PCLoginPbarRAM.FontSize = 10;
            PCLoginPbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            PCLoginPbarRAM.LabelText = "MB";
            PCLoginPbarRAM.Location = new Point(13, 87);
            PCLoginPbarRAM.MaximumValue = true;
            PCLoginPbarRAM.Name = "PCLoginPbarRAM";
            PCLoginPbarRAM.ShowStatus = true;
            PCLoginPbarRAM.Size = new Size(390, 20);
            PCLoginPbarRAM.TabIndex = 33;
            PCLoginPbarRAM.TextColor = Color.White;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(128, 120);
            label5.Name = "label5";
            label5.Size = new Size(164, 15);
            label5.TabIndex = 35;
            label5.Text = "Central Processing Unit (CPU)";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(120, 66);
            label6.Name = "label6";
            label6.Size = new Size(181, 15);
            label6.TabIndex = 34;
            label6.Text = "Random-Access Memory  (RAM)";
            // 
            // metroPanel2
            // 
            metroPanel2.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(label7);
            metroPanel2.Controls.Add(PCWorldPbarCPU);
            metroPanel2.Controls.Add(label9);
            metroPanel2.Controls.Add(PCWorldPbarRAM);
            metroPanel2.Controls.Add(label8);
            metroPanel2.CustomBackground = true;
            metroPanel2.Dock = DockStyle.Fill;
            metroPanel2.HorizontalScrollbar = false;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(5, 5);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Size = new Size(414, 174);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 4;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = false;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.FromArgb(0, 174, 219);
            label7.Location = new Point(14, 18);
            label7.Name = "label7";
            label7.Size = new Size(179, 21);
            label7.TabIndex = 37;
            label7.Text = "World Server Resource";
            // 
            // PCWorldPbarCPU
            // 
            PCWorldPbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCWorldPbarCPU.BackColor = Color.FromArgb(28, 33, 40);
            PCWorldPbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            PCWorldPbarCPU.FontSize = 10;
            PCWorldPbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            PCWorldPbarCPU.LabelText = "%";
            PCWorldPbarCPU.Location = new Point(13, 139);
            PCWorldPbarCPU.MaximumValue = false;
            PCWorldPbarCPU.Name = "PCWorldPbarCPU";
            PCWorldPbarCPU.ShowStatus = true;
            PCWorldPbarCPU.Size = new Size(390, 20);
            PCWorldPbarCPU.TabIndex = 41;
            PCWorldPbarCPU.TextColor = Color.White;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top;
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(120, 66);
            label9.Name = "label9";
            label9.Size = new Size(181, 15);
            label9.TabIndex = 39;
            label9.Text = "Random-Access Memory  (RAM)";
            // 
            // PCWorldPbarRAM
            // 
            PCWorldPbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCWorldPbarRAM.BackColor = Color.FromArgb(28, 33, 40);
            PCWorldPbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            PCWorldPbarRAM.FontSize = 10;
            PCWorldPbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            PCWorldPbarRAM.LabelText = "MB";
            PCWorldPbarRAM.Location = new Point(13, 87);
            PCWorldPbarRAM.MaximumValue = true;
            PCWorldPbarRAM.Name = "PCWorldPbarRAM";
            PCWorldPbarRAM.ShowStatus = true;
            PCWorldPbarRAM.Size = new Size(390, 20);
            PCWorldPbarRAM.TabIndex = 38;
            PCWorldPbarRAM.TextColor = Color.White;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(128, 120);
            label8.Name = "label8";
            label8.Size = new Size(164, 15);
            label8.TabIndex = 40;
            label8.Text = "Central Processing Unit (CPU)";
            // 
            // PNLLayoutTop
            // 
            PNLLayoutTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PNLLayoutTop.ColumnCount = 2;
            PNLLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            PNLLayoutTop.Controls.Add(PNLServerStatus, 0, 0);
            PNLLayoutTop.Controls.Add(PNLPCResorce, 1, 0);
            PNLLayoutTop.Location = new Point(0, 0);
            PNLLayoutTop.Margin = new Padding(0);
            PNLLayoutTop.Name = "PNLLayoutTop";
            PNLLayoutTop.Padding = new Padding(2);
            PNLLayoutTop.RowCount = 1;
            PNLLayoutTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            PNLLayoutTop.Size = new Size(845, 186);
            PNLLayoutTop.TabIndex = 2;
            // 
            // PNLServerStatus
            // 
            PNLServerStatus.BackColor = Color.FromArgb(34, 39, 46);
            PNLServerStatus.Border = true;
            PNLServerStatus.BorderColor = Color.Black;
            PNLServerStatus.BorderSize = 1;
            PNLServerStatus.Controls.Add(label12);
            PNLServerStatus.Controls.Add(label11);
            PNLServerStatus.Controls.Add(label10);
            PNLServerStatus.Controls.Add(PICMySqlServerStatus);
            PNLServerStatus.Controls.Add(PicWorldServerStatus);
            PNLServerStatus.Controls.Add(PICLoginServerStatus);
            PNLServerStatus.CustomBackground = true;
            PNLServerStatus.Dock = DockStyle.Fill;
            PNLServerStatus.HorizontalScrollbar = true;
            PNLServerStatus.HorizontalScrollbarBarColor = true;
            PNLServerStatus.HorizontalScrollbarHighlightOnWheel = false;
            PNLServerStatus.HorizontalScrollbarSize = 10;
            PNLServerStatus.Location = new Point(5, 5);
            PNLServerStatus.Name = "PNLServerStatus";
            PNLServerStatus.Padding = new Padding(2);
            PNLServerStatus.Size = new Size(414, 176);
            PNLServerStatus.Style = MetroFramework.MetroColorStyle.Blue;
            PNLServerStatus.StyleManager = null;
            PNLServerStatus.TabIndex = 0;
            PNLServerStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLServerStatus.VerticalScrollbar = true;
            PNLServerStatus.VerticalScrollbarBarColor = true;
            PNLServerStatus.VerticalScrollbarHighlightOnWheel = false;
            PNLServerStatus.VerticalScrollbarSize = 10;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ForeColor = Color.White;
            label12.Location = new Point(73, 115);
            label12.Name = "label12";
            label12.Size = new Size(147, 21);
            label12.TabIndex = 33;
            label12.Text = "World Server Status";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(73, 74);
            label11.Name = "label11";
            label11.Size = new Size(144, 21);
            label11.TabIndex = 32;
            label11.Text = "LogIn Server Status";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.ForeColor = Color.White;
            label10.Location = new Point(73, 33);
            label10.Name = "label10";
            label10.Size = new Size(156, 21);
            label10.TabIndex = 31;
            label10.Text = "MySQL Server Status";
            // 
            // PICMySqlServerStatus
            // 
            PICMySqlServerStatus.Image = (Image)resources.GetObject("PICMySqlServerStatus.Image");
            PICMySqlServerStatus.Location = new Point(32, 26);
            PICMySqlServerStatus.Name = "PICMySqlServerStatus";
            PICMySqlServerStatus.Size = new Size(35, 35);
            PICMySqlServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICMySqlServerStatus.TabIndex = 30;
            PICMySqlServerStatus.TabStop = false;
            // 
            // PicWorldServerStatus
            // 
            PicWorldServerStatus.Image = (Image)resources.GetObject("PicWorldServerStatus.Image");
            PicWorldServerStatus.Location = new Point(32, 108);
            PicWorldServerStatus.Name = "PicWorldServerStatus";
            PicWorldServerStatus.Size = new Size(35, 35);
            PicWorldServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PicWorldServerStatus.TabIndex = 29;
            PicWorldServerStatus.TabStop = false;
            // 
            // PICLoginServerStatus
            // 
            PICLoginServerStatus.Image = (Image)resources.GetObject("PICLoginServerStatus.Image");
            PICLoginServerStatus.Location = new Point(32, 67);
            PICLoginServerStatus.Name = "PICLoginServerStatus";
            PICLoginServerStatus.Size = new Size(35, 35);
            PICLoginServerStatus.SizeMode = PictureBoxSizeMode.StretchImage;
            PICLoginServerStatus.TabIndex = 28;
            PICLoginServerStatus.TabStop = false;
            // 
            // PNLPCResorce
            // 
            PNLPCResorce.BackColor = Color.FromArgb(34, 39, 46);
            PNLPCResorce.Border = true;
            PNLPCResorce.BorderColor = Color.Black;
            PNLPCResorce.BorderSize = 1;
            PNLPCResorce.Controls.Add(label1);
            PNLPCResorce.Controls.Add(PCResorcePbarCPU);
            PNLPCResorce.Controls.Add(PCResorcePbarRAM);
            PNLPCResorce.Controls.Add(label2);
            PNLPCResorce.Controls.Add(label3);
            PNLPCResorce.CustomBackground = true;
            PNLPCResorce.Dock = DockStyle.Fill;
            PNLPCResorce.HorizontalScrollbar = false;
            PNLPCResorce.HorizontalScrollbarBarColor = true;
            PNLPCResorce.HorizontalScrollbarHighlightOnWheel = false;
            PNLPCResorce.HorizontalScrollbarSize = 10;
            PNLPCResorce.Location = new Point(424, 4);
            PNLPCResorce.Margin = new Padding(2);
            PNLPCResorce.Name = "PNLPCResorce";
            PNLPCResorce.Padding = new Padding(2);
            PNLPCResorce.Size = new Size(417, 178);
            PNLPCResorce.Style = MetroFramework.MetroColorStyle.Blue;
            PNLPCResorce.StyleManager = null;
            PNLPCResorce.TabIndex = 1;
            PNLPCResorce.Theme = MetroFramework.MetroThemeStyle.Light;
            PNLPCResorce.VerticalScrollbar = false;
            PNLPCResorce.VerticalScrollbarBarColor = true;
            PNLPCResorce.VerticalScrollbarHighlightOnWheel = false;
            PNLPCResorce.VerticalScrollbarSize = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 174, 219);
            label1.Location = new Point(14, 18);
            label1.Name = "label1";
            label1.Size = new Size(92, 21);
            label1.TabIndex = 27;
            label1.Text = "PC Resorce";
            // 
            // PCResorcePbarCPU
            // 
            PCResorcePbarCPU.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCResorcePbarCPU.BackColor = Color.FromArgb(28, 33, 40);
            PCResorcePbarCPU.BarColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarCPU.FontSize = 10;
            PCResorcePbarCPU.ForeColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarCPU.LabelText = "%";
            PCResorcePbarCPU.Location = new Point(13, 140);
            PCResorcePbarCPU.MaximumValue = false;
            PCResorcePbarCPU.Name = "PCResorcePbarCPU";
            PCResorcePbarCPU.ShowStatus = true;
            PCResorcePbarCPU.Size = new Size(390, 20);
            PCResorcePbarCPU.TabIndex = 31;
            PCResorcePbarCPU.TextColor = Color.White;
            // 
            // PCResorcePbarRAM
            // 
            PCResorcePbarRAM.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            PCResorcePbarRAM.BackColor = Color.FromArgb(28, 33, 40);
            PCResorcePbarRAM.BarColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarRAM.FontSize = 10;
            PCResorcePbarRAM.ForeColor = Color.FromArgb(0, 174, 219);
            PCResorcePbarRAM.LabelText = "MB";
            PCResorcePbarRAM.Location = new Point(13, 86);
            PCResorcePbarRAM.MaximumValue = true;
            PCResorcePbarRAM.Name = "PCResorcePbarRAM";
            PCResorcePbarRAM.ShowStatus = true;
            PCResorcePbarRAM.Size = new Size(390, 20);
            PCResorcePbarRAM.TabIndex = 28;
            PCResorcePbarRAM.TextColor = Color.White;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(128, 120);
            label2.Name = "label2";
            label2.Size = new Size(164, 15);
            label2.TabIndex = 30;
            label2.Text = "Central Processing Unit (CPU)";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(120, 66);
            label3.Name = "label3";
            label3.Size = new Size(181, 15);
            label3.TabIndex = 29;
            label3.Text = "Random-Access Memory  (RAM)";
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(PNLLayoutTop);
            Controls.Add(PNLLayoutBot);
            Name = "HomeControl";
            Size = new Size(845, 370);
            Load += HomeControl_Load;
            PNLLayoutBot.ResumeLayout(false);
            metroPanel1.ResumeLayout(false);
            metroPanel1.PerformLayout();
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            PNLLayoutTop.ResumeLayout(false);
            PNLServerStatus.ResumeLayout(false);
            PNLServerStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PICMySqlServerStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)PicWorldServerStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)PICLoginServerStatus).EndInit();
            PNLPCResorce.ResumeLayout(false);
            PNLPCResorce.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer TimerWacher;
        private TableLayoutPanel PNLLayoutBot;
        private TableLayoutPanel PNLLayoutTop;
        private MetroPanel PNLServerStatus;
        private Label label12;
        private Label label11;
        private Label label10;
        private PictureBox PICMySqlServerStatus;
        private PictureBox PicWorldServerStatus;
        private PictureBox PICLoginServerStatus;
        private MetroPanel PNLPCResorce;
        private Label label1;
        private CustomProgressBar PCResorcePbarCPU;
        private CustomProgressBar PCResorcePbarRAM;
        private Label label2;
        private Label label3;
        private MetroPanel metroPanel1;
        private Label label4;
        private CustomProgressBar PCLoginPbarCPU;
        private CustomProgressBar PCLoginPbarRAM;
        private Label label5;
        private Label label6;
        private MetroPanel metroPanel2;
        private Label label7;
        private CustomProgressBar PCWorldPbarCPU;
        private Label label9;
        private CustomProgressBar PCWorldPbarRAM;
        private Label label8;
    }
}
