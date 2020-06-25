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

namespace ColorMixer
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

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setBackgroundColor();
        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setBackgroundColor();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setBackgroundColor();
        }

        private void setBackgroundColor()
        {
            // Do not try anything if the window is not ready yet.
            if (!this.IsInitialized) return;

            byte r = Convert.ToByte(slider1.Value);
            byte g = Convert.ToByte(slider2.Value);
            byte b = Convert.ToByte(slider3.Value);

            label1.Content = r;
            label2.Content = g;
            label3.Content = b;
            
            this.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

    }
}
