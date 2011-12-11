using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class MiniRE : Node
    {
        StatementList statementList;

        public StatementList StatementList
        {
            get { return statementList; }
            set { statementList = value; }
        }

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            statementList.Execute(table);

            return null;
        }
    }
}
