using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for GameBoardTest and is intended
    ///to contain all GameBoardTest Unit Tests
    ///</summary>
    [TestFixture]
    public class YahtzeeLogicTests
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
        public void SaveOnes1Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(3, 2, 1, 4, 5);
            bool pass = target.SaveOnes();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveOnes5Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 1, 1, 1, 1);
            bool pass = target.SaveOnes();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveOnes0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 3, 3, 6, 4);
            bool pass = target.SaveOnes();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveTwos1Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            bool pass = target.SaveTwos();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveTwos5Dice()
        {
            //Act
            target.SetDiceArrayValues(2, 2, 2, 2, 2);
            bool pass = target.SaveTwos(); 
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveTwos0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 3, 3, 6, 4);
            bool pass = target.SaveTwos();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveThrees1Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            bool pass = target.SaveThrees();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveThrees5Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(3, 3, 3, 3, 3);
            bool pass = target.SaveThrees();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveThrees0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 1, 1, 6, 4);
            bool pass = target.SaveThrees();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveFours1Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 1, 3, 4, 5);
            bool pass = target.SaveFours();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveFours5Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(4, 4, 4, 4, 4);
            bool pass = target.SaveFours();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveFours0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 3, 3, 6, 1);
            bool pass = target.SaveFours();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveFives1Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 5);
            bool pass = target.SaveFives();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveFives5Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(5, 5, 5, 5, 5);
            bool pass = target.SaveFives();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveFives0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 3, 3, 6, 4);
            bool pass = target.SaveFives();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveSixes1Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 4, 3, 4, 6);
            bool pass = target.SaveSixes();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveSixes5Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(6, 6, 6, 6, 6);
            bool pass = target.SaveSixes();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveSixes0Dice()
        {
            //Arrange
            //Yahtzee target = new Yahtzee();
            target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 3, 3, 1, 4);
            bool pass = target.SaveSixes();
            //Assert
            Assert.False(pass);
        }

        [Test]
        public void SaveYahtzeeOnes()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 1, 1, 1, 1);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzeeTwos()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 2, 2, 2, 2);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzeeThrees()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(3, 3, 3, 3, 3);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzeeFours()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(4, 4, 4, 4, 4);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzeeFives()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(5, 5, 5, 5, 5);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzeeSixes()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(6, 6, 6, 6, 6);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveYahtzee0Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(1, 4, 6, 3, 5);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.IsFalse(pass);
        }

        [Test]
        public void SaveYahtzee4Dice()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(6, 6, 6, 6, 5);
            bool pass = target.SaveYahtzee();
            //Assert
            Assert.IsFalse(pass);
        }

        [Test]
        public void SaveThreeKind1()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 2, 6, 6, 6);
            bool pass = target.SaveThreeKind();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveThreeKind2()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(4, 4, 4, 1, 1);
            bool pass = target.SaveThreeKind();
            //Assert
            Assert.True(pass);
        }

        [Test]
        public void SaveThreeKind2Pairs()
        {
            //Arrange
            Yahtzee target = new Yahtzee();

            //Act
            target.SetDiceArrayValues(2, 2, 6, 6, 1);
            bool pass = target.SaveThreeKind();
            //Assert
            Assert.False(pass);
        }
        #endregion
    }
}
