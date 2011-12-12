using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Filenames : Node
    {
        public Filename Destimation
        {
            get
            {
                if (Nodes.Count >= 2)
                {
                    return (Filename)Nodes[1];
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
        public Filename Filename
        {
            get
            {
                if (Nodes.Count >= 1)
                {
                    return (Filename)Nodes[0];
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

        public override bool IsFull
        {
            get
            {
                return Nodes.Count == 2;
            }
        }
    }
}
