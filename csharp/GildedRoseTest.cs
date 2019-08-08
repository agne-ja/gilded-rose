using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private int QualityUpperLimit = 50;
        private int QualityLowerLimit = 0;

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
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = 5, Quality = QualityLowerLimit } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityLowerLimit, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_NormalItemQualityLowerLimitExceededAfterSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = -1, Quality = 1 } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityLowerLimit, Items[0].Quality);
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
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 5, Quality = QualityUpperLimit } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityUpperLimit, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_AgedBrieQualityUpperLimitExceededAfterSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BRIE, SellIn = 0, Quality = QualityUpperLimit },
                                                 new Item { Name = GildedRose.BRIE, SellIn = 0, Quality = QualityUpperLimit - 1 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityUpperLimit, Items[0].Quality);
            Assert.AreEqual(QualityUpperLimit, Items[1].Quality);
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

        [Test]
        public void UpdateQuality_BackstagePassQualityMoreThanTenDaysBeforeConcert_IncreasesByOne()
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 18, Quality = startingQuality },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 11, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 1);
            Assert.AreEqual(startingQuality, Items[1].Quality - 1);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityTenDaysOrLessBeforeConcert_IncreasesByTwo()
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 10, Quality = startingQuality },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 6, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 2);
            Assert.AreEqual(startingQuality, Items[1].Quality - 2);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityFiveDaysOrLessBeforeConcert_IncreasesByThree()
        {
            var startingQuality = 10;
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 5, Quality = startingQuality },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 1, Quality = startingQuality } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(startingQuality, Items[0].Quality - 3);
            Assert.AreEqual(startingQuality, Items[1].Quality - 3);
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
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 11, Quality = QualityUpperLimit } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityUpperLimit, Items[0].Quality);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityUpperLimitExceededWhenTenOrLessDaysBeforeConcert_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 10, Quality = QualityUpperLimit - 1 },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 6, Quality = QualityUpperLimit } };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityUpperLimit, Items[0].Quality);
            Assert.AreEqual(QualityUpperLimit, Items[1].Quality);
        }

        [Test]
        public void UpdateQuality_BackstagePassQualityUpperLimitExceededWhenFiveOrLessDaysBeforeConcert_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.BACKSTAGE, SellIn = 5, Quality = QualityUpperLimit - 2 },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 3, Quality = QualityUpperLimit - 1 },
                                                 new Item { Name = GildedRose.BACKSTAGE, SellIn = 1, Quality = QualityUpperLimit }, };
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityUpperLimit, Items[0].Quality);
            Assert.AreEqual(QualityUpperLimit, Items[1].Quality);
            Assert.AreEqual(QualityUpperLimit, Items[2].Quality);
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

        [Test]
        public void UpdateQuality_ConjuredItemQualityLowerLimitExceededBeforeSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 5, Quality = QualityLowerLimit },
                                                 new Item { Name = GildedRose.CONJURED, SellIn = 5, Quality = QualityLowerLimit + 1 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityLowerLimit, Items[0].Quality);
            Assert.AreEqual(QualityLowerLimit, Items[1].Quality);
        }

        [Test]
        public void UpdateQuality_ConjureditemQualityLowerLimitExceededAfterSellIn_QualityStaysAtLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = GildedRose.CONJURED, SellIn = 0, Quality = QualityLowerLimit },
                                                 new Item { Name = GildedRose.CONJURED, SellIn = 0, Quality = QualityLowerLimit + 3 }};
            GildedRose app = new GildedRose(Items);

            app.UpdateQuality();

            Assert.AreEqual(QualityLowerLimit, Items[0].Quality);
            Assert.AreEqual(QualityLowerLimit, Items[1].Quality);
        }
    }
}
