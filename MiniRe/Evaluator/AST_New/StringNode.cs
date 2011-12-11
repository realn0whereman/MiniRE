using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class StringNode : Node
    {
        string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }


    }
}
