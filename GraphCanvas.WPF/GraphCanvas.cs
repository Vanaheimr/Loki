/*
 * Copyright (c) 2011-2012 Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Balder;
using eu.Vanaheimr.Balder.Schema;
using eu.Vanaheimr.Balder.InMemory;

#endregion

namespace eu.Vanaheimr.Loki
{

    #region GraphCanvasFactory

    /// <summary>
    /// Graph canvas helper.
    /// </summary>
    public static class GraphCanvasFactory
    {

        #region BuildGraphCanvas<...>(Graph, ..., PropertyKeys,...)

        /// <summary>
        /// Creates a new graph canvas for visualizing the given generic property graph.
        /// </summary>
        /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
        /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
        /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
        /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
        /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
        /// 
        /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
        /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
        /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
        /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
        /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
        /// 
        /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
        /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
        /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
        /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
        /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
        /// 
        /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
        /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
        /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
        /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
        /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
        /// 
        /// <param name="Graph">A generic property graph.</param>
        /// <param name="GraphCanvas_PropertyKey">The property key for storing the graph canvas.</param>
        /// 
        /// <param name="VertexX_PropertyKey">The property key for storing the x position of a vertex user control.</param>
        /// <param name="VertexY_PropertyKey">The property key for storing the y position of a vertex user control.</param>
        /// <param name="VertexZ_PropertyKey">The property key for storing the z position of a vertex user control.</param>
        /// 
        /// <param name="VertexControl_PropertyKey">The property key for storing the vertex user control.</param>
        /// <param name="EdgeControl_PropertyKey">The property key for storing the edge user control.</param>
        /// <param name="MultiEdgeControl_PropertyKey">The property key for storing the multiedge user control.</param>
        /// <param name="HyperEdgeControl_PropertyKey">The property key for storing the hyperedge user control.</param>
        public static GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            BuildGraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                TKeyVertex     GraphCanvas_PropertyKey,

                TKeyVertex     VertexX_PropertyKey,
                TKeyVertex     VertexY_PropertyKey,
                TKeyVertex     VertexZ_PropertyKey,

                TKeyVertex     VertexControl_PropertyKey,
                TKeyEdge       EdgeControl_PropertyKey,
                TKeyMultiEdge  MultiEdgeControl_PropertyKey,
                TKeyHyperEdge  HyperEdgeControl_PropertyKey)


            where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
            where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
            where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
            where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

            where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
            where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
            where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
            where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

            where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
            where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
            where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
            where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

        {

            return new GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                        Graph,
                        GraphCanvas_PropertyKey,

                        VertexX_PropertyKey,
                        VertexY_PropertyKey,
                        VertexZ_PropertyKey,

                        VertexControl_PropertyKey,
                        EdgeControl_PropertyKey,
                        MultiEdgeControl_PropertyKey,
                        HyperEdgeControl_PropertyKey);

        }

        #endregion

        #region BuildSchemaGraphCanvas<...>(Graph, SchemaGraphId, ..., PropertyKeys,...)

        /// <summary>
        /// Creates a new graph canvas for visualizing the schema
        /// of the given generic property graph.
        /// </summary>
        /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
        /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
        /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
        /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
        /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
        /// 
        /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
        /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
        /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
        /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
        /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
        /// 
        /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
        /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
        /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
        /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
        /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
        /// 
        /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
        /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
        /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
        /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
        /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
        /// 
        /// <param name="Graph">A generic property graph.</param>
        /// <param name="SchemaGraphId">The schema graph identification.</param>
        /// <param name="GraphCanvas_PropertyKey">The property key for storing the graph canvas.</param>
        /// 
        /// <param name="VertexX_PropertyKey">The property key for storing the x position of a vertex user control.</param>
        /// <param name="VertexY_PropertyKey">The property key for storing the y position of a vertex user control.</param>
        /// <param name="VertexZ_PropertyKey">The property key for storing the z position of a vertex user control.</param>
        /// 
        /// <param name="VertexControl_PropertyKey">The property key for storing the vertex user control.</param>
        /// <param name="EdgeControl_PropertyKey">The property key for storing the edge user control.</param>
        /// <param name="MultiEdgeControl_PropertyKey">The property key for storing the multiedge user control.</param>
        /// <param name="HyperEdgeControl_PropertyKey">The property key for storing the hyperedge user control.</param>
        public static GraphCanvas<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                  TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                  TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                  THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>

            BuildSchemaGraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                TVertexLabel   SchemaGraphId,
                TKeyVertex     GraphCanvas_PropertyKey,

                TKeyVertex     VertexX_PropertyKey,
                TKeyVertex     VertexY_PropertyKey,
                TKeyVertex     VertexZ_PropertyKey,

                TKeyVertex     VertexControl_PropertyKey,
                TKeyEdge       EdgeControl_PropertyKey,
                TKeyMultiEdge  MultiEdgeControl_PropertyKey,
                TKeyHyperEdge  HyperEdgeControl_PropertyKey)


            where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
            where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
            where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
            where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

            where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
            where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
            where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
            where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

            where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
            where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
            where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
            where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

        {

            return new GraphCanvas<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                   TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                   TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                   THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>(

                        Graph.StrictSchemaGraph(SchemaGraphId),
                        GraphCanvas_PropertyKey,

                        VertexX_PropertyKey,
                        VertexY_PropertyKey,
                        VertexZ_PropertyKey,

                        VertexControl_PropertyKey,
                        EdgeControl_PropertyKey,
                        MultiEdgeControl_PropertyKey,
                        HyperEdgeControl_PropertyKey);

        }

        #endregion

    }

    #endregion


    #region Non-generic GraphCanvas

    /// <summary>
    /// Creates a new canvas for visualizing a non-generic property graph.
    /// </summary>
    public class GraphCanvas : GraphCanvas<UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object>
    {

        /// <summary>
        /// Creates a new canvas for visualizing a non-generic property graph.
        /// </summary>
        public GraphCanvas()
            : base(GraphFactory.CreateGenericPropertyGraph(1),
                   "GraphCanvas",
                   "X", "Y", "Z",
                   "VertexShape", "EdgeShape", "MultiEdgeShape", "HyperEdgeShape")
        { }

    }

    #endregion

    #region Non-generic SchemaGraphCanvas

    /// <summary>
    /// Creates a new canvas for visualizing a non-generic property graph.
    /// </summary>
    public class SchemaGraphCanvas : GraphCanvas<String, Int64, VertexLabel,    String, Object,
                                                 String, Int64, EdgeLabel,      String, Object,
                                                 String, Int64, MultiEdgeLabel, String, Object,
                                                 String, Int64, HyperEdgeLabel, String, Object>
    {

        /// <summary>
        /// Creates a new canvas for visualizing a non-generic property graph.
        /// </summary>
        public SchemaGraphCanvas()
            : base(GraphFactory.CreateGenericPropertyGraph_WithStringIds("1").StrictSchemaGraph("2"),
                   "GraphCanvas",
                   "X", "Y", "Z",
                   "VertexShape", "EdgeShape", "MultiEdgeShape", "HyperEdgeShape")
        { }

    }

    #endregion

    #region Generic GraphCanvas<...>

    /// <summary>
    /// Creates a new canvas for visualizing a generic property graph.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public class GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> : Canvas

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        private Random  Random;
        private Point   SavedMousePosition;
        private VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SelectedVertexControl;

        private IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SelectedVertex;

        private String CurrentDirectory;

        #endregion

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph { get; private set; }

        #endregion


        #region PropertyKeys for user controls

        /// <summary>
        /// The property key for storing the graph canvas.
        /// </summary>
        public TKeyVertex     GraphCanvas_PropertyKey        { get; private set; }


        /// <summary>
        /// The property key for storing the x position of a vertex user control.
        /// </summary>
        public TKeyVertex     VertexX_PropertyKey            { get; private set; }

        /// <summary>
        /// The property key for storing the y position of a vertex user control.
        /// </summary>
        public TKeyVertex     VertexY_PropertyKey            { get; private set; }

        /// <summary>
        /// The property key for storing the z position of a vertex user control.
        /// </summary>
        public TKeyVertex     VertexZ_PropertyKey            { get; private set; }


        /// <summary>
        /// The property key for storing the vertex user control.
        /// </summary>
        public TKeyVertex     VertexControl_PropertyKey      { get; private set; }

        /// <summary>
        /// The property key for storing the edge user control.
        /// </summary>
        public TKeyEdge       EdgeControl_PropertyKey        { get; private set; }

        /// <summary>
        /// The property key for storing the multiedge user control.
        /// </summary>
        public TKeyMultiEdge  MultiEdgeControl_PropertyKey   { get; private set; }

        /// <summary>
        /// The property key for storing the hyperedge user control.
        /// </summary>
        public TKeyHyperEdge  HyperEdgeControl_PropertyKey   { get; private set; }

        #endregion


        #region VertexControlCreator

        private VertexControlCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given vertex.
        /// </summary>
        public VertexControlCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexControlCreator
        {

            get
            {
                return _VertexControlCreator;
            }

            set
            {
                if (value != null)
                    _VertexControlCreator = value;
            }

        }

        #endregion

        #region VertexCaption

        private VertexCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCaption;

        /// <summary>
        /// A delegate for generating caption for the given vertex.
        /// </summary>
        public VertexCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCaption
        {
            
            get
            {
                return _VertexCaption;
            }

            set
            {
                if (value != null)
                    _VertexCaption = value;
            }
        
        }

        #endregion

        #region VertexToolTip

        private VertexToolTipDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given vertex.
        /// </summary>
        public VertexToolTipDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexToolTip
        {
            
            get
            {
                return _VertexToolTip;
            }

            set
            {
                if (value != null)
                {

                    _VertexToolTip = value;

                    UserControl VertexControl;
                    TValueVertex VertexControlProperty;
                    foreach (var Vertex in Graph.Vertices())
                    {
                        if (Vertex.TryGetProperty(this.VertexControl_PropertyKey, out VertexControlProperty))
                        {

                            VertexControl = VertexControlProperty as UserControl;

                            if (VertexControl != null)
                                VertexControl.ToolTip = VertexToolTip(Vertex);

                        }
                    }

                }
            }

        }

        #endregion


        #region EdgeControlCreator

        private EdgeControlCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeControlCreator;

        /// <summary>
        /// A delegate for creating a control for the given edge.
        /// </summary>
        public EdgeControlCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeControlCreator
        {

            get
            {
                return _EdgeControlCreator;
            }

            set
            {
                if (value != null)
                    _EdgeControlCreator = value;
            }

        }

        #endregion

        #region EdgeCaption

        private EdgeCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCaption;

        /// <summary>
        /// A delegate for generating caption for the given edge.
        /// </summary>
        public EdgeCaptionDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCaption
        {

            get
            {
                return _EdgeCaption;
            }

            set
            {
                if (value != null)
                    _EdgeCaption = value;
            }
        
        }

        #endregion

        #region EdgeToolTip

        private EdgeToolTipDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeToolTip;

        /// <summary>
        /// A delegate for generating a tooltip for the given edge.
        /// </summary>
        public EdgeToolTipDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeToolTip
        {
            
            get
            {
                return _EdgeToolTip;
            }

            set
            {
                if (value != null)
                {

                    _EdgeToolTip = value;

                    UserControl EdgeControl;
                    TValueEdge EdgeControlProperty;
                    foreach (var Edge in Graph.Edges())
                    {
                        if (Edge.TryGetProperty(this.EdgeControl_PropertyKey, out EdgeControlProperty))
                        {

                            EdgeControl = EdgeControlProperty as UserControl;

                            if (EdgeControl != null)
                                EdgeControl.ToolTip = EdgeToolTip(Edge);

                        }
                    }

                }
            }
        }

        #endregion

        #endregion

        #region Events

        #region NumberOfVertices

        /// <summary>
        /// Called whenever the number of vertices changed.
        /// </summary>
        public event ChangedNumberOfVertices OnChangedNumberOfVertices;

        #endregion

        #region NumberOfEdges

        /// <summary>
        /// Called whenever the number of edges changed.
        /// </summary>
        public event ChangedNumberOfEdges OnChangedNumberOfEdges;

        #endregion

        #region MousePosition

        /// <summary>
        /// Called whenever the mouse moved.
        /// </summary>
        public event ChangedMousePosition OnChangedMousePosition;

        #endregion

        #endregion

        #region Constructor(s)

        #region GraphCanvas(Graph, GraphCanvasPropertyKey, VertexX, VertexY, VertexZ, VertexControlPropertyKey, EdgeControlPropertyKey, MultiEdgeControlPropertyKey, HyperEdgeControlPropertyKey)

        /// <summary>
        /// Creates a new canvas for visualizing the given property graph.
        /// </summary>
        /// <param name="Graph">The generic property graph to visualize.</param>
        /// <param name="GraphCanvasPropertyKey">The property key for storing the graph canvas.</param>
        /// 
        /// <param name="VertexX">The property key for storing the x position of a vertex user control.</param>
        /// <param name="VertexY">The property key for storing the y position of a vertex user control.</param>
        /// <param name="VertexZ">The property key for storing the z position of a vertex user control.</param>
        /// 
        /// <param name="VertexControlPropertyKey">The property key for storing the vertex user control.</param>
        /// <param name="EdgeControlPropertyKey">The property key for storing the edge user control.</param>
        /// <param name="MultiEdgeControlPropertyKey">The property key for storing the multiedge user control.</param>
        /// <param name="HyperEdgeControlPropertyKey">The property key for storing the hyperedge user control.</param>
        public GraphCanvas(IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                           TKeyVertex     GraphCanvasPropertyKey,

                           TKeyVertex     VertexX,
                           TKeyVertex     VertexY,
                           TKeyVertex     VertexZ,

                           TKeyVertex     VertexControlPropertyKey,
                           TKeyEdge       EdgeControlPropertyKey,
                           TKeyMultiEdge  MultiEdgeControlPropertyKey,
                           TKeyHyperEdge  HyperEdgeControlPropertyKey)

        {

            this.ClipToBounds                  = true;

            this.Graph                         = Graph;
            this.GraphCanvas_PropertyKey       = GraphCanvasPropertyKey;
            this.VertexX_PropertyKey           = VertexX;
            this.VertexY_PropertyKey           = VertexY;
            this.VertexZ_PropertyKey           = VertexZ;
            this.VertexControl_PropertyKey     = VertexControlPropertyKey;
            this.EdgeControl_PropertyKey       = EdgeControlPropertyKey;
            this.MultiEdgeControl_PropertyKey  = MultiEdgeControlPropertyKey;
            this.HyperEdgeControl_PropertyKey  = HyperEdgeControlPropertyKey;

            Graph.Set(GraphCanvasPropertyKey, (TValueVertex) (Object) this);
            DataContext                        = Graph;
            Random                             = new Random();

            this.Background                    = new SolidColorBrush(Colors.Transparent);
            this.MouseMove                    += GraphCanvas_MouseMove;
            this.MouseLeave                   += GraphCanvas_MouseLeave;

            Graph.OnVertexAddition.OnNotification += (g, v) => AddVertex(g, v as IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>);

            Graph.OnEdgeAddition.OnNotification   += (g, e) => AddEdge  (g, e as IGenericPropertyEdge  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>);

            _VertexControlCreator             = DefaultVertexControlCreator;
            _VertexCaption                    = DefaultVertexCaption;
            _VertexToolTip                    = DefaultVertexToolTip;

            _EdgeControlCreator               = DefaultEdgeControlCreator;
            _EdgeCaption                      = DefaultEdgeCaption;
            _EdgeToolTip                      = DefaultEdgeToolTip;

            CurrentDirectory                  = Directory.GetCurrentDirectory();

            AddGraphCanvasContextMenu();

            this.SizeChanged                 += new SizeChangedEventHandler(GraphCanvas_SizeChanged);

        }

        #endregion


        #region GraphCanvas_SizeChanged(Sender, SizeChangedEventArgs)

        void GraphCanvas_SizeChanged(Object Sender, SizeChangedEventArgs SizeChangedEventArgs)
        {

            //ToDo: A bit of a hack... but working for now!

            this.Graph.Vertices().ForEach(Vertex => {

                Double Pos = 0;

                var VertexControl = Vertex[VertexControl_PropertyKey] as VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;




                if (Vertex.TryGetProperty<Double>(VertexX_PropertyKey, out Pos))
                    VertexControl.X = Pos;

                else
                {

                    var posx = (Double) Random.Next(20, Convert.ToInt32(Math.Abs(this.ActualWidth)) - 20);

                    Vertex.AsMutable().Set(VertexX_PropertyKey, (TValueVertex) (Object) posx);
                    VertexControl.X = posx;

                }


                if (Vertex.TryGetProperty<Double>(VertexY_PropertyKey, out Pos))
                    VertexControl.Y = Pos;

                else
                {

                    var posy = (Double) Random.Next(20, Convert.ToInt32(Math.Abs(this.ActualHeight)) - 20);

                    Vertex.AsMutable().Set(VertexY_PropertyKey, (TValueVertex) (Object) posy);
                    VertexControl.Y = posy;

                }

                UpdateEdgesControls(Vertex);

            });

            this.SizeChanged -= new SizeChangedEventHandler(GraphCanvas_SizeChanged);

        }

        #endregion

        #endregion


        // Graph canvas

        #region GraphCanvas_MouseLeave(Sender, MouseEventArgs)

        private void GraphCanvas_MouseLeave(Object Sender, MouseEventArgs MouseEventArgs)
        {
            SavedMousePosition                 = MouseEventArgs.GetPosition(this);
            SelectedVertexControl = null;
        }

        #endregion


        // Vertices

        #region (private) AddVertex(Graph, Vertex)

        private void AddVertex(IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                               IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            if (Vertex != null)
            {

                var VertexControl                   = _VertexControlCreator(this, Vertex);
                VertexControl.MouseMove            += GraphCanvas_MouseMove;
                VertexControl.MouseLeftButtonDown  += VertexControl_MouseLeftButtonDown;
                VertexControl.MouseLeftButtonUp    += VertexControl_MouseLeftButtonUp;
                VertexControl.DataContext           = Vertex;
                Vertex.Set(this.VertexControl_PropertyKey, (TValueVertex) (Object) VertexControl);

                VertexControl.VertexCaption         = _VertexCaption;
                VertexControl.ToolTip               = VertexToolTip(Vertex);

                Children.Add(VertexControl);

                if (OnChangedNumberOfVertices != null)
                    OnChangedNumberOfVertices(Graph.NumberOfVertices());

            }

        }

        #endregion

        #region (static)  DefaultVertexControlCreator(Vertex)

        /// <summary>
        /// Returns the default control for the given vertex,
        /// which is a constant sized circle.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                                    DefaultVertexControlCreator(GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,

                                                                IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            var VertexControl             = new VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(GraphCanvas, Vertex);

            VertexControl.Fill            = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));
            VertexControl.Stroke          = new Pen(new SolidColorBrush(Colors.Black), 1.0);
            VertexControl.Width           = 30;
            VertexControl.Height          = 30;
            VertexControl.ShowCaption     = true;

            return VertexControl;

        }

        #endregion

        #region (static)  DefaultVertexCaption(Vertex)

        /// <summary>
        /// Returns the default caption for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexCaption(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {
            return Vertex.Id.ToString();
        }

        #endregion

        #region (static)  DefaultVertexToolTip(Vertex)

        /// <summary>
        /// Returns the default tooltip for the given vertex control.
        /// </summary>
        /// <param name="Vertex">A property vertex.</param>
        public static String DefaultVertexToolTip(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {
            return "VertexId: " + Vertex.Id + " [" + Vertex.OutDegree() + " OutEdges, " + Vertex.InDegree() + " InEdges]";
        }

        #endregion

        #region (private) VertexControl_MouseLeftButtonDown(Sender, MouseButtonEventArgs)

        private void VertexControl_MouseLeftButtonDown(Object Sender, MouseButtonEventArgs MouseButtonEventArgs)
        {

            SavedMousePosition     = MouseButtonEventArgs.GetPosition(this);

            SelectedVertexControl  = Sender as VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            SelectedVertex         = SelectedVertexControl.DataContext as IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

        }

        #endregion

        #region (private) VertexControl_MouseLeftButtonUp(Sender, MouseButtonEventArgs)

        private void VertexControl_MouseLeftButtonUp(Object sender, MouseButtonEventArgs MouseButtonEventArgs)
        {
            SavedMousePosition                  = MouseButtonEventArgs.GetPosition(this);
            SelectedVertexControl  = null;
        }

        #endregion

        #region (private) GraphCanvas_MouseMove(Sender, MouseEventArgs)

        private void GraphCanvas_MouseMove(Object Sender, MouseEventArgs MouseEventArgs)
        {

            var CurrentMousePosition = MouseEventArgs.GetPosition(this);

            #region If the mouse is dragging a vertex control: Move all adjacent edges

            if (SelectedVertexControl != null)
            {

                #region Move the vertex control

                Double XPos, YPos = 0.0;

                if (SelectedVertex.TryGetProperty<Double>(VertexX_PropertyKey, out XPos)) {
                    XPos -= (SavedMousePosition.X - CurrentMousePosition.X);
                    SelectedVertexControl.X = XPos;
                    SelectedVertex.Set(VertexX_PropertyKey, (TValueVertex) (Object) XPos);
                }

                if (SelectedVertex.TryGetProperty<Double>(VertexY_PropertyKey, out YPos))
                {
                    YPos -= (SavedMousePosition.Y - CurrentMousePosition.Y);
                    SelectedVertexControl.Y = YPos;
                    SelectedVertex.Set(VertexY_PropertyKey, (TValueVertex) (Object) YPos);
                }

                #endregion

                UpdateEdgesControls(SelectedVertex);

                SavedMousePosition = CurrentMousePosition;

            }

            #endregion

            if (OnChangedMousePosition != null)
                OnChangedMousePosition(CurrentMousePosition.X, CurrentMousePosition.Y);

        }

        #endregion


        // Edges

        #region (private) AddEdge(Graph, Edge)

        private void AddEdge(IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                             IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (Edge != null)
            {

                var EdgeControl             = _EdgeControlCreator(this, Edge);
                EdgeControl.EdgeCaption     = _EdgeCaption;
                EdgeControl.ToolTip         = EdgeToolTip(Edge);

                Canvas.SetZIndex(EdgeControl, -99);
                Children.Add(EdgeControl);

                Edge.Set(this.EdgeControl_PropertyKey, (TValueEdge) (Object)  EdgeControl);


                var OutVertexControl = Edge.OutVertex.GetProperty(this.VertexControl_PropertyKey) as VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
                var InVertexControl  = Edge.InVertex. GetProperty(this.VertexControl_PropertyKey) as VertexControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

                if (OutVertexControl != null)
                    OutVertexControl.ToolTip = DefaultVertexToolTip(Edge.OutVertex);

                if (InVertexControl != null)
                    InVertexControl.ToolTip  = DefaultVertexToolTip(Edge. InVertex);

                if (OnChangedNumberOfEdges != null)
                    OnChangedNumberOfEdges(Graph.NumberOfEdges());

            }

        }

        #endregion

        #region (static)  DefaultEdgeControlCreator(Vertex)

        /// <summary>
        /// Returns the default control for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                      DefaultEdgeControlCreator(GraphCanvas<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphCanvas,

                                                IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            var EdgeControl                 = new EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(GraphCanvas, Edge);
            //VertexShape.Stroke              = new SolidColorBrush(Colors.Black);
            //VertexShape.StrokeThickness     = 1;
            EdgeControl.HeadWidth           = 12;
            EdgeControl.HeadHeight          = 8;
            //EdgeShape.Stroke                = new SolidColorBrush(Colors.Black);
            //EdgeShape.StrokeThickness       = 2;
            EdgeControl.ShowCaption         = true;
            //VertexShape.Fill                = new SolidColorBrush(Color.FromArgb(0xCC, 0xff, 0x00, 0x00));

            return EdgeControl;

        }

        #endregion

        #region (static)  DefaultEdgeCaption(Edge)

        /// <summary>
        /// Returns the default caption for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static String DefaultEdgeCaption(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {
            return Edge.Label.ToString();
        }

        #endregion

        #region (static)  DefaultEdgeToolTip(Edge)

        /// <summary>
        /// Returns the default tooltip for the given edge.
        /// </summary>
        /// <param name="Edge">A property edge.</param>
        public static String DefaultEdgeToolTip(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {
            return "EdgeId: " + Edge.Id + " [OutVertexId: " + Edge.OutVertex.Id.ToString() + ", InVertexId: " + Edge.InVertex.Id.ToString() + "]";
        }

        #endregion


        #region (private) AddGraphCanvasContextMenu()

        private void AddGraphCanvasContextMenu()
        {

            // Must be here... do not why!
            this.ContextMenu = new ContextMenu();

            var ClearGraph = new MenuItem()
            {
                Header = "Clear graph"
            };
            ClearGraph.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(ClearGraph);

            var LoadGraph = new MenuItem()
            {
                Header = "Load graph..."
            };
            LoadGraph.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(LoadGraph);

            var SaveGraphAs = new MenuItem()
            {
                Header = "Save graph as..."
            };
            SaveGraphAs.Click += new RoutedEventHandler(SaveAs_Click);
            this.ContextMenu.Items.Add(SaveGraphAs);

        }

        #endregion


        #region (private) SaveAs_Click(sender, e)

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

          //  MessageBox.Show("Size: " + GraphCanvas.Width + " x " + GraphCanvas.Height);

            var SaveFileDialog              = new Microsoft.Win32.SaveFileDialog();
            SaveFileDialog.Filter           = "All files (*.*)|*.*|PNG files (*.png)|*.png*|JPEG files (*.jpg, *.jpeg)|*.jpg*;*.jpeg|XAML files (*.xaml)|*.xaml*";
            SaveFileDialog.FilterIndex      = 0;
            SaveFileDialog.AddExtension     = true;
            SaveFileDialog.InitialDirectory = CurrentDirectory;
            SaveFileDialog.Title            = "Choose a filename and a location...";
            SaveFileDialog.CheckPathExists  = true;

            var _Dialog = SaveFileDialog.ShowDialog();
            if (_Dialog.HasValue && _Dialog.Value)
            {
                try
                {

                    CurrentDirectory = SaveFileDialog.FileName.Substring(0, SaveFileDialog.FileName.LastIndexOf(System.IO.Path.DirectorySeparatorChar));

                    switch (SaveFileDialog.FilterIndex)
                    {

                        case 2:
                            if (!SaveFileDialog.FileName.EndsWith(".png"))
                                SaveFileDialog.FileName += ".png";
                            break;

                        case 3:
                            if (!(SaveFileDialog.FileName.EndsWith(".jpg") ||
                                  SaveFileDialog.FileName.EndsWith(".jpeg")))
                                SaveFileDialog.FileName += ".jpg";
                            break;

                        case 4:
                            if (!SaveFileDialog.FileName.EndsWith(".xaml"))
                                SaveFileDialog.FileName += ".xaml";
                            break;

                        default:
                            if (SaveFileDialog.FileName.EndsWith(".png"))
                                SaveFileDialog.FilterIndex = 2;
                            else if (SaveFileDialog.FileName.EndsWith(".jpg"))
                                SaveFileDialog.FilterIndex = 3;
                            else if (SaveFileDialog.FileName.EndsWith(".jpeg"))
                                SaveFileDialog.FilterIndex = 3;
                            else if (SaveFileDialog.FileName.EndsWith(".xaml"))
                                SaveFileDialog.FilterIndex = 4;
                            else
                            {
                                MessageBox.Show("A problem occured, try again later!");
                                return;
                            }
                            break;

                    }

                    using (var _FileStream = File.Create(SaveFileDialog.FileName))
                    {
                        switch (SaveFileDialog.FilterIndex)
                        {

                            case 1: break;
                            case 2: this.SaveAsPNG (                  dpiX: 300, dpiY: 300).WriteTo(_FileStream); break;
                            case 3: this.SaveAsJPEG(QualityLevel: 98, dpiX: 300, dpiY: 300).WriteTo(_FileStream); break;
                            case 4: this.SaveAsXAML(Indent: true); break;

                            default:
                                MessageBox.Show("Error occurred during XAML saving.",
                                                "Error",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                                break;

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Cancel!", "Error", MessageBoxButton.OK, MessageBoxImage .Error);
            }

        }

        #endregion


        #region (private) UpdateEdgesControls(Vertex)

        private void UpdateEdgesControls(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeControl = null;


            // Outedges
            Vertex.OutEdges().ForEach(OutEdge => {

                _EdgeControl = OutEdge.GetCastedProperty<TKeyEdge,
                                                         TValueEdge,
                                                         EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(this.EdgeControl_PropertyKey);

                _EdgeControl.ShowDirection = !_EdgeControl.ShowDirection;
                _EdgeControl.Refresh = true;

            });


            // InEdges
            Vertex.InEdges().ForEach(InEdge => {

                _EdgeControl = InEdge.GetCastedProperty<TKeyEdge,
                                                        TValueEdge,
                                                        EdgeControl<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(this.EdgeControl_PropertyKey);

                _EdgeControl.ShowDirection = !_EdgeControl.ShowDirection;
                _EdgeControl.Refresh = true;

            });

        }

        #endregion



        #region SchemaGraphCanvas(SchemaGraphId, SchemaGraphDescription = null, ContinuousLearning = true, EnforceSchema = false)

        /// <summary>
        /// Creates a new graph canvas for visualizing the schema used within
        /// the given generic property graph canvas.
        /// </summary>
        /// <param name="SchemaGraphId">The schema graph identification.</param>
        /// <param name="SchemaGraphDescription">An optional description of the schema graph.</param>
        /// <param name="ContinuousLearning">If set to true, the schema graph will subsribe vertex/edge additions in order to continuously learn the graph schema.</param>
        /// <param name="EnforceSchema">Disallow the 'continous learning' and any changes of the graph schema after setting up the schema graph. NOTE: Changing the schema graph is still allowed!</param>
        public GraphCanvas<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                           TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                           TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                           THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>

            SchemaGraphCanvas(TVertexLabel  SchemaGraphId,
                              String        SchemaGraphDescription  = null,
                              Boolean       ContinuousLearning      = true,
                              Boolean       EnforceSchema           = false)

        {

            var SchemaGraphCanvas = new GraphCanvas<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                                    TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                                    TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                                    THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>(

                                         this.Graph.StrictSchemaGraph(SchemaGraphId,
                                                                      SchemaGraphDescription, ContinuousLearning, EnforceSchema,
                                                                      new TKeyVertex[4]    { this.VertexX_PropertyKey, this.VertexY_PropertyKey, this.VertexZ_PropertyKey, this.VertexControl_PropertyKey },
                                                                      new TKeyEdge[1]      { this.EdgeControl_PropertyKey      },
                                                                      new TKeyMultiEdge[1] { this.MultiEdgeControl_PropertyKey },
                                                                      new TKeyHyperEdge[1] { this.HyperEdgeControl_PropertyKey }),
                                         this.GraphCanvas_PropertyKey,
                                         this.VertexX_PropertyKey,
                                         this.VertexY_PropertyKey,
                                         this.VertexZ_PropertyKey,
                                         this.VertexControl_PropertyKey,
                                         this.EdgeControl_PropertyKey,
                                         this.MultiEdgeControl_PropertyKey,
                                         this.HyperEdgeControl_PropertyKey);


            // Special visualization of schema graphs
            SchemaGraphCanvas.EdgeCaption = edge => edge.Id.ToString();


            return SchemaGraphCanvas;

        }

        #endregion

    }

    #endregion

}
