using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class BinOp : Node
    {
        public StringNode Operation
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (StringNode)Nodes[0];
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
                return Nodes.Count == 1;
            }
        }
    }
}
