using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Queue;
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
            try
            {
                String[] srcFile = File.ReadAllLines(this.fileName);
                for (int index = 0; index < srcFile.Length; index++)
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

                                break;

                            //REGEX
                            case '\'':

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
            }
            catch (IndexOutOfRangeException ioore)
            {
                System.Console.WriteLine("Syntax error");

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
