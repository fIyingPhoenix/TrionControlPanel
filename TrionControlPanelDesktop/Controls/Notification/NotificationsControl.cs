using System.Media;
using TrionControlPanelDesktop.Data;
using TrionLibrary.Setting;
using TrionLibrary.Sys;

namespace TrionControlPanelDesktop.Controls.Notification
{
    public partial class NotificationsControl : UserControl
    {

        public NotificationsControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {

        }
        private void AddItem(string message, DateTime DateTime)
        {
            // Add a new row to the DataTable
            DGVNotifications.Rows.Add(DGVNotifications.Rows.Count + 1.ToString(), message.ToString(), DateTime.ToString());
            Infos.Message = string.Empty;
            // Refresh the DataGridView to reflect the changes
            DGVNotifications.Refresh();

            //Update Notification count
            User.UI.Form.Notyfications++;
        }
        private void TimerNotify_Tick(object sender, EventArgs e)
        {
            // Create an instance of SoundPlayer
            SoundPlayer player = new SoundPlayer(Properties.Resources.notySound);
            if (Infos.Message != string.Empty)
            {
                if (Setting.List.NotificationSound)
                {
                    player.PlaySync();
                }
                AddItem(Infos.Message, DateTime.Now);
            }
        }
        private void BTNClean_Click(object sender, EventArgs e)
        {
            DGVNotifications.Rows.Clear();
            User.UI.Form.Notyfications = DGVNotifications.Rows.Count;
        }
    }
}

