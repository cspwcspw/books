using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mySendSMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string [] quips = {
                "Out of my mind... Back in five minutes.",
                "Your gene pool needs a little chlorine.",
                "Ever stop to think, and forget to start again?",
                "I used to have a handle on life, but it broke." };
            Random rng = new Random();
            int whichQuip = rng.Next(0, quips.Length);
            MessageBox.Show(quips[whichQuip], "Thought for the day ...");
        }
    }
}
