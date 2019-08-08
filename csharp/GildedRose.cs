using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;

        public const string BRIE = "Aged Brie";
        public const string BACKSTAGE = "Backstage passes to a TAFKAL80ETC concert";
        public const string SUFURAS = "Sulfuras, Hand of Ragnaros";
        public const string CONJURED = "Conjured Mana Cake";

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
                if (item.Name == SUFURAS)
                    continue;

                // SellIn value decrements for all items (except Sulfuras) regardless of quality.
                item.SellIn--;
                
                bool itemExpired = false;
                
                if (item.SellIn < 0)
                    itemExpired = true;


                // Determine item type from the start (to avoid checking it multiple times) and adjust quality accordingly
                switch (item.Name)
                {
                    // Increases in quality the older it gets (by 1 before SellIn, by 2 after).
                    case BRIE:
                        {
                            if (QualityCanGoHigher(item.Quality))
                            {
                                item.Quality++;
                                if (itemExpired && QualityCanGoHigher(item.Quality))
                                    item.Quality++;
                            }
                            break;
                        }
                        // Increases in Quality as its SellIn value approaches.
                        // -by 1 when there are more than 10 days,
                        // -by 2 when there are 10 days or less,
                        // -by 3 when there are 5 days or less but.
                        // Quality drops to 0 after the concert.
                    case BACKSTAGE:
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
                    // Degrades in quality twice as fast as regular items (by 2 before SellIn, by 4 after)
                    case CONJURED:
                        {
                            int qualityChange;
                            if (itemExpired) qualityChange = 4;
                            else qualityChange = 2;

                            while(QualityCanGoLower(item.Quality) && qualityChange > 0)
                            {
                                item.Quality--;
                                qualityChange--;
                            }
                            break;
                        }
                    // Degrades in quality the older it gets (by 1 before SellIn, by 2 after).
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
