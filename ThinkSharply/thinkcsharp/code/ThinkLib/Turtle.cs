
#region usings
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

#endregion

// Pete Wentworth, 2013, last revised August 2015.  Part of the book "Think Sharply with C#". 

namespace ThinkLib
{
    /// <summary>
    ///  This class encapsulates a Logo-like turtle with members like Visible, BrushDown, Brush, etc.
    ///  As the turtle goes about its business it leaves tracks and footprints in the playground.
    ///  
    ///  We use WPF's retained-mode graphics system with objects that represent each footprint or polyline.
    ///  The playground is a Canvas: its Children are UIElements, so for rendering purposes we just
    ///  make each new footprint or Path a new Child of the playground canvas.
    ///  
    ///  Drawing sequences (the trail the turtle leaves) are represented as Path objects (a Path isa UIElement). 
    ///  The turtle can also Stamp footprints, text, images and other UIElements onto the playground.  
    ///  We keep local lists of all the UIElements that this particular turtle has created 
    ///  so that we can Clear or Reset the turtle or selectively erase its footprints, etc.  
    ///  This also happens automatically when the turtle is Disposed.
    ///  
    ///  Other turtles can also play in the playground, and other UIElements can also exist independently
    ///  on the playground canvas.   This package provides no help for managing those.  
    ///  
    ///  But we can also Stamp a UIElement onto the playground via a turtle.  The stamped element then 
    ///  becomes a Footprint owned by the turtle (for purposes of its lifetime management), and additionally 
    ///  will be oriented / rotated / positioned etc. according to the turtle's current heading and position.
    /// 
    ///  The turtle itself is also represented on the GUI as a UIElement.
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough] // Ensure a student doesn't inadvertently step into these innards...
    public class Turtle: IDisposable
    {

        #region UI elements

        private List<UIElement> myUIElems;         // Renders all the turtle drawing (lines) 

        /// <summary>
        /// Get a collection of all the footprints the turtle currently has on the canvas.
        /// The collection can act as a List of Footprint, so you can do all List operations
        /// such as indexing, or enumerate the collection with a foreach loop. 
        /// The element at position [0] is the oldest footprint.  
        /// This is a reference type, so the user can remove 
        /// footprints, or clear all or part of the collection.  
        /// Footprints are automatically added when any of the Stamp methods are called.
        /// </summary>
        public FootprintCollection Footprints { get; private set; }

        // Experimental: expose this publically so that the user might be able to attach an animation
        public Path TurtleUI { get; private set; }

        // As the turtle moves with Forward or Goto, the new line segment is added here.
        // In general, the turtle's patterns will be a list of these segments.
        private PolyLineSegment currentPolyLineSeg = null;

        #endregion

        #region Fields and Properties

        /// <summary>
        /// The release date of this version of ThinkLib.
        /// </summary>
        public const string VersionDate = "8 August 2015";

        Canvas playground;            // The parent canvas.  All our UI elements are children of this canvas.

        const int backlayer = 0;      // default turtle brush and fills live here, on background Z layer.
        const int footprintlayer = 5; // footprints live on a layer above that.
        const int turtlelayer = 10;   // The turtle is always at the front of the Z-order.

        /// <summary>
        /// The turtle's home position. 
        /// </summary>
        public Point Home { get; set; }

        private Point position;

        /// <summary>
        /// Direct assignment to Position is equivalent to Goto(x, y) - i.e. a line is drawn if BrushDown.
        /// </summary>
        public Point Position
        {
            get
            {
                return position;
            }

            set
            {
                if (BrushDown)
                {
                    if (currentPolyLineSeg == null)
                    {
                        currentPolyLineSeg = startNewFragment();
                    }
                    currentPolyLineSeg.Points.Add(value);
                 }
                else
                {
                    closeCurrentFragment();
                }
                position = value;
                updateTurtleUI();
                pumpAnimation();
            }
        }

        internal double heading = 0;
        /// <summary>
        /// Heading, in degrees. 0 is facing East.  Positive angles rotate clockwise.
        /// </summary>
        public double Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                updateTurtleUI();
                pumpAnimation();
            }
        }

        private Brush lineBrush;   
        private Brush fillBrush = null;
        private Brush outlineBrush;      // The outline of the turtle's shape
        private Brush bodyBrush;         // How to paint the interior of the turtle

        private double footprintOutlineOpacity = 0.7;   // Two variables that control how we render footprints.
        private double footprintBodyOpacity = 0.1;

        /// <summary>
        /// The brush used for stamping text
        /// </summary>
        public Brush TextBrush { get; set; }
        
        /// <summary>
        /// The font used for stamping text
        /// </summary>
        public FontFamily TextFontFamily  { get; set; }
        
        /// <summary>
        /// The font size used for stamping text
        /// </summary>
        public double TextFontSize { get; set; } 

        /// <summary>
        /// The font style used for stamping text
        /// </summary>
        public FontStyle TextFontStyle { get; set; }

        /// <summary>
        /// The font weight used for stamping text
        /// </summary>
        public FontWeight TextFontWeight { get; set; } 

        /// <summary>
        /// The brush used for drawing the turtle's lines.
        /// </summary>
        public Brush LineBrush
        {
            get { return lineBrush; }
            set
            {
                lineBrush = value;
                // Whoops!   
                //Path tPath = TurtleUI as Path;
                //if (tPath != null)
                //{
                //    tPath.Fill = lineBrush;
                //}
                closeCurrentFragment();
                pumpAnimation();
            }
        }

        /// <summary>
        /// The brush used for drawing the turtle's outline shape.
        /// </summary>
        public Brush OutlineBrush
        {
            get { return outlineBrush; }
            set
            {
                outlineBrush = value;
                Path tPath = TurtleUI as Path;
                if (tPath != null)
                {
                    tPath.Stroke = outlineBrush;
                }
                closeCurrentFragment();
                pumpAnimation();
            }
        }

        /// <summary>
        /// The brush used for drawing the interior of the turtle.
        /// </summary>
        public Brush BodyBrush
        {
            get { return bodyBrush; }
            set
            {
                bodyBrush = value;
                Path tBody = TurtleUI as Path;

                //Debug.Assert(tBody != null);
                //if (tBody != null)
                //{
                    tBody.Fill = bodyBrush;
                //}
                closeCurrentFragment();
                pumpAnimation();
            }
        }

        /// <summary>
        /// Used for filling the interior of the multi-segment path drawn by the turtle. 
        /// </summary>
        public Brush FillBrush
        {
            get { return  fillBrush; }
            set
            {
                fillBrush = value;
                closeCurrentFragment();
            }
        }

        private double brushWidth = 1;
        /// <summary>
        /// Set the width of the brush strokes that the turtle leaves behind.
        /// </summary>
        public double BrushWidth
        {
            get { return brushWidth; }
            set
            {
                brushWidth = value;
                closeCurrentFragment();
            }
        }

        internal bool visible = true;
        /// <summary>
        ///  Make the turtle visible or invisible.  This does not affect drawing or footprints.
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                TurtleUI.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
                pumpAnimation();
            }
        }

        internal bool filling = false;
        /// <summary>
        /// Turn filling on or off. What this means is determined by the fill algorithms of Microsoft's geometries.  
        /// This can be quite a lot of fun.  
        /// </summary>
        public bool Filling
        {
            get { return filling; }
            set
            {
                if (filling != value)
                {
                    filling = value;
                    closeCurrentFragment();
                }
            }
        }

        bool brushDown = true;
        /// <summary>
        /// When true, the brush is down and the turtle draws when it moves.  Even when the 
        /// brush is up, we can stamp footprints.
        /// </summary>
        public bool BrushDown
        {
            get
            {
                return brushDown;
            }
            set
            {
                if (value && !brushDown)  // is the pen transitioning down?
                {
                    brushDown = value;
                    closeCurrentFragment();
                }
                brushDown = value;
            }
        }

        /// <summary>
        /// Returns a Color of the pixel in the bitmap under the turtle.  This only works
        /// if the background of the parent Canvas is a bitmap image.  In all other cases
        /// (solid color canvas, gradient fills, etc.) the method will return Color.Transparent. 
        /// </summary>
        public Color ColorUnderTurtle
        {
            get
            {
                return ColorOfBackgroundAt(Position);
            }
        }
 
        Geometry turtleGeometry;
        /// <summary>
        /// Get or set the Geometry that defines the shape of the turtle.
        /// </summary>
        private Geometry TurtleGeometry
        {
            get { return turtleGeometry; }
            set
            {
                turtleGeometry = value;
                TurtleUI.Data = turtleGeometry;
                pumpAnimation();
            }
        }

        private static PathGeometry DefaultTurtleGeometry
        {
            get
            {
                // Set up default turtle geometry ...
                Point[] turtleShapePoints = new Point[]
                { new Point(8,9), new Point(5,6), new Point(1,7), new Point(-3,5), 
                  new Point(-6,8), new Point(-8,6), new Point(-5,4), new Point(-7,0), 
                  new Point(-5,-4), new Point(-8,-6), new Point(-5,-8), new Point(-3,-5), 
                  new Point(1,-7), new Point(5,-6), new Point(8,-9), new Point(9,-7), 
                  new Point(7,-4), new Point(10,-1), new Point(14,-4), new Point(17,0), 
                  new Point(14,4), new Point(10,1), new Point(7,4), new Point(9,7), new Point(8,9) };

                return GeometryFromPoints(turtleShapePoints);
            }
        }

        #endregion

        #region Constructor, Dispose, Destructor, initialization and some useful (private) utility methods.

        /// <summary>
        /// Instantiate (create) a new turtle with the specifed parent Canvas, and the specified Home position.
        /// </summary>
        /// <param name="playground">The Canvas control that is the parent of the new turtle.</param>
        /// <param name="homeX">Sets the X coordinate of the Home position of this turtle.</param>
        /// <param name="homeY">Sets the Y coordinate of the Home position of this turtle.</param>
        public Turtle(Canvas playground, double homeX = 20.0, double homeY = 200.0)
        {
            this.playground = playground;
            Footprints = new FootprintCollection(playground);
            Home = new Point(homeX, homeY);
            myUIElems = new List<UIElement>();

            Reset();
            updateTurtleUI();
        }

        #region Disposing and Destructor for the class.

        // Here we make sure that footprints, lines and the GUI elements in the
        // playground are removed.

        /// <summary>
        /// Remove the turtle and its lines and footprints from its playground.
        /// This is implicitly called if the system garbage collects the turtle.
        /// </summary>
        public void Dispose()
        {
            if (TurtleUI != null)
            {
                this.Clear();
                playground.Children.Remove(TurtleUI);
                TurtleUI = null;
            }
        }

        ~Turtle()
        {   // If this object is being garbage collected, force a Dispose() first if
            // the user didn't explicitly do so.   Because dispose is fiddling with UIElements in
            // the playground, we have a cross-threading issue.  So we have to 
            // invoke Dispose on the UI playground's own UI thread.    
            if (TurtleUI != null)
            {
                Action disposeDelegate = delegate { this.Dispose(); };
                playground.Dispatcher.Invoke(disposeDelegate);           
            }
        }

        #endregion

        void updateTurtleUI()
        {
            TurtleUI.RenderTransform = buildCurrentTurtleTransform();
        }

        private TransformGroup buildCurrentTurtleTransform()
        {
            TransformGroup tfg = new TransformGroup();
            tfg.Children.Add(new RotateTransform(heading));
            tfg.Children.Add(new TranslateTransform(Position.X, Position.Y));
            return tfg;
        }

        private Path createDefaultTurtleUI(Brush outline, Brush bodyFill)
        {   // This is used for creating the turtle UI, and for creating each footprint.
            return new Path() { Stroke = outline, StrokeThickness = 1, Data = TurtleGeometry, Fill = bodyFill };
        }

        private double toRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private void closeCurrentFragment()
        {
            currentPolyLineSeg = null;
        }

        private PolyLineSegment startNewFragment()
        {
            // Create the new Path with current properties.  This is called from only one place in one specific situation:
            // a new Position is being set, the brush is down, and there is no prior currentPolyLineSeg to add the new segment to.
            PolyLineSegment newPls = new PolyLineSegment();

         //   newPls.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            PathSegmentCollection segs = new PathSegmentCollection();
            segs.Add(newPls);

            PathFigure fig = new PathFigure() { Segments = segs, StartPoint = Position };
            PathFigureCollection figs = new PathFigureCollection();
            figs.Add(fig);

            PathGeometry geom = new PathGeometry() { Figures = figs };
            Path p = new Path()
            {
                Stroke = lineBrush,
                StrokeThickness = brushWidth,
                Data = geom,
                Fill = filling ? fillBrush : null,
                StrokeLineJoin = PenLineJoin.Round
            };

            // Default StrokeLineJoin Mitre has issues when corner is 180 degrees, Bevel leaves a hole  
            // At brushWidth 20 this is a disaster with Mitre set
            // tess.WarpTo(120, 120);
            // square(60);
            // tess.Left(180.0);
            // tess.Forward(60);                    
                    
            playground.Children.Add(p);
            myUIElems.Add(p);
            return newPls;
        }

        private void Turn(double degrees)
        {
            heading = normalize(heading + degrees);
            updateTurtleUI();
            pumpAnimation();
        }

        private double normalize(double p)
        {
            while (p < 0)
            {
                p += 360;
            }
            while (p >= 360)
            {
                p -= 360;
            }
            return p;
        }

        #endregion

        #region Control for flushing drawing to playground

        // This is the dirtiest corner of this package.
        // 
        // In an ideal world, what I want (but have not yet got) is 
        //   a) Consistency between the student's view of execution and what appears on the playground,
        //   b) so if we're finished executing tess.Forward(100); tess.Turn(90), 
        //      the line and her new orientation must be up to date on the GUI. 
        //   c) even, and especially when, the debugger has single-stepped over these instructions.
        //   d) I want single-threaded GUI events.  My attempt to solve a, b, and c means that I have
        //      comprised (d) - it is possible to click "drawFractal" and get the event serviced
        //      even though the last "drawFractal" is still busy drawing!  This makes it
        //      almost as bad as the Python version with similar kinds of problems. 


        /// <summary>
        /// This controls the maximum number of turtle moves
        /// before we force the playground to render.  When set to a large number you 
        /// will get good speed, but your rendering will be done in jerky bursts.
        /// A value of 0 means "never flush --- fastest spped."  But the canvas will 
        /// only update when when the whole computation completes and the application 
        /// becomes idle.   
        /// When set to 1, (the default) you will flush the partial work after 
        /// every drawing, turn or colour-change command.
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// Delay this number of milliseconds between each turtle drawing step.  The default, 0 is fastest.
        /// Use this property to slow the turtle down if you want to watch the drawing pattern appear gradually
        /// on the canvas.
        /// </summary>
        public int DelayMillisecs { get; set; }
        

        int drawCountSinceLastFlush = 0;
        void pumpAnimation()
        {   // There are two issues:  in a long-running program we won't get automatic rendering updates,
            // so we count calls and when we get up to BatchSize we force intermediate rendering.
            // And then we sometimes want to watch the turtles draw slowly, to watch the pretty
            // mazes and fractals as they evolve.  So we can delay after every turtle call.  

            if (BatchSize != 0 && ++drawCountSinceLastFlush >= BatchSize)
            {
                FlushToPlayground();
            }
            if (DelayMillisecs > 0)
            {
                System.Threading.Thread.Sleep(DelayMillisecs);
            }
        }

        DateTime lastFlushCallTimestamp = DateTime.Now;
        /// <summary>
        /// The user can force drawing to be rendered by calling this method.
        /// </summary>
        public void FlushToPlayground()
        {
            // Force the canvas to render now...
            drawCountSinceLastFlush = 0;

            // Now some overly complicated logic that works most of the time ...
            // WPF has no reliable way (that I can find) to force the render thread to synchronize 
            // with the GUI thread, so this is a bunch of less-than-ideal tradeoffs.

            // We're trying to achieve 3 things:   
            // a) have the turtle rendering 100% up-to-date when single-stepping in the debugger,
            // b) flush the drawing quite efficiently (if possible)
            // c) Don't get into a deadlock. 


            // If it has been a longer-than-we-expected time since we last ran this code, 
            // we'll assume the user could be doing stuff in the debugger  ...
            // Pity the debugger is unobservable from the client it is debugging - I'd really
            // like to be able to ask whether a pending breakpoint is set, or not ...

            // Hack #1:  do we think we're single-stepping? 
            DateTime t1 = DateTime.Now;
            TimeSpan ts = t1.Subtract(lastFlushCallTimestamp);
            lastFlushCallTimestamp = t1;
            bool weThinkThisMightBeSingleStepping = (ts.TotalMilliseconds - DelayMillisecs) > 200.0;


            if (weThinkThisMightBeSingleStepping)
            {
                Window someActiveWindow = findAnyActiveWindow();
                if (someActiveWindow != null)
                {
                    // Force a flushing workaround that seems to always work.  This workaround is about 300 times slower 
                    // than the "works occasionally, but not so good while debugging" method under the "else" clause ...

                    // So the general strategy here is to use the "fast but sometimes flakey" method if calls are 
                    // occuring frequently (the user can't press the debugger F10 key that fast!) and to use this
                    // more reliable variant if we think debugging might be happening.

                    // Hack #2
                    // The popup window is actually transparent and dismisses itself immediately it has been shown.
                    // But the "show a new modal / dialog window now" seems to contain the right magic to make WPF 
                    // force our own window paint first.
                    xPopUp dummyForm = new xPopUp();
                    dummyForm.Owner = someActiveWindow;
                    dummyForm.ShowDialog();
                }
                else
                {
                    playground.Dispatcher.Invoke((Action)delegate { }, TimeSpan.FromMilliseconds(30), System.Windows.Threading.DispatcherPriority.SystemIdle);
                }
            }
            else
            {   // Hack #3
                // Call an empty delegate before proceeding.  But put this call in the dispatcher's queue at a 
                // lower priority than the renderer, so the renderer will be made to do it's thing before we
                // can continue doing our thing.  The priorities are explained at 
                //   http://msdn.microsoft.com/en-us/magazine/cc163328.aspx 

                // Additionally, this Invoke can butt heads against a Win32 window resize event,
                // leading to deadlock.  Which is why we have a timeout.

                playground.Dispatcher.Invoke((Action)delegate { }, TimeSpan.FromMilliseconds(30), System.Windows.Threading.DispatcherPriority.SystemIdle);
            }

            // And, unsolved problem:  with this messy logic in place, the user can click 
            // "drawFractal" on the GUI before the previous "drawFractal" has completed.   Ouch!
        }

        private static Window findAnyActiveWindow()
        {
            // Previously, we just used the MainWindow.  But then ...
            // Vongai's bug.  She created secondary windows and killed the MainWindow, so we got a null exception.
            // Fix: rather then depend on the MainWindow being alive, let's find any active window that we can use 
            // as the "host" for this popup.
            Window someActiveWindow = null;
            foreach (Window w in Application.Current.Windows)
            {
                if (w.IsActive)
                {
                    someActiveWindow = w;
                    break;
                }
            }
            return someActiveWindow;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Move forward by the distance.  If the brush is down, a line is drawn.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public virtual void Forward(double distance)
        {
            double theta = toRadians(heading);
            double dx = (distance * Math.Cos(theta));
            double dy = (distance * Math.Sin(theta));
            Goto(Position.X + dx, Position.Y + dy); 
       }

        /// <summary>
        /// Turn the turtle to its left.
        /// </summary>
        /// <param name="degrees">Angle, in degrees</param>
        public virtual void Left(double degrees)
        {
            Turn(-degrees);
        }

        /// <summary>
        /// Turn the turtle to its right
        /// </summary>
        /// <param name="degrees"></param>
        public virtual void Right(double degrees)
        {
            Turn(degrees);
        }

        /// <summary>
        /// Go to absolute position (x,y) without drawing. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void WarpTo(double x, double y)
        {
            WarpTo(new Point(x, y));
        }

        /// <summary>
        /// Go to absolute position pos without drawing.  
        /// </summary>
        /// <param name="pos"></param>
        public virtual void WarpTo(Point pos)
        {
            if (brushDown)
            {
                BrushDown = false;
                Position = pos;
                BrushDown = true;
            }
            else
            {
                Position = pos;
            }    
        }

         /// <summary>
        /// Go to absolute position (x,y). Draw if the brush is down.  Equivalent to setting the turtle Position property.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void Goto(double x, double y)
        {
          //  Console.WriteLine("Going to {0}, {1}", x, y); 
            Position = new Point(x, y);
        }

        /// <summary>
        /// Go to absolute position Pos. Draw if the brush is down.
        /// </summary>
        /// <param name="pos"></param>
        public virtual void Goto(Point pos)
        {
            Position = pos;
        }

        /// <summary>
        /// Erase all this turtle's drawing, footprints and text without changing position or heading, or any properties.
        /// </summary>
        public virtual void Clear()
        {
            foreach (UIElement p in myUIElems)
            {
                playground.Children.Remove(p);
            }
            myUIElems.Clear();

            Footprints.Clear();
            closeCurrentFragment();
            pumpAnimation();
        }

        /// <summary>
        /// Clear all this turtle's drawing, footprints, and text. Set position to Home, heading to 0, make it visible, 
        /// and set properties such as the BrushDown, TextFonts, Filling, and all Brush colours to their defaults.  
        /// 
        /// </summary>
        public virtual void Reset()
        {
            if (TurtleUI != null)
            {
                playground.Children.Remove(TurtleUI);  // Destroy the old turtle's path if it exists.
            }

            // Now make a new one 
            turtleGeometry = Turtle.DefaultTurtleGeometry;
            SetFootprintOpacity();
            outlineBrush = Brushes.Black;
            bodyBrush = Brushes.Magenta;

            // Create the UI widget for the turtle and add it into the playground
            TurtleUI = createDefaultTurtleUI(outlineBrush, bodyBrush);
            Canvas.SetZIndex(TurtleUI, turtlelayer);
            playground.Children.Add(TurtleUI);
          
            // Hack here.  Create a default temporary label and steal its text properties.   
            Label tempLbl = new Label();
            int lblIndx = playground.Children.Add(tempLbl); // to allow it to inherit 

            TextFontStyle = tempLbl.FontStyle;        // Steal, steal, steal...
            TextFontWeight = tempLbl.FontWeight; 
            TextFontFamily = tempLbl.FontFamily;
            TextFontSize = tempLbl.FontSize;
            TextBrush = tempLbl.Foreground.Clone();

            playground.Children.RemoveAt(lblIndx);    // Now throw away the label.
  
            // Sort out some colours, etc. 
            LineBrush = Brushes.Magenta;
            BrushWidth = 1;
            FillBrush = Brushes.LightGray;
            Filling = false;
      
            ResetAppearance();
            SetFootprintOpacity();

            WarpTo(Home);
            Clear();
            heading = 0;
            Visible = true;
            BrushDown = true;
            BatchSize = 1;
            DelayMillisecs = 0;

            // We're all set, let's go.
            updateTurtleUI();
            pumpAnimation();
        }


        /// <summary>
        /// Change the appearance of this turtle.  
        /// </summary>
        /// <param name="outlineShape">The outline shape of the turtle. The origin (0,0) of the geometry defines the "hot-spot" 
        /// for positioning the turtle. If null is passed, the current TurtleGeometry is not changed.</param>
        /// <param name="outlineBrush">The brush used to paint the outline. If null is passed, the current OutlineBrush is not 
        /// changed.  Brushes.Transparent effectively suppresses drawing the outline shape.</param>
        /// <param name="bodyBrush">The brush used to fill the interior.  If null is passed, the current BodyBrush is not changed.</param>
        public void SetAppearance(Geometry outlineShape, Brush outlineBrush, Brush bodyBrush)
        {
            if (outlineShape != null) 
                TurtleGeometry = outlineShape;
            if (outlineBrush != null) 
                OutlineBrush = outlineBrush;
            if (bodyBrush != null) 
                BodyBrush = bodyBrush;
        }

        /// <summary>
        /// Set the turtle's appearance to an image. 
        /// </summary>
        /// <param name="imgSrc"></param>
        /// <param name="hotspotX">The X coordinate within the image that will define the turtle's position</param>
        /// <param name="hotspotY">The Y coordinate within the image that will define the turtle's position</param>
        public void SetAppearance(ImageSource imgSrc, double hotspotX, double hotspotY)
        {
            ImageBrush brsh = new ImageBrush() { ImageSource = imgSrc };   // Make the brush to fill with.

            // Now sort out the geometry of the frame that will hold the image: We'll use a rectangle 
            Size sz = new Size(imgSrc.Width, imgSrc.Height);   // our rectangle size can be the image size.

            Point topLeftOffset = new Point(-hotspotX, -hotspotY);
            // New create the rectangle with my (0,0) where I want it.
            Geometry g = new RectangleGeometry(new Rect(topLeftOffset, sz));

            // We're ready ...
            SetAppearance(g, Brushes.Transparent, brsh);
        }


        /// <summary>
        /// Reset the turtle's appearance to its default shape, outline brush, and body brush.
        /// </summary>
        public void ResetAppearance()
        {
            SetAppearance(DefaultTurtleGeometry, Brushes.Black, Brushes.Magenta);
        }


        /// <summary>
        /// Given a list of points, return a WPF PathGeometry.  This is a helper method
        /// that can be used to create a simple Geometry composed of straight lines.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PathGeometry GeometryFromPoints(Point[] points)
        {
            PolyLineSegment pls = new PolyLineSegment();
            foreach (Point p in points)
            {
                pls.Points.Add(p);
            }

            PathSegmentCollection segs = new PathSegmentCollection();
            segs.Add(pls);
            PathFigure fig = new PathFigure() { Segments = segs, StartPoint = points[points.Length - 1] };
            
            PathFigureCollection figs = new PathFigureCollection();
            figs.Add(fig);

            return new PathGeometry() { Figures = figs };
        }


        /// <summary>
        /// Change the opacity of the footprint outline and body.
        /// </summary>
        /// <param name="outlineOpacity"></param>
        /// <param name="bodyOpacity"></param>
        public void SetFootprintOpacity(double outlineOpacity = 0.7, double bodyOpacity = 0.1)
        {
            footprintOutlineOpacity = outlineOpacity;
            footprintBodyOpacity = bodyOpacity;
        }


        /// <summary>
        /// Leave a footprint at the current position.
        /// </summary>
        /// <returns></returns>
        public virtual Footprint Stamp()
        {
            Brush b1 = outlineBrush.CloneCurrentValue();
            b1.Opacity = footprintOutlineOpacity;

            Brush b2 = bodyBrush.CloneCurrentValue();
            b2.Opacity = footprintBodyOpacity;

            Path theUIE = createDefaultTurtleUI(b1, b2);          
            return Stamp(theUIE);
        }


         /// <summary>
         /// Stamp a uiElement at the current position, as a footprint.   The opacity settings for footprints are not applied here.
         /// </summary>
         /// <param name="theUIE"></param>
         /// <returns>The Footprint that was created.</returns>
        public virtual Footprint Stamp(UIElement theUIE)
        {
            TransformGroup tfg = theUIE.RenderTransform as TransformGroup;
            if (tfg == null) tfg = new TransformGroup();

            tfg.Children.Add(new RotateTransform(heading));
            tfg.Children.Add(new TranslateTransform(Position.X, Position.Y));
            theUIE.RenderTransform = tfg;
           
            Canvas.SetZIndex(theUIE, footprintlayer);
            Footprint fp = new Footprint(theUIE);
            Footprints.Add(fp);
            pumpAnimation();
            return fp;
        }


        /// <summary>
        /// Stamp a user-defined Geometry at the current turtle position and heading 
        /// using the current turtle properties for OutlineBrush, BrushWidth, Filling, etc.
        /// The turtle's Heading and Position remain unchanged.
        /// </summary>
        /// <param name="geom"></param>
        /// <returns>The Footprint that was created</returns>
        public virtual Footprint Stamp(Geometry geom)
        {

            Brush b1 = outlineBrush.CloneCurrentValue();
            b1.Opacity = footprintOutlineOpacity;

            Brush b2 = bodyBrush.Clone();
            b2.Opacity = footprintBodyOpacity;

            Path p = new Path()
            {
                Stroke = b1,
                StrokeThickness = brushWidth,
                Data = geom,
                Fill = b2,  
                StrokeEndLineCap = PenLineCap.Square
            };

            return Stamp(p);
        }


        /// <summary>
        /// Stamp a string at the current turtle position.
        /// </summary>
        /// <param name="str">The text to be written</param>
        /// <param name="offsetX">Relative X offset of text from turtle's current position.</param>
        /// <param name="offsetY">Relative Y offset of text from turtle's current position.</param>
        /// <param name="useRotation">If true, the text will rotate to the same heading as the turtle.</param>
        /// <returns></returns>
        public virtual Footprint Stamp(string str, double offsetX = 0.0, double offsetY = 0.0, bool useRotation = false)
        {
            Label lbl = new Label()
            {
                Content = str,
                FontFamily = TextFontFamily,
                FontSize = TextFontSize,
                FontWeight = TextFontWeight,
                Foreground = TextBrush
            };
            TransformGroup tfg = buildCurrentTurtleTransform();
            if (!useRotation)
            {
                tfg.Children.RemoveAt(0);
            }
            if (offsetX != 0.0 || offsetY != 0.0)
            {
                tfg.Children.Insert(0, new TranslateTransform(offsetX, offsetY));
            }
            lbl.RenderTransform = tfg;

            Canvas.SetZIndex(lbl, footprintlayer);
            Footprint fp = new Footprint(lbl);
            Footprints.Add(fp);
            pumpAnimation();
            return fp;
        }

        /// <summary>
        /// Stamp an image at the current turtle position.   
        /// If the offsets are zero, the top left corner of the image will be placed at the turtle's hotspot. 
        /// </summary>
        /// <param name="imgSrc">The image to be stamped</param>
        /// <param name="hotspotX">X offset of image that will be aligned to turtle's current X position.</param>
        /// <param name="hotspotY">Y offset of image that will be aligned to turtle's current Y position.</param>
        /// <param name="useRotation">If true, the image will be rotated to the same heading as the turtle.</param>
        /// <returns></returns>
        public virtual Footprint Stamp(ImageSource imgSrc, double hotspotX = 0.0, double hotspotY = 0.0, bool useRotation = false)
        {
            Image img = new Image() { Source = imgSrc };
            TransformGroup tfg = buildCurrentTurtleTransform();
            if (!useRotation)
            {
                tfg.Children.RemoveAt(0);
            }
            if (hotspotX != 0.0 || hotspotY != 0.0)
            {
                tfg.Children.Insert(0, new TranslateTransform(-hotspotX, -hotspotY));
            }
            img.RenderTransform = tfg;

            Canvas.SetZIndex(img, footprintlayer);
            Footprint fp = new Footprint(img);
            Footprints.Add(fp);
            pumpAnimation();
            return fp;
        }



        /// <summary>
        /// Returns a Color of the pixel at a given point in the playground.  This only works
        /// if the background of the parent Canvas is a bitmap image.  In all other cases
        /// (solid color canvas, gradient fills, etc.) the method will return Color.Transparent. 
        /// </summary>
        /// <param name="pt">The canvas position at which to extract the colour.</param>
        /// <returns></returns>
        public Color ColorOfBackgroundAt(Point pt)
        {
        
            Color c = Colors.Transparent;
            ImageBrush b = playground.Background as ImageBrush;
            if (b == null) return c;
            BitmapSource source = b.ImageSource as BitmapSource;
            if (b == null) return c;

            if (source != null)
            {
                int x = (int)(pt.X * source.PixelWidth / playground.RenderSize.Width);
                int y = (int)(pt.Y * source.PixelHeight / playground.RenderSize.Height);

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
       

        #endregion

    }
}
