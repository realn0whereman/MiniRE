using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.Variables
{
    public class StringMatch
    {
        string text;
        string filename;
        int line;
        int startIndex;
        int endIndex;

        public StringMatch()  {        }

        public StringMatch(string text, string filename, int line, int startIndex = 0, int endIndex = 0)
        {
            this.text = text;
            this.filename = filename;
            this.line = line;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        public bool Matches(StringMatch match)
        {
            if (this.text == match.Text)
                return true;
            else
                return false;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public int Line
        {
            get { return line; }
            set { line = value; }
        }
        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }
        public int EndIndex
        {
            get { return endIndex; }
            set { endIndex = value; }
        }
    }
}
