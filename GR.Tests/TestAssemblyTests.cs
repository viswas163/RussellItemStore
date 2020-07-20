using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GR.Tests
{
    public static class DemoData
    {
        public static IList<Item> getData()
        {
            return new List<Item>
            {
                new Ordinary {
                    Name = "+5 Dexterity Vest",
                    SellIn = 0,
                    Quality = 0},
                new AgedBrie {
                    Name = "Aged Brie",
                    SellIn = 2,
                    Quality = 50},
                new Ordinary {
                    Name = "Elixir of the Mongoose",
                    SellIn = 5,
                    Quality = 7},
                new Sulfuras {
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = 0,
                    Quality = 80},
                new BackstagePass {
                    Name = "Backstage passes to a B6 concert",
                    SellIn = 15,
                    Quality = 20 },
                new BackstagePass {
                    Name = "Backstage passes to a B7 concert",
                    SellIn = 5,
                    Quality = 50 },
                new BackstagePass {
                    Name = "Backstage passes to a B5 concert",
                    SellIn = 10,
                    Quality = 30 },
                new BackstagePass {
                    Name = "Backstage passes to a B4 concert",
                    SellIn = 5,
                    Quality = 33 },
                new BackstagePass {
                    Name = "Backstage passes to a B3 concert",
                    SellIn = 6,
                    Quality = 27 },
                new BackstagePass {
                    Name = "Backstage passes to a B2 concert",
                    SellIn = 1,
                    Quality = 13 },
                new BackstagePass {
                    Name = "Backstage passes to a B1 concert",
                    SellIn = 0,
                    Quality = 25 },
                new Conjured {
                    Name = "Conjured Mana Cake 1",
                    SellIn = 3,
                    Quality = 6 },
                new Conjured {
                    Name = "Conjured Mana Cake 2",
                    SellIn = -1,
                    Quality = 3},
                new Conjured {
                    Name = "Conjured Mana Cake 3",
                    SellIn = 0,
                    Quality = 6 }
            };
        }
    }
    public class ExistingStoreSingleDayUpdateTest
    {
        private readonly IItemsStore _app;

        public ExistingStoreSingleDayUpdateTest()
        {
            IList<Item> Items = DemoData.getData();
            _app = new ExistingItemsStore(Items);
            _app.UpdateInventory();
        }
        [Fact]
        public void DexterityVest_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B1 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByThree()
        {
            Assert.Equal(16, _app.Items.First(x => x.Name == "Backstage passes to a B2 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTwo()
        {
            Assert.Equal(29, _app.Items.First(x => x.Name == "Backstage passes to a B3 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByThreeForFiveDays()
        {
            Assert.Equal(36, _app.Items.First(x => x.Name == "Backstage passes to a B4 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTwoForTenDays()
        {
            Assert.Equal(32, _app.Items.First(x => x.Name == "Backstage passes to a B5 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByOne()
        {
            Assert.Equal(21, _app.Items.First(x => x.Name == "Backstage passes to a B6 concert").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByTwo()
        {
            Assert.Equal(4, _app.Items.First(x => x.Name == "Conjured Mana Cake 1").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFour()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 2").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFourForZeroDays()
        {
            Assert.Equal(2, _app.Items.First(x => x.Name == "Conjured Mana Cake 3").Quality);
        }
        [Fact]
        public void AgedBrie_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Aged Brie").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Backstage passes to a B7 concert").Quality);
        }
    }
    public class UpdatedStoreSingleDayUpdateTest
    {
        private readonly IItemsStore _app;

        public UpdatedStoreSingleDayUpdateTest()
        {
            IList<Item> Items = DemoData.getData();
            _app = new UpdatedItemsStore(Items);
            _app.UpdateInventory();
        }
        [Fact]
        public void DexterityVest_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B1 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByThree()
        {
            Assert.Equal(16, _app.Items.First(x => x.Name == "Backstage passes to a B2 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTwo()
        {
            Assert.Equal(29, _app.Items.First(x => x.Name == "Backstage passes to a B3 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByThreeForFiveDays()
        {
            Assert.Equal(36, _app.Items.First(x => x.Name == "Backstage passes to a B4 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTwoForTenDays()
        {
            Assert.Equal(32, _app.Items.First(x => x.Name == "Backstage passes to a B5 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByOne()
        {
            Assert.Equal(21, _app.Items.First(x => x.Name == "Backstage passes to a B6 concert").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByTwo()
        {
            Assert.Equal(4, _app.Items.First(x => x.Name == "Conjured Mana Cake 1").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFour()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 2").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFourForZeroDays()
        {
            Assert.Equal(2, _app.Items.First(x => x.Name == "Conjured Mana Cake 3").Quality);
        }
        [Fact]
        public void AgedBrie_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Aged Brie").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Backstage passes to a B7 concert").Quality);
        }
    }
    public class ExistingStoreMultipleDayUpdateTest
    {
        private readonly IItemsStore _app;
        const int DAYS = 5;
        public ExistingStoreMultipleDayUpdateTest()
        {
            IList<Item> Items = DemoData.getData();
            _app = new ExistingItemsStore(Items);
            for (int i = 0; i < DAYS; i++)
                _app.UpdateInventory();
        }
        [Fact]
        public void DexterityVest_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B1 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZeroForOneDay()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B2 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFourteen()
        {
            Assert.Equal(41, _app.Items.First(x => x.Name == "Backstage passes to a B3 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFifteen()
        {
            Assert.Equal(48, _app.Items.First(x => x.Name == "Backstage passes to a B4 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTen_ForTenDays()
        {
            Assert.Equal(40, _app.Items.First(x => x.Name == "Backstage passes to a B5 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFive()
        {
            Assert.Equal(25, _app.Items.First(x => x.Name == "Backstage passes to a B6 concert").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldBeZero_ForThreeDays()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 1").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFour()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 2").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldBeZero_ForZeroDays()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 3").Quality);
        }
        [Fact]
        public void AgedBrie_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Aged Brie").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeFifty_ForFiveDays()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Backstage passes to a B7 concert").Quality);
        }
    }
    public class UpdatedStoreMultipleDayUpdateTest
    {
        private readonly IItemsStore _app;
        const int DAYS = 5;
        public UpdatedStoreMultipleDayUpdateTest()
        {
            IList<Item> Items = DemoData.getData();
            _app = new UpdatedItemsStore(Items);
            for (int i = 0; i < DAYS; i++)
                _app.UpdateInventory();
        }
        [Fact]
        public void DexterityVest_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZero()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B1 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeZeroForOneDay()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Backstage passes to a B2 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFourteen()
        {
            Assert.Equal(41, _app.Items.First(x => x.Name == "Backstage passes to a B3 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFifteen()
        {
            Assert.Equal(48, _app.Items.First(x => x.Name == "Backstage passes to a B4 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByTen_ForTenDays()
        {
            Assert.Equal(40, _app.Items.First(x => x.Name == "Backstage passes to a B5 concert").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldIncreaseByFive()
        {
            Assert.Equal(25, _app.Items.First(x => x.Name == "Backstage passes to a B6 concert").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldBeZero_ForThreeDays()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 1").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldDecreaseByFour()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 2").Quality);
        }
        [Fact]
        public void Conjured_Quality_ShouldBeZero_ForZeroDays()
        {
            Assert.Equal(0, _app.Items.First(x => x.Name == "Conjured Mana Cake 3").Quality);
        }
        [Fact]
        public void AgedBrie_Quality_ShouldBeFifty()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Aged Brie").Quality);
        }
        [Fact]
        public void Backstage_Quality_ShouldBeFifty_ForFiveDays()
        {
            Assert.Equal(50, _app.Items.First(x => x.Name == "Backstage passes to a B7 concert").Quality);
        }
    }
}