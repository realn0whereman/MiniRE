using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Statement : Node
    {
        AssignmentStatement assignmentStatement;
        OtherStatement otherStatement;

        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            if (assignmentStatement != null)
            {
                assignmentStatement.Execute(table);
            }
            else if (otherStatement != null)
            {
                otherStatement.Execute(table);
            }

            return null;
        }

        public AssignmentStatement AssignmentStatement
        {
            get { return assignmentStatement; }
            set { assignmentStatement = value; }
        }
        public OtherStatement OtherStatement
        {
            get { return otherStatement; }
            set { otherStatement = value; }
        }
        
    }
}
