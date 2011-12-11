using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphLibrary;
using System.Collections;
using System.Diagnostics;

namespace NFA2DFA
{
    /// <summary>
    /// Class that encapsulates a set of vertices
    /// </summary>
    public class VertexSet
    {
        /// <summary>
        /// uniquely identifies a set of vertices*
        /// * note: order of vertices in vertices arraylist 
        /// does not matter for ID computation
        /// </summary>
        public int id;

        /// <summary>
        /// List of vertices in this VertexSet
        /// </summary>
        public ArrayList vertices;
        
        /// <summary>
        /// Creates object from existing list of vertices
        /// </summary>
        /// <param name="v">List of BaseVertex objects which are the vertices for the set</param>
        public VertexSet(ArrayList v) {
            vertices = v;
            this.computeID(); // computes the ID right after assignment
        }

        /// <summary>
        /// Creates an empty VertexSet
        /// </summary>
        public VertexSet()
        {
            vertices = new ArrayList();
            id = 0;
        }

        /// <summary>
        /// gets the number of vertices in this VertexSet
        /// </summary>
        /// <returns></returns>
        public int size() {
            return this.vertices.Count;
        }

        /// <summary>
        /// Adds a vertex to the VertexSet and updates the ID
        /// </summary>
        /// <param name="v">BaseVertex to add to the set</param>
        public void addToSet(BaseVertex v){
            
            Debug.Assert(!this.isInSet(v)); // sanity check to ensure a set will never have duplicates
            vertices.Add(v);
            this.computeID();
        }

        /// <summary>
        /// tests if a BaseVertex is in the VertexSet
        /// </summary>
        /// <param name="node">node to check to see if it is in the set</param>
        /// <returns>true if the vertex is in the set, false otherwise</returns>
        public bool isInSet(BaseVertex node)
        {
            foreach (BaseVertex i in this.vertices)
            {
                if (i.Name == node.Name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// compute and set the ID for this vertexset
        /// </summary>
        private void computeID() { 
            int signature = 0;
            foreach(BaseVertex v in vertices){
                foreach(char c in v.Name){ // sums each vertex's name's characters together
                    signature += (int)c;
                }
            }
            id = signature;
            
        }


        public void printVertexSet() { // replace with toString
           // Console.Write("{");
            foreach (BaseVertex v in vertices) {
                //Console.Write(v.Name + ",");
            }
            //Console.Write("}");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            foreach (BaseVertex v in vertices)
            {
                sb.Append(v.Name + ",");
            }
            sb.Append(" }");

            return sb.ToString();
        }

        public string ToDOTString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (BaseVertex v in vertices)
            {
                sb.Append(v.Name);
            }

            if (sb.Length == 0)
                return "Empty";

            return sb.ToString();
        }

        /// <summary>
        /// Tests the equality of this and another VertexSet
        /// </summary>
        /// <param name="o"> other VertexSet to test against</param>
        /// <returns>true if the VertexSets have the same ID</returns>
        public bool Equals(VertexSet o) {
            if (o.id == this.id) {
                return true;
            }
            return false;
        }



    }
}
