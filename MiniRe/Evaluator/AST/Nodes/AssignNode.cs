using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST.Nodes
{
    /// <summary>
    /// A node that assigns a symbol a certain value
    /// </summary>
    public class AssignNode : Node
    {
        string varName;
        Node expression;

        public AssignNode(string varName, Node expression)
        {
            this.varName = varName;
            this.expression = expression;
        }

        public override object Execute(SymbolTable symbols)
        {
            symbols[varName] = expression.Execute(symbols);
            return symbols[varName];
        }
    }
}
