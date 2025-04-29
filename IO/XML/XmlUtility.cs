// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="XmlUtility.cs" company="Terry D. Eppler">
//    Kitty is a small and simple windows (wpf) application for interacting with the OpenAI API
//    that's developed in C-Sharp under the MIT license.C#.
// 
//    Copyright ©  2020-2024 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   XmlUtility.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public static class XmlUtility
    {
        /// <summary>
        /// Creates the element.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="namespaceUri">The namespace URI.</param>
        /// <returns></returns>
        public static XElement CreateElement( string name, string value = null,
            Dictionary<string, string> attributes = null, string namespaceUri = null )
        {
            try
            {
                var _element = string.IsNullOrEmpty( namespaceUri )
                    ? new XElement( name )
                    : new XElement( XName.Get( name, namespaceUri ) );

                if( value != null )
                {
                    _element.Value = value;
                }

                if( attributes != null )
                {
                    foreach( var _attribute in attributes )
                    {
                        _element.SetAttributeValue( _attribute.Key, _attribute.Value );
                    }
                }

                return _element;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( XElement );
            }
        }

        /// <summary>
        /// Serializes to XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string SerializeToXml<T>( T obj )
        {
            try
            {
                var _serializer = new XmlSerializer( typeof( T ) );
                using var _writer = new StringWriter( );
                _serializer.Serialize( _writer, obj );
                return _writer.ToString( );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Deserializes from XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>( string xml )
        {
            try
            {
                var _serializer = new XmlSerializer( typeof( T ) );
                using var _reader = new StringReader( xml );
                return ( T )_serializer.Deserialize( _reader );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( T );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = Console.Error;
        }
    }
}