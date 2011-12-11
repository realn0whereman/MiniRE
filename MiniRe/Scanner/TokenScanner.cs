using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using RDParser;

namespace Scanner
{
    public class TokenScanner
    {
        private Queue<String> tokens;
        private String fileName;

        public TokenScanner()
        {
            fileName = null;
            tokens = null;
        }

        public TokenScanner(String fileName)
        {
            this.fileName = fileName;
            this.tokens = new Queue<String>();
            scanFile();
        }

        private void scanFile()
        {
            int index = 0;

            try
            {
                String[] srcFile = File.ReadAllLines(this.fileName);
                for (; index < srcFile.Length; index++)
                {
                    int stop = 0;
                    String line = srcFile[index];
                    StringBuilder slab = new StringBuilder();
                    while (stop < line.Length)
                    {
                        switch (line[stop])
                        {
                            //ASCII String bounded by single quotes
                            case '"':
                                stop++;
                                slab.Append("\"");
                                while(line[stop] != '\"')
                                {
                                    slab.Append(line[stop]);
                                    stop++;
                                }
                                slab.Append("\"");
                                stop++;
                                tokens.Enqueue(slab.ToString());
                                slab.Clear();
                                break;

                            //REGEX
                            case '\'':
                                slab.Append("\'");
                                stop++;
                                while(line[stop] != '\'')
                                {
                                    slab.Append(line[stop]);
                                    stop++;
                                }
                                slab.Append("\'");
                                stop++;
                                tokens.Enqueue(slab.ToString());
                                slab.Clear();
                                break;

                            case '(':
                                tokens.Enqueue("(");
                                break;

                            case ')':
                                tokens.Enqueue(")");
                                break;
                            
                            case ';':
                                tokens.Enqueue(";");
                                break;
                            
                            case '=':
                                tokens.Enqueue("=");
                                break;
                            
                            case ' ':
                                break;

                            case '\n':
                                break;

                            case '\t':
                                break;

                            case '\r':
                                break;

                            default:
                                int count = 9;
                                if(isaLetter(line[stop]))
                                {
                                    slab.Append(line[stop]);
                                    stop++;

                                    while(isValidIDChar(line[stop]) && count > 0)
                                    {
                                        slab.Append(line[stop]);
                                        stop++;
                                        count--;
                                    }
                                    if (count == 0)
                                    {
                                        throw new SyntaxError("Identifier to long. Line number: " + (index + 1) + " Word starts with: " + slab);
                                    }
                                    else
                                    {
                                        tokens.Enqueue(slab.ToString());
                                        slab.Clear();
                                    }
                                }
                                else
                                {
                                    throw new LexicalException("Lexical Exception! Problem is on line: " + (index + 1) + " char: " + stop);
                                }
                                break;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                System.Console.WriteLine("ERROR File not found. Given name was: " + this.fileName);
                Environment.Exit(1);
            }
            catch (IOException ioe)
            {
                System.Console.WriteLine("ERROR and IO Exception has occured while scanning the file. Exception Method:");
                System.Console.WriteLine(ioe.Message);
                System.Console.WriteLine("");
                System.Console.WriteLine("Stack Trace:");
                System.Console.WriteLine(Environment.StackTrace);
                Environment.Exit(1);
            }
            catch (IndexOutOfRangeException)
            {
                System.Console.WriteLine("Syntax error on line: " + (index + 1) + " missed \" or \'.");
                Environment.Exit(1);
            }
        }

        public String getToken()
        {
            try
            {
                return tokens.Dequeue();
            }
            catch(InvalidOperationException)
            {
                return "$";
            }
        }

        public String peekToken()
        {
            try
            {
                return tokens.Peek();
            }
            catch (InvalidOperationException)
            {
                return "$";
            }
        }

        public bool isaLetter(char letter)
        {
            if ((letter >= 65 && letter <= 90) || (letter >= 97 && letter <= 122))
            {
                return true;
            }

            return false;
        }

        public bool isaDigit(char digit)
        {
            if (digit >= 48 && digit <= 57)
            {
                return true;
            }

            return false;
        }

        public bool isValidIDChar(char letter)
        {
            return isaDigit(letter) || isaLetter(letter) || letter == '_';
        }
    }
}
