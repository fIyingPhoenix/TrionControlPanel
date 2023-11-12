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
            panel1 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel4 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            metroProgressBar1 = new MetroFramework.Controls.MetroProgressBar();
            metroProgressBar2 = new MetroFramework.Controls.MetroProgressBar();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(8, 8);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(2);
            panel1.Size = new Size(411, 168);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(34, 39, 46);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(2, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(407, 164);
            panel3.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(panel4);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(424, 7);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(2);
            panel2.Size = new Size(414, 170);
            panel2.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(34, 39, 46);
            panel4.Controls.Add(metroProgressBar2);
            panel4.Controls.Add(metroProgressBar1);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(2, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(410, 166);
            panel4.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(5);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(845, 184);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // metroProgressBar1
            // 
            metroProgressBar1.FontSize = MetroFramework.MetroProgressBarSize.Medium;
            metroProgressBar1.FontWeight = MetroFramework.MetroProgressBarWeight.Light;
            metroProgressBar1.HideProgressText = true;
            metroProgressBar1.Location = new Point(19, 116);
            metroProgressBar1.Name = "metroProgressBar1";
            metroProgressBar1.ProgressBarStyle = ProgressBarStyle.Continuous;
            metroProgressBar1.Size = new Size(369, 17);
            metroProgressBar1.Style = MetroFramework.MetroColorStyle.Blue;
            metroProgressBar1.StyleManager = null;
            metroProgressBar1.TabIndex = 0;
            metroProgressBar1.TextAlign = ContentAlignment.MiddleRight;
            metroProgressBar1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroProgressBar1.Value = 30;
            // 
            // metroProgressBar2
            // 
            metroProgressBar2.FontSize = MetroFramework.MetroProgressBarSize.Medium;
            metroProgressBar2.FontWeight = MetroFramework.MetroProgressBarWeight.Light;
            metroProgressBar2.HideProgressText = true;
            metroProgressBar2.Location = new Point(19, 79);
            metroProgressBar2.Name = "metroProgressBar2";
            metroProgressBar2.ProgressBarStyle = ProgressBarStyle.Continuous;
            metroProgressBar2.Size = new Size(369, 17);
            metroProgressBar2.Style = MetroFramework.MetroColorStyle.Blue;
            metroProgressBar2.StyleManager = null;
            metroProgressBar2.TabIndex = 3;
            metroProgressBar2.TextAlign = ContentAlignment.MiddleRight;
            metroProgressBar2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroProgressBar2.Value = 30;
            // 
            // HomeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(tableLayoutPanel1);
            Name = "HomeControl";
            Size = new Size(845, 430);
            Load += HomeControl_Load;
            Resize += HomeControl_Resize;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar2;
        private MetroFramework.Controls.MetroProgressBar metroProgressBar1;
    }
}
