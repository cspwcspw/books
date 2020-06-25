using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using HtmlAgilityPack;

namespace GlossaryBuilder
{
    /// <summary>
    /// Extract a list of definitions from the textbook html documents.
    /// </summary>
    public class Definitions : List<Definition>
    {


        public Definitions()
            : base()
        {
        }



        internal void AddGlossaryDefs(string fullPath, string chapterTitle, int chapterNum)
        {
            HtmlDocument theDoc = new HtmlDocument();
            theDoc.OptionUseIdAttribute = true;
            theDoc.Load(fullPath);

            HtmlNodeCollection defLists = theDoc.DocumentNode.SelectNodes("//dl");

            if (defLists == null) return;

            bool seekTerm = true;
            HtmlNode key = null;

            foreach (HtmlNode theList in defLists)
            {
                foreach (var child in theList.ChildNodes)
                {
                    string idChild = child.Id;
                    if (seekTerm)
                    {
                        //  theTerm = child.SelectSingleNode("dt");
                        if (child.Name != "dt") continue;
                        key = child;
                        seekTerm = false;
                    }
                    else
                    {
                        //theDef = child.SelectSingleNode("/dd");
                        if (child.Name != "dd") continue;
                        seekTerm = true;
                        string val0 = child.OuterHtml;
                        string val = child.InnerHtml;
                        this.Add(
                             new Definition(key.InnerText, child.InnerText, key.InnerHtml, child.InnerHtml, chapterTitle, chapterNum));

                    }
                }
            }
        }
    }
}