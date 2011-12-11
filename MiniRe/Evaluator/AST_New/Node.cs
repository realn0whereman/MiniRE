using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Node
    {
        List<Node> nodes;



        public Node()
        {
            nodes = new List<Node>();
        }

        public virtual object Execute(SymbolTable table)
        {
            return null;
        }

        public virtual bool IsFull
        {
            get
            {
                return false;
            }
        }


        public List<Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
    }
}
