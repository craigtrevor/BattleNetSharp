using System;
using BattleNetSharp.Community.Wow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleNetSharp.UnitTests.Wow
{
    [TestClass]
    public class BattlegroupsTests
    {
        [TestMethod]
        [TestCategory("WOW")]
        public void TestBattlegroups()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var battlegroups = client.GetBattlegroupsAsync().Result;
            Assert.IsNotNull(battlegroups);
        }
    }
}
