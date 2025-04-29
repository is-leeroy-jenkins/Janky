// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-23-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-23-2025
// ******************************************************************************************
// <copyright file="WebSerializer.cs" company="Terry D. Eppler">
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
//   WebSerializer.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public static class WebSerializer
    {
        /// <summary>
        /// Serializes an object to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>JSON string representation of the object.</returns>
        public static string Serialize<T>( T data )
        {
            try
            {
                ThrowIf.Null( data, nameof( data ) );
                return JsonConvert.SerializeObject( data );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Deserializes a JSON string to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>Deserialized object of the specified type.</returns>
        public static T Deserialize<T>( string json )
        {
            try
            {
                ThrowIf.Empty( json, nameof( json ) );
                return JsonConvert.DeserializeObject<T>( json );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( T );
            }
        }

        /// <summary>
        /// Sends a POST request with a JSON payload and receives the JSON response.
        /// </summary>
        /// <typeparam name="TRequest">Type of the request object to serialize.</typeparam>
        /// <typeparam name="TResponse">Type of the response object to deserialize.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="requestData">The object to be serialized and sent as JSON.</param>
        /// <returns>Deserialized response object.</returns>
        public static TResponse SendJsonPostRequest<TRequest, TResponse>( string url,
            TRequest requestData )
        {
            try
            {
                ThrowIf.Empty( url, nameof( url ) );
                var _request = WebRequest.Create( url );
                _request.Method = "POST";
                _request.ContentType = "application/json";
                var _jsonPayload = Serialize( requestData );
                using var _streamWriter = new StreamWriter( _request.GetRequestStream( ) );
                _streamWriter.Write( _jsonPayload );
                using var _response = _request.GetResponse( );
                using var _responseStream = _response.GetResponseStream( );
                if( _responseStream != null )
                {
                    using var _reader = new StreamReader( _responseStream );
                    var _jsonResponse = _reader.ReadToEnd( );
                    return Deserialize<TResponse>( _jsonResponse );
                }
                else
                {
                    return default( TResponse );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( TResponse );
            }
        }

        /// <summary>
        /// Sends a GET request and receives the JSON response.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the response object to deserialize.
        /// </typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <returns>Deserialized response object.</returns>
        public static T SendJsonGetRequest<T>( string url )
        {
            try
            {
                ThrowIf.Empty( url, nameof( url ) );
                var _request = WebRequest.Create( url );
                _request.Method = "GET";
                _request.ContentType = "application/json";
                using var _response = _request.GetResponse( );
                using var _responseStream = _response.GetResponseStream( );
                if( _responseStream != null )
                {
                    using var _reader = new StreamReader( _responseStream );
                    var _jsonResponse = _reader.ReadToEnd( );
                    return Deserialize<T>( _jsonResponse );
                }
                else
                {
                    return default( T );
                }
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