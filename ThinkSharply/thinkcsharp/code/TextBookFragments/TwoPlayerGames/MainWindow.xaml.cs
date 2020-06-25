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

namespace TwoPlayerGames
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

        private void btnXoX_Click(object sender, RoutedEventArgs e)
        {
            XoXGUI newGameWin = new XoXGUI(txtPlayer1Name.Text, txtPlayer2Name.Text);
            newGameWin.Owner = this;
            newGameWin.Show();
        }
    }
}
