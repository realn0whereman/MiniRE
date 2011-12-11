using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class ExpList : Node
    {
        Exp exp;
        ExpListTail tail;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            List<object> objs = new List<object>();

            if (exp != null)
                objs.Add(exp.Execute(table));
            if (tail != null)
                objs.Add((List<Object>)tail.Execute(table));

            return objs;
        }

        public ExpListTail Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        public Exp Exp
        {
            get { return exp; }
            set { exp = value; }
        }
    }
}
