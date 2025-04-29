// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="XmlCallback.cs" company="Terry D. Eppler">
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
//   XmlCallback.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    public class XmlCallback
    {
        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// The element
        /// </summary>
        private XElement _element;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCallback"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="System.ArgumentNullException">element</exception>
        public XmlCallback( XElement element )
        {
            _element = element ?? throw new ArgumentNullException( nameof( element ) );
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>
        /// string
        /// </returns>
        public string GetAttribute( string attributeName )
        {
            try
            {
                return _element.Attribute( attributeName )?.Value;
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void SetAttribute( string attributeName, string value )
        {
            try
            {
                _element.SetAttributeValue( attributeName, value );
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        public IEnumerable<XElement> GetChildElements( string elementName )
        {
            try
            {
                return string.IsNullOrEmpty( elementName )
                    ? _element.Elements( )
                    : _element.Elements( elementName );
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Adds the child element.
        /// </summary>
        /// <param name="childElement">The child element.</param>
        public void AddChildElement( XElement childElement )
        {
            try
            {
                _element.Add( childElement );
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Removes the child element.
        /// </summary>
        /// <param name="childElement">The child element.</param>
        public void RemoveChildElement( XElement childElement )
        {
            try
            {
                childElement?.Remove( );
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue( string value )
        {
            try
            {
                _element.Value = value;
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        /// <returns></returns>
        public XElement GetParentElement( )
        {
            return _element.Parent;
        }

        /// <summary>
        /// Finds the elements by x path.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public IEnumerable<XElement> FindElementsByXPath( string xpath )
        {
            try
            {
                return _element.XPathSelectElements( xpath );
            }
            catch( Exception e )
            {
                Console.WriteLine( e );
                throw;
            }
        }

        /// <summary>
        /// Finds the element by x path.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        public XElement FindElementByXPath( string xpath )
        {
            try
            {
                return _element.XPathSelectElement( xpath );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( XElement );
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
            using var _error = Console.Error;
        }
    }
}