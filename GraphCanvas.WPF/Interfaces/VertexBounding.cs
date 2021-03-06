﻿/*
 * Copyright (c) 2011-2013 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Loki <http://www.github.com/ahzf/Loki>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * You may obtain a copy of the License at
 *   http://www.gnu.org/licenses/gpl.html
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

#endregion

namespace eu.Vanaheimr.Loki
{

    public enum VertexBorderShape
    {
        Rectangle,
        Circle
    }

    public class VertexBorder
    {

        public readonly Double              Width;
        public readonly Double              Height;
        public readonly VertexBorderShape   Shape;

        public VertexBorder(Double Width, Double Height, VertexBorderShape Shape = VertexBorderShape.Circle)
        {

            this.Width   = Width;
            this.Height  = Height;
            this.Shape   = Shape;

        }

    }


}
