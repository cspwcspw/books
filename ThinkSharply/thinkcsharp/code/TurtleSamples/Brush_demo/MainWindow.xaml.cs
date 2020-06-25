using System;
using System.Collections.Generic;
//using System.Linq;
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

namespace Brush_demo
{
    /// <summary>
    /// This is just some mess-around spike to see what we can do... 
    /// </summary>
    public partial class MainWindow : Window
    {  
       

        Turtle tess;
        public MainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground);
            tess.BrushWidth = 30;

        }

        private void btnTest1_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bird  = new BitmapImage
              (new Uri("pack://application:,,,/" + "bird.png"));

            ImageBrush brsh = new ImageBrush(bird);

            brsh.ViewportUnits = BrushMappingMode.Absolute;
            brsh.Viewport = new Rect(0, 0, bird.Width, bird.Height);
            brsh.TileMode = TileMode.Tile;

            tess.LineBrush = brsh;
        }

        private void btnSetVisualBrush_Click(object sender, RoutedEventArgs e)
        {
            VisualBrush brsh = new VisualBrush(button1);

            brsh.ViewportUnits = BrushMappingMode.Absolute;
            brsh.Viewport = new Rect(0, 0, button1.Width, button1.Height);
            brsh.TileMode = TileMode.Tile;

            tess.LineBrush = brsh;
        }

        int count = 0;

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pt = e.GetPosition(playground);
                tess.Goto(pt);
            }
            else
            {

                Point pt = e.GetPosition(playground);
                Color c = getColorOnCanvasBackground(playground, pt);
                this.Title = string.Format("Color = {0} ", c);
            }
        }


        public static Color getColorOnCanvasBackground(Canvas cvs, Point pt)
        {
             
            // So this does not work and I can only do it if the canvas has
            // an image background.

            Color c = Colors.White;

            if (pt.X < 0 || pt.X >= cvs.ActualWidth) return c;
            if (pt.Y < 0 || pt.Y >= cvs.ActualHeight) return c;

            SolidColorBrush scb = cvs.Background as SolidColorBrush;
            if (scb != null)
            {
                return scb.Color;
            }

            //GradientBrush gb = cvs.Background as GradientBrush;
            //if (gb != null)
            //{
            //   Freezable fz = gb.GetAsFrozen();
            //   GradientStopCollection gsc = gb.GradientStops;
               
            //    return gb.Color;
            //}

            ImageBrush b = cvs.Background as ImageBrush;
            if (b == null) return c;
            BitmapSource source = b.ImageSource as BitmapSource;
            if (b == null) return c;

            if (source != null)
            {
                int x = (int)(pt.X * source.PixelWidth / cvs.RenderSize.Width);
                int y = (int)(pt.Y * source.PixelHeight / cvs.RenderSize.Height);

                if (x < 0 || y < 0 || x >= source.PixelWidth || y >= source.PixelHeight) return c;
                try
                {
                    CroppedBitmap cb = new CroppedBitmap(source, new Int32Rect(x, y, 1, 1));
                    var pixels = new byte[4];
                    cb.CopyPixels(pixels, 4, 0);
                    c = Color.FromRgb(pixels[2], pixels[1], pixels[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //  Console.WriteLine(ex.Message);

                }
            }
            return c;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            tess.BrushDown = false;
            for (int i = 0; i < 30; i++)
            {
                tess.Goto(i * 20, 200 -i );
                tess.Stamp();
            }
            tess.BrushDown = true;
        }
 
    }
}
