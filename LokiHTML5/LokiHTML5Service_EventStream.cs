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
using System.Reflection;

using de.ahzf.Illias.Commons;
using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.Loki.HTML5
{

    public class LokiHTML5Service_EventStream : AHTTPService, ILokiHTML5Service
    {

        #region Constructor(s)

        #region LokiHTML5Service_EventStream()

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        public LokiHTML5Service_EventStream()
            : base(HTTPContentType.EVENTSTREAM)
        { }

        #endregion

        #region LokiHTML5Service_EventStream(IHTTPConnection)

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public LokiHTML5Service_EventStream(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.EVENTSTREAM, "LokiHTML5.resources.")
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region GetRoot()

        public HTTPResponse GetRoot()
        {
            return HTTPErrors.HTTPErrorResponse(IHTTPConnection.InHTTPRequest, HTTPStatusCode.NotAcceptable);
        }

        #endregion


        #region GetEvents()

        public HTTPResponse GetEvents()
        {

            var _RequestHeader      = IHTTPConnection.InHTTPRequest;
            var _LastEventId        = 0UL;
            var _Client_LastEventId = 0UL;
            var _EventSource        = IHTTPConnection.URLMapping.EventSource("GraphEvents");

            if (_RequestHeader.TryGet<UInt64>("Last-Event-Id", out _Client_LastEventId))
                _LastEventId = _Client_LastEventId + 1;

            var _HTTPEvents      = (from   _HTTPEvent
                                    in     _EventSource.GetEvents(_Client_LastEventId)
                                    where  _HTTPEvent != null
                                    select _HTTPEvent.ToString())
                                   .ToArray(); // For thread safety!

            // Transform HTTP events into an UTF8 string
            var _ResourceContent = (_HTTPEvents.Any()) ? _HTTPEvents.Aggregate((a, b) => { return a + Environment.NewLine + b; }).ToUTF8Bytes() : new Byte[0];

            return new HTTPResponseBuilder() {
                            HTTPStatusCode = HTTPStatusCode.OK,
                            ContentType    = HTTPContentType.EVENTSTREAM,
                            CacheControl   = "no-cache",
                            Connection     = "keep-alive",
                            Content        = _ResourceContent
                        };

        }

        #endregion

    }

}
