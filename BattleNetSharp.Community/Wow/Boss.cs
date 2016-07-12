// Copyright (C) 2016 by Craig Trevor
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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   Boss information
    /// </summary>
    [DataContract]
    public class Boss : ApiResponse
    {
        /// <summary>
        ///   Gets or sets the id of the boss
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; internal set; }

        /// <summary>
        ///   Gets or sets the name of the boss
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; internal set; }

        /// <summary>
        ///   Gets or sets the UrlSlug of the boss
        /// </summary>
        [DataMember(Name = "urlSlug", IsRequired = true)]
        public string UrlSlug { get; internal set; }

        /// <summary>
        ///   Gets or sets the Description of the boss
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; internal set; }

        /// <summary>
        ///   Gets or sets the ZoneId of the boss
        /// </summary>
        [DataMember(Name = "zoneId", IsRequired = true)]
        public int ZoneId { get; internal set; }

        /// <summary>
        ///   Gets or sets the AvailableInNormalMode of the boss
        /// </summary>
        [DataMember(Name = "availableInNormalMode", IsRequired = true)]
        public bool AvailableInNormalMode { get; internal set; }

        /// <summary>
        ///   Gets or sets the AvailableInHeroicMode of the boss
        /// </summary>
        [DataMember(Name = "availableInHeroicMode", IsRequired = true)]
        public bool AvailableInHeroicMode { get; internal set; }

        /// <summary>
        ///   Gets or sets the Health of the boss
        /// </summary>
        [DataMember(Name = "health", IsRequired = true)]
        public int Health { get; internal set; }

        /// <summary>
        ///   Gets or sets the HeroicHealth of the boss
        /// </summary>
        [DataMember(Name = "heroicHealth", IsRequired = true)]
        public int HeroicHealth { get; internal set; }

        /// <summary>
        ///   Gets or sets the Level of the boss
        /// </summary>
        [DataMember(Name = "level", IsRequired = true)]
        public int Level { get; internal set; }

        /// <summary>
        ///   Gets or sets the HeroicLevel of the boss
        /// </summary>
        [DataMember(Name = "heroicLevel", IsRequired = true)]
        public int HeroicLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the JournalId of the boss
        /// </summary>
        [DataMember(Name = "journalId", IsRequired = false)]
        public int JournalId { get; internal set; }

        /// <summary>
        ///   Gets or sets the Npcs of the boss
        /// </summary>
        [DataMember(Name = "npcs", IsRequired = true)]
        public IList<Npc> Npcs { get; internal set; }
    }
}
