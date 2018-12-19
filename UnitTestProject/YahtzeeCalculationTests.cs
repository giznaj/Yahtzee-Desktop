using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestFixture]
    public class YahtzeeCalculationTests
    {
        public Yahtzee target;

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
        [Test]
        public void RollScoreTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 2, 3, 4, 5);
            target.SaveOnes();
            //Assert
            Assert.AreEqual(1, target.RollScore);

            target.SetDiceArrayValues(1, 2, 3, 4, 1);
            target.RollScore = 0;
            target.SaveOnes();
            //Assert
            Assert.AreEqual(2, target.RollScore);
        }
        #endregion
    }
}
