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
    /// Interaction logic for Favorite_controls.xaml
    /// </summary>
    public partial class Favorite_controls : Window
    {
        public Favorite_controls()
        {
            InitializeComponent();
            string poem =
@"The truth I do not stretch or shove
When I state that the dog is full of love.
I've also found, by actual test,  
A wet dog is the lovingest.  
            
                    Ogden Nash



";

            textBox1.Text = poem;
            string inThisproject = "pack://application:,,,/";  // the magic spell
            //BitmapImage bm = new BitmapImage(new Uri(inThisproject + "Queen_of_Hearts.png"));
            //Image im2 = new Image();
            //im2.Source = bm;

            //checkBox2.Content = im2;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Opacity = canvas1.Opacity * 0.90;
        }
    }
}
