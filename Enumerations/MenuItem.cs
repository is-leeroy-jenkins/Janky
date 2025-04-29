// ******************************************************************************************
//     Assembly:                Baby
//     Author:                  Terry D. Eppler
//     Created:                 09-09-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-09-2024
// ******************************************************************************************
// <copyright file="MenuItem.cs" company="Terry D. Eppler">
//     Baby is a light-weight, full-featured, web-browser built with .NET 6 and is written
//     in C#.  The baby browser is designed for budget execution and data analysis.
//     A tool for EPA analysts and a component that can be used for general browsing.
// 
//     Copyright ©  2020 Terry D. Eppler
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
//   MenuItem.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public enum MenuItem
    {
        /// <summary>
        /// The close
        /// </summary>
        Close = 1,

        /// <summary>
        /// The other tabs
        /// </summary>
        Other = 2,

        /// <summary>
        /// The refresh
        /// </summary>
        Refresh = 3,

        /// <summary>
        /// The search
        /// </summary>
        SaveAs = 4,

        /// <summary>
        /// The browse
        /// </summary>
        Print = 5,

        /// <summary>
        /// The developer tools
        /// </summary>
        Developer = 6,

        /// <summary>
        /// The view source
        /// </summary>
        Source = 7,

        /// <summary>
        /// The chrome
        /// </summary>
        Chrome = 8,

        /// <summary>
        /// The edge
        /// </summary>
        Edge = 9,

        /// <summary>
        /// The firefox
        /// </summary>
        Firefox = 10,

        /// <summary>
        /// The calculator
        /// </summary>
        Calculator = 11,

        /// <summary>
        /// The task manager
        /// </summary>
        TaskManager = 12,

        /// <summary>
        /// The control panel
        /// </summary>
        ControlPanel = 13,

        /// <summary>
        /// The one drive
        /// </summary>
        OneDrive = 14,

        /// <summary>
        /// The exit
        /// </summary>
        Exit = 15
    }
}