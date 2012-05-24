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
using System.IO;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Markup;

#endregion

namespace de.ahzf.Vanaheimr.Loki
{

    /// <summary>
    /// Extension methods for the canvas class.
    /// </summary>
    public static class CanvasExtensions
    {

#if !SILVERLIGHT

        #region SaveAsPNG(this Canvas, dpiX = 96d, dpiY = 96d)

        /// <summary>
        /// Saves the given canvas as PNG image.
        /// </summary>
        /// <param name="Canvas">The canvas.</param>
        /// <param name="dpiX">The desired x-resolution of the saved image.</param>
        /// <param name="dpiY">The desired y-resolution of the saved image.</param>
        /// <returns>A memory stream containing the encoded PNG image.</returns>
        public static MemoryStream SaveAsPNG(this Canvas Canvas, Double dpiX = 96d, Double dpiY = 96d)
        {

            if (Canvas == null)
                throw new ArgumentNullException("The given canvas must not be null!");

            return Canvas.SaveAs(new PngBitmapEncoder(), dpiX, dpiY);

        }

        #endregion

        #region SaveAsJPEG(this Canvas, QualityLevel = 98, dpiX = 96d, dpiY = 96d)

        /// <summary>
        /// Saves the given canvas as JPEG image.
        /// </summary>
        /// <param name="Canvas">The canvas.</param>
        /// <param name="QualityLevel">The JPEG quality level.</param>
        /// <param name="dpiX">The desired x-resolution of the saved image.</param>
        /// <param name="dpiY">The desired y-resolution of the saved image.</param>
        /// <returns>A memory stream containing the encoded JPEG image.</returns>
        public static MemoryStream SaveAsJPEG(this Canvas Canvas, Int32 QualityLevel = 98, Double dpiX = 96d, Double dpiY = 96d)
        {

            if (Canvas == null)
                throw new ArgumentNullException("The given canvas must not be null!");

            return Canvas.SaveAs(new JpegBitmapEncoder() { QualityLevel = QualityLevel }, dpiX, dpiY);

        }

        #endregion

        #region SaveAsXAML(this Canvas, Indent = true)

        /// <summary>
        /// Saves the given canvas as XAML.
        /// </summary>
        /// <param name="Canvas">The canvas.</param>
        /// <param name="Indent">XML indention on or off.</param>
        /// <returns>A memory stream containing the encoded XAML.</returns>
        public static MemoryStream SaveAsXAML(this Canvas Canvas, Boolean Indent = true)
        {

            #region Initial checks

            if (Canvas == null)
                throw new ArgumentNullException("The given canvas must not be null!");

            #endregion

            var MemoryStream = new MemoryStream();
            var XAMLWriter   = new XamlDesignerSerializationManager(
                                       XmlWriter.Create(MemoryStream,
                                                        new XmlWriterSettings() {
                                                            Indent = Indent
                                                        })
                                   );

            XamlWriter.Save(Canvas, XAMLWriter);

            return MemoryStream;

        }

        #endregion

        #region SaveAs(this Canvas, BitmapEncoder, dpiX = 96d, dpiY = 96d)

        /// <summary>
        /// Saves the given canvas using the given bitmap encoder.
        /// </summary>
        /// <param name="Canvas">The canvas.</param>
        /// <param name="BitmapEncoder">A bitmap encoder.</param>
        /// <param name="dpiX">The desired x-resolution of the saved image.</param>
        /// <param name="dpiY">The desired y-resolution of the saved image.</param>
        /// <returns>A memory stream containing the encoded JPEG image.</returns>
        public static MemoryStream SaveAs(this Canvas Canvas, BitmapEncoder BitmapEncoder, Double dpiX = 96d, Double dpiY = 96d)
        {

            #region Initial checks

            if (Canvas == null)
                throw new ArgumentNullException("The given canvas must not be null!");

            if (BitmapEncoder == null)
                throw new ArgumentNullException("The bitmap encoder must not be null!");

            #endregion

            // Save and reset current canvas transform
            var CanvasTransformation = Canvas.LayoutTransform;
            Canvas.LayoutTransform = null;

            // Measure and arrange the canvas
            var CanvasWidth  = Double.IsNaN(Canvas.Width)  ? Canvas.ActualWidth  : Canvas.Width;
            var CanvasHeight = Double.IsNaN(Canvas.Height) ? Canvas.ActualHeight : Canvas.Height;
            var CanvasSize   = new Size(CanvasWidth, CanvasHeight);
            Canvas.Measure(CanvasSize);
            Canvas.Arrange(new Rect(CanvasSize));

            var _RenderTargetBitmap = new RenderTargetBitmap(
                (Int32) (CanvasSize.Width  * dpiX / 96.0),
                (Int32) (CanvasSize.Height * dpiY / 96.0),
                dpiX, dpiY, PixelFormats.Pbgra32);
            _RenderTargetBitmap.Render(Canvas);

            // Encode bitmap
            var MemoryStream = new MemoryStream();
            BitmapEncoder.Frames.Add(BitmapFrame.Create(_RenderTargetBitmap));
            BitmapEncoder.Save(MemoryStream);

            // Restore previously saved layout
            Canvas.LayoutTransform = CanvasTransformation;

            return MemoryStream;

        }

        #endregion

#endif

    }

}
