using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class ExpTail : Node
    {
        BinOp binop;
        Term term;
        ExpTail tail;

        public ExpTail Tail
        {
            get { return tail; }
            set { tail = value; }
        }
        public Term Term
        {
            get { return term; }
            set { term = value; }
        }
        public BinOp Binop
        {
            get { return binop; }
            set { binop = value; }
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
