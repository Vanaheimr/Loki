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

    public class ValuePoints
    {

        public Double X  { get; private set; }
        public Double Y  { get; private set; }
        public Double _X { get; private set; }
        public Double _Y { get; private set; }

        public ValuePoints(Double X, Double Y, Double _X, Double _Y)
        {
            this.X = X;
            this.Y = Y;
            this._X = _X;
            this._Y = _Y;
        }

    }

}
