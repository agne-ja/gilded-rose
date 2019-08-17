using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }
        public void UpdateQuality()
        {
            // Changed for to foreach, because 'item' instead of 'Items[i]' looks cleaner.
            foreach (var item in Items)
            {
                // Neither the quality nor the sellIn of Sulfuras items changes.
                // That's why the loop can immediately move on to the next iteration.
                if (item.Name == "Sulfuras, Hand of Ragnaros")
                    continue;

                // Determine item type from the start (to avoid checking it multiple times) and adjust quality accordingly.
                switch (item.Name)
                {

                    // Increases in quality the older it gets (by 1 before SellIn, by 2 after).
                    case "Aged Brie":
                        {
                            BrieChangeQuality(item);
                            break;
                        }
                    case "Backstage passes to a TAFKAL80ETC concert":
                        {
                            BackstagePassChangeQuality(item);
                            break;
                        }
                    case "Conjured Mana Cake":
                        {
                            ConjuredChangeQuality(item);
                            break;
                        }
                    default:
                        {
                            NormalItemChangeQuality(item);
                            break;
                        }
                }

                /*
                 * Another idea would be to create a different class for each item type.
                 * The classes would need to extend the Item class (to inherit Name, Quality and SellIn properties). 
                 * If the requirements didn't prohibit it, the Item class could have a virtual method ChangeQuality() 
                 * that would be overriden in derived classes and update the quality depending on the item type.
                 * In the main() method, items then could be created using the derived class constructor (e.g. Item item = new AgedBrie())
                 * and in the UpdateQuality() method item.ChangeQuality() could be called for each element in the list.
                 */

            }

        }

        // Increases in quality the older it gets(by 1 before SellIn, by 2 after).
        private void BrieChangeQuality(Item item)
        {
            item.SellIn--;
            if (item.Quality < 50)
            {
                item.Quality++;
                if (item.SellIn < 0 && item.Quality < 50)
                    item.Quality++;
            }
        }

        // Increases in Quality as its SellIn value approaches.
        // -by 1 when there are more than 10 days,
        // -by 2 when there are 10 days or less,
        // -by 3 when there are 5 days or less but.
        // Quality drops to 0 after the concert.
        private void BackstagePassChangeQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;
                if (item.SellIn <= 10 && item.Quality < 50)
                    item.Quality++;
                if (item.SellIn <= 5 && item.Quality < 50)
                    item.Quality++;
            }
            item.SellIn--;
            if (item.SellIn < 0)
                item.Quality = 0;
        }

        // Degrades in quality twice as fast as regular items (by 2 before SellIn, by 4 after)
        private void ConjuredChangeQuality(Item item)
        {
            int qualityChange;
            item.SellIn--;
            if (item.SellIn < 0) qualityChange = 4;
            else qualityChange = 2;

            while (item.Quality > 0 && qualityChange > 0)
            {
                item.Quality--;
                qualityChange--;
            }
        }

        // Degrades in quality the older it gets (by 1 before SellIn, by 2 after).
        private void NormalItemChangeQuality(Item item)
        {
            item.SellIn--;
            if (item.Quality > 0)
            {
                item.Quality--;
                if (item.SellIn < 0 && item.Quality > 0)
                    item.Quality--;
            }
        }
    }
}
