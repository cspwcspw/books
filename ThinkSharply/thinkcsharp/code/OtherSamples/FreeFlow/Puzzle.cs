
#define LIFO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;



namespace FreeFlow
{
    public class Puzzle
    {

        int rows, cols;

        public int StatesExplored { get; private set; }
 
        public PQueue PendingStates { get; private set; }

        public Puzzle(int[,] initialBoard)
        {
            State startingState = createStartingState(initialBoard);
            PendingStates = new PQueue();
            PendingStates.Push(startingState);
            StatesExplored = 0;
        }

        private static int val(char c)
        {
            switch (c)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'a': return 10;
                case 'b': return 11;
                case 'c': return 12;
                case 'd': return 13;
                case 'e': return 14;
                case 'f': return 15;
            }
            throw new ApplicationException(String.Format("Invalid character in string-based board description: '{0}'",c));
        }

        private static string clean(string p)
        {   // Remove spaces, semicolons, commas from string.  These exist to help humans parse the string
            StringBuilder sb = new StringBuilder();
            foreach (char c in p.ToLower())
            {
                if ((c == ' ') || (c == ';') || (c == ',')) continue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static Puzzle fromString(string p)
        {
            p = clean(p);
            int rows = val(p[0]);
            int cols = val(p[1]);
            int[,] initialBoard = new int[rows, cols];
            int nextColor = 1;
            for (int i = 2; i < p.Length; i += 4)
            {
                int u = val(p[i]);
                int v = val(p[i+1]);
                int w = val(p[i+2]);
                int x = val(p[i+3]);
                initialBoard[u, v] = nextColor;
                initialBoard[w, x] = nextColor;
                nextColor++;
            }
            return new Puzzle(initialBoard);
        }

        /// <summary>
        /// Advance the search by one step.
        /// </summary>
        /// <returns>Pair: next state to be advanced, null, or list of solution states.  
        /// (null, null) means we're done.  </returns>
        public State advanceSearch()
        {
            if (PendingStates.Count == 0) return State.Empty;
            State curr = PendingStates.Pop();
            StatesExplored++;
            List<State> children = findChildrenOfSomePipe(curr);
            foreach (State child in children)
            {
                child.DoNoBrainers();
                if (!child.isPruneable())  // in case we ever write this.
                {
                    PendingStates.Push(child);
                }
            }
            if (PendingStates.Count == 0) return State.Empty;
            return PendingStates.Peek();

        }


        int roundRobinPipeIndex = 1;
        private List<State> findChildrenOfSomePipe(State curr)
        {
            // It doesn't matter which pipe (or which end of the pipe) 
            // we grow our state space from (aside from efficiency). 
            // So when we're at a choice-point, we pick an arbitrary 
            // pipe and advance it.  We only advance from the head 
            // side of a not-yet-completed pipe.  (If somebody else
            // chooses to occasionally swap the heads and tails,
            // it won't bother us here ...)
            // Currently we round-robin the candidate pipes, 
            // so all pipes get a turn to grow.  I suspect a good
            // heuristic would be to pick an end of a pipe that is
            // is a "crowded" part of the board - this restricts the
            // number of children and might keep the branching factor 
            // of the search tree smaller. 

            List<State> children = new List<State>();
            int n = curr.Pipes.Length;
            int pipesToStillTry = n - 1;  // Pipe[0] is a fake pipe, hence the -1.
            while (pipesToStillTry > 0)
            {
                // It is possible to connect all pipes, but it is not a solution
                // because not all squares on the board are covered. So we must
                // cater for states where no progress is possible on any pipe.
                pipesToStillTry--;

                // Grab the next pipe
                Pipe p = curr.Pipes[roundRobinPipeIndex];
                // Advance and cycle the index, (keep in mind that pipe[0] is fake). 
                roundRobinPipeIndex++;
                if (roundRobinPipeIndex == n) roundRobinPipeIndex = 1;

                if (!p.IsConnected)
                {   // We have found the pipe we choose to work on.
                    int row = p.HeadPos.Y;
                    int col = p.HeadPos.X;
                    tryAdvancePipe(children, curr, p, row, col - 1); // West
                    tryAdvancePipe(children, curr, p, row, col + 1); // East
                    tryAdvancePipe(children, curr, p, row - 1, col); // North
                    tryAdvancePipe(children, curr, p, row + 1, col); // South              
                    break;
                }
            }
            return children;
        }

        /// <summary>
        ///  Try to advance the given pipe into the target cell.
        ///  If we succeed, clone a new board and add it to children.
        /// </summary>
        private void tryAdvancePipe(List<State> children, State curr, Pipe p, int targetRow, int targetCol)
        {
            // Don't fall off the edge of the board.
            if (targetRow < 0 || targetRow >= rows || targetCol < 0 || targetCol >= cols) return;

            XY hpos = p.HeadPos;
            int color = curr.TheBoard[hpos.Y, hpos.X];

            XY tpos = p.TailPos;

            // Does this move make the two ends of the same pipe meet in the middle?
            if (tpos.Y == targetRow && tpos.X == targetCol)
            {
                State newC = curr.Clone();
                newC.MovesToGo--;
                newC.Pipes[color].IsConnected = true;
                newC.Pipes[color].AdvanceHead(XY.Empty);
                newC.Pipes[color].AdvanceTail(XY.Empty);
                children.Add(newC);
                return;
            }

            // If the target cell is already occupied, we cannot advance.
            if (curr.TheBoard[targetRow, targetCol] != 0)
            {
                return;
            }

            // Okay, our attempt to claim the new cell for this pipe has worked.
            State newChild = curr.Clone();
            newChild.TheBoard[targetRow, targetCol] = color;
            XY newPos = new XY(targetCol, targetRow);
            newChild.Pipes[color].AdvanceHead(newPos);
            newChild.MovesToGo--;
            children.Add(newChild);
        }

        private State createStartingState(int[,] board)
        {
            cols = board.GetLength(1);
            rows = board.GetLength(0);

            int largestColor = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int v = board[r, c];
                    if (v > largestColor)
                    {
                        largestColor = v;
                    }
                }
            }

            // TODO:  are the colors contiguous?

            // Allocate arrays for the goal indexes and histories.  Index 0 is not used.
            Pipe[] pipes = new Pipe[largestColor + 1];
            int[] colorSeenCounter = new int[largestColor + 1];
            for (int i = 1; i < pipes.Length; i++)
            {
                pipes[i] = new Pipe() { HeadPath = new List<XY>(), TailPath = new List<XY>(), IsConnected = false };
            }

            // Now sweep the board and set up the starting position (head) and the goal
            // for each color.
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int v = board[row, col];

                    if (v > 0)
                    {
                        int indx = row * cols + col;
                        if (colorSeenCounter[v] == 0)
                        {
                            // First time we see a connector of a given colour it becomes a head.
                            pipes[v].TailPos = new XY(col, row);
                            colorSeenCounter[v]++;
                        }
                        else
                        { // Next sighting becomes the tail.  
                            pipes[v].HeadPos = new XY(col, row);
                            colorSeenCounter[v]++;
                        }
                    }
                }
            }

            // A touch of sanity validation ....  Every color must have been seen an even number
            // of times...
            for (int i = 0; i < colorSeenCounter.Length; i++)
            {
                if (colorSeenCounter[i] % 2 != 0)
                {
                    throw new ApplicationException(
                          String.Format("The board has {0} connectors of colour {1}.",
                                        colorSeenCounter[i], i));
                }
            }

            // We're ready to set up our first state...
            State s = new State()
            {
                TheBoard = board,
                MovesToGo = rows * cols - (pipes.Length - 1),
                Pipes = pipes
            };
            return s;
        }

        public class State
        {
            public int[,] TheBoard { get; set; }

            public int Priority = 0;

            /// <summary>
            ///  Holds a counter of the number of connections that still need to be made.
            ///  Notice that even if the board is full, we may still need to close adjacent pipes.
            /// </summary>
            public int MovesToGo { get; set; }

            public Pipe[] Pipes { get; set; }

            /// <summary>
            ///  Deep copy everything about the current state.
            /// </summary>
            /// <returns></returns>
            public State Clone()
            {
                int[,] b = (int[,])this.TheBoard.Clone();

                Pipe[] ps = new Pipe[Pipes.Length];
                for (int i = 1; i < Pipes.Length; i++)
                {
                    ps[i] = Pipes[i].Clone();
                }

                State newState = new State() { Pipes = ps, TheBoard = b, MovesToGo = this.MovesToGo };
                return newState;
            }

            public static State Empty = new State();

            public bool IsSolved()
            {
                if (this.Equals(Empty)) return false;
               // if (MovesToGo > 0) return false;
                for (int i = 1; i < Pipes.Length; i++)
                {
                    if (!Pipes[i].IsConnected) return false;
                }
                return true;
            }


            const int reachable = -1;
            public bool isPruneable()
            {
                // Any state that has isolated or unreachable empty cells can 
                // be pruned.  So let's find out with some garbage-collection
                // type tricks for establishing reachability...  
  
                int[,] bd = (int[,])TheBoard.Clone();
                for (int i = 1; i < Pipes.Length; i++)
                {
                    Pipe p = Pipes[i];
                    if (!p.IsConnected)
                    {
                        markReachablesFrom(bd, p.HeadPos.Y, p.HeadPos.X);
                        markReachablesFrom(bd, p.TailPos.Y, p.TailPos.X);
                    }
                }
                // sweep the board.  If anything is unreachable (can never be colored)
                // we can prune it.
                int rows = bd.GetLength(0);
                int cols = bd.GetLength(1);
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (bd[r, c] == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            private void markReachablesFrom(int[,] bd, int row, int col)
            {
                int rows = bd.GetLength(0);
                int cols = bd.GetLength(1);
                if (col >= 1 && (bd[row, col - 1] == 0))  // Can go West
                {
                    bd[row, col - 1] = reachable;
                    markReachablesFrom(bd, row, col-1);
                }
                if (col < cols - 1 && (bd[row, col + 1] == 0))  // Can go East
                {
                    bd[row, col + 1] = reachable;
                    markReachablesFrom(bd, row, col + 1);
                }

                if (row >= 1 && (bd[row - 1, col] == 0))  // Can go North
                {
                    bd[row - 1, col] = reachable;
                    markReachablesFrom(bd, row-1, col);
                }

                if (row < rows - 1 && (bd[row + 1, col] == 0))  // Can go South
                {
                    bd[row + 1, col] = reachable;
                    markReachablesFrom(bd, row + 1, col);
                }

            }

            public override string ToString()
            {
                if (this.Equals(State.Empty)) return "Empty State";
                StringBuilder sb = new StringBuilder();
                int rows = TheBoard.GetLength(0);
                int cols = TheBoard.GetLength(1);

                for (int row = 0; row < rows; row++)
                {

                    for (int col = 0; col < cols; col++)
                    {
                        sb.AppendFormat("{0} ", TheBoard[row, col]);
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
                sb.AppendFormat("MovesToGo={0}", MovesToGo);

                sb.AppendLine();
                return sb.ToString();
            }

            internal void DoNoBrainers()
            {
                int indx = 1;
                while (indx > 0)
                {
                    indx++;
                    if (indx >= Pipes.Length)
                    {
                        indx = 1;
                    }
                    indx = DoOneNoBrainer(indx);
                }
            }

            /// <summary>
            /// Do one no-brainer move, return the index of the next pipe to be tried if we succeeded,
            /// or 0 if we fail.
            /// </summary>
            /// <returns></returns>
            internal int DoOneNoBrainer(int startAtPipeIndex = 1)
            {
                int pipesToTry = Pipes.Length - 1;
                Priority = 0;
                
                while (pipesToTry > 0)
                {
                    pipesToTry--;
                    int n = tryPipe(Pipes[startAtPipeIndex], true);
                    if (n == 1)
                    {
                        return startAtPipeIndex;
                    }
                    Priority += n;

                    n = tryPipe(Pipes[startAtPipeIndex], false);
                    if (n == 1)
                    {
                        return startAtPipeIndex;
                    }
                    Priority += n;

                    startAtPipeIndex++;
                    if (startAtPipeIndex >= Pipes.Length)
                    {
                        startAtPipeIndex = 1;
                    }
                }
                return 0;
            }

            // Return the number of moves that were avaiable on this pipe segment.
            // If the return value is 1, the move has been made.
            private int tryPipe(Pipe p, bool tryHead)
            {
                if (p.IsConnected) return 0;

                List<XY> a = p.HeadPath;
                List<XY> b = p.TailPath;

                XY aPos, bPos;
                if (tryHead) // Swap roles of a and b
                {
                    aPos = p.HeadPos;
                    bPos = p.TailPos;
                }
                else
                {
                    aPos = p.TailPos;
                    bPos = p.HeadPos;
                }

                // See if we can advance deterministically from a, or meet b
                List<XY> possibles = findPossibleMoves(aPos, bPos);
                if (possibles.Count != 1) return possibles.Count;

                // There is one and only one move, so make it *on the current board*
                // without cloning a new state. 
                XY newPos = possibles[0];
                if (TheBoard[newPos.Y, newPos.X] == 0)
                {
                    // Propagate this pipe's color into the new position
                    TheBoard[newPos.Y, newPos.X] = TheBoard[aPos.Y, aPos.X];
                    // And extend the pipe.
                    if (tryHead)
                    {
                        p.AdvanceHead(newPos);
                    }
                    else
                    {
                        p.AdvanceTail(newPos);
                    }
                }
                else
                {
                    p.IsConnected = true;
                }
                MovesToGo--;
                return 1;
            }

            List<XY> findPossibleMoves(XY aPos, XY bPos)
            {
                int row = aPos.Y;
                int col = aPos.X;

                int cols = TheBoard.GetLength(1);
                int rows = TheBoard.GetLength(0);
                List<XY> possibilities = new List<XY>();

                if (col >= 1 && (TheBoard[row, col - 1] == 0 || (bPos.X == col - 1 && bPos.Y == row)))  // Can go West
                {
                    possibilities.Add(new XY(col - 1, row));
                }
                if (col < cols - 1 && (TheBoard[row, col + 1] == 0 || (bPos.X == col + 1 && bPos.Y == row)))  // Can go East
                {
                    possibilities.Add(new XY(col + 1, row));
                }

                if (row >= 1 && (TheBoard[row - 1, col] == 0 || (bPos.X == col && bPos.Y == row - 1)))  // Can go North
                {
                    possibilities.Add(new XY(col, row - 1));
                }

                if (row < rows - 1 && (TheBoard[row + 1, col] == 0 || (bPos.X == col && bPos.Y == row + 1)))  // Can go South
                {
                    possibilities.Add(new XY(col, row + 1));
                }

                return possibilities;
            }
        }


        public struct XY
        {
            public int X, Y;

            public XY(int x, int y)
            {
                X = x;
                Y = y;
            }

            static public XY Empty = new XY(-1, -1);
        }

        public class Pipe
        {
            public XY HeadPos;
            public XY TailPos;
            public List<XY> HeadPath = new List<XY>();
            public List<XY> TailPath = new List<XY>();
            public bool IsConnected = false;

            internal Pipe Clone()
            {
                Pipe newPipe = new Pipe()
                                 {
                                     HeadPos = this.HeadPos,
                                     TailPos = this.TailPos,
                                     HeadPath = new List<XY>(this.HeadPath),
                                     TailPath = new List<XY>(this.TailPath),
                                     IsConnected = this.IsConnected
                                 };
                return newPipe;
            }

            internal void AdvanceHead(XY newPos)
            {
                HeadPath.Add(HeadPos);
                HeadPos = newPos;
            }

            internal void AdvanceTail(XY newPos)
            {
                TailPath.Add(TailPos);
                TailPos = newPos;
            }
        }


        public class PQueue 
        {
            // a superficial attempt at a low-cost priority queue.  


            Stack<State> [] theQs;

            public int Count { get; private set; }

            public PQueue()
            {
                theQs = new Stack<State>[10];
                for (int i = 0; i < theQs.Length; i++)
                {
                    theQs[i] = new Stack<State>();
                }
                Count  = 0;
            }

            public State Peek()
            {
                foreach (var s in theQs)
                {
                    if (s.Count > 0)
                    {
                        return s.Peek();
                    }
                }
                return null;
            }

            public void Push(State s)
            {
                Count++;
                int p = s.Priority / 6;
                if (p >= theQs.Length) p = theQs.Length - 1;
                theQs[p].Push(s);
                
            }

            public State Pop()
            {
                Count--;
                foreach (var s in theQs)
                {
                    if (s.Count > 0)
                    {
                        return s.Pop();
                    }
                }
                return null;
            }

        }


        #region Built in puzzles

        static public int[][,] builtInPuzzles = 
        {   
           new int[,]   {{1,2,3,0,4,5}, 
                         {0,0,0,0,6,0},
                         {0,0,3,0,0,0},
                         {0,0,4,0,0,0},
                         {1,0,6,0,0,0},
                         {2,0,5,0,0,0}}

         , new int[,]   {{0,0,0,0,0,0,0,1},    // Level 23
                         {0,2,0,0,3,0,0,0},
                         {0,0,4,0,0,0,0,0},
                         {0,0,0,5,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,6,3,0,0,0,0},
                         {0,2,0,0,0,0,0,0},
                         {0,5,6,4,1,0,0,0}}
    
         , new int[,]   { // level 30
                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1}, 
                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                           {0, 0, 2, 3, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 7, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 4, 5, 0, 0, 8, 0, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 9, 0, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
                           {0, 0, 8,10, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 0, 0,11, 0,11,12, 0, 0}, 
                           {0, 3, 0, 0, 0, 0, 0, 0, 0,13, 0, 0, 0, 0}, 
                           {0,14, 0, 0, 0,14, 0,10, 0, 0,13, 0, 0, 0}, 
                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 9,12, 0, 0, 0}, 
                           {0, 0,15, 0, 0, 0, 0,15, 0, 0, 0, 0, 0, 0}, 
                           {2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} 
                       }
         };

        static public string Lev20 = "88  0074 0115 0206 0716 2166 2542 4564";
         
    }


        #endregion
}
 