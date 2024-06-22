namespace Zuul.src
{
    // This file contains all of the items in the game, these items all inherit from the same parent, the Item class.
    // Note: Every item has it's own functionality, this is defined in it's Use function
    class Item
    {
        protected string Description;
        public int Weight { get; }
        public string Name { get; set; }

        public Item(int weight, string description)
        {
            // Set the name to the class name
            Name = GetType().Name;
            Weight = weight;
            Description = description;
        }

        //Overload for child classes
        public Item(int weight, string description, string name) 
        {
            Weight = weight;
            Description = description;
            Name = name;
        }

        public virtual void Use(Player player)
        {
            Console.WriteLine($"Using {Name}");
        }

    }

    class Apple : Item
    {
        public Apple(int weight, string description) : base(weight, description)
        {
        }
        
        public override void Use(Player player)
        {
            player.Heal(25);
        }
    }

    class MedKit : Apple
    {
        public MedKit(int weight, string description) : base(weight, description)
        {
        }

        public override void Use(Player player)
        {
            player.Heal(50);
        }
    }

    class OfficeKey : Item
    {
        public OfficeKey(int weight, string description) : base(weight, description)
        {
        }

        public override void Use(Player player)
        {
            //This is very messy, oh well
            //Check if there is an safe in the current room
            if(player.CurrentRoom.roomInv.GetItem("OfficeSafe") != null)
            {
                Console.WriteLine("You opened the safe!");

                Apple apple = new Apple(1, "Apple");
                player.AddItemToInv(apple);
            }
        }
    }

}
