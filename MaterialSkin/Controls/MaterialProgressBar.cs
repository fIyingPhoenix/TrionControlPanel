namespace MaterialSkin.Controls
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MaterialProgressBar : ProgressBar, IMaterialControl
    {
        int _h = 5;
        Color _textColor = Color.White;
        string _pBarText = "%";
        bool _useProcetage = true;
        public MaterialProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        [Category("Behavior")]
        public bool UsaeProcentage
        {get { return _useProcetage; } set { _useProcetage = value; } }
        public string ProgressText
        {
            get { return _pBarText; }
            set { _pBarText = value; }
        }
        [Category("Behavior")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        [Category("Behavior")]
        public int PbarHeight
        {
            get { return _h; }
            set {
                if (value < 5)
                {
                    _h = 5;
                }
                else
                {
                    _h = value;
                }           
            }
        }

        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MouseState MouseState { get; set; }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, _h, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var doneProgress = (int)(Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(Enabled ?
                SkinManager.ColorScheme.PrimaryBrush :
                new SolidBrush(DrawHelper.BlendColor(SkinManager.ColorScheme.PrimaryColor, SkinManager.SwitchOffDisabledThumbColor, 197)),
                0, 0, doneProgress, Height);
            e.Graphics.FillRectangle(SkinManager.BackgroundFocusBrush, doneProgress, 0, Width - doneProgress, Height);
            // Draw the percentage text
            using (var font = new Font("Arial", 10, FontStyle.Bold)) // Use your desired font and size
            using (var brush = new SolidBrush(_textColor)) // Adjust color as needed
            {
                var text = "";
                if (_useProcetage) { text = $"{Value} {_pBarText}"; } else { text = $"{Value} / {Maximum} {_pBarText}"; }       
                var textSize = e.Graphics.MeasureString(text, font);
                var textX = (Width - textSize.Width) / 2;
                var textY = (Height - textSize.Height) / 2;

                e.Graphics.DrawString(text, font, brush, textX, textY);
            }
        }
    }
}
