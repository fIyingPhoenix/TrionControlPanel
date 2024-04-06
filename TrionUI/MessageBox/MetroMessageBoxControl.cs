using MetroFramework.Controls;
using System.Diagnostics;
using MetroFramework.Localization;

namespace MetroFramework
{
    public partial class MetroMessageBoxControl : Form
    {
        MetroLocalize metroLocalize = null!;

        public MetroMessageBoxControl()
        {
            InitializeComponent();

            _properties = new MetroMessageBoxProperties(this);

            metroButton1.Click += new EventHandler(Button1_Click);
            metroButton2.Click += new EventHandler(Button2_Click);
            metroButton3.Click += new EventHandler(Button3_Click);

            metroLocalize = new MetroLocalize(this);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _defaultColor = Color.FromArgb(45, 51, 59);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _errorColor = Color.FromArgb(45, 51, 59);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _warningColor = Color.FromArgb(45, 51, 59);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _success = Color.FromArgb(45, 51, 59);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color _question = Color.FromArgb(45, 51, 59);

        /// <summary>
        /// Gets the top body section of the control. 
        /// </summary>
        public Panel Body
        {
            get { return panelbody; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MetroMessageBoxProperties _properties = null!;

        /// <summary>
        /// Gets the message box display properties.
        /// </summary>
        public MetroMessageBoxProperties Properties
        { get { return _properties; } }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DialogResult _result;

        /// <summary>
        /// Gets the dialog result that the user have chosen.
        /// </summary>
        public DialogResult Result
        {
            get { return _result; }
        }

        /// <summary>
        /// Arranges the apperance of the message box overlay.
        /// </summary>
        public void ArrangeApperance()
        {
            titleLabel.Text = _properties.Title;
            messageLabel.Text = _properties.Message;

            switch (_properties.Icon)
            {
                case MessageBoxIcon.Exclamation:
                    panelbody.BackColor = _warningColor;
                    break;
                case MessageBoxIcon.Error:
                    panelbody.BackColor = _errorColor;
                    break;
                default: break;
            }

            switch (_properties.Buttons)
            {
                case MessageBoxButtons.OK:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Ok";
                    metroButton1.Location = metroButton3.Location;
                    metroButton1.Tag = DialogResult.OK;

                    EnableButton(metroButton2, false);
                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.OKCancel:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Ok";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.OK;

                    EnableButton(metroButton2);

                    metroButton2.Text = "Cancel";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.Cancel;

                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.RetryCancel:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Retry";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.Retry;

                    EnableButton(metroButton2);

                    metroButton2.Text = "Cancel";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.Cancel;

                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.YesNo:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Yes";
                    metroButton1.Location = metroButton2.Location;
                    metroButton1.Tag = DialogResult.Yes;

                    EnableButton(metroButton2);

                    metroButton2.Text = "No";
                    metroButton2.Location = metroButton3.Location;
                    metroButton2.Tag = DialogResult.No;

                    EnableButton(metroButton3, false);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Yes";
                    metroButton1.Tag = DialogResult.Yes;

                    EnableButton(metroButton2);

                    metroButton2.Text = "No";
                    metroButton2.Tag = DialogResult.No;

                    EnableButton(metroButton3);

                    metroButton3.Text = "Cancel";
                    metroButton3.Tag = DialogResult.Cancel;

                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    EnableButton(metroButton1);

                    metroButton1.Text = "Abort";
                    metroButton1.Tag = DialogResult.Abort;

                    EnableButton(metroButton2);

                    metroButton2.Text = "Retry";
                    metroButton2.Tag = DialogResult.Retry;

                    EnableButton(metroButton3);

                    metroButton3.Text = "Ignore";
                    metroButton3.Tag = DialogResult.Ignore;

                    break;
                default: break;
            }

            switch (_properties.Icon)
            {
                case MessageBoxIcon.Error:
                    panelbody.BackColor = _errorColor; break;
                case MessageBoxIcon.Warning:
                    panelbody.BackColor = _warningColor; break;
                case MessageBoxIcon.Information:
                    panelbody.BackColor = _defaultColor;
                    break;
                case MessageBoxIcon.Question:
                    panelbody.BackColor = _question; break;
                default:
                    panelbody.BackColor = Color.DarkGray; break;
            }
        }
        private void EnableButton(MetroButton button)
        { EnableButton(button, true); }

        private void EnableButton(MetroButton button, bool enabled)
        {
            button.Enabled = enabled; button.Visible = enabled;
        }
        /// <summary>
        /// Sets the default focused button.
        /// </summary>
        public void SetDefaultButton()
        {
            switch (_properties.DefaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    if (metroButton1 != null)
                    {
                        if (metroButton1.Enabled) metroButton1.Focus();
                    }
                    break;
                case MessageBoxDefaultButton.Button2:
                    if (metroButton2 != null)
                    {
                        if (metroButton2.Enabled) metroButton2.Focus();
                    }
                    break;
                case MessageBoxDefaultButton.Button3:
                    if (metroButton3 != null)
                    {
                        if (metroButton3.Enabled) metroButton3.Focus();
                    }
                    break;
                default: break;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Enabled) // Check if sender is Button and enabled
            {
                _result = (DialogResult)metroButton1.Tag!;
            }
            Hide();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Enabled) // Check if sender is Button and enabled
            {
                _result = (DialogResult)metroButton2.Tag!;
            }
            Hide();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Enabled) // Check if sender is Button and enabled
            {
                _result = (DialogResult)metroButton3.Tag!;
            }
            Hide();
        }
        private void MetroMessageBoxControl_Load(object sender, EventArgs e)
        {

        }
    }
}
