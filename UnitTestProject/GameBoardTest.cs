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
        #region Setup TearDown
        //Use to run code before each test
        [SetUp]
        public void MyTestSetup()
        {
            //todo
        }

        //Use TestCleanup to run code after each test has run
        [TearDown]
        public void MyTestCleanup()
        {
            //todo
        }
        #endregion

        #region Tests
        /// <summary>
        ///A test for form elments
        ///</summary>
        [Test]
        public void GameBoardElements()
        {
            //Arrange
            GameBoard target = new GameBoard();

            //Act


            //Assert
            Assert.IsTrue(target.Text.Equals("Yahtzee"));
        }

        /// <summary>
        ///A test for form actions
        ///</summary>
        //[Test]
        //public void NewGameTest()
        //{
        //    GameBoard target = new GameBoard(); // TODO: Initialize to an appropriate value

        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
        #endregion
    }
}
