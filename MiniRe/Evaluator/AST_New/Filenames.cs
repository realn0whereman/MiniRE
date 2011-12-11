using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Filenames : Node
    {
        Filename filename;
        Filename destimation;



        public Filename Destimation
        {
            get { return destimation; }
            set { destimation = value; }
        }
        public Filename Filename
        {
            get { return filename; }
            set { filename = value; }
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
