using System.ComponentModel;
using System.Media;
using MetroFramework.Forms;
using TrionControlPanel.Properties;
using TrionControlPanel.Settings;

namespace TrionControlPanel.Alerts
{
    public partial class FormAlert : MetroForm
    {
        Settings.Settings Settings = new();
        string alertMessage;
         NotificationType alertType = NotificationType.Empty;
         
        [Category("Trion Properties")]
        public string AlerMessage
        {
            get { return alertMessage; }
            set
            {
                alertMessage = value;
            }
        }
        [Category("Trion Properties")]
        public NotificationType AlertType
        {
            get { return alertType; }
            set
            {
                alertType = value;
            }
        }

        private NotificationAction notificationAction;
        private int posX, posY;
        
        private void AlertSound()
        {
            if (Settings._Data.NotificationSound == true)
            {
                SoundPlayer myclicksound = new(Resources.notySound);
                myclicksound.Play();
            }
        }

        public void Alert(string message, NotificationType eType)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string formName;
            int MaxAlertOnScreen = Screen.PrimaryScreen.WorkingArea.Height / Height + 1;
            for (int i = 1; i < MaxAlertOnScreen; i++)
            {
                formName = "Trion Alert" + i.ToString();
                FormAlert formAlert = (FormAlert)Application.OpenForms[formName]; ;
                int _height = (Height * i) + (2 * i);
                if (formAlert == null)
                {
                    Name = formName;
                    posX = Screen.PrimaryScreen.WorkingArea.Width - Width;
                    posY = Screen.PrimaryScreen.WorkingArea.Height - _height;
                    Location = new Point(posX, posY);
                    break;
                }
            }
            posX = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;
            switch (eType)
            {
                case NotificationType.Success:
                    pboxIcon.Image = Resources.emojiSucces;
                    lblTitle.Text = "SUCCESS!";
                    break;
                case NotificationType.Error:
                    pboxIcon.Image = Resources.emojiError;
                    lblTitle.Text = "ERROR!!";
                    break;
                case NotificationType.Info:
                    pboxIcon.Image = Resources.EmojiInfo;
                    lblTitle.Text = "INFO!";
                    break;
                case NotificationType.Warning:
                    pboxIcon.Image = Resources.EmojiInfo;
                    this.Text = "WARNING!";
                    break;
            }
            lblMesage.Text = message;
            AlertSound();
            notificationAction = NotificationAction.start;
            timerCheck.Interval = 1;
            timerCheck.Start();
            Show();
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
                    Left += 3;
                    if (Opacity == 0.0)
                    {
                        Close();
                    }
                    break;
            }
        }
        public static void ShowAlert(string message, NotificationType eType)
        {
            FormAlert TrionAlert = new(); //dont change this. its fix the Cannot access a disposed object and scall the notification up.
            TrionAlert.Alert(message, eType);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.Image = Resources.rightArrowBluex50;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.Image = Resources.rightArrowWhitex50;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            timerCheck.Stop();
            notificationAction = NotificationAction.close;
            timerCheck.Interval = 1;
            timerCheck.Start();
        }

        public FormAlert()
        {
            InitializeComponent();
            if (alertMessage != null)
            {
                ShowAlert(alertMessage, alertType);

            }
        }
    }
}