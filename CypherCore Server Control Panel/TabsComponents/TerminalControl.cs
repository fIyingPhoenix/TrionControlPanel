using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CypherCoreServerLaucher.TabsComponents
{
    public partial class TerminalControl : UserControl
    {
        public TerminalControl()
        {
            InitializeComponent();
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            if(tglShowPassword.Checked  == false)
            {
                txtPassword.PasswordChar = false;
                txtRePassword.PasswordChar = false;
            }
            else if (tglShowPassword.Checked == true)
            {
                txtPassword.PasswordChar = true;
                txtRePassword.PasswordChar = true;
            }
        }
    }
}
