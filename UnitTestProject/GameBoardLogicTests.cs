using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestFixture]
    public class GameBoardLogicTests
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
        public void YahtzeeTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1,1,1,1,1);
            //Assert
            Assert.True(target.SaveYahtzee());

            //Act
            target.SetDiceArrayValues(2, 2, 2, 2, 2);
            //Assert
            Assert.True(target.SaveYahtzee());

            //Act
            target.SetDiceArrayValues(3, 3, 3, 3, 3);
            //Assert
            Assert.True(target.SaveYahtzee());
        }
        #endregion
    }
}
