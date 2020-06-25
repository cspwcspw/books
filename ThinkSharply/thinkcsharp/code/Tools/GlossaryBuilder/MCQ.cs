using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlossaryBuilder
{
    public class MCQ
    {
        private List<Definition> options;
        private int correctOpt;

        public MCQ(List<Definition> options, int correctOpt)
        {
            this.options = options;
            this.correctOpt = correctOpt;
        }

        string htmlTemplate = @"
<html>
Pick the closest meaning for the term &nbsp;&nbsp;&nbsp;<b><em>{0} </em></b>
<ol type=""a"">
{1} 
</ol>
<html>";


        public string ToHtmlQuiz(bool quizByTerm)
        {
            StringBuilder sb = new StringBuilder();
             
            foreach (Definition d in options)
            {
                sb.Append("<li>");
                sb.Append(d.RhsHtml);
            }
            string page = string.Format(htmlTemplate, options[correctOpt].Term, sb.ToString());
            return page;
        }

        string termTemplate = @"
<html>
The terms are: 
<ol type=""a"">
{0} 
</ol>
<html>";

        internal string ToHtmlTerms(bool p)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Definition d in options)
            {
                sb.Append("<li>");
                sb.Append(d.TermHtml);
            }
            string page = string.Format(termTemplate, sb.ToString());
            return page;
        }
    }
}
