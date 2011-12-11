using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StatementListTail : Node
    {
        Statement statement;
        StatementListTail tail;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if(statement != null)
                statement.Execute(table);
            if(tail != null)
                tail.Execute(table);

            return null;
        }

        public StatementListTail Tail
        {
            get { return tail; }
            set { tail = value; }
        }
        public Statement Statement
        {
            get { return statement; }
            set { statement = value; }
        }
    }
}
