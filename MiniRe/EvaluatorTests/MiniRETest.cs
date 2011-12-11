using Evaluator.AST_New;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Evaluator;
using System.IO;

namespace EvaluatorTests
{
    
    
    /// <summary>
    ///This is a test class for MiniRETest and is intended
    ///to contain all MiniRETest Unit Tests
    ///</summary>
    [TestClass()]
    public class MiniRETest
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
        ///A test for MiniRE Constructor
        ///</summary>
        [TestMethod()]
        public void MiniRE()
        {
            SymbolTable table = new SymbolTable();

            MiniRE miniRe = new MiniRE();

            StatementList sl = new StatementList();
            miniRe.StatementList = sl;

            Statement statement = new Statement();
            sl.Statement = statement;

            StatementListTail tail = new StatementListTail();
            sl.Tail = tail;

            Statement s2 = new Statement();
            tail.Statement = s2;

            StatementListTail tail2 = new StatementListTail();
            tail.Tail = tail2;

            #region Statement 1
            AssignmentStatement asStmt = new AssignmentStatement();
            asStmt.Id = "x";
            statement.AssignmentStatement = asStmt;

            Exp exp = new Exp();
            asStmt.Exp = exp;

            Term term = new Term();
            exp.Term = term;

            Regex regex = new Regex();
            regex.Pattern = "a";
            term.Regex = regex;

            Filename filename = new Filename();
            filename.Path = "../../../TestFiles/file1.txt";
            term.Filename = filename; 
            #endregion

            #region Statement 2

            AssignmentStatement as2 = new AssignmentStatement();
            as2.Id = "y";
            as2.Type = AssignmentStatementType.Length;
            s2.AssignmentStatement = as2;

            Exp exp2 = new Exp();
            exp2.Id = "x";
            as2.Exp = exp2; 


            #endregion

            miniRe.Execute(table);

            object value = table["x"];


            Assert.AreEqual(7, (int)table["y"]);
        }

        [TestMethod()]
        public void Print()
        {
            SymbolTable table = new SymbolTable();

            MiniRE miniRe = new MiniRE();

            StatementList sl = new StatementList();
            miniRe.StatementList = sl;

            Statement statement = new Statement();
            sl.Statement = statement;

            StatementListTail tail = new StatementListTail();
            sl.Tail = tail;

            Statement s2 = new Statement();
            tail.Statement = s2;

            StatementListTail tail2 = new StatementListTail();
            tail.Tail = tail2;

            #region Statement 1
            AssignmentStatement asStmt = new AssignmentStatement();
            asStmt.Id = "x";
            statement.AssignmentStatement = asStmt;

            Exp exp = new Exp();
            asStmt.Exp = exp;

            Term term = new Term();
            exp.Term = term;

            Regex regex = new Regex();
            regex.Pattern = "a";
            term.Regex = regex;

            Filename filename = new Filename();
            filename.Path = "../../../TestFiles/file1.txt";
            term.Filename = filename;
            #endregion

            #region Statement 2

            AssignmentStatement as2 = new AssignmentStatement();
            as2.Id = "y";
            as2.Type = AssignmentStatementType.Length;
            s2.AssignmentStatement = as2;

            Exp exp2 = new Exp();
            exp2.Id = "x";
            as2.Exp = exp2;


            #endregion

            #region Statement 3


            Statement s3 = new Statement();
            tail2.Statement = s3;

            OtherStatement print = new OtherStatement();
            print.Mode = OtherStatementMode.Print;
            s3.OtherStatement = print;

            ExpList expList = new ExpList();
            print.ExpList = expList;

            Exp exp3 = new Exp();
            exp3.Id = "y";
            expList.Exp = exp3;



            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                Console.SetOut(sw);
                miniRe.Execute(table);
                sw.Flush();

                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    String actual = sr.ReadToEnd();
                    Assert.AreEqual(7, Int32.Parse(actual));
                }
            }
        }

        [TestMethod()]
        public void Replace()
        {
            SymbolTable table = new SymbolTable();

            MiniRE miniRe = new MiniRE();

            StatementList sl = new StatementList();
            miniRe.StatementList = sl;

            Statement statement = new Statement();
            sl.Statement = statement;

            StatementListTail tail = new StatementListTail();
            sl.Tail = tail;

            Statement s2 = new Statement();
            tail.Statement = s2;

            StatementListTail tail2 = new StatementListTail();
            tail.Tail = tail2;

            #region Statement 1
            AssignmentStatement asStmt = new AssignmentStatement();
            asStmt.Id = "x";
            statement.AssignmentStatement = asStmt;

            Exp exp = new Exp();
            asStmt.Exp = exp;

            Term term = new Term();
            exp.Term = term;

            Regex regex = new Regex();
            regex.Pattern = "a";
            term.Regex = regex;

            Filename filename = new Filename();
            filename.Path = "../../../TestFiles/file1.txt";
            term.Filename = filename;
            #endregion

            #region Statement 2

            AssignmentStatement as2 = new AssignmentStatement();
            as2.Id = "y";
            as2.Type = AssignmentStatementType.Length;
            s2.AssignmentStatement = as2;

            Exp exp2 = new Exp();
            exp2.Id = "x";
            as2.Exp = exp2;


            #endregion

            #region Statement 3

            Statement s3 = new Statement();
            tail2.Statement = s3;

            OtherStatement os = new OtherStatement();
            os.Mode = OtherStatementMode.Replace;
            os.ReplaceText = "b";
            s3.OtherStatement = os;

            Regex regex3 = new Regex();
            os.Regex = regex3;

            Filenames filenames = new Filenames();
            os.Filenames = filenames;

            Filename filename2 = new Filename();
            filename2.Path = "../../../file2.txt";
            filenames.Filename = filename2;

            Filename destimation = new Filename();
            destimation.Path = "../../../file3.txt";
            filenames.Destimation = destimation;


            #endregion

            miniRe.Execute(table);
        }
    }
}
