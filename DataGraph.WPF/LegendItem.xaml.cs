/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Illias <http://www.github.com/ahzf/Illias>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

#endregion

namespace eu.Vanaheimr.Illias.SQL
{

    /// <summary>
    /// Interaction logic for LegendItem.xaml
    /// </summary>
    public partial class LegendItem : UserControl, IEquatable<LegendItem>
    {

        private readonly Random             Random;
        private readonly Action<LegendItem> RemovalDelegate;

        public String      Legend           { get; private set; }
        public DataChannel DataChannel      { get; private set; }


        public LegendItem()
        {
            InitializeComponent();
        }

        public LegendItem(String Text, DataChannel DataChannel, Action<LegendItem> RemovalDelegate)
            : this()
        {

            this.RemovalDelegate = RemovalDelegate;
            this.DataChannel     = DataChannel;
            this.Random          = new Random(Text.GetHashCode());

            Legend               = Text;
            LegendLabel.Content  = Text;
            ToolTip              = Text;
            LegendRectangle.Fill = new SolidColorBrush(Color.FromRgb((Byte) Random.Next(255), (Byte) Random.Next(255), (Byte) Random.Next(255)));

            this.ContextMenu = new ContextMenu();

            var ContextMenuItem_NewColor = new MenuItem() { Header = "new color" };
            var ContextMenuItem_Remove   = new MenuItem() { Header = "remove" };

            ContextMenuItem_NewColor.Click += new RoutedEventHandler(SetNewColor);
            ContextMenuItem_Remove.Click   += new RoutedEventHandler(RemoveLegendItem); 

            this.ContextMenu.Items.Add(ContextMenuItem_NewColor);
            this.ContextMenu.Items.Add(ContextMenuItem_Remove);

        }


        private void SetNewColor(Object Sender, RoutedEventArgs e)
        {
            LegendRectangle.Fill = new SolidColorBrush(Color.FromRgb((Byte) Random.Next(255), (Byte) Random.Next(255), (Byte) Random.Next(255)));
        }

        private void RemoveLegendItem(Object Sender, RoutedEventArgs e)
        {
            RemovalDelegate(this);
        }


        public Boolean Equals(LegendItem other)
        {
            return LegendLabel.Content.Equals(other.LegendLabel.Content);
        }

    }

}
