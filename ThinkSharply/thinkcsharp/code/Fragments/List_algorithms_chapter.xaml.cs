using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.Diagnostics;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for List_algorithms_chapter.xaml
    /// </summary>
    public partial class List_algorithms_chapter : Window
    {

        string vocabPath = "c:\\temp\\vocab.txt";
        string bookPath = "c:\\temp\\alice_in_wonderland.txt";


        public List_algorithms_chapter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Find and return the index of target in sequence xs
        /// </summary>
        private int searchLinear(string[] xs, string target)
        {
            for (int i = 0; i < xs.Length; i++)
            {
                if (xs[i] == target)
                    return i;
            }
            return -1;
        }

        private void btnLinearSearch_Click(object sender, RoutedEventArgs e)
        {
            string[] friends = { "Joe", "Zoe", "Brad", "Angelina", "Zuki", "Thandi", "Paris" };
            Tester.TestEq(searchLinear(friends, "Zoe"), 1);
            Tester.TestEq(searchLinear(friends, "Joe"), 0);
            Tester.TestEq(searchLinear(friends, "Paris"), 6);
            Tester.TestEq(searchLinear(friends, "Bill"), -1);
        }

        /// <summary>
        /// Return a list of words in wds that do not occur in vocab 
        /// </summary>
        private string[] findUnknownWords(string[] known, string[] wds)
        {
            List<string> result = new List<string>();
            foreach (string w in wds)
            {
                if (searchBinary(known, w) < 0)
                {
                    result.Add(w);
                }
            }
            return result.ToArray();
        }

        private void btnFindUnknownWords_Click(object sender, RoutedEventArgs e)
        {
            string[] vocab = { "apple", "boy", "dog", "down", "fell", "girl", "grass", "the", "tree" };
            string[] book_words = "the apple fell from the tree to the grass".Split();
            string[] empty = { };
            Tester.TestEq(findUnknownWords(vocab, book_words), new string[] { "from", "to" });
            Tester.TestEq(findUnknownWords(empty, book_words), book_words);
            Tester.TestEq(findUnknownWords(vocab, new string[] { "the", "boy", "fell" }), empty);
        }


        private void btnLoadWordsFromFile_Click(object sender, RoutedEventArgs e)
        {
            string vocabPath = "c:\\temp\\vocab.txt";
            string[] vocab = File.ReadAllLines(vocabPath);
            MessageBox.Show(String.Format("There are {0} lines in {1}.", vocab.Length, vocabPath), "From the vocabulary");
        }


        private string[] convertToCleanedWords(string theText)
        {
            string t = theText.ToLower();
            string unwanted = " 0123456789!\"#$%&()*+,-./:;<=>?@[]^_`{|}~'\\;\r\n";
            char[] delims = unwanted.ToCharArray();
            string[] result = t.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        private string[] getWordsInBook(string bookPath)
        {
            return convertToCleanedWords(File.ReadAllText(bookPath));
        }


        private void btnCleanAndSplitString_Click(object sender, RoutedEventArgs e)
        {

            Tester.TestEq(convertToCleanedWords("My name ?? is Earl!"), new string[] { "my", "name", "is", "earl" });
            Tester.TestEq(convertToCleanedWords("\"Well, I never!\", said Alice."),
                                      new string[] { "well", "i", "never", "said", "alice" });

            string[] vocab = File.ReadAllLines(vocabPath);
            string[] bookWords = getWordsInBook(bookPath);
            //  MessageBox.Show(String.Format("There are {0} words in the book {1}.", bookWords.Length, bookPath), "From the book");
            DateTime t0 = DateTime.Now;
            string[] missingWords = findUnknownWords(vocab, bookWords);
            DateTime t1 = DateTime.Now;
            int elapsedMilliSecs = (int)(t1 - t0).TotalMilliseconds;
            MessageBox.Show(String.Format("There are {0} unknown words in the book.\n{1} ms.",
                   missingWords.Length, elapsedMilliSecs));
        }

        /// <summary>
        /// Find and return the index of target in array xs.  Return -1 if target does not exist.
        /// </summary>
        /// <param name="xs"></param>
        /// <param name="target"></param>
        public int searchBinary(string[] xs, string target)
        {
            int lb = 0;
            int ub = xs.Length;
            while (lb < ub) // exit if region of interest (ROI) becomes empty
            {
                // Next probe should be in the middle of the ROI
                int mid_index = (lb + ub) / 2;

                // Fetch the item at that position
                string item_at_mid = xs[mid_index];
                Debug.WriteLine("Target=\"{0}\"  ROI=[{1} - {2}) (size={3}), probed xs[{4}]=\"{5}\"",
                                        target, lb, ub, ub - lb, mid_index, item_at_mid);

                // How does the probed item compare to the target?
                int c = item_at_mid.CompareTo(target);
                if (c == 0)
                {
                    return mid_index;      // Found it!
                }
                if (c < 0)
                {
                    lb = mid_index + 1;    // Use upper half of ROI next time
                }
                else
                {
                    ub = mid_index;        // Use lower half of ROI next time
                }
            }
            return -1; // we did not find it, and ROI is empty.
        }

        public int searchBinary2(List<string> xs, string target)
        {
            int lb = 0;
            int ub = xs.Count;
            while (lb < ub) // exit if region of interest (ROI) becomes empty
            {
                // Next probe should be in the middle of the ROI
                int mid_index = (lb + ub) / 2;

                // Fetch the item at that position
                string item_at_mid = xs[mid_index];
                //Debug.WriteLine("Target=\"{0}\"  ROI=[{1} - {2}) (size={3}), probed xs[{4}]=\"{5}\"",
                //                        target, lb, ub, ub - lb, mid_index, item_at_mid);

                // How does the probed item compare to the target?
                int c = item_at_mid.CompareTo(target);
                if (c == 0)
                {
                    return mid_index;      // Found it!
                }
                if (c < 0)
                {
                    lb = mid_index + 1;    // Use upper half of ROI next time
                }
                else
                {
                    ub = mid_index;        // Use lower half of ROI next time
                }
            }
            return -1; // we did not find it, and ROI is empty.
        }

        private void btnBinarySearch_Click(object sender, RoutedEventArgs e)
        {

            string[] vocab = File.ReadAllLines(vocabPath);
             int pos = searchBinary(vocab, "magical");
             Debug.WriteLine("Binary search returned {0}", pos);

            //foreach (int N in new int[] {1, 2, 3, 4, 5, 10, 100, 1000, 1000000, 1000000000 })
            //{
            //    int requiredProbes = (int)Math.Ceiling(Math.Log(N + 1, 2));
            //    Debug.WriteLine("{0} items would need max of {1} probes.", N, requiredProbes);
            //}
            //string[] friends = { "Angelina", "Brad", "Joe", "Paris", "Thandi", "Zoe", "Zuki" };
            //Tester.TestEq(searchBinary(friends, "Bill"), -1);
            //Tester.TestEq(searchBinary(friends, "Abbey"), -1);
            //Tester.TestEq(searchBinary(friends, "Zummy"), -1);
            //for (int i = 0; i < friends.Length; i++)
            //{
            //    Tester.TestEq(searchBinary(friends, friends[i]), i);
            //}

            //List<string> friends = new List<string>() { "Angelina", "Brad", "Joe", "Paris", "Thandi", "Zoe", "Zuki" };
            //Tester.TestEq(searchBinary2(friends, "Bill"), -1);
            //Tester.TestEq(searchBinary2(friends, "Abbey"), -1);
            //Tester.TestEq(searchBinary2(friends, "Zummy"), -1);
            //for (int i = 0; i < friends.Count; i++)
            //{
            //    Tester.TestEq(searchBinary2(friends, friends[i]), i);
            //}
        }

        /// <summary>
        ///  Return a new list in which all adjacent
        ///  duplicates from xs have been removed.
        /// </summary>
        private string[] removeAdjacentDups(string[] xs)
        {
            if (xs.Length == 0) return new string[] { };
            List<string> result = new List<string>();
            string elemLastAdded = xs[0];
            result.Add(elemLastAdded);
            foreach (string x in xs)
            {
                if (x != elemLastAdded)
                {
                    result.Add(x);
                    elemLastAdded = x;
                }
            }
            return result.ToArray();
        }

        private void btnRemoveDups_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(removeAdjacentDups(new string[] { "a", "b", "b", "b", "c", "c" }),
                                                 new string[] { "a", "b", "c" });
            Tester.TestEq(removeAdjacentDups(new string[] { }), new string[] { });
            Tester.TestEq(removeAdjacentDups(new string[] { "a", "big", "big", "bite", "dog" }),
                                        new string[] { "a", "big", "bite", "dog" });

            string[] wds = getWordsInBook(bookPath);
            Array.Sort(wds);
            string[] uniqueWds = removeAdjacentDups(wds);
            MessageBox.Show(String.Format("{0} has {1} words, {2} are unique.",
                           bookPath, wds.Length, uniqueWds.Length));
        }


        public int[] merge(int[] xs, int[] ys)
        {
            int[] result = new int[xs.Length + ys.Length];
            int xi = 0, yi = 0, zi = 0;

            while (xi < xs.Length && yi < ys.Length)
            {
                // Both lists still have items, copy smaller item to result.   
                result[zi++] = (xs[xi] <= ys[yi] ? xs[xi++] : ys[yi++]);
            }

            // Copy items from whichever list has some remaining.
            while (yi < ys.Length) // Add remaining items from ys
            {
                result[zi++] = ys[yi++];
            }
            while (xi < xs.Length) // Add remaining items from xs
            {
                result[zi++] = xs[xi++];
            }

            return result;
        }


        public string[] merge(string[] xs, string[] ys)
        {
            string[] result = new string[xs.Length + ys.Length];
            int xi = 0, yi = 0, zi = 0;

            while (xi < xs.Length && yi < ys.Length)
            {
                // Both lists still have items, copy smaller item to result.   
                result[zi++] = (xs[xi].CompareTo(ys[yi]) <= 0 ? xs[xi++] : ys[yi++]);
            }

            // Copy items from whichever list has some remaining.
            while (yi < ys.Length) // Add remaining items from ys
            {
                result[zi++] = ys[yi++];
            }
            while (xi < xs.Length) // Add remaining items from xs
            {
                result[zi++] = xs[xi++];
            }

            return result;
        }


        public T[] merge<T>(T[] xs, T[] ys) where T : IComparable
        {
            T[] result = new T[xs.Length + ys.Length];
            int xi = 0;
            int yi = 0;
            int zi = 0;
            while (xi < xs.Length && yi < ys.Length)
            {
                // Both lists still have items, copy smaller item to result.       
                if (xs[xi].CompareTo(ys[yi]) <= 0)
                {
                    result[zi++] = xs[xi++];
                }
                else
                {
                    result[zi++] = ys[yi++];
                }
            }

            // One of the lists is definitely used up.
            // Copy items from whichever list has any remaining.
            while (yi < ys.Length) // Add remaining items from ys
            {
                result[zi++] = ys[yi++];
            }
            while (xi < xs.Length) // Add remaining items from xs
            {
                result[zi++] = xs[xi++];
            }

            return result;
        }


        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            int[] xs = { 1, 3, 5, 7, 8, 8, 13, 15, 17, 19 };
            int[] ys = { 4, 8, 12, 16, 20, 24 };
            int[] zs = { 1, 3, 4, 5, 7, 8, 8, 8, 12, 13, 15, 16, 17, 19, 20, 24 };
            int[] empty = { };

            Tester.TestEq(merge(xs, empty), xs);
            Tester.TestEq(merge(empty, ys), ys);
            Tester.TestEq(merge(empty, empty), empty);
            Tester.TestEq(merge(xs, ys), zs);
            Tester.TestEq(merge(new string[] { "a", "big", "cat" },
                                new string[] { "big", "bite", "dog" }),
                          new string[] { "a", "big", "big", "bite", "cat", "dog" });
        }


        private string[] findUnknownsMergePattern(string[] vocab, string[] wds)
        {
            List<string> result = new List<string>();

            int xi = 0, yi = 0;

            while (xi < vocab.Length && yi < wds.Length)
            {
                int v = vocab[xi].CompareTo(wds[yi]);
                if (v < 0)
                {
                    xi++;         // move past this vocab word
                }
                else if (v == 0)
                {
                    yi++;         // this word is recognized
                }
                else
                {
                    result.Add(wds[yi++]);   // this word not in vocab
                }

            }

            // Copy any words that have not yet been checked.
            // If the vocab is at the end, they are all unrecognized.
            while (yi < wds.Length)
            {
                result.Add(wds[yi++]);
            }

            return result.ToArray();
        }

        private void btnMergeBook_Click(object sender, RoutedEventArgs e)
        {
            string[] vocab = File.ReadAllLines(vocabPath);
            string[] bookWords = getWordsInBook(bookPath);

            DateTime t0 = DateTime.Now;
            Array.Sort(bookWords);
            DateTime t1 = DateTime.Now;
            string[] uniqueWds = removeAdjacentDups(bookWords);
            string[] missingWords = findUnknownsMergePattern(vocab, uniqueWds);

            DateTime t2 = DateTime.Now;
            int sortTime = (int)(t1 - t0).TotalMilliseconds;
            int checkingTime = (int)(t2 - t1).TotalMilliseconds;
            MessageBox.Show(
                 String.Format("There are {0} unknown words in the book.\n" +
                       "Sort time = {1}ms.\nRemDups and checking time = {2}ms.",
                   missingWords.Length, sortTime, checkingTime));
        }


        private void btnMergeBook2_Click(object sender, RoutedEventArgs e)
        {

            string[] vocab = File.ReadAllLines(vocabPath);
            string[] bookWords = getWordsInBook(bookPath);

            DateTime t0 = DateTime.Now;

            string[] missingWords = findUnknownWords(vocab, bookWords);
            Array.Sort(missingWords);
            string[] uniqueWds = removeAdjacentDups(missingWords);


            DateTime t1 = DateTime.Now;
            int totalTime = (int)(t1 - t0).TotalMilliseconds;

            MessageBox.Show(
                 String.Format("There are {0} unknown words in the book.\n" +
                       "Total time = {1}ms.",
                    uniqueWds.Length, totalTime));
        }


        Random rng = new Random();

        private void shuffle(int[] xs)
        {
            int[] keys = new int[xs.Length];
            // Create an array of N random numbers to use as the sorting key.
            for (int i = 0; i < xs.Length; i++) keys[i] = rng.Next();
            Array.Sort(keys, xs);
        }

        private string stringifyArray(int[] xs)
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
            Debug.WriteLine(stringifyArray(xs));

            return;

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

            int tries = 1;
            while (boardHasDiagonalClashes(bd))
            {
                shuffle(bd);
                tries++;
            }
            Debug.WriteLine("Solution {0} found in {1} tries.", stringifyArray(bd), tries);
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



        private void btnFindQueens_Click(object sender, RoutedEventArgs e)
        {
            int[] solution = findQueensSolution(8);


            int[][] my_tickets =  
                  {   new int[] { 7, 17, 37, 19, 23, 43}, 
                      new int[] { 7,  2, 13, 41, 31, 43}, 
                      new int[] { 2,  5,  7, 11, 13, 17}, 
                      new int[] {13, 17, 37, 19, 23, 43} };
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            int[] a = { 10, 20, 30 };
            int[] b = { 100, 200, 300 };
            int xi = 1;
            a[xi++] = b[xi++];  // Which way round do these increments happen. Not what one might think.
            MessageBox.Show(stringifyArray(a));
        }
    }
}
