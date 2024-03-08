using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zuul.src
{

    //Note: Every item has it's own functionality, this is defined in it's Use function

    class Item
    {
        protected string Description;
        public int Weight { get; }
        public string Name { get; set; }

        public Item(int weight, string description)
        {
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

    //Make derrived classes possible
    class Apple : Item
    {
        public Apple(int weight, string description) : base(weight, description)
        {
        }
        
        public override void Use(Player player)
        {
            player.Damage(25);
        }
        
    }

}
