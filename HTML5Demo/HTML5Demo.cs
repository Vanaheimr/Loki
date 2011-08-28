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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel;
using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.Loki.HTML5
{

    /// <summary>
    /// A simple hermod demo using TCP and HTTP servers.
    /// </summary>
    public class HTML5Demo
    {

        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="myArgs">The arguments.</param>
        public static void Main(String[] myArgs)
        {

            #region Start HTTPServers

            // Although the socket listens on IPv4Address.Any the service is
            // configured to serve clients only on http://localhost:8181
            // More details within DefaultHTTPService.cs
            var _HTTPServer1 = new HTTPServer(IPv4Address.Any, new IPPort(8181), Autostart: true)
            {
                ServerName = "Default Hermod Demo"
            };

            Console.WriteLine(_HTTPServer1);

            // This service uses a custom HTTPService defined within IRESTService.cs
            var _HTTPServer2 = new HTTPServer<LokiHTML5Service>(IPv4Address.Localhost, IPPort.HTTP, Autostart: true)
            {
                ServerName = "Customized Hermod Demo"
            };

            Console.WriteLine(_HTTPServer2);

            #endregion

            Console.ReadLine();
            Console.WriteLine("done!");

        }

    }

}
