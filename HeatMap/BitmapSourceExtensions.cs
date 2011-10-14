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

using System.IO;
using System.Windows.Media.Imaging;

#endregion

namespace de.ahzf.Loki
{

    //public static class BitmapSourceExtensions
    //{

    //    public static Stream AsBMP(this BitmapSource BitmapSource)
    //    {
    //        return EncodeBitmap(BitmapSource, new BmpBitmapEncoder());
    //    }

    //    public static Stream AsJPEG(this BitmapSource BitmapSource)
    //    {
    //        return EncodeBitmap(BitmapSource, new JpegBitmapEncoder());
    //    }

    //    public static Stream AsPNG(this BitmapSource BitmapSource)
    //    {
    //        return EncodeBitmap(BitmapSource, new PngBitmapEncoder());
    //    }

    //    private static Stream EncodeBitmap(this BitmapSource BitmapSource, BitmapEncoder BitmapEncoder)
    //    {
    //        using (var MemoryStream = new MemoryStream())
    //        {
    //            BitmapEncoder.Frames.Add(BitmapFrame.Create(BitmapSource));
    //            BitmapEncoder.Save(MemoryStream);
    //            return MemoryStream;
    //        }
    //    }

    //}

}
