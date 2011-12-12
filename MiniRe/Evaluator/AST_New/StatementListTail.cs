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

        public override bool IsFull
        {
            get
            {
                int count = 0;
                if (Nodes.Count == 0)
                    return false;
                if (Nodes[0] is StringNode)
                {
                    StringNode sn = (StringNode)Nodes[0];
                    if (sn.Token == "%")
                        return true;
                }

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
