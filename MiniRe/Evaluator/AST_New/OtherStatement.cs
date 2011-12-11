using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class OtherStatement : Node
    {
        OtherStatementMode mode;
        Regex regex;
        string replaceText;
        ExpList expList;

        public override object Execute(SymbolTable table)
        {
            return base.Execute(table);
            switch (mode)
            {
                case OtherStatementMode.Print:
                    Console.WriteLine(expList.Execute(table).ToString());
                    break;

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
    }

    public enum OtherStatementMode
    {
        Replace,
        RecursiveReplace,
        Print
    }
}
