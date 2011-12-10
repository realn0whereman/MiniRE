using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST.Nodes
{
    public class RegexNode : Node
    {
        public RegexNode()
        {
        }
        public override object Execute(SymbolTable symbols)
        {
            return base.Execute(symbols);
        }
    }
}
