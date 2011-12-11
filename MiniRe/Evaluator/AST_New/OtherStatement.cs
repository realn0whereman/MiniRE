using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RDParser;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class OtherStatement : Node
    {
        OtherStatementMode mode;
        Regex regex;
        string replaceText;
        ExpList expList;
        Filenames filenames;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            switch (mode)
            {
                case OtherStatementMode.Print:
                    List<object> objs = (List<object>)expList.Execute(table);
                    if(objs != null)
                    {
                        foreach(object obj in objs)
                        {
                            Console.WriteLine(obj.ToString());
                        }
                    }
                    break;

                case OtherStatementMode.Replace:
                    Replace();
                    break;

                case OtherStatementMode.RecursiveReplace:
                    RR();
                    break;

            }

            return null;
        }

        private void RR()
        {
            StringBuilder filetext = new StringBuilder();
            using (FileStream fs = new FileStream(filenames.Filename.Path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    filetext.Append(sr.ReadToEnd());
                }
            }

            StringMatchList matches = RegexEvaluator.Eval(Regex.Pattern, filetext.ToString());
            String output = filetext.ToString();

            while (matches.Length > 0)
            {
                output = RegexEvaluator.Replace(regex.Pattern, output, replaceText);
                matches = RegexEvaluator.Eval(Regex.Pattern, output);
            }
           

            using (FileStream fs = new FileStream(filenames.Destimation.Path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(output);
                }
            }
        }

        private void Replace()
        {
            StringBuilder filetext = new StringBuilder();
            using (FileStream fs = new FileStream(filenames.Filename.Path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    filetext.Append(sr.ReadToEnd());
                }
            }

            String output = RegexEvaluator.Replace(regex.Pattern, filetext.ToString(), replaceText);

            using (FileStream fs = new FileStream(filenames.Destimation.Path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(output);
                }
            }
        }

        public Regex Regex
        {
            get { return regex; }
            set { regex = value; }
        }
        public string ReplaceText
        {
            get { return replaceText; }
            set { replaceText = value; }
        }
        public ExpList ExpList
        {
            get { return expList; }
            set { expList = value; }
        }
        public OtherStatementMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public Filenames Filenames
        {
            get { return filenames; }
            set { filenames = value; }
        }

        public override bool IsFull
        {
            get
            {
                int count = 0;
                if (Nodes.Count == 0)
                    return false;

                StringNode info = (StringNode)Nodes[0];
                int expected;
                if (info.Token == "print")
                    expected = 1;
                else
                    expected = 3;


                foreach (Node n in Nodes)
                {
                    if (!(n is StringNode))
                        count++;
                }
                if (count == expected)
                    return true;
                else
                    return false;
            }
        }
    }

    public enum OtherStatementMode
    {
        Replace,
        RecursiveReplace,
        Print
    }
}
