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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion

namespace de.ahzf.Loki
{
    
    /// <summary>
    /// Drawing elements on a WPF canvas
    /// </summary>
    public static class DrawElements
    {

        #region DrawEllipse

        public static Ellipse DrawEllipse(this Canvas Canvas, Double x, Double y, Double width, Double height, Color StrokeColor)
        {

            var _Ellipse = new Ellipse();
            _Ellipse.Stroke = new SolidColorBrush(StrokeColor);
            _Ellipse.StrokeThickness = 1;
            _Ellipse.Width = width;
            _Ellipse.Height = height;
            _Ellipse.Fill = new SolidColorBrush(Colors.Transparent);
            Canvas.Children.Add(_Ellipse);
            Canvas.SetLeft(_Ellipse, x - width / 2);
            Canvas.SetTop(_Ellipse, y - height / 2);

            return _Ellipse;

        }

        #endregion

        #region DrawLine

        public static Line DrawLine(this Canvas Canvas, Double x1, Double y1, Double x2, Double y2, Color Color)
        {

            // Create a Line
            var _Line = new Line();
            _Line.X1 = x1;
            _Line.Y1 = y1;
            _Line.X2 = x2;
            _Line.Y2 = y2;

            // Set Line's width and color
            _Line.StrokeThickness = 0.5;
            _Line.Stroke = new SolidColorBrush(Color);

            // Add line to the canvas
            Canvas.Children.Add(_Line);

            return _Line;

        }

        #endregion

        #region DrawText

        public static TextBlock DrawText(this Canvas Canvas, Double x, Double y, String text, Color color)
        {

            var _TextBlock = new TextBlock();
            _TextBlock.Text = text;
            _TextBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(_TextBlock, x);
            Canvas.SetTop(_TextBlock, y);
            Canvas.Children.Add(_TextBlock);

            return _TextBlock;

        }

        #endregion

    }

}
