using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlossaryBuilder
{
    public class QuizMaster
    {
        Random rng = new Random();

        const int numOptions = 5;

        private Definitions filteredDefs;
        public Definitions FilteredDefs
        {
            get { return filteredDefs; }
            set { filteredDefs = value; }
        }

        public QuizMaster( )
        {
             
        }
 
        internal MCQ PoseMCQ()
        {

            //foreach (Definition d in filteredDefs)
            //{
            //    if (!(d.RhsHtml.StartsWith("<dd>") && d.RhsHtml.EndsWith("</dd>")))
            //    {
            //        System.Windows.MessageBox.Show(d.RhsHtml, "Oops");
            //    }

            //    if (!(d.TermHtml.StartsWith("<dt") && d.TermHtml.EndsWith("</dt>")))
            //    {
            //        System.Windows.MessageBox.Show(d.TermHtml, "Oops - term is bad");
            //    }

            //}



            List<int> opts = new List<int>();
            List<Definition> options = new List<Definition>();
            // Choose some distractors
            int n = 0;
            while (n < numOptions)
            {
                int opt = rng.Next(filteredDefs.Count);
                if (opts.Contains(opt)) continue;
                n++;
                opts.Add(opt);
                options.Add(filteredDefs[opt]);
            }
            // Pick a "correct" one
            int correctOpt = rng.Next(numOptions);
            return new MCQ(options, correctOpt);
        }
    }
}
