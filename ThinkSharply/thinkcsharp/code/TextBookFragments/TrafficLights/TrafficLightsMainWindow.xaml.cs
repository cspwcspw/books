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

namespace TrafficLights
{
    /// <summary>
    /// Interaction logic for TrafficLightsMainWindow.xaml
    /// </summary>
    public partial class TrafficLightsMainWindow : Window
    {
        BitmapImage logo = new BitmapImage(new Uri("http://www.ict.ru.ac.za/Resources/ThinkSharply/_static/csharp_lessons.png"));
        BitmapImage[] thePics = {
                new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightGreen.png")),
                new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightAmber.png")),
                new BitmapImage(new Uri("pack://application:,,,/" + "TrafficLightRed.png")) };

        System.Windows.Threading.DispatcherTimer theTimer;
        int currentState = 0;

        public TrafficLightsMainWindow()
        {
            InitializeComponent();
             theTimer = new System.Windows.Threading.DispatcherTimer();
            theTimer.Interval = TimeSpan.FromMilliseconds(300);
            theTimer.IsEnabled = true;
            theTimer.Tick += dispatcherTimer_Tick;
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            stateControllerAdvance();
            updateView();
        }

        private void stateControllerAdvance()
        {
            switch (currentState)
            {
                case 0:
                    currentState = 1;
                    break;

                case 1:
                    currentState = 2;
                    break;

                case 2:
                    currentState = 0;
                    break;
            }
        }

        private void updateView()
        {
            image1.Source = thePics[currentState];
        }
    }
}
