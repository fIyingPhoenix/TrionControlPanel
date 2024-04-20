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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseControl));
            TabControl1 = new MetroFramework.Controls.MetroTabControl();
            tabPage1 = new TabPage();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            BTNSaveData = new UI.Controls.CustomButton();
            label3 = new Label();
            label1 = new Label();
            customComboBox1 = new TrionControlPanel.UI.CustomComboBox();
            BTNOpenPublic = new UI.Controls.CustomButton();
            BTNOpenIntern = new UI.Controls.CustomButton();
            BTNForcerefresh = new UI.Controls.CustomButton();
            metroPanel1 = new MetroFramework.Controls.MetroPanel();
            label5 = new Label();
            TXTInternIP = new MetroFramework.Controls.MetroTextBox();
            LBLInternIP = new Label();
            TXTPublicIP = new MetroFramework.Controls.MetroTextBox();
            LBLPublicIP = new Label();
            LBLRealmsAvailable = new Label();
            CBOXReamList = new TrionControlPanel.UI.CustomComboBox();
            metroPanel3 = new MetroFramework.Controls.MetroPanel();
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
            TabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            metroPanel2.SuspendLayout();
            metroPanel1.SuspendLayout();
            metroPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.Appearance = TabAppearance.Buttons;
            TabControl1.Controls.Add(tabPage1);
            TabControl1.CustomBackground = false;
            TabControl1.Dock = DockStyle.Fill;
            TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl1.FontSize = MetroFramework.MetroTabControlSize.Medium;
            TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            TabControl1.ItemSize = new Size(100, 30);
            TabControl1.Location = new Point(0, 0);
            TabControl1.Multiline = true;
            TabControl1.Name = "TabControl1";
            TabControl1.Padding = new Point(6, 8);
            TabControl1.SelectedIndex = 0;
            TabControl1.Size = new Size(845, 370);
            TabControl1.SizeMode = TabSizeMode.Fixed;
            TabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            TabControl1.StyleManager = null;
            TabControl1.TabIndex = 2;
            TabControl1.TabStop = false;
            TabControl1.TextAlign = ContentAlignment.MiddleLeft;
            TabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            TabControl1.UseStyleColors = true;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(45, 51, 59);
            tabPage1.Controls.Add(metroPanel2);
            tabPage1.Controls.Add(metroPanel1);
            tabPage1.Controls.Add(metroPanel3);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(837, 332);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "RealmList";
            // 
            // metroPanel2
            // 
            metroPanel2.Anchor = AnchorStyles.Right;
            metroPanel2.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(BTNSaveData);
            metroPanel2.Controls.Add(label3);
            metroPanel2.Controls.Add(label1);
            metroPanel2.Controls.Add(customComboBox1);
            metroPanel2.Controls.Add(BTNOpenPublic);
            metroPanel2.Controls.Add(BTNOpenIntern);
            metroPanel2.Controls.Add(BTNForcerefresh);
            metroPanel2.CustomBackground = true;
            metroPanel2.HorizontalScrollbar = false;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(517, 5);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(315, 320);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 46;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = false;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
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
            BTNSaveData.Location = new Point(14, 166);
            BTNSaveData.Name = "BTNSaveData";
            BTNSaveData.NotificationCount = 0;
            BTNSaveData.RightToLeft = RightToLeft.No;
            BTNSaveData.Size = new Size(290, 30);
            BTNSaveData.TabIndex = 52;
            BTNSaveData.Text = "  Save";
            BTNSaveData.TextColor = Color.White;
            BTNSaveData.UseVisualStyleBackColor = false;
            BTNSaveData.Click += BTNSaveData_ClickAsync;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(0, 174, 219);
            label3.Location = new Point(100, 18);
            label3.Name = "label3";
            label3.Size = new Size(118, 21);
            label3.TabIndex = 51;
            label3.Text = "DNS SETTINGS";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(14, 72);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 45;
            label1.Text = "DDNS services";
            label1.Visible = false;
            // 
            // customComboBox1
            // 
            customComboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            customComboBox1.BackColor = Color.FromArgb(34, 34, 34);
            customComboBox1.BorderColor = Color.FromArgb(0, 174, 219);
            customComboBox1.BorderSize = 1;
            customComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            customComboBox1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            customComboBox1.ForeColor = Color.White;
            customComboBox1.IconColor = Color.FromArgb(0, 174, 219);
            customComboBox1.ListBackColor = Color.FromArgb(34, 34, 34);
            customComboBox1.ListTextColor = Color.FromArgb(0, 174, 219);
            customComboBox1.Location = new Point(14, 89);
            customComboBox1.MinimumSize = new Size(200, 27);
            customComboBox1.Name = "customComboBox1";
            customComboBox1.Padding = new Padding(1);
            customComboBox1.Size = new Size(290, 27);
            customComboBox1.TabIndex = 43;
            customComboBox1.Texts = "";
            customComboBox1.Visible = false;
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
            BTNOpenPublic.Location = new Point(14, 202);
            BTNOpenPublic.Name = "BTNOpenPublic";
            BTNOpenPublic.NotificationCount = 0;
            BTNOpenPublic.RightToLeft = RightToLeft.No;
            BTNOpenPublic.Size = new Size(290, 30);
            BTNOpenPublic.TabIndex = 30;
            BTNOpenPublic.Text = "  Open Public";
            BTNOpenPublic.TextColor = Color.White;
            BTNOpenPublic.UseVisualStyleBackColor = false;
            BTNOpenPublic.Click += BTNOpenPublic_ClickAsync;
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
            BTNOpenIntern.Location = new Point(14, 238);
            BTNOpenIntern.Name = "BTNOpenIntern";
            BTNOpenIntern.NotificationCount = 0;
            BTNOpenIntern.RightToLeft = RightToLeft.No;
            BTNOpenIntern.Size = new Size(290, 30);
            BTNOpenIntern.TabIndex = 29;
            BTNOpenIntern.Text = "   Open Intern";
            BTNOpenIntern.TextColor = Color.White;
            BTNOpenIntern.UseVisualStyleBackColor = false;
            BTNOpenIntern.Click += BTNOpenIntern_Click;
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
            BTNForcerefresh.Location = new Point(14, 274);
            BTNForcerefresh.Name = "BTNForcerefresh";
            BTNForcerefresh.NotificationCount = 0;
            BTNForcerefresh.RightToLeft = RightToLeft.No;
            BTNForcerefresh.Size = new Size(290, 30);
            BTNForcerefresh.TabIndex = 28;
            BTNForcerefresh.Text = "   Force Refresh";
            BTNForcerefresh.TextColor = Color.White;
            BTNForcerefresh.UseVisualStyleBackColor = false;
            BTNForcerefresh.Click += BTNForceUpdate_Click;
            // 
            // metroPanel1
            // 
            metroPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            metroPanel1.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel1.Border = true;
            metroPanel1.BorderColor = Color.Black;
            metroPanel1.BorderSize = 1;
            metroPanel1.Controls.Add(label5);
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
            metroPanel1.Size = new Size(250, 320);
            metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel1.StyleManager = null;
            metroPanel1.TabIndex = 45;
            metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel1.VerticalScrollbar = false;
            metroPanel1.VerticalScrollbarBarColor = true;
            metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            metroPanel1.VerticalScrollbarSize = 10;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(0, 174, 219);
            label5.Location = new Point(83, 18);
            label5.Name = "label5";
            label5.Size = new Size(78, 21);
            label5.TabIndex = 50;
            label5.Text = "OPTIONS";
            // 
            // TXTInternIP
            // 
            TXTInternIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTInternIP.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTInternIP.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTInternIP.ForeColor = Color.White;
            TXTInternIP.Location = new Point(16, 234);
            TXTInternIP.Multiline = false;
            TXTInternIP.Name = "TXTInternIP";
            TXTInternIP.PasswordChar = '\0';
            TXTInternIP.ReadOnly = true;
            TXTInternIP.SelectedText = "";
            TXTInternIP.Size = new Size(218, 25);
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
            LBLInternIP.Location = new Point(16, 212);
            LBLInternIP.Name = "LBLInternIP";
            LBLInternIP.Size = new Size(99, 15);
            LBLInternIP.TabIndex = 49;
            LBLInternIP.Text = "Intern IP Address:";
            // 
            // TXTPublicIP
            // 
            TXTPublicIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTPublicIP.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTPublicIP.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTPublicIP.ForeColor = Color.White;
            TXTPublicIP.Location = new Point(16, 279);
            TXTPublicIP.Multiline = false;
            TXTPublicIP.Name = "TXTPublicIP";
            TXTPublicIP.PasswordChar = '\0';
            TXTPublicIP.ReadOnly = true;
            TXTPublicIP.SelectedText = "";
            TXTPublicIP.Size = new Size(218, 25);
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
            LBLPublicIP.Location = new Point(16, 262);
            LBLPublicIP.Name = "LBLPublicIP";
            LBLPublicIP.Size = new Size(101, 15);
            LBLPublicIP.TabIndex = 47;
            LBLPublicIP.Text = "Public IP Address:";
            // 
            // LBLRealmsAvailable
            // 
            LBLRealmsAvailable.AutoSize = true;
            LBLRealmsAvailable.ForeColor = Color.White;
            LBLRealmsAvailable.Location = new Point(16, 72);
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
            CBOXReamList.Location = new Point(16, 89);
            CBOXReamList.MinimumSize = new Size(200, 27);
            CBOXReamList.Name = "CBOXReamList";
            CBOXReamList.Padding = new Padding(1);
            CBOXReamList.Size = new Size(218, 27);
            CBOXReamList.TabIndex = 42;
            CBOXReamList.Texts = "";
            CBOXReamList.OnSelectedIndexChanged += CBOXReamList_OnSelectedIndexChanged;
            // 
            // metroPanel3
            // 
            metroPanel3.Anchor = AnchorStyles.Left;
            metroPanel3.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel3.Border = true;
            metroPanel3.BorderColor = Color.Black;
            metroPanel3.BorderSize = 1;
            metroPanel3.Controls.Add(label2);
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
            metroPanel3.Size = new Size(250, 320);
            metroPanel3.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel3.StyleManager = null;
            metroPanel3.TabIndex = 41;
            metroPanel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel3.VerticalScrollbar = false;
            metroPanel3.VerticalScrollbarBarColor = true;
            metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            metroPanel3.VerticalScrollbarSize = 10;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 174, 219);
            label2.Location = new Point(98, 18);
            label2.Name = "label2";
            label2.Size = new Size(50, 21);
            label2.TabIndex = 19;
            label2.Text = "DATA";
            // 
            // TXTRealmID
            // 
            TXTRealmID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            TXTRealmID.FontSize = MetroFramework.MetroTextBoxSize.Small;
            TXTRealmID.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            TXTRealmID.ForeColor = Color.White;
            TXTRealmID.Location = new Point(15, 89);
            TXTRealmID.Multiline = false;
            TXTRealmID.Name = "TXTRealmID";
            TXTRealmID.PasswordChar = '\0';
            TXTRealmID.ReadOnly = false;
            TXTRealmID.SelectedText = "";
            TXTRealmID.Size = new Size(35, 25);
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
            TXTRealmFlag.Location = new Point(154, 279);
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
            LBLRealmAddress.Location = new Point(15, 118);
            LBLRealmAddress.Name = "LBLRealmAddress";
            LBLRealmAddress.Size = new Size(52, 15);
            LBLRealmAddress.TabIndex = 6;
            LBLRealmAddress.Text = "Address:";
            // 
            // LBLRealmSubnetMask
            // 
            LBLRealmSubnetMask.AutoSize = true;
            LBLRealmSubnetMask.ForeColor = Color.White;
            LBLRealmSubnetMask.Location = new Point(15, 212);
            LBLRealmSubnetMask.Name = "LBLRealmSubnetMask";
            LBLRealmSubnetMask.Size = new Size(109, 15);
            LBLRealmSubnetMask.TabIndex = 10;
            LBLRealmSubnetMask.Text = "Local Subnet Mask:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(197, 262);
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
            TXTRealmName.Location = new Point(55, 89);
            TXTRealmName.Multiline = false;
            TXTRealmName.Name = "TXTRealmName";
            TXTRealmName.PasswordChar = '\0';
            TXTRealmName.ReadOnly = false;
            TXTRealmName.SelectedText = "";
            TXTRealmName.Size = new Size(180, 25);
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
            LBLRealmLocalAddress.Location = new Point(15, 166);
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
            TXTRealmIcon.Location = new Point(110, 279);
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
            LBLRealmFlag.Location = new Point(154, 262);
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
            TXTRealmTime.Location = new Point(197, 279);
            TXTRealmTime.Multiline = false;
            TXTRealmTime.Name = "TXTRealmTime";
            TXTRealmTime.PasswordChar = '\0';
            TXTRealmTime.ReadOnly = false;
            TXTRealmTime.SelectedText = "";
            TXTRealmTime.Size = new Size(38, 25);
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
            LBLRealmName.Location = new Point(56, 72);
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
            TXTRealmLocalAddress.Location = new Point(15, 184);
            TXTRealmLocalAddress.Multiline = false;
            TXTRealmLocalAddress.Name = "TXTRealmLocalAddress";
            TXTRealmLocalAddress.PasswordChar = '\0';
            TXTRealmLocalAddress.ReadOnly = false;
            TXTRealmLocalAddress.SelectedText = "";
            TXTRealmLocalAddress.Size = new Size(220, 25);
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
            LBLRealmPort.Location = new Point(15, 262);
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
            TXTRealmAddress.Location = new Point(15, 135);
            TXTRealmAddress.Multiline = false;
            TXTRealmAddress.Name = "TXTRealmAddress";
            TXTRealmAddress.PasswordChar = '\0';
            TXTRealmAddress.ReadOnly = false;
            TXTRealmAddress.SelectedText = "";
            TXTRealmAddress.Size = new Size(220, 25);
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
            LBLRealmID.Location = new Point(15, 72);
            LBLRealmID.Name = "LBLRealmID";
            LBLRealmID.Size = new Size(18, 15);
            LBLRealmID.TabIndex = 2;
            LBLRealmID.Text = "ID";
            // 
            // LBLRealmIcon
            // 
            LBLRealmIcon.AutoSize = true;
            LBLRealmIcon.ForeColor = Color.White;
            LBLRealmIcon.Location = new Point(110, 262);
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
            TXTRealmSubnetMask.Location = new Point(15, 234);
            TXTRealmSubnetMask.Multiline = false;
            TXTRealmSubnetMask.Name = "TXTRealmSubnetMask";
            TXTRealmSubnetMask.PasswordChar = '\0';
            TXTRealmSubnetMask.ReadOnly = false;
            TXTRealmSubnetMask.SelectedText = "";
            TXTRealmSubnetMask.Size = new Size(220, 25);
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
            TXTRealmPort.Location = new Point(15, 279);
            TXTRealmPort.Multiline = false;
            TXTRealmPort.Name = "TXTRealmPort";
            TXTRealmPort.PasswordChar = '\0';
            TXTRealmPort.ReadOnly = false;
            TXTRealmPort.SelectedText = "";
            TXTRealmPort.Size = new Size(88, 25);
            TXTRealmPort.Style = MetroFramework.MetroColorStyle.Blue;
            TXTRealmPort.StyleManager = null;
            TXTRealmPort.TabIndex = 11;
            TXTRealmPort.Theme = MetroFramework.MetroThemeStyle.Dark;
            TXTRealmPort.UseStyleColors = true;
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
            tabPage1.ResumeLayout(false);
            metroPanel2.ResumeLayout(false);
            metroPanel2.PerformLayout();
            metroPanel1.ResumeLayout(false);
            metroPanel1.PerformLayout();
            metroPanel3.ResumeLayout(false);
            metroPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl TabControl1;
        private TabPage tabPage1;
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
        private UI.Controls.CustomButton BTNOpenIntern;
        private Label label1;
        private TrionControlPanel.UI.CustomComboBox customComboBox1;
        private UI.Controls.CustomButton BTNOpenPublic;
        private Label label3;
        private UI.Controls.CustomButton BTNSaveData;
    }
}
