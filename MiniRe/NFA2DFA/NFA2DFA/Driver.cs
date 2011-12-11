using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading; // debug
using GraphLibrary;

namespace NFA2DFA
{
    class Driver
    {

        static void Main() {

            string epsilon = "0";
            Graph g = new Graph();


            //create vertices
            /*
            BaseVertex one = g.CreateNewVertex("0",false);
            BaseVertex two = g.CreateNewVertex("1",false);
            BaseVertex three = g.CreateNewVertex("2",false);
            */

            //BaseVertex four = g.CreateNewVertex("4",false);
            //BaseVertex five = g.CreateNewVertex("5",false);
            /*
            one.AddConnection(two, "a");

            two.AddConnection(two, "a");
            two.AddConnection(three, epsilon);

            three.AddConnection(three, "b");
            three.AddConnection(two, "a");
            three.AddConnection(one, "a");
            */

           
            BaseVertex v0 = g.CreateNewVertex("0", false);
            BaseVertex v1 = g.CreateNewVertex("1", true);
            BaseVertex v2 = g.CreateNewVertex("2", false);

            v0.AddConnection(v1, "a");

            v1.AddConnection(v1, "a");
            v1.AddConnection(v2, epsilon);

            v2.AddConnection(v2, "b");
            v2.AddConnection(v0, "a");
            v2.AddConnection(v1, "a");


            String nfaDOT = g.ToDOT("NFA");


            Converter c = new Converter(g,epsilon);

            //Console.WriteLine();
            c.convertToDFA(v0);
            //String dfaDOT = c.table.createGraph().ToDOT("DFA");
            //Console.WriteLine("Done");
            //Console.ReadLine();
        }
    }
}
