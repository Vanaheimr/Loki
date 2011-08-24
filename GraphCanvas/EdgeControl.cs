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
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Shapes;
using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraph;

#endregion

namespace de.ahzf.Loki
{

    public class EdgeControl : UserControl, IEdgeControl
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
                return (Double)base.GetValue(X1Property);
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
                return (Double)base.GetValue(Y1Property);
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
                return (Double)base.GetValue(X2Property);
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
                return (Double)base.GetValue(Y2Property);
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
                //this.Stroke = value;
                //this.Fill = value;
            }

        }

        #endregion

        #region ShowCaption

        /// <summary>
        /// Show or hide the edge caption.
        /// </summary>
        [TypeConverter(typeof(BooleanConverter))]
        public Boolean ShowCaption
        {

            get
            {
                return (Boolean) base.GetValue(ShowCaptionProperty);
            }

            set
            {
                base.SetValue(ShowCaptionProperty, value);
            }

        }

        #endregion

        #region EdgeCaption

        private EdgeCaptionDelegate _Caption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate Caption
        {

            get
            {
                return _Caption;
            }

            set
            {
                if (value != null)
                {
                    _Caption = value;
                }
            }
        }

        #endregion

        public IPropertyEdge<UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object> Edge
        { get; private set; }

        #endregion

        #region Dependency Properties

        #region X1

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        public static readonly DependencyProperty X1Property
                             = DependencyProperty.Register("X1",
                                                           typeof(Double),
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
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
                                                           typeof(EdgeControl),
                                                           new FrameworkPropertyMetadata(Brushes.Black,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region ShowCaption

        /// <summary>
        /// Show or hide the edge caption.
        /// </summary>
        public static readonly DependencyProperty ShowCaptionProperty
                             = DependencyProperty.Register("ShowCaptionProperty",
                                                           typeof(Boolean),
                                                           typeof(EdgeControl),
                                                           new FrameworkPropertyMetadata(true,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        public EdgeControl(IPropertyEdge<UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object,
                             UInt64, Int64, String, String, Object> Edge)
        {
            this.Edge        = Edge;
            this.DataContext = Edge;
        }



        protected override void OnRender(DrawingContext DrawingContext)
        {
            
            base.OnRender(DrawingContext);

            var line = new Line2D<Double>(X1, Y1, this.X2, this.Y2);
            var center = line.Center;

            var theta = Math.Atan2(Y1 - this.Y2, X1 - this.X2);
			var sint  = Math.Sin(theta);
			var cost  = Math.Cos(theta);

            var X2 = X1 - (line.Length - 17) * cost;
            var Y2 = Y1 - (line.Length - 17) * sint;

			var ArrowOrigin = new Point(X1, Y1);
			var ArrowTarget = new Point(X2, Y2);
            var DrawingPen  = new Pen(Brushes.Black, 1);

            var pt3 = new Point(X2 + (HeadWidth  * cost - HeadHeight * sint),
				                Y2 + (HeadWidth  * sint + HeadHeight * cost));

            var pt4 = new Point(X2 + (HeadWidth  * cost + HeadHeight * sint),
				                Y2 - (HeadHeight * cost - HeadWidth  * sint));

            DrawingContext.DrawLine(DrawingPen, ArrowOrigin, ArrowTarget);
            //DrawingContext.DrawLine(DrawingPen, pt3, ArrowTarget);
            //DrawingContext.DrawLine(DrawingPen, pt4, ArrowTarget);

            var PathFigure = new PathFigure();
            PathFigure.StartPoint = ArrowTarget;

            var PathSegmentCollection = new PathSegmentCollection();
            PathSegmentCollection.Add(new LineSegment(pt3, true));
            PathSegmentCollection.Add(new LineSegment(pt4, true));
            PathSegmentCollection.Add(new LineSegment(ArrowTarget, true));

            PathFigure.Segments = PathSegmentCollection;
            var PathGeometry = new PathGeometry();
            PathGeometry.Figures.Add(PathFigure);

            DrawingContext.DrawGeometry(Brushes.Red, DrawingPen, PathGeometry);


            if (ShowCaption)
            {

                var formattedText = new FormattedText(
                                        Caption(Edge),
                                        CultureInfo.GetCultureInfo("en-us"),
                                        FlowDirection.LeftToRight,
                                        new Typeface("Verdana"),
                                        12,
                                        Brushes.Black);

                DrawingContext.DrawText(formattedText, new Point(center.X, center.Y));

            }

        }


        public Boolean ShowDirection  { get; set; }

    }

}
