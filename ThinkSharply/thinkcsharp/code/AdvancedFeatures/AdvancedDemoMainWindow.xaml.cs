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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThinkLib;

namespace AdvancedFeatures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdvancedDemoMainWindow : Window
    {
        Turtle tess, alex;

        public AdvancedDemoMainWindow()
        {
            InitializeComponent();
            tess = new Turtle(playground);
            alex = new Turtle(playground, 20, 20);
            ResetBothTurtles();

            lbDemos.ItemsSource = new string[] {
                "none", "Reset", "Stamp Geometry", "Stamp Button", "Stamp Image", "GeometryFromPoints", "DefaultShape", "ImageTurtle",
                "demo1", "Some Test Case" };
        }


        private void ResetBothTurtles()
        {
            tess.Reset();
            alex.Reset();
            alex.LineBrush = Brushes.Blue;
            alex.BodyBrush = Brushes.Blue;
            alex.Visible = false;
        }


        private void btn_demo1_Click(object sender, RoutedEventArgs e)
        {
            textHelp.Text =
@"Old: 
  The LineBrush was also used to fill the turtle's body.  
  The body outline was always black.
New: 
  There are two new brushes for the turtle's outline and filling its body.
  One useful thing is that you can make the outline brush transparent.";

            ResetBothTurtles();
            tess.Forward(100);
            tess.Stamp();
            tess.Forward(100);
            tess.OutlineBrush = Brushes.OrangeRed;   // Two new brushes
            tess.BodyBrush = Brushes.Blue;
            tess.Forward(100);
            tess.Stamp();
            tess.OutlineBrush = Brushes.Transparent;
            tess.Forward(100);
        }


        private void btnStampText_Click(object sender, RoutedEventArgs e)
        {
            textHelp.Text =
@"Old: 
  There was a 'Write' method for text.
New: 
  Now you Stamp your text.  (Rotation, fonts, offset etc. features are still available.) 
  The text now becomes a footprint, and goes into the Footprints collection.";


            ResetBothTurtles();
            tess.Left(70);
            tess.Stamp("1. Default position of text is here...");
            tess.Stamp("2. With offsets, we move the text. ", 10.0, -24);
            tess.Stamp("3. With offsets and rotation.", 10, -24, true);

            alex.Visible = true;
            alex.WarpTo(250, 20);
            alex.TextFontFamily = new FontFamily("Comic Sans MS");
            alex.TextFontSize = 22;
            alex.TextFontStyle = FontStyles.Oblique;
            alex.TextBrush = Brushes.Firebrick;
            alex.Stamp("My name is Alex.");
        }



        private void btnSetAppearance_Click(object sender, RoutedEventArgs e)
        {
            textHelp.Text =
@"Old: 
  You could change the turtle outline shape by passing in a Geometry.
New: 
  The 'ChangeAppearance()' and 'ResetAppearance()' can do all this and more.
  You pass in the Geometry and the Outline and BodyFill brushes in a single call.

  In addition, `SetFootprintOpacity' let's you control the footprint appearance.";

            ResetBothTurtles();

            tess.SetAppearance(new EllipseGeometry(new Point(0, 0), 9, 5), Brushes.Magenta, Brushes.Blue);
            tess.SetFootprintOpacity(0.4, 0.4);
            tess.Stamp();
            tess.Forward(100);
        }


        private void btnSetAppearanceII_Click(object sender, RoutedEventArgs e)
        {
            textHelp.Text = @"New:  Use an image for your turtle. "; 
            ResetBothTurtles();

            // Get the image
            ImageSource src = new BitmapImage(new Uri("pack://application:,,,/images/spider2.png"));
            tess.SetAppearance(src, 60, 90);  // (60,90) is where the spider's body segments join.
            tess.SetFootprintOpacity(0, 0.5);
            tess.Stamp();
            tess.Forward(200);
            tess.Left(70);
            tess.Stamp();
            tess.Forward(200);
        }


        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            textHelp.Text = @"New:  Some of the earlier experimental methods
  have gone away.   We can still do everything they used to do, but now we 
  use the more powerful `Stamp()` method that his new version provides. 

  You can also now get at the Turtle UIElelement and do all sorts of interesting
  WPF things: for example, blur it, or make a drop shadow, or apply an animation.";

            ResetBothTurtles();

            tess.TurtleUI.Effect = new DropShadowEffect() { Direction = -100, Color = Colors.Black, ShadowDepth = 20 };

            tess.Stamp();
            tess.Forward(100);
            tess.Left(45);

            alex.Visible = true;
            alex.TurtleUI.Effect = new BlurEffect() { Radius = 6 };
            alex.Stamp();
            alex.Forward(100);
        }


        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tess.Reset();
        }

        private void lbDemos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDemos.SelectedItem == null) return;
            switch (lbDemos.SelectedItem.ToString())
            {
                case "none":
                    return;

                case "Reset":
                    tess.Reset();
                    break;

                case "Stamp Geometry":
                    demoStampGeometry();
                    break;

                case "Stamp Image":
                    demoStampImage();
                    break;

                case "Stamp Button":
                    demoStampButton();
                    break;

                case "GeometryFromPoints":
                    demoGeometryFromPoints();
                    break;

                case "DefaultShape":
                    tess.ResetAppearance();
                    doSomeStamping();
                    break;

                case "ImageTurtle":
                    demoImageTurtle();
                    break;

                case "Some Test Case":
                    someTestCase();
                    break;
            }
        }

        private void someTestCase()
        {

            tess.Reset();

            Geometry g = new RectangleGeometry(new Rect(-20, -8, 40, 16));
            tess.WarpTo(200, 400);
            tess.BrushWidth = 3;
            tess.BodyBrush = new RadialGradientBrush(Colors.Red, Colors.Blue);
            for (int i = 0; i < 10; i++)
            {
                tess.Left(18);
                tess.Stamp(g);
                tess.Left(18);
                tess.Forward(80);
                tess.SetFootprintOpacity(i / 10.0, (10 - i) / 10.0);
            }
        }

        private void doSomeStamping()
        {
            tess.Clear();
            tess.WarpTo(300, 300);
            for (int i = 0; i < 8; i++)
            {
                tess.Stamp();
                tess.Forward(150);
                tess.Left(45);
                // change the brush colour to test that things don't go awry.
                if (i == 4)
                {
                    tess.LineBrush = Brushes.Navy;
                }
            }
        }

        int imageIndx = 0;
        string[] imageName = { "mouse.png", "Turtle.png", "Rat.png" };
        private void demoImageTurtle()
        {

            tess.Reset();
            // We can make the linebrush an ImageBrush.   Creating a different shape outline for the
            // turtle works nicely too.

            ImageSource src = new BitmapImage(new Uri("pack://application:,,,/" + imageName[imageIndx]));
            imageIndx = (imageIndx + 1) % imageName.Length;

            // make the turtle geometry into a rectangle that can hold the image exactly
            double halfWidth = src.Width / 2.0;
            double halfHeight = src.Height / 2.0;
            Geometry g = Turtle.GeometryFromPoints(new Point[] {
                new Point(-halfWidth,-halfHeight), new Point(halfWidth,-halfHeight), new Point(halfWidth,halfHeight), new Point(-halfWidth,halfHeight)
            });

            //    Geometry g = new RectangleGeometry(new Rect(new Point(-halfWidth, -halfHeight), new Point(halfWidth, halfHeight)));
            //   Geometry g = new EllipseGeometry(new Point(0, 0), halfWidth, halfHeight);

            ImageBrush brsh = new ImageBrush() { ImageSource = src };

            tess.SetAppearance(g, Brushes.Transparent, brsh);

            doSomeStamping();

        }


        private void demoGeometryFromPoints()
        {
            tess.Reset();
            tess.Forward(30);
            tess.Stamp();
            tess.Forward(30);
            Point[] pts = { new Point(-10, -10), new Point(15, 0), new Point(-10, 10), new Point(-4, 0) }; // a pointy arrowHead
            tess.SetAppearance(Turtle.GeometryFromPoints(pts), null, null);
            //     tess.TurtleUI.Opacity = 0.4;
            tess.Stamp();
            tess.Forward(30);
            tess.Stamp();
            tess.Forward(30);
            tess.Stamp();
            tess.Forward(30);

            BlurEffect be = new BlurEffect();
            be.Radius = 10;
            tess.TurtleUI.Effect = be;

            tess.TurtleUI.Effect = new DropShadowEffect() { Direction = 3, Color = Colors.Black, ShadowDepth = 20 };
        }

        private void demoStampButton()
        {
            tess.Reset();
            tess.WarpTo(200, 200);
            for (int i = 0; i < 5; i++)
            {
                Button b = new Button();
                b.Content = i.ToString();
                tess.Stamp(b);
                tess.Forward(100);
                tess.Left(360 / 5);
            }
        }

        private void demoStampGeometry()
        {
            tess.Reset();

            Geometry g = new RectangleGeometry(new Rect(-20, -8, 40, 16));
            tess.WarpTo(200, 400);
            tess.BrushWidth = 3;   // interesting... rather than pick up brush
            tess.FillBrush = new RadialGradientBrush(Colors.Red, Colors.Blue);
            for (int i = 0; i < 10; i++)
            {
                tess.Left(18);
                tess.Filling = true;
                tess.Stamp(g);
                tess.Filling = false;
                tess.Left(18);
                tess.Forward(80);
                tess.SetFootprintOpacity(i / 10.0, (10 - i) / 10.0);
            }
        }

        private void demoStampImage()
        {
            tess.Reset();
            ImageSource imgSrc = new BitmapImage(new Uri("pack://application:,,,/" + "Images/ladybug.png"));
            for (int i = 0; i < 3; i++)
            {
                tess.Stamp(imgSrc, imgSrc.Width/2, imgSrc.Height/2, true);
                tess.Forward(100);
                tess.Left(45);
            }
        }

    }
}