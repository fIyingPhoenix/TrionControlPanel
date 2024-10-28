namespace TrionControlPanelDesktop.Controls
{
    partial class DatabaseControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseControl));
            TabControl1 = new MetroFramework.Controls.MetroTabControl();
            tPageAccount = new TabPage();
            metroPanel8 = new MetroFramework.Controls.MetroPanel();
            BTNResetPassword = new UI.Controls.CustomButton();
            label15 = new Label();
            label14 = new Label();
            metroTextBox5 = new MetroFramework.Controls.MetroTextBox();
            metroTextBox6 = new MetroFramework.Controls.MetroTextBox();
            metroPanel10 = new MetroFramework.Controls.MetroPanel();
            pictureBox6 = new PictureBox();
            label7 = new Label();
            metroPanel6 = new MetroFramework.Controls.MetroPanel();
            label13 = new Label();
            label11 = new Label();
            metroTextBox3 = new MetroFramework.Controls.MetroTextBox();
            metroTextBox4 = new MetroFramework.Controls.MetroTextBox();
            metroPanel7 = new MetroFramework.Controls.MetroPanel();
            pictureBox5 = new PictureBox();
            label6 = new Label();
            BTNSetGM = new UI.Controls.CustomButton();
            metroPanel9 = new MetroFramework.Controls.MetroPanel();
            LBLExpansion = new Label();
            TXTExpansion = new MetroFramework.Controls.MetroTextBox();
            label12 = new Label();
            label10 = new Label();
            label8 = new Label();
            TXTBoxCreateEmail = new MetroFramework.Controls.MetroTextBox();
            TXTBoxCreatePassword = new MetroFramework.Controls.MetroTextBox();
            TXTBoxCreateUser = new MetroFramework.Controls.MetroTextBox();
            BTNCreateAccount = new UI.Controls.CustomButton();
            metroPanel17 = new MetroFramework.Controls.MetroPanel();
            pictureBox4 = new PictureBox();
            label30 = new Label();
            tPageRealmList = new TabPage();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            metroPanel5 = new MetroFramework.Controls.MetroPanel();
            pictureBox3 = new PictureBox();
            label3 = new Label();
            BTNOpenIntern = new UI.Controls.CustomButton();
            BTNOpenPublic = new UI.Controls.CustomButton();
            BTNSaveData = new UI.Controls.CustomButton();
            BTNForcerefresh = new UI.Controls.CustomButton();
            metroPanel1 = new MetroFramework.Controls.MetroPanel();
            metroPanel20 = new MetroFramework.Controls.MetroPanel();
            pictureBox2 = new PictureBox();
            label5 = new Label();
            TXTDomainName = new MetroFramework.Controls.MetroTextBox();
            label4 = new Label();
            TXTInternIP = new MetroFramework.Controls.MetroTextBox();
            LBLInternIP = new Label();
            TXTPublicIP = new MetroFramework.Controls.MetroTextBox();
            LBLPublicIP = new Label();
            LBLRealmsAvailable = new Label();
            CBOXReamList = new TrionControlPanel.UI.CustomComboBox();
            metroPanel3 = new MetroFramework.Controls.MetroPanel();
            metroPanel4 = new MetroFramework.Controls.MetroPanel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            TXTRealmID = new MetroFramework.Controls.MetroTextBox();
            TXTRealmFlag = new MetroFramework.Controls.MetroTextBox();
            LBLRealmAddress = new Label();
            LBLRealmSubnetMask = new Label();
            label9 = new Label();
            TXTRealmName = new MetroFramework.Controls.MetroTextBox();
            LBLRealmLocalAddress = new Label();
            TXTRealmIcon = new MetroFramework.Controls.MetroTextBox();
            LBLRealmFlag = new Label();
            TXTRealmTime = new MetroFramework.Controls.MetroTextBox();
            LBLRealmName = new Label();
            TXTRealmLocalAddress = new MetroFramework.Controls.MetroTextBox();
            LBLRealmPort = new Label();
            TXTRealmAddress = new MetroFramework.Controls.MetroTextBox();
            LBLRealmID = new Label();
            LBLRealmIcon = new Label();
            TXTRealmSubnetMask = new MetroFramework.Controls.MetroTextBox();
            TXTRealmPort = new MetroFramework.Controls.MetroTextBox();
            TimerWacher = new System.Windows.Forms.Timer(components);
            TLTHome = new TrionUI.Controls.CustomToolTip();
            TimerRefreshData = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            TabControl1.SuspendLayout();
            tPageAccount.SuspendLayout();
            metroPanel8.SuspendLayout();
            metroPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            metroPanel6.SuspendLayout();
            metroPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            metroPanel9.SuspendLayout();
            metroPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            tPageRealmList.SuspendLayout();
            metroPanel2.SuspendLayout();
            metroPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            metroPanel1.SuspendLayout();
            metroPanel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            metroPanel3.SuspendLayout();
            metroPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Appearance = TabAppearance.Buttons;
            TabControl1.Controls.Add(tPageAccount);
            TabControl1.Controls.Add(tPageRealmList);
            TabControl1.CustomBackground = false;
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.FontSize = MetroFramework.MetroTabControlSize.Medium;
            TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            TabControl1.ItemSize = new Size(80, 40);
            TabControl1.Location = new Point(0, 0);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.Padding = new Point(0, 0);
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(845, 370);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            TabControl1.StyleManager = null;
            TabControl1.TabIndex = 2;
            TabControl1.TextAlign = ContentAlignment.MiddleLeft;
            TabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            TabControl1.UseStyleColors = true;
            // 
            // tPageAccount
            // 
            tPageAccount.BackColor = Color.FromArgb(45, 51, 59);
            tPageAccount.Controls.Add(metroPanel8);
            tPageAccount.Controls.Add(metroPanel6);
            tPageAccount.Controls.Add(metroPanel9);
            tPageAccount.Location = new Point(4, 44);
            tPageAccount.Name = "tPageAccount";
            tPageAccount.Size = new Size(837, 322);
            tPageAccount.TabIndex = 1;
            tPageAccount.Text = "Account";
            // 
            // metroPanel8
            // 
            metroPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            metroPanel8.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel8.Border = true;
            metroPanel8.BorderColor = Color.Black;
            metroPanel8.BorderSize = 1;
            metroPanel8.Controls.Add(label1);
            metroPanel8.Controls.Add(metroTextBox1);
            metroPanel8.Controls.Add(BTNResetPassword);
            metroPanel8.Controls.Add(label15);
            metroPanel8.Controls.Add(label14);
            metroPanel8.Controls.Add(metroTextBox5);
            metroPanel8.Controls.Add(metroTextBox6);
            metroPanel8.Controls.Add(metroPanel10);
            metroPanel8.CustomBackground = true;
            metroPanel8.HorizontalScrollbar = false;
            metroPanel8.HorizontalScrollbarBarColor = true;
            metroPanel8.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel8.HorizontalScrollbarSize = 10;
            metroPanel8.Location = new Point(570, 5);
            metroPanel8.Name = "metroPanel8";
            metroPanel8.Size = new Size(260, 312);
            metroPanel8.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel8.StyleManager = null;
            metroPanel8.TabIndex = 45;
            metroPanel8.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel8.VerticalScrollbar = false;
            metroPanel8.VerticalScrollbarBarColor = true;
            metroPanel8.VerticalScrollbarHighlightOnWheel = false;
            metroPanel8.VerticalScrollbarSize = 10;
            // 
            // BTNResetPassword
            // 
            BTNResetPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BTNResetPassword.BackColor = Color.FromArgb(28, 33, 40);
            BTNResetPassword.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNResetPassword.BorderColor = Color.FromArgb(0, 174, 219);
            BTNResetPassword.BorderRadius = 0;
            BTNResetPassword.BorderSize = 1;
            BTNResetPassword.Cursor = Cursors.Hand;
            BTNResetPassword.FlatAppearance.BorderSize = 0;
            BTNResetPassword.FlatStyle = FlatStyle.Flat;
            BTNResetPassword.ForeColor = Color.White;
            BTNResetPassword.Image = (Image)resources.GetObject("BTNResetPassword.Image");
            BTNResetPassword.ImageAlign = ContentAlignment.MiddleLeft;
            BTNResetPassword.Location = new Point(3, 277);
            BTNResetPassword.Name = "BTNResetPassword";
            BTNResetPassword.NotificationCount = 0;
            BTNResetPassword.RightToLeft = RightToLeft.No;
            BTNResetPassword.Size = new Size(254, 30);
            BTNResetPassword.TabIndex = 62;
            BTNResetPassword.Text = "Update";
            BTNResetPassword.TextColor = Color.White;
            BTNResetPassword.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top;
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(5, 100);
            label15.Name = "label15";
            label15.Size = new Size(60, 15);
            label15.TabIndex = 62;
            label15.Text = "Password:";
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top;
            label14.AutoSize = true;
            label14.ForeColor = Color.White;
            label14.Location = new Point(5, 50);
            label14.Name = "label14";
            label14.Size = new Size(63, 15);
            label14.TabIndex = 61;
            label14.Text = "Username:";
            // 
            // metroTextBox5
            // 
            metroTextBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroTextBox5.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox5.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox5.ForeColor = Color.White;
            metroTextBox5.Location = new Point(3, 118);
            metroTextBox5.Multiline = false;
            metroTextBox5.Name = "metroTextBox5";
            metroTextBox5.PasswordChar = '\0';
            metroTextBox5.ReadOnly = false;
            metroTextBox5.SelectedText = "";
            metroTextBox5.Size = new Size(254, 25);
            metroTextBox5.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox5.StyleManager = null;
            metroTextBox5.TabIndex = 60;
            metroTextBox5.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox5.UseStyleColors = true;
            // 
            // metroTextBox6
            // 
            metroTextBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroTextBox6.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox6.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox6.ForeColor = Color.White;
            metroTextBox6.Location = new Point(3, 68);
            metroTextBox6.Multiline = false;
            metroTextBox6.Name = "metroTextBox6";
            metroTextBox6.PasswordChar = '\0';
            metroTextBox6.ReadOnly = false;
            metroTextBox6.SelectedText = "";
            metroTextBox6.Size = new Size(254, 25);
            metroTextBox6.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox6.StyleManager = null;
            metroTextBox6.TabIndex = 59;
            metroTextBox6.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox6.UseStyleColors = true;
            // 
            // metroPanel10
            // 
            metroPanel10.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel10.Border = true;
            metroPanel10.BorderColor = Color.Black;
            metroPanel10.BorderSize = 1;
            metroPanel10.Controls.Add(pictureBox6);
            metroPanel10.Controls.Add(label7);
            metroPanel10.CustomBackground = true;
            metroPanel10.Dock = DockStyle.Top;
            metroPanel10.HorizontalScrollbar = true;
            metroPanel10.HorizontalScrollbarBarColor = true;
            metroPanel10.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel10.HorizontalScrollbarSize = 10;
            metroPanel10.Location = new Point(0, 0);
            metroPanel10.Name = "metroPanel10";
            metroPanel10.Padding = new Padding(2);
            metroPanel10.Size = new Size(260, 30);
            metroPanel10.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel10.StyleManager = null;
            metroPanel10.TabIndex = 54;
            metroPanel10.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel10.VerticalScrollbar = true;
            metroPanel10.VerticalScrollbarBarColor = true;
            metroPanel10.VerticalScrollbarHighlightOnWheel = false;
            metroPanel10.VerticalScrollbarSize = 10;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(233, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(23, 23);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 63;
            pictureBox6.TabStop = false;
            TLTHome.SetToolTip(pictureBox6, "Edit Realmlist data in the Database");
            // 
            // label7
            // 
            label7.BackColor = Color.Transparent;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(0, 174, 219);
            label7.Location = new Point(2, 2);
            label7.Name = "label7";
            label7.Padding = new Padding(1);
            label7.Size = new Size(256, 26);
            label7.TabIndex = 43;
            label7.Text = "RESET PASSWORD";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // metroPanel6
            // 
            metroPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            metroPanel6.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel6.Border = true;
            metroPanel6.BorderColor = Color.Black;
            metroPanel6.BorderSize = 1;
            metroPanel6.Controls.Add(label13);
            metroPanel6.Controls.Add(label11);
            metroPanel6.Controls.Add(metroTextBox3);
            metroPanel6.Controls.Add(metroTextBox4);
            metroPanel6.Controls.Add(metroPanel7);
            metroPanel6.Controls.Add(BTNSetGM);
            metroPanel6.CustomBackground = true;
            metroPanel6.HorizontalScrollbar = false;
            metroPanel6.HorizontalScrollbarBarColor = true;
            metroPanel6.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel6.HorizontalScrollbarSize = 10;
            metroPanel6.Location = new Point(290, 5);
            metroPanel6.Name = "metroPanel6";
            metroPanel6.Size = new Size(275, 312);
            metroPanel6.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel6.StyleManager = null;
            metroPanel6.TabIndex = 44;
            metroPanel6.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel6.VerticalScrollbar = false;
            metroPanel6.VerticalScrollbarBarColor = true;
            metroPanel6.VerticalScrollbarHighlightOnWheel = false;
            metroPanel6.VerticalScrollbarSize = 10;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top;
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(5, 100);
            label13.Name = "label13";
            label13.Size = new Size(61, 15);
            label13.TabIndex = 60;
            label13.Text = "GM-Level:";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top;
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(5, 50);
            label11.Name = "label11";
            label11.Size = new Size(63, 15);
            label11.TabIndex = 59;
            label11.Text = "Username:";
            // 
            // metroTextBox3
            // 
            metroTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroTextBox3.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox3.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox3.ForeColor = Color.White;
            metroTextBox3.Location = new Point(3, 118);
            metroTextBox3.Multiline = false;
            metroTextBox3.Name = "metroTextBox3";
            metroTextBox3.PasswordChar = '\0';
            metroTextBox3.ReadOnly = false;
            metroTextBox3.SelectedText = "";
            metroTextBox3.Size = new Size(269, 25);
            metroTextBox3.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox3.StyleManager = null;
            metroTextBox3.TabIndex = 58;
            metroTextBox3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox3.UseStyleColors = true;
            // 
            // metroTextBox4
            // 
            metroTextBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroTextBox4.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox4.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox4.ForeColor = Color.White;
            metroTextBox4.Location = new Point(3, 68);
            metroTextBox4.Multiline = false;
            metroTextBox4.Name = "metroTextBox4";
            metroTextBox4.PasswordChar = '\0';
            metroTextBox4.ReadOnly = false;
            metroTextBox4.SelectedText = "";
            metroTextBox4.Size = new Size(269, 25);
            metroTextBox4.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox4.StyleManager = null;
            metroTextBox4.TabIndex = 57;
            metroTextBox4.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox4.UseStyleColors = true;
            // 
            // metroPanel7
            // 
            metroPanel7.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel7.Border = true;
            metroPanel7.BorderColor = Color.Black;
            metroPanel7.BorderSize = 1;
            metroPanel7.Controls.Add(pictureBox5);
            metroPanel7.Controls.Add(label6);
            metroPanel7.CustomBackground = true;
            metroPanel7.Dock = DockStyle.Top;
            metroPanel7.HorizontalScrollbar = true;
            metroPanel7.HorizontalScrollbarBarColor = true;
            metroPanel7.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel7.HorizontalScrollbarSize = 10;
            metroPanel7.Location = new Point(0, 0);
            metroPanel7.Name = "metroPanel7";
            metroPanel7.Padding = new Padding(2);
            metroPanel7.Size = new Size(275, 30);
            metroPanel7.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel7.StyleManager = null;
            metroPanel7.TabIndex = 54;
            metroPanel7.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel7.VerticalScrollbar = true;
            metroPanel7.VerticalScrollbarBarColor = true;
            metroPanel7.VerticalScrollbarHighlightOnWheel = false;
            metroPanel7.VerticalScrollbarSize = 10;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(249, 3);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(23, 23);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 61;
            pictureBox5.TabStop = false;
            TLTHome.SetToolTip(pictureBox5, "Edit Realmlist data in the Database");
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(0, 174, 219);
            label6.Location = new Point(2, 2);
            label6.Name = "label6";
            label6.Padding = new Padding(1);
            label6.Size = new Size(271, 26);
            label6.TabIndex = 43;
            label6.Text = "SET GM";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNSetGM
            // 
            BTNSetGM.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BTNSetGM.BackColor = Color.FromArgb(28, 33, 40);
            BTNSetGM.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNSetGM.BorderColor = Color.FromArgb(0, 174, 219);
            BTNSetGM.BorderRadius = 0;
            BTNSetGM.BorderSize = 1;
            BTNSetGM.Cursor = Cursors.Hand;
            BTNSetGM.FlatAppearance.BorderSize = 0;
            BTNSetGM.FlatStyle = FlatStyle.Flat;
            BTNSetGM.ForeColor = Color.White;
            BTNSetGM.Image = (Image)resources.GetObject("BTNSetGM.Image");
            BTNSetGM.ImageAlign = ContentAlignment.MiddleLeft;
            BTNSetGM.Location = new Point(3, 277);
            BTNSetGM.Name = "BTNSetGM";
            BTNSetGM.NotificationCount = 0;
            BTNSetGM.RightToLeft = RightToLeft.No;
            BTNSetGM.Size = new Size(269, 30);
            BTNSetGM.TabIndex = 46;
            BTNSetGM.Text = "Update";
            BTNSetGM.TextColor = Color.White;
            BTNSetGM.UseVisualStyleBackColor = false;
            // 
            // metroPanel9
            // 
            metroPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            metroPanel9.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel9.Border = true;
            metroPanel9.BorderColor = Color.Black;
            metroPanel9.BorderSize = 1;
            metroPanel9.Controls.Add(LBLExpansion);
            metroPanel9.Controls.Add(TXTExpansion);
            metroPanel9.Controls.Add(label12);
            metroPanel9.Controls.Add(label10);
            metroPanel9.Controls.Add(label8);
            metroPanel9.Controls.Add(TXTBoxCreateEmail);
            metroPanel9.Controls.Add(TXTBoxCreatePassword);
            metroPanel9.Controls.Add(TXTBoxCreateUser);
            metroPanel9.Controls.Add(BTNCreateAccount);
            metroPanel9.Controls.Add(metroPanel17);
            metroPanel9.CustomBackground = true;
            metroPanel9.HorizontalScrollbar = false;
            metroPanel9.HorizontalScrollbarBarColor = true;
            metroPanel9.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel9.HorizontalScrollbarSize = 10;
            metroPanel9.Location = new Point(5, 5);
            metroPanel9.Name = "metroPanel9";
            metroPanel9.Size = new Size(280, 312);
            metroPanel9.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel9.StyleManager = null;
            metroPanel9.TabIndex = 43;
            metroPanel9.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel9.VerticalScrollbar = false;
            metroPanel9.VerticalScrollbarBarColor = true;
            metroPanel9.VerticalScrollbarHighlightOnWheel = false;
            metroPanel9.VerticalScrollbarSize = 10;
            // 
            // LBLExpansion
            // 
            LBLExpansion.Anchor = AnchorStyles.Top;
            LBLExpansion.AutoSize = true;
            LBLExpansion.ForeColor = Color.White;
            LBLExpansion.Location = new Point(4, 201);
            LBLExpansion.Name = "LBLExpansion";
            LBLExpansion.Size = new Size(64, 15);
            LBLExpansion.TabIndex = 62;
            LBLExpansion.Text = "Expansion:";
            // 
            // TXTExpansion
            // 
            TXTExpansion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TXTExpansion.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTExpansion.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTExpansion.ForeColor = Color.White;
            TXTExpansion.Location = new Point(3, 219);
            TXTExpansion.Multiline = false;
            TXTExpansion.Name = "TXTExpansion";
            TXTExpansion.PasswordChar = '\0';
            TXTExpansion.ReadOnly = false;
            TXTExpansion.SelectedText = "";
            TXTExpansion.Size = new Size(274, 25);
            TXTExpansion.Style = MetroFramework.MetroColorStyle.Blue;
            TXTExpansion.StyleManager = null;
            TXTExpansion.TabIndex = 61;
            TXTExpansion.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTExpansion.UseStyleColors = true;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top;
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(5, 151);
            label12.Name = "label12";
            label12.Size = new Size(39, 15);
            label12.TabIndex = 60;
            label12.Text = "Email:";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top;
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(4, 100);
            label10.Name = "label10";
            label10.Size = new Size(60, 15);
            label10.TabIndex = 59;
            label10.Text = "Password:";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(5, 50);
            label8.Name = "label8";
            label8.Size = new Size(63, 15);
            label8.TabIndex = 58;
            label8.Text = "Username:";
            // 
            // TXTBoxCreateEmail
            // 
            TXTBoxCreateEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxCreateEmail.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxCreateEmail.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxCreateEmail.ForeColor = Color.White;
            TXTBoxCreateEmail.Location = new Point(3, 168);
            TXTBoxCreateEmail.Multiline = false;
            TXTBoxCreateEmail.Name = "TXTBoxCreateEmail";
            TXTBoxCreateEmail.PasswordChar = '\0';
            TXTBoxCreateEmail.ReadOnly = false;
            TXTBoxCreateEmail.SelectedText = "";
            TXTBoxCreateEmail.Size = new Size(274, 25);
            TXTBoxCreateEmail.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxCreateEmail.StyleManager = null;
            TXTBoxCreateEmail.TabIndex = 57;
            TXTBoxCreateEmail.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxCreateEmail.UseStyleColors = true;
            // 
            // TXTBoxCreatePassword
            // 
            TXTBoxCreatePassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxCreatePassword.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxCreatePassword.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxCreatePassword.ForeColor = Color.White;
            TXTBoxCreatePassword.Location = new Point(3, 118);
            TXTBoxCreatePassword.Multiline = false;
            TXTBoxCreatePassword.Name = "TXTBoxCreatePassword";
            TXTBoxCreatePassword.PasswordChar = '\0';
            TXTBoxCreatePassword.ReadOnly = false;
            TXTBoxCreatePassword.SelectedText = "";
            TXTBoxCreatePassword.Size = new Size(274, 25);
            TXTBoxCreatePassword.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxCreatePassword.StyleManager = null;
            TXTBoxCreatePassword.TabIndex = 56;
            TXTBoxCreatePassword.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxCreatePassword.UseStyleColors = true;
            // 
            // TXTBoxCreateUser
            // 
            TXTBoxCreateUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TXTBoxCreateUser.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTBoxCreateUser.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTBoxCreateUser.ForeColor = Color.White;
            TXTBoxCreateUser.Location = new Point(3, 68);
            TXTBoxCreateUser.Multiline = false;
            TXTBoxCreateUser.Name = "TXTBoxCreateUser";
            TXTBoxCreateUser.PasswordChar = '\0';
            TXTBoxCreateUser.ReadOnly = false;
            TXTBoxCreateUser.SelectedText = "";
            TXTBoxCreateUser.Size = new Size(274, 25);
            TXTBoxCreateUser.Style = MetroFramework.MetroColorStyle.Blue;
            TXTBoxCreateUser.StyleManager = null;
            TXTBoxCreateUser.TabIndex = 55;
            TXTBoxCreateUser.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTBoxCreateUser.UseStyleColors = true;
            // 
            // BTNCreateAccount
            // 
            BTNCreateAccount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BTNCreateAccount.BackColor = Color.FromArgb(28, 33, 40);
            BTNCreateAccount.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNCreateAccount.BorderColor = Color.FromArgb(0, 174, 219);
            BTNCreateAccount.BorderRadius = 0;
            BTNCreateAccount.BorderSize = 1;
            BTNCreateAccount.Cursor = Cursors.Hand;
            BTNCreateAccount.FlatAppearance.BorderSize = 0;
            BTNCreateAccount.FlatStyle = FlatStyle.Flat;
            BTNCreateAccount.ForeColor = Color.White;
            BTNCreateAccount.Image = (Image)resources.GetObject("BTNCreateAccount.Image");
            BTNCreateAccount.ImageAlign = ContentAlignment.MiddleLeft;
            BTNCreateAccount.Location = new Point(3, 277);
            BTNCreateAccount.Name = "BTNCreateAccount";
            BTNCreateAccount.NotificationCount = 0;
            BTNCreateAccount.RightToLeft = RightToLeft.No;
            BTNCreateAccount.Size = new Size(274, 31);
            BTNCreateAccount.TabIndex = 45;
            BTNCreateAccount.Text = "Create";
            BTNCreateAccount.TextColor = Color.White;
            BTNCreateAccount.UseVisualStyleBackColor = false;
            BTNCreateAccount.Click += BTNCreateAccount_Click;
            // 
            // metroPanel17
            // 
            metroPanel17.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel17.Border = true;
            metroPanel17.BorderColor = Color.Black;
            metroPanel17.BorderSize = 1;
            metroPanel17.Controls.Add(pictureBox4);
            metroPanel17.Controls.Add(label30);
            metroPanel17.CustomBackground = true;
            metroPanel17.Dock = DockStyle.Top;
            metroPanel17.HorizontalScrollbar = true;
            metroPanel17.HorizontalScrollbarBarColor = true;
            metroPanel17.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel17.HorizontalScrollbarSize = 10;
            metroPanel17.Location = new Point(0, 0);
            metroPanel17.Name = "metroPanel17";
            metroPanel17.Padding = new Padding(2);
            metroPanel17.Size = new Size(280, 30);
            metroPanel17.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel17.StyleManager = null;
            metroPanel17.TabIndex = 54;
            metroPanel17.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel17.VerticalScrollbar = true;
            metroPanel17.VerticalScrollbarBarColor = true;
            metroPanel17.VerticalScrollbarHighlightOnWheel = false;
            metroPanel17.VerticalScrollbarSize = 10;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(253, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(23, 23);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 61;
            pictureBox4.TabStop = false;
            TLTHome.SetToolTip(pictureBox4, "Edit Realmlist data in the Database");
            // 
            // label30
            // 
            label30.BackColor = Color.Transparent;
            label30.Dock = DockStyle.Fill;
            label30.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label30.ForeColor = Color.FromArgb(0, 174, 219);
            label30.Location = new Point(2, 2);
            label30.Name = "label30";
            label30.Padding = new Padding(1);
            label30.Size = new Size(276, 26);
            label30.TabIndex = 43;
            label30.Text = "ACCOUNT CREATE";
            label30.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tPageRealmList
            // 
            tPageRealmList.BackColor = Color.FromArgb(45, 51, 59);
            tPageRealmList.Controls.Add(metroPanel2);
            tPageRealmList.Controls.Add(metroPanel1);
            tPageRealmList.Controls.Add(metroPanel3);
            tPageRealmList.Location = new Point(4, 44);
            tPageRealmList.Name = "tPageRealmList";
            tPageRealmList.Size = new Size(837, 322);
            tPageRealmList.TabIndex = 0;
            tPageRealmList.Text = "Realm List";
            // 
            // metroPanel2
            // 
            metroPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            metroPanel2.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(metroPanel5);
            metroPanel2.Controls.Add(BTNOpenIntern);
            metroPanel2.Controls.Add(BTNOpenPublic);
            metroPanel2.Controls.Add(BTNSaveData);
            metroPanel2.Controls.Add(BTNForcerefresh);
            metroPanel2.CustomBackground = true;
            metroPanel2.HorizontalScrollbar = false;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(517, 5);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(315, 310);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 46;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = false;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // metroPanel5
            // 
            metroPanel5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel5.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel5.Border = true;
            metroPanel5.BorderColor = Color.Black;
            metroPanel5.BorderSize = 1;
            metroPanel5.Controls.Add(pictureBox3);
            metroPanel5.Controls.Add(label3);
            metroPanel5.CustomBackground = true;
            metroPanel5.HorizontalScrollbar = true;
            metroPanel5.HorizontalScrollbarBarColor = true;
            metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel5.HorizontalScrollbarSize = 10;
            metroPanel5.Location = new Point(0, 0);
            metroPanel5.Name = "metroPanel5";
            metroPanel5.Padding = new Padding(2);
            metroPanel5.Size = new Size(315, 30);
            metroPanel5.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel5.StyleManager = null;
            metroPanel5.TabIndex = 58;
            metroPanel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel5.VerticalScrollbar = true;
            metroPanel5.VerticalScrollbarBarColor = true;
            metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            metroPanel5.VerticalScrollbarSize = 10;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(289, 4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(23, 23);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 52;
            pictureBox3.TabStop = false;
            TLTHome.SetToolTip(pictureBox3, "Database actions");
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(0, 174, 219);
            label3.Location = new Point(2, 2);
            label3.Name = "label3";
            label3.Size = new Size(311, 26);
            label3.TabIndex = 51;
            label3.Text = "ACTIONS";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BTNOpenIntern
            // 
            BTNOpenIntern.Anchor = AnchorStyles.Top;
            BTNOpenIntern.BackColor = Color.FromArgb(28, 33, 40);
            BTNOpenIntern.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNOpenIntern.BorderColor = Color.FromArgb(0, 174, 219);
            BTNOpenIntern.BorderRadius = 0;
            BTNOpenIntern.BorderSize = 1;
            BTNOpenIntern.Cursor = Cursors.Hand;
            BTNOpenIntern.FlatAppearance.BorderSize = 0;
            BTNOpenIntern.FlatStyle = FlatStyle.Flat;
            BTNOpenIntern.ForeColor = Color.White;
            BTNOpenIntern.Image = (Image)resources.GetObject("BTNOpenIntern.Image");
            BTNOpenIntern.ImageAlign = ContentAlignment.MiddleLeft;
            BTNOpenIntern.Location = new Point(5, 187);
            BTNOpenIntern.Name = "BTNOpenIntern";
            BTNOpenIntern.NotificationCount = 0;
            BTNOpenIntern.RightToLeft = RightToLeft.No;
            BTNOpenIntern.Size = new Size(305, 30);
            BTNOpenIntern.TabIndex = 53;
            BTNOpenIntern.Text = "Open Internal";
            BTNOpenIntern.TextColor = Color.White;
            TLTHome.SetToolTip(BTNOpenIntern, "Change the Address to the Internal IP");
            BTNOpenIntern.UseVisualStyleBackColor = false;
            // 
            // BTNOpenPublic
            // 
            BTNOpenPublic.Anchor = AnchorStyles.Top;
            BTNOpenPublic.BackColor = Color.FromArgb(28, 33, 40);
            BTNOpenPublic.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNOpenPublic.BorderColor = Color.FromArgb(0, 174, 219);
            BTNOpenPublic.BorderRadius = 0;
            BTNOpenPublic.BorderSize = 1;
            BTNOpenPublic.Cursor = Cursors.Hand;
            BTNOpenPublic.FlatAppearance.BorderSize = 0;
            BTNOpenPublic.FlatStyle = FlatStyle.Flat;
            BTNOpenPublic.ForeColor = Color.White;
            BTNOpenPublic.Image = (Image)resources.GetObject("BTNOpenPublic.Image");
            BTNOpenPublic.ImageAlign = ContentAlignment.MiddleLeft;
            BTNOpenPublic.Location = new Point(5, 151);
            BTNOpenPublic.Name = "BTNOpenPublic";
            BTNOpenPublic.NotificationCount = 0;
            BTNOpenPublic.RightToLeft = RightToLeft.No;
            BTNOpenPublic.Size = new Size(305, 30);
            BTNOpenPublic.TabIndex = 54;
            BTNOpenPublic.Text = "  Open Public";
            BTNOpenPublic.TextColor = Color.White;
            TLTHome.SetToolTip(BTNOpenPublic, "Change the Address to the Public IP or Domain Name if exists");
            BTNOpenPublic.UseVisualStyleBackColor = false;
            // 
            // BTNSaveData
            // 
            BTNSaveData.Anchor = AnchorStyles.Top;
            BTNSaveData.BackColor = Color.FromArgb(28, 33, 40);
            BTNSaveData.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNSaveData.BorderColor = Color.FromArgb(0, 174, 219);
            BTNSaveData.BorderRadius = 0;
            BTNSaveData.BorderSize = 1;
            BTNSaveData.Cursor = Cursors.Hand;
            BTNSaveData.FlatAppearance.BorderSize = 0;
            BTNSaveData.FlatStyle = FlatStyle.Flat;
            BTNSaveData.ForeColor = Color.White;
            BTNSaveData.Image = (Image)resources.GetObject("BTNSaveData.Image");
            BTNSaveData.ImageAlign = ContentAlignment.MiddleLeft;
            BTNSaveData.Location = new Point(5, 224);
            BTNSaveData.Name = "BTNSaveData";
            BTNSaveData.NotificationCount = 0;
            BTNSaveData.RightToLeft = RightToLeft.No;
            BTNSaveData.Size = new Size(305, 30);
            BTNSaveData.TabIndex = 52;
            BTNSaveData.Text = "  Save";
            BTNSaveData.TextColor = Color.White;
            TLTHome.SetToolTip(BTNSaveData, "Save selected/modified realm list data");
            BTNSaveData.UseVisualStyleBackColor = false;
            BTNSaveData.Click += BTNSaveData_ClickAsync;
            // 
            // BTNForcerefresh
            // 
            BTNForcerefresh.Anchor = AnchorStyles.Top;
            BTNForcerefresh.BackColor = Color.FromArgb(28, 33, 40);
            BTNForcerefresh.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNForcerefresh.BorderColor = Color.FromArgb(0, 174, 219);
            BTNForcerefresh.BorderRadius = 0;
            BTNForcerefresh.BorderSize = 1;
            BTNForcerefresh.Cursor = Cursors.Hand;
            BTNForcerefresh.FlatAppearance.BorderSize = 0;
            BTNForcerefresh.FlatStyle = FlatStyle.Flat;
            BTNForcerefresh.ForeColor = Color.White;
            BTNForcerefresh.Image = (Image)resources.GetObject("BTNForcerefresh.Image");
            BTNForcerefresh.ImageAlign = ContentAlignment.MiddleLeft;
            BTNForcerefresh.Location = new Point(5, 260);
            BTNForcerefresh.Name = "BTNForcerefresh";
            BTNForcerefresh.NotificationCount = 0;
            BTNForcerefresh.RightToLeft = RightToLeft.No;
            BTNForcerefresh.Size = new Size(305, 30);
            BTNForcerefresh.TabIndex = 28;
            BTNForcerefresh.Text = "   Force Refresh";
            BTNForcerefresh.TextColor = Color.White;
            TLTHome.SetToolTip(BTNForcerefresh, "Refresh Database data");
            BTNForcerefresh.UseVisualStyleBackColor = false;
            BTNForcerefresh.Click += BTNForceUpdate_Click;
            // 
            // metroPanel1
            // 
            metroPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroPanel1.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(metroPanel20);
            metroPanel1.Controls.Add(TXTDomainName);
            metroPanel1.Controls.Add(label4);
            metroPanel1.Controls.Add(TXTInternIP);
            metroPanel1.Controls.Add(LBLInternIP);
            metroPanel1.Controls.Add(TXTPublicIP);
            metroPanel1.Controls.Add(LBLPublicIP);
            metroPanel1.Controls.Add(LBLRealmsAvailable);
            metroPanel1.Controls.Add(CBOXReamList);
            metroPanel1.CustomBackground = true;
            metroPanel1.HorizontalScrollbar = false;
            metroPanel1.HorizontalScrollbarBarColor = true;
            metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel1.HorizontalScrollbarSize = 10;
            metroPanel1.Location = new Point(261, 5);
            metroPanel1.Name = "metroPanel1";
            metroPanel1.Padding = new Padding(2);
            metroPanel1.Size = new Size(250, 310);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 45;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel20
            // 
            metroPanel20.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel20.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel20.Border = true;
            metroPanel20.BorderColor = Color.Black;
            metroPanel20.BorderSize = 1;
            metroPanel20.Controls.Add(pictureBox2);
            metroPanel20.Controls.Add(label5);
            metroPanel20.CustomBackground = true;
            metroPanel20.HorizontalScrollbar = true;
            metroPanel20.HorizontalScrollbarBarColor = true;
            metroPanel20.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel20.HorizontalScrollbarSize = 10;
            metroPanel20.Location = new Point(0, 0);
            metroPanel20.Name = "metroPanel20";
            metroPanel20.Padding = new Padding(2);
            metroPanel20.Size = new Size(250, 30);
            metroPanel20.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel20.StyleManager = null;
            metroPanel20.TabIndex = 57;
            metroPanel20.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel20.VerticalScrollbar = true;
            metroPanel20.VerticalScrollbarBarColor = true;
            metroPanel20.VerticalScrollbarHighlightOnWheel = false;
            metroPanel20.VerticalScrollbarSize = 10;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(224, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(23, 23);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 51;
            pictureBox2.TabStop = false;
            TLTHome.SetToolTip(pictureBox2, "Edit selected realm list");
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(0, 174, 219);
            label5.Location = new Point(2, 2);
            label5.Name = "label5";
            label5.Size = new Size(246, 26);
            label5.TabIndex = 50;
            label5.Text = "OPTIONS";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TXTDomainName
            // 
            TXTDomainName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTDomainName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTDomainName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTDomainName.ForeColor = Color.White;
            TXTDomainName.Location = new Point(5, 172);
            TXTDomainName.Multiline = false;
            TXTDomainName.Name = "TXTDomainName";
            TXTDomainName.PasswordChar = '\0';
            TXTDomainName.ReadOnly = true;
            TXTDomainName.SelectedText = "";
            TXTDomainName.Size = new Size(240, 25);
            TXTDomainName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTDomainName.StyleManager = null;
            TXTDomainName.TabIndex = 51;
            TXTDomainName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTDomainName.UseStyleColors = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(5, 152);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 52;
            label4.Text = "Domain Name:";
            // 
            // TXTInternIP
            // 
            TXTInternIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTInternIP.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTInternIP.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTInternIP.ForeColor = Color.White;
            TXTInternIP.Location = new Point(5, 221);
            TXTInternIP.Multiline = false;
            TXTInternIP.Name = "TXTInternIP";
            TXTInternIP.PasswordChar = '\0';
            TXTInternIP.ReadOnly = true;
            TXTInternIP.SelectedText = "";
            TXTInternIP.Size = new Size(240, 25);
            TXTInternIP.Style = MetroFramework.MetroColorStyle.Blue;
            TXTInternIP.StyleManager = null;
            TXTInternIP.TabIndex = 48;
            TXTInternIP.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTInternIP.UseStyleColors = true;
            // 
            // LBLInternIP
            // 
            LBLInternIP.AutoSize = true;
            LBLInternIP.ForeColor = Color.White;
            LBLInternIP.Location = new Point(5, 202);
            LBLInternIP.Name = "LBLInternIP";
            LBLInternIP.Size = new Size(105, 15);
            LBLInternIP.TabIndex = 49;
            LBLInternIP.Text = "Internal IP Address";
            // 
            // TXTPublicIP
            // 
            TXTPublicIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTPublicIP.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTPublicIP.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTPublicIP.ForeColor = Color.White;
            TXTPublicIP.Location = new Point(5, 266);
            TXTPublicIP.Multiline = false;
            TXTPublicIP.Name = "TXTPublicIP";
            TXTPublicIP.PasswordChar = '\0';
            TXTPublicIP.ReadOnly = true;
            TXTPublicIP.SelectedText = "";
            TXTPublicIP.Size = new Size(240, 25);
            TXTPublicIP.Style = MetroFramework.MetroColorStyle.Blue;
            TXTPublicIP.StyleManager = null;
            TXTPublicIP.TabIndex = 46;
            TXTPublicIP.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTPublicIP.UseStyleColors = true;
            // 
            // LBLPublicIP
            // 
            LBLPublicIP.AutoSize = true;
            LBLPublicIP.ForeColor = Color.White;
            LBLPublicIP.Location = new Point(5, 247);
            LBLPublicIP.Name = "LBLPublicIP";
            LBLPublicIP.Size = new Size(101, 15);
            LBLPublicIP.TabIndex = 47;
            LBLPublicIP.Text = "Public IP Address:";
            // 
            // LBLRealmsAvailable
            // 
            LBLRealmsAvailable.AutoSize = true;
            LBLRealmsAvailable.ForeColor = Color.White;
            LBLRealmsAvailable.Location = new Point(4, 58);
            LBLRealmsAvailable.Name = "LBLRealmsAvailable";
            LBLRealmsAvailable.Size = new Size(99, 15);
            LBLRealmsAvailable.TabIndex = 44;
            LBLRealmsAvailable.Text = "Realms Available:";
            // 
            // CBOXReamList
            // 
            CBOXReamList.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            CBOXReamList.BackColor = Color.FromArgb(34, 34, 34);
            CBOXReamList.BorderColor = Color.FromArgb(0, 174, 219);
            CBOXReamList.BorderSize = 1;
            CBOXReamList.DropDownStyle = ComboBoxStyle.DropDownList;
            CBOXReamList.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            CBOXReamList.ForeColor = Color.White;
            CBOXReamList.IconColor = Color.FromArgb(0, 174, 219);
            CBOXReamList.ListBackColor = Color.FromArgb(34, 34, 34);
            CBOXReamList.ListTextColor = Color.FromArgb(0, 174, 219);
            CBOXReamList.Location = new Point(4, 77);
            CBOXReamList.MinimumSize = new Size(200, 27);
            CBOXReamList.Name = "CBOXReamList";
            CBOXReamList.Padding = new Padding(1);
            CBOXReamList.Size = new Size(241, 27);
            CBOXReamList.TabIndex = 42;
            CBOXReamList.Texts = "";
            CBOXReamList.OnSelectedIndexChanged += CBOXReamList_OnSelectedIndexChanged;
            // 
            // metroPanel3
            // 
            metroPanel3.BackColor = Color.FromArgb(34, 39, 46);
            metroPanel3.Border = true;
            metroPanel3.BorderColor = Color.Black;
            metroPanel3.BorderSize = 1;
            metroPanel3.Controls.Add(metroPanel4);
            metroPanel3.Controls.Add(TXTRealmID);
            metroPanel3.Controls.Add(TXTRealmFlag);
            metroPanel3.Controls.Add(LBLRealmAddress);
            metroPanel3.Controls.Add(LBLRealmSubnetMask);
            metroPanel3.Controls.Add(label9);
            metroPanel3.Controls.Add(TXTRealmName);
            metroPanel3.Controls.Add(LBLRealmLocalAddress);
            metroPanel3.Controls.Add(TXTRealmIcon);
            metroPanel3.Controls.Add(LBLRealmFlag);
            metroPanel3.Controls.Add(TXTRealmTime);
            metroPanel3.Controls.Add(LBLRealmName);
            metroPanel3.Controls.Add(TXTRealmLocalAddress);
            metroPanel3.Controls.Add(LBLRealmPort);
            metroPanel3.Controls.Add(TXTRealmAddress);
            metroPanel3.Controls.Add(LBLRealmID);
            metroPanel3.Controls.Add(LBLRealmIcon);
            metroPanel3.Controls.Add(TXTRealmSubnetMask);
            metroPanel3.Controls.Add(TXTRealmPort);
            metroPanel3.CustomBackground = true;
            metroPanel3.HorizontalScrollbar = false;
            metroPanel3.HorizontalScrollbarBarColor = true;
            metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel3.HorizontalScrollbarSize = 10;
            metroPanel3.Location = new Point(5, 5);
            metroPanel3.Name = "metroPanel3";
            metroPanel3.Padding = new Padding(2);
            metroPanel3.Size = new Size(250, 310);
            metroPanel3.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel3.StyleManager = null;
            metroPanel3.TabIndex = 41;
            metroPanel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel3.VerticalScrollbar = false;
            metroPanel3.VerticalScrollbarBarColor = true;
            metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            metroPanel3.VerticalScrollbarSize = 10;
            // 
            // metroPanel4
            // 
            metroPanel4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel4.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel4.Border = true;
            metroPanel4.BorderColor = Color.Black;
            metroPanel4.BorderSize = 1;
            metroPanel4.Controls.Add(pictureBox1);
            metroPanel4.Controls.Add(label2);
            metroPanel4.CustomBackground = true;
            metroPanel4.HorizontalScrollbar = true;
            metroPanel4.HorizontalScrollbarBarColor = true;
            metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel4.HorizontalScrollbarSize = 10;
            metroPanel4.Location = new Point(0, 0);
            metroPanel4.Name = "metroPanel4";
            metroPanel4.Padding = new Padding(2);
            metroPanel4.Size = new Size(250, 30);
            metroPanel4.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel4.StyleManager = null;
            metroPanel4.TabIndex = 58;
            metroPanel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel4.VerticalScrollbar = true;
            metroPanel4.VerticalScrollbarBarColor = true;
            metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            metroPanel4.VerticalScrollbarSize = 10;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(224, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(23, 23);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            TLTHome.SetToolTip(pictureBox1, "Edit realm list data in the Database");
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 174, 219);
            label2.Location = new Point(2, 2);
            label2.Name = "label2";
            label2.Size = new Size(246, 26);
            label2.TabIndex = 19;
            label2.Text = "DATA";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TXTRealmID
            // 
            TXTRealmID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmID.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmID.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmID.ForeColor = Color.White;
            TXTRealmID.Location = new Point(5, 77);
            TXTRealmID.Multiline = false;
            TXTRealmID.Name = "TXTRealmID";
            TXTRealmID.PasswordChar = '\0';
            TXTRealmID.ReadOnly = false;
            TXTRealmID.SelectedText = "";
            TXTRealmID.Size = new Size(43, 25);
            TXTRealmID.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmID.StyleManager = null;
            TXTRealmID.TabIndex = 1;
            TXTRealmID.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmID.UseStyleColors = true;
            // 
            // TXTRealmFlag
            // 
            TXTRealmFlag.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmFlag.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmFlag.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmFlag.ForeColor = Color.White;
            TXTRealmFlag.Location = new Point(152, 267);
            TXTRealmFlag.Multiline = false;
            TXTRealmFlag.Name = "TXTRealmFlag";
            TXTRealmFlag.PasswordChar = '\0';
            TXTRealmFlag.ReadOnly = false;
            TXTRealmFlag.SelectedText = "";
            TXTRealmFlag.Size = new Size(38, 25);
            TXTRealmFlag.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmFlag.StyleManager = null;
            TXTRealmFlag.TabIndex = 15;
            TXTRealmFlag.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmFlag.UseStyleColors = true;
            // 
            // LBLRealmAddress
            // 
            LBLRealmAddress.AutoSize = true;
            LBLRealmAddress.ForeColor = Color.White;
            LBLRealmAddress.Location = new Point(5, 105);
            LBLRealmAddress.Name = "LBLRealmAddress";
            LBLRealmAddress.Size = new Size(52, 15);
            LBLRealmAddress.TabIndex = 6;
            LBLRealmAddress.Text = "Address:";
            // 
            // LBLRealmSubnetMask
            // 
            LBLRealmSubnetMask.AutoSize = true;
            LBLRealmSubnetMask.ForeColor = Color.White;
            LBLRealmSubnetMask.Location = new Point(5, 202);
            LBLRealmSubnetMask.Name = "LBLRealmSubnetMask";
            LBLRealmSubnetMask.Size = new Size(109, 15);
            LBLRealmSubnetMask.TabIndex = 10;
            LBLRealmSubnetMask.Text = "Local Subnet Mask:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(196, 247);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 18;
            label9.Text = "Time:";
            // 
            // TXTRealmName
            // 
            TXTRealmName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmName.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmName.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmName.ForeColor = Color.White;
            TXTRealmName.Location = new Point(53, 77);
            TXTRealmName.Multiline = false;
            TXTRealmName.Name = "TXTRealmName";
            TXTRealmName.PasswordChar = '\0';
            TXTRealmName.ReadOnly = false;
            TXTRealmName.SelectedText = "";
            TXTRealmName.Size = new Size(192, 25);
            TXTRealmName.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmName.StyleManager = null;
            TXTRealmName.TabIndex = 3;
            TXTRealmName.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmName.UseStyleColors = true;
            // 
            // LBLRealmLocalAddress
            // 
            LBLRealmLocalAddress.AutoSize = true;
            LBLRealmLocalAddress.ForeColor = Color.White;
            LBLRealmLocalAddress.Location = new Point(5, 152);
            LBLRealmLocalAddress.Name = "LBLRealmLocalAddress";
            LBLRealmLocalAddress.Size = new Size(83, 15);
            LBLRealmLocalAddress.TabIndex = 8;
            LBLRealmLocalAddress.Text = "Local Address:";
            // 
            // TXTRealmIcon
            // 
            TXTRealmIcon.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmIcon.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmIcon.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmIcon.ForeColor = Color.White;
            TXTRealmIcon.Location = new Point(108, 267);
            TXTRealmIcon.Multiline = false;
            TXTRealmIcon.Name = "TXTRealmIcon";
            TXTRealmIcon.PasswordChar = '\0';
            TXTRealmIcon.ReadOnly = false;
            TXTRealmIcon.SelectedText = "";
            TXTRealmIcon.Size = new Size(38, 25);
            TXTRealmIcon.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmIcon.StyleManager = null;
            TXTRealmIcon.TabIndex = 13;
            TXTRealmIcon.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmIcon.UseStyleColors = true;
            // 
            // LBLRealmFlag
            // 
            LBLRealmFlag.AutoSize = true;
            LBLRealmFlag.ForeColor = Color.White;
            LBLRealmFlag.Location = new Point(153, 247);
            LBLRealmFlag.Name = "LBLRealmFlag";
            LBLRealmFlag.Size = new Size(32, 15);
            LBLRealmFlag.TabIndex = 16;
            LBLRealmFlag.Text = "Flag:";
            // 
            // TXTRealmTime
            // 
            TXTRealmTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmTime.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmTime.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmTime.ForeColor = Color.White;
            TXTRealmTime.Location = new Point(195, 267);
            TXTRealmTime.Multiline = false;
            TXTRealmTime.Name = "TXTRealmTime";
            TXTRealmTime.PasswordChar = '\0';
            TXTRealmTime.ReadOnly = false;
            TXTRealmTime.SelectedText = "";
            TXTRealmTime.Size = new Size(50, 25);
            TXTRealmTime.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmTime.StyleManager = null;
            TXTRealmTime.TabIndex = 17;
            TXTRealmTime.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmTime.UseStyleColors = true;
            // 
            // LBLRealmName
            // 
            LBLRealmName.AutoSize = true;
            LBLRealmName.ForeColor = Color.White;
            LBLRealmName.Location = new Point(55, 58);
            LBLRealmName.Name = "LBLRealmName";
            LBLRealmName.Size = new Size(42, 15);
            LBLRealmName.TabIndex = 4;
            LBLRealmName.Text = "Name:";
            // 
            // TXTRealmLocalAddress
            // 
            TXTRealmLocalAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmLocalAddress.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmLocalAddress.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmLocalAddress.ForeColor = Color.White;
            TXTRealmLocalAddress.Location = new Point(5, 172);
            TXTRealmLocalAddress.Multiline = false;
            TXTRealmLocalAddress.Name = "TXTRealmLocalAddress";
            TXTRealmLocalAddress.PasswordChar = '\0';
            TXTRealmLocalAddress.ReadOnly = false;
            TXTRealmLocalAddress.SelectedText = "";
            TXTRealmLocalAddress.Size = new Size(240, 25);
            TXTRealmLocalAddress.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmLocalAddress.StyleManager = null;
            TXTRealmLocalAddress.TabIndex = 7;
            TXTRealmLocalAddress.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmLocalAddress.UseStyleColors = true;
            // 
            // LBLRealmPort
            // 
            LBLRealmPort.AutoSize = true;
            LBLRealmPort.ForeColor = Color.White;
            LBLRealmPort.Location = new Point(5, 249);
            LBLRealmPort.Name = "LBLRealmPort";
            LBLRealmPort.Size = new Size(32, 15);
            LBLRealmPort.TabIndex = 12;
            LBLRealmPort.Text = "Port:";
            // 
            // TXTRealmAddress
            // 
            TXTRealmAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmAddress.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmAddress.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmAddress.ForeColor = Color.White;
            TXTRealmAddress.Location = new Point(5, 123);
            TXTRealmAddress.Multiline = false;
            TXTRealmAddress.Name = "TXTRealmAddress";
            TXTRealmAddress.PasswordChar = '\0';
            TXTRealmAddress.ReadOnly = false;
            TXTRealmAddress.SelectedText = "";
            TXTRealmAddress.Size = new Size(240, 25);
            TXTRealmAddress.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmAddress.StyleManager = null;
            TXTRealmAddress.TabIndex = 5;
            TXTRealmAddress.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmAddress.UseStyleColors = true;
            // 
            // LBLRealmID
            // 
            LBLRealmID.AutoSize = true;
            LBLRealmID.ForeColor = Color.White;
            LBLRealmID.Location = new Point(5, 58);
            LBLRealmID.Name = "LBLRealmID";
            LBLRealmID.Size = new Size(18, 15);
            LBLRealmID.TabIndex = 2;
            LBLRealmID.Text = "ID";
            // 
            // LBLRealmIcon
            // 
            LBLRealmIcon.AutoSize = true;
            LBLRealmIcon.ForeColor = Color.White;
            LBLRealmIcon.Location = new Point(109, 248);
            LBLRealmIcon.Name = "LBLRealmIcon";
            LBLRealmIcon.Size = new Size(33, 15);
            LBLRealmIcon.TabIndex = 14;
            LBLRealmIcon.Text = "Icon:";
            // 
            // TXTRealmSubnetMask
            // 
            TXTRealmSubnetMask.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmSubnetMask.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmSubnetMask.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmSubnetMask.ForeColor = Color.White;
            TXTRealmSubnetMask.Location = new Point(5, 222);
            TXTRealmSubnetMask.Multiline = false;
            TXTRealmSubnetMask.Name = "TXTRealmSubnetMask";
            TXTRealmSubnetMask.PasswordChar = '\0';
            TXTRealmSubnetMask.ReadOnly = false;
            TXTRealmSubnetMask.SelectedText = "";
            TXTRealmSubnetMask.Size = new Size(240, 25);
            TXTRealmSubnetMask.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmSubnetMask.StyleManager = null;
            TXTRealmSubnetMask.TabIndex = 9;
            TXTRealmSubnetMask.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmSubnetMask.UseStyleColors = true;
            // 
            // TXTRealmPort
            // 
            TXTRealmPort.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmPort.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmPort.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmPort.ForeColor = Color.White;
            TXTRealmPort.Location = new Point(5, 267);
            TXTRealmPort.Multiline = false;
            TXTRealmPort.Name = "TXTRealmPort";
            TXTRealmPort.PasswordChar = '\0';
            TXTRealmPort.ReadOnly = false;
            TXTRealmPort.SelectedText = "";
            TXTRealmPort.Size = new Size(96, 25);
            TXTRealmPort.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmPort.StyleManager = null;
            TXTRealmPort.TabIndex = 11;
            TXTRealmPort.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmPort.UseStyleColors = true;
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // TLTHome
            // 
            TLTHome.BackColor = Color.White;
            TLTHome.BackgroundColor = Color.FromArgb(34, 39, 46);
            TLTHome.BorderColor = Color.FromArgb(0, 174, 219);
            TLTHome.BorderSize = 1;
            TLTHome.ForeColor = Color.WhiteSmoke;
            TLTHome.LinkColor = Color.DodgerBlue;
            TLTHome.LinkEnabled = false;
            TLTHome.LinkText = "";
            TLTHome.OwnerDraw = true;
            TLTHome.StripAmpersands = true;
            TLTHome.TextColor = Color.White;
            TLTHome.TextFont = new Font("Segoe UI Semibold", 10F);
            TLTHome.TitleBackgroundColor = Color.FromArgb(28, 33, 40);
            TLTHome.TitleColor = Color.FromArgb(0, 174, 219);
            TLTHome.ToolTipIcon = ToolTipIcon.Info;
            TLTHome.ToolTipTitle = "Information!";
            // 
            // TimerRefreshData
            // 
            TimerRefreshData.Interval = 1000;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(5, 151);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 64;
            label1.Text = "Re. Password:";
            // 
            // metroTextBox1
            // 
            metroTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            metroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Small;
            metroTextBox1.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            metroTextBox1.ForeColor = Color.White;
            metroTextBox1.Location = new Point(3, 169);
            metroTextBox1.Multiline = false;
            metroTextBox1.Name = "metroTextBox1";
            metroTextBox1.PasswordChar = '\0';
            metroTextBox1.ReadOnly = false;
            metroTextBox1.SelectedText = "";
            metroTextBox1.Size = new Size(254, 25);
            metroTextBox1.Style = MetroFramework.MetroColorStyle.Blue;
            metroTextBox1.StyleManager = null;
            metroTextBox1.TabIndex = 63;
            metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroTextBox1.UseStyleColors = true;
            // 
            // DatabaseControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 33, 40);
            Controls.Add(TabControl1);
            Name = "DatabaseControl";
            Size = new Size(845, 370);
            Load += DatabaseControl_LoadAsync;
            TabControl1.ResumeLayout(false);
            tPageAccount.ResumeLayout(false);
            metroPanel8.ResumeLayout(false);
            metroPanel8.PerformLayout();
            metroPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            metroPanel6.ResumeLayout(false);
            metroPanel6.PerformLayout();
            metroPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            metroPanel9.ResumeLayout(false);
            metroPanel9.PerformLayout();
            metroPanel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            tPageRealmList.ResumeLayout(false);
            metroPanel2.ResumeLayout(false);
            metroPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            metroPanel1.ResumeLayout(false);
            metroPanel1.PerformLayout();
            metroPanel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            metroPanel3.ResumeLayout(false);
            metroPanel3.PerformLayout();
            metroPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl TabControl1;
        private TabPage tPageRealmList;
        private MetroFramework.Controls.MetroTextBox TXTRealmID;
        private Label LBLRealmID;
        private Label label9;
        private MetroFramework.Controls.MetroTextBox TXTRealmTime;
        private Label LBLRealmFlag;
        private MetroFramework.Controls.MetroTextBox TXTRealmFlag;
        private Label LBLRealmIcon;
        private MetroFramework.Controls.MetroTextBox TXTRealmIcon;
        private Label LBLRealmPort;
        private MetroFramework.Controls.MetroTextBox TXTRealmPort;
        private Label LBLRealmSubnetMask;
        private MetroFramework.Controls.MetroTextBox TXTRealmSubnetMask;
        private Label LBLRealmLocalAddress;
        private MetroFramework.Controls.MetroTextBox TXTRealmLocalAddress;
        private Label LBLRealmAddress;
        private MetroFramework.Controls.MetroTextBox TXTRealmAddress;
        private Label LBLRealmName;
        private MetroFramework.Controls.MetroTextBox TXTRealmName;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private TrionControlPanel.UI.CustomComboBox CBOXReamList;
        private Label LBLRealmsAvailable;
        private Label label2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTextBox TXTPublicIP;
        private Label LBLPublicIP;
        private MetroFramework.Controls.MetroTextBox TXTInternIP;
        private Label LBLInternIP;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private Label label5;
        private UI.Controls.CustomButton BTNForcerefresh;
        private Label label1;
        private TrionControlPanel.UI.CustomComboBox customComboBox1;
        private Label label3;
        private UI.Controls.CustomButton BTNSaveData;
        private UI.Controls.CustomButton BTNOpenIntern;
        private UI.Controls.CustomButton BTNOpenPublic;
        private TabPage tPageAccount;
        private MetroFramework.Controls.MetroTextBox TXTDomainName;
        private Label label4;
        private MetroFramework.Controls.MetroPanel metroPanel5;
        private MetroFramework.Controls.MetroPanel metroPanel20;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroPanel metroPanel8;
        private MetroFramework.Controls.MetroPanel metroPanel10;
        private Label label7;
        private MetroFramework.Controls.MetroPanel metroPanel6;
        private MetroFramework.Controls.MetroPanel metroPanel7;
        private Label label6;
        private UI.Controls.CustomButton BTNSetGM;
        private MetroFramework.Controls.MetroPanel metroPanel9;
        private MetroFramework.Controls.MetroTextBox TXTBoxCreateUser;
        private UI.Controls.CustomButton BTNCreateAccount;
        private MetroFramework.Controls.MetroPanel metroPanel17;
        private Label label30;
        private MetroFramework.Controls.MetroTextBox TXTBoxCreateEmail;
        private MetroFramework.Controls.MetroTextBox TXTBoxCreatePassword;
        private MetroFramework.Controls.MetroTextBox metroTextBox5;
        private MetroFramework.Controls.MetroTextBox metroTextBox6;
        private MetroFramework.Controls.MetroTextBox metroTextBox3;
        private MetroFramework.Controls.MetroTextBox metroTextBox4;
        private Label label11;
        private Label label12;
        private Label label10;
        private Label label8;
        private Label label15;
        private Label label14;
        private Label label13;
        private UI.Controls.CustomButton BTNResetPassword;
        private System.Windows.Forms.Timer TimerWacher;
        private TrionUI.Controls.CustomToolTip TLTHome;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox6;
        private Label LBLExpansion;
        private MetroFramework.Controls.MetroTextBox TXTExpansion;
        private System.Windows.Forms.Timer TimerRefreshData;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
    }
}
