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

using System.IO;

namespace Fragments
{
    public partial class TurtleGTXGUI: Window
    {
        TurtleGTX tess;

        public TurtleGTXGUI()
        {
            InitializeComponent();
            tess = new TurtleGTX(playground, 200, 200);
        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int side = 0; side < 4; side++)
                {
                    tess.Forward(100);
                    tess.Right(90);
                }
                tess.Right(20);
            }
            MessageBox.Show(tess.ToString());
        }

        private void btnDemo2_Click(object sender, RoutedEventArgs e)
        {
            tess.Forward(40);
            tess.Spin();
            tess.Forward(10);
            tess.Jump(20);
            tess.Forward(20);
        }

        private void btnInterfaces_Click(object sender, RoutedEventArgs e)
        {
            TurtleGTX alex = new TurtleGTX(playground, 100, 100);
            TurtleGTX dave = new TurtleGTX(playground, 200, 200);
            TurtleGTX[] xs = { alex, dave };

            alex.Forward(80);
            dave.Forward(40);

            Array.Sort(xs);



        }

    }

 
}
