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
            Node miniRE = new Node();  

            while (tokens.Count != 0)
            {
                string topElem = tokens.Pop();
                string nextToken = scanner.peekToken();

                if (nextToken == "$")
                {
                    if (topElem == "$")
                        break;
                    else
                        throw new RDParser.SyntaxError("All tokens not popped off stack");
                }

                if (topElem.Contains('<') && topElem.Contains('>'))
                {
                    //This is a non-term, we need to look up and replace.  
                    List<string> newrule;
                    try
                    {
                        if (isID(nextToken))
                            newrule = parsetable.getRuleMatchingToken(topElem, "ID");
                        else if(isREGEX(nextToken))
                            newrule = parsetable.getRuleMatchingToken(topElem, "REGEX");
                        else if(isASCIISTR(nextToken))
                            newrule = parsetable.getRuleMatchingToken(topElem, "ASCII-STR");
                        else
                            newrule = parsetable.getRuleMatchingToken(topElem, nextToken);
                    }
                    catch (KeyNotFoundException e)
                    {
                        throw new RDParser.SyntaxError("Invalid token in position.");
                    }
                    //Take the existing rule, create its node, add to top elem on stack, pop top elem if full.  

                    Node newElem = GetNodeFromRuleName(topElem);
                    if (!(newrule.Count == 0 && newrule[0] == "%"))
                    {
                        if (nodes.Count != 0)
                            nodes.Peek().Nodes.Add(newElem);

                        if (nodes.Count != 0 && nodes.Peek().IsFull && nodes.Peek() is MiniRE)
                        {
                            miniRE = nodes.Pop();
                        }

                        nodes.Push(newElem);
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
                        StringNode sn = new StringNode();
                        sn.Token = "%";
                        newElem.Nodes.Add(sn);
                        nodes.Peek().Nodes.Add(newElem);

                        if (nodes.Peek().IsFull && nodes.Peek() is MiniRE)
                            miniRE = nodes.Pop();
                    }

                }
                else
                {
                    nextToken = scanner.getToken();
                    //Is a terminal; must match top of stack, possibly do stuff with AST depending on token.  
                    if (topElem == nextToken)
                    {
                        switch (nextToken)
                        {
                            case "maxfreqstring":
                                AssignmentStatement xyz = (AssignmentStatement)nodes.Peek();
                                xyz.Type = AssignmentStatementType.MaxFreqString;
                                break;
                            case "#":
                                AssignmentStatement xyza = (AssignmentStatement)nodes.Peek();
                                xyza.Type = AssignmentStatementType.Length;
                                break;
                            case "replace":
                                OtherStatement abc = (OtherStatement)nodes.Peek();
                                abc.Mode = OtherStatementMode.Replace;
                                break;
                            case "print":
                                OtherStatement bcd = (OtherStatement)nodes.Peek();
                                bcd.Mode = OtherStatementMode.Print;
                                break;
                            case "recursivereplace":
                                OtherStatement cde = (OtherStatement)nodes.Peek();
                                cde.Mode = OtherStatementMode.RecursiveReplace;
                                break;
                            case "diff":
                                BinOp op1 = (BinOp)nodes.Peek();
                                StringNode n1 = new StringNode();
                                n1.Token = "diff";
                                op1.Nodes.Add(n1);
                                break;
                            case "union":
                                BinOp op2 = (BinOp)nodes.Peek();
                                StringNode n2 = new StringNode();
                                n2.Token = "union";
                                op2.Nodes.Add(n2);
                                break;
                            case "inters":
                                BinOp op3 = (BinOp)nodes.Peek();
                                StringNode n3 = new StringNode();
                                n3.Token = "inters";
                                op3.Nodes.Add(n3);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (topElem == "REGEX" && nextToken[0] == '\'' && nextToken[nextToken.Length - 1] == '\'')
                    {
                        Regex re = new Regex();
                        re.Pattern = nextToken.Substring(1, nextToken.Length - 2);
                        nodes.Peek().Nodes.Add(re);
                    }
                    else if (topElem == "ASCII-STR" && nextToken[0] == '"' && nextToken[nextToken.Length - 1] == '"')
                    {
                        Node top = nodes.Peek();
                        if (top is OtherStatement)
                        {
                            OtherStatement nd = (OtherStatement)top;
                            nd.ReplaceText = nextToken;
                        }
                        else if (top is Filename)
                        {
                            Filename fn = (Filename)top;
                            fn.Path = nextToken.Substring(1, nextToken.Length-2);
                        }
                    }
                    else if (topElem == "ID")
                    {
                        StringNode sn = new StringNode();
                        sn.Token = nextToken;
                        nodes.Peek().Nodes.Add(sn);
                    }
                    else
                    {
                        throw new RDParser.SyntaxError("Token did not match top of LL(1) Parse Stack.");
                    }
                }
            }

            return miniRE;
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
                case "bin-op":
                    return new BinOp();
                case "term":
                    return new Term();
                case "file-names":
                    return new Filenames();
                case "source-file":
                    return new Filename();
                case "destination-file":
                    return new Filename();
                case "exp-list-tail":
                    return new ExpListTail();
                case "exp-tail":
                    return new ExpTail();


            }

            return new Node();
        }

        private bool isID(string token)
        {
            List<string> badStrs = new List<string>();

            badStrs.Add("begin");
            badStrs.Add("end");
            badStrs.Add("maxfreqstring");
            badStrs.Add("replace");
            badStrs.Add("with");
            badStrs.Add("in");
            badStrs.Add("recursivereplace");
            badStrs.Add("print");
            badStrs.Add("find");
            badStrs.Add("diff");
            badStrs.Add("union");
            badStrs.Add("inters");

            if (badStrs.Contains(token))
                return false;

            foreach (char c in token)
            {
                int asciival = (int)c;

                if (asciival > 122)
                    return false;
                if (asciival < 48)
                    return false;
                if (!(asciival >= 48 && asciival <= 57) && !(asciival >= 65 && asciival <= 90) && !(asciival >= 97 && asciival <= 122) && asciival != 95)
                    return false;
            }
            return true;
        }

        private bool isASCIISTR(string token)
        {
            if (!(token[0] == '"') || !(token[token.Length - 1] == '"'))
                return false;

            return true;
        }

        private bool isREGEX(string token)
        {
            if (!(token[0] == '\'') || !(token[token.Length - 1] == '\''))
                return false;

            return true;
        }
    }

}
