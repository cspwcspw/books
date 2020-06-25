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

namespace Snakes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Turtle tess;

        bool gameIsActive = true;

        System.Windows.Threading.DispatcherTimer theTimer;

        int cellSize = 20;

        int X, Y, direction;

        int targetBodyLength;
        int tessStepCount;

        public MainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground,10,10);
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            startNewGame();
        }

        private void startNewGame()
        {
            theTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            theTimer.IsEnabled = false;
            direction = 0;
            X = 0;
            Y = 0;
            tessStepCount = 0;
            targetBodyLength = 4;
            tess.Reset();
            tess.BrushDown = false;
            gameIsActive = true;
            playground.Background = Brushes.LightSteelBlue;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateGame();
        }

        private void updateGame()
        {       
            if (! gameIsActive) return;

            tessStepCount++;
            if (tessStepCount % 10 == 0)
            {
                targetBodyLength++;
            }

            tess.Stamp();
            while (tess.Footprints.Count > targetBodyLength)
            {
                tess.Footprints.RemoveAt(0);
            }
            int cellsHorizontal = (int)(playground.ActualWidth / cellSize);
            int cellsVertical = (int)(playground.ActualHeight / cellSize);
            switch (direction)
            {
                case 0:
                    X++;                  
                    if (X >= cellsHorizontal) X = 0; 
                    break;
                case 1:
                    Y++;                  
                    if (Y >= cellsVertical) Y = 0; 
                    break;
                case 2:
                    X--;
                    if (X < 0) X = cellsHorizontal-1; 
                    break;
                case 3:
                    Y--;                  
                    if (Y < 0) Y = cellsVertical-1; 
                    break;
                default:
                    throw new ApplicationException(String.Format("Unexpected direction {0}", direction));
            }
            Point pos = new Point(X * cellSize + 10, Y * cellSize + 10);
            if (hasCollision(tess, pos))
            {
                playground.Background = Brushes.Salmon;
                gameIsActive = false;
            }
            else
            {
                tess.Goto(pos);
            }

        }

        private bool hasCollision(Turtle t, Point p)
        {
            // Does t's head clash with t's body (footprints)
            foreach (Footprint fp in t.Footprints)
            {
                if (fp.Position == p)
                {
                    return true;
                }
            }
            return false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;

                case Key.R:
                    theTimer.IsEnabled = !theTimer.IsEnabled;
                    break;

                case Key.N:   // New game
                    startNewGame();
                    break;

                case Key.Space:
                    theTimer.IsEnabled = false;
                    updateGame();
                    break;
                    
                case Key.Left:
                    tess.Left(90);
                    direction = (direction + 3) % 4;
                    break;

                case Key.Right:
                    tess.Right(90);
                    direction = (direction + 1) % 4;
                    break;

                case Key.OemPlus:
                    theTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
                    break;

                case Key.OemMinus:
                    theTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    break;
            }
        }
    }
}
