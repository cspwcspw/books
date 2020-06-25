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
using System.Diagnostics;

namespace Fragments
{
    /// <summary>
    /// A main menu for organizing code fragments that are used in the textbook.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
          //  Console.WriteLine("Loaded");
            listBox1.ItemsSource =
                  new string[] { 
                    "StartWithWindow", "Diving_into_code",  "Nesting", "Favourite_controls",
                    "Turtles", "Void Methods", "Conditionals", "Value-returning methods", "Iteration",
                    "Strings", "Classes I",  "Arrays", 
                    "Events", "I/O and Files", "Lists", "List Algorithms", 
                    "Queens", "Recursion", "Exceptions",
                    "FrameworkClassLib","Scope_and_Lifetime", 
                    "TrafficLights", "TurtleGTX",
                    "Dictionaries", "Interfaces" };
            listBox1.SelectedItem = "Queens";
            

            //string s = "\u2659  \u265A \u2669  \u266A  \u266B  \u266C  \u263a  ";
            //textBox1.Text = s;
            //this.Title = s;
          

            //Debug.Print("The time now is {0}. It day is {1}", DateTime.Now, DateTime.Now.DayOfWeek);
            //Debug.Print("The time now is {0}", DateTime.Now.ToShortTimeString());
        }



        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Window wn = null;
            switch (listBox1.SelectedItem.ToString())
            {
                case "StartWithWindow":
                    wn = new Favorite_controls();
                    break;
                case "Nesting":
                    wn = new Nesting();
                    break;
                case "Favourite_controls":
                    wn = new Favorite_controls();
                    break;
                case "Diving_into_code":
                    wn = new Diving_into_code();
                    break; 
                case "Turtles":
                    wn = null;
                    break;
                case "Void Methods":
                    wn = null;
                    break;
                case "Conditionals":
                    wn = null;
                    break;
                case "Value-returning methods":
                    wn = null;
                    break;
                case "Iteration":
                    wn = null;
                    break;
                case "Strings":
                    wn = null;
                    break;
                case "Classes I":
                    wn = null;
                    break;
                case "Arrays":
                    wn = null;
                    break;
                case "Events":
                    wn = null;
                    break;
                case "I/O and Files":
                    wn = new Files_chapter();
                    break;
                case "Lists":
                    wn = new Lists_chapter();
                    break;
                case "List Algorithms":
                    wn = new List_algorithms_chapter();
                    break;
                case "Queens":
                    wn = new Queens();
                    break;
                case "Recursion":
                    wn = new Recursion_chapter();
                    break;
                case "Exceptions":
                    wn = new Exceptions();
                    break;
                case "FrameworkClassLib":
                    wn = new RandomDemoGUI();
                    break;
                case "Scope_and_Lifetime":
                    wn = new ScopeLifetimeGUI();
                    break;

                case "TrafficLights":
                    wn = new TrafficLightsGUI(); ;
                    break;
                case "TurtleGTX":
                    wn = new TurtleGTXGUI();
                    break;

                case "Dictionaries":
                    wn = new DictionaryGUI();
                    break;

                case "Interfaces":
                    wn = new Interfaces_chapter();
                    break;

                default:
                    break;
            }
            if (wn != null) wn.Show();// .ShowDialog();
        }


    }
}
