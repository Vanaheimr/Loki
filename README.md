
Loki is a collection of graph visualization and interaction tools for
[Blueprints.NET](http://github.com/ahzf/Blueprints.NET) based graph
data models hosted in Microsoft Windows Presentation Framework,
Silverlight or HTML5 environments.

#### An example for the usage of Loki within WPF

WPF has a very nice canvas tool for drawing all kinds of visual stuff.
Loki provides an extention to this class called GraphCanvas which makes
it easy to draw a customized 
[Blueprints.NET graph](http://github.com/ahzf/Blueprints.NET). In order
to do so just add the GraphCanavas.WPF project to your solution and...

if your **MainWindow.xaml** looks like this...

    <Window x:Class    = "de.ahzf.Loki.WPFDemo.MainWindow"
            xmlns      = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x    = "http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:Loki = "clr-namespace:de.ahzf.Loki;assembly=GraphCanvas"
            Title      = "Loki WPFDemo"
            Height     = "400"
            Width      = "600">

        <Grid>
            <Loki:GraphCanvas Grid.Column         = "0"
                              HorizontalAlignment = "Stretch"
                              Name                = "GraphCanvas"
                              VerticalAlignment   = "Stretch"
                              ClipToBounds        = "True"
                              ToolTip             = "Graph canvas"
                              SnapsToDevicePixels = "True" /><Loki:OldArrow />
        </Grid>

    </Window>


...and your **MainWindow.cs** looks like this...

    using System;
    using System.Windows;

    namespace de.ahzf.Loki.WPFDemo
    {

        public partial class MainWindow : Window
        {

            public MainWindow()
            {

                InitializeComponent();

                // Customize the vertices caption
                GraphCanvas.VertexCaption = v =>
                {
                    Object Name;
                    if (v.GetProperty("Name", out Name))
                        return Name as String;
                    else
                        return v.Id.ToString();
                };

                var Graph = GraphCanvas.Graph;

                var Alice = Graph.AddVertex(v => v.SetProperty("Name", "Alice"));
                var Bob   = Graph.AddVertex(v => v.SetProperty("Name", "Bob"  ));
                var Carol = Graph.AddVertex(v => v.SetProperty("Name", "Carol"));
                var Dave  = Graph.AddVertex(v => v.SetProperty("Name", "Dave" ));

                var e1    = Graph.AddEdge(Alice, "friends", Bob  );
                var e2    = Graph.AddEdge(Bob,   "friends", Carol);
                var e3    = Graph.AddEdge(Alice, "friends", Carol);
                var e4    = Graph.AddEdge(Carol, "friends", Dave );

                // Customize the vertices tooltip
                GraphCanvas.VertexToolTip = v => {
                    Object Name;
                    if (v.GetProperty("Name", out Name))
                        return Name as String;
                    else
                        return v.Id.ToString();
                };

                // Customize the edges tooltip
                GraphCanvas.EdgeToolTip = e => e.Label;

            }

        }

    }

...your result will be something like this:

![WPFDemo small](http://github.com/ahzf/Loki/wiki/Loki.WPF_small.png)
