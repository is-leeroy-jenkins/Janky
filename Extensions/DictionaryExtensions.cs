// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-11-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-11-2025
// ******************************************************************************************
// <copyright file="DictionaryExtensions.cs" company="Terry D. Eppler">
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
//   DictionaryExtensions.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Data.SQLite;
    using System.Data.SqlServerCe;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "BadParensLineBreaks" ) ]
    [ SuppressMessage( "ReSharper", "PreferConcreteValueOverDefault" ) ]
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Updates a IDictionary( K, V ) with a TKey and TValue or adds it
        /// </summary>
        /// <typeparam name="K">The type of the key.</typeparam>
        /// <typeparam name="V">The type of the value.</typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static V AddOrUpdate<K, V>( this IDictionary<K, V> dict, K key, V value )
        {
            try
            {
                if( !dict.ContainsKey( key ) )
                {
                    dict.Add( new KeyValuePair<K, V>( key, value ) );
                }
                else
                {
                    dict[ key ] = value;
                }

                return dict[ key ];
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( V );
            }
        }

        /// <summary>
        /// Converts an IDictionary( K, V ) into a NameValueCollection
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns>
        /// NameValueCollection
        /// </returns>
        public static NameValueCollection ToNameValueCollection(
            this IDictionary<string, object> dict )
        {
            try
            {
                var _nvp = new NameValueCollection( );
                foreach( var _kvp in dict )
                {
                    var _value = _kvp.Value.ToString( );
                    _nvp?.Add( _kvp.Key, _value );
                }

                return _nvp.Count > 0
                    ? _nvp
                    : default( NameValueCollection );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( NameValueCollection );
            }
        }

        /// <summary>
        /// Predicates the specified logic.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        public static string ToCriteria( this IDictionary<string, object> dict )
        {
            if( dict?.Any( ) == true )
            {
                try
                {
                    var _criteria = "";
                    foreach( var _kvp in dict )
                    {
                        _criteria += $"{_kvp.Key} = '{_kvp.Value}' AND ";
                    }

                    var _sql = _criteria.TrimEnd( " AND ".ToCharArray( ) );
                    return !string.IsNullOrEmpty( _sql )
                        ? _sql
                        : string.Empty;
                }
                catch( Exception ex )
                {
                    Fail( ex );
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts an IDictionary( string, object) to a bindinglist.
        /// </summary>
        /// <param name="dict">The NVC.</param>
        /// <returns></returns>
        public static BindingList<KeyValuePair<string, object>> ToBindingList(
            this IDictionary<string, object> dict )
        {
            try
            {
                var _bindingList = new BindingList<KeyValuePair<string, object>>( );
                foreach( var _kvp in dict )
                {
                    _bindingList.Add( _kvp );
                }

                return _bindingList?.Any( ) == true
                    ? _bindingList
                    : default( BindingList<KeyValuePair<string, object>> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( BindingList<KeyValuePair<string, object>> );
            }
        }

        /// <summary>
        /// Converts an IDcitionary( T, V ) to a SortedList( int, KeyValuePair(string. object)).
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns>
        /// SortedList( T, V )
        /// </returns>
        public static SortedList<int, KeyValuePair<string, object>> ToSortedList(
            this IDictionary<string, object> dict )
        {
            try
            {
                if( dict?.Count > 0 )
                {
                    var _sortedList = new SortedList<int, KeyValuePair<string, object>>( );
                    var _keys = dict?.Keys.ToArray( );
                    var _values = dict?.Values.ToArray( );
                    for( var _i = 0; _i < dict?.Count; _i++ )
                    {
                        if( _values[ _i ] != null
                            && !string.IsNullOrEmpty( _keys[ _i ] ) )
                        {
                            var _kvp =
                                new KeyValuePair<string, object>( _keys[ _i ], _values[ _i ] );

                            _sortedList?.Add( _i, _kvp );
                        }
                    }

                    return _sortedList?.Count > 0
                        ? _sortedList
                        : default( SortedList<int, KeyValuePair<string, object>> );
                }

                return default( SortedList<int, KeyValuePair<string, object>> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( SortedList<int, KeyValuePair<string, object>> );
            }
        }

        /// <summary>
        /// Converts to sql db parameters.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public static IEnumerable<DbParameter> ToSqlDbParameters(
            this IDictionary<string, object> dict, Provider provider )
        {
            try
            {
                var _columns = dict.Keys.ToArray( );
                var _values = dict.Values.ToArray( );
                switch( provider )
                {
                    case Provider.SQLite:
                    {
                        var _sqlite = new List<SQLiteParameter>( );
                        for( var _i = 0; _i < _columns.Length; _i++ )
                        {
                            var _parameter = new SQLiteParameter
                            {
                                SourceColumn = _columns[ _i ],
                                Value = _values[ _i ]
                            };

                            _sqlite.Add( _parameter );
                        }

                        return _sqlite.Any( )
                            ? _sqlite.ToArray( )
                            : default( List<DbParameter> );
                    }
                    case Provider.SqlCe:
                    {
                        var _sqlce = new List<SqlCeParameter>( );
                        for( var _i = 0; _i < _columns.Length; _i++ )
                        {
                            var _parameter = new SqlCeParameter
                            {
                                SourceColumn = _columns[ _i ],
                                Value = _values[ _i ]
                            };

                            _sqlce.Add( _parameter );
                        }

                        return _sqlce.Any( )
                            ? _sqlce.ToArray( )
                            : default( List<DbParameter> );
                    }
                    case Provider.OleDb:
                    case Provider.Excel:
                    case Provider.Access:
                    {
                        var _oledb = new List<OleDbParameter>( );
                        for( var _i = 0; _i < _columns.Length; _i++ )
                        {
                            var _parameter = new OleDbParameter
                            {
                                SourceColumn = _columns[ _i ],
                                Value = _values[ _i ]
                            };

                            _oledb.Add( _parameter );
                        }

                        return _oledb.Any( )
                            ? _oledb.ToArray( )
                            : default( List<DbParameter> );
                    }
                    case Provider.SqlServer:
                    {
                        var _sqlserver = new List<SqlParameter>( );
                        for( var _i = 0; _i < _columns.Length; _i++ )
                        {
                            var _parameter = new SqlParameter
                            {
                                SourceColumn = _columns[ _i ],
                                Value = _values[ _i ]
                            };

                            _sqlserver.Add( _parameter );
                        }

                        return _sqlserver?.Any( ) == true
                            ? _sqlserver.ToArray( )
                            : default( List<DbParameter> );
                    }
                }

                return default( List<DbParameter> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( List<DbParameter> );
            }
        }

        /// <summary>
        /// Converts to Key bindinglist.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        public static BindingList<string> ToKeyBindingList<T>(
            this IDictionary<string, object> dict )
        {
            if( dict?.Any( ) == true )
            {
                try
                {
                    var _list = new BindingList<string>( );
                    foreach( var _kvp in dict )
                    {
                        _list.Add( _kvp.Key );
                    }

                    return _list?.Any( ) == true
                        ? _list
                        : default( BindingList<string> );
                }
                catch( Exception ex )
                {
                    Fail( ex );
                    return default( BindingList<string> );
                }
            }

            return default( BindingList<string> );
        }

        /// <summary>
        /// Converts to value bindinglist.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        public static BindingList<object> ToValueBindingList<T>(
            this IDictionary<string, object> dict )
        {
            if( dict?.Any( ) == true )
            {
                try
                {
                    var _list = new BindingList<object>( );
                    foreach( var _kvp in dict )
                    {
                        _list.Add( _kvp.Value );
                    }

                    return _list?.Any( ) == true
                        ? _list
                        : default( BindingList<object> );
                }
                catch( Exception ex )
                {
                    Fail( ex );
                    return default( BindingList<object> );
                }
            }

            return default( BindingList<object> );
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// dictionary - Dictionary cannot be null</exception>
        public static string ToJson( this IDictionary<string, object> dict )
        {
            try
            {
                if( dict?.Any( ) == false )
                {
                    var _msg = "Dictionary cannot be empty!";
                    throw new ArgumentNullException( nameof( dict ), _msg );
                }

                var _options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false,
                    WriteIndented = true,
                    AllowTrailingCommas = false,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Always,
                    IncludeFields = false
                };

                return JsonSerializer.Serialize( dict, typeof( Dictionary<string, object> ),
                    _options );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// dict - Dictionary cannot be null</exception>
        public static string ToJson( this IDictionary<string, string> dict )
        {
            try
            {
                if( dict?.Any( ) == false )
                {
                    var _msg = "Dictionary cannot be empty!";
                    throw new ArgumentNullException( nameof( dict ), _msg );
                }

                var _options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false,
                    WriteIndented = true,
                    AllowTrailingCommas = false,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Always,
                    IncludeFields = false
                };

                return JsonSerializer.Serialize( dict, typeof( Dictionary<string, object> ),
                    _options );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">dict</exception>
        public static string ToJson( this NameValueCollection dict )
        {
            if( dict.Count == 0 )
            {
                var _msg = "NameValueCollection cannot be empty!";
                throw new ArgumentNullException( nameof( dict ), _msg );
            }

            var _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            return JsonSerializer.Serialize( dict, typeof( NameValueCollection ), _options );
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