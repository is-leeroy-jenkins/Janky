// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="HtmlWriter.cs" company="Terry D. Eppler">
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
//   HtmlWriter.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    public class HtmlWriter
    {
        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// The builder
        /// </summary>
        private protected StringBuilder _builder;

        /// <summary>
        /// The open tags
        /// </summary>
        private protected Stack<string> _openTags;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="HtmlWriter"/> class.
        /// </summary>
        public HtmlWriter( )
        {
            _builder = new StringBuilder( );
            _openTags = new Stack<string>( );
        }

        /// <summary>
        /// Begins an HTML element with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the HTML tag.</param>
        /// <param name="attributes">Optional attributes for the element.</param>
        public void BeginElement( string tagName, Dictionary<string, string> attributes = null )
        {
            _builder.Append( $"<{tagName}" );
            if( attributes != null )
            {
                foreach( var (_key, _value) in attributes )
                {
                    _builder.Append( $" {_key}=\"{_value}\"" );
                }
            }

            _builder.Append( ">" );
            _openTags.Push( tagName );
        }

        /// <summary>
        /// Ends the most recently opened HTML element.
        /// </summary>
        public void EndElement( )
        {
            if( _openTags.Count > 0 )
            {
                var _tagName = _openTags.Pop( );
                _builder.Append( $"</{_tagName}>" );
            }
        }

        /// <summary>
        /// Writes text content inside the current element.
        /// </summary>
        /// <param name="text">The text content to write.</param>
        public void WriteText( string text )
        {
            _builder.Append( text );
        }

        /// <summary>
        /// Adds a self-closing HTML element.
        /// </summary>
        /// <param name="tagName">The name of the self-closing tag.</param>
        /// <param name="attributes">Optional attributes for the element.</param>
        public void AddSelfClosingElement( string tagName,
            Dictionary<string, string> attributes = null )
        {
            _builder.Append( $"<{tagName}" );
            if( attributes != null )
            {
                foreach( var (_key, _value) in attributes )
                {
                    _builder.Append( $" {_key}=\"{_value}\"" );
                }
            }

            _builder.Append( " />" );
        }

        /// <summary>
        /// Returns the generated HTML as a string.
        /// </summary>
        /// <returns>The generated HTML.</returns>
        public override string ToString( )
        {
            return _builder.ToString( );
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