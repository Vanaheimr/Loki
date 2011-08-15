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

        private Random  Random;
        private Point   Mousy;
        private Ellipse SelectedVertexShape;
        private IPropertyVertex<UInt64, Int64,         String, Object,
                                UInt64, Int64, String, String, Object,
                                UInt64, Int64, String, String, Object> Vertex;

        #endregion

        #region Properties

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IPropertyGraph<UInt64, Int64,         String, Object,
                              UInt64, Int64, String, String, Object,
                              UInt64, Int64, String, String, Object> Graph { get; private set; }

        #endregion

        #region Events

        #region NumberOfVertices

        /// <summary>
        /// The current number of vertices.
        /// </summary>
        /// <param name="NumberOfVertices">The current number of vertices</param>
        public delegate void ChangedNumberOfVertices(UInt64 NumberOfVertices);

        /// <summary>
        /// Called whenever the number of vertices changed.
        /// </summary>
        public event ChangedNumberOfVertices OnChangedNumberOfVertices;

        #endregion

        #region NumberOfEdges

        /// <summary>
        /// The current number of edges.
        /// </summary>
        /// <param name="NumberOfEdges">The current number of edges</param>
        public delegate void ChangedNumberOfEdges(UInt64 NumberOfEdges);

        /// <summary>
        /// Called whenever the number of edges changed.
        /// </summary>
        public event ChangedNumberOfEdges OnChangedNumberOfEdges;

        #endregion

        #region MousePosition

        /// <summary>
        /// The current mouse position.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public delegate void ChangedMousePosition(Double X, Double Y);

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

        }

        #endregion

        #endregion


        // Canvas

        #region GraphCanvas_MouseMove(Sender, MouseEventArgs)

        private void GraphCanvas_MouseMove(Object sender, MouseEventArgs MouseEventArgs)
        {

            var MMMousy = MouseEventArgs.GetPosition(this);

            if (OnChangedMousePosition != null)
                OnChangedMousePosition(MMMousy.X, MMMousy.Y);

            if (SelectedVertexShape != null)
            {

                Point mousePos = MouseEventArgs.GetPosition(this);
                Vector diff = Mousy - mousePos;

                var canvLeft = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.LeftProperty));
                var canvTop  = Convert.ToDouble(SelectedVertexShape.GetValue(Canvas.TopProperty));

                Canvas.SetLeft(SelectedVertexShape, canvLeft - diff.X);
                Canvas.SetTop(SelectedVertexShape, canvTop - diff.Y);

                foreach (var outedge in Vertex.OutEdges())
                {
                    var EdgeLine = outedge.GetProperty("EdgeUI") as Line;
                    EdgeLine.X1 -= diff.X;
                    EdgeLine.Y1 -= diff.Y;
                }

                foreach (var inedge in Vertex.InEdges())
                {
                    var EdgeLine = inedge.GetProperty("EdgeUI") as Line;
                    EdgeLine.X2 -= diff.X;
                    EdgeLine.Y2 -= diff.Y;
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

        #region (private) AddVertex(IPropertyGraph, Vertex)

        private void AddVertex(IPropertyGraph <UInt64, Int64,         String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> IPropertyGraph,
                               IPropertyVertex<UInt64, Int64,         String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Vertex)
        {

            if (Vertex != null)
            {

                var VertexShape = new Ellipse();
            
                VertexShape.Stroke               = new SolidColorBrush(Colors.Black);
                VertexShape.StrokeThickness      = 1;
                VertexShape.Width                = 30;
                VertexShape.Height               = 30;
                VertexShape.Fill                 = new SolidColorBrush(Colors.Red);
                VertexShape.MouseMove           += VertexShape_MouseMove;
                VertexShape.MouseLeftButtonDown += VertexShape_MouseLeftButtonDown;
                VertexShape.MouseLeftButtonUp   += VertexShape_MouseLeftButtonUp;
                VertexShape.DataContext          = Vertex;
                VertexShape.ToolTip              = GetVertexToolTip(Vertex);

                if (OnChangedNumberOfVertices != null)
                    OnChangedNumberOfVertices(Graph.NumberOfVertices());

                Children.Add(VertexShape);
                Canvas.SetLeft(VertexShape, Random.Next(20, 400 - 20));
                Canvas.SetTop (VertexShape, Random.Next(20, 200 - 20));

                Vertex.SetProperty("VertexUI", VertexShape);

            }

        }

        #endregion

        #region (private) GetVertexToolTip(Vertex)

        private String GetVertexToolTip(IPropertyVertex<UInt64, Int64,         String, Object,
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
            SelectedVertexShape = Sender as Ellipse;
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

        #region (private) AddEdge(IPropertyGraph, Edge)

        private void AddEdge(IPropertyGraph<UInt64, Int64,         String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> IPropertyGraph,
                             IPropertyEdge <UInt64, Int64,         String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Edge)
        {

            if (Edge != null)
            {

                var Vertex1               = Edge.OutVertex.GetProperty("VertexUI") as Ellipse;
                var Vertex2               = Edge. InVertex.GetProperty("VertexUI") as Ellipse;

                var EdgeShape             = new Line();
                EdgeShape.X1              = Canvas.GetLeft(Vertex1) + Vertex1.Width/2;
                EdgeShape.Y1              = Canvas.GetTop (Vertex1) + Vertex1.Height/2;
                EdgeShape.X2              = Canvas.GetLeft(Vertex2) + Vertex2.Width/2;
                EdgeShape.Y2              = Canvas.GetTop (Vertex2) + Vertex2.Height/2;
                EdgeShape.Stroke          = new SolidColorBrush(Colors.Black);
                EdgeShape.StrokeThickness = 2;
                EdgeShape.DataContext     = Edge;
                EdgeShape.ToolTip         = GetVertexToolTip(Edge);
                Canvas.SetZIndex(EdgeShape, -99);
                Children.Add(EdgeShape);

                if (OnChangedNumberOfEdges != null)
                    OnChangedNumberOfEdges(Graph.NumberOfEdges());

                Edge.SetProperty("EdgeUI", EdgeShape);

                Vertex1.ToolTip           = GetVertexToolTip(Edge.OutVertex);
                Vertex2.ToolTip           = GetVertexToolTip(Edge. InVertex);

            }

        }

        #endregion

        #region (private) GetEdgeToolTip(Edge)

        private String GetVertexToolTip(IPropertyEdge<UInt64, Int64,         String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object> Edge)
        {
            return "EdgeId: " + Edge.Id + " [OutVertexId: " + Edge.OutVertex.Id.ToString() + ", InVertexId: " + Edge.InVertex.Id.ToString() + "]";
        }

        #endregion


    }

}
