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

namespace de.ahzf.Loki
{

    /// <summary>
    /// Creates a new canvas for visualizing a property graph.
    /// </summary>
    public class GraphCanvas : Canvas
    {

        #region Data

        /// <summary>
        /// The property key for storing the vertex shape.
        /// </summary>
        public const String __VertexShapePropertyKey = "VertexShape";

        /// <summary>
        /// The property key for storing the edge shape.
        /// </summary>
        public const String __EdgeShapePropertyKey   = "EdgeShape";

        private Random  Random;
        private Point   Mousy;
        private Shape   SelectedVertexShape;
        private IPropertyVertex<UInt64, Int64,         String, Object,
                                UInt64, Int64, String, String, Object,
                                UInt64, Int64, String, String, Object> Vertex;

        #endregion

        #region Delegates

        #region Vertices

        /// <summary>
        /// A delegate for creating a shape for the given vertex.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public delegate Shape  VertexShapeCreatorDelegate(IPropertyVertex<UInt64, Int64,         String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> Vertex);

        /// <summary>
        /// A delegate for generating a tooltip for the given vertex.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public delegate String VertexToolTipDelegate     (IPropertyVertex<UInt64, Int64,         String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> Vertex);

        /// <summary>
        /// The current number of vertices.
        /// </summary>
        /// <param name="NumberOfVertices">The current number of vertices</param>
        public delegate void   ChangedNumberOfVertices   (UInt64 NumberOfVertices);

        #endregion

        #region Edges

        /// <summary>
        /// A delegate for creating a shape for the given edge.
        /// </summary>
        /// <param name="Edge">A proeprty edge</param>
        public delegate Shape  EdgeShapeCreatorDelegate  (IPropertyEdge  <UInt64, Int64,         String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> Edge);

        /// <summary>
        /// A delegate for generating a tooltip for the given edge.
        /// </summary>
        /// <param name="Edge">A proeprty edge</param>
        public delegate String EdgeToolTipDelegate       (IPropertyEdge  <UInt64, Int64,         String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> Edge);

        /// <summary>
        /// The current number of edges.
        /// </summary>
        /// <param name="NumberOfEdges">The current number of edges</param>
        public delegate void   ChangedNumberOfEdges      (UInt64 NumberOfEdges);

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
        public IPropertyGraph<UInt64, Int64,         String, Object,
                              UInt64, Int64, String, String, Object,
                              UInt64, Int64, String, String, Object> Graph { get; private set; }
        
        #endregion


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

                    Shape  VertexShape;
                    Object VertexShapeProperty;
                    foreach (var Vertex in Graph.Vertices())
                    {
                        if (Vertex.GetProperty(__VertexShapePropertyKey, out VertexShapeProperty))
                        {
                            
                            VertexShape = VertexShapeProperty as Shape;
                            
                            if (VertexShape != null)
#if SILVERLIGHT
                                ToolTipService.SetToolTip(VertexShape, VertexToolTip(Vertex));
#else
                                VertexShape.ToolTip = VertexToolTip(Vertex);
#endif

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

                    Shape  EdgeShape;
                    Object EdgeShapeProperty;
                    foreach (var Edge in Graph.Edges())
                    {
                        if (Edge.GetProperty(__EdgeShapePropertyKey, out EdgeShapeProperty))
                        {
                            
                            EdgeShape = EdgeShapeProperty as Shape;
                            
                            if (EdgeShape != null)
#if SILVERLIGHT
                                ToolTipService.SetToolTip(EdgeShape, EdgeToolTip(Edge));
#else
                                EdgeShape.ToolTip = EdgeToolTip(Edge);
#endif

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

        #region GraphCanvas()

        /// <summary>
        /// Creates a new canvas for visualizing a SimplePropertyGraph.
        /// </summary>
        public GraphCanvas()
            : this(new SimplePropertyGraph())
        { }

        #endregion

        #region GraphCanvas(IPropertyGraph)

        /// <summary>
        /// Creates a new canvas for visualizing the given property graph.
        /// </summary>
        public GraphCanvas(IPropertyGraph<UInt64, Int64,         String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object> IPropertyGraph)
        {

            this.Graph           = IPropertyGraph;
            Graph.SetProperty("GraphCanvas", this);
            DataContext          = Graph;
            Random               = new Random();

            this.Background      = new SolidColorBrush(Colors.Transparent);
            this.MouseMove      += GraphCanvas_MouseMove;
            this.MouseLeave     += GraphCanvas_MouseLeave;
            Graph.OnVertexAdded += AddVertex;
            Graph.OnEdgeAdded   += AddEdge;

            _VertexShapeCreator  = DefaultVertexShape;
            _VertexToolTip       = DefaultVertexToolTip;
            _EdgeToolTip         = DefaultEdgeToolTip;

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
                Canvas.SetTop(SelectedVertexShape, canvTop - diffY);

                foreach (var outedge in Vertex.OutEdges())
                {
                    var EdgeLine = outedge.GetProperty(__EdgeShapePropertyKey) as Line;
                    EdgeLine.X1 -= diffX;
                    EdgeLine.Y1 -= diffY;
                }

                foreach (var inedge in Vertex.InEdges())
                {
                    var EdgeLine = inedge.GetProperty(__EdgeShapePropertyKey) as Line;
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

        private void AddVertex(IPropertyGraph <UInt64, Int64,         String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Graph,
                               IPropertyVertex<UInt64, Int64,         String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Vertex)
        {

            if (Vertex != null)
            {

                var VertexShape                  = _VertexShapeCreator(Vertex);                    
                VertexShape.MouseMove           += VertexShape_MouseMove;
                VertexShape.MouseLeftButtonDown += VertexShape_MouseLeftButtonDown;
                VertexShape.MouseLeftButtonUp   += VertexShape_MouseLeftButtonUp;
                VertexShape.DataContext          = Vertex;
                
#if SILVERLIGHT
                ToolTipService.SetToolTip(VertexShape, VertexToolTip(Vertex));
#else
                VertexShape.ToolTip              = VertexToolTip(Vertex);
#endif

                if (OnChangedNumberOfVertices != null)
                    OnChangedNumberOfVertices(Graph.NumberOfVertices());

                Children.Add(VertexShape);
                Canvas.SetLeft(VertexShape, Random.Next(20, 400 - 20));
                Canvas.SetTop (VertexShape, Random.Next(20, 200 - 20));

                Vertex.SetProperty(__VertexShapePropertyKey, VertexShape);

            }

        }

        #endregion

        #region (static)  DefaultVertexShape(Vertex)

        /// <summary>
        /// Returns the default shape for the given vertex,
        /// which is a constant sized circle.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static Shape DefaultVertexShape(IPropertyVertex<UInt64, Int64,         String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> Vertex)
        {

            var VertexShape             = new Ellipse();
            VertexShape.Stroke          = new SolidColorBrush(Colors.Black);
            VertexShape.StrokeThickness = 1;
            VertexShape.Width           = 30;
            VertexShape.Height          = 30;
            VertexShape.Fill            = new SolidColorBrush(Colors.Red);

            return VertexShape;

        }

        #endregion

        #region (static)  DefaultVertexToolTip(Vertex)

        /// <summary>
        /// Returns the default tooltip for the given vertex.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexToolTip(IPropertyVertex<UInt64, Int64,         String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> Vertex)
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

            Mousy               = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexShape = Sender as Shape;
            Vertex              = SelectedVertexShape.DataContext as IPropertyVertex<UInt64, Int64,         String, Object,
                                                                                     UInt64, Int64, String, String, Object,
                                                                                     UInt64, Int64, String, String, Object>;

        }

        #endregion

        #region (private) VertexShape_MouseLeftButtonUp(Sender, MouseButtonEventArgs)

        private void VertexShape_MouseLeftButtonUp(Object sender, MouseButtonEventArgs MouseButtonEventArgs)
        {
            Mousy               = MouseButtonEventArgs.GetPosition(this);
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

        private void AddEdge(IPropertyGraph<UInt64, Int64,         String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Graph,
                             IPropertyEdge <UInt64, Int64,         String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Edge)
        {

            if (Edge != null)
            {

                var Vertex1               = Edge.OutVertex.GetProperty(__VertexShapePropertyKey) as Shape;
                var Vertex2               = Edge. InVertex.GetProperty(__VertexShapePropertyKey) as Shape;

                var EdgeShape             = new Line();
                EdgeShape.X1              = Canvas.GetLeft(Vertex1) + Vertex1.Width/2;
                EdgeShape.Y1              = Canvas.GetTop (Vertex1) + Vertex1.Height/2;
                EdgeShape.X2              = Canvas.GetLeft(Vertex2) + Vertex2.Width/2;
                EdgeShape.Y2              = Canvas.GetTop (Vertex2) + Vertex2.Height/2;
                EdgeShape.Stroke          = new SolidColorBrush(Colors.Black);
                EdgeShape.StrokeThickness = 2;
                EdgeShape.DataContext     = Edge;
#if SILVERLIGHT
                ToolTipService.SetToolTip(EdgeShape, EdgeToolTip(Edge));
#else
                EdgeShape.ToolTip         = EdgeToolTip(Edge);
#endif

                Canvas.SetZIndex(EdgeShape, -99);
                Children.Add(EdgeShape);

                Edge.SetProperty(__EdgeShapePropertyKey, EdgeShape);

#if SILVERLIGHT
                ToolTipService.SetToolTip(Vertex1, DefaultVertexToolTip(Edge.OutVertex));
                ToolTipService.SetToolTip(Vertex2, DefaultVertexToolTip(Edge.InVertex));
#else
                Vertex1.ToolTip = DefaultVertexToolTip(Edge.OutVertex);
                Vertex2.ToolTip           = DefaultVertexToolTip(Edge. InVertex);
#endif

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
        public static String DefaultEdgeToolTip(IPropertyEdge<UInt64, Int64,         String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object> Edge)
        {
            return "EdgeId: " + Edge.Id + " [OutVertexId: " + Edge.OutVertex.Id.ToString() + ", InVertexId: " + Edge.InVertex.Id.ToString() + "]";
        }

        #endregion


    }

}
