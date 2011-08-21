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

    public static class Ext
    {

        public static Object GetProperty<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                             (this IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,
                              TKeyVertex                 Key,
                              Func<TValueVertex, Object> PropertyExists,
                              Func<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, Object> FalseFunc = null)

            where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
            where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
            where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

            where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
            where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
            where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

            where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
            where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

            where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
            where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
            where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge
        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (PropertyExists == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValueVertex Value;
            if (Vertex.GetProperty(Key, out Value))    // v.GetProperty("Name", out Name, typeof(String))
                return PropertyExists(Value);

            if (FalseFunc != null)
                return FalseFunc(Vertex);
            else
                return null;

        }

        public static Object GetProperty<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                             (this IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,
                              TKeyVertex                 Key,
                              Type                       myType,
                              Func<TValueVertex, Object> PropertyExistsAndIsValid,
                              Func<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, Object> FalseFunc = null)

            where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
            where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
            where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

            where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
            where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
            where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

            where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
            where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

            where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
            where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
            where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (PropertyExistsAndIsValid == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValueVertex Value;
            if (Vertex.GetProperty(Key, out Value))
                if (Value.GetType().Equals(myType))
                    return PropertyExistsAndIsValid(Value);

            if (FalseFunc != null)
                return FalseFunc(Vertex);
            else
                return null;

        }

    }


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
                                     ElseVertex    => { return ElseVertex.Id; }).ToString();
            };


            // Edges ToolTip
            GraphCanvas.EdgeToolTip = e => e.Label;

            #endregion

        }

    }

}
