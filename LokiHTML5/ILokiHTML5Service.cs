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

using de.ahzf.Vanaheimr.Hermod.HTTP;

#endregion

namespace de.ahzf.Loki.HTML5
{

    //[HTTPService(Host: "localhost:8080", ForceAuthentication: true)]
    [HTTPService(HostAuthentication: true)]
    public interface ILokiHTML5Service : IHTTPBaseService
    {

        #region Events

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns>Endless text</returns>
        [NoAuthentication]
        [HTTPEventMappingAttribute(EventIdentification: "GraphEvents", UriTemplate: "/Events", MaxNumberOfCachedEvents: 50)]
        HTTPResponse GetEvents();

        #endregion

    }

}
