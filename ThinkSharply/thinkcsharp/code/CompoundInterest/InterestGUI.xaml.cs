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

namespace CompoundInterest
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

        private double finalAmt(double p, double r, int n, double t)
        {
            // Apply the compound interest formula to p to produce the final amount.        
            double a = p * Math.Pow((1 + r / n), (n * t));
            return a;
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            double toInvest = double.Parse(txtPrincipal.Text);
            double rate = double.Parse(txtInterestRate.Text);
            int n = int.Parse(txtNumTimesCompounded.Text);
            double t = double.Parse(txtYearsInvested.Text);

            double final = finalAmt(toInvest, rate, n, t);
            txtResult.AppendText(String.Format("Returned from method: {0}\r\n", final));
            txtResult.AppendText(String.Format("Formatted as a currency: {0:C}\r\n", final));
        }
    }
}
