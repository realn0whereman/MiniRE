using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class ExpListTail : Node
    {
        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            List<object> objs = new List<object>();

            if (Exp != null)
                objs.Add(Exp.Execute(table));
            if (Tail != null)
                objs.Add((List<Object>)Tail.Execute(table));

            return objs;
        }


        public ExpListTail Tail
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (ExpListTail)Nodes[1];
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

        public Exp Exp
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (Exp)Nodes[0];
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

        public override bool IsFull
        {
            get
            {
                return Nodes.Count == 2;
            }
        }
    }
}
