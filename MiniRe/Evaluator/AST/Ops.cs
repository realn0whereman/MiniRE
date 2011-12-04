using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evaluator.AST
{
    public enum Ops
    {
        len,
        print,
        find,
        diff,
        union,
        intersec,
        maxfreqstr,
        replace,
        recursivereplace,
        lookup, //look up a variable's value
    }
}
