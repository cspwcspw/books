using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoPlayerGames
{
    public class XoXGame
    {
        int nextPlayer;   // Either a 0 or a 1. 
        int turnsTaken;

        int[] board;

        Random rng;

        public XoXGame(int whoPlaysFirst)
        {
            nextPlayer = whoPlaysFirst;
            turnsTaken = 0;
            board = new int[9];
            for (int i = 0; i < 9; i++)
            {
                board[i] = -1;  // empty
            }
            rng = new Random();
        }


        public bool IsCellEmpty(int posn)
        {
            return board[posn] < 0;
        }

        public void MakeMove(int player, int posn)
        {
            board[posn] = player;
            turnsTaken++;
         }

        public int ChooseMoveFor(int player)
        {
            int candidate = rng.Next(9);
            while (!IsCellEmpty(candidate))
            {
                 candidate = rng.Next(9);
            }
            return candidate;
         }

        public int WhoseTurn()
        {
            return nextPlayer;
        }

        public int SwitchPlayer()
        {
            nextPlayer = (nextPlayer + 1) % 2;
            return nextPlayer;
        }

        /// <summary>
        /// Tells the state of this game.  
        /// </summary>
        /// <returns></returns>
        public bool isBoardFull()
        {
            return (turnsTaken >= 9);  
        }

        public int hasWinLine(int player)
        {
            return -1;
        }

        public int[] GetBoard()
        {
            return board;
        }
    }
}
