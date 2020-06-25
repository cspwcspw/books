using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace GlossaryBuilder
{
    public class Definition
    {
        public string Term { get; set; }
        public string Rhs { get; set; }
        public string ChapterTitle {get; set; }
        public int ChapterNum{ get; set; }

        public string TermHtml { get; set; }
        public string RhsHtml { get; set; }


        public Definition() { }

        //public void SaveXML(string filename)
        //{
        //    XmlSerializer xs = new XmlSerializer(this.GetType());
        //    StreamWriter sw = File.CreateText(filename);
        //    xs.Serialize(sw, this);
        //    sw.Close();
        //}

        public Definition(string term, string rhs, string htmlTerm, string htmlRhs, string chapterTitle, int chapterNum)
        {
            Term = term;
            Rhs = rhs;
            TermHtml = htmlTerm;
            RhsHtml = htmlRhs;
            ChapterTitle = chapterTitle;
            ChapterNum = chapterNum;
        }

        public static int AlphaComparer(Definition x, Definition y)
        {
            int c1 = x.Term.CompareTo(y.Term);
            if (c1 != 0) return c1;
            return x.ChapterTitle.CompareTo(y.ChapterTitle);
        }

        public static int ChapterComparer(Definition x, Definition y)
        {
            int c1 = x.ChapterNum.CompareTo(y.ChapterNum);
            if (c1 != 0) return c1;
            return x.Term.CompareTo(y.Term);
        }
    }
}
