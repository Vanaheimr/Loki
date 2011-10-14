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

#endregion

namespace de.ahzf.Loki.WPFDemo
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// The main window
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();

            #region Customize the vertex and edge captions

            // Vertices caption
            GraphCanvas.VertexCaption = v =>
            {
                Object Name;
                if (v.GetProperty("Name", out Name))
                    return Name as String;
                else
                    return v.Id.ToString();
            };

            #endregion

            var Graph = GraphCanvas.Graph;

            var Alice = Graph.AddVertex(v => v.SetProperty("Name", "Alice"));
            var Bob   = Graph.AddVertex(v => v.SetProperty("Name", "Bob"  ));
            var Carol = Graph.AddVertex(v => v.SetProperty("Name", "Carol"));
            var Dave  = Graph.AddVertex(v => v.SetProperty("Name", "Dave" ));

            var e1    = Graph.AddEdge(Alice, "friends", Bob  );
            var e2    = Graph.AddEdge(Bob,   "friends", Carol);
            var e3    = Graph.AddEdge(Alice, "friends", Carol);
            var e4    = Graph.AddEdge(Carol, "friends", Dave );

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

    }

}
