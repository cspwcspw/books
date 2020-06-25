using System;
using System.Windows;
using System.Windows.Media;
using ThinkLib;

namespace Clock
{
    /// <summary>
    /// Draw a clockface, and get the current time.
    /// </summary>
    public partial class MainWindow : Window
    {
        Turtle alex;
        Turtle tess;

        public MainWindow()
        {
            InitializeComponent();

            alex = new Turtle(playground);
            alex.Visible = false;

            tess = new Turtle(playground);
            tess.Visible = false;          
        }

        private void btnShowTime_Click(object sender, RoutedEventArgs e)
        {
            showTime();
        }

        private void showTime()
        {
            DateTime curr = DateTime.Now;               // Get the computer's time 
            txtResult.Text =  curr.ToLongTimeString();  // Show it in the textbox

            tess.Clear();
            tess.WarpTo(200, 200);

            // Get the hours, mins and secs we want to represent on the clock face.
            double hours = curr.TimeOfDay.TotalHours;
            double mins = curr.TimeOfDay.TotalMinutes;
            double secs = curr.TimeOfDay.TotalSeconds;
           
            drawHand(tess, hours * 30, 80, 5, Brushes.Red);
            drawHand(tess, mins * 6, 110, 3, Brushes.Yellow);
            drawHand(tess, secs * 6, 140, 1, Brushes.Blue);
        }

        // Draw one hand of the clock using turtle t, at the given angle, the given length,
        // the given brush width and brush.
        private void drawHand(Turtle t, double angle, double sz, double width, Brush b)
        {    
            t.LineBrush = b;
            t.Heading = angle - 90;
            t.BrushWidth = width;
            t.Forward(sz);
            t.Forward(-sz);
        }

        private void btnDrawClockFace_Click(object sender, RoutedEventArgs e)
        {
            drawFace(alex, 200, 200, 150);
        }

        private void drawFace(Turtle t, double x, double y, double radius)
        {
            t.BrushDown = false;
            t.WarpTo(x,y);
            t.Heading = 270;

            // Have a loop that stamps the same thing 12 times
            int hr = 0;
            while (hr < 12)
            {
                t.Forward(radius);
                t.Stamp();
                t.Forward(-radius);
                t.Right(30);
                hr = hr + 1;
            }
        }
    }
}
