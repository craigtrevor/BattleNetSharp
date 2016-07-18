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
        ///   Gets the most recent auction house dump
        /// </summary>
        /// <param name="realm"> Realm name </param>
        /// <returns> the async task </returns>
        public Task<AuctionDump> GetAuctionDumpAsync(string realm)
        {
            return GetAuctionDumpAsync(realm, DateTime.MinValue);
        }

        /// <summary>
        ///   Gets the most recent auction house dump
        /// </summary>
        /// <param name="realm"> Realm name </param>
        /// <param name="ifModifiedSince"> The datetime of the lastModified header of the last auction dump. </param>
        /// <returns> the async task</returns>
        public async Task<AuctionDump> GetAuctionDumpAsync(string realm, DateTime ifModifiedSince)
        {
            var files = await GetAsync<AuctionFilesResponse>("/wow/auction/data/" + GetSlug(realm) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
            if (files == null || files.Files == null || files.Files.Count == 0
                || string.IsNullOrEmpty(files.Files[files.Files.Count - 1].DownloadPath)
                || files.Files[0].LastModifiedUtc <= ifModifiedSince)
            {
                return null;
            }
            var dump = await GetAsync<AuctionDump>(files.Files[0].DownloadPath, null);
            return dump;
        }

        /// <summary>
        ///   Begins an async operation to get boss masterlist information
        /// </summary>
        /// <returns> The status of the async operation </returns>
        public Task<BossMasterlist> GetBossMasterlistAsync()
        {
            return GetAsync<BossMasterlist>("/wow/boss/" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to get boss information
        /// </summary>
        /// <returns> The status of the async operation </returns>
        public Task<Boss> GetBossAsync(int bossId)
        {
            return GetAsync<Boss>("/wow/boss/" + bossId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Gets guild information asynchronously
        /// </summary>
        /// <param name="realm"> realm name </param>
        /// <param name="guildName"> guild name </param>
        /// <param name="fieldsToRetrieve"> the guild fields to retrieve </param>
        /// <returns> the status of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public Task<Guild> GetGuildAsync(string realm, string guildName, GuildFields fieldsToRetrieve)
        {
            string[] fields =
                EnumHelper<GuildFields>.GetNames().Where(
                    name =>
                    name != "All" &&
                    (fieldsToRetrieve & (GuildFields)Enum.Parse(typeof(GuildFields), name, true)) != 0).Select(
                        name => name.ToLowerInvariant()).ToArray();
            string queryString = fields.Length == 0 ? "" : "?fields=" + string.Join(",", fields);
            return GetAsync<Guild>(
                    "/wow/guild/" + GetRealmSlug(realm) + "/" + Uri.EscapeUriString(guildName) + queryString + "&locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   begins an async operation to get the leaders for realm.
        /// </summary>
        /// <param name="realmName"> realm name to get leaders for. If realm is null or empty string, the leaders for the reason are returned. </param>
        /// <returns> Async operation status </returns>
        public Task<ChallengesResponse> GetChallengeLeadersAsync(string realmName)
        {
            if (string.IsNullOrEmpty(realmName))
            {
                realmName = "region";
            }
            return GetAsync<ChallengesResponse>("/wow/challenge/" + GetRealmSlug(realmName) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to get an item information
        /// </summary>
        /// <param name="itemId"> item id </param>
        /// <returns> the status of the async operation </returns>
        public Task<Item> GetItemAsync(int itemId)
        {
            return GetAsync<Item>("/wow/item/" + itemId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Getting Mount information asynchronously
        /// </summary>
        /// <returns> The status of the async operation </returns>
        public Task<MountsResponse> GetMountsAsync()
        {
            return GetAsync<MountsResponse>("/wow/mount/" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Getting Pet information asynchronously
        /// </summary>
        /// <returns> The status of the async operation </returns>
        public Task<BattlePetsResponse> GetBattlePetsAsync()
        {
            return GetAsync<BattlePetsResponse>("/wow/pet/" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to retrieve information about battle pet abilities
        /// </summary>
        /// <param name="abilityId"> id of ability to retrieve </param>
        /// <returns> Async operation status </returns>
        public Task<BattlePetAbility> GetBattlePetAbilityAsync(int abilityId)
        {
            return GetAsync<BattlePetAbility>("/wow/pet/ability/" + abilityId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to retrieve information about battle pet species
        /// </summary>
        /// <param name="speciesId"> id of species to retrieve </param>
        /// <returns> Async operation status </returns>
        public Task<BattlePetSpecies> GetBattlePetSpeciesAsync(int speciesId)
        {
            return GetAsync<BattlePetSpecies>("/wow/pet/species/" + speciesId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to retrieve information about stats of battle pet species
        /// </summary>
        /// <param name="speciesId"> id of species to retrieve </param>
        /// <param name="level"> level of species to retrieve </param>
        /// <param name="breedId"> breedId of species to retrieve </param>
        /// <param name="qualityId"> qualityId of species to retrieve </param>
        /// <returns> Async operation status </returns>
        public Task<PetStats> GetBattlePetStatsAsync(int speciesId, int level, int breedId, int qualityId)
        {
            return GetAsync<PetStats>("/wow/pet/stats/" + speciesId.ToString(CultureInfo.InvariantCulture) + "?level=" + level.ToString(CultureInfo.InvariantCulture) + "&breedId=" + breedId.ToString(CultureInfo.InvariantCulture) + "&qualityId=" + qualityId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
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
            var racesResponse = await GetAsync<RacesResponse>("/wow/data/character/races" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
            races = new ReadOnlyCollection<CharacterRace>(racesResponse.Races);
            _races.AddValue(Locale, races);
            return races;
        }

        /// <summary>
        ///   Character classes cache
        /// </summary>
        private static readonly MemoryCache<string, ReadOnlyCollection<CharacterClass>> _classes =
            new MemoryCache<string, ReadOnlyCollection<CharacterClass>>();

        /// <summary>
        ///   getting class information asynchronously
        /// </summary>
        /// <returns> The status of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public async Task<IList<CharacterClass>> GetClassesAsync()
        {
            var classes = _classes.LookupValue(Locale);
            if (classes != null)
            {
                return classes;
            }
            var classesResponse = await GetAsync<ClassesResponse>("/wow/data/character/classes" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
            classes = new ReadOnlyCollection<CharacterClass>(classesResponse.Classes);
            _classes.AddValue(Locale, classes);
            return classes;
        }

        /// <summary>
        ///   Begins an asynchronous operation to character achievements
        /// </summary>
        /// <returns> The state of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<AchievementsResponse> GetCharacterAchievementsAsync()
        {
            return GetAsync<AchievementsResponse>("/wow/data/character/achievements" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an asynchronous operation to retrieve guild rewards
        /// </summary>
        /// <returns> The state of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<GuildRewardsResponse> GetGuildRewardsAsync()
        {
            return GetAsync<GuildRewardsResponse>("/wow/data/guild/rewards" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an asynchronous operation to retrieve guild perks
        /// </summary>
        /// <returns> The state of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<GuildPerksResponse> GetGuildPerksAsync()
        {
            return GetAsync<GuildPerksResponse>("/wow/data/guild/perks" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an asynchronous operation to guild achievements
        /// </summary>
        /// <returns> The state of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<AchievementsResponse> GetGuildAchievementsAsync()
        {
            return GetAsync<AchievementsResponse>("/wow/data/guild/achievements" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an asynchronous operation to item category names (item classes)
        /// </summary>
        /// <returns> The state of the async operation </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<ItemCategoryNamesResponse> GetItemCategoryNamesAsync()
        {
            return GetAsync<ItemCategoryNamesResponse>("/wow/data/item/classes" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   begins an async operation to retrieve information about class talents
        /// </summary>
        /// <returns> async operation result </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<TalentsResponse> GetTalentsAsync()
        {
            return GetAsync<TalentsResponse>("/wow/data/talents" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   Begins an async operation to retrieve information about battle pet types
        /// </summary>
        /// <returns> Async operation status </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Task<BattlePetTypesResponse> GetBattlePetTypesAsync()
        {
            return GetAsync<BattlePetTypesResponse>("/wow/data/pet/types" + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }

        /// <summary>
        ///   const used to replace accented characters with non-accented ones
        /// </summary>
        private const string AccentedCharacters = "ÂâÄäÃãÁáÀàÊêËëÉéÈèÎîÏïÍíÌìÔôÖöÕõÓóÒòÛûÜüÚúÙùÑñÇç";

        /// <summary>
        ///   const used to replace accented characters with non-accented ones
        /// </summary>
        private const string ReplacedCharacters = "aaaaaaaaaaeeeeeeeeiiiiiiiioooooooooouuuuuuuunncc";

        /// <summary>
        ///   Get a realm or battleground slug
        /// </summary>
        /// <param name="identifier"> string </param>
        /// <returns> slug </returns>
        private static string GetSlug(string identifier)
        {
            var builder = new StringBuilder();
            bool dash = false;
            foreach (char ch in identifier)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    dash = false;
                    // String.Normalise is not available in Portable Libraries
                    int index = AccentedCharacters.IndexOf(ch);
                    builder.Append(index >= 0 ? ReplacedCharacters[index] : char.ToLowerInvariant(ch));
                }
                else if (ch == ' ' && !dash)
                {
                    dash = true;
                    builder.Append('-');
                }
            }
            return builder.ToString();
        }

        /// <summary>
        ///   Get the realm string to use in the Url for guild and character requests
        /// </summary>
        /// <param name="realmName"> Realm name </param>
        /// <returns> realm string to use in Url for guild and character requests </returns>
        public static string GetRealmSlug(string realmName)
        {
            return GetSlug(realmName);
        }
    }
}
