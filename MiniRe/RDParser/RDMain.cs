using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphLibrary;

namespace RDParser
{
    
    /// <summary>
    /// Entry point to the Recursive Descent Parser.  
    /// </summary>
    public partial class RDMain
    {
        private string[] lines;
        private Dictionary<string, Graph> tokenDefinitions;
        private Dictionary<string, Graph> characterClasses;
        private List<string> parseOrder;
        private Graph lineGraph;

        private ParsingMode parsingMode;


        public RDMain(string fileContents)
        {
            lines = fileContents.Split('\n');
            tokenDefinitions = new Dictionary<string, Graph>();
            parseOrder = new List<string>();
            tokenBuffer = "";
            inIdent = false;
            tokenPosition = 0;
            charRanges = new List<Tuple<char, char>>();

            StringBuilder allChar = new StringBuilder();
            for (int i = 32; i <= 126; i++)
            {
                allChar.Append((char)i);
            }
            allChar.Remove(allChar.ToString().IndexOf('\\'), 1);
            allChar.Remove(allChar.ToString().IndexOf('^'), 1);
            allChar.Remove(allChar.ToString().IndexOf('-'), 1);
            allChar.Remove(allChar.ToString().IndexOf('['), 1);
            allChar.Remove(allChar.ToString().IndexOf(']'), 1);

            this.CLS_CHAR_NO_SPECIALS = allChar.ToString();
            this.doDelaySet = false;
            charSetString = "";

            characterClasses = new Dictionary<string, Graph>();
            parsingMode = ParsingMode.None;
            this.parenCounter = 0;
        }

        public Graph doParse()
        {
            Dictionary<String, Graph> nfas = new Dictionary<string, Graph>();
            foreach (string line in lines)
            {
                Tuple<String, Graph> g = parseLine(line);
            }

            Graph giantNFA = new Graph();
            giantNFA.StartVertex.Accepting = false;

            foreach (KeyValuePair<String, Graph> pair in tokenDefinitions)
            {
                giantNFA.CombineGraphs(pair.Value, false);
            }

            return giantNFA;
        }
        
        /// <summary>
        /// Parse a line in the character class definition and token definition file
        /// </summary>
        /// <param name="line"></param>
        private Tuple<String, Graph> parseLine(string line)
        {
            line = line.Trim();

            if (line.Length == 0)
                return null;

            //switch parsing modes when we find a header for a new section
            if (line.Substring(0, 2) == "%%")
            {
                if (parsingMode == ParsingMode.None)
                    parsingMode = ParsingMode.CharacterClasses;
                else if (parsingMode == ParsingMode.CharacterClasses)
                    parsingMode = ParsingMode.TokenDefinitions;

                //skip to next line after getting rid of the comment line
                return null;
            }

            if(line.Substring(0,1) == "$")
            {
                //get char class name
                int index = 1;
                while(line[index] != ' ' && line[index] != '\t')
                {
                    index++;
                }
                string charClassName = line.Substring(1, index-1);

                //get char class definition text
                String charClassDef = line.Substring(index).Trim();
                
                //try parsing character class definition
                lineGraph = new Graph();
                tokenBuffer = charClassDef;
                tokenPosition = 0;

                Regexp();

                //insert name/NFA into the correct dictionary depending on if we are parsing character classes or token definitions
                if(parsingMode == ParsingMode.CharacterClasses)
                {
                    characterClasses.Add(charClassName, lineGraph);
                }
                else if(parsingMode == ParsingMode.TokenDefinitions)
                {
                    tokenDefinitions.Add(charClassName, lineGraph);   
                }

                return new Tuple<string, Graph>(charClassName, lineGraph);
            }
            return null;
        }

        private void AddNewAcceptingVertex(string transString, bool charClass)
        {
            List<BaseVertex> accepting = lineGraph.Accepting;
            BaseVertex newV = lineGraph.CreateNewVertex(true);

            for (int i = 0; i < accepting.Count; i++)
            {
                accepting[i].Accepting = false;
                accepting[i].AddConnection(newV, transString, false, charClass);
            }
        }

        public Graph LineGraph
        {
            get
            {
                return this.lineGraph;
            }

            set
            {
                this.lineGraph = value;
            }
        }

        public Graph MergeIntoGiantNFA()
        {
            Graph g = new Graph();
            return g;
        }

        public Dictionary<String, Graph> CharacterClasses
        {
            get { return characterClasses; }
        }
    }

    enum ParsingMode
    {
        CharacterClasses,
        TokenDefinitions,
        None,
    }
}
