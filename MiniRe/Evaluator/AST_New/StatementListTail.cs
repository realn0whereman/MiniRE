using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StatementListTail : Node
    {
        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if (Statement != null)
                Statement.Execute(table);
            if (Tail != null)
                Tail.Execute(table);

            return null;
        }

        public Statement Statement
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (Statement)Nodes[0];
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

        public StatementListTail Tail
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (StatementListTail)Nodes[1];
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
