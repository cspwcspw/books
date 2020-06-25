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
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;
using System.Windows.Resources;

namespace GlossaryBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Definitions allDefs;
        public QuizMaster theQuizMaster;

        string path = @"..\..\..\..\..\..\thinkSharply\";

        string[] chapters = {
                            "way_of_the_program.html", 
                            "VS_guide_1.html", 
                            "start_window.html", 
                            "events_and_handlers_1.html", 
                            "hello_little_turtles.html", 
                            "methods_void.html", 
                            "conditionals.html", 
                            "methods_value_returning.html", 
                            "iteration.html", 
                            "strings.html", 
                            "classes_and_objects_0.html", 
                            "events.html", 
                            "arrays.html", 
                            "odds_and_ends.html", 
                            "files.html", 
                            "lists.html", 
                            "list_algorithms.html", 
                            "n_queens_I.html", 
                            "recursion.html", 
                            "exceptions.html",  
                            "frameworkclasslib.html", 
                            "scope_and_lifetime.html", 
                            "n_queens_II.html", 
                            "classes_and_objects_I.html", 
                            "in_the_caves.html", 
                            "inheritance.html", 
                            "dictionaries.html", 
                            "interfaces.html"    };

        string[] chapterTitles =  { "Chapter 1 The Way of the Program",
                                     "Chapter 2 Visual Studio Survival Guide",
                                     "Chapter 3 Start with a Window",
                                     "Chapter 4 Code-behind: Events and Handlers",
                                     "Chapter 5 Hello, little turtles!",
                                     "Chapter 6 Void Methods",
                                     "Chapter 7 Working with Booleans and Conditional Statements",
                                     "Chapter 8 Value-returning Methods",
                                     "Chapter 9 Iteration",
                                     "Chapter 10 Strings",
                                     "Chapter 11 Classes and Objects - an Overview",
                                     "Chapter 12 More Event Handling",
                                     "Chapter 13 Arrays",
                                     "Chapter 14 Odds and Ends",
                                     "Chapter 15 I/O and Files",
                                     "Chapter 16 Lists",
                                     "Chapter 17 List Algorithms",
                                     "Chapter 18 The N-Queens Puzzle - a Case Study",
                                     "Chapter 19 Recursion",
                                     "Chapter 20 Exceptions",
                                     "Chapter 21 The .NET Framework",
                                     "Chapter 22 Scope and Lifetime",
                                     "Chapter 23 GUIs for our Queens",
                                     "Chapter 24 Writing our own Classes",
                                     "Chapter 25 In the Caves - a Case Study",
                                     "Chapter 26 Inheritance",
                                     "Chapter 27 Dictionaries",
                                     "Chapter 28 Interfaces" };


        public MainWindow()
        {
            InitializeComponent();

#if false
            // Build a fresh set of definitions from the textbook html, 
            // and save them to a file.   When this is done, we need to 
            // put the file into this code as resource, and we'll just
            // load the defs from there in future.
            allDefs = new Definitions();

            for (int ch = 0; ch < chapters.Length; ch++)
            {
                string fullPath = path + chapters[ch];
                allDefs.AddGlossaryDefs(fullPath, chapterTitles[ch], ch + 1);
            }
            StreamWriter sw = File.CreateText(@"C:\temp\theGlossary.xml");
            XmlSerializer xs = new XmlSerializer(typeof(Definitions));
            xs.Serialize(sw, allDefs);
            sw.Close();
#else
             XmlReader rdr = XmlReader.Create(new StringReader(Properties.Resources.theGlossary));
             XmlSerializer xs = new XmlSerializer(typeof(Definitions));
             allDefs = (Definitions) xs.Deserialize(rdr);
             rdr.Close();
#endif
             theQuizMaster = new QuizMaster();
             theQuizMaster.FilteredDefs = new Definitions();
             theQuizMaster.FilteredDefs.AddRange(allDefs);
             buildFilterCheckboxes();
             lbTerms.ItemsSource = theQuizMaster.FilteredDefs;
             rebindList(true);

     
        }

        private void lbTerms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int elem = lbTerms.SelectedIndex;
            if (elem < 0) return;
            displayDefintion(theQuizMaster.FilteredDefs[elem]);
        }

        string htmlTemplate = @"
<html>
<h3>{0}</h3>
<dl>
<dt>{1}</dt>
<dd>{2}</dd>
</dl>
<html>";

        private void displayGlossaryHelp()
        {
            string someHelp = @"
<html>
Select a term on the left to display its definition.
<html>";
            theBrowser.NavigateToString(someHelp);
        }

        private void displayDefintion(Definition def)
        {
            string theHtml = string.Format(htmlTemplate, def.ChapterTitle, def.TermHtml, def.RhsHtml);
            theBrowser.NavigateToString(theHtml);
        }

        private void rb_Changed(object sender, RoutedEventArgs e)
        {
            rebindList(rbAlpha.IsChecked == true);
        }

        private void rebindList(bool wantAlphaSort)
        {
            if (!this.IsInitialized) return;
            if (wantAlphaSort)
            {
                theQuizMaster.FilteredDefs.Sort(Definition.AlphaComparer);
            }
            else
            {
                theQuizMaster.FilteredDefs.Sort(Definition.ChapterComparer);
            }

            lbTerms.Items.Refresh();
            displayGlossaryHelp();
        }

        #region Filtering the chapters

        List<CheckBox> chapterCheckboxes;


        private void buildFilterCheckboxes()
        {
            double fixedWidth = 350;
            chapterCheckboxes = new List<CheckBox>();
            chaptersCheckboxPanel.Children.Clear();
            chaptersCheckboxPanel.Orientation = Orientation.Vertical;
            for (int i = 0; i < chapterTitles.Length; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Content = chapterTitles[i].Replace("Chapter", "Ch");
                cb.IsChecked = true;
                cb.Width = fixedWidth;
                cb.Margin = new Thickness(4,1,0,0);
                chapterCheckboxes.Add(cb);
                chaptersCheckboxPanel.Children.Add(cb);
            }
        }

        private void btnApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            theQuizMaster.FilteredDefs.Clear();
            foreach (Definition d in allDefs)
            {
                if (chapterCheckboxes[d.ChapterNum - 1].IsChecked == true)
                {
                    theQuizMaster.FilteredDefs.Add(d);
                }
            }
            rebindList(rbAlpha.IsChecked == true);
            tabControl1.SelectedIndex = 0;
        }

        private void btnSelectAllFilters_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in chapterCheckboxes)
            {
                cb.IsChecked = true;
            }
        }

        private void btnInvertSelection_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox cb in chapterCheckboxes)
            {
                cb.IsChecked = ! cb.IsChecked;
            }
        }

        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            poseQuestion();
        }

        MCQ currentQuestion = null;

        private void poseQuestion()
        {
            currentQuestion = theQuizMaster.PoseMCQ();
            string theHtml = currentQuestion.ToHtmlQuiz(true);  
            theQuizBrowser.NavigateToString(theHtml);
            theQuizAnswerBrowser.NavigateToString("<html></html>");
        }

        private void btnShowAnswers_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestion == null) return;
            string theHtml = currentQuestion.ToHtmlTerms(true);
            theQuizAnswerBrowser.NavigateToString(theHtml);
        }

    }
}
