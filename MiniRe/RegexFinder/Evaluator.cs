using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NFA2DFA;
using GraphLibrary;
using RDParser;

namespace RegexFinder
{
    public class Evaluator
    {
        private static String grammar = "../../../Grammar.txt";

        public static void Find(String regex, String input)
        {
            StringBuilder file = new StringBuilder();
            using (FileStream fs = new FileStream("../../../Grammar.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        file.AppendLine(sr.ReadLine());
                    }
                }
            }
            RDMain parser = new RDMain(file.ToString());
            Graph nfa = parser.doParse();
            nfa.AddIndividualCharacters(parser.CharacterClasses);

            //convert nfa into dfa
            Converter converter = new Converter(nfa, "");
            converter.convertToDFA(nfa.StartVertex);
            Graph dfa = converter.table.createGraph();



            Console.ReadLine();
        }

        private static void TestInputFile(String path, Graph dfa)
        {
            try
            {
                StringBuilder input = new StringBuilder();
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            input.AppendLine(sr.ReadLine());
                        }
                    }
                }

                input = input.Replace("\n", "");
                input = input.Replace("\r", "");

                List<String> tokensFound = dfa.FindTokens(input.ToString());
                foreach (String token in tokensFound)
                {
                    Console.WriteLine(token + " was found.");
                }
                if (tokensFound.Count == 0)
                    Console.WriteLine("No tokens found.");
            }
            catch (FileNotFoundException err)
            {
                Console.Error.WriteLine("Couldn't open input for reading.");
                Environment.Exit(1);
            }
        }
      

    }
}
