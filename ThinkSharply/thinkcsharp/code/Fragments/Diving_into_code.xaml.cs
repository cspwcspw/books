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
    /// Interaction logic for Diving_into_code.xaml
    /// </summary>

    public partial class Diving_into_code : Window
    {
        // Because this variable definition is outside any method, 
        // it is a class-level variable and it lives on, even after a 
        // method finishes executing.
        int clickCounter = 0;

        public Diving_into_code()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // A variable defined here only has a short lifetime
            // while this method is active.
            int ageNow = 18;

            // Add one to the class-level counter variable
            clickCounter = clickCounter + 1;

            // prepare the message we want shown.
            string msg = string.Format("This was click number {0}.\n", clickCounter);

            // Show the results in our GUI
            txtResults.AppendText(msg);

            // Now even if we change the local variable, it is about to die!
            ageNow = ageNow + 1;
            string msg2 = string.Format("I turn {0} next birthday.\n", ageNow);
            txtResults.AppendText(msg2);
        }
    }
}
