using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestFixture]
    public class BugsTests
    {
        //Yahtzee target;

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
            //target.Close();
        }
        #endregion

        #region Tests
        [Test, Order(1)]
        public void FourStraightBug()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(6, 4, 1, 5, 3);
            //Assert
            Assert.True(target.SaveFourStraight());

            //target.Close();
        }

        [Test, Order(2)]
        public void FullHouseBug1()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(5, 5, 5, 6, 6);
            //Assert
            Assert.True(target.SaveFullHouse());

            //target.Close();
        }

        [Test, Order(3)]
        public void FullHouseBug2()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 1, 1, 5, 5);
            //Assert
            Assert.True(target.SaveFullHouse());

            //target.Close();
        }
        #endregion
    }
}
