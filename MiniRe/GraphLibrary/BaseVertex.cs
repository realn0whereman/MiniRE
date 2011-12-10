using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    public class BaseVertex
    {
        List<Edge> connectsTo;
        public bool visited;
        bool isAccepting;
        string name;

        /// <summary>
        /// Creates a new vertex with a given name and no connections.  
        /// </summary>
        /// <param name="name"></param>
        public BaseVertex(string name, bool accepting)
        {
            this.name = name;
            this.isAccepting = accepting;
            connectsTo = new List<Edge>();
            this.visited = false;
        }

        /// <summary>
        /// The name of this vertex
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Whether the vertex is an accepting state.  
        /// </summary>
        public bool Accepting
        {
            get
            {
                return isAccepting;
            }
            set
            {
                isAccepting = value;
            }
        }

        /// <summary>
        /// The list of edges that this vertex connects to.  
        /// </summary>
        public List<Edge> Connections
        {
            get
            {
                return connectsTo;
            }
            set { connectsTo = value; }
        }


        /// <summary>
        /// Removes all connections from this Vertex to a given target Vertex.
        /// </summary>
        /// <param name="target">Any connections from the current Vertex to target will be removed.</param>
        public void RemoveConnections(BaseVertex target)
        {
            for (int i = 0; i < connectsTo.Count; i++)
            {
                if (connectsTo[i].Connection.Name == target.Name)
                {
                    connectsTo.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Removes a specific Edge from this Vertex.  
        /// </summary>
        /// <param name="toRemove">Removes a specific edge from this Vertex</param>
        public void RemoveConnections(Edge toRemove)
        {
            connectsTo.Remove(toRemove);
        }

        public bool CheckNodeEquality(BaseVertex that)
        {
            if (this.visited)
            {
                return true;
            }

            this.visited = true;

            if (this.connectsTo.Count == 0 && that.connectsTo.Count == 0)
            {
                return true;
            }
            if (this.connectsTo.Count != that.connectsTo.Count)
            {
                return false;
            }

            if (this.Accepting != that.Accepting)
                return false;

            foreach (Edge left in this.connectsTo)
            {
                int countEqualEdges = 0;
                foreach (Edge right in that.connectsTo)
                {
                    if(left.ShaneEquals(right))// && left.Connection.visited == false && right.Connection.visited == false)
                    {
                        if (left.Connection.CheckNodeEquality(right.Connection))
                        {
                            countEqualEdges++;
                        }
                    }
                }
                //didn't find a matching edge for this one, so its not the same node
                //finding 2 equivalent edges doesn't imply the vertices are different
                if (countEqualEdges == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Edge> findEdgeAmongConnections(char trans)
        {
            List<Edge> list = new List<Edge>();
            string check = "" + trans;
            foreach (Edge edge in connectsTo)
            {
                if (edge.Connection.Accepting && edge.Condition == check)
                {
                    list.Add(edge);
                }
            }

            return list;
        }

        public void AddConnection(BaseVertex newNode, string transitionString, bool accepting, bool isCharClass)
        {
            Edge newEdge = new Edge(newNode, transitionString, isCharClass);
            connectsTo.Add(newEdge);
        }

        public void AddConnection(BaseVertex newNode, string transitionString, bool accepting)
        {
            Edge newEdge = new Edge(newNode, transitionString);
            connectsTo.Add(newEdge);
            this.isAccepting = accepting;
        }


        public void AddConnection(BaseVertex newNode, string transitionString)
        {
            Edge newEdge = new Edge(newNode, transitionString);
            connectsTo.Add(newEdge);
        }
    }
}
