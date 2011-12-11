using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StatementList : Node
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

        public Statement Statement
        {
            get { return statement; }
            set { statement = value; }
        }

        public StatementListTail Tail
        {
            get { return tail; }
            set { tail = value; }
        }

        public override bool IsFull
        {
            get
            {
                int count = 0;
                foreach (Node n in Nodes)
                {
                    if (!(n is StringNode))
                        count++;
                }
                if (count == 2)
                    return true;
                else
                    return false;
            }
        }
    }
}
