using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul.src
{

    // Not just a player inventory, this really should be renamed in more professional cases, or even be treated as a parent class
    // This also should be in a seperate file, too bad!
    class Inventory
    {
        private Dictionary<string, Item> items;
        private int currentWeight;
        public int MaxSize { get; }

        public Inventory(int maxWeight) 
        {
            items = new Dictionary<string, Item>();
            MaxSize = maxWeight;
        }

        public void RemoveFromInventory(string desireditem)
        {
            if (currentWeight > 0)
            {
                currentWeight -= GetItem(desireditem).Weight;
            }

            items.Remove(desireditem);
        }

        public int GetCurrentWeight()
        {
            return currentWeight;
        }

        public void AddToInventory(Item item)
        {
            if (currentWeight < MaxSize)
            {
                currentWeight += item.Weight;
            }
            items.Add(item.Name, item);
        }

        public Item GetItem(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                return items[itemName];
            }

            return null;
        }

        public string ListItems()
        {
            string temp = "";

            if (items.Count == 0)
            {
                return "There aren't any objects in this room!";
            }

            foreach (var item in items)
            {
                temp += item.Value.Name;
            }
            return temp;
        }
    }

    class Player
    {
        private int health;
        private Logger logger;
        private Inventory inventory;
        public Room CurrentRoom { get; set; }

        public Player() 
        {
            health = 100;
            CurrentRoom = null;
            logger = new Logger();
            //Give the player inventory a size of 20 kg
            inventory = new Inventory(10);
        }

        public bool IsAlive() 
        {
            return !(health <= 0);
        }

        public void Damage(int damage)
        {
            logger.LogToConsole($"Damage: {damage}", Logger.LogType.Info);
            health -= damage;
        }

        public void Stats()
        {
            Console.WriteLine($"Current health: {health}");
            Console.WriteLine($"Your items: {inventory.ListItems()}");
            Console.WriteLine($"You have used {inventory.GetCurrentWeight()} inventory space out of the {inventory.MaxSize}");
        }

        //Returns a string of all the items, this should mostly be used for printing an such
        public string GetAllItemsInventory()
        {
            return inventory.ListItems();
        }

        public void AddItemToInv(Item item)
        {
            inventory.AddToInventory(item);
        }

        public void PickUpItem(string desireditem)
        {
            Item item = CurrentRoom.roomInv.GetItem(desireditem);

            if (item == null)
            {
                Console.WriteLine("This item doesn't exist");
                return;
            }

            CurrentRoom.roomInv.RemoveFromInventory(item.Name);
            inventory.AddToInventory(item);
        }

        public void UseItem(string desiredItem)
        {
            Item item = inventory.GetItem(desiredItem);

            if(item != null)
            {
                item.Use(this);
                inventory.RemoveFromInventory(item.Name);
            }
            else
            {
                Console.WriteLine("This item doesn't exist");
            }
        }

        public void Heal(int amount)
        {
            if((amount + health) < 100)
            {
                health += amount;
                logger.LogToConsole($"New health: {amount}", Logger.LogType.Info);
            }
            else
            {
                logger.LogToConsole("Too much health!", Logger.LogType.Error);
            }
        }
    }
}
