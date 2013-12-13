using System;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class Shop
    {
        public void UpdateQuality()
        {
            foreach (var item in Items) item.Quality = QualityFunctionMap[item.Name](item.Quality, item.SellIn);
        }

        private static int CalculateQualityOfBackstagePass(int sellIn, int quality)
        {
            if (sellIn <= 0) return 0;
            if (sellIn > 0) quality++;
            if (sellIn > 0 && sellIn <= 10) quality++;
            if (sellIn > 0 && sellIn <= 5) quality++;
            return quality > 50 ? 50 : quality;
        }

        public IList<Item> Items = new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };

        public IDictionary<string, Func<int, int, int>> QualityFunctionMap = new Dictionary<string, Func<int, int, int>>
        {
            {"Aged Brie", (quality, sellIn) => quality < 50 ? quality + 1 : quality},
            {"Conjured Mana Cake", (quality, sellIn) => quality > 0 ? quality - 2 : quality},
            {"+5 Dexterity Vest", (quality, sellIn) => quality > 0 ? quality - 1 : quality},
            {"Elixir of the Mongoose", (quality, sellIn) => quality > 0 ? quality - 1 : quality},
            {"Sulfuras, Hand of Ragnaros", (quality, sellIn) => quality},
            {"Backstage passes to a TAFKAL80ETC concert", (quality, sellIn) => CalculateQualityOfBackstagePass(sellIn, quality)},
        };

        public class Item
        {
            public string Name { get; set; }
            public int SellIn { get; set; }
            public int Quality { get; set; }
        }
    }
}