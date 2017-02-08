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
    public class RealmStatusTests
    {
        [TestMethod]
        [TestCategory("WOW")]
        public void TestRealmStatus()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var task = client.GetRealmStatusAsync();
            RealmStatusResponse response = task.Result;
            Assert.IsNotNull(response);
            Assert.IsTrue(new RealmStatusResponse().ToString().Contains('0'));
            Assert.IsNotNull(response.ToString());
            Assert.IsNotNull(response.Realms);
            Assert.IsTrue(response.Realms.Count > 0);
            Assert.IsTrue(response.Realms.Any(r => r.IsPvp));
            Assert.IsTrue(response.Realms.Any(r => r.IsRP));
            Assert.IsTrue(response.Realms.All(r => r.Locale != null));
            Assert.IsTrue(response.Realms.All(r => r.Name != null));
            Assert.IsTrue(!response.Realms.Any(r => r.ToString().Equals(null)));
            Assert.IsTrue(response.Realms.All(r => r.Slug != null));
            Assert.IsTrue(response.Realms.All(r => r.BattleGroupName != null));
            Assert.IsTrue(response.Realms.Any(r => r.Status));
            Assert.IsTrue(response.Realms.All(r => r.TimeZone != null));
        }
    }
}
