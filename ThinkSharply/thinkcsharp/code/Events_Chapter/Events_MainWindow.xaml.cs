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
using System.Media;
using System.Windows.Resources;

namespace Events_Chapter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer theTimer;

       
 
        public MainWindow()
        {
            InitializeComponent();
            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Tick += dispatcherTimer_Tick;
            theTimer.Interval = TimeSpan.FromMilliseconds(50);
            theTimer.IsEnabled = true;
          
         }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updatePaddle();
            updateBall();       
        }

        double paddleSpeed = 4;
        string paddleState = "stopped";

        private void updatePaddle()
        {
            switch (paddleState)    // See what state the paddle is in, and respond appropriately
            {
                case "stopped":
                    break;
                case "movingLeft":
                    double nextX1 = Canvas.GetLeft(paddle) - paddleSpeed;
                    Canvas.SetLeft(paddle, nextX1);
                    break;
                case "movingRight":
                    double nextX2 = Canvas.GetLeft(paddle) + paddleSpeed;
                    Canvas.SetLeft(paddle, nextX2);
                    break;
            }
            Canvas.SetTop(paddle, canvas1.ActualHeight - paddle.ActualHeight - 5);
        }

        double velX = 3, velY = 4;

        private void updateBall()
        {
            double nextX = Canvas.GetLeft(ball) + velX;
            Canvas.SetLeft(ball, nextX);
            if (nextX < 0 || nextX + ball.ActualWidth > canvas1.ActualWidth && velX > 0)
            {
                velX = -velX;         // Change direction
                nextX += velX;        // undo the move that took us over the edge.
                makeBounceSound();
            }
            Canvas.SetLeft(ball, nextX);

            double nextY = Canvas.GetTop(ball) + velY;
            if (nextY < 0 || nextY + ball.ActualHeight >= canvas1.ActualHeight)
            {
                velY = -velY;
                nextY += velY;     // undo the move that took us over the edge.
                makeBounceSound();
            }
            Canvas.SetTop(ball, nextY);
        }

        private void makeBounceSound()
        {
            StreamResourceInfo strm = Application.GetResourceStream(new Uri("pack://application:,,,/bounce.wav"));
            SoundPlayer sp = new SoundPlayer(strm.Stream);
            sp.Play();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    paddleState = "movingLeft";
                    break;
                case Key.Right:
                    paddleState = "movingRight";
                    break;
                default:
                    paddleState = "stopped";
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            paddleState = "stopped";
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(canvas1);
            Canvas.SetLeft(paddle, pos.X);
        }



    }
}
