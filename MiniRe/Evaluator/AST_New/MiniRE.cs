using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class MiniRE : Node
    {

        public StatementList StatementList
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (StatementList)Nodes[0];
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

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            StatementList.Execute(table);

            return null;
        }

        public override bool IsFull
        {
            get
            {
                foreach (Node n in Nodes)
                {
                    if (!(n is StringNode))
                        return true;
                }
                return false;
            }
        }
    }
}
