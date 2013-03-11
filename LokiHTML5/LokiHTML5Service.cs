/*
 * Copyright (c) 2011-2013 Achim 'ahzf' Friedland <achim@ahzf.de>
 * This file is part of Loki <http://www.github.com/Vanaheimr/Loki>
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
using System.Reflection;

using eu.Vanaheimr.Hermod.HTTP;

#endregion

namespace de.ahzf.Loki.HTML5
{

    /// <summary>
    /// HTML representation
    /// </summary>
    public class LokiHTML5Service : AHTTPService, ILokiHTML5Service
    {

        #region Constructor(s)

        #region LokiHTML5Service()

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        public LokiHTML5Service()
            : base(HTTPContentType.ALL)
        { }

        #endregion

        #region LokiHTML5Service(IHTTPConnection)

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public LokiHTML5Service(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.ALL, "LokiHTML5.resources.")
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region GetRoot()

        public HTTPResponse GET_Root()
        {
            return GetResources("landingpage.html");
        }

        #endregion


        #region GetEvents()

        public HTTPResponse GetEvents()
        {
            return new HTTPResult<Object>(IHTTPConnection.RequestHeader, HTTPStatusCode.NotAcceptable).Error;
        }

        #endregion

    }

}
