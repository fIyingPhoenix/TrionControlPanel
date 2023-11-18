namespace TrionControlPanelDesktop.Controls
{
    partial class SettingsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            tabPage1 = new TabPage();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            metroTextBox4 = new MetroFramework.Controls.MetroTextBox();
            metroTextBox3 = new MetroFramework.Controls.MetroTextBox();
            metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            tabPage3 = new TabPage();
            panel3 = new Panel();
            customButton1 = new UI.Controls.CustomButton();
            customButton3 = new UI.Controls.CustomButton();
            metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            TabPageCore = new TabPage();
            metroTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // metroTabControl1
            // 
            metroTabControl1.Controls.Add(tabPage1);
            metroTabControl1.Controls.Add(tabPage3);
            metroTabControl1.Controls.Add(TabPageCore);
            metroTabControl1.CustomBackground = false;
            metroTabControl1.Dock = DockStyle.Fill;
            metroTabControl1.FontSize = MetroFramework.MetroTabControlSize.Medium;
            metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            metroTabControl1.Location = new Point(0, 0);
            metroTabControl1.Name = "metroTabControl1";
            metroTabControl1.Padding = new Point(6, 8);
            metroTabControl1.SelectedIndex = 0;
            metroTabControl1.Size = new Size(845, 370);
            metroTabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            metroTabControl1.StyleManager = null;
            metroTabControl1.TabIndex = 1;
            metroTabControl1.TextAlign = ContentAlignment.MiddleLeft;
            metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTabControl1.UseStyleColors = true;
            // 
            // tabPage1
            // 
            tabPage1.AccessibleDescription = "";
            tabPage1.AccessibleName = "";
            tabPage1.AccessibleRole = AccessibleRole.Window;
            tabPage1.BackColor = Color.FromArgb(45, 51, 59);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(metroTextBox4);
            tabPage1.Controls.Add(metroTextBox3);
            tabPage1.Controls.Add(metroTextBox2);
            tabPage1.ForeColor = Color.FromArgb(0, 174, 219);
            tabPage1.Location = new Point(4, 35);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(837, 331);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Trion ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(16, 111);
            label4.Name = "label4";
            label4.Size = new Size(157, 15);
            label4.TabIndex = 5;
            label4.Text = "MySQL Executable Location:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(16, 15);
            label3.Name = "label3";
            label3.Size = new Size(151, 15);
            label3.TabIndex = 4;
            label3.Text = "World Executable Location:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(16, 63);
            label2.Name = "label2";
            label2.Size = new Size(149, 15);
            label2.TabIndex = 3;
            label2.Text = "Login Executable Location:";
            // 
            // metroTextBox4
            // 
            metroTextBox4.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox4.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox4.Location = new Point(16, 127);
            metroTextBox4.Multiline = false;
            metroTextBox4.Name = "metroTextBox4";
            metroTextBox4.SelectedText = "";
            metroTextBox4.Size = new Size(711, 23);
            metroTextBox4.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox4.StyleManager = null;
            metroTextBox4.TabIndex = 2;
            metroTextBox4.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox4.UseStyleColors = true;
            // 
            // metroTextBox3
            // 
            metroTextBox3.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox3.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox3.Location = new Point(16, 81);
            metroTextBox3.Multiline = false;
            metroTextBox3.Name = "metroTextBox3";
            metroTextBox3.SelectedText = "";
            metroTextBox3.Size = new Size(711, 23);
            metroTextBox3.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox3.StyleManager = null;
            metroTextBox3.TabIndex = 1;
            metroTextBox3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox3.UseStyleColors = true;
            // 
            // metroTextBox2
            // 
            metroTextBox2.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox2.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox2.Location = new Point(16, 33);
            metroTextBox2.Multiline = false;
            metroTextBox2.Name = "metroTextBox2";
            metroTextBox2.SelectedText = "";
            metroTextBox2.Size = new Size(711, 23);
            metroTextBox2.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox2.StyleManager = null;
            metroTextBox2.TabIndex = 0;
            metroTextBox2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox2.UseStyleColors = true;
            // 
            // tabPage3
            // 
            tabPage3.AccessibleDescription = "";
            tabPage3.AccessibleName = "asdasdas";
            tabPage3.BackColor = Color.FromArgb(45, 51, 59);
            tabPage3.Controls.Add(panel3);
            tabPage3.Controls.Add(customButton1);
            tabPage3.Controls.Add(customButton3);
            tabPage3.Controls.Add(metroTextBox1);
            tabPage3.Location = new Point(4, 35);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(837, 331);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "MySQL";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(34, 39, 46);
            panel3.Location = new Point(23, 94);
            panel3.Name = "panel3";
            panel3.Size = new Size(383, 217);
            panel3.TabIndex = 13;
            // 
            // customButton1
            // 
            customButton1.Anchor = AnchorStyles.Top;
            customButton1.BackColor = Color.FromArgb(28, 33, 40);
            customButton1.BackgroundColor = Color.FromArgb(28, 33, 40);
            customButton1.BorderColor = Color.FromArgb(0, 174, 219);
            customButton1.BorderRadius = 0;
            customButton1.BorderSize = 1;
            customButton1.FlatAppearance.BorderSize = 0;
            customButton1.FlatStyle = FlatStyle.Flat;
            customButton1.ForeColor = Color.White;
            customButton1.Image = (Image)resources.GetObject("customButton1.Image");
            customButton1.ImageAlign = ContentAlignment.MiddleLeft;
            customButton1.Location = new Point(706, 281);
            customButton1.Name = "customButton1";
            customButton1.RightToLeft = RightToLeft.No;
            customButton1.Size = new Size(110, 30);
            customButton1.TabIndex = 12;
            customButton1.Text = "   ...";
            customButton1.TextColor = Color.White;
            customButton1.UseVisualStyleBackColor = false;
            // 
            // customButton3
            // 
            customButton3.Anchor = AnchorStyles.Top;
            customButton3.BackColor = Color.FromArgb(28, 33, 40);
            customButton3.BackgroundColor = Color.FromArgb(28, 33, 40);
            customButton3.BorderColor = Color.FromArgb(0, 174, 219);
            customButton3.BorderRadius = 0;
            customButton3.BorderSize = 1;
            customButton3.FlatAppearance.BorderSize = 0;
            customButton3.FlatStyle = FlatStyle.Flat;
            customButton3.ForeColor = Color.White;
            customButton3.Image = (Image)resources.GetObject("customButton3.Image");
            customButton3.ImageAlign = ContentAlignment.MiddleLeft;
            customButton3.Location = new Point(753, 49);
            customButton3.Name = "customButton3";
            customButton3.RightToLeft = RightToLeft.No;
            customButton3.Size = new Size(63, 23);
            customButton3.TabIndex = 10;
            customButton3.Text = "   ...";
            customButton3.TextColor = Color.White;
            customButton3.UseVisualStyleBackColor = false;
            // 
            // metroTextBox1
            // 
            metroTextBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            metroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox1.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox1.Location = new Point(23, 49);
            metroTextBox1.Multiline = false;
            metroTextBox1.Name = "metroTextBox1";
            metroTextBox1.SelectedText = "";
            metroTextBox1.Size = new Size(724, 23);
            metroTextBox1.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox1.StyleManager = null;
            metroTextBox1.TabIndex = 0;
            metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox1.UseStyleColors = true;
            // 
            // TabPageCore
            // 
            TabPageCore.BackColor = Color.FromArgb(45, 51, 59);
            TabPageCore.Location = new Point(4, 35);
            TabPageCore.Name = "TabPageCore";
            TabPageCore.Size = new Size(837, 331);
            TabPageCore.TabIndex = 1;
            TabPageCore.Text = "Core";
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(metroTabControl1);
            ForeColor = Color.White;
            Name = "SettingsControl";
            Size = new Size(845, 370);
            metroTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private TabPage tabPage1;
        private TabPage TabPageCore;
        private TabPage tabPage3;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private UI.Controls.CustomButton customButton3;
        private UI.Controls.CustomButton customButton1;
        private Panel panel3;
        private MetroFramework.Controls.MetroTextBox metroTextBox2;
        private Label label2;
        private MetroFramework.Controls.MetroTextBox metroTextBox4;
        private MetroFramework.Controls.MetroTextBox metroTextBox3;
        private Label label4;
        private Label label3;
    }
}
