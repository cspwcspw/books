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
    /// Interaction logic for Interfaces_chapter.xaml
    /// </summary>
    public partial class Interfaces_chapter : Window
    {
        public Interfaces_chapter()
        {
            InitializeComponent();
        }

        private void btnDemo1_Click(object sender, RoutedEventArgs e)
        {
            int[] nums =        { 12, 4, 7 };
            string[] names =    { "Joe", "Bill", "Mary" };
            DateTime[] times =  {new DateTime(1995,07,24), new DateTime(1995,07,11), new DateTime(1993,07,24)};
            Rectangle[] rects = { new Rectangle(), new Rectangle(), new Rectangle() };

            Array.Sort(nums);
            Array.Sort(names);
            Array.Sort(times);
        //    Array.Sort(rects);

            List<int> sd;
            int[] xs;

            Dictionary<string, string> sddf;

        }
    }
}
