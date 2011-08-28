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
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Controls;

using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraph;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// A visual representation of a property edge.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexType">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public class EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> :

                             CommonControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

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

        #region Edge

        /// <summary>
        /// The associated property edge.
        /// </summary>
        public IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge { get; private set; }

        #endregion

        #region X1

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        public Double X1
        {
            get
            {
                return OutVertexControl.X;
            }
        }

        #endregion

        #region Y1

        /// <summary>
        /// The y-coordinate of the arrow origin.
        /// </summary>
        public Double Y1
        {
            get
            {
                return OutVertexControl.Y;
            }
        }

        #endregion

        #region X2

        /// <summary>
        /// The x-coordinate of the arrow target.
        /// </summary>
        public Double X2
        {
            get
            {
                return InVertexControl.X;
            }
        }

        #endregion

        #region Y2

        /// <summary>
        /// The y-coordinate of the arrow target.
        /// </summary>
        public Double Y2
        {
            get
            {
                return InVertexControl.Y;
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

        #region EdgeCaption

        private EdgeCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCaption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCaption
        {

            get
            {
                return _EdgeCaption;
            }

            set
            {
                if (value != null)
                {
                    _EdgeCaption = value;
                }
            }

        }

        #endregion

        #endregion

        #region Dependency Properties

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

        #endregion


        #region Constructor(s)

        #region EdgeControl(GraphCanvas, Edge)

        /// <summary>
        /// Create a new visual representation of a property edge.
        /// </summary>
        /// <param name="GraphCanvas">The graph canvas hosting the edge control.</param>
        /// <param name="Edge">The associated property edge.</param>
        public EdgeControl(GraphCanvas  <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,
                           IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

            : base(GraphCanvas)

        {

            this.Edge           = Edge;
            this.DataContext    = Edge;

            OutVertexControl    = Edge.OutVertex.GetProperty(GraphCanvas.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            InVertexControl     = Edge. InVertex.GetProperty(GraphCanvas.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this.CaptionYOffset = 10.0;

        }

        #endregion

        #endregion


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

            if (EdgeCaption != null)
                RenderCaption(DrawingContext, center.X, center.Y, EdgeCaption(Edge));

        }

    }

}
