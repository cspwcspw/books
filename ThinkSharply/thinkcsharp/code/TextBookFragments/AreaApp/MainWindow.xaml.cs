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

namespace AreaApp
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

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            double w = Convert.ToDouble(txtWidth.Text);
            double h = Convert.ToDouble(txtHeight.Text);
            double area = w * h;
            string result = String.Format(" {0} x {1} gives an area of {2}.\n", w, h, area);
            txtResults.AppendText(result);
        }
    }
}
