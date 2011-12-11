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
        public void AddMatch(string text, string filename, int line, int startIndex = 0, int endIndex = 0)
        {
            matches.Add(new StringMatch(text, filename, line, startIndex, endIndex));
        }

        public void RemoveString(String s)
        {
            bool remove = false;
            StringMatch current = null;
            foreach (StringMatch match in matches)
            {
                current = match;
                if (ContainsString(s))
                {
                    remove = true;
                    break;
                }
            }

            if (remove)
                matches.Remove(current);

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
        public StringMatchList Union(StringMatchList other)
        {
            StringMatchList union = new StringMatchList();

            foreach (StringMatch match in matches)
            {
                union.AddMatch(match);
            }

            foreach (StringMatch match in other.Matches)
            {
                union.AddMatch(match);
            }

            return union;
        }
        public StringMatchList Difference(StringMatchList other)
        {
            StringMatchList union = new StringMatchList();

            foreach (StringMatch match in matches)
            {
                union.AddMatch(match);
            }

            foreach (StringMatch match in other.Matches)
            {
                if (union.ContainsString(match))
                    union.RemoveString(match.Text);
            }

            return union;

        }

        public bool ContainsString(StringMatch match)
        {
            return ContainsString(match.Text);
        }
        public bool ContainsString(String s)
        {
            foreach (StringMatch match in matches)
            {
                if (match.Text == s)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the filename for all StringMatches
        /// </summary>
        /// <returns></returns>
        public void SetFilename(String filename)
        {
            foreach (StringMatch match in matches)
            {
                match.Filename = filename;
            }
        }

        /// <summary>
        /// The length of this list
        /// </summary>
        public int Length
        {
            get { return matches.Count; }
        }

        public IEnumerable<StringMatch> Matches
        {
            get
            {
                return matches;
            }
        }

        public override string ToString()
        {

            Dictionary<String, Dictionary<String, List<StringMatch>>> groups = new Dictionary<String, Dictionary<String, List<StringMatch>>>();

            foreach (StringMatch match in matches)
            {
                if(!groups.ContainsKey(match.Text))
                {
                    groups[match.Text] = new Dictionary<string, List<StringMatch>>();
                }

                if (!groups[match.Text].ContainsKey(match.Filename))
                {
                    groups[match.Text][match.Filename] = new List<StringMatch>();
                }

                groups[match.Text][match.Filename].Add(match);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("{");


            foreach (String text in groups.Keys)
            {
                sb.Append("\"");
                sb.Append(text);
                sb.Append("\"");

                foreach (String file in groups[text].Keys)
                {
                    List<StringMatch> lines = groups[text][file];

                    sb.Append("<'");
                    sb.Append(file);
                    sb.Append("', ");

                    foreach (StringMatch match in lines)
                    {
                        sb.Append(match.Line);

                        if(match != lines[lines.Count-1])
                        {
                            sb.Append(", ");
                        }
                    }

                    sb.Append(">");
                }

            }

            //foreach(StringMatch match in matches)
            //{
            //    sb.Append(match.Filename);
            //    sb.Append("', ");
            //    sb.Append(match.Line);
            //    sb.Append(", ");
            //    sb.Append(match.StartIndex);
            //    sb.Append(", ");
            //    sb.Append(match.EndIndex);
            //}
            sb.Append("}");
            return sb.ToString();
        }


    }
}
