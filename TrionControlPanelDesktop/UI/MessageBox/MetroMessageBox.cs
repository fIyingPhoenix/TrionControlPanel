using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Interfaces;

namespace MetroFramework
{
    /// <summary>
    /// Metro-styled message notification.
    /// </summary>
    public static class MetroMessageBox
    {
        public static DialogResult Show(IWin32Window owner, String message, String title, bool Sound, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, int height = 211)
        {
            if (owner == null) return DialogResult.None;

            Form ownerForm = (owner as Form) ?? ((UserControl)owner).ParentForm!;

            var control = new MetroMessageBoxControl();
            control.BackColor = ownerForm.BackColor;
            control.Properties.Buttons = buttons;
            control.Properties.DefaultButton = defaultButton;
            control.Properties.Icon = icon;
            control.Properties.Message = message;
            control.Properties.Title = title;
            control.Padding = new Padding(0, 0, 0, 0);
            control.ControlBox = false;
            control.ShowInTaskbar = false;
            control.Size = new Size(ownerForm.Size.Width, height);
            control.Location = new Point(ownerForm.Location.X, ownerForm.Location.Y + (ownerForm.Height - control.Height) / 2);
            control.ArrangeApperance();
            if (Sound)
            {
                var soundMap = new Dictionary<MessageBoxIcon, SystemSound>
                {
                { MessageBoxIcon.Error, SystemSounds.Hand },
                { MessageBoxIcon.Exclamation, SystemSounds.Exclamation },
                { MessageBoxIcon.Question, SystemSounds.Beep },
                { MessageBoxIcon.None, SystemSounds.Hand }
                };
                soundMap[icon]?.Play();
            }

            control.ShowDialog(ownerForm);
            control.Dispose();
            return control.Result;
        }

    }
}
