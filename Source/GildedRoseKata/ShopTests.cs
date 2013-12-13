using System.Linq;
using NUnit.Framework;

namespace GildedRoseKata
{
    [TestFixture]
    public class ShopWhenADayPassesTests
    {
        [TestCase("Conjured Mana Cake", 6)]
        public void ThenTheQualityOfConjuredItemsDecreasesByTwo(string itemName, int initialQuality)
        {
            var shop = new Shop();
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(initialQuality - 2));
        }

        [TestCase("Conjured Mana Cake")]
        public void ThenTheQualityOfConjuredItemsNeverGoesBelowZero(string itemName)
        {
            var shop = new Shop();
            shop.Items.Clear();
            shop.Items.Add(new Shop.Item { Name = itemName, SellIn = 0, Quality = 0 });
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(0));
        }

        [TestCase("+5 Dexterity Vest", 20)]
        [TestCase("Elixir of the Mongoose", 7)]
        public void ThenTheQualityOfNormalItemsDecreasesByOne(string itemName, int initialQuality)
        {
            var shop = new Shop();
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(initialQuality - 1));
        }

        [TestCase("+5 Dexterity Vest")]
        [TestCase("Elixir of the Mongoose")]
        [TestCase("Conjured Mana Cake")]
        public void ThenTheQualityOfNormalItemsNeverDropsBelowZero(string itemName)
        {
            var shop = new Shop();
            shop.Items.Clear();
            shop.Items.Add(new Shop.Item { Name = itemName, SellIn = 0, Quality = 0 });
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(0));
        }

        [Test]
        public void ThenTheQualityOfTheAgedBrieGoesUp()
        {
            const int originalQuality = 0;
            var shop = new Shop();

            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == "Aged Brie").Quality, Is.EqualTo(originalQuality + 1));
        }

        [Test]
        public void ThenTheQualityOfAgedBrieNeverGoesAboveFifty()
        {
            const string itemName = "Aged Brie";

            var shop = new Shop();
            shop.Items.Clear();
            shop.Items.Add(new Shop.Item { Name = itemName, SellIn = 0, Quality = 50 });
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(50));
        }

        [Test]
        public void ThenTheHandOfRagnarossQualityDoesNotChange()
        {
            const int initialQuality = 80;
            var shop = new Shop();
            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == "Sulfuras, Hand of Ragnaros").Quality, Is.EqualTo(initialQuality));
        }

        [TestCase(15, 1)]
        [TestCase(11, 1)]
        [TestCase(10, 2)]
        [TestCase(6, 2)]
        [TestCase(5, 3)]
        [TestCase(1, 3)]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        public void ThenTheQualityOfTheTicketChangesInStrangeWays(int daysTillGig, int newItemQuality)
        {
            const string itemName = "Backstage passes to a TAFKAL80ETC concert";
            var shop = new Shop();
            shop.Items.Clear();
            shop.Items.Add(new Shop.Item { Name = itemName, SellIn = daysTillGig, Quality = 0 });

            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.EqualTo(newItemQuality));
        }

        [TestCase(15, 50)]
        [TestCase(15, 49)]
        [TestCase(15, 48)]
        [TestCase(10, 50)]
        [TestCase(10, 49)]
        [TestCase(10, 48)]
        [TestCase(10, 47)]
        [TestCase(5, 50)]
        [TestCase(5, 49)]
        [TestCase(5, 48)]
        [TestCase(5, 47)]
        [TestCase(5, 46)]
        public void ThenTheQualityOfTheTicketNeverGoesAboveFifty(int daysTillGig, int initialQuality)
        {
            const string itemName = "Backstage passes to a TAFKAL80ETC concert";

            var shop = new Shop();
            shop.Items.Clear();
            shop.Items.Add(new Shop.Item { Name = itemName, SellIn = daysTillGig, Quality = initialQuality });

            shop.UpdateQuality();

            Assert.That(shop.Items.First(item => item.Name == itemName).Quality, Is.LessThanOrEqualTo(50));
        }
    }
}