using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RDParser;

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
                    while (!sr.EndOfStream)
                    {
                        filetext.AppendLine(sr.ReadLine());
                    }
                }
            }

            return RegexEvaluator.Eval(regex.Pattern, filetext.ToString());

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
    }
}
