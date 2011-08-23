using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using de.ahzf.Blueprints.PropertyGraph;

namespace TestGraph01
{

    public partial class MainPage : UserControl
    {

        public MainPage()
        {
            
            InitializeComponent();

            var Graph = GraphCanvas.Graph;
            
            #region Customize the vertex and edge shapes

            GraphCanvas.VertexShapeCreator = v => {
                
                var VertexShape             = new Rectangle();

                VertexShape.Stroke          = new SolidColorBrush(Colors.Black);
                VertexShape.StrokeThickness = 1;
                VertexShape.Width           = v.Id * 10;
                VertexShape.Height          = v.Id * 10;
                VertexShape.Fill            = new SolidColorBrush(Colors.Red);

                return VertexShape;

            };

            #endregion

            var Alice = Graph.AddVertex(v => v.SetProperty("Name", "Alice"));
            var Bob   = Graph.AddVertex(v => v.SetProperty("Name", "Bob"  ));
            var Carol = Graph.AddVertex(v => v.SetProperty("Name", "Carol"));

            var e1    = Graph.AddEdge(Alice, Bob,   "friends");
            var e2    = Graph.AddEdge(Bob,   Carol, "friends");
            var e3    = Graph.AddEdge(Alice, Carol, "friends");

            #region Customize the vertex and edge tooltips

            // Vertices ToolTip
            //GraphCanvas.VertexToolTip = v =>
            //{
            //    Object Name;
            //    if (v.GetProperty("Name", out Name))
            //        return Name as String;
            //    else
            //        return v.Id.ToString();
            //};

            GraphCanvas.VertexToolTip = v =>
            {
                return v.GetProperty("Name", typeof(String),
                                     FoundProperty => { return FoundProperty; },
                                     VertexOnError => { return VertexOnError.Id; }).ToString();
            };


            // Edges ToolTip
            GraphCanvas.EdgeToolTip = e => e.Label;

            #endregion

        }

    }

}
