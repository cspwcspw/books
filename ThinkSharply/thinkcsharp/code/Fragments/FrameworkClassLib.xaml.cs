using System;
using System.Windows;
using System.Net.Security;

namespace Fragments
{
    public partial class RandomDemoGUI : Window
    {
        Random myRandomSource;

        protected int v4;
        internal int v3;
        private int v2;
        public int v1;

        public RandomDemoGUI()
        {
            InitializeComponent();

            myRandomSource = new Random(42);

            Exception ex = new Exception("Wharrah");
       }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            // Pick two (different) random cards from a deck of cards numbered 0 to 51.

            int card1 = myRandomSource.Next(52);
            int card2 = myRandomSource.Next(52);

            while (card2 == card1)  // oops. try again till we get a different second card.
            {
                card2 = myRandomSource.Next(52);
            }
            txtResult.AppendText(string.Format("The two cards are {0} and {1}\n", card1, card2));
            txtResult.ScrollToEnd();
        }


        private void btnRandomDbl_Click(object sender, RoutedEventArgs e)
        {
            double num = myRandomSource.NextDouble() * 7.5 + 20;  
            txtResult.AppendText(string.Format("The number is {0}\n", num));
            txtResult.ScrollToEnd();
        }
    }
}
