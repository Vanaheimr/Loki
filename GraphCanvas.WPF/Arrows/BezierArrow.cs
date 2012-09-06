/*
 * Copyright (c) 2011-2012 Achim 'ahzf' Friedland <achim@graph-database.org>
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

namespace de.ahzf.Vanaheimr.Loki
{

    /// <summary>
    /// Draws a bezier arrow.
    /// </summary>
    public class BezierArrow : AArrowShape
    {

        #region Properties

        #region X1CP

        /// <summary>
        /// The x-coordinate of the bezier control point.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double X1CP
		{

			get
            {
                return (Double) base.GetValue(X1CPProperty);
            }

			set
            {
                base.SetValue(X1CPProperty, value);
            }

		}

        #endregion

        #region Y1CP

        /// <summary>
        /// The y-coordinate of the bezier control point.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double Y1CP
		{
			
            get
            {
                return (Double) base.GetValue(Y1CPProperty);
            }

			set
            {
                base.SetValue(Y1CPProperty, value);
            }

		}

        #endregion

        #region X2CP

        /// <summary>
        /// The x-coordinate of the bezier control point.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double X2CP
		{

			get
            {
                return (Double) base.GetValue(X2CPProperty);
            }

			set
            {
                base.SetValue(X2CPProperty, value);
            }

		}

        #endregion

        #region Y2CP

        /// <summary>
        /// The y-coordinate of the bezier control point.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double Y2CP
		{
			
            get
            {
                return (Double) base.GetValue(Y2CPProperty);
            }

			set
            {
                base.SetValue(Y2CPProperty, value);
            }

		}

        #endregion

        #endregion

		#region Dependency Properties

        #region X1CP

        /// <summary>
        /// The x-coordinate of the bezier control point.
        /// </summary>
		public static readonly DependencyProperty X1CPProperty
                             = DependencyProperty.Register("X1CP",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Y1CP

        /// <summary>
        /// The y-coordinate of the bezier control point.
        /// </summary>
		public static readonly DependencyProperty Y1CPProperty
                             = DependencyProperty.Register("Y1CP",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion
        
        #region X2CP

        /// <summary>
        /// The x-coordinate of the bezier control point.
        /// </summary>
		public static readonly DependencyProperty X2CPProperty
                             = DependencyProperty.Register("X2CP",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Y2CP

        /// <summary>
        /// The y-coordinate of the bezier control point.
        /// </summary>
		public static readonly DependencyProperty Y2CPProperty
                             = DependencyProperty.Register("Y2CP",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


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

			var ArrowOrigin        = new Point(X1, Y1);
            var OriginControlPoint = new Point(X1CP, Y1CP);
            var ArrowTarget        = new Point(X2, Y2);
            var TargetControlPoint = new Point(Y2CP, Y2CP);

            var pt3 = new Point(X2 + (HeadWidth  * cost - HeadHeight * sint),
				                Y2 + (HeadWidth  * sint + HeadHeight * cost));

            var pt4 = new Point(X2 + (HeadWidth  * cost + HeadHeight * sint),
				                Y2 - (HeadHeight * cost - HeadWidth  * sint));

			StreamGeometryContext.BeginFigure(ArrowOrigin, isFilled:  false, isClosed:    false);
            StreamGeometryContext.BezierTo(OriginControlPoint, TargetControlPoint, ArrowTarget, true, true);
            //StreamGeometryContext.LineTo     (pt3,         isStroked: true, isSmoothJoin: true);
            //StreamGeometryContext.LineTo     (ArrowTarget, isStroked: true, isSmoothJoin: true);
            //StreamGeometryContext.LineTo     (pt4,         isStroked: true, isSmoothJoin: true);

        }

        #endregion

    }

}
