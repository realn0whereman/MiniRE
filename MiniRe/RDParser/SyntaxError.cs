using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDParser
{
    public class SyntaxError : System.Exception
    {
        public SyntaxError()
        {
        }

        public SyntaxError(string message)
            : base(message)
        {
        }
    }
}
