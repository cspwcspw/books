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
using System.Runtime.Serialization;

namespace Fragments
{
    /// <summary>
    /// Interaction logic for DictionaryGUI.xaml
    /// </summary>
    public partial class DictionaryGUI : Window
    {
        public DictionaryGUI()
        {
            InitializeComponent();
        }

        private void btnDemo1_Click(object sender, RoutedEventArgs e)
        {
            //Dictionary<string, int> emailCounter = new Dictionary<string, int>();

            //emailCounter["a.n.other@ru.ac.za"] = 3;
            //emailCounter["joe123@google.com"] = 12;
            //emailCounter["president@gov.za"] = 2;
            //emailCounter["abbey@foxpictures.com"] = 5;



            Dictionary<string, int> emailCounter = new Dictionary<string, int>() 
                {  {"a.n.other@ru.ac.za", 3},
                   {"joe123@google.com", 12},
                   {"president@gov.za",   2},
                   {"abbey@foxpictures.com", 5} };


           

            emailCounter["joe123@google.com"] = 1;

            emailCounter["joe123@google.com"] = 12;

            foreach (string k in emailCounter.Keys)
            {
                txtResult.AppendText(string.Format("Key {0} has value {1}.\n", k, emailCounter[k]));
            }
            txtResult.AppendText(string.Format("There are {0} key-value pairs in the dictionary.\n", emailCounter.Count));
        }

        private void btnLetterFreq_Click(object sender, RoutedEventArgs e)
        {
            int bday = 2050100508;
            MessageBox.Show(bday.ToString());

            string poem =
@"   The truth I do not stretch or shove
            When I state that the dog is full of love.
                 I've also found, by actual test,  
                    A wet dog is the lovingest.  
            
                                             Ogden Nash";

            IDictionary<char, int> freqs = letterFreqs(poem);
            foreach (char k in freqs.Keys)
            {
               txtResult.AppendText(string.Format("Character '{0}' occurs {1} times.\n", k, freqs[k]));
            }
        }

        private IDictionary<char, int> letterFreqs(string theText)
        {
            SortedDictionary<char, int> result = new SortedDictionary<char, int>();
            foreach (char c in theText)
            {
                if (char.IsLetter(c))
                {
                    if (result.ContainsKey(c))
                    {
                        result[c]++;
                    }
                    else
                    {
                        result[c] = 1;
                    }
                }
            }
            return result;
        }
    }
}
