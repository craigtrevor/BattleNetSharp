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
using BattleNetSharp.Community.Wow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleNetSharp.UnitTests.Wow
{
    [TestClass]
    public class AchievementTests
    {
        [TestMethod]
        [TestCategory("WOW")]
        public void TestAchievement()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var achievement = client.GetAchievementAsync(TestConstants.WowTestAchievementId).Result;
            Assert.IsNotNull(achievement);
            Assert.IsNotNull(achievement.Id);
            Assert.IsNotNull(achievement.Title);
            Assert.IsNotNull(achievement.Icon);
            Assert.IsNotNull(achievement.Points);
            Assert.IsNotNull(achievement.Description);
            Assert.IsNotNull(achievement.Reward);
            Assert.IsNotNull(achievement.RewardItems);
            Assert.IsTrue(achievement.RewardItems.Count > 0);
            Assert.IsNotNull(achievement.Criteria);
            Assert.IsTrue(achievement.Criteria.Count > 0);
            Assert.IsNotNull(achievement.AccountWide);
            Assert.IsNotNull(achievement.Faction);
        }

    }
}
