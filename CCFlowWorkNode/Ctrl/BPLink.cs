﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace WorkNode
{
    public class BPLink : System.Windows.Controls.Label
    {
        #region  Check processing .
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (value == true)
                {
                    Thickness d = new Thickness(0.5);
                    this.BorderThickness = d;
                    this.BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    Thickness d1 = new Thickness(0);
                    this.BorderThickness = d1;
                    this.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
        }
        public void SetUnSelectedState()
        {
            if (this.IsSelected)
            {
                this.IsSelected = false;
            }
            else
            {
                this.IsSelected = true;
            }
        }
        #endregion  Check processing .

        public string FK_MapData = null;
        /// <summary>
        /// BPLink
        /// </summary>
        public BPLink()
        {
            this.Name = "LK" + DateTime.Now.ToString("yyMMddhhmmss");
            this.Content = "link...";
            //  this.NavigateUri = new Uri("http://ccflow.org");
            this.Tag = "_blank";
        }

        /// <summary>
        /// Url
        /// </summary>
        public string URL = null;
        /// <summary>
        ///  Window target 
        /// </summary>
        public string WinTarget = "_blank";

        #region  Focus Events 
        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    this.BorderBrush.Opacity = 4;
        //    base.OnGotFocus(e);
        //}
        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    this.BorderBrush.Opacity = 0.5;
        //    base.OnLostFocus(e);
        //}
        #endregion  Focus Events 

        #region  Mobile Event 
        bool trackingMouseMove = false;
        Point mousePosition;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            mousePosition = e.GetPosition(null);
            trackingMouseMove = true;
            base.OnMouseLeftButtonDown(e);
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            trackingMouseMove = false;
            base.OnMouseLeftButtonUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //FrameworkElement element = sender as FrameworkElement;
            if (trackingMouseMove)
            {
                double moveH = e.GetPosition(null).Y - mousePosition.Y;
                double moveW = e.GetPosition(null).X - mousePosition.X;
                double newTop = moveH + (double)this.GetValue(Canvas.TopProperty);
                double newLeft = moveW + (double)this.GetValue(Canvas.LeftProperty);
                this.SetValue(Canvas.TopProperty, newTop);
                this.SetValue(Canvas.LeftProperty, newLeft);
                mousePosition = e.GetPosition(null);
            }
            base.OnMouseMove(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            //  Get  textBox  With respect to the object  Canvas 的 x Coordinate  和 y Coordinate 
            double x = (double)this.GetValue(Canvas.LeftProperty);
            double y = (double)this.GetValue(Canvas.TopProperty);

            // KeyEventArgs.Key -  Event-related keyboard keys  [System.Windows.Input.Key Enumerate ]
            switch (e.Key)
            {
                // 按 Up  Key after  textBox  Objects to  上  Mobile  1  Pixels 
                // Up  Key corresponding to  e.PlatformKeyCode == 38 
                //  When available  e.Key == Key.Unknown 时, You can use  e.PlatformKeyCode  To determine the user pressed the button 
                case Key.Up:
                    this.SetValue(Canvas.TopProperty, y - 1);
                    break;
                case Key.Down:
                    this.SetValue(Canvas.TopProperty, y + 1);
                    break;
                case Key.Left:
                    this.SetValue(Canvas.LeftProperty, x - 1);
                    break;
                case Key.Right:
                    this.SetValue(Canvas.LeftProperty, x + 1);
                    break;
                case Key.Delete:
                    if (this.Name.Contains("_blank_") == false)
                    {
                        if (MessageBox.Show(" Are you sure you want to delete it ?",
                            " Delete Tip ", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                            return;
                    }
                    Canvas c = this.Parent as Canvas;
                    c.Children.Remove(this);
                    break;
                case Key.C:
                    break;
                case Key.V:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        BPLabel tb = new BPLabel();
                        tb.Cursor = Cursors.Hand;
                        tb.SetValue(Canvas.LeftProperty, (double)this.GetValue(Canvas.LeftProperty) + 15);
                        tb.SetValue(Canvas.TopProperty, (double)this.GetValue(Canvas.TopProperty) + 15);
                        Canvas s1c = this.Parent as Canvas;
                        try
                        {
                            s1c.Children.Add(tb);
                        }
                        catch
                        {
                            s1c.Children.Remove(tb);
                        }
                    }
                    break;
                default:
                    break;
            }
            base.OnKeyDown(e);
        }
        #endregion  Mobile Event 
    }
}
