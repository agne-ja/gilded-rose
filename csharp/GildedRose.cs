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

                // SellIn value decrements for all items (except Sulfuras) regardless of quality.
                item.SellIn--;
                
                bool itemExpired = false;
                
                if (item.SellIn < 0)
                    itemExpired = true;


                // Determine item type from the start (to avoid checking it multiple times) and adjust quality accordingly
                switch (item.Name)
                {
                    case "Aged Brie":
                        {
                            if (QualityCanGoHigher(item.Quality))
                            {
                                item.Quality++;
                                if (itemExpired && QualityCanGoHigher(item.Quality))
                                    item.Quality++;
                            }
                            break;
                        }
                    case "Backstage passes to a TAFKAL80ETC concert":
                        {
                            if (itemExpired)
                                item.Quality = 0;
                            else
                            {
                                if (QualityCanGoHigher(item.Quality))
                                {
                                    item.Quality++;
                                    if (item.SellIn < 10 && QualityCanGoHigher(item.Quality))
                                        item.Quality++;
                                    if (item.SellIn < 5 && QualityCanGoHigher(item.Quality))
                                        item.Quality++;
                                }
                            }
                            break;
                        }
                    case "Conjured Mana Cake":
                        {
                            // New feature
                            break;
                        }
                    // Normal items
                    default:
                        {
                            if (QualityCanGoLower(item.Quality))
                            {
                                item.Quality--;
                                if (itemExpired && QualityCanGoLower(item.Quality))
                                    item.Quality--;
                            }
                            break;
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
