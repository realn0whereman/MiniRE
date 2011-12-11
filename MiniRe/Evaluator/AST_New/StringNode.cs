using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StringNode : Node
    {
        string token;
        private string p;

        public StringNode(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
        public StringNode()
        {
            this.p = "";
        }

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        public override bool IsFull
        {
            get
            {
                return true;
            }
        }
    }
}
