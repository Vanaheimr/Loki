using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace eu.Vanaheimr.Loki
{

    public class Diagram
    {

        public String                               Id              { get; private set; }
        public String                               YUnits          { get; private set; }
        public IEnumerable<Double>                  Values          { get; private set; }
        public Boolean                              IsVisible       { get; set; }

        public UInt64                               NumberOfValues  { get; private set; }
        public Double                               MinX            { get; private set; }
        public Double                               MinY            { get; private set; }
        public Double                               MaxX            { get; private set; }
        public Double                               MaxY            { get; private set; }

        public Brush                                LineColor       { get; set; }
        public Double                               LineSize        { get; set; }

        public Func<Diagram, ValuePoints, Brush>    ValueFill       { get; set; }
        public Func<Diagram, ValuePoints, Brush>    ValueStroke     { get; set; }
        public Func<Diagram, ValuePoints, Double>   ValueBorder     { get; set; }
        public Func<Diagram, ValuePoints, Double>   ValueSize       { get; set; }
        public Func<Diagram, ValuePoints, String>   ValueToolTip    { get; set; }

        public Func<Diagram, ValuePoints, Brush>    ValueFillHL     { get; set; }

        public Diagram(String Id, String YUnits, IEnumerable<Double> Values)
        {

            this.Id             = Id;
            this.YUnits         = YUnits;
            this.Values         = Values;
            this.IsVisible      = true;

            this.LineColor      = Brushes.Black;
            this.LineSize       = 1;

            this.ValueFill      = (d, p) => new SolidColorBrush() { Color = Color.FromArgb(255, 255, 255, 0) };
            this.ValueStroke    = (d, p) => Brushes.Black;
            this.ValueBorder    = (d, p) => 1;
            this.ValueSize      = (d, p) => 8;
            this.ValueToolTip   = (d, p) => "(" + p.X + " / " + p.Y + ")";

            this.ValueFillHL    = (d, p) => new SolidColorBrush() { Color = Color.FromArgb(255, 255, 255, 0) };

            foreach (var Y_Value in Values)
            {

                NumberOfValues++;

                if (MinY > Y_Value)
                    MinY = Y_Value;

                if (MaxY < Y_Value)
                    MaxY = Y_Value;

            }

        }

    }


}
