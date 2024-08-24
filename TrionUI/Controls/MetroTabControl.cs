using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Design;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using MetroFramework.Native;

namespace MetroFramework.Controls
{
    internal class MetroTabPageCollectionEditor : CollectionEditor
    {
        protected override CollectionForm CreateCollectionForm()
        {
            var baseForm = base.CreateCollectionForm();
            baseForm.Text = "MetroTabPage Collection Editor";
            return baseForm;
        }

        public MetroTabPageCollectionEditor(Type type) : base(type) { }

        protected override Type CreateCollectionItemType()
        {
            return typeof(MetroTabPage);
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(MetroTabPage) };
        }
    }

    [ToolboxItem(false)]
    [Editor(typeof(MetroTabPageCollectionEditor), typeof(UITypeEditor))]
    public class MetroTabPageCollection : TabControl.TabPageCollection
    {
        public MetroTabPageCollection(MetroTabControl owner) : base(owner) { }
    }

    [Designer(typeof(MetroTabControlDesigner))]
    [ToolboxBitmap(typeof(TabControl))]
    public class MetroTabControl : TabControl, IMetroControl
    {
        private MetroColorStyle metroStyle = MetroColorStyle.Blue;
        [Category("Metro Appearance")]
        public MetroColorStyle Style
        {
            get { return StyleManager?.Style ?? metroStyle; }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Light;
        [Category("Metro Appearance")]
        public MetroThemeStyle Theme
        {
            get { return StyleManager?.Theme ?? metroTheme; }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private const int TabBottomBorderHeight = 3;
        private bool useStyleColors = false;
        [Category("Metro Appearance")]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        private MetroTabControlSize metroLabelSize = MetroTabControlSize.Medium;
        [Category("Metro Appearance")]
        public MetroTabControlSize FontSize
        {
            get { return metroLabelSize; }
            set { metroLabelSize = value; }
        }

        private MetroTabControlWeight metroLabelWeight = MetroTabControlWeight.Light;
        [Category("Metro Appearance")]
        public MetroTabControlWeight FontWeight
        {
            get { return metroLabelWeight; }
            set { metroLabelWeight = value; }
        }

        private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
        [Category("Metro Appearance")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; }
        }

        [Editor(typeof(MetroTabPageCollectionEditor), typeof(UITypeEditor))]
        public new TabPageCollection TabPages
        {
            get { return base.TabPages; }
        }

        private bool isMirrored;
        [Category("Metro Appearance")]
        [DefaultValue(false)]
        public new bool IsMirrored
        {
            get { return isMirrored; }
            set
            {
                if (isMirrored != value)
                {
                    isMirrored = value;
                    UpdateStyles();
                }
            }
        }

        private bool useCustomBackground = false;
        [Category("Metro Appearance")]
        public bool CustomBackground
        {
            get { return useCustomBackground; }
            set { useCustomBackground = value; }
        }

        public MetroTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);

            Padding = new Point(6, 8);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float dpiScale = e.Graphics.DpiX / 96f;
            Color backColor = useCustomBackground ? BackColor : MetroPaint.BackColor.Form.GetButtonColor(Theme, "Normal");

            e.Graphics.Clear(backColor);

            for (var index = 0; index < TabPages.Count; index++)
            {
                if (index != SelectedIndex)
                {
                    DrawTab(index, e.Graphics, dpiScale);
                }
            }

            if (SelectedIndex <= -1) return;

            DrawTabBottomBorder(SelectedIndex, e.Graphics, dpiScale);
            DrawTab(SelectedIndex, e.Graphics, dpiScale);
            DrawTabSelected(SelectedIndex, e.Graphics, dpiScale);
        }

        private void DrawTabBottomBorder(int index, Graphics graphics, float dpiScale)
        {
            using (var bgBrush = new SolidBrush(MetroPaint.BorderColor.TabControl.GetButtonColor(Theme, "Normal")))
            {
                graphics.FillRectangle(bgBrush,
                    -2 + (int)(GetTabRect(0).X * dpiScale) + (int)(DisplayRectangle.X * dpiScale),
                    (int)(GetTabRect(index).Bottom * dpiScale) + 2 - (int)(TabBottomBorderHeight * dpiScale),
                    (int)(Width * dpiScale) - (int)((Width - DisplayRectangle.Width + DisplayRectangle.X) * dpiScale) + 4,
                    (int)(TabBottomBorderHeight * dpiScale));
            }
        }

        private void DrawTabSelected(int index, Graphics graphics, float dpiScale)
        {
            using (var selectionBrush = new SolidBrush(MetroPaint.GetStyleColor(Style)))
            {
                var selectedTabRect = GetTabRect(index);
                graphics.FillRectangle(selectionBrush,
                    new Rectangle
                    {
                        X = -2 + (int)(selectedTabRect.X * dpiScale) + (int)(DisplayRectangle.X * dpiScale),
                        Y = (int)(selectedTabRect.Bottom * dpiScale) + 2 - (int)(TabBottomBorderHeight * dpiScale),
                        Width = (int)(selectedTabRect.Width * dpiScale),
                        Height = (int)(TabBottomBorderHeight * dpiScale)
                    });
            }
        }

        private Size MeasureText(string text, float dpiScale)
        {
            Size preferredSize;
            using (var g = CreateGraphics())
            {
                var proposedSize = new Size(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, text, MetroFonts.TabControl(metroLabelSize, metroLabelWeight),
                                                         proposedSize, MetroPaint.GetTextFormatFlags(TextAlign) |
                                                         TextFormatFlags.NoPadding);
                preferredSize.Width = (int)(preferredSize.Width * dpiScale);
                preferredSize.Height = (int)(preferredSize.Height * dpiScale);
            }
            return preferredSize;
        }

        private void DrawTab(int index, Graphics graphics, float dpiScale)
        {
            Color foreColor = !Enabled ? MetroPaint.ForeColor.Label.Disabled(Theme) :
                              !useStyleColors ? MetroPaint.ForeColor.TabControl.Normal(Theme) :
                              MetroPaint.GetStyleColor(Style);

            var backColor = Parent != null ? Parent.BackColor : MetroPaint.BackColor.Form.GetButtonColor(Theme, "Normal");
            var tabPage = TabPages[index];
            var tabRect = GetTabRect(index);

            if (index == 0)
            {
                tabRect.X = DisplayRectangle.X;
            }

            tabRect.Width += 20;

            TextRenderer.DrawText(graphics, tabPage.Text, MetroFonts.TabControl(metroLabelSize, metroLabelWeight),
                                  new Rectangle((int)(tabRect.X * dpiScale), (int)(tabRect.Y * dpiScale),
                                                (int)(tabRect.Width * dpiScale), (int)(tabRect.Height * dpiScale)),
                                  foreColor, backColor, MetroPaint.GetTextFormatFlags(TextAlign));
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!DesignMode)
            {
                WinApi.ShowScrollBar(Handle, (int)WinApi.ScrollBar.SB_BOTH, 0);
            }
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int WS_EX_LAYOUTRTL = 0x400000;
                const int WS_EX_NOINHERITLAYOUT = 0x100000;
                var cp = base.CreateParams;
                if (isMirrored)
                {
                    cp.ExStyle = cp.ExStyle | WS_EX_LAYOUTRTL | WS_EX_NOINHERITLAYOUT;
                }
                return cp;
            }
        }

        private new Rectangle GetTabRect(int index)
        {
            if (index < 0) return new Rectangle();

            Rectangle baseRect = base.GetTabRect(index);
            return baseRect;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (SelectedIndex != -1)
            {
                if (!TabPages[SelectedIndex].Focused)
                {
                    TabPages[SelectedIndex].Select();
                    TabPages[SelectedIndex].Focus();
                }
            }

            base.OnMouseWheel(e);
        }
    }
}
