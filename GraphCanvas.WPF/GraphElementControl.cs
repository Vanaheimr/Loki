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
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Globalization;

#endregion

namespace eu.Vanaheimr.Loki
{

    /// <summary>
    /// A visual representation of a property graph element.
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
    public abstract class GraphElementControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> : UserControl

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

        #region GraphCanvas

        /// <summary>
        /// The associated GraphCanvas.
        /// </summary>
        public GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas { get; private set; }

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
        public Double Caption_XOffset
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
        public Double Caption_YOffset
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


        public Typeface Caption_Typeface { get; set; }

        public Double Caption_FontSize { get; set; }

        public Brush Caption_FontBrush { get; set; }

        public TextAlignment Caption_TextAlignment { get; set; }

        public TextTrimming Caption_TextTrimming { get; set; }

        public double LineHeight { get; set; }

        public int MaxLineCount { get; set; }

        public double MaxTextHeight { get; set; }

        public double MaxTextWidth { get; set; }

        public FlowDirection Caption_FlowDirection { get; set; }

        public CultureInfo Caption_CultureInfo { get; set; }

        #endregion

        #region Dependency Properties

        #region Color

        /// <summary>
        /// The color of the control (fill and stroke).
        /// </summary>
        public static readonly DependencyProperty ColorProperty
                             = DependencyProperty.Register("Color",
                                                           typeof(Brush),
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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
                                                           typeof(EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>),
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

        /// <summary>
        /// Create a new visual representation of a property graph element.
        /// </summary>
        /// <param name="GraphCanvas">The graph canvas hosting the edge control.</param>
        public GraphElementControl(GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas)
        {

            this.GraphCanvas            = GraphCanvas;

            this.Caption_Typeface       = new Typeface("Verdana");
            this.Caption_FontSize       = 12;
            this.Caption_FontBrush      = Brushes.Black;
            this.Caption_TextAlignment  = TextAlignment.Center;
            this.Caption_TextTrimming   = TextTrimming.None;
            this.Caption_FlowDirection  = FlowDirection.LeftToRight;
            this.Caption_CultureInfo    = CultureInfo.CurrentCulture;

        }

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
                                                          Caption_CultureInfo,
                                                          Caption_FlowDirection,
                                                          Caption_Typeface,
                                                          Caption_FontSize,
                                                          Caption_FontBrush) { TextAlignment = Caption_TextAlignment },

                                        new Point(X + Caption_XOffset,
                                                  Y + Caption_YOffset - Caption_FontSize / 2)

                                       );

            }

        }

        #endregion

    }

}
