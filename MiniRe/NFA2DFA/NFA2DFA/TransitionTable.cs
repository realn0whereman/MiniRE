using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using GraphLibrary;
namespace NFA2DFA
{
    /// <summary>
    /// Models a table used to represent the union of NFA states for DFA conversion
    /// </summary>
    public class TransitionTable
    {
        /// <summary>
        /// ArrayList of strings which are the column headings. This object doubles as the list of all possible state transitions in the DFA
        /// </summary>
        ArrayList columnNames;

        /// <summary>
        /// list of VertexSets that will be in the DFA
        /// </summary>
        public ArrayList rowNames;

        /// <summary>
        /// Array of ArrayLists used to model the dynamically lengthening table (but not widening, width is set upon array allocation). table[i][j] is 
        /// VertexSet containing all possible states that the states from rowNames[j] can get to with transition columnNames[i].
        /// </summary>
        ArrayList[] table;

        /// <summary>
        /// variable epsilon transition symbol (to avoid hardcoding it)
        /// </summary>
        string epsilonTransition;

        public TransitionTable(Graph g, string epsilon) { 
            columnNames = new ArrayList();
            rowNames = new ArrayList();
            epsilonTransition = epsilon;

            //create columNames ArrayList by iterating through every possible transition in the graph that is not an epsilon transition
            foreach (BaseVertex v in g.Vertices) {
                foreach (Edge e in v.Connections) { 
                    if(!this.isInList(columnNames,e.Condition) && e.Condition != epsilonTransition){
                        columnNames.Add(e.Condition);
                    } 
                }
            }

            //initialize and assign the width of the table
            table = new ArrayList[columnNames.Count];
            for (int i = 0; i < columnNames.Count;i++ ) {
                table[i] = new ArrayList(); // every array index is an arraylist, so those need to be initialized 
            }
        }

        /// <summary>
        /// Add a row to the table and figure out which states are equivalent. Here is a basic rundown of the algorithm: for every vertex of 
        /// the set in your rowNames entry for that row, union all the states that can be reached by the transition designated by that column 
        /// of the table (from any vertex from the rowNames entry). This union of states is what you store in the table.
        /// </summary>
        /// <param name="set">VertexSet that you must check the transitions of</param>
        public void addRow(VertexSet set) {
            //declared temporary variables v,e and s to hold individual BaseVertexes, Edges and strings from the foreach loops below
            BaseVertex v;
            Edge e;
            string s;
            
            //the union of the necessary BaseVertexes to store in the table
            VertexSet tablePayload;

            for(int i=0; i < columnNames.Count; i++){ // for every transition
                s = (string)columnNames[i];
                tablePayload = new VertexSet();
                for(int j=0; j < set.size(); j++){ // for every vertex in the set (the set is the corresponding rowNames entry passed in from the caller)
                    v = (BaseVertex)set.vertices[j];
                    
                    for(int k=0; k < v.Connections.Count; k++){ // for every edge in that vertex
                        e = v.Connections[k];
                        if (e.Condition == s && !tablePayload.isInSet(e.Connection)) { // check if the edge transition is the same as the column you're in and also if it's not already in the tablepayload
                            tablePayload.addToSet(e.Connection);
                        }
                    }
                }
                table[i].Add(eClosure(tablePayload)); // put that payload into the table
            }

        }


        private bool isInList(ArrayList list, String condition)
        {
            foreach (String i in list)
            {
                if (i == condition)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get an ArrayList which contains the most recently added row in the table
        /// </summary>
        /// <returns></returns>
        public ArrayList getVSFromLastRow() { 
            ArrayList vsList = new ArrayList();
            foreach (ArrayList list in table) {
                if (vsList.Count == 0) {
                    vsList.Add(list[rowNames.Count-1]);
                    continue;
                }
                if (vsList.Count > 0 && !vsList.Contains(list[rowNames.Count-1]))
                    vsList.Add(list[rowNames.Count-1]);
            }
            return vsList;
        }

        public void printTable() {
            //Console.Write("Column Names: ");
            foreach (string s in columnNames) {
                //Console.Write(s + ",");
            }
            //Console.WriteLine();
            for (int i = 0; i < rowNames.Count; i++) {
                ((VertexSet)rowNames[i]).printVertexSet();
                //Console.Write("|");
                for (int j = 0; j < columnNames.Count; j++) {
                    ((VertexSet)table[j][i]).printVertexSet();
                    //Console.Write("|");
                }
                //Console.WriteLine();
            }
        }


        public VertexSet eClosure(VertexSet vs)
        {
            VertexSet union = new VertexSet();
            VertexSet temp = new VertexSet();
            foreach (BaseVertex v in vs.vertices)
            {
                temp = eClosure(v);
                foreach (BaseVertex vert in temp.vertices)
                {
                    if (!union.isInSet(vert))
                    {
                        union.addToSet(vert);
                    }
                }

            }
            return union;

        }

        /// <summary>
        /// perform an epsilon closure on a vertex
        /// </summary>
        /// <param name="v">vertex to perform e closure on</param>
        /// <returns>list of vertices in the epsilon closure of the vertex passed into the method</returns>
        public VertexSet eClosure(BaseVertex v)
        {
            Stack<BaseVertex> nodes = new Stack<BaseVertex>();
            BaseVertex current;
            VertexSet visited = new VertexSet(new ArrayList());

            // if the node passed doesn't have e transitions, return list with just passed in vertex, otherwise push onto stack and start algorithm
            if (this.hasEpsilonTransition(v))
            {
                nodes.Push(v);
            }

            while (nodes.Count > 0)
            {
                current = nodes.Pop();
                visited.addToSet(current);
                if (this.hasEpsilonTransition(current))
                {
                    ArrayList eNodes = this.getEpsilonTransition(current);
                    foreach (BaseVertex newNode in eNodes)
                    {
                        if (!visited.isInSet(newNode))
                        {
                            nodes.Push(newNode);
                        }
                    }
                }
            }
            if (visited.size() == 0)
            {
                visited.addToSet(v);
            }

            return visited;
        }


        /// <summary>
        /// Checks all the edges to see if they are epsilon transitions
        /// </summary>
        /// <param name="node">node to check</param>
        /// <returns>true if there is an epsilon transition</returns>
        private bool hasEpsilonTransition(BaseVertex node)
        {
            foreach (Edge e in node.Connections)
            {
                if (e.Condition == this.epsilonTransition)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get list of nodes that are connected to the input node by epsilon transitions
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private ArrayList getEpsilonTransition(BaseVertex node)
        {
            ArrayList eNodes = new ArrayList();
            foreach (Edge e in node.Connections)
            {
                if (e.Condition == this.epsilonTransition)
                {
                    if (!this.isInList(eNodes, e.Connection))
                    {
                        eNodes.Add(e.Connection);
                    }
                }
            }
            return eNodes;

        }

        // checks if node is in visited by name
        private bool isInList(ArrayList visited, BaseVertex node)
        {
            foreach (BaseVertex i in visited)
            {
                if (i.Name == node.Name)
                {

                    return true;
                }
            }

            return false;
        }



        public Graph createGraph()
        {
            normalizeNames();

            Graph g = new Graph();


            BaseVertex[] verts = new BaseVertex[rowNames.Count];
            Dictionary<String, BaseVertex> vertexTable = new Dictionary<string,BaseVertex>();

            for (int x = 0; x < rowNames.Count; x++)
            {
                bool accepting = false;
                foreach (BaseVertex v in ((VertexSet)rowNames[x]).vertices)
                {
                    if (v.Accepting)
                        accepting = true;
                }
                BaseVertex vertex = g.CreateNewVertex(((VertexSet)rowNames[x]).ToDOTString(), accepting);
                vertexTable.Add(vertex.Name, vertex);
                verts[x] = vertex;
            }

            //iterate over vertices
            for (int i = 0; i < rowNames.Count; i++)
            {
                //iterate over alphabet
                for (int x = 0; x < columnNames.Count; x++)
                {
                    String toVertexName = ((VertexSet)table[x][i]).ToDOTString();
                    verts[i].AddConnection(vertexTable[toVertexName], columnNames[x].ToString());
                }
            }


            g.StartVertex = g.Vertices[1];


            return g;
        }

        /// <summary>
        /// Make all the same sets have the same name ex: {0,1,2} = {1,2,0}
        /// </summary>
        private void normalizeNames()
        {
            List<Tuple<VertexSet, String>> setNames = new List<Tuple<VertexSet, string>>();

            for (int i = 0; i < rowNames.Count; i++)
            {
                for (int j = 0; j < columnNames.Count; j++)
                {
                    VertexSet set = (VertexSet)table[j][i];

                    bool found = false;
                    foreach (Tuple<VertexSet, String> vertexSet in setNames)
                    {
                        if (set.vertices.Count != vertexSet.Item1.vertices.Count)
                        {
                            continue;
                        }
                        List<BaseVertex> setVerts = new List<BaseVertex>(vertexSet.Item1.vertices.Cast<BaseVertex>().ToList());
                        IEnumerable<BaseVertex> sharedVerts = setVerts.Intersect(set.vertices.Cast<BaseVertex>().ToList());

                        if (sharedVerts.Count() == vertexSet.Item1.vertices.Count)
                        {
                            set.vertices = vertexSet.Item1.vertices;
                            found = true;
                        }
                        //bool matchesVertices = true;
                        //foreach (BaseVertex v in vertexSet.Item1)
                        //{
                        //    bool foundVertex = false;
                        //    foreach (BaseVertex v2 in set.vertices)
                        //    {
                        //        if(v.Name == v2.Name)
                        //        {
                        //            foundVertex = true;
                        //            break;
                        //        }
                        //    }
                        //}
                    }

                    if (!found)
                        setNames.Add(new Tuple<VertexSet, String>(set, set.ToDOTString()));
                }
            }
        }
    }
}
