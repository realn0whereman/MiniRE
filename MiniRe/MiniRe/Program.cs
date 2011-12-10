using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            StreamReader reader = new StreamReader(args[0]);

            string contents = reader.ReadToEnd();

            LL1Parser ll1p = new LL1Parser(contents);


        }
    }
}
