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
            TabPageTrion = new TabPage();
            BTNMySQLOpenFolder = new UI.Controls.CustomButton();
            BTNCoreOpenFolder = new UI.Controls.CustomButton();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            TGLHideConsole = new TrionControlPanel.UI.CustomToggleButton();
            TGLNotificationSound = new TrionControlPanel.UI.CustomToggleButton();
            TGLStayInTrey = new TrionControlPanel.UI.CustomToggleButton();
            BTNMySQLExecLovation = new UI.Controls.CustomButton();
            BTNCoreExecLovation = new UI.Controls.CustomButton();
            label4 = new Label();
            label3 = new Label();
            TXTBoxMySQLExecLocation = new MetroFramework.Controls.MetroTextBox();
            TXTBoxCoreExecLocation = new MetroFramework.Controls.MetroTextBox();
            TabPageCore = new TabPage();
            label10 = new Label();
            TXTBoxMySQLExecName = new MetroFramework.Controls.MetroTextBox();
            label9 = new Label();
            TXTBoxLoginExecName = new MetroFramework.Controls.MetroTextBox();
            label8 = new Label();
            TXTBoxWorldExecName = new MetroFramework.Controls.MetroTextBox();
            label1 = new Label();
            ComboBoxCores = new TrionControlPanel.UI.CustomComboBox();
            TabPageMysql = new TabPage();
            panel3 = new Panel();
            customButton1 = new UI.Controls.CustomButton();
            customButton3 = new UI.Controls.CustomButton();
            metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            label11 = new Label();
            customToggleButton1 = new TrionControlPanel.UI.CustomToggleButton();
            metroTabControl1.SuspendLayout();
            TabPageTrion.SuspendLayout();
            TabPageCore.SuspendLayout();
            TabPageMysql.SuspendLayout();
            SuspendLayout();
            // 
            // metroTabControl1
            // 
            metroTabControl1.Controls.Add(TabPageCore);
            metroTabControl1.Controls.Add(TabPageTrion);
            metroTabControl1.Controls.Add(TabPageMysql);
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
            metroTabControl1.TabIndex = 2;
            metroTabControl1.TextAlign = ContentAlignment.MiddleLeft;
            metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTabControl1.UseStyleColors = true;
            // 
            // TabPageTrion
            // 
            TabPageTrion.AccessibleDescription = "";
            TabPageTrion.AccessibleName = "";
            TabPageTrion.AccessibleRole = AccessibleRole.Window;
            TabPageTrion.BackColor = Color.FromArgb(45, 51, 59);
            TabPageTrion.Controls.Add(BTNMySQLOpenFolder);
            TabPageTrion.Controls.Add(BTNCoreOpenFolder);
            TabPageTrion.Controls.Add(label7);
            TabPageTrion.Controls.Add(label6);
            TabPageTrion.Controls.Add(label5);
            TabPageTrion.Controls.Add(TGLHideConsole);
            TabPageTrion.Controls.Add(TGLNotificationSound);
            TabPageTrion.Controls.Add(TGLStayInTrey);
            TabPageTrion.Controls.Add(BTNMySQLExecLovation);
            TabPageTrion.Controls.Add(BTNCoreExecLovation);
            TabPageTrion.Controls.Add(label4);
            TabPageTrion.Controls.Add(label3);
            TabPageTrion.Controls.Add(TXTBoxMySQLExecLocation);
            TabPageTrion.Controls.Add(TXTBoxCoreExecLocation);
            TabPageTrion.ForeColor = Color.FromArgb(0, 174, 219);
            TabPageTrion.Location = new Point(4, 35);
            TabPageTrion.Name = "TabPageTrion";
            TabPageTrion.Size = new Size(837, 331);
            TabPageTrion.TabIndex = 0;
            TabPageTrion.Text = "Trion ";
            // 
            // BTNMySQLOpenFolder
            // 
            BTNMySQLOpenFolder.Anchor = AnchorStyles.Top;
            BTNMySQLOpenFolder.BackColor = Color.FromArgb(28, 33, 40);
            BTNMySQLOpenFolder.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNMySQLOpenFolder.BorderColor = Color.FromArgb(0, 174, 219);
            BTNMySQLOpenFolder.BorderRadius = 0;
            BTNMySQLOpenFolder.BorderSize = 1;
            BTNMySQLOpenFolder.FlatAppearance.BorderSize = 0;
            BTNMySQLOpenFolder.FlatStyle = FlatStyle.Flat;
            BTNMySQLOpenFolder.ForeColor = Color.White;
            BTNMySQLOpenFolder.Image = (Image)resources.GetObject("BTNMySQLOpenFolder.Image");
            BTNMySQLOpenFolder.ImageAlign = ContentAlignment.MiddleLeft;
            BTNMySQLOpenFolder.Location = new Point(211, 291);
            BTNMySQLOpenFolder.Name = "BTNMySQLOpenFolder";
            BTNMySQLOpenFolder.RightToLeft = RightToLeft.No;
            BTNMySQLOpenFolder.Size = new Size(186, 25);
            BTNMySQLOpenFolder.TabIndex = 31;
            BTNMySQLOpenFolder.Text = "   MySQL Working Directory";
            BTNMySQLOpenFolder.TextColor = Color.White;
            BTNMySQLOpenFolder.UseVisualStyleBackColor = false;
            // 
            // BTNCoreOpenFolder
            // 
            BTNCoreOpenFolder.Anchor = AnchorStyles.Top;
            BTNCoreOpenFolder.BackColor = Color.FromArgb(28, 33, 40);
            BTNCoreOpenFolder.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNCoreOpenFolder.BorderColor = Color.FromArgb(0, 174, 219);
            BTNCoreOpenFolder.BorderRadius = 0;
            BTNCoreOpenFolder.BorderSize = 1;
            BTNCoreOpenFolder.FlatAppearance.BorderSize = 0;
            BTNCoreOpenFolder.FlatStyle = FlatStyle.Flat;
            BTNCoreOpenFolder.ForeColor = Color.White;
            BTNCoreOpenFolder.Image = (Image)resources.GetObject("BTNCoreOpenFolder.Image");
            BTNCoreOpenFolder.ImageAlign = ContentAlignment.MiddleLeft;
            BTNCoreOpenFolder.Location = new Point(19, 291);
            BTNCoreOpenFolder.Name = "BTNCoreOpenFolder";
            BTNCoreOpenFolder.RightToLeft = RightToLeft.No;
            BTNCoreOpenFolder.Size = new Size(186, 25);
            BTNCoreOpenFolder.TabIndex = 28;
            BTNCoreOpenFolder.Text = "   Core Working Directory";
            BTNCoreOpenFolder.TextColor = Color.White;
            BTNCoreOpenFolder.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(70, 187);
            label7.Name = "label7";
            label7.Size = new Size(81, 15);
            label7.TabIndex = 21;
            label7.Text = "Hide Console.";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(70, 159);
            label6.Name = "label6";
            label6.Size = new Size(109, 15);
            label6.TabIndex = 20;
            label6.Text = "Notification sound.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(70, 131);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 19;
            label5.Text = "Stay in tray";
            // 
            // TGLHideConsole
            // 
            TGLHideConsole.AutoSize = true;
            TGLHideConsole.Location = new Point(19, 184);
            TGLHideConsole.MinimumSize = new Size(45, 22);
            TGLHideConsole.Name = "TGLHideConsole";
            TGLHideConsole.OffBackColor = Color.FromArgb(34, 34, 34);
            TGLHideConsole.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLHideConsole.OnBackColor = Color.FromArgb(34, 34, 34);
            TGLHideConsole.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLHideConsole.Size = new Size(45, 22);
            TGLHideConsole.TabIndex = 18;
            TGLHideConsole.UseVisualStyleBackColor = true;
            // 
            // TGLNotificationSound
            // 
            TGLNotificationSound.AutoSize = true;
            TGLNotificationSound.Location = new Point(19, 156);
            TGLNotificationSound.MinimumSize = new Size(45, 22);
            TGLNotificationSound.Name = "TGLNotificationSound";
            TGLNotificationSound.OffBackColor = Color.FromArgb(34, 34, 34);
            TGLNotificationSound.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLNotificationSound.OnBackColor = Color.FromArgb(34, 34, 34);
            TGLNotificationSound.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLNotificationSound.Size = new Size(45, 22);
            TGLNotificationSound.TabIndex = 17;
            TGLNotificationSound.UseVisualStyleBackColor = true;
            // 
            // TGLStayInTrey
            // 
            TGLStayInTrey.AutoSize = true;
            TGLStayInTrey.Location = new Point(19, 128);
            TGLStayInTrey.MinimumSize = new Size(45, 22);
            TGLStayInTrey.Name = "TGLStayInTrey";
            TGLStayInTrey.OffBackColor = Color.FromArgb(34, 34, 34);
            TGLStayInTrey.OffToggleColor = Color.FromArgb(255, 87, 57);
            TGLStayInTrey.OnBackColor = Color.FromArgb(34, 34, 34);
            TGLStayInTrey.OnToggleColor = Color.FromArgb(105, 195, 59);
            TGLStayInTrey.Size = new Size(45, 22);
            TGLStayInTrey.TabIndex = 16;
            TGLStayInTrey.UseVisualStyleBackColor = true;
            // 
            // BTNMySQLExecLovation
            // 
            BTNMySQLExecLovation.Anchor = AnchorStyles.Top;
            BTNMySQLExecLovation.BackColor = Color.FromArgb(28, 33, 40);
            BTNMySQLExecLovation.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNMySQLExecLovation.BorderColor = Color.FromArgb(0, 174, 219);
            BTNMySQLExecLovation.BorderRadius = 0;
            BTNMySQLExecLovation.BorderSize = 1;
            BTNMySQLExecLovation.FlatAppearance.BorderSize = 0;
            BTNMySQLExecLovation.FlatStyle = FlatStyle.Flat;
            BTNMySQLExecLovation.ForeColor = Color.White;
            BTNMySQLExecLovation.Image = (Image)resources.GetObject("BTNMySQLExecLovation.Image");
            BTNMySQLExecLovation.ImageAlign = ContentAlignment.MiddleLeft;
            BTNMySQLExecLovation.Location = new Point(756, 81);
            BTNMySQLExecLovation.Name = "BTNMySQLExecLovation";
            BTNMySQLExecLovation.RightToLeft = RightToLeft.No;
            BTNMySQLExecLovation.Size = new Size(65, 25);
            BTNMySQLExecLovation.TabIndex = 13;
            BTNMySQLExecLovation.Text = "   ...";
            BTNMySQLExecLovation.TextColor = Color.White;
            BTNMySQLExecLovation.UseVisualStyleBackColor = false;
            // 
            // BTNCoreExecLovation
            // 
            BTNCoreExecLovation.Anchor = AnchorStyles.Top;
            BTNCoreExecLovation.BackColor = Color.FromArgb(28, 33, 40);
            BTNCoreExecLovation.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNCoreExecLovation.BorderColor = Color.FromArgb(0, 174, 219);
            BTNCoreExecLovation.BorderRadius = 0;
            BTNCoreExecLovation.BorderSize = 1;
            BTNCoreExecLovation.FlatAppearance.BorderSize = 0;
            BTNCoreExecLovation.FlatStyle = FlatStyle.Flat;
            BTNCoreExecLovation.ForeColor = Color.White;
            BTNCoreExecLovation.Image = (Image)resources.GetObject("BTNCoreExecLovation.Image");
            BTNCoreExecLovation.ImageAlign = ContentAlignment.MiddleLeft;
            BTNCoreExecLovation.Location = new Point(756, 26);
            BTNCoreExecLovation.Name = "BTNCoreExecLovation";
            BTNCoreExecLovation.RightToLeft = RightToLeft.No;
            BTNCoreExecLovation.Size = new Size(65, 25);
            BTNCoreExecLovation.TabIndex = 11;
            BTNCoreExecLovation.Text = "   ...";
            BTNCoreExecLovation.TextColor = Color.White;
            BTNCoreExecLovation.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(19, 64);
            label4.Name = "label4";
            label4.Size = new Size(144, 15);
            label4.TabIndex = 5;
            label4.Text = "MySQL Working Directory";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(19, 9);
            label3.Name = "label3";
            label3.Size = new Size(131, 15);
            label3.TabIndex = 4;
            label3.Text = "Core Working Directory";
            // 
            // TXTBoxMySQLExecLocation
            // 
            TXTBoxMySQLExecLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxMySQLExecLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxMySQLExecLocation.Location = new Point(16, 81);
            TXTBoxMySQLExecLocation.Multiline = false;
            TXTBoxMySQLExecLocation.Name = "TXTBoxMySQLExecLocation";
            TXTBoxMySQLExecLocation.SelectedText = "";
            TXTBoxMySQLExecLocation.Size = new Size(735, 25);
            TXTBoxMySQLExecLocation.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxMySQLExecLocation.StyleManager = null;
            TXTBoxMySQLExecLocation.TabIndex = 2;
            TXTBoxMySQLExecLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxMySQLExecLocation.UseStyleColors = true;
            // 
            // TXTBoxCoreExecLocation
            // 
            TXTBoxCoreExecLocation.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxCoreExecLocation.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxCoreExecLocation.Location = new Point(16, 26);
            TXTBoxCoreExecLocation.Multiline = false;
            TXTBoxCoreExecLocation.Name = "TXTBoxCoreExecLocation";
            TXTBoxCoreExecLocation.SelectedText = "";
            TXTBoxCoreExecLocation.Size = new Size(735, 25);
            TXTBoxCoreExecLocation.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxCoreExecLocation.StyleManager = null;
            TXTBoxCoreExecLocation.TabIndex = 0;
            TXTBoxCoreExecLocation.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxCoreExecLocation.UseStyleColors = true;
            // 
            // TabPageCore
            // 
            TabPageCore.BackColor = Color.FromArgb(45, 51, 59);
            TabPageCore.Controls.Add(label11);
            TabPageCore.Controls.Add(customToggleButton1);
            TabPageCore.Controls.Add(label10);
            TabPageCore.Controls.Add(TXTBoxMySQLExecName);
            TabPageCore.Controls.Add(label9);
            TabPageCore.Controls.Add(TXTBoxLoginExecName);
            TabPageCore.Controls.Add(label8);
            TabPageCore.Controls.Add(TXTBoxWorldExecName);
            TabPageCore.Controls.Add(label1);
            TabPageCore.Controls.Add(ComboBoxCores);
            TabPageCore.Location = new Point(4, 35);
            TabPageCore.Name = "TabPageCore";
            TabPageCore.Size = new Size(837, 331);
            TabPageCore.TabIndex = 1;
            TabPageCore.Text = "Core";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(23, 160);
            label10.Name = "label10";
            label10.Size = new Size(146, 15);
            label10.TabIndex = 35;
            label10.Text = "MySQLl Executable Name:";
            // 
            // TXTBoxMySQLExecName
            // 
            TXTBoxMySQLExecName.Enabled = false;
            TXTBoxMySQLExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxMySQLExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxMySQLExecName.Location = new Point(20, 177);
            TXTBoxMySQLExecName.Multiline = false;
            TXTBoxMySQLExecName.Name = "TXTBoxMySQLExecName";
            TXTBoxMySQLExecName.SelectedText = "";
            TXTBoxMySQLExecName.Size = new Size(280, 25);
            TXTBoxMySQLExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxMySQLExecName.StyleManager = null;
            TXTBoxMySQLExecName.TabIndex = 34;
            TXTBoxMySQLExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxMySQLExecName.UseStyleColors = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(22, 114);
            label9.Name = "label9";
            label9.Size = new Size(132, 15);
            label9.TabIndex = 33;
            label9.Text = "Login Executable Name";
            // 
            // TXTBoxLoginExecName
            // 
            TXTBoxLoginExecName.Enabled = false;
            TXTBoxLoginExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxLoginExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxLoginExecName.Location = new Point(20, 131);
            TXTBoxLoginExecName.Multiline = false;
            TXTBoxLoginExecName.Name = "TXTBoxLoginExecName";
            TXTBoxLoginExecName.SelectedText = "";
            TXTBoxLoginExecName.Size = new Size(280, 25);
            TXTBoxLoginExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxLoginExecName.StyleManager = null;
            TXTBoxLoginExecName.TabIndex = 32;
            TXTBoxLoginExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxLoginExecName.UseStyleColors = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(23, 67);
            label8.Name = "label8";
            label8.Size = new Size(134, 15);
            label8.TabIndex = 31;
            label8.Text = "World Executable Name";
            // 
            // TXTBoxWorldExecName
            // 
            TXTBoxWorldExecName.Enabled = false;
            TXTBoxWorldExecName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxWorldExecName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxWorldExecName.Location = new Point(20, 84);
            TXTBoxWorldExecName.Multiline = false;
            TXTBoxWorldExecName.Name = "TXTBoxWorldExecName";
            TXTBoxWorldExecName.SelectedText = "";
            TXTBoxWorldExecName.Size = new Size(280, 25);
            TXTBoxWorldExecName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxWorldExecName.StyleManager = null;
            TXTBoxWorldExecName.TabIndex = 30;
            TXTBoxWorldExecName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxWorldExecName.UseStyleColors = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(23, 18);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 29;
            label1.Text = "Core:";
            // 
            // ComboBoxCores
            // 
            ComboBoxCores.BackColor = Color.FromArgb(34, 34, 34);
            ComboBoxCores.BorderColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.BorderSize = 1;
            ComboBoxCores.DropDownStyle = ComboBoxStyle.DropDown;
            ComboBoxCores.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ComboBoxCores.ForeColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.IconColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.ListBackColor = Color.FromArgb(34, 34, 34);
            ComboBoxCores.ListTextColor = Color.FromArgb(0, 174, 219);
            ComboBoxCores.Location = new Point(20, 36);
            ComboBoxCores.MinimumSize = new Size(200, 27);
            ComboBoxCores.Name = "ComboBoxCores";
            ComboBoxCores.Padding = new Padding(1);
            ComboBoxCores.Size = new Size(280, 27);
            ComboBoxCores.TabIndex = 28;
            ComboBoxCores.Texts = "";
            // 
            // TabPageMysql
            // 
            TabPageMysql.AccessibleDescription = "";
            TabPageMysql.AccessibleName = "";
            TabPageMysql.BackColor = Color.FromArgb(45, 51, 59);
            TabPageMysql.Controls.Add(panel3);
            TabPageMysql.Controls.Add(customButton1);
            TabPageMysql.Controls.Add(customButton3);
            TabPageMysql.Controls.Add(metroTextBox1);
            TabPageMysql.Location = new Point(4, 35);
            TabPageMysql.Name = "TabPageMysql";
            TabPageMysql.Size = new Size(837, 331);
            TabPageMysql.TabIndex = 2;
            TabPageMysql.Text = "MySQL";
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
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(74, 251);
            label11.Name = "label11";
            label11.Size = new Size(92, 15);
            label11.TabIndex = 37;
            label11.Text = "Custom Names.";
            // 
            // customToggleButton1
            // 
            customToggleButton1.AutoSize = true;
            customToggleButton1.Location = new Point(23, 248);
            customToggleButton1.MinimumSize = new Size(45, 22);
            customToggleButton1.Name = "customToggleButton1";
            customToggleButton1.OffBackColor = Color.FromArgb(34, 34, 34);
            customToggleButton1.OffToggleColor = Color.FromArgb(255, 87, 57);
            customToggleButton1.OnBackColor = Color.FromArgb(34, 34, 34);
            customToggleButton1.OnToggleColor = Color.FromArgb(105, 195, 59);
            customToggleButton1.Size = new Size(45, 22);
            customToggleButton1.TabIndex = 36;
            customToggleButton1.UseVisualStyleBackColor = true;
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
            Load += SettingsControl_Load;
            metroTabControl1.ResumeLayout(false);
            TabPageTrion.ResumeLayout(false);
            TabPageTrion.PerformLayout();
            TabPageCore.ResumeLayout(false);
            TabPageCore.PerformLayout();
            TabPageMysql.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private TabPage TabPageTrion;
        private TabPage TabPageCore;
        private TabPage TabPageMysql;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private UI.Controls.CustomButton customButton3;
        private UI.Controls.CustomButton customButton1;
        private Panel panel3;
        private MetroFramework.Controls.MetroTextBox TXTBoxCoreExecLocation;
        private MetroFramework.Controls.MetroTextBox TXTBoxMySQLExecLocation;
        private Label label4;
        private Label label3;
        private UI.Controls.CustomButton BTNMySQLExecLovation;
        private UI.Controls.CustomButton BTNCoreExecLovation;
        private TrionControlPanel.UI.CustomToggleButton TGLStayInTrey;
        private Label label7;
        private Label label6;
        private Label label5;
        private TrionControlPanel.UI.CustomToggleButton TGLHideConsole;
        private TrionControlPanel.UI.CustomToggleButton TGLNotificationSound;
        private UI.Controls.CustomButton BTNCoreOpenFolder;
        private Label label10;
        private MetroFramework.Controls.MetroTextBox TXTBoxMySQLExecName;
        private Label label9;
        private MetroFramework.Controls.MetroTextBox TXTBoxLoginExecName;
        private Label label8;
        private MetroFramework.Controls.MetroTextBox TXTBoxWorldExecName;
        private Label label1;
        private TrionControlPanel.UI.CustomComboBox ComboBoxCores;
        private UI.Controls.CustomButton BTNMySQLOpenFolder;
        private Label label11;
        private TrionControlPanel.UI.CustomToggleButton customToggleButton1;
    }
}
