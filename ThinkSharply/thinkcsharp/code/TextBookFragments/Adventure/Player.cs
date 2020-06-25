using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure
{
    class Player
    {
        private List<string> inventory;

        public Player(string[] initialThings)
        {
            inventory = new List<string>(initialThings);
        }

        public void RemoveThing(string thing)
        {
            if (!inventory.Contains(thing))
            {
                throw new Exception("You don't have one to drop.");
            }
            inventory.Remove(thing);
        }

        public void AddThing(string thing)
        {
            inventory.Add(thing);
        }

        public string GetThings()
        {
            return String.Format("You are carrying {0}.\n", string.Join(", ", inventory));    
        }

        public bool HasThing(string t)
        {
            return inventory.Contains(t);
        }
    }
}
