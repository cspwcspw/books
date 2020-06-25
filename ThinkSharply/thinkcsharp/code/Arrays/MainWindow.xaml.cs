using System;
using System.Windows;
using ThinkLib;

namespace Arrays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double[,] rainfall =  new double[4,3] { { 165.3,  170.5,  172.1 },
                                                { 149.6,  140.3,  152.3 },
                                                {  44.3,   42.4,   45.0 },
                                                {  95.3,   89.8,   92.4 } };

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Create an average reading for each quarter
            double[] avgs = new double[4];
            for (int row = 0; row < 4; row++)
            {
                double sum = 0;
                for (int col = 0; col < 3; col++)
                {
                    sum += rainfall[row, col];
                }
                avgs[row] = sum / 3.0;
            }
            Tester.TestEq(avgs,  new double [] {169.3, 147.4,  43.9, 92.5});
            MessageBox.Show(String.Format("{0}, {1}, {2}, {3}", avgs[0], avgs[1], avgs[2], avgs[3]));
        }
    }
}
