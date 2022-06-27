using TrionControlPanel.Properties;
using TrionControlPanel.Classes;
using System.Media;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace TrionControlPanel.Forms
{
    public partial class FormAlert : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
             int nLeftRect,     // x-coordinate of upper-left corner
             int nTopRect,      // y-coordinate of upper-left corner
             int nRightRect,    // x-coordinate of lower-right corner
             int nBottomRect,   // y-coordinate of lower-right corner
             int nWidthEllipse, // width of ellipse
             int nHeightEllipse // height of ellipse
         );
        private NotificationAction notificationAction;
        private int posX, posY;
        private static void AlertSound()
        {
            SoundPlayer myclicksound = new(Resources.notySound);
            if(Settings.Default.TogelNotySound == true)
            myclicksound.Play();
        }
        public void ShowAlert(string message, NotificationType eType)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string formName;

            for (int i = 1; i < 10; i++)
            {
                formName = "alert" + i.ToString();
                FormAlert frm = (FormAlert)Application.OpenForms[formName];
                int _height = Height * i - 0 * i;
                if (frm == null)
                {
                    Name = formName;
                    posX = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    posY = Screen.PrimaryScreen.WorkingArea.Height - _height;
                    Location = new Point(posX, posY);
                    break;
                }
            }
            posX = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;
            switch (eType)
            {
                case NotificationType.Success:
                    picIcon.Image = Resources.ok_100;
                    lblTitle.Text = "SUCCESS!";
                    break;
                case NotificationType.Error:
                    picIcon.Image = Resources.fehler_100;
                    lblTitle.Text = "ERROR!!";
                    break;
                case NotificationType.Info:
                    picIcon.Image = Resources.info_100;
                    lblTitle.Text = "INFO!";
                    break;
                case NotificationType.Warning:
                    picIcon.Image = Resources.achtung_100;
                    lblTitle.Text = "WARNING!";
                    break;
            }
            lblText.Text = message;
            Show();
            AlertSound();
             notificationAction = NotificationAction.start;
            timerCheck.Interval = 1;
            timerCheck.Start();
        }
        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            switch (notificationAction)
            {
                case NotificationAction.wait:
                    timerCheck.Interval = 5000;
                    notificationAction = NotificationAction.close;
                    break;
                case NotificationAction.start:
                    timerCheck.Interval = 1;
                    Opacity += 0.1;
                    if (posX < Location.X)
                    {
                        Left--;
                    }
                    else
                    {
                        if (Opacity == 1.0)
                        {
                            notificationAction = NotificationAction.wait;
                        }
                    }
                    break;
                case NotificationAction.close:
                    timerCheck.Interval = 1;
                    Opacity -= 0.1;
                    Left -= 3;
                    if (Opacity == 0.0)
                    {
                        Close();
                    }
                    break;
            }
        }
        private void BntClose_Click(object sender, EventArgs e)
        {
            timerCheck.Interval = 1;
            notificationAction = NotificationAction.close;
        }
        private void FormAlert_Load(object sender, EventArgs e)
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 5, 5));
        }
        public FormAlert()
        {
            InitializeComponent();  
        }
    }
}
