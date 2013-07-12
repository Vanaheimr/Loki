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

using eu.Vanaheimr.Illias.Geometry;
using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Loki
{

    /// <summary>
    /// A visual representation of a property edge.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public class EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> :

                             GraphElementControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        private readonly VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertexControl;

        private readonly VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertexControl;

        #endregion

        #region Properties

        #region Edge

        /// <summary>
        /// The associated property edge.
        /// </summary>
        public IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge { get; private set; }

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

        #region Refresh

        /// <summary>
        /// Repaint the edge control.
        /// </summary>
        public Boolean Refresh
        {

            get
            {
                return (Boolean) GetValue(RefreshProperty);
            }

            set
            {
                SetValue(RefreshProperty, value);
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

        private EdgeCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCaption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCaption
        {

            get
            {
                return _EdgeCaption;
            }

            set
            {
                if (value != null)
                    _EdgeCaption = value;
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
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
        public EdgeControl(GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,

                           IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

            : base(GraphCanvas)

        {

            this.Edge                   = Edge;
            this.DataContext            = Edge;
            this.SnapsToDevicePixels    = true;

            this.OutVertexControl       = (VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>)
                                          (Object)
                                           Edge.OutVertex[GraphCanvas.VertexControl_PropertyKey];

            this.InVertexControl        = (VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>)
                                          (Object)
                                           Edge.InVertex[GraphCanvas.VertexControl_PropertyKey];

        }

        #endregion

        #endregion


        #region (protected) OnRender(DrawingContext)

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations
        /// that are directed by the layout system. The rendering instructions for this
        /// element are not used directly when this method is invoked, and are instead
        /// preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="DrawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext DrawingContext)
        {

            base.OnRender(DrawingContext);

            var EdgeLine                = new Line2D<Double>(this.X1, this.Y1, this.X2, this.Y2);

            var theta                   = Math.Atan2(Y1 - this.Y2, X1 - this.X2);
            var sint                    = Math.Sin(theta);
            var cost                    = Math.Cos(theta);

            var EdgeOrigin              = new Point(X1 - (OutVertexControl.Width  / 2 + OutVertexControl.VertexBorder.Width ) * cost,
                                                    Y1 - (OutVertexControl.Height / 2 + OutVertexControl.VertexBorder.Height) * sint);

            var EdgeTarget              = new Point(X1 - (EdgeLine.Length - InVertexControl.Width  / 2 - InVertexControl.VertexBorder.Width ) * cost,
                                                    Y1 - (EdgeLine.Length - InVertexControl.Height / 2 - InVertexControl.VertexBorder.Height) * sint);


            // Colors
            var BlackPen                = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xAA, 0x00, 0x00, 0x00)), 1);
            var DrawingPen              = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xAA, 0x00, 0xff, 0x00)), 1);

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



            // The Edge
            DrawingContext.DrawLine(BlackPen, EdgeOrigin, EdgeTarget);



            var pt3 = new Point(EdgeTarget.X + (HeadWidth  * cost - HeadHeight * sint),
                                EdgeTarget.Y + (HeadWidth  * sint + HeadHeight * cost));

            var pt4 = new Point(EdgeTarget.X + (HeadWidth  * cost + HeadHeight * sint),
                                EdgeTarget.Y - (HeadHeight * cost - HeadWidth * sint));


            // SimpleArrow
            //DrawingContext.DrawLine(BlackPen, pt3, EdgeTarget);
            //DrawingContext.DrawLine(BlackPen, pt4, EdgeTarget);


            // SolidArrow
            var HeadArrow_PathFigure = new PathFigure() { StartPoint = EdgeTarget };

            var HeadArrow_PathSegments = new PathSegmentCollection();
            HeadArrow_PathSegments.Add(new LineSegment(pt3, true));
            HeadArrow_PathSegments.Add(new LineSegment(pt4, true));
            HeadArrow_PathSegments.Add(new LineSegment(EdgeTarget, true));

            HeadArrow_PathFigure.Segments = HeadArrow_PathSegments;

            var PathGeometry = new PathGeometry();
            PathGeometry.Figures.Add(HeadArrow_PathFigure);

            DrawingContext.DrawGeometry(Brushes.Red, BlackPen, PathGeometry);



            if (EdgeCaption != null)
            {
                var BaseLineCenter = EdgeLine.Center;
                RenderCaption(DrawingContext, BaseLineCenter.X, BaseLineCenter.Y , EdgeCaption(Edge));
            }

        }

        #endregion

    }

}
