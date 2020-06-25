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
using ThinkLib;

namespace BarChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Turtle tess;

        public MainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground);

            tess.BrushWidth = 2;
        }

        private void draw_bar(Turtle t, int height)
        {
            // Get turtle t to draw one bar, of height.
            t.Left(90);
            t.Forward(height);     // Draw up the left side
            t.Right(90);
            t.Stamp(String.Format("{0}", height), 0, height > 0 ? -24 : 0);
            t.Forward(40);         // Width of bar, along the top
            t.Right(90);
            t.Forward(height);     // And down again!
            t.Left(90);            // Put the turtle facing the way we found it.
            t.Forward(10);         // Leave small gap after each bar
        }

        private void drawChart_Click(object sender, RoutedEventArgs e)
        {
            doChart();
        }

        private void doChart()
        {

            int[] xs = { 24, 57, 100, 120, -80, 130, 110 };
            tess.Reset();
            tess.LineBrush = Brushes.Purple;
            tess.Filling = true;

            Brush b = new LinearGradientBrush(Colors.Cyan, Colors.Red, 45);
            Brush c = new RadialGradientBrush(Colors.Yellow, Colors.Magenta);

            foreach (int v in xs)
            {
                tess.FillBrush = v < 0 ? c : b;
                draw_bar(tess, v);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            doChart();
        }


    }
}
