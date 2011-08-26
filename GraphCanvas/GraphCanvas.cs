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
using System.Windows.Media.Imaging;
using System.IO;

#endregion

namespace de.ahzf.Loki
{

    #region Vertices

    /// <summary>
    /// A delegate for creating a shape for the given vertex.
    /// </summary>
    /// <param name="Vertex">A property vertex.</param>
    public delegate VertexControl  VertexControlCreatorDelegate(IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                              UInt64, Int64, String, String, Object,
                                                                              UInt64, Int64, String, String, Object,
                                                                              UInt64, Int64, String, String, Object> Vertex);

    /// <summary>
    /// A delegate for generating a caption for the given vertex.
    /// </summary>
    /// <param name="Vertex">A property vertex.</param>
    public delegate String VertexCaptionDelegate     (IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object> Vertex);

    /// <summary>
    /// A delegate for generating a tooltip for the given vertex.
    /// </summary>
    /// <param name="Vertex">A property vertex.</param>
    public delegate String VertexToolTipDelegate     (IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
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
    public delegate EdgeControl EdgeControlCreatorDelegate(IPropertyEdge<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object> Edge);

    /// <summary>
    /// A delegate for generating a caption for the given edge.
    /// </summary>
    /// <param name="Edge">A proeprty edge</param>
    public delegate String EdgeCaptionDelegate    (IPropertyEdge<UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object> Edge);

    /// <summary>
    /// A delegate for generating a tooltip for the given edge.
    /// </summary>
    /// <param name="Edge">A proeprty edge</param>
    public delegate String EdgeToolTipDelegate    (IPropertyEdge<UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object,
                                                                 UInt64, Int64, String, String, Object> Edge);

    /// <summary>
    /// The current number of edges.
    /// </summary>
    /// <param name="NumberOfEdges">The current number of edges</param>
    public delegate void ChangedNumberOfEdges     (UInt64 NumberOfEdges);

    #endregion

    #region ChangedMousePosition(X, Y)

    /// <summary>
    /// The current mouse position.
    /// </summary>
    /// <param name="X">X</param>
    /// <param name="Y">Y</param>
    public delegate void ChangedMousePosition(Double X, Double Y);

    #endregion


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
        private VertexControl SelectedVertexShape;
        private IPropertyVertex<UInt64, Int64, String, String, Object,
                                UInt64, Int64, String, String, Object,
                                UInt64, Int64, String, String, Object,
                                UInt64, Int64, String, String, Object> Vertex;
        private String CurrentDirectory;

        #endregion

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IPropertyGraph<UInt64, Int64, String, String, Object,
                              UInt64, Int64, String, String, Object,
                              UInt64, Int64, String, String, Object,
                              UInt64, Int64, String, String, Object> Graph { get; private set; }
        
        #endregion


        #region VertexControlCreator

        private VertexControlCreatorDelegate _VertexControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given vertex.
        /// </summary>
        public VertexControlCreatorDelegate VertexControlCreator
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

        private VertexCaptionDelegate _VertexCaption;

        /// <summary>
        /// A delegate for generating caption for the given vertex.
        /// </summary>
        public VertexCaptionDelegate VertexCaption
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


        #region EdgeControlCreator

        private EdgeControlCreatorDelegate _EdgeControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given edge.
        /// </summary>
        public EdgeControlCreatorDelegate EdgeControlCreator
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

        private EdgeCaptionDelegate _EdgeCaption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate EdgeCaption
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
        public GraphCanvas(IPropertyGraph<UInt64, Int64, String,  String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object> IPropertyGraph)
        {

            this.Graph              = IPropertyGraph;
            Graph.SetProperty("GraphCanvas", this);
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
                    var EdgeLine = outedge.GetProperty(__EdgeShapePropertyKey) as EdgeControl;
                    EdgeLine.X1 -= diffX;
                    EdgeLine.Y1 -= diffY;
                }

                foreach (var inedge in Vertex.InEdges())
                {
                    var EdgeLine = inedge.GetProperty(__EdgeShapePropertyKey) as EdgeControl;
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

        private void AddVertex(IPropertyGraph <UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Graph,
                               IPropertyVertex<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Vertex)
        {

            if (Vertex != null)
            {

                var VertexControl                  = _VertexControlCreator(Vertex);
                VertexControl.MouseMove           += VertexControl_MouseMove;
                VertexControl.MouseLeftButtonDown += VertexControl_MouseLeftButtonDown;
                VertexControl.MouseLeftButtonUp   += VertexControl_MouseLeftButtonUp;
                VertexControl.DataContext          = Vertex;
                Vertex.SetProperty(__VertexShapePropertyKey, VertexControl);

                VertexControl.Caption              = _VertexCaption;
                
#if SILVERLIGHT
                ToolTipService.SetToolTip(VertexShape, VertexToolTip(Vertex));
#else
                VertexControl.ToolTip              = VertexToolTip(Vertex);
#endif

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
        public static VertexControl DefaultVertexControlCreator(IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object> Vertex)
        {

            var VertexControl             = new VertexControl(Vertex);
            VertexControl.Fill            = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));
            VertexControl.Stroke          = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            VertexControl.Width           = Vertex.Id * 10;
            VertexControl.Height          = Vertex.Id * 10;
            VertexControl.ShowCaption     = true;

            return VertexControl;

        }

        #endregion

        #region (static)  DefaultVertexCaption(Vertex)

        /// <summary>
        /// Returns the default caption for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexCaption(IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> Vertex)
        {
            return Vertex.Id.ToString();
        }

        #endregion

        #region (static)  DefaultVertexToolTip(Vertex)

        /// <summary>
        /// Returns the default tooltip for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexToolTip(IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> Vertex)
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
            SelectedVertexShape = Sender as VertexControl;
            Vertex              = SelectedVertexShape.DataContext as IPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                     UInt64, Int64, String, String, Object,
                                                                                     UInt64, Int64, String, String, Object,
                                                                                     UInt64, Int64, String, String, Object>;

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

        private void AddEdge(IPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Graph,
                             IPropertyEdge <UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Edge)
        {

            if (Edge != null)
            {

                var EdgeControl             = _EdgeControlCreator(Edge);
                EdgeControl.Caption         = _EdgeCaption;

#if SILVERLIGHT
                ToolTipService.SetToolTip(EdgeShape, EdgeToolTip(Edge));
#else
                EdgeControl.ToolTip         = EdgeToolTip(Edge);
#endif

                Canvas.SetZIndex(EdgeControl, -99);
                Children.Add(EdgeControl);

                Edge.SetProperty(__EdgeShapePropertyKey, EdgeControl);


                var OutVertexControl = Edge.OutVertex.GetProperty(__VertexShapePropertyKey) as VertexControl;
                var InVertexControl  = Edge.InVertex. GetProperty(__VertexShapePropertyKey) as VertexControl;

#if SILVERLIGHT
                ToolTipService.SetToolTip(OutVertexControl, DefaultVertexToolTip(Edge.OutVertex));
                ToolTipService.SetToolTip(InVertexControl,  DefaultVertexToolTip(Edge.InVertex));
#else
                OutVertexControl.ToolTip    = DefaultVertexToolTip(Edge.OutVertex);
                 InVertexControl.ToolTip    = DefaultVertexToolTip(Edge. InVertex);
#endif

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
        public static EdgeControl DefaultEdgeControlCreator(IPropertyEdge<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> Edge)
        {

            var EdgeControl                 = new EdgeControl(Edge);
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
        public static String DefaultEdgeCaption(IPropertyEdge<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object> Edge)
        {
            return Edge.Label.ToString();
        }

        #endregion

        #region (static)  DefaultEdgeToolTip(Edge)

        /// <summary>
        /// Returns the default tooltip for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static String DefaultEdgeToolTip(IPropertyEdge<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object> Edge)
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
                MessageBox.Show("Cancel!",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }

        }

    }

}
