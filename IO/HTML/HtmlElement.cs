// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="HtmlElement.cs" company="Terry D. Eppler">
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
//   HtmlElement.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    public class HtmlElement
    {
        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// The element
        /// </summary>
        private protected IElement _element;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="HtmlElement"/> class.
        /// </summary>
        public HtmlElement( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="HtmlElement"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        public HtmlElement( IElement element )
        {
            _element = element;
        }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public IElement Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
            }
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName
        {
            get
            {
                return _element.TagName;
            }
        }

        /// <summary>
        /// Gets or sets the inner text.
        /// </summary>
        /// <value>
        /// The inner text.
        /// </value>
        public string InnerText
        {
            get
            {
                return _element.TextContent;
            }
            set
            {
                _element.TextContent = value;
            }
        }

        /// <summary>
        /// Gets or sets the inner HTML.
        /// </summary>
        /// <value>
        /// The inner HTML.
        /// </value>
        public string InnerHtml
        {
            get
            {
                return _element.InnerHtml;
            }
            set
            {
                _element.InnerHtml = value;
            }
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public List<HtmlAttribute> Attributes
        {
            get
            {
                return _element.Attributes?.Select( a => new HtmlAttribute( a.Name, a.Value ) )
                    .ToList( );
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IEnumerable<HtmlElement> Children
        {
            get
            {
                return _element.Children.Select( c => new HtmlElement( c ) );
            }
        }

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddAttribute( string name, string value )
        {
            _element.SetAttribute( name, value );
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void AddChild( HtmlElement child )
        {
            _element.AppendChild( child._element.Clone( true ) );
        }

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="child">The child.</param>
        public void RemoveChild( HtmlElement child )
        {
            _element.RemoveChild( child._element );
        }

        /// <summary>
        /// Queries the selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public HtmlElement QuerySelector( string selector )
        {
            return _element.QuerySelector( selector ) is IElement _selector
                ? new HtmlElement( _selector )
                : null;
        }

        /// <summary>
        /// Queries the selector all.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IEnumerable<HtmlElement> QuerySelectorAll( string selector )
        {
            return _element.QuerySelectorAll( selector )
                .Select( e => new HtmlElement( e ) );
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            return _element.OuterHtml;
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