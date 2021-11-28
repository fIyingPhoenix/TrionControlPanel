using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CypherCore_Server_Laucher.Classes
{
    public class RoundPanel : Panel
    {
        Pen pen;
        float penWidth = 2.0f;
        int _edge = 20;
        Color _borderColor = Color.White;
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

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                pen = new Pen(_borderColor, penWidth);
                Invalidate();
            }
        }

        public RoundPanel()
        {
            pen = new Pen(_borderColor, penWidth);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ExtendedDraw(e);
            //DrawBorder(e.Graphics);
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

        void DrawSingleBorder(Graphics graphics)
        {
            graphics.DrawArc(pen, new Rectangle(0, 0, Edge, Edge), 180, 90);
            graphics.DrawArc(pen, new Rectangle(Width - Edge - 1, -1, Edge, Edge), 270, 90);
            graphics.DrawArc(pen, new Rectangle(Width - Edge - 1, Height - Edge - 1, Edge, Edge), 0, 90);
            graphics.DrawArc(pen, new Rectangle(0, Height - Edge - 1, Edge, Edge), 90, 90);

            graphics.DrawRectangle(pen, 0.0F, 0.0F, Width - 1, Height - 1);
        }

        void DrawBorder(Graphics graphics)
        {
            DrawSingleBorder(graphics);
        }
    }

    public class CustomButton : Button
        {
            //Fields
            private int borderSize = 0;
            private int borderRadius = 0;
            private Color borderColor = Color.PaleVioletRed;

            //Properties
            [Category("CustomButton Advance")]
            public int BorderSize
            {
                get { return borderSize; }
                set
                {
                    borderSize = value;
                    this.Invalidate();
                }
            }

            [Category("CustomButton Advance")]
            public int BorderRadius
            {
                get { return borderRadius; }
                set
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }

            [Category("CustomButton Advance")]
            public Color BorderColor
            {
                get { return borderColor; }
                set
                {
                    borderColor = value;
                    this.Invalidate();
                }
            }

            [Category("CustomButton Advance")]
            public Color BackgroundColor
            {
                get { return this.BackColor; }
                set { this.BackColor = value; }
            }

            [Category("CustomButton Advance")]
            public Color TextColor
            {
                get { return this.ForeColor; }
                set { this.ForeColor = value; }
            }

            //Constructor
            public CustomButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.Size = new Size(150, 40);
                this.BackColor = Color.MediumSlateBlue;
                this.ForeColor = Color.White;
                this.Resize += new EventHandler(Button_Resize);
            }

            //Methods
            private GraphicsPath GetFigurePath(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                float curveSize = radius * 2F;

                path.StartFigure();
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                path.CloseFigure();
                return path;
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);

                Rectangle rectSurface = this.ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
                int smoothSize = 2;
                if (borderSize > 0)
                    smoothSize = borderSize;

                if (borderRadius > 2) //Rounded button
                {
                    using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                    using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                    using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        //Button surface
                        this.Region = new Region(pathSurface);
                        //Draw surface border for HD result
                        pevent.Graphics.DrawPath(penSurface, pathSurface);

                        //Button border                    
                        if (borderSize >= 1)
                            //Draw control border
                            pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
                else //Normal button
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.None;
                    //Button surface
                    this.Region = new Region(rectSurface);
                    //Button border
                    if (borderSize >= 1)
                    {
                        using (Pen penBorder = new Pen(borderColor, borderSize))
                        {
                            penBorder.Alignment = PenAlignment.Inset;
                            pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                        }
                    }
                }
            }
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
            }
            private void Container_BackColorChanged(object sender, EventArgs e)
            {
                this.Invalidate();
            }
            private void Button_Resize(object sender, EventArgs e)
            {
                if (borderRadius > this.Height)
                    borderRadius = this.Height;
            }
        }

    public enum TextPosition
        {
            Left,
            Right,
            Center,
            Sliding,
            None
        }

        public class CustomProgressBar : ProgressBar
        {
            //Fields
            //-> Appearance
            private Color channelColor = Color.LightSteelBlue;
            private Color sliderColor = Color.RoyalBlue;
            private Color foreBackColor = Color.RoyalBlue;
            private int channelHeight = 6;
            private int sliderHeight = 6;
            private TextPosition showValue = TextPosition.Right;
            private string symbolBefore = "";
            private string symbolAfter = "";
            private bool showMaximun = false;

            //-> Others
            private bool paintedBack = false;
            private bool stopPainting = false;

            //Constructor
            public CustomProgressBar()
            {
                this.SetStyle(ControlStyles.UserPaint, true);
                this.ForeColor = Color.White;
            }

            //Propertiesfff
            [Category("CustomProgressBar Advance")]
            public Color ChannelColor
            {
                get { return channelColor; }
                set
                {
                    channelColor = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public Color SliderColor
            {
                get { return sliderColor; }
                set
                {
                    sliderColor = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public Color ForeBackColor
            {
                get { return foreBackColor; }
                set
                {
                    foreBackColor = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public int ChannelHeight
            {
                get { return channelHeight; }
                set
                {
                    channelHeight = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public int SliderHeight
            {
                get { return sliderHeight; }
                set
                {
                    sliderHeight = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public TextPosition ShowValue
            {
                get { return showValue; }
                set
                {
                    showValue = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public string SymbolBefore
            {
                get { return symbolBefore; }
                set
                {
                    symbolBefore = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public string SymbolAfter
            {
                get { return symbolAfter; }
                set
                {
                    symbolAfter = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            public bool ShowMaximun
            {
                get { return showMaximun; }
                set
                {
                    showMaximun = value;
                    this.Invalidate();
                }
            }

            [Category("CustomProgressBar Advance")]
            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            public override Font Font
            {
                get { return base.Font; }
                set
                {
                    base.Font = value;
                }
            }

            [Category("CustomProgressBar Advance")]
            public override Color ForeColor
            {
                get { return base.ForeColor; }
                set
                {
                    base.ForeColor = value;
                }
            }

            //-> Paint the background & channel
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                if (stopPainting == false)
                {
                    if (paintedBack == false)
                    {
                        //Fields
                        Graphics graph = pevent.Graphics;
                        Rectangle rectChannel = new Rectangle(0, 0, this.Width, ChannelHeight);
                        using (var brushChannel = new SolidBrush(channelColor))
                        {
                            if (channelHeight >= sliderHeight)
                                rectChannel.Y = this.Height - channelHeight;
                            else rectChannel.Y = this.Height - ((channelHeight + sliderHeight) / 2);

                            //Painting
                            graph.Clear(this.Parent.BackColor);//Surface
                            graph.FillRectangle(brushChannel, rectChannel);//Channel

                            //Stop painting the back & Channel
                            if (this.DesignMode == false)
                                paintedBack = true;
                        }
                    }
                    //Reset painting the back & channel
                    if (this.Value == this.Maximum || this.Value == this.Minimum)
                        paintedBack = false;
                }
            }
            //-> Paint slider
            protected override void OnPaint(PaintEventArgs e)
            {
                if (stopPainting == false)
                {
                    //Fields
                    Graphics graph = e.Graphics;
                    double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
                    int sliderWidth = (int)(this.Width * scaleFactor);
                    Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight);
                    using (var brushSlider = new SolidBrush(sliderColor))
                    {
                        if (sliderHeight >= channelHeight)
                            rectSlider.Y = this.Height - sliderHeight;
                        else rectSlider.Y = this.Height - ((sliderHeight + channelHeight) / 2);

                        //Painting
                        if (sliderWidth > 1) //Slider
                            graph.FillRectangle(brushSlider, rectSlider);
                        if (showValue != TextPosition.None) //Text
                            DrawValueText(graph, sliderWidth, rectSlider);
                    }
                }
                if (this.Value == this.Maximum) stopPainting = true;//Stop painting
                else stopPainting = false; //Keep painting
            }

            //-> Paint value text
            private void DrawValueText(Graphics graph, int sliderWidth, Rectangle rectSlider)
            {
                //Fields
                string text = symbolBefore + this.Value.ToString() + symbolAfter + "%";
                if (showMaximun) text = symbolBefore + this.Value.ToString() + symbolAfter + "/" + symbolBefore + this.Maximum.ToString() + symbolAfter + " MB   ";
                var textSize = TextRenderer.MeasureText(text, this.Font);
                var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);
                using (var brushText = new SolidBrush(this.ForeColor))
                using (var brushTextBack = new SolidBrush(foreBackColor))
                using (var textFormat = new StringFormat())
                {
                    switch (showValue)
                    {
                        case TextPosition.Left:
                            rectText.X = 0;
                            textFormat.Alignment = StringAlignment.Near;
                            break;

                        case TextPosition.Right:
                            rectText.X = this.Width - textSize.Width;
                            textFormat.Alignment = StringAlignment.Far;
                            break;

                        case TextPosition.Center:
                            rectText.X = (this.Width - textSize.Width) / 2;
                            textFormat.Alignment = StringAlignment.Center;
                            break;

                        case TextPosition.Sliding:
                            rectText.X = sliderWidth - textSize.Width;
                            textFormat.Alignment = StringAlignment.Center;
                            //Clean previous text surface
                            using (var brushClear = new SolidBrush(this.Parent.BackColor))
                            {
                                var rect = rectSlider;
                                rect.Y = rectText.Y;
                                rect.Height = rectText.Height;
                                graph.FillRectangle(brushClear, rect);
                            }
                            break;
                    }
                    //Painting
                    graph.FillRectangle(brushTextBack, rectText);
                    graph.DrawString(text, this.Font, brushText, rectText, textFormat);
                }
            }


        }
    }
