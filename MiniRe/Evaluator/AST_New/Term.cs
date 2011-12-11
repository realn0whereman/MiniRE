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
        Regex regex;
        Filename filename;

        public override object Execute(SymbolTable table)
        {
            StringBuilder filetext = new StringBuilder();
            using (FileStream fs = new FileStream(filename.Path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    filetext.Append(sr.ReadToEnd());
                }
            }

            StringMatchList matches = RegexEvaluator.Eval(regex.Pattern, filetext.ToString());
            matches.SetFilename(filename.Path);
            return matches;

        }

        public Filename Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public Regex Regex
        {
            get { return regex; }
            set { regex = value; }
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
