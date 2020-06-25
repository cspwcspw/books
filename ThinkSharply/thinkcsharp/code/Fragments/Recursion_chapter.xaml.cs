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

namespace Fragments
{
    /// <summary>
    /// Interaction logic for Recursion_chapter.xaml
    /// </summary>
    public partial class Recursion_chapter : Window
    {
        public Recursion_chapter()
        {
            InitializeComponent();
        }

        private void btnAllSubsets_Click(object sender, RoutedEventArgs e)
        {

            List<List<int>> try0 = allSubsets(new List<int>() { });
            List<List<int>> try1 = allSubsets(new List<int>() { 42 });
            List<List<int>> try2 = allSubsets(new List<int>() { 42, 50 });


            List<int> inp1 = new List<int>() { 1, 2, 3 };
            List<List<int>> out1 = new List<List<int>>() 
                         { new List<int> {}, 
                           new List<int> {1},
                           new List<int> {2},
                           new List<int> {1,2},
                           new List<int> {3},
                           new List<int> {1,3},
                           new List<int> {2,3},
                           new List<int> {1,2,3} };

            List<List<int>> out2 = new List<List<int>>() 
                         { new List<int> {}, 
                           new List<int> {1},
                           new List<int> {2},
                           new List<int> {1,2},
                           new List<int> {3},
                           new List<int> {1,3},
                           new List<int> {2,3},
                           new List<int> {1,2,3} };

            Tester.TestEq(out1, out2);
            List<List<int>> try4 = allSubsets(inp1);
            Tester.TestEq(try4, out1); 
        }

        private List<List<int>> allSubsets(List<int> orig)
        {
            // It's a fun algorithm, but not for our first years! 
            int n = orig.Count;
            if (n == 0) return new List<List<int>>() { new List<int> () };

            int elem = orig[n-1];
            List<int> tail = new List<int>(orig); // clone the list
            tail.RemoveAt(n-1);

            List<List<int>> result = allSubsets(tail);

            int numSubsets = result.Count;
            for (int i=0; i < numSubsets; i++)  
            {
                List<int> clone = new List<int>(result[i]);
                clone.Add(elem);
                result.Add(clone);
            }

            return result;
        }
    }
}
