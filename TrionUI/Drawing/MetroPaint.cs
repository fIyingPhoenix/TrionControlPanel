/**
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

namespace MetroFramework.Drawing
{
    public class MetroPaintEventArgs : EventArgs
    {
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }
        public Graphics Graphics { get; private set; }

        public MetroPaintEventArgs(Color backColor, Color foreColor, Graphics g)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Graphics = g;
        }
    }
    public sealed class MetroPaint
    {
        public sealed class BorderColor
        {
            public static class Button
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new Dictionary<MetroThemeStyle, Dictionary<string, Color>>()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(204, 204, 204) },
                             { "Hover", Color.FromArgb(102, 102, 102) },
                             { "Press", Color.FromArgb(51, 51, 51) },
                             { "Disabled", Color.FromArgb(155, 155, 155) },
                         }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(68, 68, 68) },
                             { "Hover", Color.FromArgb(170, 170, 170) },
                             { "Press", Color.FromArgb(238, 238, 238) },
                             { "Disabled", Color.FromArgb(109, 109, 109) },
                    }
                }
            };

                public static Color GetButtonColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
            public static class CheckBox
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new Dictionary<MetroThemeStyle, Dictionary<string, Color>>()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                       {
                         { "Normal", Color.FromArgb(153, 153, 153) },
                         { "Hover", Color.FromArgb(51, 51, 51) },
                         { "Press", Color.FromArgb(153, 153, 153) },
                         { "Disabled", Color.FromArgb(204, 204, 204) },
                       }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                      {
                        { "Normal", Color.FromArgb(153, 153, 153) },
                        { "Hover", Color.FromArgb(204, 204, 204) },
                        { "Press", Color.FromArgb(153, 153, 153) },
                        { "Disabled", Color.FromArgb(85, 85, 85) },
                      }
                    }
                 };

                public static Color GetCheckBoxColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
            public static class ComboBox
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new Dictionary<MetroThemeStyle, Dictionary<string, Color>>()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(153, 153, 153) },
                             { "Hover", Color.FromArgb(102, 102, 102) },
                             { "Press", Color.FromArgb(51, 51, 51) },
                             { "Disabled", Color.FromArgb(155, 155, 155) },
                         }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(153, 153, 153) },
                             { "Hover", Color.FromArgb(170, 170, 170) },
                             { "Press", Color.FromArgb(238, 238, 238) },
                             { "Disabled", Color.FromArgb(109, 109, 109) },
                        }
                    }
                };

                public static Color GetButtonColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
            public static class ProgressBar
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(204, 204, 204) }, //FromArgb(153, 153, 153) },
                             { "Hover", Color.FromArgb(68, 68, 68) },
                             { "Press", Color.FromArgb(68, 68, 68) },
                             { "Disabled", Color.FromArgb(155, 155, 155) },
                         }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.Black },
                             { "Hover", Color.FromArgb(204, 204, 204) },
                             { "Press", Color.FromArgb(204, 204, 204) },
                             { "Disabled", Color.FromArgb(109, 109, 109) },
                        }
                    }
                };

                public static Color GetButtonColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
            public static class TabControl
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(204, 204, 204) }, //FromArgb(153, 153, 153) },
                             { "Hover", Color.FromArgb(68, 68, 68) },
                             { "Press", Color.FromArgb(68, 68, 68) },
                             { "Disabled", Color.FromArgb(155, 155, 155) },
                         }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(68, 68, 68) },
                             { "Hover", Color.FromArgb(204, 204, 204) },
                             { "Press", Color.FromArgb(204, 204, 204) },
                             { "Disabled", Color.FromArgb(109, 109, 109) },
                        }
                    }
                };

                public static Color GetButtonColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
        }

        public sealed class BackColor
        {

            public class Form
            {
                private static readonly Dictionary<MetroThemeStyle, Dictionary<string, Color>> colorMap = new()
                {
                    { MetroThemeStyle.Light, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(255, 255, 255) }
                         }
                    },
                    { MetroThemeStyle.Dark, new Dictionary<string, Color>()
                        {
                             { "Normal", Color.FromArgb(28, 33, 40) }
                        }
                    }
                };

                public static Color GetButtonColor(MetroThemeStyle theme, string state)
                {
                    if (!colorMap.ContainsKey(theme) || !colorMap[theme].ContainsKey(state))
                    {
                        throw new ArgumentException("Invalid theme or state");
                    }
                    return colorMap[theme][state];
                }
            }
            public sealed class Button
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(34, 34, 34);

                    return Color.FromArgb(238, 238, 238);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(102, 102, 102);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(238, 238, 238);

                    return Color.FromArgb(51, 51, 51);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(80, 80, 80);

                    return Color.FromArgb(204, 204, 204);
                }
            }
            public sealed class TrackBar
            {
                public sealed class Thumb
                {
                    public static Color Normal(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(153, 153, 153);

                        return Color.FromArgb(102, 102, 102);
                    }

                    public static Color Hover(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(204, 204, 204);

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Press(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(204, 204, 204);

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Disabled(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(85, 85, 85);

                        return Color.FromArgb(179, 179, 179);
                    }
                }

                public sealed class Bar
                {
                    public static Color Normal(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Hover(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Press(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Disabled(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(34, 34, 34);

                        return Color.FromArgb(230, 230, 230);
                    }
                }
            }
            public sealed class ScrollBar
            {
                public sealed class Thumb
                {
                    public static Color Normal(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(221, 221, 221);
                    }

                    public static Color Hover(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(204, 204, 204);

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Press(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(204, 204, 204);

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Disabled(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(221, 221, 221);
                    }
                }

                public sealed class Bar
                {
                    public static Color Normal(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(38, 38, 38);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Hover(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(38, 38, 38);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Press(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(38, 38, 38);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Disabled(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(38, 38, 38);

                        return Color.FromArgb(234, 234, 234);
                    }
                }
            }
            public sealed class ProgressBar
            {
                public sealed class Bar
                {
                    public static Color Normal(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(28, 33, 40);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Hover(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(28, 33, 40);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Press(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(28, 33, 40);

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Disabled(MetroThemeStyle theme)
                    {
                        if (theme == MetroThemeStyle.Dark)
                            return Color.FromArgb(51, 51, 51);

                        return Color.FromArgb(221, 221, 221);
                    }
                }
            }
        }
        public sealed class ForeColor
        {
            public sealed class Button
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(204, 204, 204);

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(17, 17, 17);

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(17, 17, 17);

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(109, 109, 109);

                    return Color.FromArgb(136, 136, 136);
                }
            }
            public static Color Title(MetroThemeStyle theme)
            {
                if (theme == MetroThemeStyle.Dark)
                    return Color.FromArgb(255, 255, 255);

                return Color.FromArgb(0, 0, 0);
            }
            public sealed class Tile
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(255, 255, 255);

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(255, 255, 255);

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(255, 255, 255);

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(209, 209, 209);

                    return Color.FromArgb(209, 209, 209);
                }
            }
            public sealed class Link
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(93, 93, 93);

                    return Color.FromArgb(128, 128, 128);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(93, 93, 93);

                    return Color.FromArgb(128, 128, 128);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(51, 51, 51);

                    return Color.FromArgb(209, 209, 209);
                }
            }
            public sealed class Label
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(51, 51, 51);

                    return Color.FromArgb(209, 209, 209);
                }
            }
            public sealed class CheckBox
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(17, 17, 17);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(153, 153, 153);

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(153, 153, 153);

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(93, 93, 93);

                    return Color.FromArgb(136, 136, 136);
                }
            }
            public sealed class ComboBox
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(153, 153, 153);

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Hover(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(17, 17, 17);
                }

                public static Color Press(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(153, 153, 153);

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(93, 93, 93);

                    return Color.FromArgb(136, 136, 136);
                }
            }
            public sealed class ProgressBar
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(51, 51, 51);

                    return Color.FromArgb(209, 209, 209);
                }
            }
            public sealed class TabControl
            {
                public static Color Normal(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(170, 170, 170);

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(MetroThemeStyle theme)
                {
                    if (theme == MetroThemeStyle.Dark)
                        return Color.FromArgb(51, 51, 51);

                    return Color.FromArgb(209, 209, 209);
                }
            }
        }

        #region Helper Methods
        public static Color GetStyleColor(MetroColorStyle style)
        {
            return style switch
            {
                MetroColorStyle.Black => MetroColors.Black,
                MetroColorStyle.White => MetroColors.White,
                MetroColorStyle.Silver => MetroColors.Silver,
                MetroColorStyle.Blue => MetroColors.Blue,
                MetroColorStyle.Green => MetroColors.Green,
                MetroColorStyle.Lime => MetroColors.Lime,
                MetroColorStyle.Teal => MetroColors.Teal,
                MetroColorStyle.Orange => MetroColors.Orange,
                MetroColorStyle.Brown => MetroColors.Brown,
                MetroColorStyle.Pink => MetroColors.Pink,
                MetroColorStyle.Magenta => MetroColors.Magenta,
                MetroColorStyle.Purple => MetroColors.Purple,
                MetroColorStyle.Red => MetroColors.Red,
                MetroColorStyle.Yellow => MetroColors.Yellow,
                _ => MetroColors.Blue,
            };
        }

        public static SolidBrush? GetStyleBrush(MetroColorStyle style)
        {
            return style switch
            {
                MetroColorStyle.Black => MetroBrushes.Black,
                MetroColorStyle.White => MetroBrushes.White,
                MetroColorStyle.Silver => MetroBrushes.Silver,
                MetroColorStyle.Blue => MetroBrushes.Blue,
                MetroColorStyle.Green => MetroBrushes.Green,
                MetroColorStyle.Lime => MetroBrushes.Lime,
                MetroColorStyle.Teal => MetroBrushes.Teal,
                MetroColorStyle.Orange => MetroBrushes.Orange,
                MetroColorStyle.Brown => MetroBrushes.Brown,
                MetroColorStyle.Pink => MetroBrushes.Pink,
                MetroColorStyle.Magenta => MetroBrushes.Magenta,
                MetroColorStyle.Purple => MetroBrushes.Purple,
                MetroColorStyle.Red => MetroBrushes.Red,
                MetroColorStyle.Yellow => MetroBrushes.Yellow,
                MetroColorStyle.Default => throw new NotImplementedException(),
                _ => MetroBrushes.Blue!
            };
        }
        public static Pen GetStyl5ePen(MetroColorStyle style)
        {
            return style switch
            {
                MetroColorStyle.Black => MetroPens.Black!,
                MetroColorStyle.White => MetroPens.White!,
                MetroColorStyle.Silver => MetroPens.Silver!,
                MetroColorStyle.Blue => MetroPens.Blue!,
                MetroColorStyle.Green => MetroPens.Green!,
                MetroColorStyle.Lime => MetroPens.Lime!,
                MetroColorStyle.Teal => MetroPens.Teal!,
                MetroColorStyle.Orange => MetroPens.Orange!,
                MetroColorStyle.Brown => MetroPens.Brown!,
                MetroColorStyle.Pink => MetroPens.Pink!,
                MetroColorStyle.Magenta => MetroPens.Magenta!,
                MetroColorStyle.Purple => MetroPens.Purple!,
                MetroColorStyle.Red => MetroPens.Red!,
                MetroColorStyle.Yellow => MetroPens.Yellow!,
                _ => MetroPens.Blue!,
            };
        }
        public static TextFormatFlags GetTextFormatFlags(ContentAlignment textAlign)
        {
            TextFormatFlags controlFlags = TextFormatFlags.EndEllipsis;

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopRight:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleLeft:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.BottomLeft:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomRight:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }
            return controlFlags;
        }
        #endregion
    }
}
