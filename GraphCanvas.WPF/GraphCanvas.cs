﻿/*
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.PropertyGraph.InMemory;
using System.Windows.Media.Imaging;
using System.IO;

#endregion

namespace de.ahzf.Loki
{

    #region Non-generic GraphCanvas

    /// <summary>
    /// Creates a new canvas for visualizing a non-generic property graph.
    /// </summary>
    public class GraphCanvas : GraphCanvas<UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object>
    {

        #region Constructor(s)

        #region GraphCanvas()

        /// <summary>
        /// Creates a new canvas for visualizing a non-generic property graph.
        /// </summary>
        public GraphCanvas()
            : base(new SimplePropertyGraph(), "GraphCanvas", "VertexShape", "EdgeShape")
        { }

        #endregion

        #endregion

    }

    #endregion

    #region Generic GraphCanvas

    /// <summary>
    /// Creates a new canvas for visualizing a generic property graph.
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
    public class GraphCanvas<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> : Canvas

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

        private Random  Random;
        private Point   Mousy;
        private VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SelectedVertexShape;
        private IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;
        private String CurrentDirectory;

        #endregion

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph { get; private set; }

        #endregion

        /// <summary>
        /// The property key for storing the graph canvas.
        /// </summary>
        public TKeyVertex GraphCanvasPropertyKey { get; private set; }

        /// <summary>
        /// The property key for storing the vertex shape.
        /// </summary>
        public TKeyVertex VertexShapePropertyKey { get; private set; }

        /// <summary>
        /// The property key for storing the edge shape.
        /// </summary>
        public TKeyEdge EdgeShapePropertyKey { get; private set; }


        #region VertexControlCreator

        private VertexControlCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given vertex.
        /// </summary>
        public VertexControlCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexControlCreator
        {
            
            get
            {
                return _VertexControlCreator;
            }

            set
            {
                if (value != null)
                    _VertexControlCreator = value;
            }

        }

        #endregion

        #region VertexCaption

        private VertexCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCaption;

        /// <summary>
        /// A delegate for generating caption for the given vertex.
        /// </summary>
        public VertexCaptionDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCaption
        {
            
            get
            {
                return _VertexCaption;
            }

            set
            {
                if (value != null)
                {
                    _VertexCaption = value;
                }
            }
        
        }

        #endregion

        #region VertexToolTip

        private VertexToolTipDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given vertex.
        /// </summary>
        public VertexToolTipDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexToolTip
        {
            
            get
            {
                return _VertexToolTip;
            }

            set
            {
                if (value != null)
                {

                    _VertexToolTip = value;

                    Shape  VertexShape;
                    TValueVertex VertexShapeProperty;
                    foreach (var Vertex in Graph.Vertices())
                    {
                        if (Vertex.GetProperty(this.VertexShapePropertyKey, out VertexShapeProperty))
                        {
                            
                            VertexShape = VertexShapeProperty as Shape;
                            
                            if (VertexShape != null)
                                VertexShape.ToolTip = VertexToolTip(Vertex);

                        }
                    }

                }
            }

        }

        #endregion


        #region EdgeControlCreator

        private EdgeControlCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given edge.
        /// </summary>
        public EdgeControlCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeControlCreator
        {

            get
            {
                return _EdgeControlCreator;
            }

            set
            {
                if (value != null)
                    _EdgeControlCreator = value;
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
                    _EdgeCaption = value;
            }
        
        }

        #endregion

        #region EdgeToolTip

        private EdgeToolTipDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given edge.
        /// </summary>
        public EdgeToolTipDelegate<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeToolTip
        {
            
            get
            {
                return _EdgeToolTip;
            }

            set
            {
                if (value != null)
                {

                    _EdgeToolTip = value;

                    Shape      EdgeShape;
                    TValueEdge EdgeShapeProperty;
                    foreach (var Edge in Graph.Edges())
                    {
                        if (Edge.GetProperty(this.EdgeShapePropertyKey, out EdgeShapeProperty))
                        {
                            
                            EdgeShape = EdgeShapeProperty as Shape;
                            
                            if (EdgeShape != null)
                                EdgeShape.ToolTip = EdgeToolTip(Edge);

                        }
                    }

                }
            }
        }

        #endregion

        #endregion

        #region Events

        #region NumberOfVertices

        /// <summary>
        /// Called whenever the number of vertices changed.
        /// </summary>
        public event ChangedNumberOfVertices OnChangedNumberOfVertices;

        #endregion

        #region NumberOfEdges

        /// <summary>
        /// Called whenever the number of edges changed.
        /// </summary>
        public event ChangedNumberOfEdges OnChangedNumberOfEdges;

        #endregion

        #region MousePosition

        /// <summary>
        /// Called whenever the mouse moved.
        /// </summary>
        public event ChangedMousePosition OnChangedMousePosition;

        #endregion

        #endregion

        #region Constructor(s)

        #region GraphCanvas(IPropertyGraph)

        /// <summary>
        /// Creates a new canvas for visualizing the given property graph.
        /// </summary>
        public GraphCanvas(IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyGraph,
            TKeyVertex GraphCanvasPropertyKey,
            TKeyVertex VertexShapePropertyKey,
            TKeyEdge   EdgeShapePropertyKey)
        {

            this.Graph = IPropertyGraph;
            this.GraphCanvasPropertyKey = GraphCanvasPropertyKey;
            this.VertexShapePropertyKey = VertexShapePropertyKey;
            this.EdgeShapePropertyKey   = EdgeShapePropertyKey;
            Graph.SetProperty(GraphCanvasPropertyKey, (TValueVertex) (Object) this);
            DataContext             = Graph;
            Random                  = new Random();

            this.Background         = new SolidColorBrush(Colors.Transparent);
            this.MouseMove         += GraphCanvas_MouseMove;
            this.MouseLeave        += GraphCanvas_MouseLeave;
            Graph.OnVertexAdded    += AddVertex;
            Graph.OnEdgeAdded      += AddEdge;

            _VertexControlCreator   = DefaultVertexControlCreator;
            _VertexCaption          = DefaultVertexCaption;
            _VertexToolTip          = DefaultVertexToolTip;

            _EdgeControlCreator     = DefaultEdgeControlCreator;
            _EdgeCaption            = DefaultEdgeCaption;
            _EdgeToolTip            = DefaultEdgeToolTip;

            CurrentDirectory        = Directory.GetCurrentDirectory();

            AddGraphCanvasContextMenu();

        }

        #endregion

        #endregion


        // Graph canvas

        #region GraphCanvas_MouseMove(Sender, MouseEventArgs)

        private void GraphCanvas_MouseMove(Object sender, MouseEventArgs MouseEventArgs)
        {

            var MMMousy = MouseEventArgs.GetPosition(this);

            if (OnChangedMousePosition != null)
                OnChangedMousePosition(MMMousy.X, MMMousy.Y);

            if (SelectedVertexShape != null)
            {

                var mousePos = MouseEventArgs.GetPosition(this);
                var diffX    = Mousy.X - mousePos.X;
                var diffY    = Mousy.Y - mousePos.Y;

                var canvLeft = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.LeftProperty));
                var canvTop  = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.TopProperty));

                Canvas.SetLeft(SelectedVertexShape, canvLeft - diffX);
                Canvas.SetTop (SelectedVertexShape, canvTop  - diffY);

                foreach (var outedge in Vertex.OutEdges())
                {
                    var EdgeLine = outedge.GetProperty(this.EdgeShapePropertyKey) as EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
                    EdgeLine.X1 -= diffX;
                    EdgeLine.Y1 -= diffY;
                }

                foreach (var inedge in Vertex.InEdges())
                {
                    var EdgeLine = inedge.GetProperty(this.EdgeShapePropertyKey) as EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
                    EdgeLine.X2 -= diffX;
                    EdgeLine.Y2 -= diffY;
                }

                Mousy = MouseEventArgs.GetPosition(this);

            }

        }

        #endregion

        #region GraphCanvas_MouseLeave(Sender, MouseEventArgs)

        private void GraphCanvas_MouseLeave(Object Sender, MouseEventArgs MouseEventArgs)
        {
            Mousy               = MouseEventArgs.GetPosition(this);
            SelectedVertexShape = null;
        }

        #endregion


        // Vertices

        #region (private) AddVertex(Graph, Vertex)

        private void AddVertex(IPropertyGraph <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,
                               IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            if (Vertex != null)
            {

                var VertexControl                  = _VertexControlCreator(this, Vertex);
                VertexControl.MouseMove           += VertexControl_MouseMove;
                VertexControl.MouseLeftButtonDown += VertexControl_MouseLeftButtonDown;
                VertexControl.MouseLeftButtonUp   += VertexControl_MouseLeftButtonUp;
                VertexControl.DataContext          = Vertex;
                Vertex.SetProperty(this.VertexShapePropertyKey, (TValueVertex) (Object) VertexControl);

                VertexControl.Caption              = _VertexCaption;
                VertexControl.ToolTip              = VertexToolTip(Vertex);

                if (OnChangedNumberOfVertices != null)
                    OnChangedNumberOfVertices(Graph.NumberOfVertices());

                Children.Add(VertexControl);
                Canvas.SetLeft(VertexControl, Random.Next(20, 400 - 20));
                Canvas.SetTop (VertexControl, Random.Next(20, 200 - 20));

            }

        }

        #endregion

        #region (static)  DefaultVertexControlCreator(Vertex)

        /// <summary>
        /// Returns the default control for the given vertex,
        /// which is a constant sized circle.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                                    DefaultVertexControlCreator(GraphCanvas    <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,
                                                                IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            var VertexControl             = new VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(GraphCanvas, Vertex);
            VertexControl.Fill            = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));
            VertexControl.Stroke          = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            VertexControl.Width           = 30;
            VertexControl.Height          = 30;
            VertexControl.ShowCaption     = true;

            return VertexControl;

        }

        #endregion

        #region (static)  DefaultVertexCaption(Vertex)

        /// <summary>
        /// Returns the default caption for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexCaption(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {
            return Vertex.Id.ToString();
        }

        #endregion

        #region (static)  DefaultVertexToolTip(Vertex)

        /// <summary>
        /// Returns the default tooltip for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexToolTip(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {
            return "VertexId: " + Vertex.Id + " [" + Vertex.OutDegree() + " OutEdges, " + Vertex.InDegree() + " InEdges]";
        }

        #endregion

        #region (private) VertexControl_MouseLeftButtonDown(Sender, MouseButtonEventArgs)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="MouseButtonEventArgs"></param>
        private void VertexControl_MouseLeftButtonDown(Object Sender, MouseButtonEventArgs MouseButtonEventArgs)
        {

            Mousy               = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexShape = Sender as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
            Vertex              = SelectedVertexShape.DataContext as IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

        }

        #endregion

        #region (private) VertexControl_MouseLeftButtonUp(Sender, MouseButtonEventArgs)

        private void VertexControl_MouseLeftButtonUp(Object sender, MouseButtonEventArgs MouseButtonEventArgs)
        {
            Mousy               = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexShape = null;
        }

        #endregion

        #region (private) VertexControl_MouseMove(Sender, MouseEventArgs)

        private void VertexControl_MouseMove(Object Sender, MouseEventArgs MouseEventArgs)
        {
            GraphCanvas_MouseMove(Sender, MouseEventArgs);
        }

        #endregion


        // Edges

        #region (private) AddEdge(Graph, Edge)

        private void AddEdge(IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,
                             IPropertyEdge <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (Edge != null)
            {

                var EdgeControl             = _EdgeControlCreator(this, Edge);
                EdgeControl.Caption         = _EdgeCaption;
                EdgeControl.ToolTip         = EdgeToolTip(Edge);

                Canvas.SetZIndex(EdgeControl, -99);
                Children.Add(EdgeControl);

                Edge.SetProperty(this.EdgeShapePropertyKey, (TValueEdge) (Object)  EdgeControl);


                var OutVertexControl = Edge.OutVertex.GetProperty(this.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
                var InVertexControl  = Edge.InVertex. GetProperty(this.VertexShapePropertyKey) as VertexControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

                OutVertexControl.ToolTip    = DefaultVertexToolTip(Edge.OutVertex);
                 InVertexControl.ToolTip    = DefaultVertexToolTip(Edge. InVertex);

                if (OnChangedNumberOfEdges != null)
                    OnChangedNumberOfEdges(Graph.NumberOfEdges());

            }

        }

        #endregion

        #region (static)  DefaultEdgeControlCreator(Vertex)

        /// <summary>
        /// Returns the default control for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                      DefaultEdgeControlCreator(GraphCanvas  <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,
                                                IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            var EdgeControl                 = new EdgeControl<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(GraphCanvas, Edge);
            //VertexShape.Stroke              = new SolidColorBrush(Colors.Black);
            //VertexShape.StrokeThickness     = 1;
            EdgeControl.HeadWidth           = 12;
            EdgeControl.HeadHeight          = 8;
            //EdgeShape.Stroke                = new SolidColorBrush(Colors.Black);
            //EdgeShape.StrokeThickness       = 2;
            EdgeControl.ShowCaption         = true;
            //VertexShape.Fill                = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));

            return EdgeControl;

        }

        #endregion

        


        #region (static)  DefaultEdgeCaption(Edge)

        /// <summary>
        /// Returns the default caption for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static String DefaultEdgeCaption(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {
            return Edge.Label.ToString();
        }

        #endregion

        #region (static)  DefaultEdgeToolTip(Edge)

        /// <summary>
        /// Returns the default tooltip for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static String DefaultEdgeToolTip(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {
            return "EdgeId: " + Edge.Id + " [OutVertexId: " + Edge.OutVertex.Id.ToString() + ", InVertexId: " + Edge.InVertex.Id.ToString() + "]";
        }

        #endregion


        private void AddGraphCanvasContextMenu()
        {

            // Must be here... do not why!
            this.ContextMenu = new ContextMenu();

            var ClearGraph = new MenuItem()
            {
                Header = "Clear graph"
            };
            ClearGraph.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(ClearGraph);

            var LoadGraph = new MenuItem()
            {
                Header = "Load graph..."
            };
            LoadGraph.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(LoadGraph);

            var SaveGraphAs = new MenuItem()
            {
                Header = "Save graph as..."
            };
            SaveGraphAs.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(SaveGraphAs);

        }


        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

          //  MessageBox.Show("Size: " + GraphCanvas.Width + " x " + GraphCanvas.Height);

            var SaveFileDialog              = new Microsoft.Win32.SaveFileDialog();
            SaveFileDialog.Filter           = "All files (*.*)|*.*|PNG files (*.png)|*.png*|JPEG files (*.jpg, *.jpeg)|*.jpg*;*.jpeg|XAML files (*.xaml)|*.xaml*";
            SaveFileDialog.FilterIndex      = 0;
            SaveFileDialog.AddExtension     = true;
            SaveFileDialog.InitialDirectory = CurrentDirectory;
            SaveFileDialog.Title            = "Choose a filename and a location...";
            SaveFileDialog.CheckPathExists  = true;

            var _Dialog = SaveFileDialog.ShowDialog();
            if (_Dialog.HasValue && _Dialog.Value)
            {
                try
                {

                    CurrentDirectory = SaveFileDialog.FileName.Substring(0, SaveFileDialog.FileName.LastIndexOf(System.IO.Path.DirectorySeparatorChar));

                    switch (SaveFileDialog.FilterIndex)
                    {

                        case 2:
                            if (!SaveFileDialog.FileName.EndsWith(".png"))
                                SaveFileDialog.FileName += ".png";
                            break;

                        case 3:
                            if (!(SaveFileDialog.FileName.EndsWith(".jpg") ||
                                  SaveFileDialog.FileName.EndsWith(".jpeg")))
                                SaveFileDialog.FileName += ".jpg";
                            break;

                        case 4:
                            if (!SaveFileDialog.FileName.EndsWith(".xaml"))
                                SaveFileDialog.FileName += ".xaml";
                            break;

                        default:
                            if (SaveFileDialog.FileName.EndsWith(".png"))
                                SaveFileDialog.FilterIndex = 2;
                            else if (SaveFileDialog.FileName.EndsWith(".jpg"))
                                SaveFileDialog.FilterIndex = 3;
                            else if (SaveFileDialog.FileName.EndsWith(".jpeg"))
                                SaveFileDialog.FilterIndex = 3;
                            else if (SaveFileDialog.FileName.EndsWith(".xaml"))
                                SaveFileDialog.FilterIndex = 4;
                            else
                            {
                                MessageBox.Show("A problem occured, try again later!");
                                return;
                            }
                            break;

                    }

                    using (var _FileStream = File.Create(SaveFileDialog.FileName))
                    {
                        switch (SaveFileDialog.FilterIndex)
                        {

                            case 1: break;
                            case 2: this.SaveAsPNG (                  dpiX: 300, dpiY: 300).WriteTo(_FileStream); break;
                            case 3: this.SaveAsJPEG(QualityLevel: 98, dpiX: 300, dpiY: 300).WriteTo(_FileStream); break;
                            case 4: this.SaveAsXAML(Indent: true); break;

                            default:
                                MessageBox.Show("Error occurred during XAML saving.",
                                                "Error",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                                break;

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Cancel!", "Error", MessageBoxButton.OK, MessageBoxImage .Error);
            }

        }

    }

    #endregion

}