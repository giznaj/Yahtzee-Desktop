using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    
    
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestFixture]
    public class GameBoardTest
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
        ///A test for GameBoard Constructor
        ///</summary>
        [TestCase, Order(1)]
        public void GameBoardConstructorTest()
        {
            GameBoard target = new GameBoard();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for NewGameMessage
        ///</summary>
        [TestCase()]
        public void NewGameMessageTest()
        {
            GameBoard target = new GameBoard(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.NewGameMessage = expected;
            actual = target.NewGameMessage;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
