using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class Exp : Node
    {
        string id;
        Term term;
        ExpTail tail;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if (term != null) //<exp> -> <term><exp-tail>
            {
                if (tail != null && tail.Binop != null )
                {
                    return ExecuteBinop(table);
                }

                object termResult = term.Execute(table);
                return termResult;
            }
            else //<exp> -> ID
            {
                return table[id];
            }
        }

        private object ExecuteBinop(SymbolTable table)
        {
            String op = tail.Binop.Operation.Token;

            switch (op)
            {
                case "diff":
                    {
                        StringMatchList term1 = (StringMatchList)term.Execute(table);
                        StringMatchList term2 = (StringMatchList)tail.Term.Execute(table);

                        return term1.Difference(term2);
                    }
                case "union":
                    {
                        StringMatchList term1 = (StringMatchList)term.Execute(table);
                        StringMatchList term2 = (StringMatchList)tail.Term.Execute(table);

                        return term1.Union(term2);
                    }
                case "inters":
                    {
                        StringMatchList term1 = (StringMatchList)term.Execute(table);
                        StringMatchList term2 = (StringMatchList)tail.Term.Execute(table);

                        return term1.Intersect(term2);
                    }
            }

            return null;
        }

        public Term Term
        {
            get { return term; }
            set { term = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public ExpTail Tail
        {
            get { return tail; }
            set { tail = value; }
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
