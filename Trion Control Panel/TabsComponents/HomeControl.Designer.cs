using TrionControlPanel.UI;
namespace TrionControlPanel.TabsComponents
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
            this.sPanel2 = new CustomPanel();
            this.lblWorldResource = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.worldCpuUsageProgressBar = new CustomProgressBar();
            this.worldRamUsageProgressBar = new CustomProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.sPanel1 = new CustomPanel();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.lblMySql = new System.Windows.Forms.Label();
            this.lblBnet = new System.Windows.Forms.Label();
            this.lblWorld = new System.Windows.Forms.Label();
            this.mysqlServerLight = new CustomPanel();
            this.worldServerLight = new CustomPanel();
            this.bnetServerLight = new CustomPanel();
            this.ServerStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.roundPanel1 = new CustomPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BnetCpuUsageProgressBar = new CustomProgressBar();
            this.lblBnetResource = new System.Windows.Forms.Label();
            this.BnetRamUsageProgressBar = new CustomProgressBar();
            this.roundPanel2 = new CustomPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totalCpuUsageProgressBar = new CustomProgressBar();
            this.totalRamUsageProgressBar = new CustomProgressBar();
            this.lblPCresource = new System.Windows.Forms.Label();
            this.WorldResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.BnetResourceTimer = new System.Windows.Forms.Timer(this.components);
            this.bntStartAll = new CustomButton();
            this.btnStartBent = new CustomButton();
            this.btnStartWorld = new CustomButton();
            this.bntStopAll = new CustomButton();
            this.btnStopWorld = new CustomButton();
            this.btnStopBnet = new CustomButton();
            this.customWebBrowser1 = new CustomWebBrowser();
            this.customWebBrowser2 = new CustomWebBrowser();
            this.customWebBrowser3 = new CustomWebBrowser();
            this.customWebBrowser4 = new CustomWebBrowser();
            this.customWebBrowser6 = new CustomWebBrowser();
            this.pBarDownloadMysql = new CustomProgressBar();
            this.bntDownloadMysql = new CustomButton();
            this.bWorkerDownloadComplate = new System.ComponentModel.BackgroundWorker();
            this.bntStopMysql = new CustomButton();
            this.btnStartMysql = new CustomButton();
            this.customPanel1 = new CustomPanel();
            this.SuspendLayout();
            // 
            // sPanel2
            // 
            this.sPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.sPanel2.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.sPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.sPanel2.BorderSize = 1;
            this.sPanel2.Edge = 0;
            this.sPanel2.Location = new System.Drawing.Point(10, 224);
            this.sPanel2.Name = "sPanel2";
            this.sPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.sPanel2.Size = new System.Drawing.Size(325, 152);
            this.sPanel2.TabIndex = 6;
            // 
            // lblWorldResource
            // 
            this.lblWorldResource.AutoSize = true;
            this.lblWorldResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblWorldResource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorldResource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorldResource.Location = new System.Drawing.Point(20, 225);
            this.lblWorldResource.Name = "lblWorldResource";
            this.lblWorldResource.Size = new System.Drawing.Size(179, 21);
            this.lblWorldResource.TabIndex = 19;
            this.lblWorldResource.Text = "World Server Resource";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label4.Location = new System.Drawing.Point(91, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Central Processing Unit (CPU)";
            // 
            // worldCpuUsageProgressBar
            // 
            this.worldCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.worldCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.worldCpuUsageProgressBar.FontSize = 10;
            this.worldCpuUsageProgressBar.LabelText = "%";
            this.worldCpuUsageProgressBar.Location = new System.Drawing.Point(21, 332);
            this.worldCpuUsageProgressBar.MaximumValue = false;
            this.worldCpuUsageProgressBar.Name = "worldCpuUsageProgressBar";
            this.worldCpuUsageProgressBar.ShowStatus = true;
            this.worldCpuUsageProgressBar.Size = new System.Drawing.Size(305, 20);
            this.worldCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.worldCpuUsageProgressBar.TabIndex = 26;
            this.worldCpuUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // worldRamUsageProgressBar
            // 
            this.worldRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.worldRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.worldRamUsageProgressBar.FontSize = 10;
            this.worldRamUsageProgressBar.LabelText = "MB";
            this.worldRamUsageProgressBar.Location = new System.Drawing.Point(21, 283);
            this.worldRamUsageProgressBar.MaximumValue = true;
            this.worldRamUsageProgressBar.Name = "worldRamUsageProgressBar";
            this.worldRamUsageProgressBar.ShowStatus = true;
            this.worldRamUsageProgressBar.Size = new System.Drawing.Size(305, 20);
            this.worldRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.worldRamUsageProgressBar.TabIndex = 25;
            this.worldRamUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label1.Location = new System.Drawing.Point(83, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Random-Access Memory  (RAM)";
            // 
            // sPanel1
            // 
            this.sPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.sPanel1.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.sPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.sPanel1.BorderSize = 1;
            this.sPanel1.Edge = 0;
            this.sPanel1.Location = new System.Drawing.Point(10, 64);
            this.sPanel1.Name = "sPanel1";
            this.sPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.sPanel1.Size = new System.Drawing.Size(254, 151);
            this.sPanel1.TabIndex = 5;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblServerStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblServerStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblServerStatus.Location = new System.Drawing.Point(20, 73);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(107, 21);
            this.lblServerStatus.TabIndex = 18;
            this.lblServerStatus.Text = "Server Status";
            // 
            // lblMySql
            // 
            this.lblMySql.AutoSize = true;
            this.lblMySql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblMySql.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMySql.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblMySql.Location = new System.Drawing.Point(58, 171);
            this.lblMySql.Name = "lblMySql";
            this.lblMySql.Size = new System.Drawing.Size(99, 19);
            this.lblMySql.TabIndex = 16;
            this.lblMySql.Text = "MySQL Server";
            // 
            // lblBnet
            // 
            this.lblBnet.AutoSize = true;
            this.lblBnet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblBnet.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnet.Location = new System.Drawing.Point(57, 140);
            this.lblBnet.Name = "lblBnet";
            this.lblBnet.Size = new System.Drawing.Size(125, 19);
            this.lblBnet.TabIndex = 15;
            this.lblBnet.Text = "Bnet / Auth Server";
            // 
            // lblWorld
            // 
            this.lblWorld.AutoSize = true;
            this.lblWorld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWorld.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblWorld.Location = new System.Drawing.Point(58, 109);
            this.lblWorld.Name = "lblWorld";
            this.lblWorld.Size = new System.Drawing.Size(92, 19);
            this.lblWorld.TabIndex = 14;
            this.lblWorld.Text = "World Server";
            // 
            // mysqlServerLight
            // 
            this.mysqlServerLight.BackColor = System.Drawing.Color.White;
            this.mysqlServerLight.BodyColor = System.Drawing.Color.Red;
            this.mysqlServerLight.BorderColor = System.Drawing.Color.White;
            this.mysqlServerLight.BorderSize = 0;
            this.mysqlServerLight.Edge = 20;
            this.mysqlServerLight.Location = new System.Drawing.Point(32, 171);
            this.mysqlServerLight.Name = "mysqlServerLight";
            this.mysqlServerLight.Size = new System.Drawing.Size(19, 19);
            this.mysqlServerLight.TabIndex = 12;
            // 
            // worldServerLight
            // 
            this.worldServerLight.BackColor = System.Drawing.Color.White;
            this.worldServerLight.BodyColor = System.Drawing.Color.Red;
            this.worldServerLight.BorderColor = System.Drawing.Color.White;
            this.worldServerLight.BorderSize = 0;
            this.worldServerLight.Edge = 20;
            this.worldServerLight.Location = new System.Drawing.Point(32, 109);
            this.worldServerLight.Name = "worldServerLight";
            this.worldServerLight.Size = new System.Drawing.Size(19, 19);
            this.worldServerLight.TabIndex = 11;
            // 
            // bnetServerLight
            // 
            this.bnetServerLight.BackColor = System.Drawing.Color.White;
            this.bnetServerLight.BodyColor = System.Drawing.Color.Red;
            this.bnetServerLight.BorderColor = System.Drawing.Color.White;
            this.bnetServerLight.BorderSize = 0;
            this.bnetServerLight.Edge = 20;
            this.bnetServerLight.Location = new System.Drawing.Point(32, 140);
            this.bnetServerLight.Name = "bnetServerLight";
            this.bnetServerLight.Size = new System.Drawing.Size(19, 19);
            this.bnetServerLight.TabIndex = 13;
            // 
            // ServerStatusTimer
            // 
            this.ServerStatusTimer.Enabled = true;
            this.ServerStatusTimer.Interval = 1000;
            this.ServerStatusTimer.Tick += new System.EventHandler(this.ServerStatusTimer_Tick);
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.roundPanel1.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.roundPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.roundPanel1.BorderSize = 1;
            this.roundPanel1.Edge = 0;
            this.roundPanel1.Location = new System.Drawing.Point(346, 224);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.roundPanel1.Size = new System.Drawing.Size(338, 152);
            this.roundPanel1.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label5.Location = new System.Drawing.Point(432, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Central Processing Unit (CPU)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label2.Location = new System.Drawing.Point(424, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Random-Access Memory  (RAM)";
            // 
            // BnetCpuUsageProgressBar
            // 
            this.BnetCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.BnetCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BnetCpuUsageProgressBar.FontSize = 10;
            this.BnetCpuUsageProgressBar.LabelText = "%";
            this.BnetCpuUsageProgressBar.Location = new System.Drawing.Point(359, 331);
            this.BnetCpuUsageProgressBar.MaximumValue = false;
            this.BnetCpuUsageProgressBar.Name = "BnetCpuUsageProgressBar";
            this.BnetCpuUsageProgressBar.ShowStatus = true;
            this.BnetCpuUsageProgressBar.Size = new System.Drawing.Size(310, 20);
            this.BnetCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BnetCpuUsageProgressBar.TabIndex = 28;
            this.BnetCpuUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // lblBnetResource
            // 
            this.lblBnetResource.AutoSize = true;
            this.lblBnetResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblBnetResource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBnetResource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblBnetResource.Location = new System.Drawing.Point(356, 225);
            this.lblBnetResource.Name = "lblBnetResource";
            this.lblBnetResource.Size = new System.Drawing.Size(222, 21);
            this.lblBnetResource.TabIndex = 19;
            this.lblBnetResource.Text = "Bnet  / Auth Server Resource";
            // 
            // BnetRamUsageProgressBar
            // 
            this.BnetRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.BnetRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.BnetRamUsageProgressBar.FontSize = 10;
            this.BnetRamUsageProgressBar.LabelText = "MB";
            this.BnetRamUsageProgressBar.Location = new System.Drawing.Point(359, 282);
            this.BnetRamUsageProgressBar.MaximumValue = true;
            this.BnetRamUsageProgressBar.Name = "BnetRamUsageProgressBar";
            this.BnetRamUsageProgressBar.ShowStatus = true;
            this.BnetRamUsageProgressBar.Size = new System.Drawing.Size(310, 20);
            this.BnetRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BnetRamUsageProgressBar.TabIndex = 27;
            this.BnetRamUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.roundPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.roundPanel2.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.roundPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.roundPanel2.BorderSize = 1;
            this.roundPanel2.Edge = 0;
            this.roundPanel2.Location = new System.Drawing.Point(275, 64);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.roundPanel2.Size = new System.Drawing.Size(409, 151);
            this.roundPanel2.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label6.Location = new System.Drawing.Point(395, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "Central Processing Unit (CPU)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(387, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Random-Access Memory  (RAM)";
            // 
            // totalCpuUsageProgressBar
            // 
            this.totalCpuUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.totalCpuUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.totalCpuUsageProgressBar.FontSize = 10;
            this.totalCpuUsageProgressBar.LabelText = "%";
            this.totalCpuUsageProgressBar.Location = new System.Drawing.Point(287, 177);
            this.totalCpuUsageProgressBar.MaximumValue = false;
            this.totalCpuUsageProgressBar.Name = "totalCpuUsageProgressBar";
            this.totalCpuUsageProgressBar.ShowStatus = true;
            this.totalCpuUsageProgressBar.Size = new System.Drawing.Size(380, 17);
            this.totalCpuUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.totalCpuUsageProgressBar.TabIndex = 24;
            this.totalCpuUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // totalRamUsageProgressBar
            // 
            this.totalRamUsageProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.totalRamUsageProgressBar.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.totalRamUsageProgressBar.FontSize = 10;
            this.totalRamUsageProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.totalRamUsageProgressBar.LabelText = "MB";
            this.totalRamUsageProgressBar.Location = new System.Drawing.Point(287, 129);
            this.totalRamUsageProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.totalRamUsageProgressBar.MaximumValue = true;
            this.totalRamUsageProgressBar.Name = "totalRamUsageProgressBar";
            this.totalRamUsageProgressBar.ShowStatus = true;
            this.totalRamUsageProgressBar.Size = new System.Drawing.Size(380, 17);
            this.totalRamUsageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.totalRamUsageProgressBar.TabIndex = 23;
            this.totalRamUsageProgressBar.TextColor = System.Drawing.Color.White;
            // 
            // lblPCresource
            // 
            this.lblPCresource.AutoSize = true;
            this.lblPCresource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.lblPCresource.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPCresource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.lblPCresource.Location = new System.Drawing.Point(286, 73);
            this.lblPCresource.Name = "lblPCresource";
            this.lblPCresource.Size = new System.Drawing.Size(101, 21);
            this.lblPCresource.TabIndex = 19;
            this.lblPCresource.Text = "PC Resource";
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
            // bntStartAll
            // 
            this.bntStartAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntStartAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStartAll.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStartAll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.bntStartAll.BorderRadius = 0;
            this.bntStartAll.BorderSize = 1;
            this.bntStartAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntStartAll.FlatAppearance.BorderSize = 0;
            this.bntStartAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStartAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntStartAll.ForeColor = System.Drawing.Color.White;
            this.bntStartAll.Location = new System.Drawing.Point(494, 388);
            this.bntStartAll.Name = "bntStartAll";
            this.bntStartAll.Size = new System.Drawing.Size(192, 40);
            this.bntStartAll.TabIndex = 31;
            this.bntStartAll.Text = "Start All";
            this.bntStartAll.TextColor = System.Drawing.Color.White;
            this.bntStartAll.UseVisualStyleBackColor = false;
            this.bntStartAll.Click += new System.EventHandler(this.BntStartAll_Click);
            // 
            // btnStartBent
            // 
            this.btnStartBent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartBent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartBent.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartBent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStartBent.BorderRadius = 0;
            this.btnStartBent.BorderSize = 1;
            this.btnStartBent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartBent.FlatAppearance.BorderSize = 0;
            this.btnStartBent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartBent.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartBent.ForeColor = System.Drawing.Color.White;
            this.btnStartBent.Location = new System.Drawing.Point(463, 436);
            this.btnStartBent.Name = "btnStartBent";
            this.btnStartBent.Size = new System.Drawing.Size(108, 35);
            this.btnStartBent.TabIndex = 32;
            this.btnStartBent.Text = "Start Bnet";
            this.btnStartBent.TextColor = System.Drawing.Color.White;
            this.btnStartBent.UseVisualStyleBackColor = false;
            this.btnStartBent.Click += new System.EventHandler(this.BtnStartBent_Click);
            // 
            // btnStartWorld
            // 
            this.btnStartWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartWorld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartWorld.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartWorld.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStartWorld.BorderRadius = 0;
            this.btnStartWorld.BorderSize = 1;
            this.btnStartWorld.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartWorld.FlatAppearance.BorderSize = 0;
            this.btnStartWorld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartWorld.ForeColor = System.Drawing.Color.White;
            this.btnStartWorld.Location = new System.Drawing.Point(578, 436);
            this.btnStartWorld.Name = "btnStartWorld";
            this.btnStartWorld.Size = new System.Drawing.Size(108, 35);
            this.btnStartWorld.TabIndex = 33;
            this.btnStartWorld.Text = "Start World";
            this.btnStartWorld.TextColor = System.Drawing.Color.White;
            this.btnStartWorld.UseVisualStyleBackColor = false;
            this.btnStartWorld.Click += new System.EventHandler(this.BtnStartWorld_Click);
            // 
            // bntStopAll
            // 
            this.bntStopAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntStopAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStopAll.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStopAll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.bntStopAll.BorderRadius = 0;
            this.bntStopAll.BorderSize = 1;
            this.bntStopAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntStopAll.FlatAppearance.BorderSize = 0;
            this.bntStopAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStopAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntStopAll.ForeColor = System.Drawing.Color.White;
            this.bntStopAll.Location = new System.Drawing.Point(10, 388);
            this.bntStopAll.Name = "bntStopAll";
            this.bntStopAll.Size = new System.Drawing.Size(192, 40);
            this.bntStopAll.TabIndex = 34;
            this.bntStopAll.Text = "Stop All";
            this.bntStopAll.TextColor = System.Drawing.Color.White;
            this.bntStopAll.UseVisualStyleBackColor = false;
            this.bntStopAll.Click += new System.EventHandler(this.BntStopAll_Click);
            // 
            // btnStopWorld
            // 
            this.btnStopWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopWorld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStopWorld.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStopWorld.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStopWorld.BorderRadius = 0;
            this.btnStopWorld.BorderSize = 1;
            this.btnStopWorld.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopWorld.FlatAppearance.BorderSize = 0;
            this.btnStopWorld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopWorld.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStopWorld.ForeColor = System.Drawing.Color.White;
            this.btnStopWorld.Location = new System.Drawing.Point(122, 436);
            this.btnStopWorld.Name = "btnStopWorld";
            this.btnStopWorld.Size = new System.Drawing.Size(108, 35);
            this.btnStopWorld.TabIndex = 35;
            this.btnStopWorld.Text = "Stop World";
            this.btnStopWorld.TextColor = System.Drawing.Color.White;
            this.btnStopWorld.UseVisualStyleBackColor = false;
            this.btnStopWorld.Click += new System.EventHandler(this.BtnStopWorld_Click);
            // 
            // btnStopBnet
            // 
            this.btnStopBnet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopBnet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStopBnet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStopBnet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStopBnet.BorderRadius = 0;
            this.btnStopBnet.BorderSize = 1;
            this.btnStopBnet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopBnet.FlatAppearance.BorderSize = 0;
            this.btnStopBnet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopBnet.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStopBnet.ForeColor = System.Drawing.Color.White;
            this.btnStopBnet.Location = new System.Drawing.Point(236, 436);
            this.btnStopBnet.Name = "btnStopBnet";
            this.btnStopBnet.Size = new System.Drawing.Size(108, 35);
            this.btnStopBnet.TabIndex = 36;
            this.btnStopBnet.Text = "Stop Bnet";
            this.btnStopBnet.TextColor = System.Drawing.Color.White;
            this.btnStopBnet.UseVisualStyleBackColor = false;
            this.btnStopBnet.Click += new System.EventHandler(this.BtnStopBnet_Click);
            // 
            // customWebBrowser1
            // 
            this.customWebBrowser1.AllowNavigation = false;
            this.customWebBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.customWebBrowser1.Location = new System.Drawing.Point(16, 20);
            this.customWebBrowser1.Name = "customWebBrowser1";
            this.customWebBrowser1.ScriptErrorsSuppressed = true;
            this.customWebBrowser1.ScrollBarsEnabled = false;
            this.customWebBrowser1.Size = new System.Drawing.Size(142, 28);
            this.customWebBrowser1.TabIndex = 37;
            this.customWebBrowser1.Url = new System.Uri("https://img.shields.io/github/issues/fIyingPhoenix/TrionControlPanel.svg?style=fo" +
        "r-the-badge", System.UriKind.Absolute);
            // 
            // customWebBrowser2
            // 
            this.customWebBrowser2.AllowNavigation = false;
            this.customWebBrowser2.IsWebBrowserContextMenuEnabled = false;
            this.customWebBrowser2.Location = new System.Drawing.Point(164, 20);
            this.customWebBrowser2.Name = "customWebBrowser2";
            this.customWebBrowser2.ScriptErrorsSuppressed = true;
            this.customWebBrowser2.ScrollBarsEnabled = false;
            this.customWebBrowser2.Size = new System.Drawing.Size(100, 28);
            this.customWebBrowser2.TabIndex = 38;
            this.customWebBrowser2.Url = new System.Uri("https://img.shields.io/github/stars/fIyingPhoenix/TrionControlPanel.svg?style=for" +
        "-the-badge", System.UriKind.Absolute);
            // 
            // customWebBrowser3
            // 
            this.customWebBrowser3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.customWebBrowser3.AllowNavigation = false;
            this.customWebBrowser3.AllowWebBrowserDrop = false;
            this.customWebBrowser3.IsWebBrowserContextMenuEnabled = false;
            this.customWebBrowser3.Location = new System.Drawing.Point(269, 20);
            this.customWebBrowser3.Name = "customWebBrowser3";
            this.customWebBrowser3.ScriptErrorsSuppressed = true;
            this.customWebBrowser3.ScrollBarsEnabled = false;
            this.customWebBrowser3.Size = new System.Drawing.Size(97, 28);
            this.customWebBrowser3.TabIndex = 39;
            this.customWebBrowser3.Url = new System.Uri("https://img.shields.io/github/forks/fIyingPhoenix/TrionControlPanel.svg?style=for" +
        "-the-badge", System.UriKind.Absolute);
            this.customWebBrowser3.WebBrowserShortcutsEnabled = false;
            // 
            // customWebBrowser4
            // 
            this.customWebBrowser4.AllowNavigation = false;
            this.customWebBrowser4.AllowWebBrowserDrop = false;
            this.customWebBrowser4.IsWebBrowserContextMenuEnabled = false;
            this.customWebBrowser4.Location = new System.Drawing.Point(372, 20);
            this.customWebBrowser4.Name = "customWebBrowser4";
            this.customWebBrowser4.ScriptErrorsSuppressed = true;
            this.customWebBrowser4.ScrollBarsEnabled = false;
            this.customWebBrowser4.Size = new System.Drawing.Size(152, 28);
            this.customWebBrowser4.TabIndex = 40;
            this.customWebBrowser4.Url = new System.Uri("https://img.shields.io/github/contributors/fIyingPhoenix/TrionControlPanel.svg?st" +
        "yle=for-the-badge", System.UriKind.Absolute);
            this.customWebBrowser4.WebBrowserShortcutsEnabled = false;
            // 
            // customWebBrowser6
            // 
            this.customWebBrowser6.AllowNavigation = false;
            this.customWebBrowser6.Location = new System.Drawing.Point(530, 20);
            this.customWebBrowser6.Name = "customWebBrowser6";
            this.customWebBrowser6.ScriptErrorsSuppressed = true;
            this.customWebBrowser6.ScrollBarsEnabled = false;
            this.customWebBrowser6.Size = new System.Drawing.Size(147, 28);
            this.customWebBrowser6.TabIndex = 42;
            this.customWebBrowser6.Url = new System.Uri("https://img.shields.io/codefactor/grade/github/fIyingPhoenix/TrionControlPanel?st" +
        "yle=for-the-badge", System.UriKind.Absolute);
            // 
            // pBarDownloadMysql
            // 
            this.pBarDownloadMysql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pBarDownloadMysql.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.pBarDownloadMysql.FontSize = 1;
            this.pBarDownloadMysql.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.pBarDownloadMysql.LabelText = "";
            this.pBarDownloadMysql.Location = new System.Drawing.Point(211, 422);
            this.pBarDownloadMysql.MaximumValue = false;
            this.pBarDownloadMysql.Name = "pBarDownloadMysql";
            this.pBarDownloadMysql.ShowStatus = false;
            this.pBarDownloadMysql.Size = new System.Drawing.Size(277, 5);
            this.pBarDownloadMysql.Step = 0;
            this.pBarDownloadMysql.TabIndex = 44;
            this.pBarDownloadMysql.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(245)))));
            this.pBarDownloadMysql.Visible = false;
            // 
            // bntDownloadMysql
            // 
            this.bntDownloadMysql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntDownloadMysql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntDownloadMysql.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntDownloadMysql.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.bntDownloadMysql.BorderRadius = 0;
            this.bntDownloadMysql.BorderSize = 1;
            this.bntDownloadMysql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntDownloadMysql.FlatAppearance.BorderSize = 0;
            this.bntDownloadMysql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntDownloadMysql.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntDownloadMysql.ForeColor = System.Drawing.Color.White;
            this.bntDownloadMysql.Location = new System.Drawing.Point(209, 388);
            this.bntDownloadMysql.Name = "bntDownloadMysql";
            this.bntDownloadMysql.Size = new System.Drawing.Size(280, 40);
            this.bntDownloadMysql.TabIndex = 43;
            this.bntDownloadMysql.Text = "Download MySQL Server";
            this.bntDownloadMysql.TextColor = System.Drawing.Color.White;
            this.bntDownloadMysql.UseVisualStyleBackColor = false;
            this.bntDownloadMysql.Click += new System.EventHandler(this.BntDownloadMysql_Click);
            // 
            // bWorkerDownloadComplate
            // 
            this.bWorkerDownloadComplate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWorkerDownloadComplate_DoWork);
            this.bWorkerDownloadComplate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWorkerDownloadComplate_RunWorkerCompleted);
            // 
            // bntStopMysql
            // 
            this.bntStopMysql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bntStopMysql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStopMysql.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.bntStopMysql.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.bntStopMysql.BorderRadius = 0;
            this.bntStopMysql.BorderSize = 1;
            this.bntStopMysql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntStopMysql.FlatAppearance.BorderSize = 0;
            this.bntStopMysql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntStopMysql.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bntStopMysql.ForeColor = System.Drawing.Color.White;
            this.bntStopMysql.Location = new System.Drawing.Point(10, 436);
            this.bntStopMysql.Name = "bntStopMysql";
            this.bntStopMysql.Size = new System.Drawing.Size(108, 35);
            this.bntStopMysql.TabIndex = 45;
            this.bntStopMysql.Text = "Stop MySQL";
            this.bntStopMysql.TextColor = System.Drawing.Color.White;
            this.bntStopMysql.UseVisualStyleBackColor = false;
            this.bntStopMysql.Click += new System.EventHandler(this.BntStopMysql_Click);
            // 
            // btnStartMysql
            // 
            this.btnStartMysql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartMysql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartMysql.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(26)))), ((int)(((byte)(30)))));
            this.btnStartMysql.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnStartMysql.BorderRadius = 0;
            this.btnStartMysql.BorderSize = 1;
            this.btnStartMysql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartMysql.FlatAppearance.BorderSize = 0;
            this.btnStartMysql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartMysql.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartMysql.ForeColor = System.Drawing.Color.White;
            this.btnStartMysql.Location = new System.Drawing.Point(350, 436);
            this.btnStartMysql.Name = "btnStartMysql";
            this.btnStartMysql.Size = new System.Drawing.Size(108, 35);
            this.btnStartMysql.TabIndex = 46;
            this.btnStartMysql.Text = "Start MySQL";
            this.btnStartMysql.TextColor = System.Drawing.Color.White;
            this.btnStartMysql.UseVisualStyleBackColor = false;
            this.btnStartMysql.Click += new System.EventHandler(this.BtnStartMysql_Click);
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
            this.customPanel1.Size = new System.Drawing.Size(674, 38);
            this.customPanel1.TabIndex = 47;
            // 
            // HomeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMySql);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblBnet);
            this.Controls.Add(this.lblWorld);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mysqlServerLight);
            this.Controls.Add(this.totalCpuUsageProgressBar);
            this.Controls.Add(this.worldServerLight);
            this.Controls.Add(this.worldCpuUsageProgressBar);
            this.Controls.Add(this.totalRamUsageProgressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPCresource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BnetCpuUsageProgressBar);
            this.Controls.Add(this.lblWorldResource);
            this.Controls.Add(this.lblBnetResource);
            this.Controls.Add(this.BnetRamUsageProgressBar);
            this.Controls.Add(this.worldRamUsageProgressBar);
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnetServerLight);
            this.Controls.Add(this.btnStartMysql);
            this.Controls.Add(this.bntStopMysql);
            this.Controls.Add(this.pBarDownloadMysql);
            this.Controls.Add(this.bntDownloadMysql);
            this.Controls.Add(this.customWebBrowser6);
            this.Controls.Add(this.customWebBrowser4);
            this.Controls.Add(this.customWebBrowser3);
            this.Controls.Add(this.customWebBrowser2);
            this.Controls.Add(this.customWebBrowser1);
            this.Controls.Add(this.btnStopBnet);
            this.Controls.Add(this.btnStopWorld);
            this.Controls.Add(this.bntStopAll);
            this.Controls.Add(this.btnStartWorld);
            this.Controls.Add(this.btnStartBent);
            this.Controls.Add(this.bntStartAll);
            this.Controls.Add(this.sPanel2);
            this.Controls.Add(this.sPanel1);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.customPanel1);
            this.MaximumSize = new System.Drawing.Size(700, 560);
            this.Name = "HomeControl";
            this.Size = new System.Drawing.Size(700, 560);
            this.Load += new System.EventHandler(this.HomeControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomPanel sPanel2;
        private CustomPanel sPanel1;
        private Label lblServerStatus;
        private Label lblMySql;
        private Label lblBnet;
        private Label lblWorld;
        private CustomPanel mysqlServerLight;
        private CustomPanel bnetServerLight;
        private CustomPanel worldServerLight;
        private System.Windows.Forms.Timer ServerStatusTimer;
        private Label lblWorldResource;
        private CustomPanel roundPanel1;
        private Label lblBnetResource;
        private CustomPanel roundPanel2;
        private Label lblPCresource;
        private System.Windows.Forms.Timer WorldResourceTimer;
        private System.Windows.Forms.Timer BnetResourceTimer;
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
        private CustomButton bntStopAll;
        private CustomButton btnStopWorld;
        private CustomButton btnStopBnet;
        private CustomWebBrowser customWebBrowser1;
        private CustomWebBrowser customWebBrowser2;
        private CustomWebBrowser customWebBrowser3;
        private CustomWebBrowser customWebBrowser4;
        internal CustomProgressBar totalRamUsageProgressBar;
        private CustomWebBrowser customWebBrowser6;
        private CustomProgressBar pBarDownloadMysql;
        private CustomButton bntDownloadMysql;
        private System.ComponentModel.BackgroundWorker bWorkerDownloadComplate;
        private CustomButton bntStopMysql;
        private CustomButton btnStartMysql;
        private CustomPanel customPanel1;
    }
}
