using YahtzeeApplication;
using NUnit.Framework;

namespace UnitTestProject
{
    /// <summary>
    ///This is a test class for HelpScreenTest and is intended
    ///to contain all HelpScreenTest Unit Tests
    ///</summary>
    [TestFixture]
    public class HelpScreenElementTests
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
        public void HelpScreenElements()
        {
            //Arrange
            HelpScreen target = new HelpScreen();

            //Act

            //Assert
            Assert.IsTrue(target.HelpText.Equals("For complete rules visit:"));
            Assert.IsTrue(target.WikiUrl.Equals("http://en.wikipedia.org/wiki/Yahtzee"));
            Assert.IsTrue(target.btnClose.Text.Equals("Close"));
            Assert.IsTrue(target.pictureBox1.BackgroundImage != null);
        }

        /// <summary>
        ///A test for form actions
        ///</summary>
        [Test]
        public void HelpScreenCloseButton()
        {
            //Arrange
            HelpScreen target = new HelpScreen();

            //Act
            target.btnClose.PerformClick();

            //Assert
            //Assert.IsTrue(target.IsDisposed);
        }
        #endregion
    }
}
