using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eu.Vanaheimr.Loki
{

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ChartControl : UserControl
    {

        #region Data

        public const String DefaultId   = "default";
        public const String DefaultUnit = "default";
        public const Byte   Padding     = 15;
        public const Byte   ArrowSize   = 4;

        private Line MouseLine;

        #endregion

        #region Properties

        public Dictionary<String, Diagram>              Diagrams            { get; private set; }
        public Dictionary<String, List<Diagram>>        YUnits              { get; private set; }

        public Double                                   SizeX               { get; private set; }
        public Double                                   SizeY               { get; private set; }

        public Double                                   MinY                { get; private set; }
        public Double                                   MaxY                { get; private set; }

        public UInt64                                   NumberOfValues      { get; private set; }

        #endregion

        public ChartControl()
        {

            InitializeComponent();

            this.Background = Brushes.Transparent;
            this.Diagrams   = new Dictionary<String, Diagram>();
            this.YUnits     = new Dictionary<String, List<Diagram>>();

            this.MouseMove += ProcessMouseMovement;

            this.MouseLine                  = new Line();
            this.MouseLine.Y1               = 0;
            this.MouseLine.Stroke           = Brushes.LightSteelBlue;
            this.MouseLine.StrokeThickness  = 1;
            this.MouseLine.IsHitTestVisible = false;

            this.SizeChanged += new SizeChangedEventHandler(ChartCanvas_SizeChanged);

        }



        void ChartCanvas_SizeChanged(Object Sender, SizeChangedEventArgs SizeChangedEventArgs)
        {
            Redraw();
        }


        #region Add(Values)

        public void Add(IEnumerable<Double> Values)
        {

            this.Diagrams.Add(DefaultId, new Diagram(DefaultId, DefaultUnit, Values));

            Redraw();

        }

        #endregion

        #region Add(Id, YUnits, Values)

        public void Add(String Id, String YUnits, IEnumerable<Double> Values)
        {
            Add(new Diagram(Id, YUnits, Values));
        }

        #endregion

        #region Add(Diagram)

        public void Add(Diagram Diagram)
        {

            if (!Diagrams.ContainsKey(Diagram.Id))
                Diagrams.Add(Diagram.Id, Diagram);

            if (!YUnits.ContainsKey(Diagram.YUnits))
                YUnits.Add(Diagram.YUnits, new List<Diagram>() { Diagram });
            else
                YUnits[Diagram.YUnits] = new List<Diagram>() { Diagram };

            Redraw();

        }

        #endregion


        #region Contains()

        public Boolean Contains(String Id)
        {
            return this.Diagrams.ContainsKey(Id);
        }

        #endregion


        #region (private) ProcessMouseMovement(Sender, MouseEventArgs)

        private void ProcessMouseMovement(Object Sender, MouseEventArgs MouseEventArgs)
        {

            var position = MouseEventArgs.GetPosition(this);

            MouseLine.X1 = position.X;
            MouseLine.X2 = position.X;
            MouseLine.Y2 = ActualHeight;

        }

        #endregion


        #region Redraw()

        public void Redraw()
        {

            var Height = this.ChartCanvas1.ActualHeight;
            var Width  = this.ChartCanvas1.ActualWidth;


            #region Initial checks

            if (!Diagrams.Any())
                return;

            this.ChartCanvas1.Children.Clear();

            #endregion

            #region Get NumberOfValues/MinY/MaxY values

            NumberOfValues  = 0;
            MinY            = Double.MaxValue;
            MaxY            = Double.MinValue;

            foreach (var Diagram in Diagrams.Values)
            {

                if (NumberOfValues < Diagram.NumberOfValues)
                    NumberOfValues = Diagram.NumberOfValues;

                if (MinY > Diagram.MinY)
                    MinY = Diagram.MinY;

                if (MaxY < Diagram.MaxY)
                    MaxY = Diagram.MaxY;

            }

            var YStepping = (Height - 2 * Padding) / (Math.Abs(MinY) + Math.Abs(MaxY));
            var ZeroY     = YStepping * MaxY;

            var XStepping = (Width - 2 * Padding) / (NumberOfValues - 1);

            #endregion

            #region Draw X-axis

            var x_axis = new Line();
            x_axis.X1               = 0;
            x_axis.X2               = Width;
            x_axis.Y1               = ZeroY + Padding;
            x_axis.Y2               = ZeroY + Padding;
            x_axis.Stroke           = Brushes.LightSteelBlue;
            x_axis.StrokeThickness  = 1;

            this.ChartCanvas1.Children.Add(x_axis);

            var x_axis1 = new Line();
            x_axis1.X1               = Width - ArrowSize;
            x_axis1.X2               = Width;
            x_axis1.Y1               = ZeroY + Padding - ArrowSize;
            x_axis1.Y2               = ZeroY + Padding;
            x_axis1.Stroke           = Brushes.LightSteelBlue;
            x_axis1.StrokeThickness  = 1;

            this.ChartCanvas1.Children.Add(x_axis1);

            var x_axis2 = new Line();
            x_axis2.X1               = Width - ArrowSize;
            x_axis2.X2               = Width;
            x_axis2.Y1               = ZeroY + Padding + ArrowSize;
            x_axis2.Y2               = ZeroY + Padding;
            x_axis2.Stroke           = Brushes.LightSteelBlue;
            x_axis2.StrokeThickness  = 1;

            this.ChartCanvas1.Children.Add(x_axis2);

            #endregion

            foreach (var Diagram in Diagrams.Values.Where(v => v.IsVisible))
            {

                #region Calculations

                var x      = 0;
                var Points = new List<ValuePoints>();

                foreach (var Value in Diagram.Values)
                {
                    Points.Add(new ValuePoints(x, Value, x * XStepping + Padding, ZeroY - Value * YStepping + Padding));
                    x++;
                }

                #endregion

                #region Draw line

                var pl = new Polyline();
                pl.Points           = new PointCollection(Points.Select(v => new Point(v._X, v._Y)));
                pl.Stroke           = Diagram.LineColor;
                pl.StrokeThickness  = Diagram.LineSize;

                // Must be local; may change in .NET 4.5
                var CurrentDiagram1 = Diagram;

                pl.MouseEnter += (s, e) => {
                    pl.Stroke = Brushes.Green;
                };

                pl.MouseLeave += (s, e) => {
                    pl.Stroke = CurrentDiagram1.LineColor;
                };

                this.ChartCanvas1.Children.Add(pl);

                #endregion

                #region Draw values

                //foreach (var Point in Points)
                //{

                //    var circle = new Ellipse();

                //    circle.Fill             = Diagram.ValueFill   (Diagram, Point);
                //    circle.StrokeThickness  = Diagram.ValueBorder (Diagram, Point);
                //    circle.Stroke           = Diagram.ValueStroke (Diagram, Point);
                //    circle.Width            = Diagram.ValueSize   (Diagram, Point);
                //    circle.Height           = Diagram.ValueSize   (Diagram, Point);
                //    circle.ToolTip          = Diagram.ValueToolTip(Diagram, Point);

                //    // Must be local; may change in .NET 4.5
                //    var CurrentDiagram = Diagram;
                //    var CurrentPoint   = Point;

                //    circle.MouseEnter += (s, e) => {
                //        circle.Fill   = CurrentDiagram.ValueFillHL(CurrentDiagram, CurrentPoint);
                //        circle.Width  = circle.Width  * 1.2;
                //        circle.Height = circle.Height * 1.2;
                //        Canvas.SetTop (circle, CurrentPoint._Y - circle.Height / 2);
                //        Canvas.SetLeft(circle, CurrentPoint._X - circle.Width / 2);
                //    };

                //    circle.MouseLeave += (s, e) => {
                //        circle.Fill   = CurrentDiagram.ValueFill(CurrentDiagram, CurrentPoint);
                //        circle.Width  = circle.Width  / 1.2;
                //        circle.Height = circle.Height / 1.2;
                //        Canvas.SetTop (circle, CurrentPoint._Y - circle.Height / 2);
                //        Canvas.SetLeft(circle, CurrentPoint._X - circle.Width / 2);
                //    };

                //    Canvas.SetTop (circle, Point._Y - circle.Height / 2);
                //    Canvas.SetLeft(circle, Point._X - circle.Width  / 2);

                //    this.Children.Add(circle);

                //}

                #endregion

            }

            MouseLine.Y2 = Height;

            this.ChartCanvas1.Children.Add(MouseLine);

        }

        #endregion



        private void DeleteChartButton_Click(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

            var _Parent = this.Parent as ChartStack;

            if (_Parent != null)
            {

                _Parent.RemoveChart(1);

            }

        }


    }

}
