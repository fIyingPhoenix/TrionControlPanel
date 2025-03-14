namespace TrionControlPanel.Desktop
{
    partial class LoadingScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreen));
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 15;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(100, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // metroProgressSpinner1
            // 
            metroProgressSpinner1.CustomBackground = false;
            metroProgressSpinner1.Location = new Point(165, 208);
            metroProgressSpinner1.Maximum = 100;
            metroProgressSpinner1.Name = "metroProgressSpinner1";
            metroProgressSpinner1.Size = new Size(60, 60);
            metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Blue;
            metroProgressSpinner1.StyleManager = null;
            metroProgressSpinner1.TabIndex = 1;
            metroProgressSpinner1.Text = "metroProgressSpinner1";
            metroProgressSpinner1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroProgressSpinner1.Value = 80;
            // 
            // LoadingScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 33, 40);
            ClientSize = new Size(400, 300);
            Controls.Add(metroProgressSpinner1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoadingScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoadingForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}