using CypherCore_Server_Laucher.UI;
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
            this.sPanel2 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWorldResource = new System.Windows.Forms.Label();
            this.worldRamUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.worldCpuUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.sPanel1 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.lblApache = new System.Windows.Forms.Label();
            this.lblMySql = new System.Windows.Forms.Label();
            this.lblBnet = new System.Windows.Forms.Label();
            this.lblWorld = new System.Windows.Forms.Label();
            this.apacheServerLight = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.mysqlServerLight = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.bnetServerLight = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.worldServerLight = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.ServerStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.roundPanel1 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BnetCpuUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.lblBnetResource = new System.Windows.Forms.Label();
            this.BnetRamUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.roundPanel2 = new CypherCore_Server_Laucher.UI.RoundPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totalCpuUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.totalRamUsageProgressBar = new CypherCore_Server_Laucher.UI.CustomProgressBar();
            this.lblPCresource = new System.Windows.Forms.Label();
            this.WorldResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.BnetResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.bntStartAll = new CypherCore_Server_Laucher.UI.CustomButton();
            this.btnStartBent = new CypherCore_Server_Laucher.UI.CustomButton();
            this.btnStartWorld = new CypherCore_Server_Laucher.UI.CustomButton();
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
            this.sPanel2.Controls.Add(this.label4);
            this.sPanel2.Controls.Add(this.label1);
            this.sPanel2.Controls.Add(this.lblWorldResource);
            this.sPanel2.Controls.Add(this.worldRamUsageProgressBar);
            this.sPanel2.Controls.Add(this.worldCpuUsageProgressBar);
            this.sPanel2.Edge = 10;
            this.sPanel2.Location = new System.Drawing.Point(15, 180);
            this.sPanel2.Name = "sPanel2";
            this.sPanel2.Size = new System.Drawing.Size(325, 150);
            this.sPanel2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label4.Location = new System.Drawing.Point(67, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Central Processing Unit (CPU)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label1.Location = new System.Drawing.Point(67, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Random-Access Memory  (RAM)";
            // 
            // lblWorldResource
            // 
            this.lblWorldResource.AutoSize = true;
            this.lblWorldResource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorldResource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorldResource.Location = new System.Drawing.Point(7, 7);
            this.lblWorldResource.Name = "lblWorldResource";
            this.lblWorldResource.Size = new System.Drawing.Size(179, 21);
            this.lblWorldResource.TabIndex = 19;
            this.lblWorldResource.Text = "World Server Resource";
            // 
            // worldRamUsageProgressBar
            // 
            this.worldRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.worldRamUsageProgressBar.FontSize = 10;
            this.worldRamUsageProgressBar.LabelText = "MB";
            this.worldRamUsageProgressBar.Location = new System.Drawing.Point(20, 61);
            this.worldRamUsageProgressBar.MaximumValue = true;
            this.worldRamUsageProgressBar.Name = "worldRamUsageProgressBar";
            this.worldRamUsageProgressBar.Size = new System.Drawing.Size(275, 17);
            this.worldRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.worldRamUsageProgressBar.TabIndex = 25;
            this.worldRamUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // worldCpuUsageProgressBar
            // 
            this.worldCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.worldCpuUsageProgressBar.FontSize = 10;
            this.worldCpuUsageProgressBar.LabelText = "%";
            this.worldCpuUsageProgressBar.Location = new System.Drawing.Point(20, 113);
            this.worldCpuUsageProgressBar.MaximumValue = false;
            this.worldCpuUsageProgressBar.Name = "worldCpuUsageProgressBar";
            this.worldCpuUsageProgressBar.Size = new System.Drawing.Size(275, 17);
            this.worldCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.worldCpuUsageProgressBar.TabIndex = 26;
            this.worldCpuUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // sPanel1
            // 
            this.sPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.sPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.sPanel1.Controls.Add(this.lblServerStatus);
            this.sPanel1.Controls.Add(this.lblApache);
            this.sPanel1.Controls.Add(this.lblMySql);
            this.sPanel1.Controls.Add(this.lblBnet);
            this.sPanel1.Controls.Add(this.lblWorld);
            this.sPanel1.Controls.Add(this.apacheServerLight);
            this.sPanel1.Controls.Add(this.mysqlServerLight);
            this.sPanel1.Controls.Add(this.bnetServerLight);
            this.sPanel1.Controls.Add(this.worldServerLight);
            this.sPanel1.Edge = 20;
            this.sPanel1.Location = new System.Drawing.Point(15, 15);
            this.sPanel1.Name = "sPanel1";
            this.sPanel1.Size = new System.Drawing.Size(248, 150);
            this.sPanel1.TabIndex = 5;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblServerStatus.Location = new System.Drawing.Point(7, 7);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(107, 21);
            this.lblServerStatus.TabIndex = 18;
            this.lblServerStatus.Text = "Server Status";
            // 
            // lblApache
            // 
            this.lblApache.AutoSize = true;
            this.lblApache.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblApache.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblApache.Location = new System.Drawing.Point(53, 110);
            this.lblApache.Name = "lblApache";
            this.lblApache.Size = new System.Drawing.Size(99, 19);
            this.lblApache.TabIndex = 17;
            this.lblApache.Text = "Apache Server";
            // 
            // lblMySql
            // 
            this.lblMySql.AutoSize = true;
            this.lblMySql.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySql.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblMySql.Location = new System.Drawing.Point(53, 85);
            this.lblMySql.Name = "lblMySql";
            this.lblMySql.Size = new System.Drawing.Size(99, 19);
            this.lblMySql.TabIndex = 16;
            this.lblMySql.Text = "MySQL Server";
            // 
            // lblBnet
            // 
            this.lblBnet.AutoSize = true;
            this.lblBnet.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnet.Location = new System.Drawing.Point(53, 60);
            this.lblBnet.Name = "lblBnet";
            this.lblBnet.Size = new System.Drawing.Size(125, 19);
            this.lblBnet.TabIndex = 15;
            this.lblBnet.Text = "Bnet / Auth Server";
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorld.Location = new System.Drawing.Point(53, 36);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(92, 19);
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
            this.ServerStatusTimer.Interval = 1000;
            this.ServerStatusTimer.Tick += new System.EventHandler(this.ServerStatusTimer_Tick);
            // 
            // roundPanel1
            // 
            this.roundPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel1.BorderColor = System.Drawing.Color.White;
            this.roundPanel1.Controls.Add(this.label5);
            this.roundPanel1.Controls.Add(this.label2);
            this.roundPanel1.Controls.Add(this.BnetCpuUsageProgressBar);
            this.roundPanel1.Controls.Add(this.lblBnetResource);
            this.roundPanel1.Controls.Add(this.BnetRamUsageProgressBar);
            this.roundPanel1.Edge = 10;
            this.roundPanel1.Location = new System.Drawing.Point(357, 180);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Size = new System.Drawing.Size(325, 150);
            this.roundPanel1.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label5.Location = new System.Drawing.Point(62, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Central Processing Unit (CPU)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label2.Location = new System.Drawing.Point(62, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Random-Access Memory  (RAM)";
            // 
            // BnetCpuUsageProgressBar
            // 
            this.BnetCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.BnetCpuUsageProgressBar.FontSize = 10;
            this.BnetCpuUsageProgressBar.LabelText = "%";
            this.BnetCpuUsageProgressBar.Location = new System.Drawing.Point(20, 113);
            this.BnetCpuUsageProgressBar.MaximumValue = false;
            this.BnetCpuUsageProgressBar.Name = "BnetCpuUsageProgressBar";
            this.BnetCpuUsageProgressBar.Size = new System.Drawing.Size(275, 17);
            this.BnetCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BnetCpuUsageProgressBar.TabIndex = 28;
            this.BnetCpuUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // lblBnetResource
            // 
            this.lblBnetResource.AutoSize = true;
            this.lblBnetResource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBnetResource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnetResource.Location = new System.Drawing.Point(7, 7);
            this.lblBnetResource.Name = "lblBnetResource";
            this.lblBnetResource.Size = new System.Drawing.Size(222, 21);
            this.lblBnetResource.TabIndex = 19;
            this.lblBnetResource.Text = "Bnet  / Auth Server Resource";
            // 
            // BnetRamUsageProgressBar
            // 
            this.BnetRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.BnetRamUsageProgressBar.FontSize = 10;
            this.BnetRamUsageProgressBar.LabelText = "MB";
            this.BnetRamUsageProgressBar.Location = new System.Drawing.Point(20, 64);
            this.BnetRamUsageProgressBar.MaximumValue = true;
            this.BnetRamUsageProgressBar.Name = "BnetRamUsageProgressBar";
            this.BnetRamUsageProgressBar.Size = new System.Drawing.Size(275, 17);
            this.BnetRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BnetRamUsageProgressBar.TabIndex = 27;
            this.BnetRamUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // roundPanel2
            // 
            this.roundPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            this.roundPanel2.BorderColor = System.Drawing.Color.White;
            this.roundPanel2.Controls.Add(this.label6);
            this.roundPanel2.Controls.Add(this.label3);
            this.roundPanel2.Controls.Add(this.totalCpuUsageProgressBar);
            this.roundPanel2.Controls.Add(this.totalRamUsageProgressBar);
            this.roundPanel2.Controls.Add(this.lblPCresource);
            this.roundPanel2.Edge = 10;
            this.roundPanel2.Location = new System.Drawing.Point(280, 15);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Size = new System.Drawing.Size(405, 150);
            this.roundPanel2.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label6.Location = new System.Drawing.Point(108, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "Central Processing Unit (CPU)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(108, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Random-Access Memory  (RAM)";
            // 
            // totalCpuUsageProgressBar
            // 
            this.totalCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.totalCpuUsageProgressBar.FontSize = 10;
            this.totalCpuUsageProgressBar.LabelText = "%";
            this.totalCpuUsageProgressBar.Location = new System.Drawing.Point(22, 112);
            this.totalCpuUsageProgressBar.MaximumValue = false;
            this.totalCpuUsageProgressBar.Name = "totalCpuUsageProgressBar";
            this.totalCpuUsageProgressBar.Size = new System.Drawing.Size(352, 17);
            this.totalCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.totalCpuUsageProgressBar.TabIndex = 24;
            this.totalCpuUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // totalRamUsageProgressBar
            // 
            this.totalRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.totalRamUsageProgressBar.FontSize = 10;
            this.totalRamUsageProgressBar.LabelText = "MB";
            this.totalRamUsageProgressBar.Location = new System.Drawing.Point(22, 64);
            this.totalRamUsageProgressBar.MaximumValue = true;
            this.totalRamUsageProgressBar.Name = "totalRamUsageProgressBar";
            this.totalRamUsageProgressBar.Size = new System.Drawing.Size(352, 17);
            this.totalRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.totalRamUsageProgressBar.TabIndex = 23;
            this.totalRamUsageProgressBar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(59)))));
            // 
            // lblPCresource
            // 
            this.lblPCresource.AutoSize = true;
            this.lblPCresource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPCresource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPCresource.Location = new System.Drawing.Point(7, 7);
            this.lblPCresource.Name = "lblPCresource";
            this.lblPCresource.Size = new System.Drawing.Size(101, 21);
            this.lblPCresource.TabIndex = 19;
            this.lblPCresource.Text = "PC Resource";
            // 
            // WorldResourceTimer
            // 
            this.WorldResourceTimer.Tick += new System.EventHandler(this.WorldResourceTimer_Tick);
            // 
            // BnetResourceTimer
            // 
            this.BnetResourceTimer.Tick += new System.EventHandler(this.BnetResourceTimer_Tick);
            // 
            // bntStartAll
            // 
            this.bntStartAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntStartAll.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.bntStartAll.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.bntStartAll.BorderRadius = 5;
            this.bntStartAll.BorderSize = 0;
            this.bntStartAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntStartAll.FlatAppearance.BorderSize = 0;
            this.bntStartAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStartAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntStartAll.ForeColor = System.Drawing.Color.White;
            this.bntStartAll.Location = new System.Drawing.Point(565, 346);
            this.bntStartAll.Name = "bntStartAll";
            this.bntStartAll.Size = new System.Drawing.Size(120, 35);
            this.bntStartAll.TabIndex = 31;
            this.bntStartAll.Text = "Start All";
            this.bntStartAll.TextColor = System.Drawing.Color.White;
            this.bntStartAll.UseVisualStyleBackColor = false;
            this.bntStartAll.Click += new System.EventHandler(this.bntStartAll_Click);
            // 
            // btnStartBent
            // 
            this.btnStartBent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnStartBent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnStartBent.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnStartBent.BorderRadius = 5;
            this.btnStartBent.BorderSize = 0;
            this.btnStartBent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartBent.FlatAppearance.BorderSize = 0;
            this.btnStartBent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartBent.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartBent.ForeColor = System.Drawing.Color.White;
            this.btnStartBent.Location = new System.Drawing.Point(439, 346);
            this.btnStartBent.Name = "btnStartBent";
            this.btnStartBent.Size = new System.Drawing.Size(120, 35);
            this.btnStartBent.TabIndex = 32;
            this.btnStartBent.Text = "Start Bnet";
            this.btnStartBent.TextColor = System.Drawing.Color.White;
            this.btnStartBent.UseVisualStyleBackColor = false;
            this.btnStartBent.Click += new System.EventHandler(this.btnStartBent_Click);
            // 
            // btnStartWorld
            // 
            this.btnStartWorld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnStartWorld.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.btnStartWorld.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnStartWorld.BorderRadius = 5;
            this.btnStartWorld.BorderSize = 0;
            this.btnStartWorld.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartWorld.FlatAppearance.BorderSize = 0;
            this.btnStartWorld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartWorld.ForeColor = System.Drawing.Color.White;
            this.btnStartWorld.Location = new System.Drawing.Point(313, 346);
            this.btnStartWorld.Name = "btnStartWorld";
            this.btnStartWorld.Size = new System.Drawing.Size(120, 35);
            this.btnStartWorld.TabIndex = 33;
            this.btnStartWorld.Text = "Start World";
            this.btnStartWorld.TextColor = System.Drawing.Color.White;
            this.btnStartWorld.UseVisualStyleBackColor = false;
            this.btnStartWorld.Click += new System.EventHandler(this.btnStartWorld_Click);
            // 
            // HomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.btnStartWorld);
            this.Controls.Add(this.btnStartBent);
            this.Controls.Add(this.bntStartAll);
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.sPanel2);
            this.Controls.Add(this.sPanel1);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(697, 561);
            this.Load += new System.EventHandler(this.HomeControl_Load);
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

        private CypherCore_Server_Laucher.UI.RoundPanel sPanel2;
        private CypherCore_Server_Laucher.UI.RoundPanel sPanel1;
        private Label lblServerStatus;
        private Label lblApache;
        private Label lblMySql;
        private Label lblBnet;
        private Label lblWorld;
        private RoundPanel apacheServerLight;
        private RoundPanel mysqlServerLight;
        private RoundPanel bnetServerLight;
        private RoundPanel worldServerLight;
        private System.Windows.Forms.Timer ServerStatusTimer;
        private Label lblWorldResource;
        private RoundPanel roundPanel1;
        private Label lblBnetResource;
        private RoundPanel roundPanel2;
        private Label lblPCresource;
        private System.Windows.Forms.Timer WorldResourceTimer;
        private System.Windows.Forms.Timer BnetResourceTimer;
        private CustomProgressBar totalRamUsageProgressBar;
        private CustomProgressBar totalCpuUsageProgressBar;
        private CustomProgressBar worldRamUsageProgressBar;
        private CustomProgressBar worldCpuUsageProgressBar;
        private CustomProgressBar BnetCpuUsageProgressBar;
        private CustomProgressBar BnetRamUsageProgressBar;
        private Label label4;
        private Label label1;
        private Label label5;
        private Label label2;
        private Label label6;
        private Label label3;
        private CustomButton bntStartAll;
        private CustomButton btnStartBent;
        private CustomButton btnStartWorld;
    }
}
