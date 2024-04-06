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

namespace MetroFramework.Animation
{
    public delegate void AnimationAction();
    public delegate bool AnimationFinishedEvaluator();

    public abstract class AnimationBase
    {
        public event EventHandler AnimationCompleted;
        private void OnAnimationCompleted()
        {
            AnimationCompleted?.Invoke(this, EventArgs.Empty);
        }

        private DelayedCall timer;
        private Control targetControl;

        private AnimationAction actionHandler;
        private AnimationFinishedEvaluator evaluatorHandler;

        protected TransitionType transitionType;
        protected int counter;
        protected int startTime;
        protected int targetTime;

        public bool IsCompleted => timer?.IsWaiting == false; // Combine null check and conditional inversion
        public bool IsRunning => timer?.IsWaiting == true;  // Combine null check and conditional inversion
        public void Cancel() => timer?.Cancel(); // Simplified conditional expression

        protected void Start(Control control, TransitionType transitionType, int duration, AnimationAction actionHandler)
        {
            Start(control, transitionType, duration, actionHandler, null);
        }
        protected void Start(Control control, TransitionType transitionType, int duration, AnimationAction actionHandler, AnimationFinishedEvaluator? evaluatorHandler)
        {
            this.targetControl = control;
            this.transitionType = transitionType;
            this.actionHandler = actionHandler;
            this.evaluatorHandler = evaluatorHandler!;

            this.counter = 0;
            this.startTime = 0;
            this.targetTime = duration;

            timer = DelayedCall.Start(DoAnimation, duration);
        }

        private void DoAnimation()
        {
            if (evaluatorHandler == null || evaluatorHandler.Invoke())
            {
                OnAnimationCompleted();
            }
            else
            {
                actionHandler.Invoke();
                counter++;

                timer.Start();
            }
        }
        protected int MakeTransition(float t, float b, float d, float c)
        {
            float easedTime;
            switch (transitionType)
            {
                case TransitionType.Linear:
                    easedTime = t / d; // No easing needed
                    break;
                case TransitionType.EaseInQuad:
                    easedTime = (t /= d) * t;
                    break;
                case TransitionType.EaseOutQuad:
                    easedTime = -(t /= d) * (t - 2);
                    break;
                case TransitionType.EaseInOutQuad:
                    easedTime = (t /= d / 2) < 1 ? (t *= t) * c / 2 + b : ((--t) * (t - 2) - 1) * c / 2 + b;
                    break;
                case TransitionType.EaseInCubic:
                    easedTime = (t /= d) * t * t;
                    break;
                case TransitionType.EaseOutCubic:
                    easedTime = ((t = t / d - 1) * t * t + 1);
                    break;
                case TransitionType.EaseInOutCubic:
                    easedTime = (t /= d / 2) < 1 ? (t *= t * t) * c / 2 + b : ((t -= 2) * t * t + 2) * c / 2 + b;
                    break;
                case TransitionType.EaseInQuart:
                    easedTime = (t /= d) * t * t * t;
                    break;
                case TransitionType.EaseInExpo:
                    easedTime = (float)(t == 0 ? 0 : c * Math.Pow(2, 10 * (t / d - 1)) + b);
                    break;
                case TransitionType.EaseOutExpo:
                    easedTime = (float)(t == d ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b);
                    break;
                default:
                    return 0;
            }
            // Apply easing to calculate the final value
            return (int)(easedTime * c + b);
        }


    }
}
