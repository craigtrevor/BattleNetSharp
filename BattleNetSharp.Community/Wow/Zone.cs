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
    ///   Zone information
    /// </summary>
    [DataContract]
    public class Zone : ApiResponse
    {
        /// <summary>
        ///   Gets or sets the id of the zone
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; internal set; }

        /// <summary>
        ///   Gets or sets the name of the zone
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; internal set; }

        /// <summary>
        ///   Gets or sets the UrlSlug of the zone
        /// </summary>
        [DataMember(Name = "urlSlug", IsRequired = true)]
        public string UrlSlug { get; internal set; }

        /// <summary>
        ///   Gets or sets the Description of the zone
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; internal set; }

        /// <summary>
        ///   Gets or sets the location of the zone
        /// </summary>
        [DataMember(Name = "location", IsRequired = false)]
        public Location Location { get; internal set; }

        /// <summary>
        ///   Gets or sets the expansionId of the zone
        /// </summary>
        [DataMember(Name = "expansionId", IsRequired = false)]
        public int ExpansionId { get; internal set; }

        /// <summary>
        ///   Gets or sets the numPlayers of the zone
        /// </summary>
        [DataMember(Name = "numPlayers", IsRequired = false)]
        public string NumPlayers { get; internal set; }

        /// <summary>
        ///   Gets or sets the isDungeon of the zone
        /// </summary>
        [DataMember(Name = "isDungeon", IsRequired = false)]
        public bool IsDungeon { get; internal set; }

        /// <summary>
        ///   Gets or sets the isRaid of the zone
        /// </summary>
        [DataMember(Name = "isRaid", IsRequired = false)]
        public bool IsRaid { get; internal set; }

        /// <summary>
        ///   Gets or sets the advisedMinLevel of the zone
        /// </summary>
        [DataMember(Name = "advisedMinLevel", IsRequired = false)]
        public int AdvisedMinLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the advisedMaxLevel of the zone
        /// </summary>
        [DataMember(Name = "advisedMaxLevel", IsRequired = false)]
        public int AdvisedMaxLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the advisedHeroicMinLevel of the zone
        /// </summary>
        [DataMember(Name = "advisedHeroicMinLevel", IsRequired = false)]
        public int AdvisedHeroicMinLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the advisedHeroicMaxLevel of the zone
        /// </summary>
        [DataMember(Name = "advisedHeroicMaxLevel", IsRequired = false)]
        public int AdvisedHeroicMaxLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the availableModes of the zone
        /// </summary>
        [DataMember(Name = "availableModes", IsRequired = false)]
        public IList<string> AvailableModes { get; internal set; }

        /// <summary>
        ///   Gets or sets the lfgNormalMinGearLevel of the zone
        /// </summary>
        [DataMember(Name = "lfgNormalMinGearLevel", IsRequired = false)]
        public int LfgNormalMinGearLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the lfgHeroicMinGearLevel of the zone
        /// </summary>
        [DataMember(Name = "lfgHeroicMinGearLevel", IsRequired = false)]
        public int LfgHeroicMinGearLevel { get; internal set; }

        /// <summary>
        ///   Gets or sets the floors of the zone
        /// </summary>
        [DataMember(Name = "floors", IsRequired = false)]
        public int Floors { get; internal set; }

        /// <summary>
        ///   Gets or sets the id of the bosses
        /// </summary>
        [DataMember(Name = "bosses", IsRequired = false)]
        public IList<Boss> Bosses { get; internal set; }
    }
}
