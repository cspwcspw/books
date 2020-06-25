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
using System.IO;
using ThinkLib;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for Exceptions.xaml
    /// </summary>
    public partial class Exceptions : Window
    {
        public Exceptions()
        {
            InitializeComponent();

            //Console.WriteLine("What is your name?");
            //string s = Console.ReadLine();
            //Console.WriteLine("Hello, {0}.", s);
        }


        int f(int x)
        {
            return x;
        }

        int f(int x, int y)
        {
            return x + y;
        }

        int f( )
        {
            return 0;
        }

        double f(params int[] xs)
        {
            return 99.0;
        }


        private void btnOther_Click(object sender, RoutedEventArgs e)
        {

//            int n = Convert.ToInt32("123.45");





//            Console.Write("What is your name? ");
//            string s = Console.ReadLine();
//            //int j = 142;
//            //int k = 0;

//            //if (j % k == 1)
//            //{
//            //    MessageBox.Show("It is odd");
//            //}


//         //   int result = f(3, 4, 5, 6);
//            double d = f(3, 4);


//            //int yearOfBirth = 1995;
//            //int yearNow = DateTime.Now.Year;
//            //int age = yearNow - yearOfBirth;
//            //string s1 = string.Format("{0}, born in {1}, turns {2} years old in {3}.", 
//            //      "Joe", yearOfBirth, age, yearNow);
//            //MessageBox.Show(s1);

//            string s1 = @"  this is
//   a string
//with 
//newlines";  // "Joe,19,BSc,CompSci";

//            string poem =
//@"   The truth I do not stretch or shove
//When I state that the dog is full of love.
//     I've also found, by actual test,  
//        A wet dog is the lovingest.  
//
//                                 Ogden Nash";

//            MessageBox.Show(poem, "The Dog");





//            string s2 = "Kim   ,19  ;BSc-CompSci, female";
//            string[] parts = s2.Split(new char[] {' ', ',', ';', '-'},
//                                      StringSplitOptions.RemoveEmptyEntries);

//            return;






//            string filename = textBox1.Text;

            


//            try
//            {
//                string content = File.ReadAllText(filename);

//            }
//            catch
//            {

//            }
//            //catch (Exception ex)
//            //{   string msg = string.Format("Reading file {0} threw an exception: \n{1}", 
//            //          filename, ex.Message);
//            //    MessageBox.Show(msg);
//            //}
//            finally
//            {

//            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TextWriter f = File.CreateText("C:\\temp\\test1.txt");

            try
            {
                f.WriteLine("My first file written from C#");
                f.WriteLine("-----------------------------");
                // This next line has a deliberate bug...
                f.WriteLine("Now is {0}, {1}", DateTime.Now);
            }
            catch (IOException ex)
            {
                f.WriteLine("Hey, we caught exception {0}.", ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                f.WriteLine("Hey, we caught exception {0}.", ex.Message);
                throw ex;
            }
            finally
            {
                f.WriteLine("We're executing the finally block, and closing the file.");
                f.Close();
            }
        }


        private int findMax(List<int> xs)
        {
            if (xs.Count < 1)
            {
                Exception myBad = new InvalidOperationException("Cannot find max of an empty list.");
                throw myBad;
            }
            int result = xs[0];
            for (int i = 0; i < xs.Count; i++)
            {
                if (xs[i] > result)
                {
                    result = xs[i];
                }
            }
            return result;
        }




        private void btnFindMax_Click(object sender, RoutedEventArgs e)
        {
            List<int> xs = new List<int>() { };     

             int p = findMax(xs);
        }

        private void btnParseInt_Click(object sender, RoutedEventArgs e)
        {

            string s = textBox1.Text;

            try
            {
                int n = Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

    }
}
