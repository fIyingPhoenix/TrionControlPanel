namespace CypherCore_Server_Laucher.Forms
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
            this.lblText = new System.Windows.Forms.Label();
            this.bntClose = new CypherCore_Server_Laucher.UI.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIcon.Image = global::Cypher_CoreServer_Laucher.Properties.Resources.ok_100;
            this.picIcon.Location = new System.Drawing.Point(8, 9);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(80, 80);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblTitle.Location = new System.Drawing.Point(94, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(60, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle";
            // 
            // timerCheck
            // 
            this.timerCheck.Interval = 10;
            this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblText.Location = new System.Drawing.Point(94, 44);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(41, 15);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "lblText";
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
            this.bntClose.Location = new System.Drawing.Point(302, 8);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(20, 20);
            this.bntClose.TabIndex = 3;
            this.bntClose.TextColor = System.Drawing.Color.White;
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // FormAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(330, 100);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlert";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "FormAlert";
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox picIcon;
        private Label lblTitle;
        private System.Windows.Forms.Timer timerCheck;
        private Label lblText;
        private UI.CustomButton bntClose;
    }
}