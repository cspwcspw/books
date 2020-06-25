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

namespace Window_design_chapter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listBox1.ItemsSource =
                new string[] {
                    "MyMood",
                    "WhereAmI",
                    "MoviesILike",
                    "SMSSender",
                    "BirthDate"
                };
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            
            switch (listBox1.SelectedItem.ToString())
            {

                case "MyMood":
                   new MyMood().Show();
                   break;

                case "WhereAmI":
                   new WhereAmI().Show();
                   break;

                case "MoviesILike":
                   new MoviesILike().Show();
                   break;

                case "SMSSender":
                   new SMSSender().Show();
                   break;

                case "BirthDate":
                   new BirthDate().Show();
                   break;

            }
        }
    }
}
