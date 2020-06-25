using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    /// <summary>
    /// Encapsulates a single location (cave, or island) in a game.
    /// </summary>
    class State
    {
        private string description;
        private List<string> inventory;

        public State(string theDescription, string[] initialThings)
        {
            description = theDescription;
            inventory = new List<string>(initialThings);
        }
        
        public string GetDescription()
        {
            if (inventory.Count > 0)
            {
                return String.Format("{0}\nThere are some things here: {1}.",
                                         description, string.Join(", ", inventory)); 
            }
            else
            {
                return string.Format("{0}", description);
            }
        }

        public void AddThing(string thing)
        {
            if (inventory.Contains(thing))
            {
                throw new Exception("There is already one here.");
            }
            inventory.Add(thing);
        }

        public void RemoveThing(string thing)
        {
            if (!inventory.Contains(thing))
            {
                throw new Exception("There isn't one here.");
            }
            inventory.Remove(thing);
        }

        public bool HasThing(string t)
        {
            return inventory.Contains(t);
        }
    }
}
