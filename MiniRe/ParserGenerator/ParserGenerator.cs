using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ParserGenerator
{
    public class ParserGenerator
    {
        Dictionary<String, Dictionary<String, List<String>>> parseTable;
        public ParserGenerator() {
            parseTable = new Dictionary<string, Dictionary<string, List<string>>>();
            constructParseTable();
        }

        private void constructParseTable() {
            parseTable.Add("<MiniRE-program>", new Dictionary<String, List<String>>());
            parseTable.Add("<statement-list>", new Dictionary<String, List<String>>());
            parseTable.Add("<statement-list-tail>", new Dictionary<String, List<String>>());
            parseTable.Add("<statement>", new Dictionary<String, List<String>>());
            parseTable.Add("<assign-statement>", new Dictionary<String, List<String>>());
            parseTable.Add("<other-statement>", new Dictionary<String, List<String>>());
            parseTable.Add("<file-names>", new Dictionary<String, List<String>>());
            parseTable.Add("<source-file>", new Dictionary<String, List<String>>());
            parseTable.Add("<destination-file>", new Dictionary<String, List<String>>());
            parseTable.Add("<exp-list>", new Dictionary<String, List<String>>());
            parseTable.Add("<exp-list-tail>", new Dictionary<String, List<String>>());
            parseTable.Add("<exp>", new Dictionary<String, List<String>>());
            parseTable.Add("<exp-tail>", new Dictionary<String, List<String>>());
            parseTable.Add("<term>", new Dictionary<String, List<String>>());
            parseTable.Add("<file-name>", new Dictionary<String, List<String>>());
            parseTable.Add("<bin-op>", new Dictionary<String, List<String>>());

            parseTable["<MiniRE-program>"].Add("begin", new List<String>());
            parseTable["<MiniRE-program>"]["begin"].Add("begin");
            parseTable["<MiniRE-program>"]["begin"].Add("<statement-list>");
            parseTable["<MiniRE-program>"]["begin"].Add("end");


            parseTable["<statement-list>"].Add("ID", new List<String>());
            parseTable["<statement-list>"]["ID"].Add("<statement>");
            parseTable["<statement-list>"]["ID"].Add("<statement-list-tail>");


            parseTable["<statement-list>"].Add("replace", new List<String>());
            parseTable["<statement-list>"]["replace"].Add("<statement>");
            parseTable["<statement-list>"]["replace"].Add("<statement-list-tail>");


            parseTable["<statement-list>"].Add("recursivereplace", new List<String>());
            parseTable["<statement-list>"]["recursivereplace"].Add("<statement>");
            parseTable["<statement-list>"]["recursivereplace"].Add("<statement-list-tail>");


            parseTable["<statement-list>"].Add("print", new List<String>());
            parseTable["<statement-list>"]["print"].Add("<statement>");
            parseTable["<statement-list>"]["print"].Add("<statement-list-tail>");


            parseTable["<statement-list-tail>"].Add("ID", new List<String>());
            parseTable["<statement-list-tail>"]["ID"].Add("<statement>");
            parseTable["<statement-list-tail>"]["ID"].Add("<statement-list-tail>");


            parseTable["<statement-list-tail>"].Add("replace", new List<String>());
            parseTable["<statement-list-tail>"]["replace"].Add("<statement>");
            parseTable["<statement-list-tail>"]["replace"].Add("<statement-list-tail>");


            parseTable["<statement-list-tail>"].Add("recursivereplace", new List<String>());
            parseTable["<statement-list-tail>"]["recursivereplace"].Add("<statement>");
            parseTable["<statement-list-tail>"]["recursivereplace"].Add("<statement-list-tail>");


            parseTable["<statement-list-tail>"].Add("print", new List<String>());
            parseTable["<statement-list-tail>"]["print"].Add("<statement>");
            parseTable["<statement-list-tail>"]["print"].Add("<statement-list-tail>");


            parseTable["<statement-list-tail>"].Add("end", new List<String>());
            parseTable["<statement-list-tail>"]["end"].Add("%");


            parseTable["<statement>"].Add("ID", new List<String>());
            parseTable["<statement>"]["ID"].Add("ID");
            parseTable["<statement>"]["ID"].Add("=");
            parseTable["<statement>"]["ID"].Add("<assign-statement>");


            parseTable["<statement>"].Add("replace", new List<String>());
            parseTable["<statement>"]["replace"].Add("<other-statement>");


            parseTable["<statement>"].Add("recursivereplace", new List<String>());
            parseTable["<statement>"]["recursivereplace"].Add("<other-statement>");


            parseTable["<statement>"].Add("print", new List<String>());
            parseTable["<statement>"]["print"].Add("<other-statement>");


            parseTable["<assign-statement>"].Add("ID", new List<String>());
            parseTable["<assign-statement>"]["ID"].Add("<exp>");
            parseTable["<assign-statement>"]["ID"].Add(";");


            parseTable["<assign-statement>"].Add("(", new List<String>());
            parseTable["<assign-statement>"]["("].Add("<exp>");
            parseTable["<assign-statement>"]["("].Add(";");


            parseTable["<assign-statement>"].Add("find", new List<String>());
            parseTable["<assign-statement>"]["find"].Add("<exp>");
            parseTable["<assign-statement>"]["find"].Add(";");


            parseTable["<assign-statement>"].Add("#", new List<String>());
            parseTable["<assign-statement>"]["#"].Add("#");
            parseTable["<assign-statement>"]["#"].Add("<exp>");
            parseTable["<assign-statement>"]["#"].Add(";");


            parseTable["<assign-statement>"].Add("maxfreqstring", new List<String>());
            parseTable["<assign-statement>"]["maxfreqstring"].Add("maxfreqstring");
            parseTable["<assign-statement>"]["maxfreqstring"].Add("(");
            parseTable["<assign-statement>"]["maxfreqstring"].Add("ID");
            parseTable["<assign-statement>"]["maxfreqstring"].Add(")");
            parseTable["<assign-statement>"]["maxfreqstring"].Add(";");


            parseTable["<other-statement>"].Add("replace", new List<String>());
            parseTable["<other-statement>"]["replace"].Add("replace");
            parseTable["<other-statement>"]["replace"].Add("REGEX");
            parseTable["<other-statement>"]["replace"].Add("with");
            parseTable["<other-statement>"]["replace"].Add("ASCII-STR");
            parseTable["<other-statement>"]["replace"].Add("in");
            parseTable["<other-statement>"]["replace"].Add("<file-names>");
            parseTable["<other-statement>"]["replace"].Add(";");


            parseTable["<other-statement>"].Add("recursivereplace", new List<String>());
            parseTable["<other-statement>"]["recursivereplace"].Add("recursivereplace");
            parseTable["<other-statement>"]["recursivereplace"].Add("REGEX");
            parseTable["<other-statement>"]["recursivereplace"].Add("with");
            parseTable["<other-statement>"]["recursivereplace"].Add("ASCII-STR");
            parseTable["<other-statement>"]["recursivereplace"].Add("in");
            parseTable["<other-statement>"]["recursivereplace"].Add("<file-names>");
            parseTable["<other-statement>"]["recursivereplace"].Add(";");


            parseTable["<other-statement>"].Add("print", new List<String>());
            parseTable["<other-statement>"]["print"].Add("print");
            parseTable["<other-statement>"]["print"].Add("(");
            parseTable["<other-statement>"]["print"].Add("<exp-list>");
            parseTable["<other-statement>"]["print"].Add(")");
            parseTable["<other-statement>"]["print"].Add(";");


            parseTable["<file-names>"].Add("ASCII-STR", new List<String>());
            parseTable["<file-names>"]["ASCII-STR"].Add("<source-file>");
            parseTable["<file-names>"]["ASCII-STR"].Add(">!");
            parseTable["<file-names>"]["ASCII-STR"].Add("<destination-file>");


            parseTable["<source-file>"].Add("ASCII-STR", new List<String>());
            parseTable["<source-file>"]["ASCII-STR"].Add("ASCII-STR");


            parseTable["<destination-file>"].Add("ASCII-STR", new List<String>());
            parseTable["<destination-file>"]["ASCII-STR"].Add("ASCII-STR");


            parseTable["<exp-list>"].Add("ID", new List<String>());
            parseTable["<exp-list>"]["ID"].Add("<exp>");
            parseTable["<exp-list>"]["ID"].Add("<exp-list-tail>");


            parseTable["<exp-list>"].Add("(", new List<String>());
            parseTable["<exp-list>"]["("].Add("<exp>");
            parseTable["<exp-list>"]["("].Add("<exp-list-tail>");


            parseTable["<exp-list>"].Add("find", new List<String>());
            parseTable["<exp-list>"]["find"].Add("<exp>");
            parseTable["<exp-list>"]["find"].Add("<exp-list-tail>");


            parseTable["<exp-list-tail>"].Add(",", new List<String>());
            parseTable["<exp-list-tail>"][","].Add(",");
            parseTable["<exp-list-tail>"][","].Add("<exp>");
            parseTable["<exp-list-tail>"][","].Add("<exp-list-tail>");


            parseTable["<exp-list-tail>"].Add(")", new List<String>());
            parseTable["<exp-list-tail>"][")"].Add("%");


            parseTable["<exp>"].Add("ID", new List<String>());
            parseTable["<exp>"]["ID"].Add("ID");


            parseTable["<exp>"].Add("(", new List<String>());
            parseTable["<exp>"]["("].Add("(");
            parseTable["<exp>"]["("].Add("<exp>");
            parseTable["<exp>"]["("].Add(")");


            parseTable["<exp>"].Add("find", new List<String>());
            parseTable["<exp>"]["find"].Add("<term>");
            parseTable["<exp>"]["find"].Add("<exp-tail>");


            parseTable["<exp-tail>"].Add("ID", new List<String>());
            parseTable["<exp-tail>"]["ID"].Add("%");


            parseTable["<exp-tail>"].Add(",", new List<String>());
            parseTable["<exp-tail>"][","].Add("%");


            parseTable["<exp-tail>"].Add("diff", new List<String>());
            parseTable["<exp-tail>"]["diff"].Add("<bin-op>");
            parseTable["<exp-tail>"]["diff"].Add("<term>");
            parseTable["<exp-tail>"]["diff"].Add("<exp-tail>");


            parseTable["<exp-tail>"].Add("union", new List<String>());
            parseTable["<exp-tail>"]["union"].Add("<bin-op>");
            parseTable["<exp-tail>"]["union"].Add("<term>");
            parseTable["<exp-tail>"]["union"].Add("<exp-tail>");


            parseTable["<exp-tail>"].Add("inters", new List<String>());
            parseTable["<exp-tail>"]["inters"].Add("<bin-op>");
            parseTable["<exp-tail>"]["inters"].Add("<term>");
            parseTable["<exp-tail>"]["inters"].Add("<exp-tail>");


            parseTable["<exp-tail>"].Add("replace", new List<String>());
            parseTable["<exp-tail>"]["replace"].Add("%");


            parseTable["<exp-tail>"].Add("recursivereplace", new List<String>());
            parseTable["<exp-tail>"]["recursivereplace"].Add("%");


            parseTable["<exp-tail>"].Add("print", new List<String>());
            parseTable["<exp-tail>"]["print"].Add("%");


            parseTable["<exp-tail>"].Add("end", new List<String>());
            parseTable["<exp-tail>"]["end"].Add("%");


            parseTable["<exp-tail>"].Add(")", new List<String>());
            parseTable["<exp-tail>"][")"].Add("%");
            parseTable["<exp-tail>"].Add(";", new List<String>());
            parseTable["<exp-tail>"][";"].Add("%");


            parseTable["<term>"].Add("find", new List<String>());
            parseTable["<term>"]["find"].Add("find");
            parseTable["<term>"]["find"].Add("REGEX");
            parseTable["<term>"]["find"].Add("in");
            parseTable["<term>"]["find"].Add("<file-name>");


            parseTable["<file-name>"].Add("ASCII-STR", new List<String>());
            parseTable["<file-name>"]["ASCII-STR"].Add("ASCII-STR");


            parseTable["<bin-op>"].Add("diff", new List<String>());
            parseTable["<bin-op>"]["diff"].Add("diff");


            parseTable["<bin-op>"].Add("union", new List<String>());
            parseTable["<bin-op>"]["union"].Add("union");


            parseTable["<bin-op>"].Add("inters", new List<String>());
            parseTable["<bin-op>"]["inters"].Add("inters");
        }

        public List<String> getRuleMatchingToken(String currentState, String token) {
            return parseTable[currentState][token];
        }

        
    }
}
