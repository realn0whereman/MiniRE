using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Exp : Node
    {
        string id;
        Term term;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            if (term != null)
            {
                return term.Execute(table);
            }
            else
            {
                return table[id];
            }
        }

        public Term Term
        {
            get { return term; }
            set { term = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
