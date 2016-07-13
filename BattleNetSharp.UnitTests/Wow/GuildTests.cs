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
using System.Linq;
using BattleNetSharp.Community.Wow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleNetSharp.UnitTests.Wow
{
    [TestClass]
    public class GuildTests
    {
        [TestMethod]
        [TestCategory("WOW")]
        public void TestGuild()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var guild = client.GetGuildAsync(TestConstants.TestRealmName, TestConstants.TestGuildName, GuildFields.All).Result;
            Assert.IsNotNull(guild);
            Assert.IsNotNull(guild.Members);
            Assert.IsNotNull(guild.Achievements);
            Assert.IsTrue(guild.Members.Count > 0);
            Assert.IsTrue(guild.Members.Any(m => m.Rank > 0));
            Assert.IsTrue(string.Equals(guild.Members[0].Character.Realm, TestConstants.TestRealmName,
                                        StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(string.Equals(guild.Name, TestConstants.TestGuildName, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(guild.ToString());
            Assert.IsNotNull(guild.Faction);
            Assert.IsNotNull(guild.Members[0].ToString());
            Assert.IsTrue(guild.AchievementPoints > 0);
            Assert.IsNotNull(guild.BattleGroupName);
            Assert.IsNotNull(guild.Emblem);
            Assert.IsNotNull(guild.Emblem.BackgroundColor);
            Assert.IsNotNull(guild.Emblem.BorderColor);
            Assert.IsNotNull(guild.Emblem.IconColor);
            Assert.IsTrue(guild.Emblem.Icon > 0);
            Assert.IsTrue(guild.Emblem.Border >= 0);
            Assert.IsTrue(guild.Level >= 25);
            Assert.AreEqual(TestConstants.TestRealmName, guild.Realm);

            Assert.IsNotNull(guild.News);
            Assert.IsTrue(guild.News.All(n => n != null));
            Assert.IsTrue(guild.News.All(n => n.Achievement != null
                                              ||
                                              (n.GuildNewsItemType != GuildNewsItemType.PlayerAchievement &&
                                               n.GuildNewsItemType != GuildNewsItemType.GuildAchievement)));
            Assert.IsTrue(guild.News.All(n => n.ItemId > 0
                                              ||
                                              (n.GuildNewsItemType == GuildNewsItemType.PlayerAchievement ||
                                               n.GuildNewsItemType == GuildNewsItemType.GuildAchievement)));

            Assert.IsNotNull(guild.Challenges);
            Assert.AreNotEqual(0, guild.Challenges.Count);
        }
    }
}
