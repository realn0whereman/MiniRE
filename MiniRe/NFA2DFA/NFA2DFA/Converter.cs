using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using GraphLibrary;
using System.Diagnostics;

namespace NFA2DFA
{
    /// <summary>
    /// class to convert NFA to a DFA
    /// </summary>

    public class Converter
    {
        Graph nfa;
        string epsilonTransition;
        public TransitionTable table;

        public Converter(Graph n,string epsilon) {
            nfa = n;
            epsilonTransition = epsilon;
            table = new TransitionTable(n,epsilon);
        }


        /// <summary>
        /// perform an epsilon closure on a vertex
        /// </summary>
        /// <param name="v">vertex to perform e closure on</param>
        /// <returns>list of vertices in the epsilon closure of the vertex passed into the method</returns>
        public VertexSet eClosure(BaseVertex v) {
            Stack<BaseVertex> nodes = new Stack<BaseVertex>();
            BaseVertex current;
            VertexSet visited = new VertexSet(new ArrayList());

            // if the node passed doesn't have e transitions, return list with just passed in vertex, otherwise push onto stack and start algorithm
            if (this.hasEpsilonTransition(v)) { 
                nodes.Push(v);
            }

            while (nodes.Count > 0) {
                current = nodes.Pop();
                visited.addToSet(current);
                if (this.hasEpsilonTransition(current)) {
                    ArrayList eNodes = this.getEpsilonTransition(current);
                    foreach (BaseVertex newNode in eNodes) {
                        if (!visited.isInSet(newNode)) {
                            nodes.Push(newNode);
                        }
                    }
                } 
            }
            if (visited.size() == 0) {
                visited.addToSet(v);
            }

            return visited;
        }

        public VertexSet eClosure(VertexSet vs) {
            VertexSet union = new VertexSet();
            VertexSet temp = new VertexSet();
            foreach (BaseVertex v in vs.vertices) {
                temp = eClosure(v);
                foreach (BaseVertex vert in temp.vertices) {
                    if (!union.isInSet(vert)) {
                        union.addToSet(vert);
                    }
                }
            
            }
            return union;
        
        }

        /// <summary>
        /// Convert NFA to DFA starting at the startState BaseVertex. Basic algorithm works as follows. 
        /// Get eclosure of start state. Add this set as a rowName entry. The first row must be added before the algorithm can enter a loop and start working
        /// on the data until it is finished. 
        /// </summary>
        /// <param name="startState">Start state of the graph</param>
        public void convertToDFA(BaseVertex startState) {
            //get eClosure of startState and add as rowName entry
            VertexSet startSet = this.eClosure(startState);
            Debug.Assert(startSet.vertices.Count != 0);
            table.rowNames.Add(startSet);

           
            //process this new addition to rowNames and fill out the data for the first row of the table.

            //algorithm description in method comments
            
            table.addRow(startSet);

            // get the last row added to the table (in this case, it is also the first row)
            ArrayList lastRow = table.getVSFromLastRow();      
            
            Queue<VertexSet> remainingSets = new Queue<VertexSet>(); // unencountered sets left to process
            
            foreach (VertexSet vs in lastRow) {
                //check if any of the table entries from the last row have never been seen before, if so, queue for processing 
                if (isNewVSInTable(vs,remainingSets)) {
                    //vs.fromFirstRow = true;
                    remainingSets.Enqueue(vs);
                }
            }

            VertexSet current;
            while (remainingSets.Count > 0) {
                current = remainingSets.Dequeue(); // get a VertexSet to process
                //Console.WriteLine("Dequeued");
                //current.printVertexSet();
                //if(!current.fromFirstRow){
                  //current = eClosure(current);
                //}
                table.rowNames.Add(current); // add it to the rowNames list
                table.addRow(current); // process that entry now
                //Console.WriteLine(table.rowNames.Count);
                //check for any new VertexSets that have not been processed and queue them if they need it.
                lastRow = table.getVSFromLastRow();
           
                
                
                foreach (VertexSet vs in lastRow) {
                    
                    if (isNewVSInTable(eClosure(vs), remainingSets) ) {
                        
                        vs.printVertexSet();
                        remainingSets.Enqueue(eClosure(vs));
                    }
                    //if(isNewVSInTable(eClosure(vs),remainingSets)){
                    //    remainingSets.Enqueue(eClosure(vs));
                    //}
                }
            }
            
          
            table.printTable();
        
        }

        /// <summary>
        /// check if a VertexSet is a new VertexSet to the table
        /// </summary>
        /// <param name="vs">VertexSet to check</param>
        /// <param name="remaining">list to check</param>
        /// <returns>true if the VertexSet has not been seen before</returns>
        private bool isNewVSInTable(VertexSet vs, Queue<VertexSet> remaining)
        {
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Cond1: " + !isInList(remaining, vs));
            //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Cond2: " + !!isInRowNames(vs));
            if (!isInList(remaining,vs) && !isInRowNames(vs)){ //!isInList(table.rowNames,vs)) {
                return true;
            }
            return false;
        }

        private bool isInRowNames(VertexSet vs){
            //Console.WriteLine("START");

            foreach(VertexSet set in table.rowNames){
                if (set.id == vs.id)
                {
                    return true;
                }
            }
            return false;

        }


        // checks if node is in visited by name
        private bool isInList(ArrayList visited,BaseVertex node) {
            foreach (BaseVertex i in visited) {
                if (i.Name == node.Name)
                {
                   
                    return true;
                }
            }
           
            return false;
        }

        
        private bool isInList(Queue<VertexSet> list, VertexSet set) {
            foreach (VertexSet i in list)
            {
                if (i.id == set.id)
                    return true;
            }
            return false;
        
        }

        private bool isInList(ArrayList list, VertexSet set)
        {
            foreach (VertexSet i in list)
            {
                if (set.Equals(i))
                    return true;
            }
            return false;

        }

        /// <summary>
        /// Checks all the edges to see if they are epsilon transitions
        /// </summary>
        /// <param name="node">node to check</param>
        /// <returns>true if there is an epsilon transition</returns>
        private bool hasEpsilonTransition(BaseVertex node) {
            foreach (Edge e in node.Connections) {
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
        private ArrayList getEpsilonTransition(BaseVertex node) {
            ArrayList eNodes = new ArrayList();
            foreach (Edge e in node.Connections)
            {
                if (e.Condition == this.epsilonTransition) { 
                   if(!this.isInList(eNodes,e.Connection)){
                        eNodes.Add(e.Connection);
                   }
                }
            }
            return eNodes;
        
        }



    }
}
