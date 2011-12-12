using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class Exp : Node
    {
        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if (Term != null) //<exp> -> <term><exp-tail>
            {
                if (Tail != null && Tail.Binop != null)
                {
                    return ExecuteBinop(table);
                }

                object termResult = Term.Execute(table);
                return termResult;
            }
            else //<exp> -> ID
            {
                //if (Id == null)
                //    return null;

                return table[Id.Token];
            }
        }

        private object ExecuteBinop(SymbolTable table)
        {
            String op = Tail.Binop.Operation.Token;

            switch (op)
            {
                case "diff":
                    {
                        StringMatchList term1 = (StringMatchList)Term.Execute(table);
                        term1.SetFilename(Term.Filename.Path);

                        StringMatchList term2 = (StringMatchList)Tail.Term.Execute(table);
                        term2.SetFilename(Tail.Term.Filename.Path);

                        return term1.Difference(term2);
                    }
                case "union":
                    {
                        StringMatchList term1 = (StringMatchList)Term.Execute(table);
                        term1.SetFilename(Term.Filename.Path);

                        StringMatchList term2 = (StringMatchList)Tail.Term.Execute(table);
                        term2.SetFilename(Tail.Term.Filename.Path);

                        return term1.Union(term2);
                    }
                case "inters":
                    {
                        StringMatchList term1 = (StringMatchList)Term.Execute(table);
                        term1.SetFilename(Term.Filename.Path);

                        StringMatchList term2 = (StringMatchList)Tail.Term.Execute(table);
                        term2.SetFilename(Tail.Term.Filename.Path);

                        return term1.Intersect(term2);
                    }
            }

            return null;
        }

        public Term Term
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    if (Nodes[0] is Term)
                        return (Term)Nodes[0];
                    else
                        return null;
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 1)
                    Nodes.Add(null);
                Nodes[0] = value;
            }
        }
        public StringNode Id
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    if (Nodes[0] is StringNode)
                        return (StringNode)Nodes[0];
                    else
                        return null;
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 1)
                    Nodes.Add(null);
                Nodes[0] = value;
            }
        }
        public ExpTail Tail
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (ExpTail)Nodes[1];
                }
                else
                    return null;
            }
            set
            {
                if (Nodes.Count < 2)
                    Nodes.Add(null);
                Nodes[1] = value;
            }
        }

        public override bool IsFull
        {
            get
            {
                if (Nodes.Count == 0)
                    return false;
                if (Nodes.Count == 2)
                    return true;
                else
                {
                    if (Nodes[0] is StringNode || Nodes[0] is Exp)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}
