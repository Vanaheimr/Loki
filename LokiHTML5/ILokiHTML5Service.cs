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
using de.ahzf.Hermod.TCP;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.HTTP.Common;

#endregion

namespace de.ahzf.Loki.HTML5
{

    //[HTTPService(Host: "localhost:8080", ForceAuthentication: true)]
    [HTTPService(HostAuthentication: true)]
    public interface ILokiHTML5Service : IHTTPService
    {

        #region Landingpage

        /// <summary>
        /// Get Landingpage
        /// </summary>
        /// <returns>Some HTML and JavaScript</returns>
        [HTTPMapping(HTTPMethods.GET, "/"), NoAuthentication]
        HTTPResponse GetRoot();

        #endregion

        #region Events

        // In contrast to other popular Comet protocols such as Bayeux or BOSH, Server-Sent Events
        // support a unidirectional server-to-client channel only. The Bayeux protocol on the other
        // side supports a bidirectional communication channel. Furthermore, Bayeux can use HTTP
        // streaming as well as long polling. Like Bayeux, the BOSH protocol is a bidirectional
        // protocol. BOSH is based on the long polling approach.

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns>Endless text</returns>
        [HTTPMapping(HTTPMethods.GET, "/Events"), NoAuthentication]
        HTTPResponse GetEvents();

        #endregion

        #region Utilities

        /// <summary>
        /// Will return internal resources
        /// </summary>
        /// <returns>internal resources</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/resources/{myResource}")]
        HTTPResponse GetResources(String myResource);

        /// <summary>
        /// Get /favicon.ico
        /// </summary>
        /// <returns>Some HTML and JavaScript.</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/favicon.ico")]
        HTTPResponse GetFavicon();

        /// <summary>
        /// Get /robots.txt
        /// </summary>
        /// <returns>Some search engine info.</returns>
        [NoAuthentication]
        [HTTPMapping(HTTPMethods.GET, "/robots.txt")]
        HTTPResponse GetRobotsTxt();

        #endregion

    }

}
