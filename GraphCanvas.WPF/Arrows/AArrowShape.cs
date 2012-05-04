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
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// An abstract class for drawing an arrow.
    /// </summary>
	public abstract class AArrowShape : Shape
	{

		#region Properties

        #region X1

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double X1
		{

			get
            {
                return (Double) base.GetValue(X1Property);
            }

			set
            {
                base.SetValue(X1Property, value);
            }

		}

        #endregion

        #region Y1

        /// <summary>
        /// The y-coordinate of the arrow origin.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double Y1
		{
			
            get
            {
                return (Double) base.GetValue(Y1Property);
            }

			set
            {
                base.SetValue(Y1Property, value);
            }

		}

        #endregion

        #region X2

        /// <summary>
        /// The x-coordinate of the arrow target.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double X2
		{
			
            get
            {
                return (Double) base.GetValue(X2Property);
            }

			set
            {
                base.SetValue(X2Property, value);
            }

		}

        #endregion

        #region Y2

        /// <summary>
        /// The y-coordinate of the arrow target.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double Y2
		{
			
            get
            {
                return (Double) base.GetValue(Y2Property);
            }

			set
            {
                base.SetValue(Y2Property, value);
            }

		}

        #endregion

        #region HeadWidth

        /// <summary>
        /// The width of the arrowhead.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double HeadWidth
		{
			
            get
            {
                return (Double) base.GetValue(HeadWidthProperty);
            }
			
            set
            {
                base.SetValue(HeadWidthProperty, value);
            }

		}

        #endregion

        #region HeadHeight

        /// <summary>
        /// The height of the arrowhead.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
		public Double HeadHeight
		{
			
            get
            {
                return (Double) base.GetValue(HeadHeightProperty);
            }

			set
            {
                base.SetValue(HeadHeightProperty, value);
            }

		}

        #endregion

        #region Color

        /// <summary>
        /// The color of the arrow (fill and stroke).
        /// </summary>
        [TypeConverter(typeof(BrushConverter))]
        public Brush Color
        {

            get
            {
                return (Brush) base.GetValue(ColorProperty);
            }

            set
            {
                base.SetValue(ColorProperty, value);
                this.Stroke = value;
                this.Fill   = value;
            }

        }

        #endregion

        #endregion

		#region Dependency Properties

        #region X1

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
		public static readonly DependencyProperty X1Property
                             = DependencyProperty.Register("X1",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Y1

        /// <summary>
        /// The y-coordinate of the arrow origin.
        /// </summary>
		public static readonly DependencyProperty Y1Property
                             = DependencyProperty.Register("Y1",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));
        
        #endregion

        #region X2

        /// <summary>
        /// The x-coordinate of the arrow target.
        /// </summary>
		public static readonly DependencyProperty X2Property
                             = DependencyProperty.Register("X2",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Y2

        /// <summary>
        /// The y-coordinate of the arrow target.
        /// </summary>
		public static readonly DependencyProperty Y2Property
                             = DependencyProperty.Register("Y2",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region HeadWidth

        /// <summary>
        /// The width of the arrowhead.
        /// </summary>
		public static readonly DependencyProperty HeadWidthProperty
                             = DependencyProperty.Register("HeadWidth",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region HeadHeight

        /// <summary>
        /// The height of the arrowhead.
        /// </summary>
		public static readonly DependencyProperty HeadHeightProperty
                             = DependencyProperty.Register("HeadHeight",
                                                           typeof(Double),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Color

        /// <summary>
        /// The color of the arrow (fill and stroke).
        /// </summary>
        public static readonly DependencyProperty ColorProperty
                             = DependencyProperty.Register("Color",
                                                           typeof(Brush),
                                                           typeof(OldArrow),
                                                           new FrameworkPropertyMetadata(Brushes.Black,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        #region (protected) DefiningGeometry

        /// <summary>
        /// Create a StreamGeometry for describing the shape.
        /// </summary>
        protected override Geometry DefiningGeometry
		{
			get
			{

				var _StreamGeometry = new StreamGeometry() {
                    FillRule = FillRule.EvenOdd
                };

				using (var _StreamGeometryContext = _StreamGeometry.Open())
				{
                    DrawArrowGeometry(_StreamGeometryContext);
				}

				_StreamGeometry.Freeze();

				return _StreamGeometry;

			}
		}		

		#endregion

        #region (protected) DrawArrowGeometry(StreamGeometryContext)

        /// <summary>
        /// The actual method for drawing the arrow.
        /// </summary>
        /// <param name="StreamGeometryContext">Describes a geometry using drawing commands.</param>
		protected abstract void DrawArrowGeometry(StreamGeometryContext StreamGeometryContext);

        #endregion

    }

}
