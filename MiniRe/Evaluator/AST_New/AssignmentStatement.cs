using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class AssignmentStatement : Node
    {
        AssignmentStatementType type = AssignmentStatementType.Expression;


        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if (type == AssignmentStatementType.Expression)
            {
                return Exp.Execute(table);
            }
            else if (type == AssignmentStatementType.Length)
            {
                return ((StringMatchList)Exp.Execute(table)).Matches.Count();
            }
            else if (type == AssignmentStatementType.MaxFreqString)
            {
                StringMatchList matches = (StringMatchList) table[Id.Token];
                Dictionary<String, int> counts = new Dictionary<string, int>();

                foreach (StringMatch match in matches.Matches)
                {
                    if (counts.ContainsKey(match.Text))
                        counts[match.Text]++;
                    else
                        counts[match.Text] = 1;
                }

                int max = Int16.MinValue;
                String maxString = "";

                foreach (String key in counts.Keys)
                {
                    if (counts[key] > max)
                    {
                        max = counts[key];
                        maxString = key;
                    }
                }

                return maxString;


            }

            return null;
        }

        public Exp Exp
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (Exp)Nodes[0];
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 1)
                    Nodes.Add(null);
                Nodes[0] = value;
            }
        }
        public StringNode Id
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (StringNode)Nodes[0];
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 1)
                    Nodes.Add(null);
                Nodes[0] = value;
            }
        }
        public AssignmentStatementType Type
        {
            get { return type; }
            set { type = value; }
        }

        public override bool IsFull
        {
            get
            {
                return Nodes.Count == 1;
            }
        }
    }

    public enum AssignmentStatementType
    {
        Expression,
        Length,
        MaxFreqString,
    }
}
