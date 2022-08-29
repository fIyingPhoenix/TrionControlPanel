using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace TrionControlPanel.UI
{
    public class CustomPanel : UserControl
    {
        //Fields
        int _edge = 20;
        Panel pnlBody;
        Color _bodyColor = Color.FromArgb(22, 26, 30);
        Color _borderColor = Color.FromArgb(0, 174, 219);
        int _brderSize = 0;

        //Properties
        [Category("1 CustomButton Advance")]
        public int Edge
        {
            get
            {
                return _edge;
            }
            set
            {
                _edge = value;
                Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public int BorderSize
        {
            get
            {
                return _brderSize;
            }
            set
            {
                _brderSize = value;
                this.Padding = new Padding(_brderSize, _brderSize, _brderSize, _brderSize);
                Invalidate();
            }
        }

        [Category("1 CustomButton Advance")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                BackColor = _borderColor;
                Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public Color BodyColor
        {
            get
            {
                return _bodyColor;
            }
            set
            {
                _bodyColor = value;
                pnlBody.BackColor = _bodyColor!;
                Invalidate();
            }
        }
        public CustomPanel()
        {
            InitializeComponent();
            pnlBody.BackColor = _bodyColor;
            BackColor = _borderColor;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(Edge > 0)
            {
                ExtendedDraw(e);
            }
            
        }
        private void ExtendedDraw(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.StartFigure();
            path.AddArc(GetLeftUpper(Edge), 180, 90);
            path.AddLine(Edge, 0, Width - Edge, 0);
            path.AddArc(GetRightUpper(Edge), 270, 90);
            path.AddLine(Width, Edge, Width, Height - Edge);
            path.AddArc(GetRightLower(Edge), 0, 90);
            path.AddLine(Width - Edge, Height, Edge, Height);
            path.AddArc(GetLeftLower(Edge), 90, 90);
            path.AddLine(0, Height - Edge, 0, Edge);
            path.CloseFigure();
            Region = new Region(path);
        }
        Rectangle GetLeftUpper(int e)
        {
            return new Rectangle(0, 0, e, e);
        }
        Rectangle GetRightUpper(int e)
        {
            return new Rectangle(Width - e, 0, e, e);
        }
        Rectangle GetRightLower(int e)
        {
            return new Rectangle(Width - e, Height - e, e, e);
        }
        Rectangle GetLeftLower(int e)
        {
            return new Rectangle(0, Height - e, e, e);
        }

        private void InitializeComponent()
        {
            this.pnlBody = new Panel();
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(489, 162);
            this.pnlBody.TabIndex = 0;
            this.pnlBody.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CustomPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Name = "CustomPanel";
            this.Size = new System.Drawing.Size(489, 162);
            this.ResumeLayout(false);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}