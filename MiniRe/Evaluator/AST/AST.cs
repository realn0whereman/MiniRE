using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST
{
    /// <summary>
    /// Abstract syntax tree class
    /// </summary>
    public class AST
    {
        Node root;

        public Node Root
        {
            get { return root; }
            set { root = value; }
        }
    }
}
