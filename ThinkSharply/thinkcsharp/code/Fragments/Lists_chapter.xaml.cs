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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using ThinkLib;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Lists_chapter : Window
    {
        public Lists_chapter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            List<int> daysInMonth = new List<int>() { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int v1 = sumList1(daysInMonth);
            int v2 = sumList2(daysInMonth);
         //   Tester.TestEq(v1, v2);

            string[] textLines1 = File.ReadAllLines("c:\\temp\\vocab.txt");
            List<string> xs = new List<string>(textLines1);
 
            string[] textLines2 = File.ReadAllLines("c:\\temp\\alice_in_wonderland.txt");
            xs.AddRange(textLines2);

            MessageBox.Show(string.Format("There are {0} strings in xs", xs.Count));

            xs.Sort();

            int i = xs.IndexOf("rotten");

            string[] us = { "I", "am", "not", "a", "crook" };
            string[] vs = { "I", "am", "not", "a", "crook" };
            Debug.WriteLine("Test 1: {0}",  us == vs);
            us = vs;
            Debug.WriteLine("Test 2: {0}", us == vs);
            
        }

        private int sumList1(List<int> xs)
        {
            int sum = 0;
            for (int i = 0; i < xs.Count; i++)
            {
                sum += xs[i];
            }
            return sum;
        }

        private int sumList2(List<int> xs)
        {
            int sum = 0;
            foreach (int x in xs)
            {
                sum += x;
            }
            return sum;

        }



        private void btnDoubleAll_Click(object sender, RoutedEventArgs e)
        {
                List<int> xs = new List<int>() { 3,5,4,7,2 };      
                Tester.TestEq(doubleAll_1(xs), new List<int>() {6, 10, 8, 14, 4});
                Tester.TestEq(xs, new List<int>() { 3,5,4,7,2 });


                List<int> ys = new List<int>() { 3, 5, 4, 7, 2 };
                doubleAll_2(ys);
                Tester.TestEq(ys, new List<int>() { 6, 10, 8, 14, 4 });
        }

        private List<int> doubleAll_1(List<int> xs)
        {
            List<int> result = new List<int>();
            foreach (int x in xs)
            {
                result.Add(2*x);
            }
            return result;
        }


        private void doubleAll_2(List<int> xs)
        {
            List<int> result = new List<int>();
            for (int i=0; i < xs.Count; i++) 
            {
                xs[i] *= 2;
            }
        }

        private void btnMoveToBack_Click(object sender, RoutedEventArgs e)
        {
            List<int> xs = new List<int>() { 30, 50, 40, 70, 20 };
            moveToBack(xs, 2);  // move element at position to the back
            Tester.TestEq(xs, new List<int>() { 30, 50, 70, 20, 40 });
            moveToBack(xs, 0);
            moveToBack(xs, -1);
            moveToBack(xs, 4);
            moveToBack(xs, 5);
            moveToBack(xs, 2);
            Tester.TestEq(xs, new List<int>() { 50, 70, 40, 30, 20 });
        }

        private void moveToBack(List<int> xs, int p)
        {
            if (p < 0 || p >= xs.Count) return;
            int e = xs[p];
            xs.RemoveAt(p);
            xs.Add(e);
        }

        private void btnDeleteSmallerSuccessors_Click(object sender, RoutedEventArgs e)
        {
            List<int> xs = new List<int>() { 12, 16, 14, 14, 16, 18, 11, 9, 12, 4, 2 };
            
            deleteSmallerSuccessors(xs);

            List<int> expected = new List<int>() { 12, 16, 14, 16, 18, 12 };
            Tester.TestEq(xs, expected);

            deleteSmallerSuccessors(xs);
            Tester.TestEq(xs, new List<int>() { 12, 16, 16, 18 });

        }


        private void deleteSmallerSuccessors(List<int> xs)
        {
            for (int i = xs.Count - 1; i > 0; i--)
            {
                if (xs[i] < xs[i - 1])
                {
                    xs.RemoveAt(i);
                }
            }
        }

    }
}
