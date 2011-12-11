using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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


        }
    }
}
