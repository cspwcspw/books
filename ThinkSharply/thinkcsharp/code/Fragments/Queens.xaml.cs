using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThinkLib;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for Queens.xaml
    /// </summary>
    public partial class Queens : Window
    {
        public Queens()
        {
            InitializeComponent();
        }

        Random rng = new Random();

        private void shuffle(int[] xs)
        {
            int[] keys = new int[xs.Length];
            // Create an array of N random numbers to use as the sorting key.
            for (int i = 0; i < xs.Length; i++) keys[i] = rng.Next();
            Array.Sort(keys, xs);
        }

        private string stringify(int[] xs)
        {
            string result = "{";
            string separator = "";
            foreach (int x in xs)
            {
                result += separator + x.ToString();
                separator = ", ";
            }
            return result + "}";
        }

        private void btnShuffleTest_Click(object sender, RoutedEventArgs e)
        {
            int[] xs = { 12, 15, 13, 18, 16, 24, 96, 42, 33, 19 };
            shuffle(xs);
            txtResults.AppendText(string.Format("{0}\n",stringify(xs)));
            txtResults.ScrollToEnd();
        }

        private void btnLogicTests_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(shareDiagonal(5, 2, 2, 0), false);
            Tester.TestEq(shareDiagonal(5, 2, 3, 0), true);
            Tester.TestEq(shareDiagonal(5, 2, 4, 3), true);
            Tester.TestEq(shareDiagonal(5, 2, 4, 1), true);

            Tester.TestEq(colHasDiagonalClashes(new int[] { 6, 4, 2, 0, 5 }, 4), false);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 6, 4, 2, 0, 5, 7, 1, 3 }, 7), false);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 0, 1 }, 1), true);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 5, 6 }, 1), true);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 6, 5 }, 1), true);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 0, 6, 4, 3 }, 3), true);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 5, 0, 7 }, 2), true);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 2, 0, 1, 3 }, 1), false);
            Tester.TestEq(colHasDiagonalClashes(new int[] { 2, 0, 1, 3 }, 2), true);

            Tester.TestEq(boardHasDiagonalClashes(new int[] { 6, 4, 2, 0, 5, 7, 1, 3 }), false); // Solution from above
            Tester.TestEq(boardHasDiagonalClashes(new int[] { 4, 6, 2, 0, 5, 7, 1, 3 }), true);  // Swap rows of first two
            Tester.TestEq(boardHasDiagonalClashes(new int[] { 0, 1, 2, 3 }), true);          // Try small 4x4 board
            Tester.TestEq(boardHasDiagonalClashes(new int[] { 2, 0, 3, 1 }), false);         // Solution to 4x4 case
        }


        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int sz = Convert.ToInt32(txtSz.Text);
            int numWanted = Convert.ToInt32(txtNumSolutions.Text);

            tot = 0;
            int found = 0;
            while (found < numWanted)
            {
                int[] solution = findQueensSolution(sz);
                found++;
            }
            txtResults.AppendText(string.Format("Average tries: {0}\n", tot/numWanted));
            txtResults.ScrollToEnd();
        }


        private void btnShowUnsolvedBoard_Click(object sender, RoutedEventArgs e)
        {
            int sz = Convert.ToInt32(txtSz.Text);
            int[] bd = new int[sz];
            for (int i = 0; i < sz; i++) bd[i] = i;
            QueensGUI theWindow = new QueensGUI(bd);
            theWindow.Owner = this;
            theWindow.Show();
            theWindow.RefreshQueenPositions();
        }


        int tot = 0;
        /// <summary>
        /// Find and return one random solution to the NxN queens problem.
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        private int[] findQueensSolution(int N)
        {
            // set up the initial board of the correct size
            int[] bd = new int[N];
            for (int i = 0; i < N; i++) bd[i] = i;

            bool wantGUI = cbShowBoard.IsChecked.Value == true;
            bool wantAnimation = wantGUI && cbAnimated.IsChecked.Value == true;

            QueensGUI theWindow = null;
            if (wantGUI)
            {
                theWindow = new QueensGUI(bd);
                theWindow.Owner = this;
                theWindow.Show();
            }
            
            int tries = 1;
            while (boardHasDiagonalClashes(bd))
            {
                shuffle(bd);
                if (wantAnimation)
                {
                    theWindow.RefreshQueenPositions();
                }
                tries++;
            }

            if (wantGUI)
            {
                theWindow.RefreshQueenPositions();
            }
            tot += tries;

            txtResults.AppendText(string.Format("Solution {0} found in {1} tries.\n", stringify(bd), tries));
            txtResults.ScrollToEnd();

            return bd;
        }

        /// <summary>
        /// Is (x0, y0) on a shared diagonal with (x1, y1)?
        /// </summary>
        private bool shareDiagonal(int x0, int y0, int x1, int y1)
        {
            int dy = Math.Abs(y1 - y0);       // Calc the absolute y distance 
            int dx = Math.Abs(x1 - x0);       // Calc the absolute x distance
            return dx == dy;                  // They clash if dx == dy
        }

        /// <summary>
        /// Return true if the queen at column col clashes
        /// on the diagonal with any queen to its left.
        /// </summary>
        private bool colHasDiagonalClashes(int[] bd, int col)
        {
            for (int i = 0; i < col; i++)   // Look at all columns to the left of col
            {
                if (shareDiagonal(i, bd[i], col, bd[col]))
                    return true;
            }
            return false;           // No clashes 
        }

        /// <summary>
        /// Determine whether we have any queens clashing on the diagonals.
        /// </summary>
        private bool boardHasDiagonalClashes(int[] bd)
        {
            for (int col = 1; col < bd.Length; col++)
            {
                if (colHasDiagonalClashes(bd, col)) return true;
            }
            return false;
        }


        //private bool boardHasDiagonalClashes(int[] bd)
        //{  bad code from a student
        //    int c = 0;
        //    for (int i = 1; i < bd.Length; i++)
        //    {
        //        int countd = i + 1;
        //        int countu = i + 1;
        //        for (int j = bd[i] + 1; j < bd.Length; j++)
        //        {
        //            if (bd[countd++] == j) return true;
        //        }
        //        for (int j = bd[i] - 1; j >= 0; j--)
        //        {
        //            if (bd[countu++] == j) return true;
        //        }
        //    }
        //    return false;
        //}

        
    }
}
