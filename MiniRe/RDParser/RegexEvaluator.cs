using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GraphLibrary;
using NFA2DFA;
using Evaluator.Variables;

namespace RDParser
{
    public class RegexEvaluator
    {
        public static StringMatchList Eval(String pattern, String input)
        {
            String grammar = "%% \n%% \n$token " + pattern;

            RDMain parser = new RDMain(grammar);
            Graph nfa = parser.doParse();
            nfa.AddIndividualCharacters(parser.CharacterClasses);
            Converter converter = new Converter(nfa, "");
            converter.convertToDFA(nfa.StartVertex);
            Graph dfa = converter.table.createGraph();

            StringMatchList tokens = new StringMatchList();
            List<StringMatch> tokensFound = dfa.FindTokens(input.ToString());
            foreach (StringMatch match in tokensFound)
            {
                tokens.AddMatch(match);
            }

            return tokens;
        }
        public static String Replace(String pattern, String input, String replaceText)
        {
            String grammar = "%% \n%% \n$token " + pattern;

            RDMain parser = new RDMain(grammar);
            Graph nfa = parser.doParse();
            nfa.AddIndividualCharacters(parser.CharacterClasses);
            Converter converter = new Converter(nfa, "");
            converter.convertToDFA(nfa.StartVertex);
            Graph dfa = converter.table.createGraph();

            StringMatchList tokens = new StringMatchList();
            List<StringMatch> tokensFound = dfa.FindTokens(input.ToString());

            foreach (StringMatch match in tokensFound)
            {
                input = input.Replace(match.Text, replaceText);
            }

            return input;
        }
        //private static StringMatchList TestInputFile(String input, Graph dfa)
        //{
        //    try
        //    {
        //        StringBuilder input = new StringBuilder();
        //        using (FileStream fs = new FileStream(path, FileMode.Open))
        //        {
        //            using (StreamReader sr = new StreamReader(fs))
        //            {
        //                while (!sr.EndOfStream)
        //                {
        //                    input.AppendLine(sr.ReadLine());
        //                }
        //            }
        //        }

        //        input = input.Replace("\n", "");
        //        input = input.Replace("\r", "");

        //        StringMatchList tokens = new StringMatchList();
        //        List<StringMatch> tokensFound = dfa.FindTokens(input.ToString());
        //        foreach (StringMatch match in tokensFound)
        //        {
        //            tokens.AddMatch(match);
        //        }

        //        return tokens;
        //    }
        //    catch (FileNotFoundException err)
        //    {
        //        Console.Error.WriteLine("Couldn't open input for reading.");
        //        Environment.Exit(1);
        //    }
        //}
    }
}
