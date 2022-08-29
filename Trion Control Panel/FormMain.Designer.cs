using TrionControlPanel.UI;
namespace TrionControlPanel
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnSettings = new CustomButton();
            this.btnTerminal = new CustomButton();
            this.btnHome = new CustomButton();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.mainNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.trionNotificationMenu = new CustomDropdownMenu(this.components);
            this.showTrionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTrionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTrionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitTrionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.timerCrashCheck = new System.Windows.Forms.Timer(this.components);
            this.timerResize = new System.Windows.Forms.Timer(this.components);
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.trionNotificationMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.panelMenu.Controls.Add(this.picLogo);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnTerminal);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Location = new System.Drawing.Point(0, 5);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(50, 615);
            this.panelMenu.TabIndex = 5;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::TrionControlPanel.Properties.Resources.Logo_weiss;
            this.picLogo.Location = new System.Drawing.Point(5, 9);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(40, 40);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSettings.BorderRadius = 0;
            this.btnSettings.BorderSize = 1;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Image = global::TrionControlPanel.Properties.Resources.settings_x40;
            this.btnSettings.Location = new System.Drawing.Point(3, 166);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(45, 45);
            this.btnSettings.TabIndex = 38;
            this.btnSettings.TextColor = System.Drawing.Color.White;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // btnTerminal
            // 
            this.btnTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnTerminal.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnTerminal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTerminal.BorderRadius = 0;
            this.btnTerminal.BorderSize = 1;
            this.btnTerminal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminal.FlatAppearance.BorderSize = 0;
            this.btnTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTerminal.ForeColor = System.Drawing.Color.White;
            this.btnTerminal.Image = global::TrionControlPanel.Properties.Resources.terminal_x40;
            this.btnTerminal.Location = new System.Drawing.Point(3, 115);
            this.btnTerminal.Name = "btnTerminal";
            this.btnTerminal.Size = new System.Drawing.Size(45, 45);
            this.btnTerminal.TabIndex = 38;
            this.btnTerminal.TextColor = System.Drawing.Color.White;
            this.btnTerminal.UseVisualStyleBackColor = false;
            this.btnTerminal.Click += new System.EventHandler(this.BtnTerminal_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnHome.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.btnHome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnHome.BorderRadius = 0;
            this.btnHome.BorderSize = 1;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = global::TrionControlPanel.Properties.Resources.home_x40;
            this.btnHome.Location = new System.Drawing.Point(3, 64);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(45, 45);
            this.btnHome.TabIndex = 37;
            this.btnHome.TextColor = System.Drawing.Color.White;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.pnlTabs.Location = new System.Drawing.Point(51, 54);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(700, 560);
            this.pnlTabs.TabIndex = 6;
            // 
            // mainNotify
            // 
            this.mainNotify.ContextMenuStrip = this.trionNotificationMenu;
            this.mainNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotify.Icon")));
            this.mainNotify.Text = "Trion Control Panel";
            this.mainNotify.Visible = true;
            this.mainNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainNotify_MouseDoubleClick);
            // 
            // trionNotificationMenu
            // 
            this.trionNotificationMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.trionNotificationMenu.IsMainMenu = true;
            this.trionNotificationMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTrionItem,
            this.startTrionItem,
            this.stopTrionItem,
            this.exitTrionItem});
            this.trionNotificationMenu.MenuItemHeight = 15;
            this.trionNotificationMenu.MenuItemTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.trionNotificationMenu.Name = "customDropdownMenu1";
            this.trionNotificationMenu.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.trionNotificationMenu.Size = new System.Drawing.Size(134, 92);
            // 
            // showTrionItem
            // 
            this.showTrionItem.Image = global::TrionControlPanel.Properties.Resources.Logo_weiss_klein1;
            this.showTrionItem.Name = "showTrionItem";
            this.showTrionItem.Size = new System.Drawing.Size(133, 22);
            this.showTrionItem.Text = "Show Trion";
            this.showTrionItem.Click += new System.EventHandler(this.ShowTrionItem_Click);
            // 
            // startTrionItem
            // 
            this.startTrionItem.Image = global::TrionControlPanel.Properties.Resources.anfang_20;
            this.startTrionItem.Name = "startTrionItem";
            this.startTrionItem.Size = new System.Drawing.Size(133, 22);
            this.startTrionItem.Text = "Start Server";
            this.startTrionItem.Click += new System.EventHandler(this.StartTrionItem_Click);
            // 
            // stopTrionItem
            // 
            this.stopTrionItem.Image = global::TrionControlPanel.Properties.Resources.stopp_20;
            this.stopTrionItem.Name = "stopTrionItem";
            this.stopTrionItem.Size = new System.Drawing.Size(133, 22);
            this.stopTrionItem.Text = "Stop Server";
            this.stopTrionItem.Click += new System.EventHandler(this.stopTrionItem_Click);
            // 
            // exitTrionItem
            // 
            this.exitTrionItem.Image = global::TrionControlPanel.Properties.Resources.schließen_20;
            this.exitTrionItem.Name = "exitTrionItem";
            this.exitTrionItem.Size = new System.Drawing.Size(133, 22);
            this.exitTrionItem.Text = "Exit Trion";
            this.exitTrionItem.Click += new System.EventHandler(this.ExitTrionItem_Click);
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Interval = 10;
            this.timerCheck.Tick += new System.EventHandler(this.TimerCheck_Tick);
            // 
            // timerCrashCheck
            // 
            this.timerCrashCheck.Interval = 3000;
            this.timerCrashCheck.Tick += new System.EventHandler(this.timerCrashCheck_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(752, 620);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.panelMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Resizable = false;
            this.Text = "Trion Control Panel";
            this.TextAlign = MetroFramework.Forms.TextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.trionNotificationMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panelMenu;
        private PictureBox picLogo;
        private NotifyIcon mainNotify;
        private System.Windows.Forms.Timer timerCheck;
        private CustomButton btnHome;
        private CustomButton btnSettings;
        private CustomButton btnTerminal;
        internal System.Windows.Forms.Timer timerCrashCheck;
        private CustomDropdownMenu trionNotificationMenu;
        private ToolStripMenuItem showTrionItem;
        private ToolStripMenuItem startTrionItem;
        private ToolStripMenuItem stopTrionItem;
        private ToolStripMenuItem exitTrionItem;
        private System.Windows.Forms.Timer timerResize;
        private Panel pnlTabs;
    }
}
