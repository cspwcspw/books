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

namespace Debugging1
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

            tess = new Turtle(playground, 10, 30);
            tess.LineBrush = Brushes.Purple;
            tess.BrushWidth = 3;
        }

        private void btnDemo1_Click(object sender, RoutedEventArgs e)
        {
            drawRectangle(tess, 150.0, 100.0);
            tess.WarpTo(200, 50);
            tess.LineBrush = Brushes.Green;
            drawRectangle(tess, 30.0, 20.0);
        }

        private void drawRectangle(Turtle tx, double wSz , double hSz)
        {
            for (int i = 0; i < 2; i = i + 1)
            {
                tx.Forward(wSz);
                tx.Right(90);
                tx.Forward(hSz);
                tx.Right(90);
            }
        }
    }
}
