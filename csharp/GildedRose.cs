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


                // Check if this item's quality reduces with time (normal item or conjured item)
                // If quality can go lower (hasn't reached 0 yet), it is reduced by 1
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality > 0)
                    {
                        item.Quality--;
                    }
                }
                // Check if the item's quality increases with time (aged brie or backstage pass)
                // If quality can go higher (hasn't reached 50 yet), it is increased by 1
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;

                        // Backstage pass quality goes up faster as concert approaches
                        // If 10 days or less are left and quality can go higher, it is increased by 1 (adding up to +2).
                        // If 5 days or less are left and quality can go higher, it is increased by 1 again (adding up to +3).
                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality++;
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality++;
                                }
                            }
                        }
                    }
                }
                // SellIn value decrements for any type of item,
                // except Sulfuras, but it was eliminated at the start of the method.
                item.SellIn --;

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
                            if (item.Quality > 0)
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
                        if (item.Quality < 50)
                        {
                            item.Quality++;
                        }
                    }
                }
            }
        }
    }
}
