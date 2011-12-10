using RDParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Evaluator.Variables;

namespace EvaluatorTests
{
    
    
    /// <summary>
    ///This is a test class for RegexEvaluatorTest and is intended
    ///to contain all RegexEvaluatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RegexEvaluatorTest
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
        ///A test for Eval
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RDParser.dll")]
        public void RegexEvalTest1()
        {
            string pattern = "ste(ph|v)en";
            string text = "stephen steve steven seven stepvhen sharks LOLOLOL";

            StringMatchList list = RegexEvaluator.Eval(pattern, text);
            string actual = "";
            foreach (StringMatch match in list.Matches)
            {
                actual += match.Text;
                actual+= " ";
            }

            string expected = "stephen steven ";
            Assert.AreEqual(expected, actual);          
        }

        /// <summary>
        ///A test for Eval
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RDParser.dll")]
        public void RegexEvalTest2()
        {
            string pattern = @"[a-zA-Z]([a-zA-Z]|[0-9])*@[a-zA-Z]([a-zA-Z]|[0-9])*\.[a-zA-Z]([a-zA-Z]|[0-9])*";
            string text = "@ (*&#$(&(*&(@# stephen@iscool.com will@weredone.net llamas willbarr d0123@me.com 101@sdf.com sdf@1.com sdf@com.1";

            StringMatchList list = RegexEvaluator.Eval(pattern, text);
            string actual = "";
            foreach (StringMatch match in list.Matches)
            {
                actual += match.Text;
                actual += " ";
            }

            string expected = "stephen@iscool.com will@weredone.net d0123@me.com ";
            Assert.AreEqual(expected, actual);
        }
    }
}
