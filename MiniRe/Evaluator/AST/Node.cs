using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST
{
    /// <summary>
    /// Node in an AST
    /// </summary>
    public  class Node
    {
        Node parent;
        List<Node> children;

        public Node()
        {
            children = new List<Node>();
        }

        /// <summary>
        /// Executes the script for this node and returns a value if applicable
        /// </summary>
        public virtual object Execute(SymbolTable symbols)
        {
            return null;
        }


        /// <summary>
        /// The parent Node to this Node
        /// </summary>
        public Node Parent
        {
            get { return parent; }
        }
        /// <summary>
        /// The child Nodes for this Node
        /// </summary>
        public List<Node> Children
        {
            get { return children; }
        }
    }
}
