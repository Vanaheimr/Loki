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

#endregion

namespace de.ahzf.Loki.HTML5
{

    public class LokiHTML5Service : ILokiHTML5Service
    {


        #region Properties

        public IHTTPConnection IHTTPConnection { get; set; }

        #endregion

        #region Constructor(s)

        #region LokiHTML5Service()

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        public LokiHTML5Service()
        { }

        #endregion

        #region LokiHTML5Service(myIHTTPConnection)

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        /// <param name="myIHTTPConnection">The http connection for this request.</param>
        public LokiHTML5Service(IHTTPConnection myIHTTPConnection)
        {
            IHTTPConnection = myIHTTPConnection;
        }

        #endregion

        #endregion

        
        #region (private) HTMLBuilder(myHeadline, myFunc)

        private String HTMLBuilder(String myHeadline, Action<StringBuilder> myFunc)
        {

            var _StringBuilder = new StringBuilder();

            _StringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _StringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            _StringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            _StringBuilder.AppendLine("<head>");
            _StringBuilder.AppendLine("<title>Hermod HTTP Server</title>");
            _StringBuilder.AppendLine("</head>");
            _StringBuilder.AppendLine("<body>");
            _StringBuilder.Append("<h2>").Append(myHeadline).AppendLine("</h2>");
            _StringBuilder.AppendLine("<table>");
            _StringBuilder.AppendLine("<tr>");
            _StringBuilder.AppendLine("<td style=\"width: 100px\">&nbsp;</td>");
            _StringBuilder.AppendLine("<td>");

            myFunc(_StringBuilder);

            _StringBuilder.AppendLine("</td>");
            _StringBuilder.AppendLine("</tr>");
            _StringBuilder.AppendLine("</table>");
            _StringBuilder.AppendLine("</body>").AppendLine("</html>").AppendLine();

            return _StringBuilder.ToString();

        }

        #endregion


        #region GetRoot()

        public HTTPResponse GetRoot()
        {

            return new HTTPResponse(

                new HTTPResponseHeader()
                {
                    HttpStatusCode = HTTPStatusCode.OK,
                    CacheControl   = "no-cache",
                    ContentType    = HTTPContentType.XHTML_UTF8
                },

                HTMLBuilder("Hello World!", _StringBuilder =>
                {

                    _StringBuilder.Append("<p><a href=\"/robots.txt\">Look at the '/robots.txt'!</a></p><br /><br />");
                    _StringBuilder.Append("<p><a href=\"/raw\">Look at your raw http request header!</a></p><br /><br />");

                }).ToUTF8Bytes()

            );

        }

        #endregion

        #region GetRAWRequestHeader()

        public HTTPResponse GetRAWRequestHeader()
        {

            return new HTTPResponse(

                new HTTPResponseHeader()
                {
                    HttpStatusCode = HTTPStatusCode.OK,
                    CacheControl   = "no-cache",
                    Connection     = "close",
                    ContentType    = HTTPContentType.TEXT_UTF8
                },

                Encoding.UTF8.GetBytes("Incoming http connection from '" + IHTTPConnection.RemoteSocket + "'" +
                                        Environment.NewLine + Environment.NewLine +
                                        IHTTPConnection.RequestHeader.RAWHTTPHeader +
                                        Environment.NewLine + Environment.NewLine +
                                        "Method => " + IHTTPConnection.RequestHeader.HTTPMethod + Environment.NewLine +
                                        "URL => " + IHTTPConnection.RequestHeader.Url + Environment.NewLine +
                                        "QueryString => " + IHTTPConnection.RequestHeader.QueryString + Environment.NewLine +
                                        "Protocol => " + IHTTPConnection.RequestHeader.ProtocolName + Environment.NewLine +
                                        "Version => " + IHTTPConnection.RequestHeader.ProtocolVersion + Environment.NewLine +
                                        Environment.NewLine + Environment.NewLine +
                                        IHTTPConnection.ResponseHeader.HttpStatusCode
                                        )

            );

        }

        #endregion

        #region /HelloWorld

        #region HelloWorld_OPTIONS()

        public HTTPResponse HelloWorld_OPTIONS()
        {

            return new HTTPResponse(

                new HTTPResponseHeader()
                {

                    HttpStatusCode = HTTPStatusCode.OK,
                    CacheControl = "no-cache",

                    Allow = new List<HTTPMethod> {
                                          HTTPMethod.OPTIONS,
                                          HTTPMethod.HEAD,
                                          HTTPMethod.GET
                                      }

                }

            );

        }

        #endregion

        #region HelloWorld_HEAD()

        public HTTPResponse HelloWorld_HEAD()
        {

            var _RequestHeader = IHTTPConnection.RequestHeader;
            var _Content = Encoding.UTF8.GetBytes("Hello world!");

            return new HTTPResponse(

                new HTTPResponseHeader()
                {
                    HttpStatusCode = HTTPStatusCode.OK,
                    CacheControl   = "no-cache",
                    ContentLength  = (UInt64) _Content.Length,
                    ContentType    = HTTPContentType.TEXT_UTF8
                },

                _Content

            );

        }

        #endregion

        #region HelloWorld_GET()

        public HTTPResponse HelloWorld_GET()
        {

            var _RequestHeader = IHTTPConnection.RequestHeader;
            var _Content = Encoding.UTF8.GetBytes("Hello world!");

            return new HTTPResponse(

                new HTTPResponseHeader()
                {
                    HttpStatusCode = HTTPStatusCode.OK,
                    CacheControl   = "no-cache",
                    ContentLength  = (UInt64) _Content.Length,
                    ContentType    = HTTPContentType.TEXT_UTF8
                },

                _Content

            );

        }

        #endregion

        #endregion


        #region GetResources(myResource)

        /// <summary>
        /// Returns internal resources embedded within the assembly.
        /// </summary>
        /// <param name="myResource">The path and name of the resource.</param>
        public HTTPResponse GetResources(String myResource)
        {

            #region Data

            var _Assembly     = Assembly.GetExecutingAssembly();
            var _AllResources = _Assembly.GetManifestResourceNames();

            myResource = myResource.Replace('/', '.');

            #endregion

            #region Return internal assembly resources...

            if (_AllResources.Contains("HermodDemo.resources." + myResource))
            {

                var _ResourceContent = _Assembly.GetManifestResourceStream("HermodDemo.resources." + myResource);

                HTTPContentType _ResponseContentType = null;

                // Get the apropriate content type based on the suffix of the requested resource
                switch (myResource.Remove(0, myResource.LastIndexOf(".") + 1))
                {
                    case "htm":  _ResponseContentType = HTTPContentType.XHTML_UTF8;      break;
                    case "html": _ResponseContentType = HTTPContentType.XHTML_UTF8;      break;
                    case "css":  _ResponseContentType = HTTPContentType.CSS_UTF8;        break;
                    case "gif":  _ResponseContentType = HTTPContentType.GIF;             break;
                    case "ico":  _ResponseContentType = HTTPContentType.ICO;             break;
                    case "swf":  _ResponseContentType = HTTPContentType.SWF;             break;
                    case "js":   _ResponseContentType = HTTPContentType.JAVASCRIPT_UTF8; break;
                    default:     _ResponseContentType = HTTPContentType.OCTETSTREAM;     break;
                }

                return new HTTPResponse(

                    new HTTPResponseHeader()
                        {
                            HttpStatusCode = HTTPStatusCode.OK,
                            ContentType    = _ResponseContentType,
                            ContentLength  = (UInt64) _ResourceContent.Length,
                            CacheControl   = "no-cache",
                            Connection     = "close",
                        },

                    _ResourceContent

                );

            }

            #endregion

            #region ...or send an (custom) error 404!

            else
            {
                
                Stream _ResourceContent = null;

                if (_AllResources.Contains("HermodDemo.resources.errorpages.Error404.html"))
                    _ResourceContent = _Assembly.GetManifestResourceStream("HermodDemo.resources.errorpages.Error404.html");
                else
                    _ResourceContent = new MemoryStream(UTF8Encoding.UTF8.GetBytes("Error 404 - File not found!"));

                return new HTTPResponse(

                    new HTTPResponseHeader()
                        {
                            HttpStatusCode = HTTPStatusCode.NotFound,
                            ContentType    = HTTPContentType.XHTML_UTF8,
                            ContentLength  = (UInt64) _ResourceContent.Length,
                            CacheControl   = "no-cache",
                            Connection     = "close",
                        },

                    _ResourceContent

                );

            }

            #endregion

        }

        #endregion

        #region GetFavicon()

        /// <summary>
        /// Get /favicon.ico
        /// </summary>
        /// <returns>Some HTML and JavaScript.</returns>
        public HTTPResponse GetFavicon()
        {
            return GetResources("favicon.ico");
        }

        #endregion

        #region GetRobotsTxt()

        /// <summary>
        /// Get /robots.txt
        /// </summary>
        /// <returns>Some search engine info.</returns>
        public HTTPResponse GetRobotsTxt()
        {
            return GetResources("robots.txt");
        }

        #endregion


        public IEnumerable<HTTPContentType> HTTPContentTypes
        {
            get
            {
                return new List<HTTPContentType>() { HTTPContentType.TEXT_UTF8 };
            }
        }

    }

}
