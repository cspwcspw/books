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
using System.Diagnostics;
//using System.Runtime.InteropServices;

namespace RacingTurtle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RacingTurtleMainWindow : Window
    {
        Turtle tess;
        System.Windows.Threading.DispatcherTimer theTimer;

        BitmapImage theTrack = new BitmapImage(new Uri("pack://application:,,,/" + "Images/race_track.png"));

        double tessSpeed = 2;

        public RacingTurtleMainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground, 287, 444);

            tess.LineBrush = Brushes.Magenta;
            tess.BrushWidth = 3;

            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            theTimer.Interval = TimeSpan.FromMilliseconds(10);
            theTimer.IsEnabled = false;  // true;
            playground.RenderSize = new Size(300, 300);

            // Some situations to check that it all works under transforms.  

            //string inThisProject = "pack://application:,,,/";
            //BitmapImage bmi =  new BitmapImage(new Uri(inThisProject + "images/race_track.png"));

            //CroppedBitmap cbm = new CroppedBitmap(bmi, new Int32Rect(100, 100, 500, 500));
            
            //playground.Background = new ImageBrush() { Stretch =  Stretch.Fill, TileMode = TileMode.None, ImageSource = cbm  };

            //playground.RenderTransform = new RotateTransform(45, 300, 300);
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            advanceTess();
        }

        private void advanceTess()
        {
            tess.Forward(onTheRoad(tess) ? tessSpeed : tessSpeed / 5.0);    
        }

        private bool onTheRoad(Turtle t)
        {
            Color c = t.ColorUnderTurtle;
            return (c.G < 66);   // Inspect the green ccomponent to tell if tess is on the road.
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.Left:
                    tess.Left(15);
                    break;
                case Key.Right:
                    tess.Right(15);
                    break;
                case Key.X:
                    tessSpeed -= 1;
                    break;
                case Key.Z:
                    tessSpeed += 1;
                    break;
                case Key.Space:
                     theTimer.IsEnabled = !theTimer.IsEnabled;
                    break;
            }

        }
 

        // Useful for inspecting the picture...

        private void playground_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(playground);
            Color c = tess.ColorOfBackgroundAt(pos);
            this.Title = String.Format("{0}", c);
        }
    }
}
