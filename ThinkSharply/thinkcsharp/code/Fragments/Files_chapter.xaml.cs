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
using System.Net;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for Files_chapter.xaml
    /// </summary>
    public partial class Files_chapter : Window
    {
        public Files_chapter()
        {
            InitializeComponent();
        }

      private void button1_Click(object sender, RoutedEventArgs e)
        {
            writeMyFirstTextFile();
        }

        private void writeMyFirstTextFile()
        {
            TextWriter f = File.CreateText("C:\\temp\\test1.txt");
            f.WriteLine("My first file written from C#");
            f.WriteLine("-----------------------------");
            f.WriteLine("Hello, world!");
            f.WriteLine("The time now is {0}", DateTime.Now.ToLongTimeString());
            f.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            readMyFirstFileLineByLine();
        }

        private void readMyFirstFileLineByLine()
        {
            TextReader myfile = File.OpenText("C:\\temp\\poem.txt");
            txtResult.Clear();
            while (true)                              // Keep reading forever.
            {
                string theline = myfile.ReadLine();   // Try to read next line.
                if (theline == null) break;           // If no more lines, leave the loop.

                txtResult.AppendText(theline + "\n"); // process the line we've just read.
            }
            myfile.Close();                           // Close the file handle.
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            readToListOfLines();
        }

        private void readToListOfLines()
        {
            string[] lines = File.ReadAllLines("C:\\temp\\poem.txt");
            Array.Sort(lines);

            foreach (string line in lines)
            {
                txtResult.AppendText(line + "\n");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            readToString();
        }

        private void readToString()
        {
            string content = File.ReadAllText("C:\\temp\\poem.txt");

            string[] delimiters = null;
            string[] words = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            MessageBox.Show(String.Format("There are {0} words in the file.", words.Length));
        }


        private void copyBinaryFile()
        {
            byte[] buf = File.ReadAllBytes("C:\\temp\\somewhere.mp4");
            File.WriteAllBytes("C:\\temp\\thecopy.mp4", buf);
        }


        private void copyBinaryFileOneChunkAtATime()
        {
            
            Stream inpf = File.OpenRead("C:\\temp\\somewhere.mp4");
            Stream outf = File.Create("C:\\temp\\thecopy2.mp4");

            const int chunkSz = 10204;
            byte[] buf = new byte[chunkSz];
            while (true)
            {
                int n = inpf.Read(buf, 0, chunkSz);
                if (n == 0) break;
                outf.Write(buf, 0, n);
            }
            inpf.Close();
            outf.Close();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            copyBinaryFileOneChunkAtATime();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
           // copyBinaryFile();
            //StreamReader tr =  new StreamReader(myResp.GetResponseStream());
            //txtResult.Text = tr.ReadToEnd();
            //tr.Close();

            string myUri = "http://www.ict.ru.ac.za/resources/thinkSharply/thinksharply/_images/csharp_lessons.png";

            HttpWebRequest myReq = (HttpWebRequest) HttpWebRequest.Create(myUri);
            WebResponse myResp = myReq.GetResponse();

            Stream inpf = myResp.GetResponseStream();

            Stream outf = File.Create("C:\\temp\\theLogo.png");

            const int chunkSz = 10204;
            byte[] buf = new byte[chunkSz];
            while (true)
            {
                int n = inpf.Read(buf, 0, chunkSz);
                if (n == 0) break;
                outf.Write(buf, 0, n);
            }
            inpf.Close();
            outf.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            TextReader myfile = File.OpenText("C:\\temp\\poem.txt");
            TextWriter newfile = File.CreateText("C:\\temp\\poem_filtered.txt");
            txtResult.Clear();
            while (true)                               
            {
                string theline = myfile.ReadLine();    
                if (theline == null) break;

                if (theline.Length > 30)
                {
                    txtResult.AppendText(theline + "\n");
                    newfile.WriteLine(theline);
                }
            }
            myfile.Close();
            newfile.Close();
        }

        private void btnGolf_Click(object sender, RoutedEventArgs e)
        {


            TextReader myfile = File.OpenText("C:\\temp\\golf1.txt");
            TextWriter ff = File.CreateText("C:\\temp\\golf_fixed_format.txt");
            TextWriter fd = File.CreateText("C:\\temp\\golf_delimited.txt");
            txtResult.Clear();
            while (true)
            {
                string theRecord = myfile.ReadLine();
                if (theRecord == null) break;
                string[] parts = theRecord.Split(';');

                string[] fields = parts[1].Split(new char[] {' '}
                                     ,    StringSplitOptions.RemoveEmptyEntries);

                string result = string.Format("{0,-20}{1,5}{2,5}{3,5}{4,5}", parts[0].Trim(),
                    Convert.ToInt32(fields[3]),
                    Convert.ToInt32(fields[4]),
                    Convert.ToInt32(fields[5]),
                    Convert.ToInt32(fields[6]));
                txtResult.AppendText(result + "\n");
                ff.WriteLine(result);

                string result2 = string.Format("{0},{1},{2},{3},{4}", parts[0].Trim(),
                    Convert.ToInt32(fields[3]),
                    Convert.ToInt32(fields[4]),
                    Convert.ToInt32(fields[5]),
                    Convert.ToInt32(fields[6]));
                fd.WriteLine(result2);

            }
            myfile.Close();
            ff.Close();
            fd.Close();
        }

        private void btnReadFromWeb_Click(object sender, RoutedEventArgs e)
        {
            WebClient wc = new WebClient();

            string address1 = "http://www.ict.ru.ac.za/resources/ThinkSharply/thinkSharply/_downloads/golf_fixed_format.txt";
         //   address1 = "ftp://muaddib.ict.ru.ac.za/welcome.msg";
 
            string content1 = wc.DownloadString(address1);   // like File.ReadAllText - brings down the whole text file.

            // Now fetch an image into a byte array, and save it to disk
            string address2 = "http://www.ict.ru.ac.za/resources/ThinkSharply/thinkSharply/_images/csharp_lessons.png";
            address2 = "file:///F:/MCQ/MCQCourseInfo/csc101/Pics/14M2613.jpg";
            byte[] buf = wc.DownloadData(address2);
            File.WriteAllBytes("C:\\temp\\pic.png",  buf);

        }
    }
 }
