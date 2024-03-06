using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace TrionControlPanel.UI
{
    public class CustomProgressBar : ProgressBar
    {
        //Fields
        private bool seeLabel = true;  
        private string labelText = "%";
        private bool maximumValue = false;
        private int fontSize = 10;
        private Color textColor = Color.FromArgb(45, 51, 59);
        private Color barColor = Color.FromArgb(83, 155, 245);
        private FontFamily textFont = new("Segoe UI Semibold");
        //Fields
        private int borderSize = 0;

        //Properties
        [Category("1 CustomButton Advance")]
    
        public FontFamily TextFont
        {
            get { return textFont; }    
            set 
            {
                textFont = value;
            }   
        }

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
        public bool ShowStatus
        {
            get { return seeLabel; }
            set
            {
                seeLabel = value;
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
            //color bar.
            Brush barColor = new SolidBrush(BarColor);
            //progress
            Rectangle rec = e.ClipRectangle;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            //if (ProgressBarRenderer.IsSupported)
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height;
            e.Graphics.FillRectangle(barColor, 0,0, rec.Width, rec.Height);
            Graphics g = e.Graphics;
            //text
            using (Font f = new(textFont, fontSize))
            {
                string textMinimum = $"{Value} {labelText}";
                string textMaximum = $"{Value} {labelText} / {Maximum} {labelText} ";
                string text = "";
                if(seeLabel == true)
                {
                    if (maximumValue == false)
                    {
                        text = textMinimum;
                    }
                    else
                    {
                        text = textMaximum;
                    }
                }
                using (Pen penBorder = new(Color.Black, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                }

                SizeF size = g.MeasureString(text, f);
                int textlocationWidth = (int)((Width / 2) - (size.Width / 2));
                int textlocationHeight = (int)((Height / 2) - (size.Height / 2));
                Point location = new(textlocationWidth, textlocationHeight);
                SolidBrush textolor = new(TextColor);
                g.DrawString(text, f, textolor, location);
            }
        }
    }
}