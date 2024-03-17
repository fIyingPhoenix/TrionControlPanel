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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            TimerWacher = new System.Windows.Forms.Timer(components);
            TimerNotify = new System.Windows.Forms.Timer(components);
            DGVNotifications = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            Time = new DataGridViewTextBoxColumn();
            PNLNotify = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)DGVNotifications).BeginInit();
            PNLNotify.SuspendLayout();
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
            // DGVNotifications
            // 
            DGVNotifications.AllowUserToAddRows = false;
            DGVNotifications.AllowUserToDeleteRows = false;
            DGVNotifications.AllowUserToResizeColumns = false;
            DGVNotifications.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DGVNotifications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DGVNotifications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVNotifications.Columns.AddRange(new DataGridViewColumn[] { ID, Message, Time });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            DGVNotifications.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            DGVNotifications.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            DGVNotifications.RowsDefaultCellStyle = dataGridViewCellStyle6;
            DGVNotifications.RowTemplate.Height = 25;
            DGVNotifications.RowTemplate.ReadOnly = true;
            DGVNotifications.RowTemplate.Resizable = DataGridViewTriState.True;
            DGVNotifications.ShowCellErrors = false;
            DGVNotifications.ShowCellToolTips = false;
            DGVNotifications.ShowEditingIcon = false;
            DGVNotifications.ShowRowErrors = false;
            DGVNotifications.Size = new Size(789, 290);
            DGVNotifications.TabIndex = 0;
            // 
            // ID
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(28, 33, 40);
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            // PNLNotify
            // 
            PNLNotify.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PNLNotify.BackColor = Color.FromArgb(34, 39, 46);
            PNLNotify.Border = true;
            PNLNotify.BorderColor = Color.Black;
            PNLNotify.BorderSize = 1;
            PNLNotify.Controls.Add(DGVNotifications);
            PNLNotify.CustomBackground = true;
            PNLNotify.HorizontalScrollbar = true;
            PNLNotify.HorizontalScrollbarBarColor = true;
            PNLNotify.HorizontalScrollbarHighlightOnWheel = false;
            PNLNotify.HorizontalScrollbarSize = 10;
            PNLNotify.Location = new Point(24, 25);
            PNLNotify.Name = "PNLNotify";
            PNLNotify.Padding = new Padding(2);
            PNLNotify.Size = new Size(793, 294);
            PNLNotify.Style = MetroFramework.MetroColorStyle.Blue;
            PNLNotify.StyleManager = null;
            PNLNotify.TabIndex = 31;
            PNLNotify.Theme = MetroFramework.MetroThemeStyle.Dark;
            PNLNotify.VerticalScrollbar = true;
            PNLNotify.VerticalScrollbarBarColor = true;
            PNLNotify.VerticalScrollbarHighlightOnWheel = false;
            PNLNotify.VerticalScrollbarSize = 10;
            // 
            // NotificationsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 51, 59);
            Controls.Add(PNLNotify);
            Name = "NotificationsControl";
            Size = new Size(845, 370);
            ((System.ComponentModel.ISupportInitialize)DGVNotifications).EndInit();
            PNLNotify.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer TimerWacher;
        private System.Windows.Forms.Timer TimerNotify;
        private DataGridView DGVNotifications;
        private MetroFramework.Controls.MetroPanel PNLNotify;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Message;
        private DataGridViewTextBoxColumn Time;
    }
}
