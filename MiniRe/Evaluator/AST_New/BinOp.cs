using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class BinOp : Node
    {
        public override bool IsFull
        {
            get
            {
                return Nodes.Count == 1;
            }
        }
    }
}
