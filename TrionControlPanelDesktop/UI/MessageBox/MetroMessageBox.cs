using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrionControlPanelDesktop.UI.MessageBox
{
    public partial class MetroMessageBox : MetroForm
    {
        public MetroMessageBox(string Message, string Title, Settings.MessageBoxButtons MessageBoxButtons)
        {
            InitializeComponent();

        }

    }
    public class Settings
    {
        public enum MessageBoxButtons
        {
            Ok,
            OkCancel,
            AbortRetryIgnore,
            YesNoCancel,
            YesNO,
            CancelContinue
        }
    }

}
