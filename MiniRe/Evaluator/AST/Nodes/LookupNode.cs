using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST.Nodes
{
    public class LookupNode : Node
    {
        string varName;

        public LookupNode(string varName)
        {
            this.varName = varName;
        }

        public override object Execute(SymbolTable symbols)
        {
            return symbols[varName];
        }
    }
}
