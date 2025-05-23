﻿using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class MouseWheelRedirector : IMessageFilter
{
    private static MouseWheelRedirector instance = null;
    private static bool _active = false;

    public static bool Active
    {
        set
        {
            if (_active != value)
            {
                _active = value;
                if (_active)
                {
                    if (instance == null)
                        instance = new MouseWheelRedirector();
                    Application.AddMessageFilter(instance);
                }
                else if (instance != null)
                    Application.RemoveMessageFilter(instance);
            }
        }
        get
        {
            return _active;
        }
    }

    public static void Attach(Control control)
    {
        if (!_active)
            Active = true;
        control.MouseEnter += instance.ControlMouseEnter;
        control.MouseLeave += instance.ControlMouseLeaveOrDisposed;
        control.Disposed += instance.ControlMouseLeaveOrDisposed;
    }

    public static void Detach(Control control)
    {
        if (instance == null)
            return;
        control.MouseEnter -= instance.ControlMouseEnter;
        control.MouseLeave -= instance.ControlMouseLeaveOrDisposed;
        control.Disposed -= instance.ControlMouseLeaveOrDisposed;
        if (instance.currentControl == control)
            instance.currentControl = null;
    }

    public MouseWheelRedirector()
    {
    }

    private Control currentControl;

    private void ControlMouseEnter(object sender, EventArgs e)
    {
        var control = (Control)sender;
        if (!control.Focused)
            currentControl = control;
        else
            currentControl = null;
    }

    private void ControlMouseLeaveOrDisposed(object sender, EventArgs e)
    {
        if (currentControl == sender)
            currentControl = null;
    }

    private const int WM_MOUSEWHEEL = 0x20A;
    public bool PreFilterMessage(ref Message m)
    {
        if (currentControl != null && m.Msg == WM_MOUSEWHEEL)
        {
            SendMessage(currentControl.Handle, m.Msg, m.WParam, m.LParam);
            return true;
        }
        else
            return false;
    }

    [DllImport("user32.dll", SetLastError = false)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
}
