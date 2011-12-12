using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RDParser;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class Term : Node
    {
        public override object Execute(SymbolTable table)
        {
            StringBuilder filetext = new StringBuilder();
            using (FileStream fs = new FileStream(Filename.Path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    filetext.Append(sr.ReadToEnd());
                }
            }

            StringMatchList matches = RegexEvaluator.Eval(Regex.Pattern, filetext.ToString());
            matches.SetFilename(Filename.Path);
            return matches;

        }

        public Filename Filename
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    if (Nodes[1] is Filename)
                        return (Filename)Nodes[1];
                    else
                        return null;
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 2)
                    Nodes.Add(null);
                Nodes[1] = value;
            }
        }

        public Regex Regex
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    if (Nodes[0] is Regex)
                        return (Regex)Nodes[0];
                    else
                        return null;
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

        public override bool IsFull
        {
            get
            {
                return Nodes.Count == 2;
            }
        }
    }
}
