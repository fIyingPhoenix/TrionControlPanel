using System.ComponentModel;



namespace CypherCore_Server_Laucher.UI
{
    public class CustomProgressBar : ProgressBar
    {
        //Fields
        private string labelText = "%";
        private bool maximumValue = false;
        private int fontSize = 10;
        private Color textColor = Color.FromArgb(45, 51, 59);
        private Color barColor = Color.FromArgb(83, 155, 245);


        //Properties
        [Category("1 CustomButton Advance")]
        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                this.Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public bool MaximumValue
        {
            get { return maximumValue; }
            set
            {
                maximumValue = value;
                this.Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                this.Invalidate();
            }
        }
        [Category("1 CustomButton Advance")]
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }
        [Category("1 CustomButton Advance")]
        public Color BarColor
        {
            get { return barColor; }
            set { barColor = value; }
        }

        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        //fix flikering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //progress

            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            SolidBrush barcollor = new SolidBrush(TextColor);
            e.Graphics.FillRectangle(Brushes.RoyalBlue, 2, 2, rec.Width, rec.Height);
            Graphics g = e.Graphics;



            FontFamily font = new FontFamily("Segoe UI Semibold");

            //text
            using (Font f = new Font(font, fontSize))
            {
                string textMinimum = $"{this.Value} {labelText}";
                string textMaximum = $"{this.Value} {labelText} / {this.Maximum} {labelText} ";
                string text = "";

                if (maximumValue == false)
                    text = textMinimum;
                else
                    text = textMaximum;

                SizeF size = g.MeasureString(text, f);

                int textlocationWidth = (int)((this.Width / 2) - (size.Width / 2));
                int textlocationHeight = (int)((this.Height / 2) - (size.Height / 2));

                Point location = new Point(textlocationWidth, textlocationHeight);
                SolidBrush textolor = new SolidBrush(TextColor);
                g.DrawString(text, f, textolor, location);


            }
        }
    }
}