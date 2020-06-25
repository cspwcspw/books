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
    /// Interaction logic for ScopeLifetimeGUI.xaml
    /// </summary>
    public partial class ScopeLifetimeGUI : Window
    {
        public ScopeLifetimeGUI()
        {
            InitializeComponent();
        }


        private void btnForceGC_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }


        Brush[] myFaves = { Brushes.Aqua, Brushes.Red, Brushes.Plum, Brushes.Yellow, Brushes.Blue, Brushes.SpringGreen };

        private void btnNewTurtle_Click(object sender, RoutedEventArgs e)
        {
            Random rng = new Random();
          
            Turtle tess = new Turtle(playground); //, rng.Next(400), rng.Next(400));

            long m1 = GC.GetTotalMemory(false);
            double[] xs = new double[10000];
            long m2 = GC.GetTotalMemory(false);
            string memInUse = string.Format("Memory for one turtle = {0}", m2-m1);
            this.Title = memInUse;

            tess.Heading = rng.Next(360);
            tess.BrushWidth = rng.Next(2, 10);
            tess.LineBrush = myFaves[rng.Next(myFaves.Length)];
            tess.Forward(100);


        }


        int v1 = 10;
        int v2 = 2;

        private int f1(int v1)
        {
            int result = 0;

            for (int i = 0; i < v1; i++)
            {
                result += v2 * i;
            }

            {
                Random rng = new Random(2013);
                int d1 = rng.Next(1, 7);
                int d2 = rng.Next(1, 7);
                result += d1 + d2;
            }

            {
                int i = 1;
                int d1 = 15;
                result += d1 + i;
            }

            return result;
        }


        private void btnScopes_Click(object sender, RoutedEventArgs e)
        {
            int v2 = 5;
            int n = f1(v2);
            MessageBox.Show(string.Format("The result is {0}", n));
        }

        private void btnMemUsage_Click(object sender, RoutedEventArgs e)
        {
            // Show the memory in use
            string memInUse = string.Format("Memory in use = {0}", GC.GetTotalMemory(false));
            this.Title = memInUse;
        }

        private void btnNewArray_Click(object sender, RoutedEventArgs e)
        {
            long m1 = GC.GetTotalMemory(false);
            double[] xs = new double[100000];  // allocate a large array,
            long m2 = GC.GetTotalMemory(false);
            string memInUse = string.Format("Memory for one array = {0}", m2 - m1);
            this.Title = memInUse;
        }

 
    }
}
