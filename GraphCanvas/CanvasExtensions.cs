/*
 * Copyright (c) 2011 Achim 'ahzf' Friedland <achim@ahzf.de>
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.PropertyGraph.InMemory;
using System.Windows.Media.Imaging;
using System.IO;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// Extension methods for the canvas class.
    /// </summary>
    public static class CanvasExtensions
    {

        public static MemoryStream SaveAsPNG(this Canvas _Canvas, Double dpiX = 96d, Double dpiY = 96d)
        {
            return _Canvas.SaveAs(new PngBitmapEncoder(), dpiX, dpiY);
        }

        public static MemoryStream SaveAsJPEG(this Canvas _Canvas, Double dpiX = 96d, Double dpiY = 96d)
        {
            return _Canvas.SaveAs(new JpegBitmapEncoder(), dpiX, dpiY);
        }

        public static MemoryStream SaveAs(this Canvas _Canvas, BitmapEncoder BitmapEncoder, Double dpiX = 96d, Double dpiY = 96d)
        {

            if (BitmapEncoder == null)
                throw new ArgumentNullException("The bitmap encoder must not be null!");

            // Save and reset current canvas transform
            var CanvasTransformation = _Canvas.LayoutTransform;
            _Canvas.LayoutTransform = null;

            // Measure and arrange the canvas
            var w = Double.IsNaN(_Canvas.Width)  ? _Canvas.ActualWidth  : _Canvas.Width;
            var h = Double.IsNaN(_Canvas.Height) ? _Canvas.ActualHeight : _Canvas.Height;
            var CanvasSize = new Size(w, h);
            _Canvas.Measure(CanvasSize);
            _Canvas.Arrange(new Rect(CanvasSize));

            var _RenderTargetBitmap = new RenderTargetBitmap(
                (Int32) (CanvasSize.Width  * dpiX / 96.0),
                (Int32) (CanvasSize.Height * dpiY / 96.0),
                dpiX, dpiY,
                PixelFormats.Pbgra32);
            _RenderTargetBitmap.Render(_Canvas);

            var MemoryStream = new MemoryStream();
            BitmapEncoder.Frames.Add(BitmapFrame.Create(_RenderTargetBitmap));
            BitmapEncoder.Save(MemoryStream);

            // Restore previously saved layout
            _Canvas.LayoutTransform = CanvasTransformation;

            return MemoryStream;

        }

    }

}
