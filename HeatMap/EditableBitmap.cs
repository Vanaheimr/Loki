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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// An editable bitmap.
    /// </summary>
    public class EditableBitmap
    {

        #region Data

        private readonly Int32  Stride;
        private readonly Byte[] Bitplane;

        #endregion

        #region Properties

        /// <summary>
        /// The width of the bitmap.
        /// </summary>
        public Int32   Width            { get; private set; }

        /// <summary>
        /// The height of the bitmap.
        /// </summary>
        public Int32   Height           { get; private set; }

        /// <summary>
        /// The x resolution of the bitmap in dpi.
        /// </summary>
        public Int32   DpiX             { get; set; }

        /// <summary>
        /// The y resolution of the bitmap in dpi.
        /// </summary>
        public Int32   DpiY             { get; set; }

        /// <summary>
        /// Use BGRA with alpha blending or RGB without alpha blending.
        /// </summary>
        public Boolean UseAlphaBlending { get; private set; }

        #endregion

        #region Constructor(s)

        #region EditableBitmap(Width, Height, UseAlphaBlending = false)

        /// <summary>
        /// Create a new editable bitmap.
        /// </summary>
        /// <param name="Width">The width of the bitmap.</param>
        /// <param name="Height">The height of the bitmap.</param>
        /// <param name="UseAlphaBlending">Use BGRA with alpha blending or RGB without alpha blending.</param>
        public EditableBitmap(Int32 Width, Int32 Height, Boolean UseAlphaBlending = false)
        {

            this.Width            = Width;
            this.Height           = Height;
            this.UseAlphaBlending = UseAlphaBlending;

            // BGRA
            if (UseAlphaBlending)
            {
                this.Stride   = Width * 4 + (Width % 4);
                this.Bitplane = new Byte[Height * Stride];
            }

            // RGB
            else
            {
                this.Stride   = Width * 3 + (Width % 4);
                this.Bitplane = new Byte[Height * Stride];
            }

        }

        #endregion

        #region EditableBitmap(Width, Height, GrayValue)

        /// <summary>
        /// Create a new editable RGB bitmap using the given
        /// gray value as background.
        /// </summary>
        /// <param name="Width">The width of the bitmap.</param>
        /// <param name="Height">The height of the bitmap.</param>
        /// <param name="GrayValue">A gray value.</param>
        public EditableBitmap(Int32 Width, Int32 Height, Byte GrayValue)
            : this(Width, Height, GrayValue, GrayValue, GrayValue)
        { }

        #endregion

        #region EditableBitmap(Width, Height, GrayValue, AlphaValue)

        /// <summary>
        /// Create a new editable BGRA bitmap using the given
        /// gray and alpha value as background.
        /// </summary>
        /// <param name="Width">The width of the bitmap.</param>
        /// <param name="Height">The height of the bitmap.</param>
        /// <param name="GrayValue">The gray value of the background.</param>
        /// <param name="AlphaValue">The transparency of the background.</param>
        public EditableBitmap(Int32 Width, Int32 Height, Byte GrayValue, Byte AlphaValue)
            : this(Width, Height, GrayValue, GrayValue, GrayValue, AlphaValue)
        { }

        #endregion

        #region EditableBitmap(Width, Height, RedValue, GreenValue, BlueValue)

        /// <summary>
        /// Create a new editable RGB bitmap using the given
        /// color values as background.
        /// </summary>
        /// <param name="Width">The width of the bitmap.</param>
        /// <param name="Height">The height of the bitmap.</param>
        /// <param name="RedValue">The red value of the background.</param>
        /// <param name="GreenValue">The green value of the background.</param>
        /// <param name="BlueValue">The blue value of the background.</param>
        public EditableBitmap(Int32 Width, Int32 Height, Byte RedValue, Byte GreenValue, Byte BlueValue)
            : this(Width, Height, false)
        {
            // RGB
            Parallel.For(0, Bitplane.Length/3, i => {
                Bitplane[4 * i    ] = RedValue;
                Bitplane[4 * i + 1] = GreenValue;
                Bitplane[4 * i + 2] = BlueValue;
            });
        }

        #endregion

        #region EditableBitmap(Width, Height, RedValue, GreenValue, BlueValue, AlphaValue)

        /// <summary>
        /// Create a new editable BGRA bitmap using the given
        /// color and alpha values as background.
        /// </summary>
        /// <param name="Width">The width of the bitmap.</param>
        /// <param name="Height">The height of the bitmap.</param>
        /// <param name="RedValue">The red value of the background.</param>
        /// <param name="GreenValue">The green value of the background.</param>
        /// <param name="BlueValue">The blue value of the background.</param>
        /// <param name="AlphaValue">The transparency of the background.</param>
        public EditableBitmap(Int32 Width, Int32 Height, Byte RedValue, Byte GreenValue, Byte BlueValue, Byte AlphaValue)
            : this(Width, Height, true)
        {
            // BGRA
            Parallel.For(0, Bitplane.Length/4, i => {
                Bitplane[4 * i    ] = BlueValue;
                Bitplane[4 * i + 1] = GreenValue;
                Bitplane[4 * i + 2] = RedValue;
                Bitplane[4 * i + 3] = AlphaValue;
            });
        }

        #endregion

        #endregion


        #region PutPixel(x, y, Color)

        /// <summary>
        /// Paints a pixel of the given color values
        /// at the given position.
        /// </summary>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="Color">The color value of the pixel.</param>
        public void PutPixel(Int32 x, Int32 y, Color Color)
        {
            PutPixel(x, y, Color.R, Color.G, Color.B, Color.A);
        }

        #endregion

        #region PutPixel(x, y, GrayValue, AlphaValue = 0xff)

        /// <summary>
        /// Paints a pixel of the given gray and alpha
        /// values at the given position.
        /// </summary>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="GrayValue">The gray value of the pixel.</param>
        /// <param name="AlphaValue">The alpha value of the pixel.</param>
        public void PutPixel(Int32 x, Int32 y, Byte GrayValue, Byte AlphaValue = 0xff)
        {
            PutPixel(x, y, GrayValue, GrayValue, GrayValue, AlphaValue);
        }

        #endregion

        #region PutPixel(x, y, RedValue, GreenValue, BlueValue, AlphaValue = 0xff)

        /// <summary>
        /// Paints a pixel of the given color and alpha
        /// values at the given position.
        /// </summary>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="GrayValue">The gray value of the pixel.</param>
        /// <param name="RedValue">The ref value of the pixel.</param>
        /// <param name="GreenValue">The green value of the pixel.</param>
        /// <param name="BlueValue">The blue value of the pixel.</param>
        /// <param name="AlphaValue">The alpha value of the pixel.</param>
        public void PutPixel(Int32 x, Int32 y, Byte RedValue, Byte GreenValue, Byte BlueValue, Byte AlphaValue = 0xff)
        {

            // BGRA
            if (UseAlphaBlending)
            {
                Bitplane[4 * x + y * Stride    ] = BlueValue;
                Bitplane[4 * x + y * Stride + 1] = GreenValue;
                Bitplane[4 * x + y * Stride + 2] = RedValue;
                Bitplane[4 * x + y * Stride + 3] = AlphaValue;
            }

            // RGB
            else
            {
                Bitplane[3 * x + y * Stride    ] = RedValue;
                Bitplane[3 * x + y * Stride + 1] = GreenValue;
                Bitplane[3 * x + y * Stride + 2] = BlueValue;
            }

        }

        #endregion


        #region GetPixel(x, y)

        /// <summary>
        /// Gets a pixel from the EditableBitmap.
        /// </summary>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <returns>The RGBA color information of the pixel.</returns>
        public Color GetPixel(Int32 x, Int32 y)
        {

            if (UseAlphaBlending)
                return Color.FromArgb(Bitplane[4 * x + y * Stride + 3],
                                      Bitplane[4 * x + y * Stride    ],
                                      Bitplane[4 * x + y * Stride + 1],
                                      Bitplane[4 * x + y * Stride + 2]);
            else
                return Color.FromArgb(0xff,
                                      Bitplane[4 * x + y * Stride    ],
                                      Bitplane[4 * x + y * Stride + 1],
                                      Bitplane[4 * x + y * Stride + 2]);
        }

        #endregion


        #region RenderBitmap()

        /// <summary>
        /// Renders the given bitmap to a bitmapsource.
        /// </summary>
        public BitmapSource RenderBitmap()
        {
            
            if (UseAlphaBlending)
                return BitmapSource.Create(Width, Height, DpiX, DpiY, PixelFormats.Bgra32, null, Bitplane, Stride);

            else
                return BitmapSource.Create(Width, Height, DpiX, DpiY, PixelFormats.Rgb24,  null, Bitplane, Stride);

        }

        #endregion

        #region RenderBitmap(DpiX, DpiY)

        /// <summary>
        /// Renders the given bitmap to a bitmapsource.
        /// </summary>
        /// <param name="DpiX">The x resolution of the bitmap in dpi.</param>
        /// <param name="DpiY">The y resolution of the bitmap in dpi.</param>
        public BitmapSource RenderBitmap(Int32 DpiX, Int32 DpiY)
        {
            
            if (UseAlphaBlending)
                return BitmapSource.Create(Width, Height, DpiX, DpiY, PixelFormats.Bgra32, null, Bitplane, Stride);

            else
                return BitmapSource.Create(Width, Height, DpiX, DpiY, PixelFormats.Rgb24,  null, Bitplane, Stride);

        }

        #endregion

    }

}
