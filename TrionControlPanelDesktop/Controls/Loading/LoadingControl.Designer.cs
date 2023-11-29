namespace TrionControlPanelDesktop.Controls
{
    partial class LoadingControl
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
            label1 = new Label();
            metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 174, 219);
            label1.Location = new Point(372, 311);
            label1.Name = "label1";
            label1.Size = new Size(100, 30);
            label1.TabIndex = 3;
            label1.Text = "Loading..";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroProgressSpinner1
            // 
            metroProgressSpinner1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            metroProgressSpinner1.CustomBackground = true;
            metroProgressSpinner1.Location = new Point(283, 30);
            metroProgressSpinner1.Maximum = 100;
            metroProgressSpinner1.Name = "metroProgressSpinner1";
            metroProgressSpinner1.Size = new Size(279, 241);
            metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Blue;
            metroProgressSpinner1.StyleManager = null;
            metroProgressSpinner1.TabIndex = 2;
            metroProgressSpinner1.Text = "metroProgressSpinner1";
            metroProgressSpinner1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroProgressSpinner1.Value = 15;
            // 
            // LoadingControlcs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(label1);
            Controls.Add(metroProgressSpinner1);
            Name = "LoadingControlcs";
            Padding = new Padding(1);
            Size = new Size(845, 370);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}
