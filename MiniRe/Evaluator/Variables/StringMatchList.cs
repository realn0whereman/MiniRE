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

        public void AddMatch(StringMatch match)
        {
            matches.Add(new StringMatch(match.Text, match.Filename, match.Line, match.StartIndex, match.EndIndex));
        }
        public void AddMatch(string text, string filename, int line, int startIndex, int endIndex)
        {
            matches.Add(new StringMatch(text, filename, line, startIndex, endIndex));
        }

        public StringMatchList Intersect(StringMatchList other)
        {
            StringMatchList list = new StringMatchList();

            foreach (StringMatch s1 in matches)
            {
                foreach (StringMatch s2 in other.matches)
                {
                    if (s1.Matches(s2))
                    {
                        list.AddMatch(s1);
                    }
                }
            }

            return list;
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
