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

namespace Part1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Turtle tess, alex;
        int myAge = 18;       // Define and initialize some other class-level variables.
        bool haveLicence = true;

        public MainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground, 100, 30);  // Create a turtle in the playgound.
            tess.BrushWidth = 5.1;                     // Set some properties.

            alex = new Turtle(playground, 300, 100);  // Create another turtle in the playgound
            alex.LineBrush = Brushes.Blue;
            alex.BodyBrush = Brushes.Blue;
            alex.BrushWidth = 1;
        }

        private void drawSquare(Turtle t, double sz)  // t and sz are parameters
        {
            int side = 0;
            while (side < 4)
            {
                t.Forward(sz);
                t.Right(90);
                side = side + 1;
            }
        }

        private void drawMulticolorSquare(Turtle t, float sz)
        {
            Brush[] bs = { Brushes.Red, Brushes.Yellow, Brushes.Magenta, Brushes.Blue };
            foreach (Brush b in bs)
            {
                t.LineBrush = b;
                t.Forward(sz);
                t.Right(90);
            }
        }

        private void btnDemo1_Click(object sender, RoutedEventArgs e)
        {
            alex.BrushWidth = 3;
            alex.Visible = true;
            int size = 20;
            for (int i = 0; i < 15; i = i + 1)
            {
                drawMulticolorSquare(alex, size);
                size = size + 6;        // Increase the size for next time
                alex.Forward(10);       // Move alex along a little
                alex.Right(18);
            }
        }

        private void btnDemo2_Click(object sender, RoutedEventArgs e)
        {
            alex.DelayMillisecs = 100;
            int i = 0;
            while (i < 72)
            {
                alex.Forward(120);
                alex.Right(95);
                i = i + 1;
            }
        }

        private void btnDemo3_Click(object sender, RoutedEventArgs e)
        {
            alex.WarpTo(200, 200);    // Warp without drawing
            alex.BrushDown = false;   // Pick up the brush
            int size = 10;
            int i = 0;
            while (i < 30)
            {
                alex.Stamp();         // Stamp a footprint
                size = size + 2;
                alex.Forward(size);
                alex.Right(24);
                i = i + 1;
            }
        }
    }
}
