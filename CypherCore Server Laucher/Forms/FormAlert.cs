using CypherCoreServerLaucher.Properties;
using CypherCore_Server_Laucher.Classes;
using System.Media;

namespace CypherCore_Server_Laucher.Forms
{

    public partial class FormAlert : Form
    {
        private NotificationAction notificationAction;
        private int posX, posY;
        SoundPlayer myclicksound = new SoundPlayer(Resources.notySound);
        public void ShowAlert(string message, NotificationType eType)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string formName;

            for (int i = 1; i < 10; i++)
            {
                formName = "alert" + i.ToString();
                FormAlert frm = (FormAlert)Application.OpenForms[formName];

                if (frm == null)
                {
                    Name = formName;
                    posX = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    posY = Screen.PrimaryScreen.WorkingArea.Height - Height * i - 5 * i;
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
            myclicksound.Play();
            notificationAction = NotificationAction.start;
            timerCheck.Interval = 1;
            timerCheck.Start();
        }

        private void timerCheck_Tick(object sender, EventArgs e)
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

        private void bntClose_Click(object sender, EventArgs e)
        {
            timerCheck.Interval = 1;
            notificationAction = NotificationAction.close;
        }

        public FormAlert()
        {
          InitializeComponent();  

        }
    }
}
