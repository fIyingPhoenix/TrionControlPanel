namespace TrionControlPanelDesktop.Controls
{
    partial class ConsoleControl
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
            TabControl1 = new MetroFramework.Controls.MetroTabControl();
            TimerWacher = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // TabControl1
            // 
            TabControl1.CustomBackground = false;
            TabControl1.FontSize = MetroFramework.MetroTabControlSize.Medium;
            TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            TabControl1.Location = new Point(0, 0);
            TabControl1.Name = "TabControl1";
            TabControl1.Padding = new Point(6, 8);
            TabControl1.Size = new Size(845, 370);
            TabControl1.Style = MetroFramework.MetroColorStyle.Blue;
            TabControl1.StyleManager = null;
            TabControl1.TabIndex = 2;
            TabControl1.TextAlign = ContentAlignment.MiddleCenter;
            TabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            TabControl1.UseStyleColors = true;
            // 
            // TimerWacher
            // 
            TimerWacher.Enabled = true;
            TimerWacher.Interval = 1;
            TimerWacher.Tick += TimerWacher_Tick;
            // 
            // ConsoleControl
            // 
            BackColor = Color.Transparent;
            Controls.Add(TabControl1);
            ForeColor = Color.White;
            Name = "ConsoleControl";
            Size = new Size(845, 370);
            ResumeLayout(false);
        }

        #endregion

        private MetroFramework.Controls.MetroTabControl TabControl1;
        private System.Windows.Forms.Timer TimerWacher;
    }
}
