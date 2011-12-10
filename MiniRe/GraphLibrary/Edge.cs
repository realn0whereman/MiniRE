using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    /// <summary>
    /// This class represents an Edge in a directed graph which is only valid when a given condition is met.  
    /// </summary>
    public class Edge
    {
        BaseVertex connectsTo;
        string validOnCondition;
        bool isCharClass;

        /// <summary>
        /// Creates a new Edge with the given target Vertex and condition
        /// </summary>
        /// <param name="nextVertex">The vertex pointed to by this edge.  </param>
        /// <param name="condition">The condition which must be true for this vertex to be valid.</param>
        public Edge(BaseVertex nextVertex, string condition)
        {
            connectsTo = nextVertex;
            validOnCondition = condition;
            isCharClass = false;
        }


        /// <summary>
        /// Creates a new Edge with the given target Vertex and condition
        /// </summary>
        /// <param name="nextVertex">The vertex pointed to by this edge.  </param>
        /// <param name="condition">The condition which must be true for this vertex to be valid.</param>
        public Edge(BaseVertex nextVertex, string condition, bool isCharClass)
        {
            connectsTo = nextVertex;
            validOnCondition = condition;
            this.isCharClass = isCharClass;
        }

        /// <summary>
        /// The vertex pointed to by the edge
        /// </summary>
        public BaseVertex Connection
        {
            get
            {
                return connectsTo;
            }
            set
            {
                connectsTo = value;
            }
        }

        /// <summary>
        /// The condition which must be met for the edge to be valid.  
        /// </summary>
        public string Condition
        {
            get
            {
                return validOnCondition;
            }
            set
            {
                validOnCondition = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge)
            {
                Edge o = (Edge) obj;
                if (connectsTo.Name == o.Connection.Name && this.validOnCondition.Equals(o.Condition))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// If this edge has the same condition
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ShaneEquals(object obj)
        {
            if (obj is Edge)
            {
                Edge that = (Edge)obj;
                if (this.Condition.Equals(that.Condition))
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsCharClass
        {
            get
            {
                return isCharClass;
            }
        }
    }
}
