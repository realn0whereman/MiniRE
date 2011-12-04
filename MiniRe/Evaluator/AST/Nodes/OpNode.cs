using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;

namespace Evaluator.AST.Nodes
{
    /// <summary>
    /// A node that performs an operation. Ops: len, print, find, diff, union, intersec, maxfreqstr, replace, recursivereplace, lookup
    /// </summary>
    public class OpNode : Node
    {
        Ops operation;
        List<Node> args;


        public OpNode() { }
        public OpNode(Ops operation, params Node[] args)
        {
            this.operation = operation;
            if (args.Length == 0)
            {
                this.args = new List<Node>();
            }
            else
            {
                this.args = args.ToList();
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
                    break;
                default:
                    throw new NotImplementedException();
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
