namespace CypherCore_Server_Laucher.TabsComponents
{
    partial class HomeControl
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
            this.sPanel2 = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.worldCpuUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.worldRamUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.sPanel1 = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWorld = new System.Windows.Forms.Label();
            this.apacheServerLight = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.mysqlServerLight = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.bnetServerLight = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.worldServerLight = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.ServerStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.roundPanel1 = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.BnetCpuUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.BnetRamUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.roundPanel2 = new CypherCore_Server_Laucher.Classes.RoundPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.totalCpuUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.totalRamUsageProgressBar = new CypherCore_Server_Laucher.Classes.CustomProgressBar();
            this.WorldResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.BnetResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.sPanel2.SuspendLayout();
            this.sPanel1.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sPanel2
            // 
            this.sPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.sPanel2.BorderColor = System.Drawing.Color.White;
            this.sPanel2.Controls.Add(this.label5);
            this.sPanel2.Controls.Add(this.worldCpuUsageProgressBar);
            this.sPanel2.Controls.Add(this.worldRamUsageProgressBar);
            this.sPanel2.Edge = 20;
            this.sPanel2.Location = new System.Drawing.Point(15, 183);
            this.sPanel2.Name = "sPanel2";
            this.sPanel2.Size = new System.Drawing.Size(326, 150);
            this.sPanel2.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label5.Location = new System.Drawing.Point(7, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 21);
            this.label5.TabIndex = 19;
            this.label5.Text = "World Server Resource";
            // 
            // worldCpuUsageProgressBar
            // 
            this.worldCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.worldCpuUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.worldCpuUsageProgressBar.ChannelHeight = 10;
            this.worldCpuUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.worldCpuUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.worldCpuUsageProgressBar.Location = new System.Drawing.Point(22, 102);
            this.worldCpuUsageProgressBar.Name = "worldCpuUsageProgressBar";
            this.worldCpuUsageProgressBar.ShowMaximun = false;
            this.worldCpuUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.worldCpuUsageProgressBar.Size = new System.Drawing.Size(273, 28);
            this.worldCpuUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.worldCpuUsageProgressBar.SliderHeight = 10;
            this.worldCpuUsageProgressBar.SymbolAfter = "";
            this.worldCpuUsageProgressBar.SymbolBefore = "";
            this.worldCpuUsageProgressBar.TabIndex = 2;
            // 
            // worldRamUsageProgressBar
            // 
            this.worldRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.worldRamUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.worldRamUsageProgressBar.ChannelHeight = 10;
            this.worldRamUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.worldRamUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.worldRamUsageProgressBar.Location = new System.Drawing.Point(22, 53);
            this.worldRamUsageProgressBar.Maximum = 0;
            this.worldRamUsageProgressBar.Name = "worldRamUsageProgressBar";
            this.worldRamUsageProgressBar.ShowMaximun = true;
            this.worldRamUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.worldRamUsageProgressBar.Size = new System.Drawing.Size(273, 28);
            this.worldRamUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.worldRamUsageProgressBar.SliderHeight = 10;
            this.worldRamUsageProgressBar.SymbolAfter = "";
            this.worldRamUsageProgressBar.SymbolBefore = "";
            this.worldRamUsageProgressBar.TabIndex = 1;
            // 
            // sPanel1
            // 
            this.sPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.sPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.sPanel1.Controls.Add(this.label1);
            this.sPanel1.Controls.Add(this.label4);
            this.sPanel1.Controls.Add(this.label3);
            this.sPanel1.Controls.Add(this.label2);
            this.sPanel1.Controls.Add(this.lblWorld);
            this.sPanel1.Controls.Add(this.apacheServerLight);
            this.sPanel1.Controls.Add(this.mysqlServerLight);
            this.sPanel1.Controls.Add(this.bnetServerLight);
            this.sPanel1.Controls.Add(this.worldServerLight);
            this.sPanel1.Edge = 20;
            this.sPanel1.Location = new System.Drawing.Point(15, 16);
            this.sPanel1.Name = "sPanel1";
            this.sPanel1.Size = new System.Drawing.Size(248, 150);
            this.sPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 21);
            this.label1.TabIndex = 18;
            this.label1.Text = "Server Status";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label4.Location = new System.Drawing.Point(64, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Apache Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(64, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "MySQL Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label2.Location = new System.Drawing.Point(64, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Bnet / Auth Server";
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorld.Location = new System.Drawing.Point(64, 42);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(76, 15);
            this.lblWorld.TabIndex = 14;
            this.lblWorld.Text = "World Server";
            // 
            // apacheServerLight
            // 
            this.apacheServerLight.BackColor = System.Drawing.Color.DarkRed;
            this.apacheServerLight.BorderColor = System.Drawing.Color.White;
            this.apacheServerLight.Edge = 20;
            this.apacheServerLight.Location = new System.Drawing.Point(28, 112);
            this.apacheServerLight.Name = "apacheServerLight";
            this.apacheServerLight.Size = new System.Drawing.Size(19, 19);
            this.apacheServerLight.TabIndex = 10;
            // 
            // mysqlServerLight
            // 
            this.mysqlServerLight.BackColor = System.Drawing.Color.DarkRed;
            this.mysqlServerLight.BorderColor = System.Drawing.Color.White;
            this.mysqlServerLight.Edge = 20;
            this.mysqlServerLight.Location = new System.Drawing.Point(28, 87);
            this.mysqlServerLight.Name = "mysqlServerLight";
            this.mysqlServerLight.Size = new System.Drawing.Size(19, 19);
            this.mysqlServerLight.TabIndex = 12;
            // 
            // bnetServerLight
            // 
            this.bnetServerLight.BackColor = System.Drawing.Color.DarkRed;
            this.bnetServerLight.BorderColor = System.Drawing.Color.White;
            this.bnetServerLight.Edge = 20;
            this.bnetServerLight.Location = new System.Drawing.Point(28, 62);
            this.bnetServerLight.Name = "bnetServerLight";
            this.bnetServerLight.Size = new System.Drawing.Size(19, 19);
            this.bnetServerLight.TabIndex = 13;
            // 
            // worldServerLight
            // 
            this.worldServerLight.BackColor = System.Drawing.Color.DarkRed;
            this.worldServerLight.BorderColor = System.Drawing.Color.White;
            this.worldServerLight.Edge = 20;
            this.worldServerLight.Location = new System.Drawing.Point(28, 38);
            this.worldServerLight.Name = "worldServerLight";
            this.worldServerLight.Size = new System.Drawing.Size(19, 19);
            this.worldServerLight.TabIndex = 11;
            // 
            // ServerStatusTimer
            // 
            this.ServerStatusTimer.Enabled = true;
            this.ServerStatusTimer.Interval = 500;
            this.ServerStatusTimer.Tick += new System.EventHandler(this.ServerStatusTimer_Tick);
            // 
            // roundPanel1
            // 
            this.roundPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel1.BorderColor = System.Drawing.Color.White;
            this.roundPanel1.Controls.Add(this.label6);
            this.roundPanel1.Controls.Add(this.BnetCpuUsageProgressBar);
            this.roundPanel1.Controls.Add(this.BnetRamUsageProgressBar);
            this.roundPanel1.Edge = 20;
            this.roundPanel1.Location = new System.Drawing.Point(358, 183);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Size = new System.Drawing.Size(326, 150);
            this.roundPanel1.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label6.Location = new System.Drawing.Point(7, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 21);
            this.label6.TabIndex = 19;
            this.label6.Text = "Bnet  / Auth Server Resource";
            // 
            // BnetCpuUsageProgressBar
            // 
            this.BnetCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.BnetCpuUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.BnetCpuUsageProgressBar.ChannelHeight = 10;
            this.BnetCpuUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.BnetCpuUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.BnetCpuUsageProgressBar.Location = new System.Drawing.Point(22, 102);
            this.BnetCpuUsageProgressBar.Name = "BnetCpuUsageProgressBar";
            this.BnetCpuUsageProgressBar.ShowMaximun = false;
            this.BnetCpuUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.BnetCpuUsageProgressBar.Size = new System.Drawing.Size(273, 28);
            this.BnetCpuUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.BnetCpuUsageProgressBar.SliderHeight = 10;
            this.BnetCpuUsageProgressBar.SymbolAfter = "";
            this.BnetCpuUsageProgressBar.SymbolBefore = "";
            this.BnetCpuUsageProgressBar.TabIndex = 2;
            // 
            // BnetRamUsageProgressBar
            // 
            this.BnetRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.BnetRamUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.BnetRamUsageProgressBar.ChannelHeight = 10;
            this.BnetRamUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.BnetRamUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.BnetRamUsageProgressBar.Location = new System.Drawing.Point(22, 53);
            this.BnetRamUsageProgressBar.Maximum = 0;
            this.BnetRamUsageProgressBar.Name = "BnetRamUsageProgressBar";
            this.BnetRamUsageProgressBar.ShowMaximun = true;
            this.BnetRamUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.BnetRamUsageProgressBar.Size = new System.Drawing.Size(273, 28);
            this.BnetRamUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.BnetRamUsageProgressBar.SliderHeight = 10;
            this.BnetRamUsageProgressBar.SymbolAfter = "";
            this.BnetRamUsageProgressBar.SymbolBefore = "";
            this.BnetRamUsageProgressBar.TabIndex = 1;
            // 
            // roundPanel2
            // 
            this.roundPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel2.BorderColor = System.Drawing.Color.White;
            this.roundPanel2.Controls.Add(this.label7);
            this.roundPanel2.Controls.Add(this.totalCpuUsageProgressBar);
            this.roundPanel2.Controls.Add(this.totalRamUsageProgressBar);
            this.roundPanel2.Edge = 20;
            this.roundPanel2.Location = new System.Drawing.Point(279, 16);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Size = new System.Drawing.Size(405, 150);
            this.roundPanel2.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label7.Location = new System.Drawing.Point(7, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "PC Resource";
            // 
            // totalCpuUsageProgressBar
            // 
            this.totalCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.totalCpuUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.totalCpuUsageProgressBar.ChannelHeight = 10;
            this.totalCpuUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.totalCpuUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.totalCpuUsageProgressBar.Location = new System.Drawing.Point(22, 102);
            this.totalCpuUsageProgressBar.Name = "totalCpuUsageProgressBar";
            this.totalCpuUsageProgressBar.ShowMaximun = false;
            this.totalCpuUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.totalCpuUsageProgressBar.Size = new System.Drawing.Size(339, 28);
            this.totalCpuUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.totalCpuUsageProgressBar.SliderHeight = 10;
            this.totalCpuUsageProgressBar.SymbolAfter = "";
            this.totalCpuUsageProgressBar.SymbolBefore = "";
            this.totalCpuUsageProgressBar.TabIndex = 2;
            // 
            // totalRamUsageProgressBar
            // 
            this.totalRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.totalRamUsageProgressBar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.totalRamUsageProgressBar.ChannelHeight = 10;
            this.totalRamUsageProgressBar.ForeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.totalRamUsageProgressBar.ForeColor = System.Drawing.Color.White;
            this.totalRamUsageProgressBar.Location = new System.Drawing.Point(22, 53);
            this.totalRamUsageProgressBar.Maximum = 0;
            this.totalRamUsageProgressBar.Name = "totalRamUsageProgressBar";
            this.totalRamUsageProgressBar.ShowMaximun = true;
            this.totalRamUsageProgressBar.ShowValue = CypherCore_Server_Laucher.Classes.TextPosition.Center;
            this.totalRamUsageProgressBar.Size = new System.Drawing.Size(339, 28);
            this.totalRamUsageProgressBar.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(154)))), ((int)(((byte)(243)))));
            this.totalRamUsageProgressBar.SliderHeight = 10;
            this.totalRamUsageProgressBar.SymbolAfter = "";
            this.totalRamUsageProgressBar.SymbolBefore = "";
            this.totalRamUsageProgressBar.TabIndex = 1;
            // 
            // WorldResourceTimer
            // 
            this.WorldResourceTimer.Interval = 1000;
            this.WorldResourceTimer.Tick += new System.EventHandler(this.WorldResourceTimer_Tick);
            // 
            // BnetResourceTimer
            // 
            this.BnetResourceTimer.Interval = 1000;
            this.BnetResourceTimer.Tick += new System.EventHandler(this.BnetResourceTimer_Tick);
            // 
            // HomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.sPanel2);
            this.Controls.Add(this.sPanel1);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(697, 561);
            this.sPanel2.ResumeLayout(false);
            this.sPanel2.PerformLayout();
            this.sPanel1.ResumeLayout(false);
            this.sPanel1.PerformLayout();
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.roundPanel2.ResumeLayout(false);
            this.roundPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Classes.RoundPanel sPanel2;
        private Classes.RoundPanel sPanel1;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label lblWorld;
        private Classes.RoundPanel apacheServerLight;
        private Classes.RoundPanel mysqlServerLight;
        private Classes.RoundPanel bnetServerLight;
        private Classes.RoundPanel worldServerLight;
        private System.Windows.Forms.Timer ServerStatusTimer;
        private Classes.CustomProgressBar worldRamUsageProgressBar;
        private Label label5;
        private Classes.CustomProgressBar worldCpuUsageProgressBar;
        private Classes.RoundPanel roundPanel1;
        private Label label6;
        private Classes.CustomProgressBar BnetCpuUsageProgressBar;
        private Classes.CustomProgressBar BnetRamUsageProgressBar;
        private Classes.RoundPanel roundPanel2;
        private Label label7;
        private Classes.CustomProgressBar totalCpuUsageProgressBar;
        private Classes.CustomProgressBar totalRamUsageProgressBar;
        private System.Windows.Forms.Timer WorldResourceTimer;
        private System.Windows.Forms.Timer BnetResourceTimer;
    }
}
