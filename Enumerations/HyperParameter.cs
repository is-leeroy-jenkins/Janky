// ******************************************************************************************
//     Assembly:                Kitty
//     Author:                  Terry D. Eppler
//     Created:                 11-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        11-25-2024
// ******************************************************************************************
// <copyright file="HyperParameter.cs" company="Terry D. Eppler">
//    Kitty is a small windows (wpf) application for interacting with
//    Chat GPT that's developed in C-Sharp under the MIT license
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
//   HyperParameter.cs
// </summary>
// ******************************************************************************************

namespace Janky
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public enum HyperParameter
    {
        /// <summary>
        /// The number
        /// </summary>
        Number,

        /// <summary>
        /// The top percent
        /// </summary>
        TopPercent,

        /// <summary>
        /// The store
        /// </summary>
        Store,

        /// <summary>
        /// The maximum tokens
        /// </summary>
        MaxCompletionTokens,

        /// <summary>
        /// The temperature
        /// </summary>
        Temperature,

        /// <summary>
        /// The frequency
        /// </summary>
        FrequencyPenalty, 

        /// <summary>
        /// The presence
        /// </summary>
        PresencePenalty,

        /// <summary>
        /// The stream
        /// </summary>
        Stream
    }
}