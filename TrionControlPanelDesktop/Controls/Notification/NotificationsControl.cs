using System.Windows.Forms;
using TrionControlPanelDesktop.FormData;
using TrionLibrary;

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
            Data.Message = string.Empty;
            // Refresh the DataGridView to reflect the changes
            DGVNotifications.Refresh();

            //Update Notification count
            UIData.Notyfications++;
        }
        private void TimerNotify_Tick(object sender, EventArgs e)
        {
            if (Data.Message != string.Empty)
            {
                AddItem(Data.Message, DateTime.Now);
            }
        }
        private void BTNClean_Click(object sender, EventArgs e)
        {
            DGVNotifications.Rows.Clear();
            UIData.Notyfications = DGVNotifications.Rows.Count;
        }
    }
}

