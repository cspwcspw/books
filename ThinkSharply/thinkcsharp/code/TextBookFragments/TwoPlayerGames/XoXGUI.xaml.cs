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

namespace TwoPlayerGames
{

    public partial class XoXGUI : Window
    {

        private string player1;
        private string player2;


        Button[] theButtons;


        XoXGame theGame;




        public XoXGUI(string p1, string p2)
        {
            InitializeComponent();
            player1 = p1;
            player2 = p2;

            this.Title = string.Format("{0} vs {1}", player1, player2);

            theButtons = new Button[] { button0, button1, button2, button3, button4, button5, button6, button7, button8 };

            theGame = new XoXGame(0);
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            makeMove(0);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            makeMove(1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            makeMove(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            makeMove(3);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            makeMove(4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            makeMove(5);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            makeMove(6);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            makeMove(7);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            makeMove(8);
        }

        private void makeMove(int posn)
        {
            if (!theGame.IsCellEmpty(posn))
            {
                MessageBox.Show("You can't play there.  Try again.", "Oops");
                return;
            }
            int who = theGame.WhoseTurn();
            theGame.MakeMove(who, posn);
            theGame.SwitchPlayer();
            updateGUI();
        }

        private void updateGUI()
        {
            int[] bd = theGame.GetBoard();
            for (int i = 0; i < 9; i++)
            {
                if (bd[i] == 0)
                {
                    theButtons[i].Content = "0";
                }
                else if (bd[i] == 1)
                {
                    theButtons[i].Content = "X";
                }
                else
                {
                    theButtons[i].Content = "";
                }
            }
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
  

        }

    }
}
