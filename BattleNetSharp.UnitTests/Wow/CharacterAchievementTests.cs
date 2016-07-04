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
    public class CharacterAchievementTests
    {
        [TestMethod]
        public void TestCharacterAchievements()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var achievements = client.GetCharacterAchievementsAsync(TestConstants.WowTestAchievementId).Result;
            Assert.IsNotNull(achievements.Categories);
            Assert.AreEqual(achievements.Categories[0].Name, achievements.Categories[0].ToString());
            var generalCategory = achievements.Categories.FirstOrDefault(c => c.Name == "General");
            Assert.IsNotNull(generalCategory);
            Assert.IsNotNull(generalCategory.Achievements);
            var higherlearning = generalCategory.Achievements.FirstOrDefault(a => a.Title == "Higher Learning");
            Assert.IsNotNull(higherlearning);
            Assert.IsTrue(higherlearning.Id > 0);
            Assert.AreEqual(higherlearning.Title, higherlearning.ToString());
            Assert.IsNotNull(higherlearning.Criteria);
            var criterion = higherlearning.Criteria[5];
            Assert.IsNotNull(criterion.Description);
            Assert.IsTrue(criterion.Id > 0);
            Assert.IsTrue(criterion.OrderIndex > 0);
            Assert.IsTrue(criterion.Max > 0);
            Assert.AreEqual(false, higherlearning.AccountWide);
            Assert.IsNotNull(higherlearning.Description);
            Assert.AreEqual(10, higherlearning.Points);
            Assert.IsNotNull(higherlearning.Reward);
            Assert.IsNotNull(higherlearning.RewardItems);
            Assert.IsTrue(higherlearning.RewardItems.Count > 0);
            var armoredBrownBear = generalCategory.Achievements.FirstOrDefault(a => a.Title == "Armored Brown Bear");
            Assert.IsNotNull(armoredBrownBear);
            Assert.AreEqual(true, armoredBrownBear.AccountWide);

            var questCategory = achievements.Categories.FirstOrDefault(c => c.Name == "Quests");
            Assert.IsTrue(questCategory != null && questCategory.Id > 0);
            Assert.IsNotNull(questCategory.Categories);
            Assert.IsTrue(questCategory.Categories.Count > 0);
        }
    }
}
