using System;
using System.Collections.Generic;
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

namespace Iteration_Chapter
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

        //private void generateMultiples(int n, int sz)
        //{
        //    for (int i = 1; i <= sz; i++)
        //    {
        //        txtResult.AppendText(String.Format("{0,5}", i * n));
        //    }
        //    txtResult.AppendText("\r\n");
        //}

        //private void generateMultiplicationTable(int sz)
        //{
        //    for (int i = 1; i <= sz; i++)
        //    {
        //        generateMultiples(i, sz);
        //    }
        //}

        //private void btnG0_Click(object sender, RoutedEventArgs e)
        //{
        //    int tsz = int.Parse(txtTableSize.Text);
        //    generateMultiplicationTable(tsz);
        //}


        private double sqrt(double n)
        {
            double approx = n / 2.0;     // Start with some or other guess at the answer
            while (true)
            {
                double better = (approx + n / approx) / 2.0;
                if (Math.Abs(approx - better) < 0.000000001)
                {
                    return better;
                }
                approx = better;
            }
        }

        private string remove_punctuation(string s)
        {
            string result = "";
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                {
                    result += c;                 // This step is inefficient!
                }
            }
            return result;
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            string book = System.IO.File.ReadAllText("c:\\temp\\alice_in_wonderland.txt");
            string cleanedString = remove_punctuation(book);
            txtResult.Text = cleanedString;
            string[] words = cleanedString.Split();
            MessageBox.Show(String.Format("There are {0} words in the book.", words.Length));
        }

    
        //private void btnG0_Click(object sender, RoutedEventArgs e)
        //{
        //    double x = double.Parse(textBox1.Text);
        //    double y = sqrt(x);
        //    string output = String.Format("sqrt({0}) gives {1}\r\n", x, y);
        //    txtResult.AppendText(output);
        //}

        //private void btnG0_Click(object sender, RoutedEventArgs e)
        //{
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        string txt = String.Format("{0,4} {1,8}\r\n", i, Math.Pow(2, i));
        //        txtResult.AppendText(txt);
        //    }
        //}
    }
}
