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
            for (var i = 0; i < Items.Count; i++)
            {
                // Check if this item's quality reduces with time (normal item or conjured item)
                // If quality can go lower (hasn't reached 0 yet), it is reduced by 1
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                // Check if the item's quality increases with time (aged brie or backstage pass)
                // If quality can go higher (hasn't reached 50 yet), it is increased by 1
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        // Backstage pass quality goes up faster as concert approaches
                        // If 10 days or less are left and quality can go higher, it is increased by 1 (adding up to +2).
                        // If 5 days or less are left and quality can go higher, it is increased by 1 again (adding up to +3).
                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }
                // SellIn of any item, except Sulfuras, is reduced.
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                // If item has reached the sell by date, the quality:
                // - reduces by double for normal and conjured items,
                // - increases by double for aged brie,
                // - goes to 0 for backstage passes.
                if (Items[i].SellIn < 0)
                {
                    // If quality can go lower for normal and conjured items, it goes down by 1 (adding up to -2).
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        // If a backstage pass has expired, its quality goes to 0.
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    // If aged brie quality can go higher, it is increased by 1 (adding up to +2).
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
