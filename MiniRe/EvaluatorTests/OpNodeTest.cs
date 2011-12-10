using Evaluator.AST.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Evaluator;
using Evaluator.Variables;
using Evaluator.AST;

namespace EvaluatorTests
{
    
    
    /// <summary>
    ///This is a test class for OpNodeTest and is intended
    ///to contain all OpNodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OpNodeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Execute
        ///</summary>
        [TestMethod()]
        public void ExecuteTest()
        {
            //init symbol table
            string varName = "test";
            StringMatchList varValue = new StringMatchList();
            varValue.AddMatch("sdf", "file1.txt", 30, 70, 100);
            SymbolTable symbols = new SymbolTable();
            symbols[varName] = varValue;


            string pattern1 = "[A-Z a-z]*ment[A-Z a-z]*";
            string file1 = "file1.txt";
            OpNode find1 = new OpNode(Ops.find, pattern1, file1);

            string pattern2 = "A|a) [A-Z a-z]*'";
            string file2 = "file2.txt";
            OpNode find2 = new OpNode(Ops.find, pattern2, file2);

            OpNode intersect = new OpNode(Ops.intersec, find1, find2);
            StringMatchList intersectList = (StringMatchList) intersect.Execute(symbols);

            
           



            //LookupNode expression = new LookupNode(varName);
            //OpNode len = new OpNode(Ops.len, expression);
            //string outputVar = "output";
            //AssignNode assign = new AssignNode(outputVar, len);
            //object actualValue = assign.Execute(symbols);

            //Assert.AreEqual(varValue.Length, actualValue);
            //Assert.AreEqual(varValue.Length, Int32.Parse(symbols[outputVar].ToString()));
        }
    }
}
