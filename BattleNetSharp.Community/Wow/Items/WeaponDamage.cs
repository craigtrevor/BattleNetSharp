﻿// Copyright (C) 2011 by Sherif Elmetainy (Grendiser@Kazzak-EU)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Globalization;
using System.Runtime.Serialization;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   Represents a weapon's damage
    /// </summary>
    [DataContract]
    public class WeaponDamage
    {
        /// <summary>
        ///   Gets or sets the maximum weapon damage
        /// </summary>
        [DataMember(Name = "max", IsRequired = true)]
        public int MaximumDamage
        {
            get;
            internal set;
        }

        /// <summary>
        ///   Gets or sets the minimum weapon damage
        /// </summary>
        [DataMember(Name = "min", IsRequired = true)]
        public int MinimumDamage
        {
            get;
            internal set;
        }

        /// <summary>
        ///   Gets or sets the exact maximum weapon damage
        /// </summary>
        [DataMember(Name = "exactMax", IsRequired = true)]
        public double ExactMaximumDamage
        {
            get;
            internal set;
        }

        /// <summary>
        ///   Gets or sets the exact minimum weapon damage
        /// </summary>
        [DataMember(Name = "exactMin", IsRequired = true)]
        public double ExactMinimumDamage
        {
            get;
            internal set;
        }


        /// <summary>
        ///   Gets string representation (for debugging purposes)
        /// </summary>
        /// <returns> Gets string representation (for debugging purposes) </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} - {1}", MinimumDamage, MaximumDamage);
        }
    }
}