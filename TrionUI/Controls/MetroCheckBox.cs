﻿/**
 * MetroFramework - Modern UI for WinForms
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Sven Walter, http://github.com/viperneo
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.ComponentModel;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Controls
{
    [ToolboxBitmap(typeof(CheckBox))]
    public class MetroCheckBox : CheckBox, IMetroControl
    {
        #region Interface
        private MetroColorStyle metroStyle = MetroColorStyle.Blue;
        [Category("Metro Appearance")]
        public MetroColorStyle Style
        {
            get
            {
                if (StyleManager != null)
                    return StyleManager.Style;

                return metroStyle;
            }
            set { metroStyle = value; }
        }
        private MetroThemeStyle metroTheme = MetroThemeStyle.Light;
        [Category("Metro Appearance")]
        public MetroThemeStyle Theme
        {
            get
            {
                if (StyleManager != null)
                    return StyleManager.Theme;
                return metroTheme;
            }
            set { metroTheme = value; }
        }
        private MetroStyleManager? metroStyleManager = null;
        [Browsable(false)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager!; }
            set { metroStyleManager = value; }
        }
        #endregion
        #region Fields
        private bool useStyleColors = false;
        [Category("Metro Appearance")]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        private MetroLinkSize metroLinkSize = MetroLinkSize.Small;
        [Category("Metro Appearance")]
        public MetroLinkSize FontSize
        {
            get { return metroLinkSize; }
            set { metroLinkSize = value; }
        }
        private MetroLinkWeight metroLinkWeight = MetroLinkWeight.Regular;
        [Category("Metro Appearance")]
        public MetroLinkWeight FontWeight
        {
            get { return metroLinkWeight; }
            set { metroLinkWeight = value; }
        }
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value!;
            }
        }
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        private bool useCustomBackground = false;
        [Category("Metro Appearance")]
        public bool CustomBackground
        {
            get { return useCustomBackground; }
            set { useCustomBackground = value; }
        }
        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor
        public MetroCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }
        #endregion

        #region Paint Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            Color backColor, borderColor, foreColor;

            if (useCustomBackground)
            {
                backColor = BackColor;
            }   
            else
            {
                backColor = MetroPaint.BackColor.Form.GetButtonColor(Theme,"Normal");
            }
            if (isHovered && !isPressed && Enabled)
            {
                foreColor = MetroPaint.ForeColor.CheckBox.Hover(Theme);
                borderColor = MetroPaint.BorderColor.CheckBox.GetCheckBoxColor(Theme, "Hover"); 
            }
            else if (isHovered && isPressed && Enabled)
            {
                foreColor = MetroPaint.ForeColor.CheckBox.Press(Theme);
                borderColor = MetroPaint.BorderColor.CheckBox.GetCheckBoxColor(Theme, "Press");
            }
            else if (!Enabled)
            {
                foreColor = MetroPaint.ForeColor.CheckBox.Disabled(Theme);
                borderColor = MetroPaint.BorderColor.CheckBox.GetCheckBoxColor(Theme, "Disabled"); 
            }
            else
            {
                foreColor = !useStyleColors ? MetroPaint.ForeColor.CheckBox.Normal(Theme) : MetroPaint.GetStyleColor(Style);
                borderColor = MetroPaint.BorderColor.CheckBox.GetCheckBoxColor(Theme, "Normal");
            }
            e.Graphics.Clear(backColor);
            using (Pen p = new(borderColor))
            {
                Rectangle boxRect = new(0, Height / 2 - 6, 12, 12);
                e.Graphics.DrawRectangle(p, boxRect);
            }
            if (Checked)
            {
                Color fillColor = CheckState == CheckState.Indeterminate ? borderColor : MetroPaint.GetStyleColor(Style);
                using (SolidBrush b = new(fillColor))
                {
                    Rectangle boxRect = new(2, Height / 2 - 4, 9, 9);
                    e.Graphics.FillRectangle(b, boxRect);
                }
            }

            Rectangle textRect = new(16, 0, Width - 16, Height);
            TextRenderer.DrawText(e.Graphics, Text, MetroFonts.Link(metroLinkSize, metroLinkWeight), textRect, foreColor, backColor, MetroPaint.GetTextFormatFlags(TextAlign));

            if (false && isFocused)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
        }
        #endregion
        #region Focus Methods
        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            Invalidate();
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();
            base.OnLostFocus(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();
            base.OnLeave(e);
        }
        #endregion
        #region Keyboard Methods
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                Invalidate();
            }
            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            isHovered = false;
            isPressed = false;
            Invalidate();
            base.OnKeyUp(e);
        }
        #endregion
        #region Mouse Methods
        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
        #endregion

        #region Overridden Methods
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            Invalidate();
        }
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);
            using (var g = CreateGraphics())
            {
                proposedSize = new Size(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, Text, MetroFonts.Link(metroLinkSize, metroLinkWeight), proposedSize, MetroPaint.GetTextFormatFlags(TextAlign));
                preferredSize.Width += 16;
            }
            return preferredSize;
        }
        #endregion
    }
}
