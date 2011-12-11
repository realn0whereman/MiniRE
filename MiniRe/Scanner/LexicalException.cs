using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scanner
{
    public class LexicalException : System.Exception
    {
        public LexicalException()
        :base("Lexical Exception has been thrown on an unknown line")
        {
            
        }

        public LexicalException(String message)
        :base(message)
        {

        }
    }
}
