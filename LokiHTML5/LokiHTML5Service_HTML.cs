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
using System.Text;
using System.Reflection;

using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.Loki.HTML5
{

    /// <summary>
    /// HTML representation
    /// </summary>
    public class LokiHTML5Service_HTML : AHTTPService, ILokiHTML5Service
    {

        #region Constructor(s)

        #region LokiHTML5Service_HTML()

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        public LokiHTML5Service_HTML()
            : base(HTTPContentType.HTML_UTF8)
        { }

        #endregion

        #region LokiHTML5Service_HTML(IHTTPConnection)

        /// <summary>
        /// Creates a new LokiHTML5Service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public LokiHTML5Service_HTML(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.HTML_UTF8, "LokiHTML5.resources.")
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
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
            return GetResources("landingpage.html");
        }

        #endregion


        #region GetEvents()

        public HTTPResponse GetEvents()
        {
            return Error406_NotAcceptable();
        }

        #endregion

    }

}
