﻿using System;
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

            }

            return null;
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

            StringMatchList matches = RegexEvaluator.Eval(regex.Pattern, replaceText);
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
    }

    public enum OtherStatementMode
    {
        Replace,
        RecursiveReplace,
        Print
    }
}
