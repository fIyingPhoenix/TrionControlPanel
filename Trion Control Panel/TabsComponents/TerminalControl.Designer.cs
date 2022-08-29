using TrionControlPanel.UI;
namespace TrionControlPanel.TabsComponents
{
    partial class TerminalControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label lblUsername;
            System.Windows.Forms.Label lblPassword;
            System.Windows.Forms.Label lblRePassword;
            System.Windows.Forms.Label lblShowPassword;
            System.Windows.Forms.Label lblEmail;
            System.Windows.Forms.Label lblGMlevel;
            System.Windows.Forms.Label lblGMUsername;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            this.lblWorldName = new System.Windows.Forms.Label();
            this.lblRealmBuild = new System.Windows.Forms.Label();
            this.lblRealmRegion = new System.Windows.Forms.Label();
            this.btnLoadRealm = new CustomButton();
            this.btnSetRealm = new CustomButton();
            this.txtRealmRegion = new CustomTextBox();
            this.txtRealmGameBuild = new CustomTextBox();
            this.txtRealmTimeZone = new CustomTextBox();
            this.txtRealmPort = new CustomTextBox();
            this.txtRealmSubMask = new CustomTextBox();
            this.txtRealmLocalAddress = new CustomTextBox();
            this.txtRealmAddress = new CustomTextBox();
            this.txtRealmName = new CustomTextBox();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.customPanelPanel2 = new CustomPanel();
            this.btnCreateAccount = new CustomButton();
            this.txtEmail = new CustomTextBox();
            this.tglShowPassword = new CustomToggleButton();
            this.txtRePassword = new CustomTextBox();
            this.txtUsername = new CustomTextBox();
            this.txtPassword = new CustomTextBox();
            this.customPanelPanel3 = new CustomPanel();
            this.btnSetGMLvl = new CustomButton();
            this.customTextBox18 = new CustomTextBox();
            this.txtUserGM = new CustomTextBox();
            this.lblSetGMLevel = new System.Windows.Forms.Label();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.customPanelPanel4 = new CustomPanel();
            this.txtChangePasswordCurrentPassword = new CustomTextBox();
            this.btnChangePassword = new CustomButton();
            this.txtChangePasswordEmail = new CustomTextBox();
            this.tglChengePasswordShowPassword = new CustomToggleButton();
            this.txtChangePasswordReNewPassword = new CustomTextBox();
            this.txtChangePasswordNewPassword = new CustomTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.customPanel1 = new CustomPanel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            lblUsername = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            lblRePassword = new System.Windows.Forms.Label();
            lblShowPassword = new System.Windows.Forms.Label();
            lblEmail = new System.Windows.Forms.Label();
            lblGMlevel = new System.Windows.Forms.Label();
            lblGMUsername = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label1.Location = new System.Drawing.Point(36, 97);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(88, 15);
            label1.TabIndex = 36;
            label1.Text = "Realm Address:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label2.Location = new System.Drawing.Point(36, 144);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(119, 15);
            label2.TabIndex = 37;
            label2.Text = "Realm Local Address:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label4.Location = new System.Drawing.Point(250, 97);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 15);
            label4.TabIndex = 42;
            label4.Text = "Realm Port:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label5.Location = new System.Drawing.Point(250, 49);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(94, 15);
            label5.TabIndex = 41;
            label5.Text = "Realm SubMask:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label7.Location = new System.Drawing.Point(720, 151);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(99, 15);
            label7.TabIndex = 49;
            label7.Text = "Realm TimeZone:";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblUsername.Location = new System.Drawing.Point(28, 257);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new System.Drawing.Size(68, 15);
            lblUsername.TabIndex = 46;
            lblUsername.Text = "User Name:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblPassword.Location = new System.Drawing.Point(28, 305);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(60, 15);
            lblPassword.TabIndex = 48;
            lblPassword.Text = "Password:";
            // 
            // lblRePassword
            // 
            lblRePassword.AutoSize = true;
            lblRePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblRePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblRePassword.Location = new System.Drawing.Point(257, 306);
            lblRePassword.Name = "lblRePassword";
            lblRePassword.Size = new System.Drawing.Size(79, 15);
            lblRePassword.TabIndex = 50;
            lblRePassword.Text = "Re. Password:";
            // 
            // lblShowPassword
            // 
            lblShowPassword.AutoSize = true;
            lblShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblShowPassword.Location = new System.Drawing.Point(87, 367);
            lblShowPassword.Name = "lblShowPassword";
            lblShowPassword.Size = new System.Drawing.Size(92, 15);
            lblShowPassword.TabIndex = 51;
            lblShowPassword.Text = "Shwo Password.";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblEmail.Location = new System.Drawing.Point(256, 255);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(44, 15);
            lblEmail.TabIndex = 53;
            lblEmail.Text = "E-Mail:";
            // 
            // lblGMlevel
            // 
            lblGMlevel.AutoSize = true;
            lblGMlevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblGMlevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblGMlevel.Location = new System.Drawing.Point(499, 306);
            lblGMlevel.Name = "lblGMlevel";
            lblGMlevel.Size = new System.Drawing.Size(60, 15);
            lblGMlevel.TabIndex = 48;
            lblGMlevel.Text = "Password:";
            // 
            // lblGMUsername
            // 
            lblGMUsername.AutoSize = true;
            lblGMUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            lblGMUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            lblGMUsername.Location = new System.Drawing.Point(499, 257);
            lblGMUsername.Name = "lblGMUsername";
            lblGMUsername.Size = new System.Drawing.Size(68, 15);
            lblGMUsername.TabIndex = 46;
            lblGMUsername.Text = "User Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label3.Location = new System.Drawing.Point(28, 448);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 15);
            label3.TabIndex = 53;
            label3.Text = "E-Mail:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label6.Location = new System.Drawing.Point(560, 509);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(92, 15);
            label6.TabIndex = 51;
            label6.Text = "Shwo Password.";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label8.Location = new System.Drawing.Point(261, 489);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(109, 15);
            label8.TabIndex = 50;
            label8.Text = "Re.  New Password:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label13.Location = new System.Drawing.Point(28, 491);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(87, 15);
            label13.TabIndex = 48;
            label13.Text = "New Password:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            label14.Location = new System.Drawing.Point(261, 451);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(60, 15);
            label14.TabIndex = 56;
            label14.Text = "Password:";
            // 
            // lblWorldName
            // 
            this.lblWorldName.AutoSize = true;
            this.lblWorldName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblWorldName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorldName.Location = new System.Drawing.Point(36, 49);
            this.lblWorldName.Name = "lblWorldName";
            this.lblWorldName.Size = new System.Drawing.Size(78, 15);
            this.lblWorldName.TabIndex = 35;
            this.lblWorldName.Text = "Realm Name:";
            // 
            // lblRealmBuild
            // 
            this.lblRealmBuild.AutoSize = true;
            this.lblRealmBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblRealmBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblRealmBuild.Location = new System.Drawing.Point(464, 49);
            this.lblRealmBuild.Name = "lblRealmBuild";
            this.lblRealmBuild.Size = new System.Drawing.Size(73, 15);
            this.lblRealmBuild.TabIndex = 59;
            this.lblRealmBuild.Text = "Realm Build:";
            // 
            // lblRealmRegion
            // 
            this.lblRealmRegion.AutoSize = true;
            this.lblRealmRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblRealmRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblRealmRegion.Location = new System.Drawing.Point(465, 97);
            this.lblRealmRegion.Name = "lblRealmRegion";
            this.lblRealmRegion.Size = new System.Drawing.Size(83, 15);
            this.lblRealmRegion.TabIndex = 60;
            this.lblRealmRegion.Text = "Realm Region:";
            // 
            // btnLoadRealm
            // 
            this.btnLoadRealm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnLoadRealm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnLoadRealm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnLoadRealm.BorderRadius = 0;
            this.btnLoadRealm.BorderSize = 1;
            this.btnLoadRealm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadRealm.FlatAppearance.BorderSize = 0;
            this.btnLoadRealm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadRealm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadRealm.ForeColor = System.Drawing.Color.White;
            this.btnLoadRealm.Location = new System.Drawing.Point(567, 159);
            this.btnLoadRealm.Name = "btnLoadRealm";
            this.btnLoadRealm.Size = new System.Drawing.Size(97, 25);
            this.btnLoadRealm.TabIndex = 58;
            this.btnLoadRealm.Text = "Load";
            this.btnLoadRealm.TextColor = System.Drawing.Color.White;
            this.btnLoadRealm.UseVisualStyleBackColor = false;
            this.btnLoadRealm.Click += new System.EventHandler(this.BtnLoadRealm_Click);
            // 
            // btnSetRealm
            // 
            this.btnSetRealm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSetRealm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSetRealm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSetRealm.BorderRadius = 0;
            this.btnSetRealm.BorderSize = 1;
            this.btnSetRealm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetRealm.FlatAppearance.BorderSize = 0;
            this.btnSetRealm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetRealm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSetRealm.ForeColor = System.Drawing.Color.White;
            this.btnSetRealm.Location = new System.Drawing.Point(465, 159);
            this.btnSetRealm.Name = "btnSetRealm";
            this.btnSetRealm.Size = new System.Drawing.Size(97, 25);
            this.btnSetRealm.TabIndex = 57;
            this.btnSetRealm.Text = "Set";
            this.btnSetRealm.TextColor = System.Drawing.Color.White;
            this.btnSetRealm.UseVisualStyleBackColor = false;
            this.btnSetRealm.Click += new System.EventHandler(this.BtnSetRealm_Click);
            // 
            // txtRealmRegion
            // 
            this.txtRealmRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmRegion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmRegion.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmRegion.BorderSize = 1;
            this.txtRealmRegion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmRegion.Location = new System.Drawing.Point(465, 112);
            this.txtRealmRegion.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmRegion.Multiline = true;
            this.txtRealmRegion.Name = "txtRealmRegion";
            this.txtRealmRegion.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmRegion.PasswordChar = false;
            this.txtRealmRegion.ReadOnly = false;
            this.txtRealmRegion.Size = new System.Drawing.Size(200, 25);
            this.txtRealmRegion.TabIndex = 52;
            this.txtRealmRegion.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmRegion.Texts = "";
            this.txtRealmRegion.UnderlinedStyle = false;
            this.txtRealmRegion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtRealmGameBuild
            // 
            this.txtRealmGameBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmGameBuild.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmGameBuild.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmGameBuild.BorderSize = 1;
            this.txtRealmGameBuild.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmGameBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmGameBuild.Location = new System.Drawing.Point(464, 64);
            this.txtRealmGameBuild.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmGameBuild.Multiline = true;
            this.txtRealmGameBuild.Name = "txtRealmGameBuild";
            this.txtRealmGameBuild.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmGameBuild.PasswordChar = false;
            this.txtRealmGameBuild.ReadOnly = false;
            this.txtRealmGameBuild.Size = new System.Drawing.Size(201, 25);
            this.txtRealmGameBuild.TabIndex = 51;
            this.txtRealmGameBuild.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmGameBuild.Texts = "";
            this.txtRealmGameBuild.UnderlinedStyle = false;
            this.txtRealmGameBuild.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtRealmTimeZone
            // 
            this.txtRealmTimeZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmTimeZone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmTimeZone.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmTimeZone.BorderSize = 1;
            this.txtRealmTimeZone.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmTimeZone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmTimeZone.Location = new System.Drawing.Point(250, 159);
            this.txtRealmTimeZone.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmTimeZone.Multiline = true;
            this.txtRealmTimeZone.Name = "txtRealmTimeZone";
            this.txtRealmTimeZone.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmTimeZone.PasswordChar = false;
            this.txtRealmTimeZone.ReadOnly = false;
            this.txtRealmTimeZone.Size = new System.Drawing.Size(200, 25);
            this.txtRealmTimeZone.TabIndex = 46;
            this.txtRealmTimeZone.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmTimeZone.Texts = "";
            this.txtRealmTimeZone.UnderlinedStyle = false;
            this.txtRealmTimeZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtRealmPort
            // 
            this.txtRealmPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmPort.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmPort.BorderSize = 1;
            this.txtRealmPort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmPort.Location = new System.Drawing.Point(250, 112);
            this.txtRealmPort.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmPort.Multiline = true;
            this.txtRealmPort.Name = "txtRealmPort";
            this.txtRealmPort.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmPort.PasswordChar = false;
            this.txtRealmPort.ReadOnly = false;
            this.txtRealmPort.Size = new System.Drawing.Size(200, 25);
            this.txtRealmPort.TabIndex = 39;
            this.txtRealmPort.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmPort.Texts = "";
            this.txtRealmPort.UnderlinedStyle = false;
            this.txtRealmPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtRealmSubMask
            // 
            this.txtRealmSubMask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmSubMask.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmSubMask.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmSubMask.BorderSize = 1;
            this.txtRealmSubMask.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmSubMask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmSubMask.Location = new System.Drawing.Point(250, 64);
            this.txtRealmSubMask.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmSubMask.Multiline = true;
            this.txtRealmSubMask.Name = "txtRealmSubMask";
            this.txtRealmSubMask.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmSubMask.PasswordChar = false;
            this.txtRealmSubMask.ReadOnly = false;
            this.txtRealmSubMask.Size = new System.Drawing.Size(200, 25);
            this.txtRealmSubMask.TabIndex = 38;
            this.txtRealmSubMask.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmSubMask.Texts = "";
            this.txtRealmSubMask.UnderlinedStyle = false;
            this.txtRealmSubMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowJustNumbers);
            // 
            // txtRealmLocalAddress
            // 
            this.txtRealmLocalAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmLocalAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmLocalAddress.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmLocalAddress.BorderSize = 1;
            this.txtRealmLocalAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmLocalAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmLocalAddress.Location = new System.Drawing.Point(28, 159);
            this.txtRealmLocalAddress.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmLocalAddress.Multiline = true;
            this.txtRealmLocalAddress.Name = "txtRealmLocalAddress";
            this.txtRealmLocalAddress.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmLocalAddress.PasswordChar = false;
            this.txtRealmLocalAddress.ReadOnly = false;
            this.txtRealmLocalAddress.Size = new System.Drawing.Size(208, 25);
            this.txtRealmLocalAddress.TabIndex = 34;
            this.txtRealmLocalAddress.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmLocalAddress.Texts = "";
            this.txtRealmLocalAddress.UnderlinedStyle = false;
            // 
            // txtRealmAddress
            // 
            this.txtRealmAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmAddress.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmAddress.BorderSize = 1;
            this.txtRealmAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmAddress.Location = new System.Drawing.Point(28, 112);
            this.txtRealmAddress.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmAddress.Multiline = true;
            this.txtRealmAddress.Name = "txtRealmAddress";
            this.txtRealmAddress.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmAddress.PasswordChar = false;
            this.txtRealmAddress.ReadOnly = false;
            this.txtRealmAddress.Size = new System.Drawing.Size(208, 25);
            this.txtRealmAddress.TabIndex = 33;
            this.txtRealmAddress.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmAddress.Texts = "";
            this.txtRealmAddress.UnderlinedStyle = false;
            // 
            // txtRealmName
            // 
            this.txtRealmName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRealmName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRealmName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRealmName.BorderSize = 1;
            this.txtRealmName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRealmName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRealmName.Location = new System.Drawing.Point(27, 64);
            this.txtRealmName.Margin = new System.Windows.Forms.Padding(0);
            this.txtRealmName.Multiline = true;
            this.txtRealmName.Name = "txtRealmName";
            this.txtRealmName.Padding = new System.Windows.Forms.Padding(1);
            this.txtRealmName.PasswordChar = false;
            this.txtRealmName.ReadOnly = false;
            this.txtRealmName.Size = new System.Drawing.Size(209, 25);
            this.txtRealmName.TabIndex = 32;
            this.txtRealmName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRealmName.Texts = "";
            this.txtRealmName.UnderlinedStyle = false;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblServerStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblServerStatus.Location = new System.Drawing.Point(15, 19);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(143, 21);
            this.lblServerStatus.TabIndex = 19;
            this.lblServerStatus.Text = "Change Realm List";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label12.Location = new System.Drawing.Point(15, 222);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 21);
            this.label12.TabIndex = 19;
            this.label12.Text = "Create Account.";
            // 
            // customPanelPanel2
            // 
            this.customPanelPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel2.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.customPanelPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel2.BorderSize = 1;
            this.customPanelPanel2.Edge = 0;
            this.customPanelPanel2.Location = new System.Drawing.Point(10, 217);
            this.customPanelPanel2.Name = "customPanelPanel2";
            this.customPanelPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.customPanelPanel2.Size = new System.Drawing.Size(459, 187);
            this.customPanelPanel2.TabIndex = 1;
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnCreateAccount.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnCreateAccount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCreateAccount.BorderRadius = 0;
            this.btnCreateAccount.BorderSize = 1;
            this.btnCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateAccount.FlatAppearance.BorderSize = 0;
            this.btnCreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCreateAccount.ForeColor = System.Drawing.Color.White;
            this.btnCreateAccount.Location = new System.Drawing.Point(297, 358);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(153, 30);
            this.btnCreateAccount.TabIndex = 54;
            this.btnCreateAccount.Text = "Create";
            this.btnCreateAccount.TextColor = System.Drawing.Color.White;
            this.btnCreateAccount.UseVisualStyleBackColor = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtEmail.BorderSize = 1;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtEmail.Location = new System.Drawing.Point(250, 272);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(1);
            this.txtEmail.PasswordChar = false;
            this.txtEmail.ReadOnly = false;
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 52;
            this.txtEmail.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmail.Texts = "";
            this.txtEmail.UnderlinedStyle = false;
            // 
            // tglShowPassword
            // 
            this.tglShowPassword.AutoSize = true;
            this.tglShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tglShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglShowPassword.Location = new System.Drawing.Point(36, 363);
            this.tglShowPassword.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglShowPassword.Name = "tglShowPassword";
            this.tglShowPassword.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglShowPassword.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglShowPassword.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglShowPassword.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglShowPassword.Size = new System.Drawing.Size(45, 22);
            this.tglShowPassword.SolidStyle = false;
            this.tglShowPassword.TabIndex = 51;
            this.tglShowPassword.UseVisualStyleBackColor = false;
            // 
            // txtRePassword
            // 
            this.txtRePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtRePassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtRePassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtRePassword.BorderSize = 1;
            this.txtRePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtRePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtRePassword.Location = new System.Drawing.Point(250, 321);
            this.txtRePassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtRePassword.Multiline = true;
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.Padding = new System.Windows.Forms.Padding(1);
            this.txtRePassword.PasswordChar = false;
            this.txtRePassword.ReadOnly = false;
            this.txtRePassword.Size = new System.Drawing.Size(200, 23);
            this.txtRePassword.TabIndex = 49;
            this.txtRePassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRePassword.Texts = "";
            this.txtRePassword.UnderlinedStyle = false;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtUsername.BorderSize = 1;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtUsername.Location = new System.Drawing.Point(27, 272);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(0);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Padding = new System.Windows.Forms.Padding(1);
            this.txtUsername.PasswordChar = false;
            this.txtUsername.ReadOnly = false;
            this.txtUsername.Size = new System.Drawing.Size(209, 23);
            this.txtUsername.TabIndex = 45;
            this.txtUsername.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtPassword.BorderSize = 1;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtPassword.Location = new System.Drawing.Point(28, 321);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(1);
            this.txtPassword.PasswordChar = false;
            this.txtPassword.ReadOnly = false;
            this.txtPassword.Size = new System.Drawing.Size(209, 23);
            this.txtPassword.TabIndex = 47;
            this.txtPassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = false;
            // 
            // customPanelPanel3
            // 
            this.customPanelPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel3.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.customPanelPanel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel3.BorderSize = 1;
            this.customPanelPanel3.Edge = 0;
            this.customPanelPanel3.Location = new System.Drawing.Point(479, 217);
            this.customPanelPanel3.Name = "customPanelPanel3";
            this.customPanelPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.customPanelPanel3.Size = new System.Drawing.Size(206, 187);
            this.customPanelPanel3.TabIndex = 2;
            // 
            // btnSetGMLvl
            // 
            this.btnSetGMLvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSetGMLvl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSetGMLvl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSetGMLvl.BorderRadius = 0;
            this.btnSetGMLvl.BorderSize = 1;
            this.btnSetGMLvl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetGMLvl.FlatAppearance.BorderSize = 0;
            this.btnSetGMLvl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetGMLvl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSetGMLvl.ForeColor = System.Drawing.Color.White;
            this.btnSetGMLvl.Location = new System.Drawing.Point(494, 358);
            this.btnSetGMLvl.Name = "btnSetGMLvl";
            this.btnSetGMLvl.Size = new System.Drawing.Size(170, 30);
            this.btnSetGMLvl.TabIndex = 55;
            this.btnSetGMLvl.Text = "Set";
            this.btnSetGMLvl.TextColor = System.Drawing.Color.White;
            this.btnSetGMLvl.UseVisualStyleBackColor = false;
            // 
            // customTextBox18
            // 
            this.customTextBox18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.customTextBox18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customTextBox18.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.customTextBox18.BorderSize = 1;
            this.customTextBox18.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.customTextBox18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.customTextBox18.Location = new System.Drawing.Point(494, 321);
            this.customTextBox18.Margin = new System.Windows.Forms.Padding(0);
            this.customTextBox18.Multiline = true;
            this.customTextBox18.Name = "customTextBox18";
            this.customTextBox18.Padding = new System.Windows.Forms.Padding(1);
            this.customTextBox18.PasswordChar = false;
            this.customTextBox18.ReadOnly = false;
            this.customTextBox18.Size = new System.Drawing.Size(170, 23);
            this.customTextBox18.TabIndex = 47;
            this.customTextBox18.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.customTextBox18.Texts = "";
            this.customTextBox18.UnderlinedStyle = false;
            // 
            // txtUserGM
            // 
            this.txtUserGM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtUserGM.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtUserGM.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtUserGM.BorderSize = 1;
            this.txtUserGM.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtUserGM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtUserGM.Location = new System.Drawing.Point(494, 272);
            this.txtUserGM.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserGM.Multiline = true;
            this.txtUserGM.Name = "txtUserGM";
            this.txtUserGM.Padding = new System.Windows.Forms.Padding(1);
            this.txtUserGM.PasswordChar = false;
            this.txtUserGM.ReadOnly = false;
            this.txtUserGM.Size = new System.Drawing.Size(171, 23);
            this.txtUserGM.TabIndex = 45;
            this.txtUserGM.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserGM.Texts = "";
            this.txtUserGM.UnderlinedStyle = false;
            // 
            // lblSetGMLevel
            // 
            this.lblSetGMLevel.AutoSize = true;
            this.lblSetGMLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblSetGMLevel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSetGMLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblSetGMLevel.Location = new System.Drawing.Point(486, 222);
            this.lblSetGMLevel.Name = "lblSetGMLevel";
            this.lblSetGMLevel.Size = new System.Drawing.Size(102, 21);
            this.lblSetGMLevel.TabIndex = 19;
            this.lblSetGMLevel.Text = "Set GM level";
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Tick += new System.EventHandler(this.TimerCheck_Tick);
            // 
            // customPanelPanel4
            // 
            this.customPanelPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel4.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.customPanelPanel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanelPanel4.BorderSize = 1;
            this.customPanelPanel4.Edge = 0;
            this.customPanelPanel4.Location = new System.Drawing.Point(9, 413);
            this.customPanelPanel4.Name = "customPanelPanel4";
            this.customPanelPanel4.Padding = new System.Windows.Forms.Padding(1);
            this.customPanelPanel4.Size = new System.Drawing.Size(677, 137);
            this.customPanelPanel4.TabIndex = 3;
            // 
            // txtChangePasswordCurrentPassword
            // 
            this.txtChangePasswordCurrentPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtChangePasswordCurrentPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtChangePasswordCurrentPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtChangePasswordCurrentPassword.BorderSize = 1;
            this.txtChangePasswordCurrentPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtChangePasswordCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtChangePasswordCurrentPassword.Location = new System.Drawing.Point(261, 466);
            this.txtChangePasswordCurrentPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangePasswordCurrentPassword.Multiline = true;
            this.txtChangePasswordCurrentPassword.Name = "txtChangePasswordCurrentPassword";
            this.txtChangePasswordCurrentPassword.Padding = new System.Windows.Forms.Padding(1);
            this.txtChangePasswordCurrentPassword.PasswordChar = false;
            this.txtChangePasswordCurrentPassword.ReadOnly = false;
            this.txtChangePasswordCurrentPassword.Size = new System.Drawing.Size(204, 23);
            this.txtChangePasswordCurrentPassword.TabIndex = 55;
            this.txtChangePasswordCurrentPassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChangePasswordCurrentPassword.Texts = "";
            this.txtChangePasswordCurrentPassword.UnderlinedStyle = false;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnChangePassword.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnChangePassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnChangePassword.BorderRadius = 0;
            this.btnChangePassword.BorderSize = 1;
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(499, 466);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(166, 30);
            this.btnChangePassword.TabIndex = 54;
            this.btnChangePassword.Text = "Create";
            this.btnChangePassword.TextColor = System.Drawing.Color.White;
            this.btnChangePassword.UseVisualStyleBackColor = false;
            // 
            // txtChangePasswordEmail
            // 
            this.txtChangePasswordEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtChangePasswordEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtChangePasswordEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtChangePasswordEmail.BorderSize = 1;
            this.txtChangePasswordEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtChangePasswordEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtChangePasswordEmail.Location = new System.Drawing.Point(28, 466);
            this.txtChangePasswordEmail.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangePasswordEmail.Multiline = true;
            this.txtChangePasswordEmail.Name = "txtChangePasswordEmail";
            this.txtChangePasswordEmail.Padding = new System.Windows.Forms.Padding(1);
            this.txtChangePasswordEmail.PasswordChar = false;
            this.txtChangePasswordEmail.ReadOnly = false;
            this.txtChangePasswordEmail.Size = new System.Drawing.Size(215, 23);
            this.txtChangePasswordEmail.TabIndex = 52;
            this.txtChangePasswordEmail.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChangePasswordEmail.Texts = "";
            this.txtChangePasswordEmail.UnderlinedStyle = false;
            // 
            // tglChengePasswordShowPassword
            // 
            this.tglChengePasswordShowPassword.AutoSize = true;
            this.tglChengePasswordShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.tglChengePasswordShowPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tglChengePasswordShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.tglChengePasswordShowPassword.Location = new System.Drawing.Point(509, 506);
            this.tglChengePasswordShowPassword.MinimumSize = new System.Drawing.Size(45, 22);
            this.tglChengePasswordShowPassword.Name = "tglChengePasswordShowPassword";
            this.tglChengePasswordShowPassword.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.tglChengePasswordShowPassword.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.tglChengePasswordShowPassword.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tglChengePasswordShowPassword.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.tglChengePasswordShowPassword.Size = new System.Drawing.Size(45, 22);
            this.tglChengePasswordShowPassword.SolidStyle = false;
            this.tglChengePasswordShowPassword.TabIndex = 51;
            this.tglChengePasswordShowPassword.UseVisualStyleBackColor = false;
            // 
            // txtChangePasswordReNewPassword
            // 
            this.txtChangePasswordReNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtChangePasswordReNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtChangePasswordReNewPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtChangePasswordReNewPassword.BorderSize = 1;
            this.txtChangePasswordReNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtChangePasswordReNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtChangePasswordReNewPassword.Location = new System.Drawing.Point(261, 506);
            this.txtChangePasswordReNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangePasswordReNewPassword.Multiline = true;
            this.txtChangePasswordReNewPassword.Name = "txtChangePasswordReNewPassword";
            this.txtChangePasswordReNewPassword.Padding = new System.Windows.Forms.Padding(1);
            this.txtChangePasswordReNewPassword.PasswordChar = false;
            this.txtChangePasswordReNewPassword.ReadOnly = false;
            this.txtChangePasswordReNewPassword.Size = new System.Drawing.Size(204, 23);
            this.txtChangePasswordReNewPassword.TabIndex = 49;
            this.txtChangePasswordReNewPassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChangePasswordReNewPassword.Texts = "";
            this.txtChangePasswordReNewPassword.UnderlinedStyle = false;
            // 
            // txtChangePasswordNewPassword
            // 
            this.txtChangePasswordNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.txtChangePasswordNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.txtChangePasswordNewPassword.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(99)))), ((int)(((byte)(173)))));
            this.txtChangePasswordNewPassword.BorderSize = 1;
            this.txtChangePasswordNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.txtChangePasswordNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.txtChangePasswordNewPassword.Location = new System.Drawing.Point(28, 506);
            this.txtChangePasswordNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txtChangePasswordNewPassword.Multiline = true;
            this.txtChangePasswordNewPassword.Name = "txtChangePasswordNewPassword";
            this.txtChangePasswordNewPassword.Padding = new System.Windows.Forms.Padding(1);
            this.txtChangePasswordNewPassword.PasswordChar = false;
            this.txtChangePasswordNewPassword.ReadOnly = false;
            this.txtChangePasswordNewPassword.Size = new System.Drawing.Size(215, 23);
            this.txtChangePasswordNewPassword.TabIndex = 47;
            this.txtChangePasswordNewPassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChangePasswordNewPassword.Texts = "";
            this.txtChangePasswordNewPassword.UnderlinedStyle = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label15.Location = new System.Drawing.Point(15, 418);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(139, 21);
            this.label15.TabIndex = 19;
            this.label15.Text = "Chenge Password";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 2000;
            this.timerRefresh.Tick += new System.EventHandler(this.TimerRefresh_Tick);
            // 
            // customPanel1
            // 
            this.customPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanel1.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.customPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.customPanel1.BorderSize = 1;
            this.customPanel1.Edge = 0;
            this.customPanel1.Location = new System.Drawing.Point(10, 15);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.customPanel1.Size = new System.Drawing.Size(675, 193);
            this.customPanel1.TabIndex = 61;
            // 
            // TerminalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Controls.Add(lblGMUsername);
            this.Controls.Add(label14);
            this.Controls.Add(this.btnSetGMLvl);
            this.Controls.Add(label8);
            this.Controls.Add(this.txtChangePasswordCurrentPassword);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(lblGMlevel);
            this.Controls.Add(label3);
            this.Controls.Add(this.customTextBox18);
            this.Controls.Add(this.txtChangePasswordEmail);
            this.Controls.Add(lblRePassword);
            this.Controls.Add(label6);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.tglChengePasswordShowPassword);
            this.Controls.Add(this.btnCreateAccount);
            this.Controls.Add(lblShowPassword);
            this.Controls.Add(this.txtChangePasswordReNewPassword);
            this.Controls.Add(this.btnLoadRealm);
            this.Controls.Add(label13);
            this.Controls.Add(this.tglShowPassword);
            this.Controls.Add(this.txtChangePasswordNewPassword);
            this.Controls.Add(this.lblRealmBuild);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(this.txtRePassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(lblUsername);
            this.Controls.Add(this.btnSetRealm);
            this.Controls.Add(this.txtUserGM);
            this.Controls.Add(this.txtRealmRegion);
            this.Controls.Add(this.lblRealmRegion);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtRealmGameBuild);
            this.Controls.Add(this.label12);
            this.Controls.Add(label7);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.txtRealmTimeZone);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(this.txtRealmSubMask);
            this.Controls.Add(this.txtRealmName);
            this.Controls.Add(this.txtRealmLocalAddress);
            this.Controls.Add(this.txtRealmPort);
            this.Controls.Add(this.txtRealmAddress);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(this.lblWorldName);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.lblSetGMLevel);
            this.Controls.Add(this.customPanelPanel3);
            this.Controls.Add(this.customPanelPanel2);
            this.Controls.Add(this.customPanelPanel4);
            this.Name = "TerminalControl";
            this.Size = new System.Drawing.Size(700, 560);
            this.Load += new System.EventHandler(this.TerminalControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label lblServerStatus;
        private Label label12;
        private CustomPanel customPanelPanel2;
        private CustomTextBox txtRePassword;
        private CustomTextBox txtPassword;
        private CustomTextBox txtUsername;
        private CustomToggleButton tglShowPassword;
        private CustomTextBox txtEmail;
        private CustomButton btnCreateAccount;
        private CustomPanel customPanelPanel3;
        private CustomButton btnSetGMLvl;
        private CustomTextBox customTextBox18;
        private CustomTextBox txtUserGM;
        private Label lblSetGMLevel;
        private System.Windows.Forms.Timer timerCheck;
        private CustomButton btnSetRealm;
        private CustomPanel customPanelPanel4;
        private CustomTextBox txtChangePasswordCurrentPassword;
        private CustomButton btnChangePassword;
        private CustomTextBox txtChangePasswordEmail;
        private CustomToggleButton tglChengePasswordShowPassword;
        private CustomTextBox txtChangePasswordReNewPassword;
        private CustomTextBox txtChangePasswordNewPassword;
        private Label label15;
        internal CustomTextBox txtRealmLocalAddress;
        internal CustomTextBox txtRealmAddress;
        internal CustomTextBox txtRealmName;
        internal CustomTextBox txtRealmPort;
        internal CustomTextBox txtRealmSubMask;
        internal CustomTextBox txtRealmRegion;
        internal CustomTextBox txtRealmGameBuild;
        internal CustomTextBox txtRealmTimeZone;
        private System.Windows.Forms.Timer timerRefresh;
        private CustomButton btnLoadRealm;
        private Label lblRealmBuild;
        private Label lblRealmRegion;
        private Label lblWorldName;
        private CustomPanel customPanel1;
    }
}
