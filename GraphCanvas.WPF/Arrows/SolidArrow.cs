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
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// Draws an arrow having a solid head.
    /// </summary>
    public class SolidArrow : AArrowShape
    {

        #region (protected) DrawArrowGeometry(StreamGeometryContext)

        /// <summary>
        /// The actual method for drawing the arrow.
        /// </summary>
        /// <param name="StreamGeometryContext">Describes a geometry using drawing commands.</param>
        protected override void DrawArrowGeometry(StreamGeometryContext StreamGeometryContext)
		{

			var theta = Math.Atan2(Y1 - Y2, X1 - X2);
			var sint  = Math.Sin(theta);
			var cost  = Math.Cos(theta);

			var ArrowOrigin = new Point(X1, this.Y1);
			var ArrowTarget = new Point(X2, this.Y2);

            var pt3 = new Point(X2 + (HeadWidth  * cost - HeadHeight * sint),
				                Y2 + (HeadWidth  * sint + HeadHeight * cost));

            var pt4 = new Point(X2 + (HeadWidth  * cost + HeadHeight * sint),
				                Y2 - (HeadHeight * cost - HeadWidth  * sint));

			StreamGeometryContext.BeginFigure(ArrowOrigin, isFilled:  true, isClosed:    false);
			StreamGeometryContext.LineTo     (ArrowTarget, isStroked: true, isSmoothJoin: true);
            StreamGeometryContext.LineTo     (pt3,         isStroked: true, isSmoothJoin: true);
			StreamGeometryContext.LineTo     (pt4,         isStroked: true, isSmoothJoin: true);
            StreamGeometryContext.LineTo     (ArrowTarget, isStroked: true, isSmoothJoin: true);
            StreamGeometryContext.Close();

        }

        #endregion

    }

}
