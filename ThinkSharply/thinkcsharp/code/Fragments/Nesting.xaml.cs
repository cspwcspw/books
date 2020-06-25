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

namespace Fragments
{
    /// <summary>
    /// Interaction logic for Nesting.xaml
    /// </summary>
    public partial class Nesting : Window
    {
        public Nesting()
        {
            InitializeComponent();
        }

        private void Tess_Click(object sender, RoutedEventArgs e)
        {
            int x = -7 % 4;
            int y = 7 % -4;
            int z = -7 % -4;
            int a = 5, b = 10, c = a + b;
            MessageBox.Show("It is Tess' birthday on 15 January!", "Birthday reminder");
        }



    }
}
