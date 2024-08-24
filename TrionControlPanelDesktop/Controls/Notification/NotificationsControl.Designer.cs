namespace TrionControlPanelDesktop.Controls.Notification
{
    partial class NotificationsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationsControl));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            TimerWacher = new System.Windows.Forms.Timer(components);
            TimerNotify = new System.Windows.Forms.Timer(components);
            BTNClean = new UI.Controls.CustomButton();
            metroPanel2 = new MetroFramework.Controls.MetroPanel();
            DGVNotifications = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            Time = new DataGridViewTextBoxColumn();
            TLTHome = new TrionUI.Controls.CustomToolTip();
            metroPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DGVNotifications).BeginInit();
            SuspendLayout();
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1000;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // TimerNotify
            // 
            TimerNotify.Enabled = true;
            TimerNotify.Interval = 1;
            TimerNotify.Tick += TimerNotify_Tick;
            // 
            // BTNClean
            // 
            BTNClean.BackColor = Color.FromArgb(28, 33, 40);
            BTNClean.BackgroundColor = Color.FromArgb(28, 33, 40);
            BTNClean.BorderColor = Color.Black;
            BTNClean.BorderRadius = 0;
            BTNClean.BorderSize = 1;
            BTNClean.Cursor = Cursors.Hand;
            BTNClean.Dock = DockStyle.Bottom;
            BTNClean.FlatAppearance.BorderSize = 0;
            BTNClean.FlatStyle = FlatStyle.Flat;
            BTNClean.ForeColor = Color.White;
            BTNClean.Image = (Image)resources.GetObject("BTNClean.Image");
            BTNClean.Location = new Point(0, 339);
            BTNClean.Name = "BTNClean";
            BTNClean.NotificationCount = 0;
            BTNClean.Size = new Size(845, 31);
            BTNClean.TabIndex = 32;
            BTNClean.TextColor = Color.White;
            TLTHome.SetToolTip(BTNClean, "Clear notification history.");
            BTNClean.UseVisualStyleBackColor = false;
            BTNClean.Click += BTNClean_Click;
            // 
            // metroPanel2
            // 
            metroPanel2.BackColor = Color.FromArgb(28, 33, 40);
            metroPanel2.Border = true;
            metroPanel2.BorderColor = Color.Black;
            metroPanel2.BorderSize = 1;
            metroPanel2.Controls.Add(DGVNotifications);
            metroPanel2.CustomBackground = true;
            metroPanel2.Dock = DockStyle.Top;
            metroPanel2.HorizontalScrollbar = false;
            metroPanel2.HorizontalScrollbarBarColor = true;
            metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            metroPanel2.HorizontalScrollbarSize = 10;
            metroPanel2.Location = new Point(0, 0);
            metroPanel2.Name = "metroPanel2";
            metroPanel2.Padding = new Padding(2);
            metroPanel2.Size = new Size(845, 338);
            metroPanel2.Style = MetroFramework.MetroColorStyle.Blue;
            metroPanel2.StyleManager = null;
            metroPanel2.TabIndex = 47;
            metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            metroPanel2.VerticalScrollbar = false;
            metroPanel2.VerticalScrollbarBarColor = true;
            metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            metroPanel2.VerticalScrollbarSize = 10;
            // 
            // DGVNotifications
            // 
            DGVNotifications.AllowUserToAddRows = false;
            DGVNotifications.AllowUserToDeleteRows = false;
            DGVNotifications.AllowUserToResizeColumns = false;
            DGVNotifications.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            DGVNotifications.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DGVNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVNotifications.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DGVNotifications.BackgroundColor = Color.FromArgb(34, 39, 46);
            DGVNotifications.BorderStyle = BorderStyle.None;
            DGVNotifications.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DGVNotifications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DGVNotifications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVNotifications.Columns.AddRange(new DataGridViewColumn[] { ID, Message, Time });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DGVNotifications.DefaultCellStyle = dataGridViewCellStyle4;
            DGVNotifications.Dock = DockStyle.Fill;
            DGVNotifications.EnableHeadersVisualStyles = false;
            DGVNotifications.GridColor = Color.Black;
            DGVNotifications.Location = new Point(2, 2);
            DGVNotifications.Name = "DGVNotifications";
            DGVNotifications.ReadOnly = true;
            DGVNotifications.RightToLeft = RightToLeft.No;
            DGVNotifications.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            DGVNotifications.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            DGVNotifications.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DGVNotifications.RowsDefaultCellStyle = dataGridViewCellStyle6;
            DGVNotifications.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DGVNotifications.RowTemplate.ReadOnly = true;
            DGVNotifications.RowTemplate.Resizable = DataGridViewTriState.True;
            DGVNotifications.ShowCellErrors = false;
            DGVNotifications.ShowCellToolTips = false;
            DGVNotifications.ShowEditingIcon = false;
            DGVNotifications.ShowRowErrors = false;
            DGVNotifications.Size = new Size(841, 334);
            DGVNotifications.TabIndex = 35;
            // 
            // ID
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            ID.DefaultCellStyle = dataGridViewCellStyle3;
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Visible = false;
            // 
            // Message
            // 
            Message.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Message.HeaderText = "Message";
            Message.Name = "Message";
            Message.ReadOnly = true;
            // 
            // Time
            // 
            Time.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Time.FillWeight = 30F;
            Time.HeaderText = "Time";
            Time.Name = "Time";
            Time.ReadOnly = true;
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
            // NotificationsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(metroPanel2);
            Controls.Add(BTNClean);
            Name = "NotificationsControl";
            Size = new Size(845, 370);
            metroPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DGVNotifications).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer TimerWacher;
        private System.Windows.Forms.Timer TimerNotify;
        private UI.Controls.CustomButton BTNClean;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private DataGridView DGVNotifications;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Message;
        private DataGridViewTextBoxColumn Time;
        private TrionUI.Controls.CustomToolTip TLTHome;
    }
}
