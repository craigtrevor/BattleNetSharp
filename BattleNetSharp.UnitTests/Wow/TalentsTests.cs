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

using BattleNetSharp.Community.Wow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleNetSharp.UnitTests.Wow
{
    [TestClass]
    public class TalentsTests
    {
        [TestMethod]
        [TestCategory("WOW")]
        public void TestTalents()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var talents = client.GetTalentsAsync().Result;
            Assert.IsNotNull(talents);
            Assert.IsNotNull(talents.Warrior);
            Assert.IsNotNull(talents.Warlock);
            Assert.IsNotNull(talents.Shaman);
            Assert.IsNotNull(talents.Rogue);
            Assert.IsNotNull(talents.Priest);
            Assert.IsNotNull(talents.Paladin);
            Assert.IsNotNull(talents.Monk);
            Assert.IsNotNull(talents.Mage);
            Assert.IsNotNull(talents.Hunter);
            Assert.IsNotNull(talents.Druid);
            Assert.IsNotNull(talents.DeathKnight);
        }
    }
}
