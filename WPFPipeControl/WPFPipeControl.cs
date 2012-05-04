/*
 * Copyright (c) 2011 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Loki <http://www.github.com/ahzf/Loki>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * You may obtain a copy of the License at
 *     http://www.gnu.org/licenses/gpl.html
 *     
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 */

#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion

namespace WPFPipeControl
{

    //public class HalfValueConverter : IMultiValueConverter
    //{
    //    #region IMultiValueConverter Members

    //    public object Convert(object[] values,
    //                          Type targetType,
    //                          object parameter,
    //                          CultureInfo culture)
    //    {
    //        if (values == null || values.Length < 2)
    //        {
    //            throw new ArgumentException(
    //                "HalfValueConverter expects 2 double values to be passed" +
    //                " in this order -> totalWidth, width",
    //                "values");
    //        }
    //        if (values != null && values.Length == 2 && values[0] != null && values[1] != null)
    //        {
    //            double totalWidth = Double.Parse(values[0].ToString());
    //            double width      = Double.Parse(values[1].ToString());
    //            return (Object)(totalWidth);
    //        }
    //        //return (object)((totalWidth - width) / 2);

    //        return 0;

    //    }

    //    public object[] ConvertBack(object value,
    //                                Type[] targetTypes,
    //                                object parameter,
    //                                CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}

    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFPipeControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFPipeControl;assembly=WPFPipeControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public partial class WPFPipeControl : HeaderedContentControl
    {

        #region PipeName

        static public readonly DependencyProperty PipeNameProperty = DependencyProperty.Register("PipeName", typeof(String), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata("pipe", null));

        public String PipeName
        {
            get
            {
                return (String)this.GetValue(PipeNameProperty);
            }
            set
            {
                this.SetValue(PipeNameProperty, value);
            }
        }

        #endregion

        #region GradientColor1

        static public readonly DependencyProperty GradientColor1Property = DependencyProperty.Register("GradientColor1", typeof(String), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata("#2a802f", null));

        public String GradientColor1
        {
            get
            {
                return (String) this.GetValue(GradientColor1Property);
            }
            set
            {
                this.SetValue(GradientColor1Property, value);
            }
        }

        #endregion

        #region GradientColor2

        static public readonly DependencyProperty GradientColor2Property = DependencyProperty.Register("GradientColor2", typeof(String), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata("#2a802f", null));

        public String GradientColor2
        {
            get
            {
                return (String) this.GetValue(GradientColor2Property);
            }
            set
            {
                this.SetValue(GradientColor2Property, value);
            }
        }

        #endregion

        #region BorderColor

        static public readonly DependencyProperty BorderColorProperty = DependencyProperty.Register("BorderColor", typeof(String), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata("green", null));

        public String BorderColor
        {
            get
            {
                return (String) this.GetValue(BorderColorProperty);
            }
            set
            {
                this.SetValue(BorderColorProperty, value);
            }
        }

        #endregion

        #region TextColor

        static public readonly DependencyProperty TextColorProperty = DependencyProperty.Register("TextColor", typeof(String), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata("yellow", null));

        public String TextColor
        {
            get
            {
                return (String)this.GetValue(TextColorProperty);
            }
            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }

        #endregion



        public Double Width1
        {
            get
            {
                return ActualWidth * 5 / 6 ;
            }
        }

        static public readonly DependencyProperty Width2Property = DependencyProperty.Register("Width2", typeof(Double), typeof(WPFPipeControl),
            new FrameworkPropertyMetadata(10.0, null));



        public Double myWidth { get; set; }



        public Double Width2
        {
            get
            {
                return (Double) this.GetValue(Width2Property);
            }
            set
            {
                this.SetValue(Width2Property, value);
            }
        }

        public Double Height1
        {
            get
            {
                return ActualHeight * 5 / 6 + 10;
            }
        }

        static WPFPipeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WPFPipeControl), new FrameworkPropertyMetadata(typeof(WPFPipeControl)));
            
        }

        public WPFPipeControl()
        {
            var _rec3 = new Rectangle();
            _rec3.Width = 100;
            _rec3.Height = 100;
            _rec3.Fill = new SolidColorBrush(Colors.Red);            
        }

    }
}
