using Evaluator.AST.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Evaluator.AST;
using Evaluator;
using Evaluator.Variables;

namespace EvaluatorTests
{
    
    
    /// <summary>
    ///This is a test class for AssignNodeTest and is intended
    ///to contain all AssignNodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AssignNodeTest
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

            LookupNode expression = new LookupNode(varName);
            OpNode len = new OpNode(Ops.len, expression);
            string outputVar = "output";
            AssignNode assign = new AssignNode(outputVar, len);
            object actualValue = assign.Execute(symbols);

            Assert.AreEqual(varValue.Length, actualValue);
            Assert.AreEqual(varValue.Length, Int32.Parse(symbols[outputVar].ToString()));
        }

        /// <summary>
        ///A test for GetReturnValue
        ///</summary>
        [TestMethod()]
        public void GetReturnValueTest()
        {
            //Node parent = null; // TODO: Initialize to an appropriate value
            //string varName = string.Empty; // TODO: Initialize to an appropriate value
            //AssignNode target = new AssignNode(parent, varName); // TODO: Initialize to an appropriate value
            //object expected = null; // TODO: Initialize to an appropriate value
            //object actual;
            //actual = target.GetReturnValue();
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
