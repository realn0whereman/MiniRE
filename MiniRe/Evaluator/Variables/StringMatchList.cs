using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.Variables
{
    /// <summary>
    /// A variable type in mini RE that 
    /// </summary>
    public class StringMatchList
    {
        List<StringMatch> matches;

        public StringMatchList()
        {
            matches = new List<StringMatch>();
        }

        public void AddMatch(string text, string filename, int line, int startIndex, int endIndex)
        {
            matches.Add(new StringMatch(text, filename, line, startIndex, endIndex));
        }

        /// <summary>
        /// The length of this list
        /// </summary>
        public int Length
        {
            get { return matches.Count; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach(StringMatch match in matches)
            {
                sb.Append("\"");
                sb.Append(match.Text);
                sb.Append("\"");
                sb.Append(" <'");
                sb.Append(match.Filename);
                sb.Append("', ");
                sb.Append(match.Line);
                sb.Append(", ");
                sb.Append(match.StartIndex);
                sb.Append(", ");
                sb.Append(match.EndIndex);
                sb.Append(">");
            }
            sb.Append("}");
            return base.ToString();
        }


    }
}
