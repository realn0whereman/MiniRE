using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evaluator.Variables;

namespace Evaluator.AST_New
{
    public class AssignmentStatement : Node
    {
        string id;
        Exp exp;
        AssignmentStatementType type = AssignmentStatementType.Expression;


        public override object Execute(SymbolTable table)
        {
            base.Execute(table);

            if (type == AssignmentStatementType.Expression)
            {
                table[id] = exp.Execute(table);
            }
            else if (type == AssignmentStatementType.Length)
            {
                table[id] = ((StringMatchList)exp.Execute(table)).Matches.Count();
            }
            else if (type == AssignmentStatementType.MaxFreqString)
            {

            }

            return null;
        }

        public Exp Exp
        {
            get { return exp; }
            set { exp = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public AssignmentStatementType Type
        {
            get { return type; }
            set { type = value; }
        }
    }

    public enum AssignmentStatementType
    {
        Expression,
        Length,
        MaxFreqString,
    }
}
