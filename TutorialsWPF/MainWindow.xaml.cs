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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RunCSharp;
using System.Windows.Shapes;

#endregion

namespace TutorialsWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Data

        private          Runner                         _Compiler;
        private          ObservableCollection<RunBlock> History;
        private readonly LinkedList<String>             CommandHistory;
        private          LinkedListNode<String>         CurrentCommand;
        private readonly Int32                          MaxCommandsHistorySize;

        #endregion

        #region Constructor(s)

        public MainWindow()
        {

            InitializeComponent();

            MaxCommandsHistorySize = 50;
            CommandHistory         = new LinkedList<String>();
            CommandHistory.AddLast("");
            CurrentCommand         = CommandHistory.Last;

            GraphCanvas.OnChangedNumberOfVertices += (number) => NumberOfVertices.Text = number + " vertices";
            GraphCanvas.OnChangedNumberOfEdges    += (number) => NumberOfEdges.Text    = number + " edges";
            GraphCanvas.OnChangedMousePosition    += (X, Y)   => MousePosition.Text    = X + " / " + Y;

            _Compiler = new Runner();

            History   = new ObservableCollection<RunBlock>();

            #region Customize the vertex and edge shapes

            //GraphCanvas.VertexShapeCreator = v => {
                
            //    var VertexShape             = new Rectangle();

            //    VertexShape.Stroke          = new SolidColorBrush(Colors.Black);
            //    VertexShape.StrokeThickness = 1;
            //    VertexShape.Width           = v.Id * 10;
            //    VertexShape.Height          = v.Id * 10;
            //    VertexShape.Fill            = new SolidColorBrush(Colors.Red);

            //    return VertexShape;

            //};

            #endregion

            var Graph = GraphCanvas.Graph;

            var Alice = Graph.AddVertex(1, v => v.SetProperty("Name", "Alice"));
            var Bob   = Graph.AddVertex(2, v => v.SetProperty("Name", "Bob"  ));
            var Carol = Graph.AddVertex(3, v => v.SetProperty("Name", "Carol"));

            var e1    = Graph.AddEdge(Alice, Bob,   3, "friends");
            var e2    = Graph.AddEdge(Bob,   Carol, 4, "friends");
            var e3    = Graph.AddEdge(Alice, Carol, 5, "friends");

            #region Customize the vertex and edge tooltips

            // Vertices ToolTip
            GraphCanvas.VertexToolTip = v => {
                Object Name;
                if (v.GetProperty("Name", out Name))
                    return Name as String;
                else
                    return v.Id.ToString();
            };

            // Edges ToolTip
            GraphCanvas.EdgeToolTip = e => e.Label;

            #endregion

        }

        #endregion

        private void expander1_Expanded(Object Sender, RoutedEventArgs RoutedEventArgs)
        {

        }

        #region (private) ResultTextBlock_PreviewMouseLeftButtonDown(Sender, MouseButtonEventArgs)

        private void ResultTextBlock_PreviewMouseLeftButtonDown(Object Sender, MouseButtonEventArgs MouseButtonEventArgs)
        {
            CommandTextBox.Focus();
        }

        #endregion

        #region (private) CommandTextBox_GotFocus(Sender, RoutedEventArgs)

        private void CommandTextBox_GotFocus(Object Sender, RoutedEventArgs RoutedEventArgs)
        {
            CommandTextBox.Foreground = new SolidColorBrush(Colors.Black);
            CommandTextBox.Background = new SolidColorBrush(Color.FromRgb(0xf0, 0xf0, 0xff));
            CommandTextBox.Text       = "";
        }

        #endregion

        #region (private) CommandTextBox_LostFocus(Sender, RoutedEventArgs)

        private void CommandTextBox_LostFocus(Object Sender, RoutedEventArgs RoutedEventArgs)
        {
            CommandTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
            CommandTextBox.Background = new SolidColorBrush(Colors.Transparent);
            CommandTextBox.Text       = "Your C# command...";
        }

        #endregion

        #region (private) CommandTextBox_PreviewKeyDown(Sender, KeyEventArgs)

        private void CommandTextBox_PreviewKeyDown(Object Sender, KeyEventArgs KeyEventArgs)
        {

            #region Escape

            if (KeyEventArgs.Key == Key.Escape)
            {
                CommandTextBox.Text  = "";
                CurrentCommand.Value = "";
                KeyEventArgs.Handled = true;
            }

            #endregion

            #region Up

            else if (KeyEventArgs.Key == Key.Up)
            {
                
                if (CurrentCommand != null && CurrentCommand.Previous != null)
                {
                    CurrentCommand      = CurrentCommand.Previous;
                    CommandTextBox.Text = CurrentCommand.Value;
                }
                
                KeyEventArgs.Handled = true;

            }

            #endregion

            #region Down

            else if (KeyEventArgs.Key == Key.Down)
            {
                
                if (CurrentCommand != null && CurrentCommand.Next != null)
                {
                    CurrentCommand      = CurrentCommand.Next;
                    CommandTextBox.Text = CurrentCommand.Value;
                }

                KeyEventArgs.Handled = true;

            }

            #endregion

            #region Enter

            else if (KeyEventArgs.Key == Key.Enter)
            {

                ResultTextBlock.AppendText("> " + CommandTextBox.Text.Trim() + Environment.NewLine);
                
                var _RunBlock  = new RunBlock(CommandTextBox.Text);
                var _TextRange = new TextRange(ResultTextBlock.Document.ContentEnd, ResultTextBlock.Document.ContentEnd);

                if (_RunBlock.Run(_Compiler))
                {

                    #region Errors...

                    if (_RunBlock.HasErrors)
                    {

                        foreach (var Error in _RunBlock.Errors)
                            _TextRange.Text += Error.Text.Trim() + Environment.NewLine + Environment.NewLine;

                        _TextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));

                        ResultTextBlockScrollBar.ScrollToEnd();
                        ResultTextBlockScrollBar.LineUp();

                        KeyEventArgs.Handled = true;
                        return;

                    }

                    #endregion

                    #region ...output...

                    if (_RunBlock.HasOutput)
                    {
                        _TextRange.Text = _RunBlock.OutputText.Trim() + Environment.NewLine + Environment.NewLine;
                        _TextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                    }

                    #endregion

                    // ...or something like: "var a = 23";

                    #region Add successful command to command history

                    CurrentCommand.Value = CommandTextBox.Text.Trim();
                    CommandHistory.AddLast("");
                    CurrentCommand = CommandHistory.Last;

                    if (CommandHistory.Count > MaxCommandsHistorySize)
                        CommandHistory.RemoveFirst();

                    #endregion

                    CommandTextBox.Text = "";

                }
                else
                {
                    _TextRange.Text = "Syntax error!" + Environment.NewLine + Environment.NewLine;
                    _TextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                }

                ResultTextBlockScrollBar.ScrollToEnd();
                ResultTextBlockScrollBar.LineUp();

                KeyEventArgs.Handled = true;

            }

            #endregion

            CurrentCommand.Value = CommandTextBox.Text.Trim();

        }

        #endregion

    }

}
