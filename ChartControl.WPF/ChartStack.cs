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

    public class ChartStack : Grid
    {

        #region Data

        private Dictionary<Byte, ChartControl>  Charts;
        private Dictionary<Byte, RowDefinition> Rows;

        #endregion

        #region Constructor(s)

        public ChartStack()
        {
            this.Charts  = new Dictionary<Byte, ChartControl>();
            this.Rows    = new Dictionary<Byte, RowDefinition>();
        }

        #endregion


        #region AddChart(Background)

        public ChartControl AddChart(Brush Background)
        {

            #region Add a grid splitter between multiple charts

            if (this.RowDefinitions.Count > 0)
            {

                var NewSplitterRow = new RowDefinition() {
                                         Height  = new GridLength(3, GridUnitType.Pixel)
                                     };

                this.Rows.Add((Byte) this.RowDefinitions.Count, NewSplitterRow);
                this.RowDefinitions.Add(NewSplitterRow);

                var NewSplitter = new GridSplitter() {
                                     Background           = Brushes.White,
                                     HorizontalAlignment  = System.Windows.HorizontalAlignment.Stretch,
                                     VerticalAlignment    = System.Windows.VerticalAlignment.Stretch
                                  };

                this.Children.Add(NewSplitter);
                Grid.SetRow(NewSplitter, this.Children.Count - 1);

            }

            #endregion

            #region Add a new chart control

            var NewChartRow = new RowDefinition() {
                                  Height = new GridLength(1, GridUnitType.Star)
                              };

            this.Rows.Add((Byte) this.RowDefinitions.Count, NewChartRow);
            this.RowDefinitions.Add(NewChartRow);

            var NewChart = new ChartControl() {
                               HorizontalAlignment  = System.Windows.HorizontalAlignment.Stretch,
                               VerticalAlignment    = System.Windows.VerticalAlignment.Stretch,
                               Background           = Background,
                               Width                = Double.NaN,
                               Height               = Double.NaN
                           };

            this.Children.Add(NewChart);
            this.Charts.Add((Byte) this.Charts.Count, NewChart);
            Grid.SetRow(NewChart, this.Children.Count - 1);

            return NewChart;

            #endregion

        }

        #endregion

        #region RemoveChart(ChartId)

        public void RemoveChart(Byte ChartId)
        {

            if (ChartId == 0)
            {

                this.RowDefinitions.Remove(Rows[0]);
                this.Rows.Remove(0);

                this.RowDefinitions.Remove(Rows[1]);
                this.Rows.Remove(1);
                this.Charts.Remove(0);

            }

            else
            {

                this.RowDefinitions.Remove(Rows[(Byte)(2 * ChartId - 1)]);
                this.Rows.Remove((Byte)(2 * ChartId - 1));

                this.RowDefinitions.Remove(Rows[(Byte)(2 * ChartId)]);
                this.Rows.Remove((Byte)(2 * ChartId));
                this.Charts.Remove(ChartId);

            }

        }

        #endregion


        #region this[ChartId]

        public ChartControl this[Byte ChartId]
        {
            get
            {
                return Charts[ChartId];
            }
        }

        #endregion

        #region ChartCanvas()

        public IEnumerable<ChartControl> ChartCanvas()
        {

            return from   Chart
                   in     Charts.Values
                   select Chart;

        }

        #endregion

    }

}
