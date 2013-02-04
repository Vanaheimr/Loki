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
using System.Linq;

using de.ahzf.Vanaheimr.Hermod.HTTP;
using de.ahzf.Vanaheimr.Hermod.Datastructures;
using System.Threading.Tasks;
using System.Threading;

#endregion

namespace de.ahzf.Loki.HTML5
{

    /// <summary>
    /// A simple loki HTML5 demo.
    /// </summary>
    public class HTML5Demo
    {

        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="myArgs">The arguments.</param>
        public static void Main(String[] myArgs)
        {

            #region Start the HTTPServer

            var _HTTPServer = new HTTPServer<ILokiHTML5Service>(IPv4Address.Any, IPPort.HTTP, Autostart: true)
            {
                ServerName = "Loki HTML5 Demo"
            };

            Console.WriteLine(_HTTPServer);

            #endregion

            var _Random                  = new Random();
            var _CancellationTokenSource = new CancellationTokenSource();
            var _EventSource             = _HTTPServer.URLMapping.EventSource("GraphEvents");

            var _SubmitTask = Task.Factory.StartNew(() =>
            {

                while (!_CancellationTokenSource.IsCancellationRequested)
                {
                    _EventSource.SubmitSubEvent("vertexadded", "{\"radius\": " + _Random.Next(5, 50) + ", \"x\": " + _Random.Next(50, 550) + ", \"y\": ",
                                                                                 _Random.Next(50, 350) + "}");
                    Thread.Sleep(1000);
                }

            },

            _CancellationTokenSource.Token,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);


            Console.ReadLine();

            _CancellationTokenSource.Cancel();

            Console.WriteLine("done!");

        }

    }

}
