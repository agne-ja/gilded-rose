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

                // SellIn value decrements for all items (except Sulfuras) regardless of quality.
                item.SellIn--;

                // Check if this item's quality reduces with time (normal item or conjured item)
                // If quality can go lower (hasn't reached 0 yet), it is reduced by 1
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (QualityCanGoLower(item.Quality))
                    {
                        item.Quality--;
                    }
                }
                // Check if the item's quality increases with time (aged brie or backstage pass)
                // If quality can go higher (hasn't reached 50 yet), it is increased by 1
                else
                {
                    if (QualityCanGoHigher(item.Quality))
                    {
                        item.Quality++;

                        // Backstage pass quality goes up faster as concert approaches
                        // If 10 days or less are left and quality can go higher, it is increased by 1 (adding up to +2).
                        // If 5 days or less are left and quality can go higher, it is increased by 1 again (adding up to +3).
                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.SellIn < 10)
                            {
                                if (QualityCanGoHigher(item.Quality))
                                {
                                    item.Quality++;
                                }
                            }

                            if (item.SellIn < 5)
                            {
                                if (QualityCanGoHigher(item.Quality))
                                {
                                    item.Quality++;
                                }
                            }
                        }
                    }
                }

                // If item has reached the sell by date, the quality:
                // - reduces by double for normal and conjured items,
                // - increases by double for aged brie,
                // - goes to 0 for backstage passes.
                if (item.SellIn < 0)
                {
                    // If quality can go lower for normal and conjured items, it goes down by 1 (adding up to -2).
                    if (item.Name != "Aged Brie")
                    {
                        if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (QualityCanGoLower(item.Quality))
                            {
                                item.Quality--;
                            }
                        }
                        // If a backstage pass has expired, its quality goes to 0.
                        else
                        {
                            item.Quality = 0;
                        }
                    }
                    // If aged brie quality can go higher, it is increased by 1 (adding up to +2).
                    else
                    {
                        if (QualityCanGoHigher(item.Quality))
                        {
                            item.Quality++;
                        }
                    }
                }
            }
        }

        private bool QualityCanGoHigher(int quality)
        {
            return quality < 50;
        }

        private bool QualityCanGoLower(int quality)
        {
            return quality > 0;
        }
    }
}
