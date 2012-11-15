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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Win32;
using de.ahzf.Illias.SQL;

#endregion

namespace de.ahzf.Illias.SQL
{

    /// <summary>
    /// Interaction logic for DataGraph.xaml
    /// </summary>
    public partial class DataGraph : UserControl
    {

        #region Data

        private String OldOffset;
        private String OldLimit;

        private readonly Brush DisabledColor;
        private readonly Brush EnabledColor;

        #endregion

        #region Properties

        #region LegendItems

        /// <summary>
        /// The legend of the data graph.
        /// </summary>
        public IEnumerable<LegendItem> LegendItems
        {
            get
            {
                return DataGraphLegend.Children.OfType<LegendItem>();
            }
        }

        #endregion

        #region HasLegendItems

        /// <summary>
        /// The legend of the data graph.
        /// </summary>
        public Boolean HasLegendItems
        {
            get
            {
                return DataGraphLegend.Children.OfType<LegendItem>().Any();
            }
        }

        #endregion

        #region AreEnabled

        private Boolean _AreEnabled;

        public Boolean AreEnabled
        {

            get
            {
                return _AreEnabled;
            }

            set
            {

                _AreEnabled = value;

                OffsetTextBox.IsEnabled    = _AreEnabled;
                LimitTextBox.IsEnabled     = _AreEnabled;
                LoadDataButton.IsEnabled   = _AreEnabled;
                //SaveQueryButton.IsEnabled  = _AreEnabled;

                if (IsEnabled)
                {
                    UpdateQueryTextBox();
                    OffsetLabel.Foreground = EnabledColor;
                    LimitLabel.Foreground  = EnabledColor;
                }
                else
                {
                    DataGraphQueryTextBox.Text      = "";
                    OffsetLabel.Foreground = DisabledColor;
                    LimitLabel.Foreground  = DisabledColor;
                }

            }

        }

        #endregion

        #region Offset

        public Nullable<UInt64> Offset
        {
            get
            {
                UInt64 _Offset;
                if (UInt64.TryParse(OffsetTextBox.Text, out _Offset))
                    return _Offset;
                return null;
            }
        }

        #endregion

        #region Limit

        public Nullable<UInt64> Limit
        {
            get
            {
                UInt64 _Limit;
                if (UInt64.TryParse(LimitTextBox.Text, out _Limit))
                    return _Limit;
                return null;
            }
        }

        #endregion

        #region ConstructQueryDelegate

        public Func<String>                          ConstructQueryDelegate         { get; set; }
        public Func<String, String, Boolean, String> ConstructStorageQueryDelegate  { get; set; }

        #endregion

        #region DisableGraph

        public Boolean DisableGraph
        {

            get
            {
                return DataGridTabControl.Items.Contains(GraphTab);
            }

            set
            {

                if (value)
                {
                    if (DataGridTabControl.Items.Contains(GraphTab))
                        DataGridTabControl.Items.Remove(GraphTab);
                }
                else
                    DataGridTabControl.Items.Add(GraphTab);

            }

        }

        #endregion

        public Func<String, String> StoreQueryTransformator { get; set; }

        #endregion

        #region Events

        #region SomeThingChanged

        public delegate void MinEventHandler(Object Sender);

        public MinEventHandler SomeThingChanged { get; set; }

        #endregion

        #region OnQueryButtonClicked

        public delegate void QueryButtonClickedEventHandler(DataGrid DataGrid);

        public event QueryButtonClickedEventHandler OnLoadDataButtonClicked;

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new data graph.
        /// </summary>
        public DataGraph()
        {

            InitializeComponent();

            this.ConstructQueryDelegate = () => "";

            this.DisabledColor = new SolidColorBrush(Color.FromRgb(130, 130, 130));
            this.EnabledColor  = new SolidColorBrush(Color.FromRgb(  0,   0,   0));

            DataGraphGrid.PreviewMouseWheel += DataGraphGrid_MouseWheel;

        }

        #endregion


        #region OffsetTextBox

        private void OffsetTextBox_TextChanged(Object Sender, TextChangedEventArgs e)
        {

            // Especially for mouse paste events...

            var RegExpr = new Regex("^[0-9]*$", RegexOptions.Compiled);

            if (!RegExpr.IsMatch(OffsetTextBox.Text))
                OffsetTextBox.Text = OldOffset;
            else
                OldOffset = OffsetTextBox.Text;

            UpdateQueryTextBox();

        }

        private void OffsetTextBox_PreviewTextInput(Object Sender, TextCompositionEventArgs e)
        {

            var RegExpr = new Regex("^[0-9]+$", RegexOptions.Compiled);

            if (!RegExpr.IsMatch(e.Text))
                e.Handled = true;

            UpdateQueryTextBox();

        }

        #endregion

        #region LimitTextBox

        private void LimitTextBox_TextChanged(Object Sender, TextChangedEventArgs e)
        {

            // Especially for mouse paste events...

            var RegExpr = new Regex("^[0-9]*$", RegexOptions.Compiled);

            if (!RegExpr.IsMatch(LimitTextBox.Text))
                LimitTextBox.Text = OldLimit;
            else
                OldLimit = LimitTextBox.Text;

            UpdateQueryTextBox();

        }

        private void LimitTextBox_PreviewTextInput(Object Sender, TextCompositionEventArgs e)
        {

            var RegExpr = new Regex("^[0-9]+$", RegexOptions.Compiled);

            if (!RegExpr.IsMatch(e.Text))
                e.Handled = true;

            UpdateQueryTextBox();

        }

        #endregion


        public void UpdateQueryTextBox()
        {
            if (ConstructQueryDelegate != null)
                DataGraphQueryTextBox.Text = ConstructQueryDelegate();
        }


        #region DataGraphGrid_MouseWheel(Sender, MouseWheelEventArgs MouseWheelEventArgs)

        private void DataGraphGrid_MouseWheel(Object Sender, MouseWheelEventArgs MouseWheelEventArgs)
        {
            DataGridScroller.ScrollToVerticalOffset(DataGridScroller.VerticalOffset - MouseWheelEventArgs.Delta / 3);
        }

        #endregion

        private void SomeThingChanged_private()
        {
            if (this.SomeThingChanged != null)
                SomeThingChanged(this);
        }


        #region AddLegendItem(Text, DataChannel, DBColumn)

        public Boolean AddLegendItem(String Text, DataChannel DataChannel)
        {

            var NewItem = new LegendItem(Text, DataChannel, item => { DataGraphLegend.Children.Remove(item); SomeThingChanged_private(); });
            var Found = false;

            foreach (var Item in DataGraphLegend.Children)
            {
                if (NewItem.Equals(Item as LegendItem))
                    Found = true;
            }

            if (!Found)
                DataGraphLegend.Children.Add(NewItem);

            SomeThingChanged_private();

            return Found;

        }

        #endregion

        public void ClearLegend()
        {
            DataGraphLegend.Children.Clear();
        }

        public void Clear()
        {
            DataGraphQueryTextBox.Clear();
            DataGraphLegend.Children.Clear();
            DataGraphGrid.Columns.Clear();
        }

        #region LoadDataButton_Click(Sender, RoutedEventArgs)

        private void LoadDataButton_Click(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

            if (OnLoadDataButtonClicked != null)
                OnLoadDataButtonClicked(this.DataGraphGrid);

        }

        #endregion


        #region LoadQueryButton_Click(Sender, RoutedEventArgs)

        private void LoadQueryButton_Click(Object Sender, RoutedEventArgs RoutedEventArgs)
        {
        }

        #endregion

        #region SaveQueryButton_Click(Sender, RoutedEventArgs)

        private void SaveQueryButton_Click(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

            var _SaveFileDialog = new SaveFileDialog();
            _SaveFileDialog.FileName          = "myQuery";
            _SaveFileDialog.DefaultExt        = ".dbquery";
            _SaveFileDialog.Filter            = "DB query files (.dbquery)|*.dbquery|All Files|*.*";
            _SaveFileDialog.InitialDirectory  = Directory.GetCurrentDirectory();

            // Show save file dialog box
            var result = _SaveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {

                var file = new FileInfo(_SaveFileDialog.FileName);
                if (file.Exists)
                    file.Delete();

                var QueryString = ConstructStorageQueryDelegate("$StartDate", "$EndDate", false);

                if (StoreQueryTransformator != null)
                    QueryString = StoreQueryTransformator(QueryString);

                File.WriteAllText(_SaveFileDialog.FileName, QueryString);

            }

        }

        #endregion


    }

}
