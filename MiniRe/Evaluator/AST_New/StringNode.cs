using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StringNode : Node
    {
        string token;

        public StringNode(String token)
        {
            this.token = token;
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
