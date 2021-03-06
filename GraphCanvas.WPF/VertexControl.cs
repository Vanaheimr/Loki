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
using System.Windows.Media;
using System.ComponentModel;

using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Loki
{

    /// <summary>
    /// A visual representation of a property vertex.
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
    public class VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

        #region Properties

        #region Vertex

        /// <summary>
        /// The associated property vertex.
        /// </summary>
        public IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex { get; private set; }

        #endregion

        #region VertexBorder

        public VertexBorder VertexBorder { get; set; }

        #endregion

        #region X

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public Double X
        {

            get
            {
                return (Double) base.GetValue(XProperty);
            }

            set
            {
                base.SetValue(XProperty, value);
               // Canvas.SetLeft(this, value);
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
                return (Double) base.GetValue(YProperty);
            }

            set
            {
                base.SetValue(YProperty, value);
          //      Canvas.SetTop(this, value);
            }

        }

        #endregion

        #region VertexCaption

        private VertexCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCaption;

        /// <summary>
        /// A delegate for generating caption for the given vertex.
        /// </summary>
        public VertexCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCaption
        {

            get
            {
                return _VertexCaption;
            }

            set
            {
                if (value != null)
                    _VertexCaption = value;
            }

        }

        #endregion

        #region DrawingContextDelegate

        /// <summary>
        /// A delegate creating a visual for this control.
        /// </summary>
        public Action<VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                                    DrawingContext>

            DrawingContextDelegate { get; set; }

        #endregion

        #endregion

        #region Dependency Properties

        #region X

        /// <summary>
        /// The x-coordinate of the arrow origin.
        /// </summary>
        public static readonly DependencyProperty XProperty
                             = DependencyProperty.Register("X",
                                                           typeof(Double),
                                                           typeof(VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        #region Constructor(s)

        #region VertexControl(GraphCanvas, Vertex, DrawingContextDelegate = null)

        /// <summary>
        /// Create a new visual representation of a property vertex.
        /// </summary>
        /// <param name="GraphCanvas">The graph canvas hosting the edge control.</param>
        /// <param name="Vertex">The associated property vertex.</param>
        /// <param name="DrawingContextDelegate">An optional delegate creating a visual for this control.</param>
        public VertexControl(GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,

                             IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,

                             Action<VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                                    DrawingContext>
                                 DrawingContextDelegate = null)

            : base(GraphCanvas)

        {

            this.Vertex                  = Vertex;
            this.DataContext             = Vertex;
            this.Fill                    = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xCC, 0xff, 0x00, 0x00));
            this.Stroke                  = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            this.SnapsToDevicePixels     = true;
            this.VertexBorder            = new VertexBorder(2, 2, VertexBorderShape.Circle);

            this.DrawingContextDelegate  = (DrawingContextDelegate != null)
                                           ? DrawingContextDelegate
                                           : (VertexControl, DrawingContext) =>
                                                DrawingContext.DrawEllipse(VertexControl.Fill,
                                                                           VertexControl.Stroke,
                                                                           new Point(VertexControl.X, VertexControl.Y),
                                                                           VertexControl.Width  / 2,
                                                                           VertexControl.Height / 2);

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

            DrawingContextDelegate(this, DrawingContext);

            if (VertexCaption != null)
                RenderCaption(DrawingContext, X, Y, VertexCaption(Vertex));

        }

        #endregion

    }

}
