using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Scanner;

using RDParser;

namespace Scanner_Tester
{
    
    
    /// <summary>
    ///This is a test class for TokenScannerTest and is intended
    ///to contain all TokenScannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TokenScannerTest
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
        ///A test for scanFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Scanner.dll")]
        public void scanTestPass()
        {
            TokenScanner scanner = new TokenScanner("..\\..\\..\\TestFiles\\scanTestPass.txt");
            Assert.AreEqual(scanner.getToken(), "begin");
            Assert.AreEqual(scanner.getToken(), "hi");
            Assert.AreEqual(scanner.getToken(), "=");
            Assert.AreEqual(scanner.getToken(), "a");
            Assert.AreEqual(scanner.getToken(), "union");
            Assert.AreEqual(scanner.getToken(), "b");
            Assert.AreEqual(scanner.getToken(), ";");
            Assert.AreEqual(scanner.getToken(), "end");
            Assert.AreEqual(scanner.getToken(), "$");
        }

        [TestMethod()]
        [DeploymentItem("Scanner.dll")]
        public void scanTestWorkToLong()
        {
            try
            {
                TokenScanner scanner = new TokenScanner("..\\..\\..\\TestFiles\\ScanTestWordToLong.txt");
                Assert.Fail();
            }
            catch(SyntaxError)
            {

            }
            
        }

        [TestMethod()]
        [DeploymentItem("Scanner.dll")]
        public void scanTestFailBadSymbol()
        {
            try
            {
                TokenScanner scanner = new TokenScanner("..\\..\\..\\TestFiles\\ScanTestFailBadSymbol.txt");
                Assert.Fail();
            }
            catch (LexicalException)
            {

            }
        }

        [TestMethod()]
        [DeploymentItem("Scanner.dll")]
        public void scanBreakUp()
        {
            TokenScanner scanner = new TokenScanner("..\\..\\..\\TestFiles\\ScanTestBreakUp.txt");
            Assert.AreEqual(scanner.peekToken(), "break");
            Assert.AreEqual(scanner.getToken(), "break");
            Assert.AreEqual(scanner.getToken(), "(");
            Assert.AreEqual(scanner.getToken(), "up");
            Assert.AreEqual(scanner.getToken(), ")");
            Assert.AreEqual(scanner.getToken(), "(");
            Assert.AreEqual(scanner.getToken(), "this");
            Assert.AreEqual(scanner.getToken(), ")");
            Assert.AreEqual(scanner.getToken(), "(");
            Assert.AreEqual(scanner.getToken(), ")");
            Assert.AreEqual(scanner.getToken(), ";");
            Assert.AreEqual(scanner.getToken(), "l");
            Assert.AreEqual(scanner.getToken(), ";");
            Assert.AreEqual(scanner.getToken(), "ol");
            Assert.AreEqual(scanner.getToken(), "$");
            Assert.AreEqual(scanner.peekToken(), "$");
        }
        /// <summary>
        ///A test for isaLetter
        ///</summary>
        [TestMethod()]
        public void isaLetterTest()
        {
            TokenScanner scanner = new TokenScanner();
            Assert.IsTrue(scanner.isaLetter('a'));
            Assert.IsTrue(scanner.isaLetter('z'));
            Assert.IsTrue(scanner.isaLetter('q'));
            Assert.IsTrue(scanner.isaLetter('A'));
            Assert.IsTrue(scanner.isaLetter('Z'));
            Assert.IsTrue(scanner.isaLetter('S'));

            Assert.IsFalse(scanner.isaLetter('0'));
            Assert.IsFalse(scanner.isaLetter('*'));
            Assert.IsFalse(scanner.isaLetter(' '));
            Assert.IsFalse(scanner.isaLetter('~'));
            Assert.IsFalse(scanner.isaLetter((char)3));
        }

        /// <summary>
        ///A test for isaDigit
        ///</summary>
        [TestMethod()]
        public void isaDigitTest()
        {
            TokenScanner scanner = new TokenScanner();
            Assert.IsTrue(scanner.isaDigit('0'));
            Assert.IsTrue(scanner.isaDigit('1'));
            Assert.IsTrue(scanner.isaDigit('2'));
            Assert.IsTrue(scanner.isaDigit('3'));
            Assert.IsTrue(scanner.isaDigit('4'));
            Assert.IsTrue(scanner.isaDigit('5'));
            Assert.IsTrue(scanner.isaDigit('6'));
            Assert.IsTrue(scanner.isaDigit('7'));
            Assert.IsTrue(scanner.isaDigit('8'));
            Assert.IsTrue(scanner.isaDigit('9'));

            Assert.IsFalse(scanner.isaDigit('a'));
            Assert.IsFalse(scanner.isaDigit('_'));
        }
    }
}
