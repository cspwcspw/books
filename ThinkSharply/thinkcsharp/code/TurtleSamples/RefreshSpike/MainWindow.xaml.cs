using System.Windows;
using System.Windows.Threading;
 

namespace ThinkLib
{
 
    // Some of these programs serve no purpose other than as a place to try things out.
    // This is one of those, trying to see how we can get the debugger to play 
    // nicely with the turtle.    An ongoing issue with the package is that the GUI
    // is not updated at high-enough priority, and we have all sorts of ugly hacks 
    // to work around that.  
    
    // In an ideal world, what I want (but have not yet got) is 
    //   a) Consistency between the student's view of execution and what appears on the playground,
    //   b) so if we're finished executing tess.Forward(100); tess.Turn(90), 
    //      the line and her new orientation must be up to date on the GUI. 
    //   c) even, and especially when, the debugger has single-stepped over these instructions.
    //   d) I want single-threaded GUI events.  My attempt to solve a, b, and c means that I have
    //      comprised (d) - it is possible to click "drawFractal" and get the event serviced
    //      even though the last "drawFractal" is still busy drawing!  This makes it
    //      almost as bad as the Python version with similar kinds of problems. 

    public partial class MainWindow : Window
    {

       Point currTurtlePos;
       int currTurtleDirection;  // 0=East, 1=South, etc, clockwise
     
        
        public MainWindow()
        {
            InitializeComponent();
            btnReset_Click(null, null);
        }


        private void btnReset_Click(object sender, RoutedEventArgs e)
        {   // Discard any points added recently
            while (thePolyLine.Points.Count > 2)
            {
                thePolyLine.Points.RemoveAt(2);
            }
            // Start the turtle at the lower end of the existing blue line.
            currTurtlePos = thePolyLine.Points[1];
            currTurtleDirection = 0; // East

            Dispatcher.CurrentDispatcher.Thread.Priority = System.Threading.ThreadPriority.Lowest;

        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            drawNotQuiteSquare(100);
        }

        private void drawNotQuiteSquare(double sz)
        {
            for (int i = 0; i < 5; i++)
            {
                forward(sz / 2, true);    // to the east      
                forward(sz, true);        // down
                forward(sz, true);        // to the west
                forward(sz * 1.1, true);    // up
                forward(sz * 0.6, false);  // to the east again  
            }
        }

        private void turnRight()
        {
            currTurtleDirection = (currTurtleDirection + 1) % 4;

        }

        private void forward(double distance, bool wantTurn)
        {
            switch (currTurtleDirection)
            {
                case 0:
                    currTurtlePos.X += distance;
                    break;
                case 1:
                    currTurtlePos.Y += distance;
                    break;
                case 2:
                    currTurtlePos.X -= distance;
                    break;
                case 3:
                    currTurtlePos.Y -= distance;
                    break;
            }
            // Now extend the polyLineSeg
            thePolyLine.Points.Add(currTurtlePos);

            if (wantTurn) turnRight();
            // Call a stub to update the GUI
             forceGUIupdate_version3();
            // slow things down to show that the flushing of the GUI is working nicely!
             System.Threading.Thread.Sleep(100);
        }


        private void forceGUIupdate_version3()
        {
          //  Form f2 = new PopUp();
         //   f2.ShowDialog();
            PopUp f2 = new PopUp();
            f2.Owner = this;
            bool? b = f2.ShowDialog();
        }
    }
}


#if false
        private void forceGUIupdate_version1()
        {
            System.Threading.Thread.Sleep(10);
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                                         new DispatcherOperationCallback(ExitFrame), frame);
            // Start a new message pump which will flush pending GUI updates
            Dispatcher.PushFrame(frame);
            System.Threading.Thread.Sleep(10);
        }

        public object ExitFrame(object f)
        {
            System.Threading.Thread.Sleep(10); 
            ((DispatcherFrame)f).Continue = false;   // Tell the new message pump to exit
            return null;
        }
        
        // Call my empty delegate before proceeding.  But put this call in the dispatcher's queue at a 
        // lower priority than the renderer, so the renderer will be made to do it's thing before we
        // can continue doing our thing.  The priorities are explained at 
        //   http://msdn.microsoft.com/en-us/magazine/cc163328.aspx 
        private Action emptyDelegate = delegate { System.Threading.Thread.Sleep(10); };

        private void forceGUIupdate_version2()
        {
     
        //    System.Threading.Thread.Sleep(10);
        //    playground.Dispatcher.Invoke(emptyDelegate, System.Windows.Threading.DispatcherPriority.SystemIdle);
        }

#endif
 