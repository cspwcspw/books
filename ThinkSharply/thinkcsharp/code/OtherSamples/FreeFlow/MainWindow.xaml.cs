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
using System.Windows.Threading;

namespace FreeFlow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Puzzle thePuzzle;

        DispatcherTimer theTimer;

          public MainWindow()
        {
            InitializeComponent();
            thePuzzle = Puzzle.fromString(Puzzle.Lev20); new Puzzle(Puzzle.builtInPuzzles[2]);
            renderState(thePuzzle.PendingStates.Peek());

            theTimer = new DispatcherTimer();
       //     theTimer.Interval = TimeSpan.FromMilliseconds(1000);
            theTimer.Tick += new EventHandler(theTimer_Tick);
        }

        void theTimer_Tick(object sender, EventArgs e)
        {
            // advance some states
            Puzzle.State curr;
            while (true)
            {
                curr = thePuzzle.advanceSearch();
                if (curr.Equals(Puzzle.State.Empty))
                {
                    this.Title = String.Format("States explored={0}, pending={1},  No further solutions", thePuzzle.StatesExplored, thePuzzle.PendingStates.Count);
                    txtResult.Text = "No further solutions";
                    theTimer.IsEnabled = false;
                    break;
                }
                if (thePuzzle.StatesExplored % 10000 == 0) break;
                if (curr.IsSolved())
                {
                    theTimer.IsEnabled = false;
                    break;
                }
            }

            this.Title = String.Format("States explored={0}, pending={1},  isSolved={2}", thePuzzle.StatesExplored, thePuzzle.PendingStates.Count, curr.IsSolved());
            txtResult.Text = curr.ToString();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            Puzzle.State curr = thePuzzle.advanceSearch();
            this.Title = String.Format(" Is solved? {0} ", curr.IsSolved());

            renderState(curr);
        }

        private void renderState(Puzzle.State state)
        {
            txtResult.Text = state.ToString();
            flush();
            
        }

        private Action emptyDelegate = delegate { };
        private void flush()
        {
            this.Dispatcher.Invoke(emptyDelegate, System.Windows.Threading.DispatcherPriority.SystemIdle);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            theTimer.IsEnabled = !theTimer.IsEnabled;
            //stateRunning = true;
            //Puzzle.State curr = Puzzle.State.Empty;
            //while (stateRunning)
            //{
            //     curr = thePuzzle.advanceSearch();
            //    if (curr.Equals(Puzzle.State.Empty))
            //    {
            //        this.Title = String.Format("States explored={0}, pending={1}", thePuzzle.StatesExplored, thePuzzle.PendingStates.Count);
            //        txtResult.Text = "No further solutions";
            //        break;
            //    }

            //    if (thePuzzle.StatesExplored % 10000 == 0)
            //    {
            //        this.Title = String.Format("States explored={0}, pending={1}", thePuzzle.StatesExplored, thePuzzle.PendingStates.Count);
            //        renderState(curr);
            //    }

            //    if (curr.IsSolved())
            //    {
            //        this.Title = String.Format("States explored={0}, pending={1}", thePuzzle.StatesExplored, thePuzzle.PendingStates.Count);
            //        renderState(curr);
            //        break;
            //    }
            //}

            
            //flush();
        }

        private void btnNoBrainer_Click(object sender, RoutedEventArgs e)
        {
            Puzzle.State curr = thePuzzle.PendingStates.Peek();

            curr.DoNoBrainers();
            this.Title = String.Format(" Is solved? {0} ", curr.IsSolved());

            renderState(curr);
        }
    }
}
