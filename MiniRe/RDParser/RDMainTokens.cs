using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphLibrary;

namespace RDParser
{
    public partial class RDMain
    {
        private string tokenBuffer;
        private int tokenPosition;
        private bool inIdent;
        private List<Tuple<char, char>> charRanges;
        private char rangeStart;
        private bool doDelaySet;
        private bool groundsetIsCharRange;
        private string charSetString;
        private int parenCounter;

        private readonly string CLS_CHAR_NO_SPECIALS;


        private void Regexp()
        {
            this.Rexp();
            if(tokenPosition != tokenBuffer.Length)
                throw new SyntaxError("Not all tokens were consumed.");
        }

        private void Rexp()
        {
            this.Rexp1();
            this.RexpPrime();
        }

        private void RexpPrime()
        {
            if (tokenPosition >= tokenBuffer.Length)
                return;
            if (tokenBuffer[tokenPosition] == '|')
            {
                tokenPosition++;
                Graph oldGraph = lineGraph;
                int oldPosition = tokenPosition;
                lineGraph = new Graph();
                Rexp1();
                RexpPrime();
                if (oldPosition == tokenPosition)
                {
                    throw new SyntaxError("Union Operator not followed by any expressions");
                }
                else
                {
                    oldGraph.CombineGraphs(lineGraph, true);
                    lineGraph = oldGraph;
                }
            }
        }

        private void Rexp1()
        {
            this.Rexp2();
            if (tokenPosition >= tokenBuffer.Length)
                return;
            this.Rexp1Prime();
        }

        private void Rexp1Prime()
        {
            int oldPosition = tokenPosition;
            Rexp2();
            if (oldPosition == tokenPosition)
                return;
            Rexp1Prime();
        }

        private void Rexp2()
        {
            if (tokenPosition >= tokenBuffer.Length)
                return;
            if (tokenBuffer[tokenPosition] == '(')
            {
                tokenPosition++;
                parenCounter++;
                Graph oldGraph = lineGraph;
                lineGraph = new Graph();
                Rexp();
                if (tokenPosition >= tokenBuffer.Length)
                    throw new SyntaxError("Not enough closing parentheses.");
                if (tokenBuffer[tokenPosition] != ')')
                {
                    throw new SyntaxError("Lexical Error. Failed to close a ) grouping");
                }
                tokenPosition++;
                parenCounter--;
                Rexp2Tail();
                oldGraph.CombineGraphs(this.lineGraph);
                this.lineGraph = oldGraph;
            }
            else if (RE_CHAR())
            {
                if (tokenPosition >= tokenBuffer.Length)
                    return;
                Rexp2Tail();
            }
            else
            {
                Rexp3();
            }
        }

        private void Rexp2Tail()
        {
            if (tokenPosition >= tokenBuffer.Length) 
                return;
            if (tokenBuffer[tokenPosition] == '*' || tokenBuffer[tokenPosition] == '+')
            {
                if (tokenBuffer[tokenPosition - 1] == ')')
                {
                    foreach (BaseVertex leaf in lineGraph.Accepting)
                    {
                        leaf.AddConnection(lineGraph.start, "");
                        if (tokenBuffer[tokenPosition] == '*')
                        {
                            
                            lineGraph.start.AddConnection(leaf, "");
                        }
                    }
                }
                else
                {
                    List<BaseVertex> temp = lineGraph.findAcceptingParents(tokenBuffer[tokenPosition-1]);
                    foreach (BaseVertex node in temp)
                    {
                        List<Edge> edges = node.findEdgeAmongConnections(tokenBuffer[tokenPosition-1]);
                        foreach (Edge connection in edges)
                        {
                            connection.Connection.AddConnection(node, "");
                            if (tokenBuffer[tokenPosition] == '*')
                            {
                                node.AddConnection(connection.Connection, "");
                            }
                        }
                    }
                }

                tokenPosition++;
            }
        }

        private void Rexp3()
        {
            CharClass();
        }

        private bool RE_CHAR()
        {
            char temp = tokenBuffer[tokenPosition];
            if (temp >= 32 && temp <= 126)
            {
                if (temp == ' ' || temp == '\t' || temp == '\r' || temp == '\n')
                {
                    tokenPosition++;
                }
                else if (temp == '*' || temp == '+' || temp == '?' || temp == '|' || temp == '[' || temp == ']' || temp == '(' || temp == ')' || temp == '.' || temp == '\'' || temp == '"' || temp == '$')
                {
                    if (parenCounter == 0 && temp == ')')
                    {
                        throw new SyntaxError("Mismatched ')' or missing escape character.");
                    }
                    return false;
                }
                else if (temp == '\\')
                {
                    temp = tokenBuffer[tokenPosition + 1];
                    if (temp == ' ' || temp == '*' || temp == '+' || temp == '?' || temp == '|' || temp == '[' || temp == ']' || temp == '(' || temp == ')' || temp == '.' || temp == '\'' || temp == '"' || temp == '\\' || temp == '$')
                    {
                        //add to the nfa
                        tokenPosition+= 2;
                        this.AddNewAcceptingVertex("" + temp, false);
                        return true;
                    }
                    else
                    {
                        throw new SyntaxError("Escaping a non special character in RE_CHAR()");
                    }
                }
                else
                {
                    //add to the nfa
                    tokenPosition++;
                    this.AddNewAcceptingVertex("" + temp, false);
                    return true;
                }
            }
            return false;
        }

        public void CharClass()
        {
            //All printables
            if (tokenBuffer[tokenPosition] == '.')
            {
                tokenPosition++;

                StringBuilder allChar = new StringBuilder();
                for (int i = 32; i <= 126; i++)
                {
                    allChar.Append((char)i);
                }

                this.AddNewAcceptingVertex(allChar.ToString(), false);
                
            }
            //Looking for char class 1
            else if (tokenBuffer[tokenPosition] == '[')
            {
                tokenPosition++;
                this.CharClass1();
            }
            //Looking for defined char class
            else if (tokenBuffer[tokenPosition] == '$')
            {
                tokenPosition++;
                string classname = this.DefinedClass();
                if (classname.Length == 0)
                    throw new SyntaxError("Invalid character class name.");


                //Refactor this such that only defined classes match here.  
                if(characterClasses.ContainsKey(classname))
                {
                    this.AddNewAcceptingVertex(classname, true);
                }
                else
                    throw new SyntaxError("Character class not defined.");

            }
            else
            {
                return;
            }
        }

        private void CharClass1()
        {
            int oldTokenPos = tokenPosition;
            charRanges = new List<Tuple<char, char>>();

            if(tokenBuffer[tokenPosition] == '^')
                this.ExcludeSet();
            else
                this.CharSetList();
            

            if (tokenPosition == oldTokenPos)
                throw new SyntaxError("No character class detected.");

        }


        private void CharSetList()
        {
            if (tokenBuffer[tokenPosition] == ']')
            {
                tokenPosition++;
                //Build NFA to accept all chars in that range.  

                StringBuilder transString = new StringBuilder();
                foreach (Tuple<char, char> range in charRanges)
                {

                    for (int i = (int)range.Item1; i <= (int)range.Item2; i++)
                    {
                        transString.Append((char)i);
                    }
                }

                //If we wind up with an empty range, we have an error.  
                if (transString.ToString().Equals(""))
                    throw new SyntaxError("Character range error: Empty range not permitted.");

                if (!doDelaySet)
                    this.AddNewAcceptingVertex(transString.ToString(), true);

                //Covers cases where we may be using this for an exclude set.  
                else
                    charSetString = transString.ToString();

                //Clear the charranges so we start with a fresh range next time.  
                this.charRanges.Clear();

                return;
            }
            else
            {
                this.Charset();
                this.CharSetList();
            }
        }
        private void Charset()
        {
            if (tokenBuffer[tokenPosition] == '\\')
            {
                tokenPosition++;
                char[] acceptable = { '\\', '^', '-', '[', ']' };
                foreach (char c in acceptable)
                {
                    if (c == tokenBuffer[tokenPosition])
                    {
                        rangeStart = tokenBuffer[tokenPosition];
                        tokenPosition++;
                        break;
                    }
                }
            }

            else if (this.CLS_CHAR_NO_SPECIALS.IndexOf(tokenBuffer[tokenPosition]) != -1)
            {
                rangeStart = tokenBuffer[tokenPosition];
                tokenPosition++;
            }
            else
                throw new SyntaxError("Current char is not a CLS_CHAR");

            this.CharSetTail();

            
        }

        private void CharSetTail()
        {
            //Epsilon case covered by default rangeEnd assignment.  
            char rangeEnd = rangeStart;
            if (tokenPosition >= tokenBuffer.Length)
                throw new SyntaxError("Reached end of string without closing bracket.");
            if (tokenBuffer[tokenPosition] == '-')
            {
                tokenPosition++;
                //Detect CLS_CHAR specials
                if (tokenBuffer[tokenPosition] == '\\')
                {
                    tokenPosition++;
                    char[] acceptable = { '\\', '^', '-', '[', ']' };
                    foreach (char c in acceptable)
                    {
                        if (c == tokenBuffer[tokenPosition])
                        {
                            rangeEnd = tokenBuffer[tokenPosition];
                            tokenPosition++;
                            break;
                        }
                    }
                    throw new SyntaxError(@"\ not followed by proper character.");
                }

                else if (this.CLS_CHAR_NO_SPECIALS.IndexOf(tokenBuffer[tokenPosition]) != -1)
                {
                    rangeEnd = tokenBuffer[tokenPosition];
                    tokenPosition++;
                }
                else
                    throw new SyntaxError("Character range operator not followed by CLS_CHAR");
            }
            Tuple<char, char> charRange = new Tuple<char, char>(rangeStart, rangeEnd);

            charRanges.Add(charRange);
        }

        private string DefinedClass()
        {
            string classname = "";
            while (tokenPosition < tokenBuffer.Count())
            {
                int token = (int)tokenBuffer[tokenPosition];
                if ((token >= 48 && token <= 57) || (token >= 65 && token <= 90) || (token >= 97 && token <= 122))
                {
                    classname += (char)token;
                    tokenPosition++;
                }
                else
                    break;
            }
            return classname;
        }

        private void ExcludeSet()
        {
            if (tokenBuffer[tokenPosition] == '^')
            {
                tokenPosition++;
                this.doDelaySet = true;

                //Grammar change: Use char-set-list instead so we can have multiple ranges.  
                CharSetList();

                string excludeString = this.charSetString;

                //Empty it out so we can check if they gave us something bad later.  
                this.charSetString = "";

                    
                string excluded = excludeString;
                if (tokenPosition + 4 >= tokenBuffer.Length)
                    throw new SyntaxError("Missing IN block from exclude set.");

                string next = tokenBuffer.Substring(tokenPosition, 4);
                if (next.Equals(" IN "))
                {
                    tokenPosition += 4;
                    this.ExcludeSetTail();

                    string groundset = this.charSetString;
                    if (this.groundsetIsCharRange)
                    {
                        Graph groundsetgraph = this.characterClasses[groundset];

                        groundset = groundsetgraph.StartVertex.Connections[0].Condition;
                    }

                    //Build transition string for this exclude set.  
                    StringBuilder excludedSet = new StringBuilder();

                    foreach (char c in groundset)
                    {
                        if (!(excluded.Contains(c)))
                            excludedSet.Append(c);
                    }

                    if (excludedSet.ToString().Equals(""))
                        throw new SyntaxError("Empty character ranges are forbidden.");
                    this.AddNewAcceptingVertex(excludedSet.ToString(), false);

                }
                else
                {
                    throw new SyntaxError(@"Missing ' IN ' from exclude set.");
                }
                this.doDelaySet = false;
            }
        }

        private void ExcludeSetTail()
        {
            //Looking for defined char class
            if (tokenBuffer[tokenPosition] == '$')
            {
                tokenPosition++;
                string classname = this.DefinedClass();
                if (classname.Length == 0)
                    throw new SyntaxError("Invalid character class name.");


                //Refactor this such that only defined classes match here.  
                if (characterClasses.ContainsKey(classname))
                {
                    this.groundsetIsCharRange = true;
                    this.charSetString = classname;
                    return;
                }
                else
                    throw new SyntaxError("Character class not defined.");

            }
            else if (tokenBuffer[tokenPosition] == '[')
            {
                tokenPosition++;
                this.groundsetIsCharRange = false;
                //Find the ground set.  
                //Grammar change:  Use char-set-list so we can have multiple ranges in the set.  
                this.CharSetList();
            }
            else
                throw new SyntaxError("Improper exclude set tail");
        }
    }
}