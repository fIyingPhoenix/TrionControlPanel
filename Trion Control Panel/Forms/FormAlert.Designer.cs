namespace TrionControlPanel.Forms
{
    partial class FormAlert
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlert));
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.bntClose = new TrionControlPanel.UI.CustomButton();
            this.lblText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIcon.Image = global::TrionControlPanel.Properties.Resources.ok_100;
            this.picIcon.Location = new System.Drawing.Point(19, 20);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(74, 72);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblTitle.Location = new System.Drawing.Point(107, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(60, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle";
            // 
            // timerCheck
            // 
            this.timerCheck.Interval = 10;
            this.timerCheck.Tick += new System.EventHandler(this.TimerCheck_Tick);
            // 
            // bntClose
            // 
            this.bntClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntClose.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.bntClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bntClose.BackgroundImage")));
            this.bntClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bntClose.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.bntClose.BorderRadius = 0;
            this.bntClose.BorderSize = 0;
            this.bntClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntClose.FlatAppearance.BorderSize = 0;
            this.bntClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntClose.ForeColor = System.Drawing.Color.White;
            this.bntClose.Location = new System.Drawing.Point(452, 11);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(20, 20);
            this.bntClose.TabIndex = 1;
            this.bntClose.TextColor = System.Drawing.Color.White;
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.BntClose_Click);
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.lblText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblText.Location = new System.Drawing.Point(107, 41);
            this.lblText.Multiline = true;
            this.lblText.Name = "lblText";
            this.lblText.ReadOnly = true;
            this.lblText.Size = new System.Drawing.Size(367, 61);
            this.lblText.TabIndex = 2;
            // 
            // FormAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(482, 110);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAlert";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "FormAlert";
            this.Load += new System.EventHandler(this.FormAlert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picIcon;
        private Label lblTitle;
        private System.Windows.Forms.Timer timerCheck;
        private UI.CustomButton bntClose;
        private TextBox lblText;
    }
}