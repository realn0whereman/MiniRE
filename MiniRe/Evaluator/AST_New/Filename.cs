using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Filename : Node
    {
        string path = "";

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public override bool IsFull
        {
            get
            {
                return this.path != "";
            }
        }
    }
}
