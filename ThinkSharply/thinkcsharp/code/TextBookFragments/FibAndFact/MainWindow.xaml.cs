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
using System.Numerics;

namespace FibAndFact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int fibCallCount;
        int maxDepthSeen;

        private int fib(int n, int depth = 0)
        {
            fibCallCount++;
            if (depth > maxDepthSeen)
            {
                maxDepthSeen = depth;
            }

            if (n <= 1) return n;
            int t = fib(n - 1, depth + 1) + fib(n - 2, depth + 2);
            return t;
        }

        private void btnFib_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtFib.Text);
            DateTime t0 = DateTime.Now;
            maxDepthSeen = 0;
            fibCallCount = 0;
            BigInteger result = fib(n);
            double elapsedSecs = DateTime.Now.Subtract(t0).TotalSeconds;
            MessageBox.Show(
                String.Format("The {0}'th Fib number is {1}\nTime taken {2:F2} secs.\nCalls={3} MaxDepth={4}", 
                              n, result, elapsedSecs, fibCallCount, maxDepthSeen));
        }

        private void btnFact_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtFact.Text);
            BigInteger result = fact(n);
            BigInteger s = result;
            int c = 0;
            while (s % 10 == 0)
            {
                s = s / 10;
                c++;
            }
            MessageBox.Show(String.Format("{0} factorial is {1}\n which has {2} trailing zeros", n, result, c));
        }

        private BigInteger fact(int n)
        {
            if (n <= 1) return 1;
            else return n * fact(n - 1);
        }
    }
}
