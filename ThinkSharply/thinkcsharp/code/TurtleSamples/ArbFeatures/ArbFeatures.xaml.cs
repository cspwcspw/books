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
using System.Threading;

namespace ArbFeatures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Turtle tess, alex;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = ThinkLib.Turtle.VersionDate;
            tess = new Turtle(playground, 100, 200);
            alex = new Turtle(playground, 50, 50);
            alex.BrushWidth = 6;
            alex.LineBrush = Brushes.Blue;

            alex.Stamp(new EllipseGeometry(new Point(0,0), 30, 10));

         //   listBox1.DataContext = names;
            listBox1.ItemsSource = names;
            tess.Stamp(new EllipseGeometry(new Point(0, 0), 30, 10));
         //  
        }


        string[] names = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        int[,] pirate = { { 10, 30 }, { 5, 15 }, { 3, 7 } };

        public void drawRectangle(Turtle tx, double wSz, double hSz)
        {
            for (int i = 0; i < 2; i = i + 1)
            {
                tx.Forward(wSz);
                tx.Right(90.0);
                tx.Forward(hSz);
                tx.Right(90.0);

            }
        }




        void koch(Turtle t, int order, double size)
        {
            // Make turtle t draw a Koch fractal of 'order' and 'size'.
            // Leave the turtle facing the same direction.

            if (order == 0)
            {   // The base case is just a straight line
                t.Forward(size);
            }
            else
            {
                koch(t, order - 1, size / 3);   // Go 1/3 of the way
                t.Left(60);
                koch(t, order - 1, size / 3);
                t.Right(120);
                koch(t, order - 1, size / 3);
                t.Left(60);
                koch(t, order - 1, size / 3);
            }
        }


        void koch0(Turtle t, double size)
        {
                t.Forward(size);
        }

        void koch1(Turtle t, double size)
        {
                koch0(t, size / 3);   
                t.Left(60);
                koch0(t, size / 3);
                t.Right(120);
                koch0(t, size / 3);
                t.Left(60);
                koch0(t, size / 3);
        }

        void koch2(Turtle t, double size)
        {
            koch1(t, size / 3);
            t.Left(60);
            koch1(t, size / 3);
            t.Right(120);
            koch1(t, size / 3);
            t.Left(60);
            koch1(t, size / 3);
        }

        void koch3(Turtle t, double size)
        {
            koch2(t, size / 3);
            t.Left(60);
            koch2(t, size / 3);
            t.Right(120);
            koch2(t, size / 3);
            t.Left(60);
            koch2(t, size / 3);
        }
 
        private void btnKoch_Click(object sender, RoutedEventArgs e)
        {
            DateTime t0 = DateTime.Now;
            tess.Reset();
            tess.BrushWidth = 0.5;
            tess.DelayMillisecs = 0;
           // tess.Visible = false;
            tess.LineBrush = Brushes.DarkBlue;
            //// Demonstrate how to batch calls for speedier drawing
            tess.BatchSize = 100;
            tess.WarpTo(10, 400);
            koch(tess, 1, 1200);
            tess.WarpTo(10, 400);
            tess.LineBrush = Brushes.Magenta;
         //   koch(tess, 6, 1200);
            koch3(tess, 1200);
             
            DateTime t1 = DateTime.Now;
            TimeSpan ts = t1.Subtract(t0);
            this.Title = string.Format("Koch took {0} ", ts.TotalMilliseconds);
        }

        private void butClearTess_Click(object sender, RoutedEventArgs e)
        {
            tess.Clear();
        }

         private void btnSpiral_Click(object sender, RoutedEventArgs e)
        {
            Brush[] myBrushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            doSpiral(4.2, myBrushes);
        }

         private void button1_Click(object sender, RoutedEventArgs e)
        {
            ImageSource imgSrc = new BitmapImage(new Uri("pack://application:,,,/" + "lips.png"));
            ImageBrush b = new ImageBrush(imgSrc);
            Brush[] myBrushes = { b };
            doSpiral(12.5, myBrushes);
        }

         Point[] triangle = { new Point(0, 0), new Point(-15, -5), new Point(-15, 5) };

        private void doSpiral(double brushWidth, Brush[] myBrushes)
        {
         //   if (g0 == null)
         //   {
         //       g0 = tess.TurtleGeometry;
         //   }

         //   Geometry g1 = new EllipseGeometry(new Point(0,0), 20, 10);
         //   Geometry g2 = new LineGeometry(new Point(-15, -15), new Point(15, 15));
         //   GeometryGroup g3 = new GeometryGroup();
         //   g3.Children.Add(g0);
         //   g3.Children.Add(g1);

         ////   Geometry g4 = new CombinedGeometry(GeometryCombineMode.Xor, g1, g0);

         //   Geometry[] gs = { Turtle.GeometryFromPoints(triangle),
                            
         //                   g1, g2,  g3
         //                    };


            tess.Reset();
       //     tess.WantFill = true;
            tess.WarpTo(300, 300);
            tess.BrushWidth = brushWidth;
            
             
       //     tess.DelayMillisecs = 10;
            tess.BatchSize = 1;
            for (int i = 0; i < 302; i++)
            {
                if (i % 10 == 0)
                {
                    tess.Stamp();
                    tess.LineBrush = myBrushes[(i / 10) % myBrushes.Length];             
                }
                if (i == 200)
                {
             //       tess.TurtleGeometry = gs[count];
                    //count = (count + 1) % gs.Length;
                }
                //if (i % 3 == 0)
                //{
                //    tess.BrushDown = !tess.BrushDown;
                //}
                tess.Forward(i / 10);
                tess.Left(10);
                Thread.Sleep(2); // slow it down to test if it flushes nicely
            }
        }

        private void btnToggleVisibility_Click(object sender, RoutedEventArgs e)
        {
            tess.Visible = !tess.Visible;
        }


        private void drawLine(Turtle t, float x0, float y0, float x1, float y1)
        {
            t.WarpTo(x0, y0);
            t.BrushDown = true;
            t.Goto(x1, y1);
        }

        private void btnLine_Click(object sender, RoutedEventArgs e)
        {
            tess.Reset();
            tess.BrushWidth = 1;
            drawLine(tess, 100, 200, 300, 200);
            drawLine(tess, 200, 100, 200, 300);
            drawLine(alex, 20, 10, 40, 70);
            drawRectangle(tess, 150.0, 100.0);
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            tess.Reset();
            tess.Left(70);
            tess.Stamp("1. Default position of text is here...");
            tess.Stamp("2. With offsets, we move the text. ", 10.0, -24);
            tess.Stamp("3. With offsets and rotation.", 10,-24, true);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Got 1");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Got 2");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Got 3");
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = listBox1.SelectedItem.ToString();
            MessageBox.Show(s);
        }


        private void btn31_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Got 3");
        }

        private void listBox1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("DataContext changed");
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tess.DelayMillisecs = (int)slider1.Value;
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte Red = (byte)slider2.Value;
            Slide2Val.Content = Red.ToString();
            
            playground.Background =  new SolidColorBrush(Color.FromArgb(255, Red, Red, Red));
        }

        private void btnKillAlex_Click(object sender, RoutedEventArgs e)
        {
             alex.Dispose();
             alex.Dispose();
             alex.Dispose();
             alex.Dispose();
        //     alex = null;
       //     GC.Collect();
        }

        private void btnStar_Click(object sender, RoutedEventArgs e)
        {
            tess.Reset();
   
            tess.WarpTo(100, 100);
            drawStar(50);
            tess.WarpTo(0, 0);
            tess.Right(36);
            tess.Forward(12);
            tess.Left(36);
            //tess.WarpTo(110, 110);
           // drawStar(40);



        }

        private void drawStar(double sz)
        {
            for (int i = 0; i < 5; i++)
            {
                tess.Forward(sz);
                tess.Left(72);
                tess.Forward(sz);
                tess.Right(144);
                tess.Forward(sz);
                tess.Left(72);
                tess.Forward(sz);
                tess.Right(144);
            }
        }


        private void btnMadness_Click(object sender, RoutedEventArgs e)
        {
            Geometry geom = new RectangleGeometry(new Rect(0, 0, 10, 5));
            double scalingFactor = 1;
            for (int i = 0; i <= 18; i++)
            {

                Path p = new Path()  { Stroke = Brushes.Black,  StrokeThickness = 3, Data = geom,  
                                   Fill = Brushes.Red,  StrokeEndLineCap = PenLineCap.Round  };
                TransformGroup tg = new TransformGroup();              
                tg.Children.Add(new RotateTransform(i*20));  // i * 20 degrees of rotation
                scalingFactor += 0.2;
                tg.Children.Add(new ScaleTransform(scalingFactor, scalingFactor));  // grow it bigger
                p.RenderTransform = tg;
                tess.Stamp(p);
                tess.Forward(50);
            }
         }

        private void btnMoreMad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nothing was supposed to happen.");
        }

        private void btnAppearance_Click(object sender, RoutedEventArgs e)
        {
            tess.SetAppearance(new EllipseGeometry(new Point(0, 0), 20, 10), Brushes.Red, Brushes.Blue);
            tess.Forward(100);


            string[] daynames = { "Mon", "Tue", "Wed", "Thur", "Fri" };
            tess.Reset();
            tess.WarpTo(200, 200);
            for (int i = 0; i < 5; i++)   // A five-sided regular polygon is called a pentagon.
            {
                Button b = new Button();  // Make a new button.
                b.Content = daynames[i];  // Set a property.
                tess.Stamp(b);            // Stamp the button it where tess is at the moment.
                tess.Forward(100);
                tess.Left(360 / 5);
            }
        }



        //private void panel1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

        //    tess.TheBrush = new TextureBrush(Image.FromFile(@"C:\C#\Samples\Turtle\Sample1\lips.jpg"));
        //    tess.BrushWidth = 25;
        //    tess.BrushDown = false;
        //    tess.Goto(e.X, e.Y);
        //    tess.BrushDown = true;
        //}

        //private void panel1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
        //    tess.Goto(e.X, e.Y);
        //}

        //private void panel1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    tess.BrushDown = false;
        //}

        //private void btnBarChart_Click(object sender, EventArgs e)
        //{
        //    int[] xs = { 24, 57, -100, 120, -80, 130, 110 };
        //    tess.Reset();
        //    tess.TheBrush = Brushes.Purple;

        //    tess.WantFill = true;
        //    foreach (int v in xs)
        //    {
        //        tess.TheBrush = v < 0 ? Brushes.Red : Brushes.Blue;
        //        draw_bar(tess, v);
        //    }
        //    tess.WantFill = false;
        //}


        //private void draw_bar(Turtle t, int height)
        //{
        //    // Get turtle t to draw one bar, of height.  
        //    t.Left(90);
        //    t.Forward(height);     // Draw up the left side
        //    t.Write(height.ToString(), height >= 0);
        //    t.Right(90);
        //    t.Forward(40);         // Width of bar, along the top
        //    t.Right(90);
        //    t.Forward(height);     // And down again!
        //    t.Left(90);            // Put the turtle facing the way we found it.
        //    t.Forward(10);         // Leave small gap after each bar
        //}
    }
}
