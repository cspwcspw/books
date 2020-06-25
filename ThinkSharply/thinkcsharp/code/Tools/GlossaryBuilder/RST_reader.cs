using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GlossaryBuilder
{
    class RST_reader
    {
        private TextReader rstDoc;

        public int Linenum { get; set; }

        private string currLine = null;
        // A one-line "unread" buffer
        string pushedBackLine = null;

        public RST_reader(TextReader rstDoc)
        {
            this.rstDoc = rstDoc;
            Linenum = 0;
        }

        /// <summary>
        /// Read up to the glossary heading, then skip over the section underlining.
        /// </summary>
        /// <returns></returns>
        internal bool SkipOverGlossary()
        {
            while (true)
            {
                Linenum++;
                currLine = rstDoc.ReadLine();
                if (currLine == null) break;
                if (currLine.ToLower().StartsWith("glossary"))
                {
                    currLine = rstDoc.ReadLine();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return the next word being defined.
        /// </summary>
        /// <returns></returns>
        internal string GetNextWord()
        {
            while (true)
            {
                Linenum++;
                if (pushedBackLine != null)
                {
                    currLine = pushedBackLine;
                    pushedBackLine = null;
                }
                else
                {
                    currLine = rstDoc.ReadLine();
                }
                if (currLine == null) break;
                if (currLine.Trim() == "" || currLine.StartsWith("..")) continue;
                if (!currLine.StartsWith("   ")) return null;
                return currLine.Trim();
            }
            return null;
        }

        internal string GetNextDefinition()
        {
            StringBuilder sb = new StringBuilder();
            string separator = "";
            while (true)
            {
                Linenum++;
                currLine = rstDoc.ReadLine();
                if (currLine == null) break;
                if (currLine.Trim() == "" || currLine.StartsWith("..")) continue;
                if (!currLine.StartsWith("      "))
                {
                    pushedBackLine = currLine;
                    break;
                }
                sb.Append(separator);
                sb.Append(currLine.Trim());
                separator = " ";
            }
            return sb.ToString();
        }
    }
}
