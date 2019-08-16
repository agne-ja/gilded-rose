using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }

        // Normal items

        [Test]
        public void UpdateQuality_NormalItemSellInDays_Lowers()
        {
            var startingSellIn = 1;
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = startingSellIn, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingSellIn, Items[0].SellIn + 1);
        }

        [Test]
        public void UpdateQuality_NormalItemQualityBeforeSellIn_LowersByOne()
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = 5, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality + 1);
        }

        [Test]
        public void UpdateQuality_NormalItemQualityAfterSellIn_LowersByTwo()
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = 0, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality + 2);
        }

        [Test]
        public void UpdateQuality_NormalItemQualityLowerLimitExceededBeforeSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = 5, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_NormalItemQualityLowerLimitExceededAfterSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = -1, Quality = 1 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        // Aged Brie

        [Test]
        public void UpdateQuality_AgedBrieSellInDays_Lowers()
        {
            var startingSellIn = 1;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = startingSellIn, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingSellIn, Items[0].SellIn + 1);
        }

        [Test]
        public void UpdateQuality_AgedBrieQualityBeforeSellIn_IncreasesByOne()
        {
            var startingQuality = 1;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 5, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 1);
        }

        [Test]
        public void UpdateQuality_AgedBrieQualityAfterSellIn_IncreasesByTwo()
        {
            var startingQuality = 1;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 0, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 2);
        }

        [Test]
        public void UpdateQuality_AgedBrieQualityUpperLimitExceededBeforeSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 5, Quality = 50 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test, Combinatorial]
        public void UpdateQuality_AgedBrieQualityUpperLimitExceededAfterSellIn_QualityStaysAtLimit(
            [Values(50, 49)] int testQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 0, Quality = testQuality}};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        // Sulfuras

        [Test]
        public void UpdateQuality_SulfurasQuality_DoesntChange()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.SUFURAS, SellIn = -1, Quality = 80 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(80, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_SulfurasSellIn_DoesntChange()
        {
            var startingSellIn = -1;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.SUFURAS, SellIn = startingSellIn, Quality = 80 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingSellIn, Items[0].SellIn);
        }

        // Backstage Pass

        [Test]
        public void UpdateQuality_BackstagePassSellIn_Lowers()
        {
            var startingSellIn = 5;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = startingSellIn, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingSellIn, Items[0].SellIn + 1);
        }

        [Test, Combinatorial]
        public void UpdateQuality_BackstagePassQualityMoreThanTenDaysBeforeConcert_IncreasesByOne(
            [Values(18, 11)] int testSellIn)
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = testSellIn, Quality = startingQuality }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 1);
        }

        [Test, Combinatorial]
        public void UpdateQuality_BackstagePassQualityTenDaysOrLessBeforeConcert_IncreasesByTwo(
            [Values(10, 6)] int testSellIn)
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = testSellIn, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 2);
        }

        [Test, Combinatorial]
        public void UpdateQuality_BackstagePassQualityFiveDaysOrLessBeforeConcert_IncreasesByThree(
            [Values(1, 5)] int testSellIn)
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = testSellIn, Quality = startingQuality }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 3);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityAfterConcert_GoesToZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 0, Quality = 15 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityUpperLimitExceededWhenMoreThanTenDaysBeforeConcert_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 11, Quality = 50 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test, Combinatorial]
        public void UpdateQuality_BackstagePassQualityUpperLimitExceededWhenTenOrLessDaysBeforeConcert_QualityStaysAtLimit(
            [Values (50, 49)] int testQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 10, Quality = testQuality }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test, Combinatorial]
        public void UpdateQuality_BackstagePassQualityUpperLimitExceededWhenFiveOrLessDaysBeforeConcert_QualityStaysAtLimit(
            [Values (50, 49, 48)] int testQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 5, Quality = testQuality} };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(50, Items[0].Quality);
        }

        // Conjured Items

        [Test]
        public void UpdateQuality_ConjuredItemSellInDays_Lowers()
        {
            var startingSellIn = 1;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = startingSellIn, Quality = 0 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingSellIn, Items[0].SellIn + 1);
        }

        [Test]
        public void UpdateQuality_ConjuredItemQualityBeforeSellIn_LowersByTwo()
        {
            var startingQuality = 5;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 5, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality + 2);
        }

        [Test]
        public void UpdateQuality_ConjuredItemQualityAfterSellIn_LowersByFour()
        {
            var startingQuality = 5;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 0, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality + 4);
        }

        [Test, Combinatorial]
        public void UpdateQuality_ConjuredItemQualityLowerLimitExceededBeforeSellIn_QualityStaysAtLimit(
            [Values (0, 1)] int testQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 5, Quality = testQuality }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }

        [Test, Combinatorial]
        public void UpdateQuality_ConjuredItemQualityLowerLimitExceededAfterSellIn_QualityStaysAtLimit(
            [Values (0, 1, 2, 3)] int testQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 0, Quality = testQuality }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(0, Items[0].Quality);
        }
    }
}
