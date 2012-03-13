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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using de.ahzf.Blueprints;
using de.ahzf.Illias.Geometry;

#endregion

namespace de.ahzf.Loki
{

    public class HeatMap
    {

        #region Data

        private EditableBitmap EditableBitmap;

        #endregion

        #region Properties

        public Int32 Width  { get; set; }
        public Int32 Height { get; set; }
        public Int32 DpiX   { get; set; }
        public Int32 DpiY   { get; set; }

        #endregion

        #region Constructor(s)

        #region HeatMap(Width, Height, DpiX = 96, DpiY = 96)

        public HeatMap(Int32 Width, Int32 Height, Int32 DpiX = 96, Int32 DpiY = 96)
        {
            this.Width  = Width;
            this.Height = Height;
            this.DpiX   = DpiX;
            this.DpiY   = DpiY;
        }

        #endregion

        #endregion


        public BitmapSource Create()
        {

            this.EditableBitmap = new EditableBitmap(Width, Height, 0xff, 0xff, 0, 0xaa);
            //return this.EditableBitmap.RenderBitmap();

            var _Sensors = new List<IPixelValuePair<Double, Byte>>();
            _Sensors.Add(new PixelValuePair<Double, Byte>(32,  64,  128));
            _Sensors.Add(new PixelValuePair<Double, Byte>(128, 128, 255));

            Pixel<Double> Pixel = null;
            var Distance = new ThreadLocal<Double>();
            var Force    = new ThreadLocal<Double>();
            var Force2   = new ThreadLocal<Double>();
            Int32 n = _Sensors.Count;

            Parallel.For(0, Width, (_sx) =>
            {
                for (var _sy = 0; _sy < Height; _sy++)
                {

                    Force.Value = 0.0;

                    foreach (var _Sensor in _Sensors)
                    {
                        Distance.Value = _Sensor.DistanceTo(_sx, _sy);
                        if (Distance.Value == 0)
                            Force.Value += _Sensor.Value;
                        else
                            Force.Value += n * _Sensor.Value / Distance.Value;
                    }

                    if (Force.Value <= 255 * n)
                        Force2.Value = (255 * n - Force.Value) / n;
                    else
                        Force2.Value = 255;

                    if (Force2.Value < 0)
                    {
                        var ab = 1;
                        ab = 2;
                    }

                    if (Force2.Value > 255)
                    {
                        var ac = 1;
                        ac = 2;
                    }

                    this.EditableBitmap.PutPixel(_sx, _sy, (Byte) Force2.Value, 0xaa);

                }
            });

            return this.EditableBitmap.RenderBitmap();

            //var _Image2 = new Image();
            //_Image2.Stretch = Stretch.Uniform;
            //_Image2.Source = Pic;
            //_Image2.Width = Pic.PixelWidth;

        }

    }

}
