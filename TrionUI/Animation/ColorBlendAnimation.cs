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
using System.Drawing.Drawing2D;
using System.Reflection;

namespace MetroFramework.Animation
{
    public sealed class ColorBlendAnimation : AnimationBase
    {
        public void Start(Control control, string property, Color targetColor, int duration)
        {
            if (duration <= 0) // Ensure duration is positive
                duration = 1;

            base.Start(control, transitionType, 2 * duration,
                delegate
                {
                    Color controlColor = GetPropertyValue(property, control);
                    Color newColor = DoColorBlend(controlColor, targetColor, 0.1);

                    SetPropertyValue(property, control, newColor);
                },
                delegate
                {
                    Color controlColor = GetPropertyValue(property, control);

                    return controlColor.A == targetColor.A &&
                           controlColor.R == targetColor.R &&
                           controlColor.G == targetColor.G &&
                           controlColor.B == targetColor.B;
                });
        }
        private Color DoColorBlend(Color startColor, Color targetColor, double ratio)
        {
            int a = (int)Math.Round(startColor.A * (1 - ratio) + targetColor.A * ratio);
            int r = (int)Math.Round(startColor.R * (1 - ratio) + targetColor.R * ratio);
            int g = (int)Math.Round(startColor.G * (1 - ratio) + targetColor.G * ratio);
            int b = (int)Math.Round(startColor.B * (1 - ratio) + targetColor.B * ratio);
            return Color.FromArgb(a, r, g, b);
        }
        private Color GetPropertyValue(string propertyName, Control control)
        {
            return (Color)control.GetType().GetProperty(propertyName)!.GetValue(control)!;
        }
        private void SetPropertyValue(string propertyName, Control control, Color value)
        {
            control.GetType().GetProperty(propertyName)!.SetValue(control, value);
        }

    }
}
