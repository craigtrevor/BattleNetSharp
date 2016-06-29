// Copyright (C) 2011 by Sherif Elmetainy (Grendiser@Kazzak-EU)
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   ApiClient for World of WarCraft APIs
    /// </summary>
    public class WowClient : ApiClient
    {

        public WowClient(Region region, string publicKey, string locale)
            : base(region, publicKey, locale)
        {
            _publicKey = publicKey;
            _locale = locale;
        }

        private readonly string _publicKey;
        private readonly string _locale;

        /// <summary>
        ///   Begins an asynchronous operation to get information about an achievement
        /// </summary>
        /// <param name="achievementId"> achievement id </param>
        /// <returns> The state of the async operation </returns>
        public Task<Achievement> GetAchievementAsync(int achievementId)
        {
            return GetAsync<Achievement>("/wow/achievement/" + achievementId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   begins an async operation to retrieve information about a spell
        /// </summary>
        /// <param name="spellId"> spell id </param>
        /// <returns> async operation result </returns>
        public Task<Spell> GetSpellAsync(int spellId)
        {
            return GetAsync<Spell>("/wow/spell/" + spellId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Get the battlegroups for the region
        /// </summary>
        /// <returns> The status of the async operation </returns>
        public Task<BattlegroupsResponse> GetBattlegroupsAsync()
        {
            return GetAsync<BattlegroupsResponse>("/wow/data/battlegroups/" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Character Races cache
        /// </summary>
        private static readonly MemoryCache<string, ReadOnlyCollection<CharacterRace>> _races =
            new MemoryCache<string, ReadOnlyCollection<CharacterRace>>();

        /// <summary>
        ///   getting Race information asynchronously
        /// </summary>
        /// <returns> The status of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public async Task<IList<CharacterRace>> GetRacesAsync()
        {
            var races = _races.LookupValue(Locale);
            if (races != null)
            {
                return races;
            }
            var racesResponse = await GetAsync<RacesResponse>("/api/wow/data/character/races", null);
            races = new ReadOnlyCollection<CharacterRace>(racesResponse.Races);
            _races.AddValue(Locale, races);
            return races;
        }
    }
}
