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

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   Information about character's collected pets
    /// </summary>
    [DataContract]
    public class CharacterPets
    {
        /// <summary>
        ///   gets or sets a collection of collected pets
        /// </summary>
        [DataMember(Name = "collected", IsRequired = false)]
        public IList<Pet> Collected
        {
            get;
            internal set;
        }

        /// <summary>
        ///   gets or sets number of collected pets
        /// </summary>
        [DataMember(Name = "numCollected", IsRequired = false)]
        public int CollectedCount
        {
            get;
            internal set;
        }

        /// <summary>
        ///   gets or sets number of non collected pets
        /// </summary>
        [DataMember(Name = "numNotCollected", IsRequired = false)]
        public int NotCollectedCount
        {
            get;
            internal set;
        }

        /// <summary>
        ///   String representation for debugging purposes
        /// </summary>
        /// <returns> String representation for debugging purposes </returns>
        public override string ToString()
        {
            return CollectedCount + " pets collected";
        }
    }
}