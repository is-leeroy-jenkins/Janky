// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-23-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-23-2025
// ******************************************************************************************
// <copyright file="StringExtensions.cs" company="Terry D. Eppler">
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
//   StringExtensions.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "ReplaceSubstringWithRangeIndexer" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    public static class StringExtensions
    {
        /// <summary>
        /// The SplitPascal
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The
        /// <see cref="string" />
        /// </returns>
        public static string SplitPascal( this string text )
        {
            try
            {
                if( !string.IsNullOrEmpty( text )
                    && ( text.Length > 4 ) )
                {
                    var _pascal = Regex.Replace( text, "([A-Z])", " $1", RegexOptions.Compiled )
                        ?.Trim( );

                    if( _pascal?.StartsWith( "Rpio " ) == true )
                    {
                        return _pascal.Replace( "Rpio ", "RPIO " );
                    }
                    else if( _pascal?.StartsWith( "Npm " ) == true )
                    {
                        return _pascal.Replace( "Npm ", "NPM " );
                    }
                    else if( _pascal?.StartsWith( "Boc " ) == true )
                    {
                        return _pascal.Replace( "Boc ", "BOC " );
                    }
                    else if( _pascal?.StartsWith( "Foc " ) == true )
                    {
                        return _pascal.Replace( "Foc ", "FOC " );
                    }
                    else if( _pascal?.StartsWith( "Org " ) == true )
                    {
                        return _pascal.Replace( "Org ", "ORG " );
                    }
                    else if( _pascal?.StartsWith( "Omb " ) == true )
                    {
                        return _pascal.Replace( "Omb ", "OMB " );
                    }
                    else if( _pascal?.StartsWith( "Prc " ) == true )
                    {
                        return _pascal.Replace( "Prc ", "PRC " );
                    }
                    else if( _pascal?.StartsWith( "Ah " ) == true )
                    {
                        return _pascal.Replace( "Ah ", "AH " );
                    }
                    else if( _pascal?.StartsWith( "Rc " ) == true )
                    {
                        return _pascal.Replace( "Rc ", "RC " );
                    }
                    else if( _pascal?.EndsWith( " Id" ) == true )
                    {
                        return _pascal.Replace( " Id", " ID" );
                    }
                    else
                    {
                        return !string.IsNullOrEmpty( _pascal )
                            ? _pascal
                            : string.Empty;
                    }
                }

                return text;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// The IfNullThen
        /// </summary>
        /// <param name="text">The text
        /// <see cref="string" /></param>
        /// <param name="alt">The alt
        /// <see cref="string" /></param>
        /// <returns>
        /// The
        /// <see cref="string" />
        /// </returns>
        public static string IfNullThen( this string text, string alt )
        {
            try
            {
                ThrowIf.Null( alt, nameof( alt ) );
                return text ?? alt ?? string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Ins the specified values.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>
        /// true if the values list contains the object, else false.
        /// </returns>
        public static bool In( this string text, params string[ ] values )
        {
            try
            {
                ThrowIf.Null( values, nameof( values ) );
                return Array.IndexOf( values, text ) != -1;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// The Right
        /// </summary>
        /// <param name="text">The text
        /// <see cref="string" /></param>
        /// <param name="length">The length
        /// <see cref="int" /></param>
        /// <returns>
        /// The
        /// <see cref="string" />
        /// </returns>
        public static string Last( this string text, int length )
        {
            try
            {
                ThrowIf.Negative( length, nameof( length ) );
                return text?.Length > length
                    ? text.Substring( text.Length - length )
                    : text;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// The Left
        /// </summary>
        /// <param name="text">The text
        /// <see cref="string" /></param>
        /// <param name="length">The length
        /// <see cref="int" /></param>
        /// <returns>
        /// The
        /// <see cref="string" />
        /// </returns>
        public static string First( this string text, int length )
        {
            try
            {
                ThrowIf.Negative( length, nameof( length ) );
                return text?.Length > length
                    ? text[ length ].ToString( )
                    : text;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// The FirstToUpper
        /// </summary>
        /// <param name="text">The theString
        /// <see cref="string" /></param>
        /// <returns>
        /// The
        /// <see cref="string" />
        /// </returns>
        public static string FirstToUpper( this string text )
        {
            try
            {
                var _letters = text.ToCharArray( );
                _letters[ 0 ] = char.ToUpper( _letters[ 0 ] );
                return new string( _letters );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// The ToDateTime
        /// </summary>
        /// <param name="text">The text
        /// <see cref="string" /></param>
        /// <returns>
        /// The
        /// <see />
        /// </returns>
        public static DateTime ToDateTime( this string text )
        {
            try
            {
                var _date = DateTime.TryParse( text, out var _dateTime );
                return _date
                    ? _dateTime
                    : default( DateTime );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( DateTime );
            }
        }

        /// <summary>
        /// The ToStream
        /// </summary>
        /// <param name="text">The source
        /// <see cref="string" /></param>
        /// <returns>
        /// The
        /// <see cref="MemoryStream" />
        /// </returns>
        public static MemoryStream ToMemoryStream( this string text )
        {
            try
            {
                var _buffer = Encoding.UTF8.GetBytes( text );
                return new MemoryStream( _buffer );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( MemoryStream );
            }
        }

        /// <summary>
        /// A string extension method that converts the str to an XmlDocument.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>
        /// str as an XmlDocument.
        /// </returns>
        public static XmlDocument ToXmlDocument( this string xml )
        {
            try
            {
                var _document = new XmlDocument( );
                _document.LoadXml( xml );
                return _document;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( XmlDocument );
            }
        }

        /// <summary>
        /// A string extension method that converts the str to a byte array.
        /// </summary>
        /// <param name="text">The str to act on.</param>
        /// <returns>
        /// str as a byte[].
        /// </returns>
        public static byte[ ] ToByteArray( this string text )
        {
            try
            {
                Encoding _encoding = Activator.CreateInstance<ASCIIEncoding>( );
                return _encoding.GetBytes( text );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( byte[ ] );
            }
        }

        /// <summary>
        /// The WordCount
        /// </summary>
        /// <param name="text">The input
        /// <see cref="string" /></param>
        /// <returns>
        /// The
        /// <see cref="int" />
        /// </returns>
        public static int WordCount( this string text )
        {
            var _count = 0;
            try
            {
                var _re = new Regex( @"[^\text]+" );
                var _matches = _re.Matches( text );
                _count = _matches.Count;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return _count;
            }

            return _count;
        }

        /// <summary>
        /// Writes out a text to a file.
        /// </summary>
        /// <param name="text">The complete file filePath to write to.</param>
        /// <param name="filePath">A String containing text to be written to the file.</param>
        public static void WriteToFile( this string text, string filePath )
        {
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                using var _writer = new StreamWriter( text, false );
                _writer.Write( filePath );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Send an email using the supplied string.
        /// </summary>
        /// <param name="body">String that will be used i the body of the email.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="sender">The email address from which the message was sent.</param>
        /// <param name="recipient">The receiver of the email.</param>
        /// <param name="server">The server from which the email will be sent.</param>
        /// <returns>
        /// A boolean value indicating the success of the email send.
        /// </returns>
        public static bool Email( this string body, string subject, string sender,
            string recipient, string server )
        {
            try
            {
                ThrowIf.Null( subject, nameof( subject ) );
                ThrowIf.Null( sender, nameof( sender ) );
                ThrowIf.Null( recipient, nameof( recipient ) );
                var _message = new MailMessage( );
                _message.To.Add( recipient );
                var _address = new MailAddress( sender );
                _message.From = _address;
                _message.Subject = subject;
                _message.Body = body;
                var _client = new SmtpClient( server );
                var _credentials = new NetworkCredential( );
                _client.Credentials = _credentials;
                _client.Send( _message );
                return true;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// remove space, not line end Useful when parsing user
        /// input such phone, price int.Print("1 000
        /// 000".RemoveSpaces(),.....
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveSpaces( this string text )
        {
            if( text.Contains( " " ) )
            {
                try
                {
                    return text.Replace( " ", "" );
                }
                catch( Exception ex )
                {
                    Fail( ex );
                    return text;
                }
            }

            return text;
        }

        /// <summary>
        /// Determines whether [is valid email address].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// <c> true </c>
        /// if [is valid email address] [the specified s]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsValidEmailAddress( this string s )
        {
            try
            {
                var _regex = new Regex( @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" );
                return _regex.IsMatch( s );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Removes the prefix.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="caseSensitive">if set to <c>true</c> [case sensitive].</param>
        /// <returns></returns>
        public static string RemovePrefix( this string str, string prefix,
            bool caseSensitive = true )
        {
            try
            {
                ThrowIf.Null( prefix, nameof( prefix ) );
                return str.Length >= prefix.Length && str.BeginsWith( prefix, caseSensitive )
                    ? str.Substring( prefix.Length )
                    : str;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Beginses the with.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="beginsWith">The begins with.</param>
        /// <param name="caseSensitive">
        /// if set to <c>true</c> [case sensitive].
        /// </param>
        /// <returns></returns>
        public static bool BeginsWith( this string str, string beginsWith,
            bool caseSensitive = true )
        {
            try
            {
                ThrowIf.Null( beginsWith, nameof( beginsWith ) );
                if( beginsWith.Length > str.Length )
                {
                    return false;
                }

                if( beginsWith.Length == str.Length )
                {
                    return string.Equals( beginsWith, str, caseSensitive
                        ? StringComparison.Ordinal
                        : StringComparison.OrdinalIgnoreCase );
                }

                return str.LastIndexOf( beginsWith, beginsWith.Length - 1, caseSensitive
                    ? StringComparison.Ordinal
                    : StringComparison.OrdinalIgnoreCase ) == 0;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Checks if valid.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="trimAndCheck">if set to <c>true</c> [trim and check].</param>
        /// <returns></returns>
        public static bool IsNull( this string text, bool trimAndCheck = false )
        {
            return string.IsNullOrEmpty( text );
        }

        /// <summary>
        /// Supporteds the in file path.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static bool SupportedInFilePath( this char c )
        {
            return !( c == '?' || c == '\'' || c == '\"' || c == '/' || c == '\\' || c == ';'
                || c == ':' || c == '&' || c == '*' || c == '<' || c == '>' || c == '|' || c == '{'
                || c == '}' || c == '[' || c == ']' || c == '(' || c == ')' );
        }

        /// <summary>
        /// Changes the path slash.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="slash">The slash.</param>
        /// <returns></returns>
        public static string ChangePathSlash( this string filePath, string slash )
        {
            return slash switch
            {
                "\\" when filePath.Contains( '/' ) => filePath.Replace( "/", "\\" ),
                "/" when filePath.Contains( '\\' ) => filePath.Replace( "\\", "/" ),
                var _ => filePath
            };
        }

        /// <summary>
        /// Files the not exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static bool IsFIle( this string path )
        {
            try
            {
                return !File.Exists( path );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Checks if file path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static bool IsFilePath( this string path )
        {
            if( path.Length >= 3 )
            {
                if( path[ 1 ] == ':' )
                {
                    if( path[ 2 ] == '\\' )
                    {
                        if( char.IsLetter( path[ 0 ] ) )
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the after.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="find">The find.</param>
        /// <param name="startAt">The start at.</param>
        /// <param name="returnAll">
        /// if set to <c>true</c> [return all].
        /// </param>
        /// <param name="forward">
        /// if set to <c>true</c> [forward].
        /// </param>
        /// <returns>
        /// string
        /// </returns>
        public static string GetAfter( this string text, string find, int startAt = 0,
            bool returnAll = false, bool forward = true )
        {
            if( text == null )
            {
                return returnAll
                    ? text
                    : "";
            }

            var _idx = !forward
                ? text.LastIndexOf( find, text.Length - startAt, StringComparison.Ordinal )
                : text.IndexOf( find, startAt, StringComparison.Ordinal );

            if( _idx == -1 )
            {
                return returnAll
                    ? text
                    : "";
            }

            _idx += find.Length;
            return text.Substring( _idx );
        }

        /// <summary>
        /// Gets the after last.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="find">The find.</param>
        /// <param name="returnAll">if set to <c>true</c> [return all].</param>
        /// <returns></returns>
        public static string GetAfterLast( this string text, string find, bool returnAll = false )
        {
            var _index = text.LastIndexOf( find, StringComparison.Ordinal );
            if( _index == -1 )
            {
                return returnAll
                    ? text
                    : "";
            }

            _index += find.Length;
            return text.Substring( _index );
        }

        /// <summary>
        /// Paths to URL.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="removeBaseDir">The remove base dir.</param>
        /// <returns></returns>
        public static string PathToUrl( this string filePath, string removeBaseDir = null )
        {
            try
            {
                if( !filePath.IsNull( ) )
                {
                    return "";
                }

                return @"file:///" + filePath.Replace( @"\", "/" );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Determines whether [is file offline].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// <c> true </c>
        /// if [is file offline] [the specified URL]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsFileOffline( this string url )
        {
            try
            {
                return url.StartsWith( "file://", StringComparison.Ordinal );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether this instance is localhost.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// <c> true </c>
        /// if the specified URL is localhost; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsLocalhost( this string url )
        {
            try
            {
                return url.BeginsWith( "http://localhost" ) || url.BeginsWith( "localhost" );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Hexadecimals to int.
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns></returns>
        public static int HexToInt( this char hex )
        {
            try
            {
                return hex switch
                {
                    >= '0' and <= '9' => hex - '0',
                    >= 'a' and <= 'f' => hex - 'a' + 10,
                    >= 'A' and <= 'F' => hex - 'A' + 10,
                    var _ => -1
                };
            }
            catch( Exception ex )
            {
                Fail( ex );
                return -1;
            }
        }

        /// <summary>
        /// URLs the encode.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string UrlEncode( this string text )
        {
            return Uri.EscapeDataString( text );
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