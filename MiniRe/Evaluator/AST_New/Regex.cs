﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST_New
{
    public class Regex : Node
    {
        string pattern;

        public string Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }
    }
}
