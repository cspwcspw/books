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

namespace ClockFace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClockFaceMain : Window
    {

        Turtle tess;
        public ClockFaceMain()
        {
            InitializeComponent();
            tess = new Turtle(playground);
        }

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            tess.Clear();
            tess.WarpTo(200, 200);
            tess.BrushDown = false;
            tess.BrushWidth = 4;
            tess.DelayMillisecs = 100;

            int h = 0;
            while (h < 12)
            {
                tess.Forward(90);
                tess.BrushDown = true;
                tess.Forward(10);
                tess.BrushDown = false;
                tess.Forward(20);
                tess.Stamp();
                tess.Forward(-120);
                tess.Right(360 / 12);
                h = h + 1;
            }
        }

    }
}
