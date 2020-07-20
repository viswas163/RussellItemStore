using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Moq;

namespace GR
{
    public interface IItemsStore
    {
        IList<Item> Items { get; set; }
        void UpdateInventory();
    }

    public class ExistingItemsStore : IItemsStore
    {
        IList<Item> _items;
        public IList<Item> Items 
        {
            get => _items; 
            set => _items = new List<Item>(value); 
        }
        public ExistingItemsStore(IList<Item> items)
        {
            Items = new List<Item>(items);
        }

        public void UpdateInventory()
        {
            Console.WriteLine("Updating inventory");

            foreach (var item in Items)
            {
                Console.WriteLine(" - Item: {0}", item.Name);
                if (item.Name != "Aged Brie" && !item.Name.Contains("Backstage passes"))
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            if (item.Name.Contains("Conjured Mana Cake"))
                            {
                                item.Quality = Math.Max(0, item.Quality - 2);
                            }
                            else // If item is Ordinary
                            {
                                item.Quality -= 1;
                            }
                        }
                    }
                }
                else // if item is Aged brie or Backstage pass
                {
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;

                        if (item.Name.Contains("Backstage passes"))
                        {
                            if (item.SellIn < 11)
                            {
                                item.Quality = Math.Min(50, item.Quality + 1);
                            }

                            if (item.SellIn < 6)
                            {
                                item.Quality = Math.Min(50, item.Quality + 1);
                            }
                        }
                    }
                }

                // Decrement Sellin for any item other than Sulfuras
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn -= 1;
                }

                // If Sellin > 0, then no need to compute quality past expiration
                if (item.SellIn >= 0)
                    continue;

                // If Sellin < 0
                if (item.Name != "Aged Brie")
                {
                    if (item.Name.Contains("Backstage passes"))
                    {
                        item.Quality = 0;
                    }
                    else // If item is Ordinary OR Sulfuras OR Conjured
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            if (item.Name.Contains("Conjured Mana Cake"))
                            {
                                item.Quality = Math.Max(0, item.Quality - 2);
                            }
                            else // If item is Ordinary
                            {
                                item.Quality = Math.Max(0, item.Quality - 1);
                            }
                        }
                    }
                }
                else // If item is Aged Brie
                {
                    if (item.Quality < 50)
                    {
                        item.Quality += 1;
                    }
                }
            }
            Console.WriteLine("Inventory update complete");
        }
    }

    // Another way to approach the implementation of the system
    public class UpdatedItemsStore : IItemsStore
    {
        IList<Item> _items;
        public IList<Item> Items
        {
            get => _items;
            set => _items = new List<Item>(value);
        }
        public UpdatedItemsStore(IList<Item> items)
        {
            Items = new List<Item>(items);
        }

        public void UpdateInventory()
        {
            Console.WriteLine("Updating inventory");

            foreach (var item in Items)
            {
                Console.WriteLine(" - Item: {0}", item.Name);

                item.UpdateQuality();
                item.UpdateSellin();
            }

            Console.WriteLine("Inventory update complete");
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var Items = new List<Item>
            {
                new Ordinary {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Ordinary {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new BackstagePass
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            GenerateInventory(Items);

            Console.ReadKey();
        }

        private static void GenerateInventory(IList<Item> Items)
        {
            var app = new UpdatedItemsStore(Items);

            app.UpdateInventory();

            var filename = $"inventory_{DateTime.Now:yyyyddMM-HHmmss}.txt";
            var inventoryOutput = JsonConvert.SerializeObject(app.Items, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), inventoryOutput);
        }
    }
    public abstract class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
        public abstract void UpdateQuality();
        public virtual void UpdateSellin() 
        {
            SellIn = Math.Max(0, SellIn - 1);
        }
    }
    public class AgedBrie : Item
    {
        public override void UpdateQuality()
        {            
            Quality = Math.Min(50, Quality + 1);

            if (SellIn <= 0)
                Quality = Math.Min(50, Quality + 1);
        }
    }
    public class BackstagePass : Item
    {
        public override void UpdateQuality()
        {
            Quality = Math.Min(50, Quality + 1);

            if (SellIn < 11)
                Quality = Math.Min(50, Quality + 1);
            
            if (SellIn < 6)
                Quality = Math.Min(50, Quality + 1);
            
            if (SellIn <= 0)
                Quality = 0;
        }
    }
    public class Conjured : Item
    {
        public override void UpdateQuality()
        {
            Quality = Math.Max(0, Quality - 2);

            if (SellIn <= 0) 
                Quality = Math.Max(0, Quality - 2);
        }
    }
    public class Ordinary : Item
    {
        public override void UpdateQuality()
        {
            Quality = Math.Max(0, Quality - 1);

            if (SellIn <= 0)
                Quality = Math.Max(0, Quality - 1);
        }
    }
    public class Sulfuras : Item
    {
        public override void UpdateQuality()
        {
            return;
        }
        public override void UpdateSellin()
        {
            return;
        }
    }
}