using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class FSM
    {
        private int currStateIndx = 0;
        private int[][] transitionTable;
        private State[] theStates;
        private List<string> transitionVerbs;
        

        public State CurrentState { get; private set; }

        public FSM(State[] states, int[][] stateTransitionTable, string[] eventVerbs)
        {
            theStates = states;
            transitionTable = stateTransitionTable;
            CurrentState = states[0]; // the initial state
            transitionVerbs = new List<string>(eventVerbs);
        }

        public void DoTransition(string fsmEventVerb)
        {
            int indx = transitionVerbs.IndexOf(fsmEventVerb);

            if (indx < 0)
            {
                throw new Exception("Go where?");
            }

            int newState = transitionTable[currStateIndx][indx];
            if (newState >= 0)
            {
                currStateIndx = newState;
                CurrentState = theStates[currStateIndx];
            }
            else
            {
                throw new Exception("THAT'S NOT POSSIBLE!");
            }
        }


        public string GetExitsDescription()
        {
            string exitDescription = "You can go ";
            string separator = "";
            for (int col = 0; col < transitionVerbs.Count; col++)
            {
                if (transitionTable[currStateIndx][col] >= 0)
                {
                    exitDescription += separator + transitionVerbs[col];
                    separator = ", ";
                }
            }

            if (separator == "") return "There is no way out!";
            return  exitDescription + ".";
        }
    }

}
