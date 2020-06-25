using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fragments
{
    class TrafficLightFSM
    {
        public int CurrentState { get; private set; }

        public TrafficLightFSM()
        {
            CurrentState = 0;
        }

        public void Advance()
        {
            switch (CurrentState)
            {
                case 0:
                    CurrentState = 1;
                    break;
                case 1:
                    CurrentState = 2;
                    break;
                case 2:
                    CurrentState = 0;
                    break;
            }
        }
    }
}
