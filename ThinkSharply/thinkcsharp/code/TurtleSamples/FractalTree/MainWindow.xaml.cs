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

namespace FractalTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Turtle tess;

        double theta = 90;

        public MainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground);
            tess.Visible = false;
            tess.BrushWidth = 3;
            System.Windows.Threading.DispatcherTimer theTimer = new System.Windows.Threading.DispatcherTimer();
           // theTimer.Interval = TimeSpan.FromMilliseconds(1);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;
        }

        DateTime e0 = DateTime.Now;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - e0;
            this.Title = String.Format("Timer interval = {0} ms", ts.TotalMilliseconds);
            drawNextTree();
            e0 = DateTime.Now;
        }

        DateTime t0 = DateTime.Now;
        int frameCounter = 0;

        private void drawNextTree()
        {
            tess.BatchSize = 0;
            tess.Clear();

            tess.WarpTo(playground.ActualWidth / 2, playground.ActualHeight - 10);
            tess.Heading = -90;

            drawTree(7, theta, playground.ActualHeight - 10);

            theta += 2;
            tess.FlushToPlayground();

            if (++frameCounter % 20 == 0)
            {
                TimeSpan ts = DateTime.Now.Subtract(t0);
                double rate = frameCounter / ts.TotalSeconds;
                frameCounter = 0;
                //   this.Title = String.Format("Frame rate = {0}", rate);
                t0 = DateTime.Now;
            }
        }

        const double trunk_ratio = 0.29;       // How big is the trunk relative to whole tree?

        public void drawTree(int order, double theta, double treeSize)
        {
            double trunkSize = treeSize * trunk_ratio;  // length of trunk
            tess.Forward(trunkSize);                    // always draw the trunk

            if (order > 0)                              // must we also draw subtrees?
            {
                double branchSize = treeSize - trunkSize;
                tess.Left(theta);
                drawTree(order - 1, theta, branchSize);
                tess.Right(2 * theta);
                //if (order == 7)
                //{
                //    tess.LineBrush = Brushes.Green;
                //    drawTree(order - 1, theta, branchSize);
                //    tess.LineBrush = Brushes.Magenta;
                //}
                //else
                {
                    drawTree(order - 1, theta, branchSize);
                }

                tess.Left(theta);
            }

            tess.Forward(-trunkSize);          // make sure we end up where we started.
        }
    }
}