using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GraphLibrary
{
    /// <summary>
    /// A basic representation of a Graph.  
    /// </summary>
    public class Graph
    {
        List<BaseVertex> vertices;
        public BaseVertex start;


        /// <summary>
        /// Creates a new Graph with a default start vertex Vertices.  
        /// </summary>
        public Graph()
        {
            vertices = new List<BaseVertex>();
            start = this.CreateNewVertex("start", true);
        }


        /// <summary>
        /// The list of Vertices present in this graph.  
        /// </summary>
        public List<BaseVertex> Vertices
        {
            get
            {
                return vertices;
            }
        }

        public List<BaseVertex> Accepting
        {
            get
            {
                List<BaseVertex> acceptings = new List<BaseVertex>();
                foreach (BaseVertex v in vertices)
                {
                    if (v.Accepting)
                        acceptings.Add(v);
                }
                return acceptings;

                ////Do BFS to find accept states

                //Queue<BaseVertex> nodeQueue = new Queue<BaseVertex>();
                //List<BaseVertex> visited = new List<BaseVertex>();
                //List<BaseVertex> acceptStates = new List<BaseVertex>();
                //nodeQueue.Enqueue(start);

                //while (nodeQueue.Count > 0)
                //{
                //    BaseVertex elem = nodeQueue.Dequeue();
                //    visited.Add(elem);
                //    foreach (Edge e in elem.Connections)
                //    {
                //        if (!(visited.Contains(e.Connection)))
                //        {
                //            nodeQueue.Enqueue(e.Connection);
                //        }
                //    }
                //    if (elem.Accepting)
                //    {
                //        acceptStates.Add(elem);
                //    }
                //}

                //return acceptStates;

            }
        }


        /// <summary>
        /// The starting (initial) vertex in this graph.  
        /// </summary>
        public BaseVertex StartVertex
        {
            get
            {
                return this.start;
            }
            set { start = value; }
        }

        /// <summary>
        /// Creates and returns a new BaseVertex with the given name and adds it to the graph
        /// </summary>
        /// <param name="name">The name of the vertex to be added.  The name must be unique.  </param>
        /// <returns>The new Vertex which was just created</returns>
        public BaseVertex CreateNewVertex(string name, bool accepting)
        {
            BaseVertex newV = new BaseVertex(name, accepting);
            vertices.Add(newV);
            return newV;
        }


        /// <summary>
        /// Creates a new vertex with a name equal to the current length of the vertex list
        /// </summary>
        /// <returns>The new Vertex which was just created.  </returns>
        public BaseVertex CreateNewVertex()
        {
            return this.CreateNewVertex("" + vertices.Count, false);
        }

        /// <summary>
        /// Creates a new vertex with a name equal to the current length of the vertex list
        /// </summary>
        /// <param name="accepting">Whether the vertex is an accepting state or not.</param>
        /// <returns>The new Vertex which was just created.  </returns>
        public BaseVertex CreateNewVertex(bool accepting)
        {
            return this.CreateNewVertex("" + vertices.Count, accepting);
        }


        /// <summary>
        /// Removes the given Vertex from the graph; also removes all edges which use this vertex.  
        /// </summary>
        /// <param name="toRemove">The Vertex which will be completely removed from the graph.  </param>
        public void RemoveVertex(BaseVertex toRemove)
        {
            vertices.Remove(toRemove);

            foreach (BaseVertex vtx in vertices)
                vtx.RemoveConnections(toRemove);
        }
        /// <summary>
        /// Combines two graphs. Uses this as the base and adds the passed one to the end.
        /// </summary>
        /// <param name="that">Graph -> The graph to tack onto this one.</param>
        public void CombineGraphs(Graph that)
        {
            List<BaseVertex> ends = this.Accepting;

            that.start.Name = "start" + (this.vertices.Count + that.Vertices.Count);

            //Make sure to put vertices from the other graph into this one.  
            foreach (BaseVertex v in that.Vertices)
            {
                this.vertices.Add(v);
                v.Name = "" + vertices.Count;
            }
            foreach (BaseVertex temp in ends)
            {
                temp.AddConnection(that.start, "");
                temp.Accepting = false;
            }


        }

        public void CombineGraphs(Graph that, bool spacewaster)
        {

            that.start.Name = "start" + (this.vertices.Count + that.Vertices.Count);

            //Make sure to put vertices from the other graph into this one.  
            foreach (BaseVertex v in that.Vertices)
            {
                this.vertices.Add(v);
                v.Name = "" + vertices.Count;
            }
            BaseVertex temp = this.start;
            temp.AddConnection(that.start, "");
        }

        public override bool Equals(object other)
        {
            if (other is Graph)
            {
                Graph that = (Graph)other;

                foreach (BaseVertex vertex in this.vertices)
                {
                    vertex.visited = false;
                }
                foreach (BaseVertex vertex in that.vertices)
                {
                    vertex.visited = false;
                }

                return this.start.CheckNodeEquality(that.start);
            }
            else
            {
                return true;
            }
        }

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Graph))
        //        return false;
        //    Graph otherGraph = (Graph)obj;

        //    if(Vertices.Count != otherGraph.Vertices.Count)
        //        return false;

        //    List<String>[][] myMatrix = GetAdjacencyMatrix();
        //    List<String>[][] theirMatrix = otherGraph.GetAdjacencyMatrix();

        //    for (int x = 0; x < vertices.Count; x++)
        //    {
        //        for (int y = 0; y < vertices.Count; y++)
        //        {
        //            bool equalVertex = true;
        //            foreach (String c1 in myMatrix[x][y])
        //            {
        //                bool c1Found = false;
        //                foreach (String c2 in theirMatrix[x][y])
        //                {
        //                    if (c1 == c2)
        //                    {
        //                        c1Found = true;
        //                        break;
        //                    }
        //                }

        //                if (!c1Found)
        //                {
        //                    equalVertex = false;
        //                    break;
        //                }
        //            }


        //            if (!equalVertex)
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        public List<String>[][] GetAdjacencyMatrix()
        {
            List<String>[][] matrix = new List<string>[vertices.Count][];

            for (int x = 0; x < vertices.Count; x++)
            {
                matrix[x] = new List<string>[vertices.Count];
            }

            for (int x = 0; x < vertices.Count; x++)
            {
                for (int y = 0; y < vertices.Count; y++)
                {
                    matrix[x][y] = new List<string>();
                }
            }

            for (int x = 0; x < vertices.Count; x++)
            {
                BaseVertex vertex = vertices[x];

                foreach (Edge edge in vertex.Connections)
                {
                    BaseVertex other = edge.Connection;
                    int otherVertexIndex = vertices.IndexOf(other);

                    matrix[x][otherVertexIndex].Add(edge.Condition);
                }
            }


            return matrix;
        }

        public List<BaseVertex> findAcceptingParents(char transChar)
        {
            List<BaseVertex> list = new List<BaseVertex>();
            bool connected = false;
            string check = "" + transChar;

            foreach(BaseVertex node in this.vertices)
            {
                foreach(Edge edge in node.Connections)
                {
                    if(edge.Connection.Accepting && edge.Condition == check)
                    {
                        connected = true;
                    }
                    else
                    {
                        connected = false;
                    }
                }

                if(connected)
                {
                    list.Add(node);
                }
            }

            return list;
        }  
        /// <summary>
        /// Stores this graph in the DOT format to show in graphviz
        /// </summary>
        /// <param name="graphName">The name of this graph.</param>
        /// <returns></returns>
        public String ToDOT(String graphName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("digraph ");
            sb.Append(graphName);
            sb.Append("{\n");

            //format which nodes are accepting
            sb.Append("node [shape = doublecircle]; ");
            int acceptingCount = 0;
            foreach (BaseVertex vertex in vertices)
            {
                if (vertex.Accepting)
                {
                    acceptingCount++;
                    sb.Append(vertex.Name);
                    sb.Append(" ");
                }
            }
            if(acceptingCount > 0)
                sb.Append(";");

            //set all other nodes to be a single circle
            sb.Append("\nnode [shape = circle];\n");

            //format edges and labels
            foreach (BaseVertex vertex in vertices)
            {
                foreach (Edge edge in vertex.Connections)
                {
                    //label format
                    //\t v1 -> v2 [label="<edge text here>"]; \n

                    sb.Append("\t ");
                    sb.Append(vertex.Name);
                    sb.Append(" -> ");
                    sb.Append(edge.Connection.Name);
                    sb.Append(" [label=\"");
                    sb.Append(edge.Condition);
                    sb.Append("\"];\n");
                }
            }
            sb.Append("}");

            return sb.ToString();
        }


        public bool AcceptsToken(String token)
        {
            BaseVertex current = start;
            foreach (Char c in token)
            {
                bool foundNextVertex = false;
                foreach (Edge edge in current.Connections)
                {
                    if (edge.Condition == c.ToString())
                    {
                        current = edge.Connection;
                        foundNextVertex = true;
                        break;
                    }
                }

                if (!foundNextVertex)
                    return false;
            }

            return current.Accepting;
        }


        /// <summary>
        /// Switches the character classes to individual characters
        /// </summary>
        public void AddIndividualCharacters(Dictionary<String, Graph> characterClasses)
        {
            foreach(BaseVertex vertex in vertices)
            {
                List<Edge> newConnections = new List<Edge>();
                foreach (Edge e in vertex.Connections)
                {
                    if (e.Condition == "")
                    {
                        newConnections.Add(e);
                        continue;
                    }

                    String letters = "";
                    if(e.IsCharClass)
                    {
                        if (characterClasses.ContainsKey(e.Condition))
                            letters = characterClasses[e.Condition].StartVertex.Connections[0].Condition;
                        else
                            letters = e.Condition;
                    }
                    else
                    {
                        letters = e.Condition;
                    }

                    foreach (Char c in letters)
                    {
                        newConnections.Add(new Edge(e.Connection, c.ToString()));
                    }
                }

                vertex.Connections = newConnections;
            }
        }

        public void SaveToFile(String path)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    List<String>[][] matrix = GetAdjacencyMatrix();

                    sw.WriteLine(matrix.Length);

                    foreach (BaseVertex v in vertices)
                    {
                        if (v.Accepting)
                            sw.WriteLine("Accepting");
                        else
                            sw.WriteLine("Not");
                    }

                    for (int x = 0; x < matrix.Length; x++)
                    {
                        for (int y = 0; y < matrix[x].Length; y++)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (String s in matrix[x][y])
                            {
                                sb.Append(s);
                                sb.Append(",");
                            }

                            sw.WriteLine(sb.ToString());
                        }
                    }
                }
            }
        }

        public static Graph ReadFromFile(String path)
        {
            Graph g = new Graph();
            List<BaseVertex> verts = new List<BaseVertex>();

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int length = Int32.Parse(sr.ReadLine());

                    for (int x = 0; x < length; x++)
                    {
                        String isAccepting = sr.ReadLine();
                        bool accepting = isAccepting == "Accepting";
                        verts.Add(new BaseVertex(x.ToString(), accepting));
                    }

                    for (int x = 0; x < length; x++)
                    {
                        for (int y = 0; y < length; y++)
                        {
                            String connections = sr.ReadLine();
                            String[] transitions = connections.Split(',');

                            if (transitions.Length > 0)
                            {
                                foreach (String transition in transitions)
                                {
                                    String text = transition.Trim();
                                    if (text.Length > 0)
                                        verts[x].AddConnection(verts[y], text);
                                }
                            }
                        }
                    }
                }
            }

            g.vertices = verts;
            g.StartVertex = verts[1];

            return g;
        }

        public void WriteTransitionTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("next|\t");
            for(int x=0; x<vertices.Count; x++)
            {
                sb.Append(vertices[x].Name);
                sb.Append("|\t");
            }
            sb.Append("\n");

            List<Tuple<String, String>> transitions = new List<Tuple<string, string>>();

            for (int current = 0; current < vertices.Count; current++)
            {
                sb.Append(vertices[current].Name);
                sb.Append("|\t");
                for (int next = 0; next < vertices.Count; next++)
                {
                    bool createdTransition = false;
                    foreach(Edge connection in vertices[current].Connections)
                    {
                        if(connection.Connection == vertices[next])
                        {
                            if (!createdTransition)
                            {
                                createdTransition = true;
                                transitions.Add(new Tuple<string, string>((transitions.Count + 1).ToString(),
                                    connection.Condition));
                                sb.Append(transitions.Count);                           
                            }
                            else
                            {
                                transitions[transitions.Count-1] = new Tuple<string,string>(transitions.Count.ToString(),
                                    transitions[transitions.Count-1].Item2 + "," + connection.Condition);
                            }
                        }
                    }
                    sb.Append("|\t");
                }
                sb.Append("\n");
            }

            sb.Append("\n\nKey:\n");
            for (int x = 0; x < transitions.Count; x++)
            {
                sb.Append(transitions[x].Item1);
                sb.Append(" -> ");
                sb.Append(transitions[x].Item2);
                sb.Append("\n");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
