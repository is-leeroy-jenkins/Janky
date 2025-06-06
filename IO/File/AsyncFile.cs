﻿// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 01-07-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        01-07-2025
// ******************************************************************************************
// <copyright file="AsyncFile.cs" company="Terry D. Eppler">
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
//   AsyncFile.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class AsyncFile : AsyncFileBase
    {
        /// <inheritdoc />
        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <returns>
        /// <c>true</c>
        /// if [contains] [the specified search];
        /// otherwise, <c>false</c>.
        /// </returns>
        public Task<bool> ContainsAsync( string search )
        {
            if( _fileExists )
            {
                var _async = new TaskCompletionSource<bool>( );
                try
                {
                    ThrowIf.Null( search, nameof( search ) );
                    using var _stream = File.Open( _input, FileMode.Open );
                    using var _reader = new StreamReader( _stream );
                    if( _reader != null )
                    {
                        var Text = _reader?.ReadLine( );
                        var _result = false;
                        while( !string.IsNullOrEmpty( Text ) )
                        {
                            if( Regex.IsMatch( Text, search ) )
                            {
                                _result = true;
                                break;
                            }

                            Text = _reader.ReadLine( );
                        }

                        _async.SetResult( _result );
                        return _async.Task;
                    }

                    return default( Task<bool> );
                }
                catch( IOException ex )
                {
                    _async.SetException( ex );
                    Fail( ex );
                    return default( Task<bool> );
                }
            }

            return default( Task<bool> );
        }

        /// <inheritdoc />
        /// <summary>
        /// Searches the specified pattern.
        /// </summary>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// </returns>
        public Task<IList<FileInfo>> SearchFileAsync( string pattern )
        {
            if( _fileExists )
            {
                var _async = new TaskCompletionSource<IList<FileInfo>>( );
                try
                {
                    var _enumerable = Directory.GetDirectories( _input, pattern );
                    var _list = new List<FileInfo>( );
                    foreach( var _file in _enumerable )
                    {
                        _list.Add( new FileInfo( _file ) );
                    }

                    _async.SetResult( _list );
                    return _list?.Any( ) == true
                        ? _async.Task
                        : default( Task<IList<FileInfo>> );
                }
                catch( IOException ex )
                {
                    Fail( ex );
                    return default( Task<IList<FileInfo>> );
                }
            }

            return default( Task<IList<FileInfo>> );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<DirectoryInfo> GetParentAsync( )
        {
            var _async = new TaskCompletionSource<DirectoryInfo>( );
            try
            {
                var _fileInfo = Directory.GetParent( _fullPath );
                _async.SetResult( _fileInfo );
                return _async.Task;
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                Fail( ex );
                return default( Task<DirectoryInfo> );
            }
        }

        /// <summary>
        /// Creates the specified file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// FileInfo
        /// </returns>
        public static Task<FileInfo> GetFileAsync( string filePath )
        {
            var _async = new TaskCompletionSource<FileInfo>( );
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                var _fileInfo = new FileInfo( filePath );
                _async.SetResult( _fileInfo );
                return _async.Task;
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                Fail( ex );
                return default( Task<FileInfo> );
            }
        }

        /// <summary>
        /// Opens the dialog.
        /// </summary>
        /// <returns>
        /// string
        /// </returns>
        public static Task<string> ShowFileDialogAsnyc( )
        {
            var _async = new TaskCompletionSource<string>( );
            try
            {
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                Fail( ex );
                return default( Task<string> );
            }

            return default( Task<string> );
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public Task<object> OpenSaveDialogAsync( )
        {
            FileStream _stream = null;
            var _async = new TaskCompletionSource<object>( );
            try
            {
                _stream.Close( );
                _async.SetResult( _stream );
                return _async.Task;
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                Fail( ex );
                _stream?.Close( );
                return _async.Task;
            }
            finally
            {
                _stream?.Close( );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" />
        /// that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            try
            {
                var _file = new DataFile( _input );
                var _extenstion = _file.Extension ?? string.Empty;
                var _name = _file.FileName ?? string.Empty;
                var _filePath = _file.FullPath ?? string.Empty;
                var _create = _file.Created;
                var _modify = _file.Modified;
                var _len = _file.Length.ToString( "N0" ) ?? string.Empty;
                var _pathsep = _file.PathSeparator;
                var _drivesep = _file.DriveSeparator;
                var _foldersep = _file.FolderSeparator;
                var _root = _file.Drive;
                var _nl = Environment.NewLine;
                var _attrs = _file.FileAttributes;
                var Tb = char.ToString( '\t' );
                var Text = _nl + Tb + "File Name: " + Tb + _name + _nl + _nl + Tb
                    + "File Path: " + Tb + _filePath + _nl + _nl + Tb + "File Attributes: " + Tb
                    + _attrs + _nl + _nl + Tb + "Extension: " + Tb + _extenstion + _nl + _nl + Tb
                    + "Path Root: " + Tb + _root + _nl + _nl + Tb + "Path Separator: " + Tb
                    + _pathsep + _nl + _nl + Tb + "Drive Separator: " + Tb + _drivesep + _nl + _nl
                    + Tb + "Folder Separator: " + Tb + _foldersep + _nl + _nl + Tb + "Length: "
                    + Tb + _len + _nl + _nl + Tb + "Created: " + Tb
                    + _create.ToShortDateString( ) + _nl + _nl + Tb + "Modified: " + Tb
                    + _modify.ToShortDateString( ) + _nl + _nl;

                return !string.IsNullOrEmpty( Text )
                    ? Text
                    : string.Empty;
            }
            catch( IOException ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Kitty.DataFile" /> class.
        /// </summary>
        public AsyncFile( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Kitty.DataFile" /> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public AsyncFile( string input )
            : base( input )
        {
            _input = input;
            _fileName = Path.GetFileNameWithoutExtension( input );
            _fileExists = File.Exists( input );
            _hasExtension = Path.HasExtension( input );
            _fullPath = Path.GetFullPath( input );
            _absolutePath = Path.GetFullPath( input );
            _fileAttributes = File.GetAttributes( input );
            _created = File.GetCreationTime( input );
            _modified = File.GetLastWriteTime( input );
            _hasParent = !string.IsNullOrEmpty( Directory.GetParent( input )?.Name );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Kitty.DataFile" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AsyncFile( DataFile file )
        {
            _input = file.Input;
            _fileName = file.FileName;
            _fileExists = File.Exists( file.FullPath );
            _hasExtension = Path.HasExtension( file.FullPath );
            _fullPath = file.FullPath;
            _absolutePath = file.AbsolutePath;
            _fileAttributes = file.FileAttributes;
            _created = file.Created;
            _modified = file.Modified;
            _hasParent = !string.IsNullOrEmpty( Directory.GetParent( file.FullPath )?.Name );
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public long Size
        {
            get
            {
                return _fileExists
                    ? File.Open( _fullPath, FileMode.Open ).Length
                    : 0L;
            }
        }
    }
}