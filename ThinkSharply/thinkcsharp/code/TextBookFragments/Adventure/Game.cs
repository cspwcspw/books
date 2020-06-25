using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure
{
    class Game
    {
        private FSM theFSM;
        private Player thePlayer;

        public Game()
        {
            theFSM = makeTinyCaveSystem();
            thePlayer = new Player(new string[] { "torch", "matches", "knife", "book" });
            describeSituation();
        }

        private FSM makeTinyCaveSystem()
        {
            int[][] transitionTable =
            {   new int[] {0,   1,   0,   2},          // 0 state (start state)
                new int[] {2,  -1,   0,  -1},          // 1         
                new int[] {2,   1,   3,   0},          // 2
                new int[] {-1, -1,  -1,  -1}           // 3 final state, no way out.
            };

            State[] States =
            {
               new State("You're in a room with a big zero painted on the floor.", new string[] { "keys", "ipad" }),
               new State("You're in a bright yellow room.", new string[] {}),
               new State("This is a round room.", new string[] {}),
               new State("As you enter the room a rockfall closes the entrance!", new string[] {}) 
            };

            FSM result = new FSM(States, transitionTable, new string[] { "north", "east", "south", "west" });

            return result;
        }

        private void describeSituation()
        {
            Console.WriteLine("{0}\n{1}\n{2}",
                             theFSM.CurrentState.GetDescription(),
                             theFSM.GetExitsDescription(),
                             thePlayer.GetThings());
        }

        public void RunGameLoop()
        {

            List<string> verbsRequiringObjects = new List<string>() { "go", "read", "drop", "take", "throw" };

            while (true)
            {
                Console.Write("\nWhat do you want to do? ");
                string response = Console.ReadLine().ToLower();
                Console.WriteLine();

                // Split the user's input into a verb and another word
                string[] words = response.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 2)
                {
                    Console.WriteLine("The most I can cope with is two words at a time!");
                    continue;
                }

                string theVerb = words[0];
                string theObject = "";
                bool verbNeedsAnObject = verbsRequiringObjects.Contains(theVerb);
                if (verbNeedsAnObject && words.Length == 1) 
                {
                    Console.WriteLine("{0} what or where?", theVerb);
                    continue;
                }
                if (!verbNeedsAnObject && words.Length == 2)
                {
                    Console.WriteLine("I don't know how to do that.");
                    continue;
                }
                if (verbNeedsAnObject) theObject = words[1];

                try
                {
                    switch (theVerb)
                    {
                        case "die":
                        case "exit":
                        case "quit": return;     // leave the game.

                        case "go":
                            theFSM.DoTransition(theObject);
                            describeSituation();
                            break;

                        case "help":
                            Console.WriteLine("Valid verbs are go, drop, take, look, help, throw, read, quit, ");
                            Console.WriteLine("and perhaps some others.   Some verbs must be followed by another");
                            Console.WriteLine("object word to make sense, e.g.  'go west', or 'read book'.");
                            break;

                        case "drop":
                            thePlayer.RemoveThing(theObject);
                            theFSM.CurrentState.AddThing(theObject);
                            Console.WriteLine("OK.");
                            break;

                        case "take":
                            theFSM.CurrentState.RemoveThing(theObject);
                            thePlayer.AddThing(theObject);                         
                            Console.WriteLine("OK.");
                            break;

                        case "look":
                            describeSituation();
                            break;

                        case "read":

                            if (theObject == "book" || theObject == "ipad")
                            {
                                if (thePlayer.HasThing(theObject) || theFSM.CurrentState.HasThing(theObject))
                                {
                                    if (theObject == "book")
                                    {
                                        Console.WriteLine("The title is 'Think Sharply with C#'. It looks boring!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The iPad is locked, and you don't know the PIN code.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("There is no {0} here to read.", theObject);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nobody can read a {0}.", theObject);
                            }
                            break;

                        default:
                            Console.WriteLine("Huh?");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}