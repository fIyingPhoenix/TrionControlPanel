﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using MetroFramework.Components;
using MetroFramework.Interfaces;
using MetroFramework.Drawing;
using MetroFramework.Native;

namespace MetroFramework.Controls
{
    [ToolboxBitmap(typeof(Panel))]
    public class MetroPanel : Panel, IMetroControl
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

        private MetroStyleManager metroStyleManager;
        [Browsable(false)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        #endregion

        #region Fields

        private MetroScrollBar verticalScrollbar;
        private MetroScrollBar horizontalScrollbar;

        private bool showHorizontalScrollbar = false;
        [Category("Metro Appearance")]
        public bool HorizontalScrollbar
        {
            get { return showHorizontalScrollbar; }
            set { showHorizontalScrollbar = value; }
        }

        private bool border = false;
        [Category("Metro Appearance")]
        public bool Border
        {
            get { return border; }
            set { border = value; }
        }

        private Color borderColor = Color.Black;
        [Category("Metro Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private int borderSize = 0;
        [Category("Metro Appearance")]
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; }
        }

        [Category("Metro Appearance")]
        public int HorizontalScrollbarSize
        {
            get { return horizontalScrollbar.ScrollbarSize; }
            set { horizontalScrollbar.ScrollbarSize = value; }
        }

        [Category("Metro Appearance")]
        public bool HorizontalScrollbarBarColor
        {
            get { return horizontalScrollbar.UseBarColor; }
            set { horizontalScrollbar.UseBarColor = value; }
        }

        [Category("Metro Appearance")]
        public bool HorizontalScrollbarHighlightOnWheel
        {
            get { return horizontalScrollbar.HighlightOnWheel; }
            set { horizontalScrollbar.HighlightOnWheel = value; }
        }

        private bool showVerticalScrollbar = false;
        [Category("Metro Appearance")]
        public bool VerticalScrollbar
        {
            get { return showVerticalScrollbar; }
            set { showVerticalScrollbar = value; }
        }

        [Category("Metro Appearance")]
        public int VerticalScrollbarSize
        {
            get { return verticalScrollbar.ScrollbarSize; }
            set { verticalScrollbar.ScrollbarSize = value; }
        }

        [Category("Metro Appearance")]
        public bool VerticalScrollbarBarColor
        {
            get { return verticalScrollbar.UseBarColor; }
            set { verticalScrollbar.UseBarColor = value; }
        }

        [Category("Metro Appearance")]
        public bool VerticalScrollbarHighlightOnWheel
        {
            get { return verticalScrollbar.HighlightOnWheel; }
            set { verticalScrollbar.HighlightOnWheel = value; }
        }

        [Category("Metro Appearance")]
        public new bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                if (value)
                {
                    showHorizontalScrollbar = true;
                    showVerticalScrollbar = true;
                }

                base.AutoScroll = value;
            }
        }

        private bool useCustomBackground = false;
        [Category("Metro Appearance")]
        public bool CustomBackground
        {
            get { return useCustomBackground; }
            set { useCustomBackground = value; }
        }

        #endregion

        #region Constructor

        public MetroPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);

            verticalScrollbar = new MetroScrollBar(MetroScrollOrientation.Vertical);
            horizontalScrollbar = new MetroScrollBar(MetroScrollOrientation.Horizontal);

            Controls.Add(verticalScrollbar);
            Controls.Add(horizontalScrollbar);

            verticalScrollbar.UseBarColor = true;
            horizontalScrollbar.UseBarColor = true;

            verticalScrollbar.Scroll += VerticalScrollbarScroll;
            horizontalScrollbar.Scroll += HorizontalScrollbarScroll;

            Resize += MetroPanel_Resize;
        }

        #endregion

        #region Event Handlers

        private void MetroPanel_Resize(object sender, EventArgs e)
        {
            UpdateScrollBarPositions();
        }

        private void HorizontalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new Point(e.NewValue, verticalScrollbar.Value);
            UpdateScrollBarPositions();
        }

        private void VerticalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new Point(horizontalScrollbar.Value, e.NewValue);
            UpdateScrollBarPositions();
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color backColor = MetroPaint.BackColor.Form.GetButtonColor(Theme, "Normal");

            if (useCustomBackground)
                backColor = BackColor;

            e.Graphics.Clear(backColor);

            if (DesignMode)
            {
                horizontalScrollbar.Visible = false;
                verticalScrollbar.Visible = false;
                return;
            }

            UpdateScrollBarPositions();

            if (HorizontalScrollbar)
            {
                horizontalScrollbar.Visible = HorizontalScroll.Visible;
            }
            if (HorizontalScroll.Visible)
            {
                horizontalScrollbar.Minimum = HorizontalScroll.Minimum;
                horizontalScrollbar.Maximum = HorizontalScroll.Maximum;
                horizontalScrollbar.SmallChange = HorizontalScroll.SmallChange;
                horizontalScrollbar.LargeChange = HorizontalScroll.LargeChange;
            }

            if (VerticalScrollbar)
            {
                verticalScrollbar.Visible = VerticalScroll.Visible;
            }
            if (VerticalScroll.Visible)
            {
                verticalScrollbar.Minimum = VerticalScroll.Minimum;
                verticalScrollbar.Maximum = VerticalScroll.Maximum;
                verticalScrollbar.SmallChange = VerticalScroll.SmallChange;
                verticalScrollbar.LargeChange = VerticalScroll.LargeChange;
            }
            if (Border && BorderSize > 0)
            {
                Pen borderPen = new(BorderColor);
                using (SolidBrush brush = new(BackColor))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }

                e.Graphics.DrawRectangle(borderPen, 0, 0, ClientSize.Width - BorderSize, ClientSize.Height - BorderSize);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            verticalScrollbar.Value = VerticalScroll.Value;
            horizontalScrollbar.Value = HorizontalScroll.Value;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!DesignMode)
            {
                WinApi.ShowScrollBar(Handle, (int)WinApi.ScrollBar.SB_BOTH, 0);
            }
        }

        #endregion

        #region Management Methods

        private void UpdateScrollBarPositions()
        {
            if (DesignMode)
            {
                return;
            }

            if (!AutoScroll)
            {
                verticalScrollbar.Visible = false;
                horizontalScrollbar.Visible = false;
                return;
            }
            if (VerticalScrollbar)
            {
                if (VerticalScroll.Visible)
                {
                    verticalScrollbar.Location = new Point(ClientRectangle.Width - verticalScrollbar.Width, ClientRectangle.Y);
                    verticalScrollbar.Height = ClientRectangle.Height - (HorizontalScroll.Visible ? horizontalScrollbar.Height : 0);
                }
            }
            else
            {
                verticalScrollbar.Visible = false;
            }

            if (HorizontalScrollbar)
            {
                if (HorizontalScroll.Visible)
                {
                    horizontalScrollbar.Location = new Point(ClientRectangle.X, ClientRectangle.Height - horizontalScrollbar.Height);
                    horizontalScrollbar.Width = ClientRectangle.Width - (VerticalScroll.Visible ? verticalScrollbar.Width : 0);
                }
            }
            else
            {
                horizontalScrollbar.Visible = false;
            }
        }

        #endregion
    }
}
