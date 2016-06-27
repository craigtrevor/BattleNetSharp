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
        ///   begins an async operation to retrieve information about a spell
        /// </summary>
        /// <param name="spellId"> spell id </param>
        /// <returns> async operation result </returns>
        public Task<Spell> GetSpellAsync(int spellId)
        {
            return GetAsync<Spell>("/wow/spell/" + spellId.ToString(CultureInfo.InvariantCulture) + "?locale=" + _locale + "&apikey=" + _publicKey, null);
        }
    }
}
