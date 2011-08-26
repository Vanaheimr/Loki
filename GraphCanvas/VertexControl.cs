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

    public class VertexControl : UserControl, IVertexControl
    {

        #region Properties

        #region X

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public Double X
        {

            get
            {
                return (Double)base.GetValue(XProperty);
            }

            set
            {
                base.SetValue(XProperty, value);
            }

        }

        #endregion

        #region Y

        /// <summary>
        /// The y-coordinate of the arrow origin.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public Double Y
        {

            get
            {
                return (Double)base.GetValue(YProperty);
            }

            set
            {
                base.SetValue(YProperty, value);
            }

        }

        #endregion

        #region ShowDirection

        /// <summary>
        /// Show or hide the direction of an edge.
        /// </summary>
        [TypeConverter(typeof(BooleanConverter))]
        public Boolean ShowDirection
        {

            get
            {
                return (Boolean) base.GetValue(ShowDirectionProperty);
            }

            set
            {
                base.SetValue(ShowDirectionProperty, value);
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

        //#region Color

        ///// <summary>
        ///// The color of the arrow (fill and stroke).
        ///// </summary>
        //[TypeConverter(typeof(BrushConverter))]
        //public Brush Color
        //{

        //    get
        //    {
        //        return (Brush) base.GetValue(ColorProperty);
        //    }

        //    set
        //    {
        //        base.SetValue(ColorProperty, value);
        //        //this.Stroke = value;
        //        //this.Fill = value;
        //    }

        //}

        //#endregion

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

        #region Caption

        private VertexCaptionDelegate _Caption;

        /// <summary>
        /// A delegate for generating caption for the given vertex.
        /// </summary>
        public VertexCaptionDelegate Caption
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

        public Brush Fill { get; set; }
        public Pen Stroke { get; set; }

        public Typeface Typeface { get; set; }

        public Double FontSize { get; set; }

        public Brush FontBrush { get; set; }

        public TextAlignment TextAlignment { get; set; }

        public TextTrimming TextTrimming { get; set; }

        public double LineHeight { get; set; }
        
        public int MaxLineCount { get; set; }
        
        public double MaxTextHeight { get; set; }
        
        public double MaxTextWidth { get; set; }

        public IPropertyVertex<UInt64, Int64, String, String, Object,
                               UInt64, Int64, String, String, Object,
                               UInt64, Int64, String, String, Object,
                               UInt64, Int64, String, String, Object> Vertex
        { get; private set; }

        #endregion

        #region Dependency Properties

        #region X

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        public static readonly DependencyProperty XProperty
                             = DependencyProperty.Register("X",
                                                           typeof(Double),
                                                           typeof(VertexControl),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region Y

        /// <summary>
        /// The y-coordinate of the arrow origin.
        /// </summary>
        public static readonly DependencyProperty YProperty
                             = DependencyProperty.Register("Y",
                                                           typeof(Double),
                                                           typeof(VertexControl),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region ShowDirection

        /// <summary>
        /// Show or hide hide the direction of an edge.
        /// </summary>
        public static readonly DependencyProperty ShowDirectionProperty
                             = DependencyProperty.Register("ShowDirectionProperty",
                                                           typeof(Boolean),
                                                           typeof(VertexControl),
                                                           new FrameworkPropertyMetadata(true,
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
                                                           typeof(VertexControl),
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
                                                           typeof(VertexControl),
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
                                                           typeof(VertexControl),
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
                                                           typeof(VertexControl),
                                                           new FrameworkPropertyMetadata(true,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        public VertexControl(IPropertyVertex<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object> Vertex)
        {

            this.Vertex        = Vertex;
            this.DataContext   = Vertex;

            this.Fill          = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));
            this.Stroke        = new Pen(new SolidColorBrush(Colors.Black), 1.0);

            this.Typeface      = new Typeface("Verdana");
            this.FontSize      = 12;
            this.FontBrush     = Brushes.Black;
            this.TextAlignment = TextAlignment.Center;
            this.TextTrimming  = TextTrimming.None;

        }



        protected override void OnRender(DrawingContext DrawingContext)
        {
            
            base.OnRender(DrawingContext);

            DrawingContext.DrawEllipse(Fill,
                                       Stroke,
                                       new Point(X, Y),
                                       Vertex.Id * 10,
                                       Vertex.Id * 10);

            if (ShowCaption && Caption != null)
            {

                DrawingContext.DrawText(new FormattedText(Caption(Vertex),
                                                          CultureInfo.CurrentCulture,
                                                          FlowDirection.LeftToRight,
                                                          Typeface,
                                                          FontSize,
                                                          FontBrush) { TextAlignment = TextAlignment.Center },
                                        new Point(X, Y));

            }

        }


        public VertexBounding VertexBounding
        {
            get
            {
                return VertexBounding.Circle;
            }
            set
            {
                
            }
        }
    }

}
