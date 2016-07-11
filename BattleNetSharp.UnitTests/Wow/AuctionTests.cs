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

using System.Globalization;
using BattleNetSharp.Community.Wow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleNetSharp.UnitTests.Wow
{
    [TestClass]
    public class AuctionTests
    {
        [TestMethod]
        public void TestAuctions()
        {
            var client = new WowClient(TestConstants.TestRegion, Properties.Settings.Default.PublicKey, TestConstants.TestLocale);
            var result = client.GetAuctionDumpAsync(TestConstants.TestAuctionHouseRealm).Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Neutral);
            Assert.IsNotNull(result.Alliance);
            Assert.IsNotNull(result.Alliance.Auctions);
            Assert.IsNotNull(result.Horde);
            Assert.IsNotNull(result.Realm);

            Assert.IsNotNull(result.Horde.Auctions);
            Assert.IsTrue(result.Horde.Auctions.Count > 0);

            Assert.IsTrue(
                result.Horde.ToString().StartsWith(result.Horde.Auctions.Count.ToString(CultureInfo.InvariantCulture)));

            var auction = result.Horde.Auctions.FirstOrDefault(a => a.BuyoutValue.HasValue);
            Assert.IsNotNull(auction);
            Assert.IsTrue(auction.AuctionId > 0);
            Assert.IsTrue(auction.ItemId > 0);
            Assert.IsNotNull(auction.OwnerName != null);
            Assert.IsTrue(auction.Quantity >= 1);
            Assert.IsTrue(auction.CurrentBidValue > 0);
            Assert.IsTrue(auction.BuyoutValue != null && auction.BuyoutValue.Value >= auction.CurrentBidValue);
            Assert.IsNotNull(auction.ToString());
        }
    }
}
