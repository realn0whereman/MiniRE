using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Evaluator.AST_New;
using Evaluator;
using Scanner;

namespace MiniRe
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid usage.  \n\nUsage:  MiniRE.exe <scriptfile>");
                return;
            }

            LL1Parser ll1p = new LL1Parser(args[0]);
            Node astRoot = ll1p.doParse();
            SymbolTable st = new SymbolTable();
            astRoot.Execute(st);

            Console.Read();

        }
    }
}
