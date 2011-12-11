using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.AST;
using Scanner;
using ParserGenerator;


namespace MiniRe
{
    class LL1Parser
    {
        private TokenScanner scanner;
        private ParserGenerator.ParserGenerator parsetable;

        public LL1Parser(string contents)
        {
            // TODO: Complete member initialization
            this.scanner = new TokenScanner(contents);
            parsetable = new ParserGenerator.ParserGenerator();
        }

        public AST doParse()
        {
            //% is epsilon.  
            
            Stack<string> parseStack = new Stack<string>();
            parseStack.Push("$");
            parseStack.Push("<MiniRE-program>");

            while (parseStack.Count != 0)
            {
                string topElem = parseStack.Pop();
                string nextToken = scanner.getToken();

                if (topElem.Contains('<') && topElem.Contains('>'))
                {
                    //This is a non-term, we need to look up and replace.  
                    List<string> newrule = parsetable.getRuleMatchingToken(topElem, nextToken);
                    newrule.Reverse();

                    if (newrule.Count > 0 && newrule[0] != "%")
                    {
                        foreach (string s in newrule)
                        {
                            parseStack.Push(s);
                        }
                    }

                }
                else
                {
                    //Is a terminal; must match top of stack, possibly do stuff with AST depending on token.  
                    if (topElem == nextToken)
                    {
                        //Here's where we are going to do AST stuff.  
                    }
                    else
                    {
                        throw new RDParser.SyntaxError("Token did not match top of LL(1) Parse Stack.");
                    }
                }
            }

            return null;
        }

    }
}
