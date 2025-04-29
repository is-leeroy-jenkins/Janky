// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-16-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-16-2025
// ******************************************************************************************
// <copyright file="HtmlReader.cs" company="Terry D. Eppler">
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
//   HtmlReader.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using AngleSharp.Html.Parser;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class HtmlReader
    {
        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// The parser
        /// </summary>
        private protected IHtmlParser _parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReader"/> class.
        /// </summary>
        public HtmlReader( )
        {
            _parser = new HtmlParser( );
        }

        /// <summary>
        /// Parses an HTML string and returns the root element of the document.
        /// </summary>
        /// <param name="html">The HTML string to parse.</param>
        /// <returns>The root element of the parsed document.</returns>
        public HtmlElement Parse( string html )
        {
            try
            {
                ThrowIf.Empty( html, nameof( html ) );
                var _document = _parser.ParseDocument( html );
                return new HtmlElement( _document.DocumentElement );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( HtmlElement );
            }
        }

        /// <summary>
        /// Gets or sets the parser.
        /// </summary>
        /// <value>
        /// The parser.
        /// </value>
        public IHtmlParser Parser
        {
            get
            {
                return _parser;
            }
            set
            {
                _parser = value;
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