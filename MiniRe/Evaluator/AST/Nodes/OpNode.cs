using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;
using RDParser;
using System.IO;

namespace Evaluator.AST.Nodes
{
    /// <summary>
    /// A node that performs an operation. Ops: len, print, find, diff, union, intersec, maxfreqstr, replace, recursivereplace, lookup
    /// </summary>
    public class OpNode : Node
    {
        Ops operation;
        List<Node> args;
        List<String> inputs;


        public OpNode() { }
        public OpNode(Ops operation, params Node[] nodeArgs)
        {
            this.operation = operation;
            if (nodeArgs.Length == 0)
            {
                this.args = new List<Node>();
            }
            else
            {
                this.args = nodeArgs.ToList();
            }

        }
        public OpNode(Ops operation, params String[] inputs)
        {
            this.operation = operation;
            if (inputs.Length == 0)
            {
                this.inputs = new List<String>();
            }
            else
            {
                this.inputs = inputs.ToList();
            }
        }



        public override object Execute(SymbolTable symbols)
        {
            switch (operation)
            {
                //case Ops.print:
                //    Console.WriteLine(args[0].Execute().ToString());
                //    break;
                case Ops.len:
                    return ((StringMatchList)args[0].Execute(symbols)).Length;

                case Ops.find:
                    String pattern = inputs[0];
                    String file = ReadFile(inputs[1]);

                    StringMatchList matches = RegexEvaluator.Eval(pattern, file);
                    return matches;

                case Ops.intersec:
                    StringMatchList list1 = (StringMatchList)args[0].Execute(symbols);
                    StringMatchList list2 = (StringMatchList)args[1].Execute(symbols);

                    StringMatchList intersect = list1.Intersect(list2);
                    return intersect;
                default:
                    throw new NotImplementedException();
            }
        }

        private String ReadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    StringBuilder sb = new StringBuilder();

                    while (!sr.EndOfStream)
                    {
                        sb.AppendLine(sr.ReadLine());
                    }

                    String file = sb.ToString();

                    file = file.Replace("\n", " ");
                    file = file.Replace("\r", " ");
                    
                    return sb.ToString();
                }

            }
        }

        /// <summary>
        /// Argument Nodes needed for this operation node
        /// </summary>
        public List<Node> Args
        {
            get { return args; }
        }
    }
}
