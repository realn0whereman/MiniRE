using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class ExpTail : Node
    {
        public ExpTail Tail
        {
            get
            {
                if (Nodes.Count >= 3)
                {
                    if (Nodes[2] is ExpTail)
                        return (ExpTail)Nodes[2];
                    else
                        return null;
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 3)
                    Nodes.Add(null);
                Nodes[2] = value;
            }
        }
        public Term Term
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    if (Nodes[1] is Term)
                        return (Term)Nodes[1];
                    else
                        return null;
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
        public BinOp Binop
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    if (Nodes[0] is BinOp)
                        return (BinOp)Nodes[0];
                    else
                        return null;
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
                return Nodes.Count == 3;
            }
        }
    }
}
