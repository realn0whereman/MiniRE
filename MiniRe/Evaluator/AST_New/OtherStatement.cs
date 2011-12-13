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
        string replaceText;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            switch (mode)
            {
                case OtherStatementMode.Print:
                    List<object> objs = (List<object>)ExpList.Execute(table);
                    if(objs != null)
                    {
                        foreach(object obj in objs)
                        {
                            if(!(obj is List<object>))
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
            using (FileStream fs = new FileStream(Filenames.Filename.Path, FileMode.Open))
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
                output = RegexEvaluator.Replace(Regex.Pattern, output, replaceText);
                matches = RegexEvaluator.Eval(Regex.Pattern, output);
            }
           

            using (FileStream fs = new FileStream(Filenames.Destimation.Path, FileMode.Create))
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
            using (FileStream fs = new FileStream(Filenames.Filename.Path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    filetext.Append(sr.ReadToEnd());
                }
            }

            String output = RegexEvaluator.Replace(Regex.Pattern, filetext.ToString(), replaceText);

            using (FileStream fs = new FileStream(Filenames.Destimation.Path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(output);
                }
            }
        }

        public Regex Regex
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (Regex)Nodes[0];
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
        public string ReplaceText
        {
            get { return replaceText; }
            set { replaceText = value; }
        }
        public ExpList ExpList
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (ExpList)Nodes[0];
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
        public OtherStatementMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public Filenames Filenames
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (Filenames)Nodes[1];
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

        public override bool IsFull
        {
            get
            {
                if (Nodes.Count == 0)
                    return false;

                if (this.Mode == OtherStatementMode.Print)
                    return Nodes.Count == 1;
                if(this.Mode == OtherStatementMode.RecursiveReplace || this.Mode == OtherStatementMode.Replace)
                    return Nodes.Count == 2;

                throw new Exception("SHOULD NEVER, EVER HAPPEN.");

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
