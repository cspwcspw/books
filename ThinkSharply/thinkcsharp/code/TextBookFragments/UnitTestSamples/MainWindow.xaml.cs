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
using ThinkLib;

namespace UnitTestSamples
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

        private string classifyMark(int percent)
        {
            if (percent >= 50)
            {
                return "Good news!";
            }
            return "Whoops!";
        }

        private object Die()
        {
            throw new System.IO.IOException("G'bye, mate.");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double d = Math.Sqrt(2.0);

            double[] a1 = { d * d };
            double[] a2 = { 2.0 };
            Tester.TestEq(a1, a2);
            Tester.TestEq(a1, a2);
            Tester.TestEq(d, a2);
            Tester.TestEq(new double[] {d}, a2);
            Tester.TestEq(new float[] { 2f }, a2);

           // Tester.TestEq(()=>Die(), 2.0);
            Tester.TestEq(d * d, 2.0);
            Tester.TestEq(d * d, 2.0);
            Tester.TestEq(classifyMark(25), "Whoops!");
            Tester.TestEq(classifyMark(65), "Good news!");
            Tester.TestEq(classifyMark(50), "Good news!");
            Tester.TestEq(classifyMark(50), "naah!");
            Tester.TestEq("hello world".Length, 13);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            bool b = 2 == 2.0;
            byte c = 25;
            bool d = c == 25;

            // Do ints and  doubles compare ok, etc?
            Tester.TestEq(2, 2.0);
            Tester.TestEq(2.0, 2);
            Tester.TestEq(2, 2.0f);
            Tester.TestEq(2.0f, 2);
            Tester.TestEq(c, 25);
        }

        private void btnListFormatting_Click(object sender, RoutedEventArgs e)
        {
            double[][] da1 = { new [] { 2.0 }, new [] { -1.5 }, new [] { 3, 4.0 } };
            double[][] da2 = { new[] { 2.0 }, new[] { -1.5 }, new[] { 3.0, 4.0 } };
            double[] da3 = { 2.0, -1.5, 3.0, 4.0 };
            double[][] da4 = { new[] { 2.0 }, new[] { -1.5 }, new[] { 3.1, 4.0 } };
            float[][] da5 = { new[] { 2.0f }, new[] { -1.5f }, new[] { 3.0f, 4.0f } };

            Tester.TestEq(da1, da2); // expect: true
            Tester.TestEq(da1, da5); // expect: true
            Tester.TestEq(da1, da4); // expect: true
            Tester.TestEq(da1, null); // expect: false
            Tester.TestEq(da1, da3); // expect: false
            Tester.TestEq(da1, da4); // expect: false
        }

        private void btnHandleNull_Click(object sender, RoutedEventArgs e)
        {
            string s1 = null;
            string s2 = null;
            string s3 = "hello";
            Tester.TestEq(s1, s2);
            Tester.TestEq(s1, s3);
            Tester.TestEq(s3, s1);
            Tester.TestEq("h", 'h');
            

            string [] sa1 = {"one", null, "three"};
            string [] sa2 = {"one", null, "three"};
            string [] sa3 = {"one", "two", null};

            Tester.TestEq(sa1, null);
            Tester.TestEq(null, sa1);
            Tester.TestEq(sa1, sa2);
            Tester.TestEq(sa1, sa3);
        }

        private void btnInfinite_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(3, 3);   // We expect this successful test to be flushed to the GUI
            while (true)           // sven though this infinite loop is here.
            {
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        { // all shall pass! (This isn't the bridge of Khazad-Dum, after all)
            Tester.TestEq(5, (float)5);
            Tester.TestEq(5, (int)5);
            Tester.TestEq(5, (double)5);
            Tester.TestEq(5, (short)5);
            Tester.TestEq(5, (long)5);
            Tester.TestEq(5, (uint)5);
            Tester.TestEq(5, (ushort)5);
            Tester.TestEq(5, (ulong)5);
            Tester.TestEq(5, (byte)5);
            Tester.TestEq(5, (sbyte)5);

            Tester.TestEq(new[] { 5 }, new[] { (float)5 });
            Tester.TestEq(new[] { 5 }, new[] { (int)5 });
            Tester.TestEq(new[] { 5 }, new[] { (double)5 });
            Tester.TestEq(new[] { 5 }, new[] { (short)5 });
            Tester.TestEq(new[] { 5 }, new[] { (long)5 });
            Tester.TestEq(new[] { 5 }, new[] { (uint)5 });
            Tester.TestEq(new[] { 5 }, new[] { (ushort)5 });
            Tester.TestEq(new[] { 5 }, new[] { (ulong)5 });
            Tester.TestEq(new[] { 5 }, new[] { (byte)5 });
            Tester.TestEq(new[] { 5 }, new[] { (sbyte)5 });
        }

        private void btnLists_Click(object sender, RoutedEventArgs e)
        {
            List<string> friends1 = new List<string>() { "Angelina", "Brad", "Joe", "Paris", "Thandi", "Zoe", "Zuki" };
            List<string> friends2 = new List<string>() { "Angelina", "Brad", "Joe", "Paris", "Thandi", "Zoe", "Zuki" };
            List<string> friends3 = new List<string>() { "Angelina", "Brad", "Joesph", "Paris", "Thandi", "Zoe", "Zuki" };
            List<string> friends4 = new List<string>() { "Angelina", "Brad", "Thandi", "Zoe", "Zuki" };
            List<string> friends5 = new List<string>() {  };
            Tester.TestEq(friends1, friends2);     // should pass
            Tester.TestFailEq(friends1, friends3); // should fail
            Tester.TestFailEq(friends1, friends4); // should fail
            Tester.TestFailEq(friends1, friends5); // should fail
        }

        private void btnCloseStrings_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(" Hello", "Hello");
            Tester.TestEq(" Hello ", "Hello");
            Tester.TestEq("Hello ", "Hello");


            Tester.TestEq('x', 'y');
            Tester.TestEq('x', '\0');

            Tester.TestEq(new string[] {"Hello", "World"},  new string[] {"Hello", "World "});
            Tester.TestEq(new string[] {"Hello", "World" }, new string[] { "Hello", "World", "News"});

            Tester.TestEq(new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 'a', 'e', 'i', '0', 'u' });

        }
    }
}
