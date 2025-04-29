// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-30-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-30-2025
// ******************************************************************************************
// <copyright file="XmlReader.cs" company="Terry D. Eppler">
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
//   XmlReader.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Xml.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MergeConditionalExpression" ) ]
    public class XmlReader
    {
        /// <summary>
        /// The busy
        /// </summary>
        private protected bool _busy;

        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlReader"/> class.
        /// </summary>
        public XmlReader( )
        {
        }

        /// <summary>
        /// Reads the element value.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        public string ReadElementValue( string filePath, string elementName )
        {
            try
            {
                ThrowIf.Empty( filePath, nameof( filePath ) );
                ThrowIf.Empty( elementName, nameof( elementName ) );
                var _document = XDocument.Load( filePath );
                var _element = _document.Descendants( elementName ).FirstOrDefault( );
                var _value = _element?.Value;
                return _value != null
                    ? _value
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Reads the attributes.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        public IDictionary<string, string> ReadAttributes( string filePath, string elementName )
        {
            try
            {
                ThrowIf.Empty( filePath, nameof( filePath ) );
                ThrowIf.Empty( elementName, nameof( elementName ) );
                var _document = XDocument.Load( filePath );
                var _element = _document.Descendants( elementName )?.FirstOrDefault( );
                var _data = _element?.Attributes( )
                    ?.ToDictionary( a => a.Name.LocalName, a => a.Value );

                return _data?.Any( ) == true
                    ? _data
                    : default( IDictionary<string, string> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IDictionary<string, string> );
            }
        }

        /// <summary>
        /// Reads all elements.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        public IEnumerable<XElement> ReadAllElements( string filePath, string elementName )
        {
            try
            {
                ThrowIf.Empty( filePath, nameof( filePath ) );
                ThrowIf.Empty( elementName, nameof( elementName ) );
                var _document = XDocument.Load( filePath );
                var _elements = _document.Descendants( elementName );
                return _elements?.Any( ) == true
                    ? _elements
                    : default( IEnumerable<XElement> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IEnumerable<XElement> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c>
        /// to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose( bool disposing )
        {
            if( disposing )
            {
                _timer?.Dispose( );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private protected void Fail( Exception ex )
        {
            var _error = Console.Error;
        }
    }
}