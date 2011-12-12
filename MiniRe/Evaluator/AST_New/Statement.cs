using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Statement : Node
    {   
        public override object Execute(SymbolTable table)
        {
            base.Execute(table);
            if (AssignmentStatement != null)
            {
                table[Id.Token] = AssignmentStatement.Execute(table);
            }
            else if (OtherStatement != null)
            {
                OtherStatement.Execute(table);
            }

            return null;
        }

        public AssignmentStatement AssignmentStatement
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (AssignmentStatement)Nodes[1];
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
        public OtherStatement OtherStatement
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (OtherStatement)Nodes[0];
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
                    return (StringNode)Nodes[0];
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
        
    }
}
