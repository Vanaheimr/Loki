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

    public class EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> : UserControl, IEdgeControl

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge           : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge            : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TVertexType             : IEquatable<TVertexType>,          IComparable<TVertexType>,          IComparable
        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel         : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge    : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    {

        #region Data

        private VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertexControl;

        private VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>  InVertexControl;

        #endregion

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

        private EdgeCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Caption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Caption
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

        public Typeface Typeface { get; set; }

        public Double FontSize { get; set; }

        public Brush FontBrush { get; set; }

        public TextAlignment TextAlignment { get; set; }

        public TextTrimming TextTrimming { get; set; }

        public double LineHeight { get; set; }
        
        public int MaxLineCount { get; set; }
        
        public double MaxTextHeight { get; set; }
        
        public double MaxTextWidth { get; set; }

        public GraphCanvas<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas { get; private set; }

        public IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge { get; private set; }

        #endregion

        #region Dependency Properties

        #region X1

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        public static readonly DependencyProperty X1Property
                             = DependencyProperty.Register("X1",
                                                           typeof(Double),
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
                                                           new FrameworkPropertyMetadata(true,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        public EdgeControl(GraphCanvas<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,
                           IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            this.GraphCanvas   = GraphCanvas;
            this.Edge          = Edge;
            this.DataContext   = Edge;

            OutVertexControl = Edge.OutVertex.GetProperty(GraphCanvas.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
             InVertexControl = Edge. InVertex.GetProperty(GraphCanvas.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this.X1 = Canvas.GetLeft(OutVertexControl);// + Vertex1.Width/2;
            this.Y1 = Canvas.GetTop (OutVertexControl);// + Vertex1.Height/2;
            this.X2 = Canvas.GetLeft (InVertexControl);// + Vertex2.Width/2;
            this.Y2 = Canvas.GetTop  (InVertexControl);// + Vertex2.Height/2;

            this.Typeface      = new Typeface("Verdana");
            this.FontSize      = 12;
            this.FontBrush     = Brushes.Black;
            this.TextAlignment = TextAlignment.Center;
            this.TextTrimming  = TextTrimming.None;

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
            var BlackPen    = new Pen(Brushes.Black, 2);

            var blueBlackLGB            = new LinearGradientBrush();
            blueBlackLGB.StartPoint     = new Point(0, 0);
            blueBlackLGB.EndPoint       = new Point(1, 1);

            var blueGS                  = new GradientStop();
            blueGS.Color                = Colors.Blue;
            blueGS.Offset               = 0.0;
            blueBlackLGB.GradientStops.Add(blueGS);

            var blackGS                 = new GradientStop();
            blackGS.Color               = Colors.Black;
            blackGS.Offset              = 1.0;
            blueBlackLGB.GradientStops.Add(blackGS);

            var blackBluePen = new Pen();
            blackBluePen.Thickness      = 5;
            blackBluePen.LineJoin       = PenLineJoin.Bevel;
            blackBluePen.StartLineCap   = PenLineCap.Triangle;
            blackBluePen.EndLineCap     = PenLineCap.Round;
            blackBluePen.Brush          = blueBlackLGB;

            var pt3 = new Point(X2 + (HeadWidth  * cost - HeadHeight * sint),
				                Y2 + (HeadWidth  * sint + HeadHeight * cost));

            var pt4 = new Point(X2 + (HeadWidth  * cost + HeadHeight * sint),
				                Y2 - (HeadHeight * cost - HeadWidth  * sint));

            DrawingContext.DrawLine(BlackPen, ArrowOrigin, ArrowTarget);
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

            DrawingContext.DrawGeometry(Brushes.Red, BlackPen, PathGeometry);


            if (ShowCaption && Caption != null)
            {

                DrawingContext.DrawText(new FormattedText(Caption(Edge),
                                                          CultureInfo.CurrentCulture,
                                                          FlowDirection.LeftToRight,
                                                          Typeface,
                                                          FontSize,
                                                          FontBrush) { TextAlignment = TextAlignment.Center },
                                        new Point(center.X, center.Y));

            }

        }

    }

}
