using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scanner;
using ParserGenerator;
using Evaluator.AST_New;


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

        public Node doParse()
        {
            //% is epsilon.  
            
            Stack<string> tokens = new Stack<string>();
            tokens.Push("$");
            tokens.Push("<MiniRE-program>");
            
            Stack<Node> nodes = new Stack<Node>();

            while (tokens.Count != 0)
            {
                string topElem = tokens.Pop();
                string nextToken = scanner.peekToken();

                if (topElem.Contains('<') && topElem.Contains('>'))
                {
                    //This is a non-term, we need to look up and replace.  
                    List<string> newrule = parsetable.getRuleMatchingToken(topElem, nextToken);

                    //Take the existing rule, create its node, add to top elem on stack, pop top elem if full.  

                    Node newElem = GetNodeFromRuleName(topElem);
                    if (nodes.Count != 0)
                    {
                        nodes.Peek().Nodes.Add(newElem);
                    }

                    newrule.Reverse();

                    if (newrule.Count > 0 && newrule[0] != "%")
                    {
                        foreach (string s in newrule)
                        {
                            tokens.Push(s);
                        }
                    }

                }
                else
                {
                    nextToken = scanner.getToken();
                    //Is a terminal; must match top of stack, possibly do stuff with AST depending on token.  
                    if (topElem == nextToken)
                    {
                        //Here's where we are going to do AST stuff.  
                    }
                    else
                    {
                        //throw new RDParser.SyntaxError("Token did not match top of LL(1) Parse Stack.");
                    }
                }
            }

            return null;
        }


        private Node GetNodeFromRuleName(string name)
        {
            name = name.Substring(1, name.Length - 2);

            switch (name)
            {
                case "MiniRE-program":
                    return new MiniRE();
                case "statement-list":
                    return new StatementList();
                case "statement-list-tail":
                    return new StatementListTail();
                case "statement":
                    return new Statement();
                case "assign-statement":
                    return new AssignmentStatement();
                case "other-statement":
                    return new OtherStatement();
                case "file-name":
                    return new Filename();
                case "exp":
                    return new Exp();
                case "exp-list":
                    return new ExpList();

            }

            return new Node();
        }
    }
}
