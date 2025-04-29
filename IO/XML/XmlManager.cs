// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="XmlManager.cs" company="Terry D. Eppler">
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
//   XmlManager.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Xml.Linq;
    using System.Xml.Schema;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Kitty.PropertyChangedBase" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class XmlManager : PropertyChangedBase
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
        /// The document
        /// </summary>
        private XDocument _document;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="XmlManager"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public XmlManager( string filePath )
        {
            Load( filePath );
            _document = new XDocument( );
        }

        /// <summary>
        /// Creates the new document.
        /// </summary>
        /// <param name="rootElementName">
        /// Name of the root element.
        /// </param>
        /// <param name="namespaceUri">
        /// The namespace URI.
        /// </param>
        public void CreateDocument( string rootElementName, string namespaceUri = null )
        {
            try
            {
                ThrowIf.Empty( rootElementName, nameof( rootElementName ) );
                ThrowIf.Empty( namespaceUri, nameof( namespaceUri ) );
                var _root = string.IsNullOrEmpty( namespaceUri )
                    ? new XElement( rootElementName )
                    : new XElement( XName.Get( rootElementName, namespaceUri ) );

                _document = new XDocument( _root );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Loads the specified file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public void Load( string filePath )
        {
            try
            {
                ThrowIf.Empty( filePath, nameof( filePath ) );
                _document = XDocument.Load( filePath );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Saves the specified file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public void Save( string filePath )
        {
            try
            {
                ThrowIf.Empty( filePath, nameof( filePath ) );
                _document?.Save( filePath );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Gets the root element.
        /// </summary>
        /// <returns>
        /// </returns>
        public XElement GetRootElement( )
        {
            return _document?.Root;
        }

        /// <summary>
        /// Adds the root element.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public void AddRootElement( XElement rootElement )
        {
            try
            {
                ThrowIf.Empty( rootElement, nameof( rootElement ) );
                _document?.Add( rootElement );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void RemoveElement( XElement element )
        {
            try
            {
                ThrowIf.Empty( element, nameof( element ) );
                element?.Remove( );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Validates the schema.
        /// </summary>
        /// <param name="schemaPath">
        /// The schema path.
        /// </param>
        public void ValidateSchema( string schemaPath )
        {
            try
            {
                ThrowIf.Empty( schemaPath, nameof( schemaPath ) );
                var _schema = new XmlSchemaSet( );
                _schema.Add( null, schemaPath );
                _document.Validate( _schema, ( sender, e ) =>
                {
                    Console.WriteLine( $"{e.Message} at {e.Exception?.LineNumber}" );
                } );
            }
            catch( Exception ex )
            {
                Fail( ex );
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