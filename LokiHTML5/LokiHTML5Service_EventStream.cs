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
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.HTTP.Common;
using System.Threading;

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
        { }

        #endregion

        #region LokiHTML5Service_EventStream(myIHTTPConnection)

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        /// <param name="myIHTTPConnection">The http connection for this request.</param>
        public LokiHTML5Service_EventStream(IHTTPConnection myIHTTPConnection)
            : base(myIHTTPConnection, "LokiHTML5.resources.")
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region GetRoot()

        public HTTPResponse GetRoot()
        {
            return Error406_NotAcceptable();
        }

        #endregion



        #region GetEvents()

        public HTTPResponse GetEvents()
        {

            var _RequestHeader      = IHTTPConnection.RequestHeader;
            var _LastEventId        = 0UL;
            var _Client_LastEventId = 0UL;
            var _EventSource        = IHTTPConnection.URLMapping.EventSource("GraphEvents");

            if (_RequestHeader.TryGet<UInt64>("Last-Event-Id", out _Client_LastEventId))
                _LastEventId = _Client_LastEventId + 1;

            var _Random = new Random();
            _EventSource.Submit("vertexadded", "{\"radius\": " + _Random.Next(5, 50) + ", \"x\": " + _Random.Next(50, 550) + ", \"y\": " + _Random.Next(50, 350) + "}");

            //var _ResourceContent = new StringBuilder();
            //_ResourceContent.AppendLine("event:vertexadded");
            //_ResourceContent.AppendLine("id: " + _LastEventId);
            //_ResourceContent.Append("data: ");
            //_ResourceContent.Append("{\"radius\": " + _Random.Next(5, 50));
            //_ResourceContent.Append(", \"x\": "     + _Random.Next(50, 550));
            //_ResourceContent.Append(", \"y\": "     + _Random.Next(50, 350) + "}");
            //_ResourceContent.AppendLine().AppendLine();

            var _ResourceContent = _EventSource.GetEvents(_Client_LastEventId);
            var _ResourceContent2 = _ResourceContent.Select(e => e.ToString()).Aggregate((a, b) => { return a + Environment.NewLine + b; });
            var _ResourceContent3 = _ResourceContent2.ToUTF8Bytes();

            return new HTTPResponse(

                    new HTTPResponseHeader()
                        {
                            HttpStatusCode = HTTPStatusCode.OK,
                            ContentType    = HTTPContentType.EVENTSTREAM,
                            ContentLength  = (UInt64) _ResourceContent3.Length,
                            CacheControl   = "no-cache",
                            Connection     = "keep-alive",
                        },

                    _ResourceContent3

                );
        }

        #endregion


        
        public IEnumerable<HTTPContentType> HTTPContentTypes
        {
            get
            {
                return new List<HTTPContentType>() { HTTPContentType.EVENTSTREAM };
            }
        }

    }

}
