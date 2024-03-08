namespace MetroFramework
{
    partial class MetroMessageBoxControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetroMessageBoxControl));
            panelbody = new Panel();
            tlpBody = new TableLayoutPanel();
            messageLabel = new Label();
            pnlBottom = new Panel();
            metroButton2 = new Controls.MetroButton();
            metroButton1 = new Controls.MetroButton();
            metroButton3 = new Controls.MetroButton();
            titleLabel = new Label();
            panelbody.SuspendLayout();
            tlpBody.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelbody
            // 
            panelbody.BackColor = Color.FromArgb(45, 51, 59);
            panelbody.Controls.Add(tlpBody);
            panelbody.Dock = DockStyle.Fill;
            panelbody.Location = new Point(0, 0);
            panelbody.Margin = new Padding(0);
            panelbody.Name = "panelbody";
            panelbody.Size = new Size(804, 211);
            panelbody.TabIndex = 2;
            // 
            // tlpBody
            // 
            tlpBody.ColumnCount = 3;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tlpBody.Controls.Add(messageLabel, 1, 2);
            tlpBody.Controls.Add(pnlBottom, 1, 3);
            tlpBody.Controls.Add(titleLabel, 1, 1);
            tlpBody.Dock = DockStyle.Fill;
            tlpBody.Location = new Point(0, 0);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 4;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            tlpBody.RowStyles.Add(new RowStyle());
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpBody.Size = new Size(804, 211);
            tlpBody.TabIndex = 6;
            // 
            // messageLabel
            // 
            messageLabel.AutoSize = true;
            messageLabel.BackColor = Color.Transparent;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(83, 30);
            messageLabel.Margin = new Padding(3, 0, 0, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(640, 141);
            messageLabel.TabIndex = 0;
            messageLabel.Text = "message here";
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.Transparent;
            pnlBottom.Controls.Add(metroButton2);
            pnlBottom.Controls.Add(metroButton1);
            pnlBottom.Controls.Add(metroButton3);
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Location = new Point(80, 171);
            pnlBottom.Margin = new Padding(0);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(643, 40);
            pnlBottom.TabIndex = 2;
            // 
            // metroButton2
            // 
            metroButton2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton2.Highlight = false;
            metroButton2.Location = new Point(448, 7);
            metroButton2.Name = "metroButton2";
            metroButton2.Size = new Size(90, 26);
            metroButton2.Style = MetroColorStyle.Blue;
            metroButton2.StyleManager = null;
            metroButton2.TabIndex = 4;
            metroButton2.Text = "button 2";
            metroButton2.Theme = MetroThemeStyle.Light;
            // 
            // metroButton1
            // 
            metroButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton1.BackColor = Color.ForestGreen;
            metroButton1.Highlight = false;
            metroButton1.Location = new Point(350, 7);
            metroButton1.Name = "metroButton1";
            metroButton1.Size = new Size(90, 26);
            metroButton1.Style = MetroColorStyle.Blue;
            metroButton1.StyleManager = null;
            metroButton1.TabIndex = 3;
            metroButton1.Text = "button 1";
            metroButton1.Theme = MetroThemeStyle.Light;
            metroButton1.UseVisualStyleBackColor = false;
            // 
            // metroButton3
            // 
            metroButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton3.Highlight = false;
            metroButton3.Location = new Point(546, 7);
            metroButton3.Name = "metroButton3";
            metroButton3.Size = new Size(90, 26);
            metroButton3.Style = MetroColorStyle.Blue;
            metroButton3.StyleManager = null;
            metroButton3.TabIndex = 5;
            metroButton3.Text = "button 3";
            metroButton3.Theme = MetroThemeStyle.Light;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.WhiteSmoke;
            titleLabel.Location = new Point(80, 5);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(643, 25);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "message title";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MetroMessageBoxControl
            // 
            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            ClientSize = new Size(804, 211);
            ControlBox = false;
            Controls.Add(panelbody);
            Font = new Font("Segoe UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "MetroMessageBoxControl";
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Load += MetroMessageBoxControl_Load;
            panelbody.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            tlpBody.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelbody;
        private Label titleLabel;
        private Label messageLabel;
        private Controls.MetroButton metroButton1;
        private Controls.MetroButton metroButton2;
        private Controls.MetroButton metroButton3;
        private TableLayoutPanel tlpBody;
        private Panel pnlBottom;
    }
}
