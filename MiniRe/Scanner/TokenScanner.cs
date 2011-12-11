using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Scanner
{
    public class TokenScanner
    {
        private Queue<String> tokens;
        private String fileName;

        public TokenScanner(String fileName)
        {
            this.fileName = fileName;
            this.tokens = new Queue<String>();
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
                        }
                    }
                }
            }
            //catch (FileNotFoundException fnfe)
            //{
            //    System.Console.WriteLine("ERROR File not found. Given name was: " + this.fileName);
            //    Environment.Exit(1);
            //}
            catch (IOException ioe)
            {
                System.Console.WriteLine("ERROR and IO Exception has occured while scanning the file. Exception Method:");
                System.Console.WriteLine(ioe.Message);
                System.Console.WriteLine("");
                System.Console.WriteLine("Stack Trace:");
                System.Console.WriteLine(Environment.StackTrace);
                Environment.Exit(1);
            }
            catch (IndexOutOfRangeException ioore)
            {

                System.Console.WriteLine("Syntax error on line: " + index + " missed \" or \'.");
                Environment.Exit(1);
            }
        }

        public String getToken()
        {
            return tokens.Dequeue();
        }

        public String peekToken()
        {
            return tokens.Peek();
        }
    }
}
