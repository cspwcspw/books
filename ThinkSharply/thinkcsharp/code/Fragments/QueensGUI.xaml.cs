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
using System.Windows.Shapes;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for QueensGUI.xaml
    /// </summary>
    public partial class QueensGUI : Window
    {
        private List<List<Rectangle>> rects; // Define a reference to a list of lists   
        private List<Image> queenImages;     // Define a reference to a list
        private int[] theBoard;              // Our own reference to the caller's board
        private int N;                       // Number of squares on each edge, eg 8 

        public QueensGUI(int[] board)
        {
            InitializeComponent();           // Initialize all the bits defined in XAML
            theBoard = board;                // Save the reference for later use
            N = theBoard.Length;             // Save board size as class-level variable
            rects = createRects();           // Create the NxN rectangles
            queenImages = createQueenImages();  // Create the N queen images
        }

        private List<Image> createQueenImages()
        {
            List<Image> result = new List<Image>();   // Create the List
            // Fetch the bitmap that we're going to use
            BitmapImage bm =
                 new BitmapImage(new Uri("pack://application:,,,/Queen_of_Hearts.png"));

            for (int i = 0; i < N; i++)
            {
                Image im = new Image();     // Create the WPF control
                im.Source = bm;             // Tell it what image to display
                canvas1.Children.Add(im);   // Add the control to the canvas               
                result.Add(im);             // And remember a reference to it.
            }
            return result;
        }


        private List<List<Rectangle>> createRects()
        {
            List<List<Rectangle>> result = new List<List<Rectangle>>();
            Brush[] bs = { Brushes.Red, Brushes.Blue };
            for (int row = 0; row < N; row++)
            {
                List<Rectangle> thisRow = new List<Rectangle>();
                int whichBrush = row % 2;
                for (int col = 0; col < N; col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.HorizontalAlignment = HorizontalAlignment.Left;
                    rect.VerticalAlignment = VerticalAlignment.Top;
                    rect.Fill = bs[whichBrush];
                    canvas1.Children.Add(rect);
                    thisRow.Add(rect);
                    whichBrush = (whichBrush + 1) % 2;
                }
                result.Add(thisRow);
            }
            return result;
        }

        public void RefreshQueenPositions()
        {
            layoutQueens();
            // Magic spell to force the GUI to get updated immediately
            Dispatcher.Invoke((Action)delegate { }, System.Windows.Threading.DispatcherPriority.SystemIdle);
        }

        private void layoutRectangles()
        {
            double side = Math.Min(canvas1.ActualWidth, canvas1.ActualHeight);
            double rectSz = side / N;

            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < N; col++)
                {
                    Rectangle rect = rects[row][col];
                    Canvas.SetLeft(rect, rectSz * col + 0.05 * rectSz);
                    Canvas.SetTop(rect, rectSz * row + 0.05 * rectSz);
                    rect.Width = rectSz - 0.10 * rectSz;
                    rect.Height = rectSz - 0.10 * rectSz;
                }
            }
        }

        private void layoutQueens()
        {
            double side = Math.Min(canvas1.ActualWidth, canvas1.ActualHeight);
            double rectSz = side / N;
            // Lay out the queens
            for (int i = 0; i < N; i++)
            {
                Image q = queenImages[i];
                q.Width = rectSz;
                q.Height = rectSz;
                Canvas.SetLeft(q, i * rectSz);
                Canvas.SetTop(q, theBoard[i] * rectSz);
            }
        }

        private void canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            layoutRectangles();
            layoutQueens();
        }
    }
}