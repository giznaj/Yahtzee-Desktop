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
        public void SaveOnesTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 2, 3, 4, 5);
            //Assert
            Assert.True(target.SaveOnes());

            //Act
            target.SetDiceArrayValues(1, 1, 1, 1, 1);
            //Assert
            Assert.True(target.SaveOnes());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 6, 1);
            //Assert
            Assert.True(target.SaveOnes());

            //target.Close();
        }

        [Test, Order(2)]
        public void SaveTwosTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            //Assert
            Assert.True(target.SaveTwos());

            //Act
            target.SetDiceArrayValues(2, 2, 2, 2, 2);
            //Assert
            Assert.True(target.SaveTwos());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 6, 2);
            //Assert
            Assert.True(target.SaveTwos());

            //target.Close();
        }

        [Test, Order(3)]
        public void SaveThreesTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            //Assert
            Assert.True(target.SaveThrees());

            //Act
            target.SetDiceArrayValues(3, 3, 3, 3, 3);
            //Assert
            Assert.True(target.SaveThrees());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 6, 2);
            //Assert
            Assert.True(target.SaveThrees());

            //target.Close();
        }

        [Test, Order(4)]
        public void SaveFoursTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            //Assert
            Assert.True(target.SaveFours());

            //Act
            target.SetDiceArrayValues(4, 4, 4, 4, 4);
            //Assert
            Assert.True(target.SaveFours());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 4, 2);
            //Assert
            Assert.True(target.SaveFours());

            //target.Close();
        }

        [Test, Order(5)]
        public void SaveFivesTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            //Assert
            Assert.True(target.SaveFives());

            //Act
            target.SetDiceArrayValues(5, 5, 5, 5, 5);
            //Assert
            Assert.True(target.SaveFives());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 4, 2);
            //Assert
            Assert.True(target.SaveFives());

            //target.Close();
        }

        [Test, Order(6)]
        public void SaveSixesTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 6);
            //Assert
            Assert.True(target.SaveSixes());

            //Act
            target.SetDiceArrayValues(6, 6, 6, 6, 6);
            //Assert
            Assert.True(target.SaveSixes());

            //Act
            target.SetDiceArrayValues(3, 5, 6, 4, 2);
            //Assert
            Assert.True(target.SaveSixes());

            //target.Close();
        }

        [Test, Order(7)]
        public void SaveYahtzeeTests()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 1, 1, 1, 1);
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

            //Act
            target.SetDiceArrayValues(4, 4, 4, 4, 4);
            //Assert
            Assert.True(target.SaveYahtzee());

            //Act
            target.SetDiceArrayValues(5, 5, 5, 5, 5);
            //Assert
            Assert.True(target.SaveYahtzee());

            //Act
            target.SetDiceArrayValues(6, 6, 6, 6, 6);
            //Assert
            Assert.True(target.SaveYahtzee());

            //target.Close();
        }

        [Test, Order(8)]
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
