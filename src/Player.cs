using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul.src
{
    class Player
    {
        private int health;
        private Logger logger;
        public Room CurrentRoom { get; set; }

        public Player() 
        {
            health = 100;
            CurrentRoom = null;
            logger = new Logger();
        }

        public bool IsAlive() 
        {
            return health != 0;
        }

        public void Damage(int damage)
        {
            //Make sure our health is not below zero
            if ((health - damage) < 0)
                return;

            logger.LogToConsole($"Damage: {damage}", Logger.LogType.Info);
            health -= damage;
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
