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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.PropertyGraph.InMemory;

#endregion

namespace de.ahzf.Loki.Silverlight
{

    //ToolTipService.SetToolTip(OutVertexControl, DefaultVertexToolTip(Edge.OutVertex));
    //ToolTipService.SetToolTip(InVertexControl,  DefaultVertexToolTip(Edge.InVertex));

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

        private Random Random;
        private Point Mousy;
        private Shape SelectedVertexShape;
        private IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;

        #endregion

        #region Delegates

        #region Vertices

        /// <summary>
        /// A delegate for creating a shape for the given vertex.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public delegate Shape VertexShapeCreatorDelegate(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex);

        /// <summary>
        /// A delegate for generating a tooltip for the given vertex.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public delegate String VertexToolTipDelegate(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex);

        /// <summary>
        /// The current number of vertices.
        /// </summary>
        /// <param name="NumberOfVertices">The current number of vertices</param>
        public delegate void ChangedNumberOfVertices(UInt64 NumberOfVertices);

        #endregion

        #region Edges

        /// <summary>
        /// A delegate for creating a shape for the given edge.
        /// </summary>
        /// <param name="Edge">A proeprty edge</param>
        public delegate Shape EdgeShapeCreatorDelegate(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        /// <summary>
        /// A delegate for generating a tooltip for the given edge.
        /// </summary>
        /// <param name="Edge">A proeprty edge</param>
        public delegate String EdgeToolTipDelegate(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        /// <summary>
        /// The current number of edges.
        /// </summary>
        /// <param name="NumberOfEdges">The current number of edges</param>
        public delegate void ChangedNumberOfEdges(UInt64 NumberOfEdges);

        #endregion

        /// <summary>
        /// The current mouse position.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public delegate void ChangedMousePosition(Double X, Double Y);

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


        #region VertexShapeCreator

        private VertexShapeCreatorDelegate _VertexShapeCreator;

        /// <summary>
        /// A delegate for creating a shape for the given vertex.
        /// </summary>
        public VertexShapeCreatorDelegate VertexShapeCreator
        {

            get
            {
                return _VertexShapeCreator;
            }

            set
            {
                if (value != null)
                    _VertexShapeCreator = value;
            }

        }

        #endregion

        #region VertexToolTip

        private VertexToolTipDelegate _VertexToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given vertex.
        /// </summary>
        public VertexToolTipDelegate VertexToolTip
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

                    Shape VertexShape;
                    TValueVertex VertexShapeProperty;
                    foreach (var Vertex in Graph.Vertices())
                    {
                        if (Vertex.GetProperty(VertexShapePropertyKey, out VertexShapeProperty))
                        {

                            VertexShape = VertexShapeProperty as Shape;

                            if (VertexShape != null)
                                ToolTipService.SetToolTip(VertexShape, VertexToolTip(Vertex));

                        }
                    }

                }
            }
        }

        #endregion


        #region EdgeShapeCreator

        private EdgeShapeCreatorDelegate _EdgeShapeCreator;

        /// <summary>
        /// A delegate for creating a shape for the given edge.
        /// </summary>
        public EdgeShapeCreatorDelegate EdgeShapeCreator
        {

            get
            {
                return _EdgeShapeCreator;
            }

            set
            {
                if (value != null)
                    _EdgeShapeCreator = value;
            }

        }

        #endregion

        #region EdgeToolTip

        private EdgeToolTipDelegate _EdgeToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given edge.
        /// </summary>
        public EdgeToolTipDelegate EdgeToolTip
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

                    Shape EdgeShape;
                    TValueEdge EdgeShapeProperty;
                    foreach (var Edge in Graph.Edges())
                    {
                        if (Edge.GetProperty(EdgeShapePropertyKey, out EdgeShapeProperty))
                        {

                            EdgeShape = EdgeShapeProperty as Shape;

                            if (EdgeShape != null)
                                ToolTipService.SetToolTip(EdgeShape, EdgeToolTip(Edge));

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
            DataContext = Graph;
            Random = new Random();

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.MouseMove += GraphCanvas_MouseMove;
            this.MouseLeave += GraphCanvas_MouseLeave;
            Graph.OnVertexAdded += AddVertex;
            Graph.OnEdgeAdded += AddEdge;

            _VertexShapeCreator = DefaultVertexShape;
            _VertexToolTip = DefaultVertexToolTip;
            _EdgeToolTip = DefaultEdgeToolTip;

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
                var diffX = Mousy.X - mousePos.X;
                var diffY = Mousy.Y - mousePos.Y;

                var canvLeft = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.LeftProperty));
                var canvTop  = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.TopProperty));

                Canvas.SetLeft(SelectedVertexShape, canvLeft - diffX);
                Canvas.SetTop(SelectedVertexShape, canvTop - diffY);

                foreach (var outedge in Vertex.OutEdges())
                {
                    var EdgeLine = outedge.GetProperty(EdgeShapePropertyKey) as Line;
                    EdgeLine.X1 -= diffX;
                    EdgeLine.Y1 -= diffY;
                }

                foreach (var inedge in Vertex.InEdges())
                {
                    var EdgeLine = inedge.GetProperty(EdgeShapePropertyKey) as Line;
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
            Mousy = MouseEventArgs.GetPosition(this);
            SelectedVertexShape = null;
        }

        #endregion


        // Vertices

        #region (private) AddVertex(Graph, Vertex)

        private void AddVertex(IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
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

                var VertexShape = _VertexShapeCreator(Vertex);
                VertexShape.MouseMove += VertexShape_MouseMove;
                VertexShape.MouseLeftButtonDown += VertexShape_MouseLeftButtonDown;
                VertexShape.MouseLeftButtonUp += VertexShape_MouseLeftButtonUp;
                VertexShape.DataContext = Vertex;

                ToolTipService.SetToolTip(VertexShape, VertexToolTip(Vertex));

                if (OnChangedNumberOfVertices != null)
                    OnChangedNumberOfVertices(Graph.NumberOfVertices());

                Children.Add(VertexShape);
                Canvas.SetLeft(VertexShape, Random.Next(20, 400 - 20));
                Canvas.SetTop(VertexShape, Random.Next(20, 200 - 20));

                Vertex.SetProperty(VertexShapePropertyKey, (TValueVertex) (Object) VertexShape);

            }

        }

        #endregion

        #region (static)  DefaultVertexShape(Vertex)

        /// <summary>
        /// Returns the default shape for the given vertex,
        /// which is a constant sized circle.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static Shape DefaultVertexShape(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            var VertexShape = new Ellipse();
            VertexShape.Stroke = new SolidColorBrush(Colors.Black);
            VertexShape.StrokeThickness = 1;
            VertexShape.Width = 30;
            VertexShape.Height = 30;
            VertexShape.Fill = new SolidColorBrush(Colors.Red);

            return VertexShape;

        }

        #endregion

        #region (static)  DefaultVertexToolTip(Vertex)

        /// <summary>
        /// Returns the default tooltip for the given vertex.
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

        #region (private) VertexShape_MouseLeftButtonDown(Sender, MouseButtonEventArgs)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="MouseButtonEventArgs"></param>
        private void VertexShape_MouseLeftButtonDown(Object Sender, MouseButtonEventArgs MouseButtonEventArgs)
        {

            Mousy = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexShape = Sender as Shape;
            Vertex = SelectedVertexShape.DataContext as IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

        }

        #endregion

        #region (private) VertexShape_MouseLeftButtonUp(Sender, MouseButtonEventArgs)

        private void VertexShape_MouseLeftButtonUp(Object sender, MouseButtonEventArgs MouseButtonEventArgs)
        {
            Mousy = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexShape = null;
        }

        #endregion

        #region (private) VertexShape_MouseMove(Sender, MouseEventArgs)

        private void VertexShape_MouseMove(Object Sender, MouseEventArgs MouseEventArgs)
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
                             IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (Edge != null)
            {

                var Vertex1 = Edge.OutVertex.GetProperty(VertexShapePropertyKey) as Shape;
                var Vertex2 = Edge.InVertex.GetProperty(VertexShapePropertyKey) as Shape;

                var EdgeShape = new Line();
                EdgeShape.X1 = Canvas.GetLeft(Vertex1) + Vertex1.Width / 2;
                EdgeShape.Y1 = Canvas.GetTop(Vertex1) + Vertex1.Height / 2;
                EdgeShape.X2 = Canvas.GetLeft(Vertex2) + Vertex2.Width / 2;
                EdgeShape.Y2 = Canvas.GetTop(Vertex2) + Vertex2.Height / 2;
                EdgeShape.Stroke = new SolidColorBrush(Colors.Black);
                EdgeShape.StrokeThickness = 2;
                EdgeShape.DataContext = Edge;
                ToolTipService.SetToolTip(EdgeShape, EdgeToolTip(Edge));

                Canvas.SetZIndex(EdgeShape, -99);
                Children.Add(EdgeShape);

                Edge.SetProperty(EdgeShapePropertyKey, (TValueEdge) (Object) EdgeShape);

                ToolTipService.SetToolTip(Vertex1, DefaultVertexToolTip(Edge.OutVertex));
                ToolTipService.SetToolTip(Vertex2, DefaultVertexToolTip(Edge.InVertex));

                if (OnChangedNumberOfEdges != null)
                    OnChangedNumberOfEdges(Graph.NumberOfEdges());

            }

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

    }

    #endregion

}
