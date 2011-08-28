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
using System.Windows.Controls;
using System.ComponentModel;
using System.Globalization;

#endregion

namespace de.ahzf.Loki
{

    /// <summary>
    /// A visual representation of a property graph element.
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
    public abstract class CommonControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> : UserControl

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

        #region Properties

        #region GraphCanvas

        /// <summary>
        /// The associated GraphCanvas.
        /// </summary>
        public GraphCanvas<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas { get; private set; }

        #endregion

        #region Color

        /// <summary>
        /// The color of the control (fill and stroke).
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

        #region CaptionXOffset

        /// <summary>
        /// The x-offset of the caption.
        /// </summary>
        [TypeConverter(typeof(DoubleConverter))]
        public Double CaptionXOffset
        {

            get
            {
                return (Double) base.GetValue(CaptionXOffsetProperty);
            }

            set
            {
                base.SetValue(CaptionXOffsetProperty, value);
            }

        }

        #endregion

        #region CaptionYOffset

        /// <summary>
        /// The y-offset of the caption.
        /// </summary>
        [TypeConverter(typeof(DoubleConverter))]
        public Double CaptionYOffset
        {

            get
            {
                return (Double) base.GetValue(CaptionYOffsetProperty);
            }

            set
            {
                base.SetValue(CaptionYOffsetProperty, value);
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
        

        #endregion

        #region Dependency Properties

        #region Color

        /// <summary>
        /// The color of the control (fill and stroke).
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

        #region CaptionXOffset

        /// <summary>
        /// The x-offset of the caption.
        /// </summary>
        public static readonly DependencyProperty CaptionXOffsetProperty
                             = DependencyProperty.Register("CaptionXOffsetProperty",
                                                           typeof(Double),
                                                           typeof(EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
                                                           new FrameworkPropertyMetadata(0.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region CaptionYOffset

        /// <summary>
        /// The y-offset of the caption.
        /// </summary>
        public static readonly DependencyProperty CaptionYOffsetProperty
                             = DependencyProperty.Register("CaptionYOffsetProperty",
                                                           typeof(Double),
                                                           typeof(EdgeControl<TIdVertex, TRevisionIdVertex, TVertexType, TKeyVertex, TValueVertex,
                                                                              TIdEdge, TRevisionIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
                                                           new FrameworkPropertyMetadata(18.0,
                                                                                         FrameworkPropertyMetadataOptions.AffectsRender |
                                                                                         FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #endregion


        #region Constructor(s)

        #region CommonControl(GraphCanvas)

        /// <summary>
        /// Create a new visual representation of a property graph element.
        /// </summary>
        /// <param name="GraphCanvas">The graph canvas hosting the edge control.</param>
        public CommonControl(GraphCanvas<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas)
        {

            this.GraphCanvas    = GraphCanvas;

            this.Typeface       = new Typeface("Verdana");
            this.FontSize       = 12;
            this.FontBrush      = Brushes.Black;
            this.TextAlignment  = TextAlignment.Center;
            this.TextTrimming   = TextTrimming.None;

        }

        #endregion

        #endregion


        #region RenderCaption(DrawingContext, X, Y, CaptionText)

        /// <summary>
        /// Render the caption of this control.
        /// </summary>
        /// <param name="DrawingContext">The drawing context.</param>
        /// <param name="X">The x-coordinate of the caption.</param>
        /// <param name="Y">The y-coordinate of the caption.</param>
        /// <param name="CaptionText">The text of the caption.</param>
        protected void RenderCaption(DrawingContext DrawingContext, Double X, Double Y, String CaptionText)
        {

            if (ShowCaption)
            {

                DrawingContext.DrawText(new FormattedText(CaptionText,
                                                          CultureInfo.CurrentCulture,
                                                          FlowDirection.LeftToRight,
                                                          Typeface,
                                                          FontSize,
                                                          FontBrush) { TextAlignment = TextAlignment.Center },
                                        new Point(X + CaptionXOffset, Y + CaptionYOffset));

            }

        }

        #endregion

    }

}
