using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrionControlPanelDesktop.Controls
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent();
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {

        }

        private void HomeControl_Resize(object sender, EventArgs e)
        {
            panel2.Left = this.Right - 420;
        }
    }
}
