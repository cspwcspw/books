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
using System.Windows.Shapes;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for TrafficLights.xaml
    /// </summary>
    public partial class TrafficLightsGUI : Window
    {
        private BitmapImage[] thePics;
        private TrafficLightFSM model1, model2, model3;
        private System.Windows.Threading.DispatcherTimer theTimer;

        public TrafficLightsGUI()
        {
            InitializeComponent();

            model1 = new TrafficLightFSM();
            model2 = new TrafficLightFSM();
            model3 = new TrafficLightFSM();

            thePics = new BitmapImage[] { 
                        new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightGreen.png")),
                        new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightAmber.png")),
                        new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightRed.png")) };

            theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Tick += theTimer_Tick;
            theTimer.Interval = TimeSpan.FromMilliseconds(500);
            theTimer.Start();
        }


        private void theTimer_Tick(object sender, EventArgs e)
        {
            model1.Advance();    // advance the model, and then refresh the view.
            image1.Source = thePics[model1.CurrentState];
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Controller for light set 2 - press the N key for Next
                case Key.N:                 
                    model2.Advance();
                    switch (model2.CurrentState)
                    {
                        case 0: lblOutputSet2.Content = "GO!";
                            break;
                        case 1: lblOutputSet2.Content = "PAUSE!";
                            break;
                        case 2: lblOutputSet2.Content = "STOP!";
                            break;
                    }
                    break;
            }
        }

        private void btnAdvance_Click(object sender, RoutedEventArgs e)
        {
            model3.Advance();
            switch (model3.CurrentState)
            {
                case 0: progressBar1.Value = 33;
                    progressBar1.Foreground = Brushes.Green;
                    break;
                case 1: progressBar1.Value = 66;
                    progressBar1.Foreground = Brushes.Orange;
                    break;
                case 2: progressBar1.Value = 100;
                    progressBar1.Foreground = Brushes.Red;
                    break;
            }
        }
    }
}
