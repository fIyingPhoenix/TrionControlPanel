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
            messageLabel = new Label();
            metroButton2 = new Controls.MetroButton();
            metroButton1 = new Controls.MetroButton();
            metroButton3 = new Controls.MetroButton();
            titleLabel = new Label();
            metroPanel1 = new Controls.MetroPanel();
            panel1 = new Panel();
            metroPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // messageLabel
            // 
            messageLabel.BackColor = Color.Transparent;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(0, 0);
            messageLabel.Margin = new Padding(3, 0, 0, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(780, 107);
            messageLabel.TabIndex = 6;
            messageLabel.Text = "message here";
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroButton2
            // 
            metroButton2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton2.Highlight = false;
            metroButton2.Location = new Point(604, 173);
            metroButton2.Name = "metroButton2";
            metroButton2.Size = new Size(90, 26);
            metroButton2.Style = MetroColorStyle.Blue;
            metroButton2.StyleManager = null;
            metroButton2.TabIndex = 9;
            metroButton2.Text = "button 2";
            metroButton2.Theme = MetroThemeStyle.Dark;
            // 
            // metroButton1
            // 
            metroButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton1.BackColor = Color.ForestGreen;
            metroButton1.Highlight = false;
            metroButton1.Location = new Point(506, 173);
            metroButton1.Name = "metroButton1";
            metroButton1.Size = new Size(90, 26);
            metroButton1.Style = MetroColorStyle.Blue;
            metroButton1.StyleManager = null;
            metroButton1.TabIndex = 8;
            metroButton1.Text = "button 1";
            metroButton1.Theme = MetroThemeStyle.Dark;
            metroButton1.UseVisualStyleBackColor = false;
            // 
            // metroButton3
            // 
            metroButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            metroButton3.Highlight = false;
            metroButton3.Location = new Point(702, 173);
            metroButton3.Name = "metroButton3";
            metroButton3.Size = new Size(90, 26);
            metroButton3.Style = MetroColorStyle.Blue;
            metroButton3.StyleManager = null;
            metroButton3.TabIndex = 10;
            metroButton3.Text = "button 3";
            metroButton3.Theme = MetroThemeStyle.Dark;
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            titleLabel.ForeColor = Color.WhiteSmoke;
            titleLabel.Location = new Point(0, 0);
            titleLabel.Margin = new Padding(0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(780, 42);
            titleLabel.TabIndex = 7;
            titleLabel.Text = "message title";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroPanel1
            // 
            metroPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel1.Border = false;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(titleLabel);
            metroPanel1.CustomBackground = false;
            metroPanel1.HorizontalScrollbar = false;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(12, 12);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Size = new Size(780, 42);
            metroPanel1.Style = MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 11;
            metroPanel1.Theme = MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(messageLabel);
            panel1.Location = new Point(12, 60);
            panel1.Name = "panel1";
            panel1.Size = new Size(780, 107);
            panel1.TabIndex = 12;
            // 
            // MetroMessageBoxControl
            // 
            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            ClientSize = new Size(804, 211);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(metroPanel1);
            Controls.Add(metroButton2);
            Controls.Add(metroButton1);
            Controls.Add(metroButton3);
            Font = new Font("Segoe UI Light", 12F);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 5, 4, 5);
            Name = "MetroMessageBoxControl";
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            Load += MetroMessageBoxControl_Load;
            metroPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label messageLabel;
        private Controls.MetroButton metroButton2;
        private Controls.MetroButton metroButton1;
        private Controls.MetroButton metroButton3;
        private Label titleLabel;
        private Controls.MetroPanel metroPanel1;
        private Panel panel1;
    }
}
