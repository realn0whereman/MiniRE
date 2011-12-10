using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ParserGenerator
{
    public class ParserGenerator
    {
        public enum rules { };
        Dictionary<String, Dictionary<String, List<String>>> parseTable;
        StreamReader grammarFile;
        public ParserGenerator() {
            constructParseTable();
        }

        private void constructParseTable() {
            parseTable.Add("<MiniRE-program>",
                new Dictionary<String, List<String>>()
            );
            parseTable["<MiniRE-program>"].Add("begin", new List<String>());
            parseTable["<MiniRE-program>"]["begin"].Add("begin");
            parseTable["<MiniRE-program>"]["begin"].Add("<statement-list>");
            parseTable["<MiniRE-program>"]["begin"].Add("end");
        }

        public String getRuleMatchingToken(String currentState, String token) {
            return "";
        }

        /*public void parseGrammar(){
            String line;
            String[] parts;
            List<String> terms, nonterms;
            terms = new List<String>();
            nonterms = new List<String>();
            while ((line = grammarFile.ReadLine()) != null) {
                parts = line.Split(' ');
                foreach(String s in parts){
                    if (s[0] == '<' && !terms.Contains(s)) {
                        terms.Add(s);
                        continue;
                    }

                    //if non term, add to list of non terms
                    //if term, add to list of terms
                    //construct table
                }
                Console.WriteLine();
            }
        }*/
    }
}
