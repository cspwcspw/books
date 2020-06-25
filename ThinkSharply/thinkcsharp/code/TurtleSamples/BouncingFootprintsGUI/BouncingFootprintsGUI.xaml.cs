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

namespace BouncingFootprints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BouncingFootprintsGUI : Window
    {

        Turtle tess;
        System.Windows.Threading.DispatcherTimer theTimer;

        double tessSpeed = 10;

        List<MovingFootprint> gameThings = new List<MovingFootprint>();

        public BouncingFootprintsGUI()
        {
            InitializeComponent();
            tess = new Turtle(playground);
            tess.BrushDown = false;

            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(100);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateGameThings();
        }

        private void updateGameThings()
        {
            tess.Forward(tessSpeed);
            for (int i = gameThings.Count - 1; i >= 0; i--)
            {
                MovingFootprint p = gameThings[i];
                p.Update();
                if (p.getTimeToLive() <= 0)
                {
                    gameThings.Remove(p);
                    tess.Footprints.Remove(p.myFp);
                }

            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    tess.Left(15);
                    break;
                case Key.Right:
                    tess.Right(15);
                    break;
                case Key.Space:
                    Footprint fp = tess.Stamp();
                    MovingFootprint mfp = new MovingFootprint(fp);
                    gameThings.Add(mfp);
                    break;
            }
        }

    }
}
