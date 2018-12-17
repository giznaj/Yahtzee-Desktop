// GameBoard class (code)
//

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace YahtzeeApplication
{
    public partial class GameBoard : Form
    {
        #region Private Members
        private int selectedIndex; // Stores the selected category the user is either taking a 0 for, or getting a score for
        private bool[] rollOrHoldArray = new bool[5]; // Stores the bool value for each dice for when rolling.  (hold = false, true = roll)
        private CheckBox[] checkBoxArray = new CheckBox[5]; // Array of checkboxes
        private PictureBox[] pictureBoxArray = new PictureBox[5]; // Array of picture boxes for the dice
        private PictureBox[] categoryArray = new PictureBox[13]; // Array of categories for the user to score with
        private Image[] diceImageArray = new Image[6]; // Array of dice images
        private Image[] categoryImageArray = new Image[2]; // Array of images for category (green checmkar, red x)
        private Image[] rollImageArray = new Image[4]; // Array of images for the roll number
        private Random randomNumbers = new Random(); // random-number generator
        private Yahtzee NewYahtzee; // Declare Yahtzee.cs object (game logic)
        private HelpScreen newHelper; // Declare new HelpScreen.cs object (help screen) 
        private bool newGameMessage; // User enables or disables the warning message when a new game starts
        private bool saveStatus; // true if the user has selected a category for the current round of rolls
        private bool takeZeroStatus; // true if the take zero save was succesful.
        private string noScoreSaved = "You don't have the dice combination for this category." + "\n" +
                            "Did you mean to take a Zero for this category?";
        #endregion

        #region Public Properties
        /// <summary>
        /// Sets the new game warning message flag
        /// </summary>
        public bool NewGameMessage
        {
            get { return newGameMessage; }
            set { newGameMessage = value; }
        }

        /// <summary>
        /// Sets and gets the value for saveStatus.  True if the user has saved something.  false as soon as the next turn starts.
        /// </summary>
        public bool SaveStatus
        {
            get { return saveStatus; }
            set { saveStatus = value; }
        }

        /// <summary>
        /// Sets and gets the value for selectedIndex.  True if the user has selected a category.  false as soon as the next turn starts.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        /// <summary>
        /// Sets and gets the value for takeZeroStatus.  True if the take zero save is sucessful
        /// </summary>
        public bool TakeZeroStatus
        {
            get { return takeZeroStatus; }
            set { takeZeroStatus = value; }
        }
        #endregion

        #region Initializers
        // instantiate the game logic and the help form
        private void Yahtzee_Load(object sender, EventArgs e)
        {
            btnRollDice.Enabled = false; // Disable the 'Roll' button until a new game is started
            btnNextTurn.Enabled = false; // Disable the 'Next Turn' button until the new game is started
            btnSaveScore.Enabled = false; // Disable the 'Save Score' button until the new game is started

            // Disable all category boxes before the game starts.
            // DisableAllCategory();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the class
        /// </summary>
        public GameBoard()
        {
            InitializeComponent();

            // Instantiate a new Yahtzee class (game logic) and help/info screen
            NewYahtzee = new Yahtzee(); // Instantiate new gameboard logic  
            
            // Array of category pictureboxes
            categoryArray[0] = pictureBoxOnes;
            categoryArray[1] = pictureBoxTwos;
            categoryArray[2] = pictureBoxThrees;
            categoryArray[3] = pictureBoxFours;
            categoryArray[4] = pictureBoxFives;
            categoryArray[5] = pictureBoxSixes;
            categoryArray[6] = pictureBoxThreeKind;
            categoryArray[7] = pictureBoxFourKind;
            categoryArray[8] = pictureBoxFourStraight;
            categoryArray[9] = pictureBoxFiveStraight;
            categoryArray[10] = pictureBoxFullHouse;
            categoryArray[11] = pictureBoxChance;
            categoryArray[12] = pictureBoxYahtzee;

            // Array of checkboxes
            checkBoxArray[0] = checkBox1;
            checkBoxArray[1] = checkBox2;
            checkBoxArray[2] = checkBox3;
            checkBoxArray[3] = checkBox4;
            checkBoxArray[4] = checkBox5;

            // Array of dice PictureBoxes.  Not the array of pictures that populate the picturebox, the picturebox itself (element)
            //pictureBoxArray[0] = picDiceBox1;
            //pictureBoxArray[1] = picDiceBox2;
            //pictureBoxArray[2] = picDiceBox3;
            //pictureBoxArray[3] = picDiceBox4;
            //pictureBoxArray[4] = picDiceBox5;

            // Array of dice images themselves that populate the PictureBox array
            //diceImageArray[0] = Image.FromFile("Images\\1.dice.GIF");
            //diceImageArray[1] = Image.FromFile("Images\\2.dice.GIF");
            //diceImageArray[2] = Image.FromFile("Images\\3.dice.GIF");
            //diceImageArray[3] = Image.FromFile("Images\\4.dice.GIF");
            //diceImageArray[4] = Image.FromFile("Images\\5.dice.GIF");
            //diceImageArray[5] = Image.FromFile("Images\\6.dice.GIF");

            // Array of status images (Green checkmark or Red X)
            //categoryImageArray[0] = Image.FromFile("Images\\green_checkmark.GIF");
            //categoryImageArray[1] = Image.FromFile("Images\\red_x.GIF");

            // Array of images for the roll number
            //rollImageArray[0] = Image.FromFile("Images\\rollZero.PNG");
            //rollImageArray[1] = Image.FromFile("Images\\rollOne.PNG");
            //rollImageArray[2] = Image.FromFile("Images\\rollTwo.PNG");
            //rollImageArray[3] = Image.FromFile("Images\\rollThree.PNG");

            // Display friendly name for all categories (the pictureboxes/categories you select for points)
            pictureBoxOnes.Tag = "Ones";
            pictureBoxTwos.Tag = "Twos";
            pictureBoxThrees.Tag = "Threes";
            pictureBoxFours.Tag = "Fours";
            pictureBoxFives.Tag = "Fives";
            pictureBoxSixes.Tag = "Sixes";
            pictureBoxThreeKind.Tag = "Three of a kind";
            pictureBoxFourKind.Tag = "Four of a kind";
            pictureBoxFourStraight.Tag = "Small straight";
            pictureBoxFiveStraight.Tag = "Large straight";
            pictureBoxFullHouse.Tag = "Full House";
            pictureBoxChance.Tag = "Chance";
            pictureBoxYahtzee.Tag = "Yahtzee";
        }
        #endregion

        #region Destructors
        #endregion

        #region Event Handlers
        /// <summary>
        /// Method starts a new game and initializes the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.GameStatus) // Enter here if there is a game in progress
            {
                DialogResult dialogResult = MessageBox.Show("Game is not over!  Are you sure?", "Quit Game", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Clear the form for the new game (static methods in ultility.cs)
                    Utilities.ResetAllControls(this);
                    // Calls the newGame() method in Yahtzee
                    NewYahtzee.NewGame();
                    // Show roll 0 for a new game
                    //pictureBoxRolls.Image = rollImageArray[0];
                    labelRollNumber.Text = "0";
                    // Show the running bonus for the game
                    textRunBonus.Text = Convert.ToString(NewYahtzee.RunBonus);
                    // Show the running score for the game
                    textRunScore.Text = Convert.ToString(NewYahtzee.RunScore);
                    // Clear score entering selected category (should be handled in utilities in future - code is there, just not working)
                    textSelectedCategory.Text = null;
                    }
            }

            else // Enter here if there is no game in progress
            {          
                // Clear the form for the new game (static methods in ultility.cs)
                Utilities.ResetAllControls(this);
                // Calls the newGame() method in Yahtzee
                NewYahtzee.NewGame();
                // Show roll 0 for a new game
                //pictureBoxRolls.Image = rollImageArray[0];
                labelRollNumber.Text = "0";
                // Show the running bonus for the game
                textRunBonus.Text = Convert.ToString(NewYahtzee.RunBonus);
                // Show the running score for the game
                textRunScore.Text = Convert.ToString(NewYahtzee.RunScore);
            }
        }

        /// <summary>
        /// Calls the rollDice(rollOrHoldArray) in Yahtzee.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRollDice_Click(object sender, EventArgs e)
        {
            btnRollDice.Enabled = false;
            // Loop through all checkboxes.  Build array of bool values (to roll or not to roll - hehehe)
            for (int counter = 0; counter < checkBoxArray.Length; ++counter)
            {
                // Checkbox is not checked (not held)
                if (checkBoxArray[counter].Checked == false)
                {
                    rollOrHoldArray[counter] = true; // Then roll the dice
                }

                // Checkbox is checked (hold the dice)
                else
                {
                    rollOrHoldArray[counter] = false; // Then don't roll the dice
                }
            }

            // Calls the rollEffect(rollOrHoldArray) in Yahtzze.cs - CREATES ROLLING EFFECT - 
            //for (int effectCounter = 0; effectCounter < 7; ++effectCounter)
            //{
            //    DisplayRoll(NewYahtzee.RollEffect(rollOrHoldArray));
            //    Application.DoEvents();
            //    Thread.Sleep(85);
            //}

            // Call the displayRoll method and pass the return of the rollDice method in Yahtzee.cs
            DisplayRoll(NewYahtzee.RollDice(rollOrHoldArray));

            // Display the roll number. 
            //pictureBoxRolls.Image = rollImageArray[NewYahtzee.RollNumber];
            labelRollNumber.Text = NewYahtzee.RollNumber.ToString(); ;

            // Check to see if user has rolled the dice 3 times.  If so, disable to the roll button
            if (NewYahtzee.RollNumber == 3)
            {
                btnRollDice.Enabled = false;
            }
            else
            {
                btnRollDice.Enabled = true;
            }
            btnNextTurn.Enabled = true;
        }

        /// <summary>
        /// Displays the help form.  Has a nice logo and link to wikipedia (rules to playing Yahtzee)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            newHelper = new HelpScreen();
            newHelper.Show();        
        }

        /// <summary>
        /// Sets the game up for the next turn.  Need to click this either after 3 rolls, or if you save/zero a category
        /// Checks if the user has saved a category (SaveStatus=true).  If the user hasn't saved a category, we make them do it before
        /// they can roll the dice again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNextTurn_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.SaveStatus)
            {
                // Set rolls back to 0
                NewYahtzee.RollNumber = 0;
                // Set rollScore back to 0
                NewYahtzee.RollScore = 0;
                // Display the 0 in the roll box.  Roll number box shows the number of rolls used
                labelRollNumber.Text = NewYahtzee.RollNumber.ToString();
                // Enable the 'roll' button again
                btnRollDice.Enabled = true;
                // Enable the 'save' button again
                btnSaveScore.Enabled = true;

                // Uncheck all of the checkboxes for the next turn
                for (int uncheckCounter = 0; uncheckCounter < checkBoxArray.Length; ++uncheckCounter)
                {
                    checkBoxArray[uncheckCounter].Checked = false;
                }

                // Set back to false so validation will work for the next roll
                NewYahtzee.SaveStatus = false;

                // Uncheck 'Take Zero' checkbox for next turn
                if (NewYahtzee.TakeZeroStatus)
                {
                    NewYahtzee.TakeZeroStatus = false;
                    checkBoxTakeZero.Checked = false;
                }

                //// Enable all of the category pictureboxes that are null (so they can be selected for next round of scoring)
                //for (int enableCounter = 0; enableCounter < categoryArray.Length; ++enableCounter)
                //{
                //    if (categoryArray[enableCounter].Image == null)
                //    {
                //        categoryArray[enableCounter].Enabled = true;
                //    }
                //} 
            }

            else
            {
                MessageBox.Show("You haven't saved a category for this round yet!" + "\n" +
                    "Save your points or take a 'Zero' for a caegory first!");
            }
        }

        /// <summary>
        /// Responds to the 'Save Score' click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveScore_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.RollNumber == 0) // User has not rolled the dice at least 1 time.  User is not allowed to 'Save Score'
            {
                NewYahtzee.GameMessages(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.GameTipsMessage.ToString());
            }
            else if(categoryArray[SelectedIndex].Image != null) // User has used this category before (either scored or took a zero)
            {
                NewYahtzee.GameMessages(1);
                MessageBox.Show(NewYahtzee.GameTipsMessage.ToString());
            }
            else // User has rolled at least 1 time
            {
                SaveScore();
            }
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 1's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxOnes_Click(object sender, EventArgs e)
        {
            SelectedIndex = 0;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 2's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxTwos_Click(object sender, EventArgs e)
        {
            SelectedIndex = 1;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 3's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxThrees_Click(object sender, EventArgs e)
        {
            SelectedIndex = 2;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 4's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFours_Click(object sender, EventArgs e)
        {
            SelectedIndex = 3;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 5's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFives_Click(object sender, EventArgs e)
        {
            SelectedIndex = 4;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 6's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSixes_Click(object sender, EventArgs e)
        {
            SelectedIndex = 5;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 3 dice the same
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxThreeKind_Click(object sender, EventArgs e)
        {
            SelectedIndex = 6;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 4 dice the same
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFourKind_Click(object sender, EventArgs e)
        {
            SelectedIndex = 7;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Save's 30 points for the the large straight.  Winning rolls are either 1-2-3-4, 2-3-4-5, 3-4-5-6 (4 in a row)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFourStraight_Click(object sender, EventArgs e)
        {
            SelectedIndex = 8;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 5 card straight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFiveStraight_Click(object sender, EventArgs e)
        {
            SelectedIndex = 9;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Save's 25 points for the full house no matter the dice.  Need a pair and 3 of a kind together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFullHouse_Click(object sender, EventArgs e)
        {
            SelectedIndex = 10;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the Chance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxChance_Click(object sender, EventArgs e)
        {
            SelectedIndex = 11;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the Yahtzee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxYahtzee_Click(object sender, EventArgs e)
        {
            SelectedIndex = 12;
            DisplaySelectedCategory(categoryArray[SelectedIndex].Tag.ToString());
        }

        /// <summary>
        /// For closing the application
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.CloseApplication();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method displays the dice. 
        /// </summary>
        /// <param name="diceArray"></param>
        private void DisplayRoll(int[] diceArray)
        {
            //MessageBox.Show(diceArray[0].ToString() +" ,"+ diceArray[1].ToString() + " ," + diceArray[2].ToString() + " ," + diceArray[3].ToString() + " ," + diceArray[4].ToString());
            //picDiceBox1.Image = diceImageArray[diceArray[0] - 1];
            //picDiceBox2.Image = diceImageArray[diceArray[1] - 1];
            //picDiceBox3.Image = diceImageArray[diceArray[2] - 1];
            //picDiceBox4.Image = diceImageArray[diceArray[3] - 1];
            //picDiceBox5.Image = diceImageArray[diceArray[4] - 1];

            labelDiceBox1.Text = diceArray[0].ToString();
            labelDiceBox2.Text = diceArray[1].ToString();
            labelDiceBox3.Text = diceArray[2].ToString();
            labelDiceBox4.Text = diceArray[3].ToString();
            labelDiceBox5.Text = diceArray[4].ToString();
        }

        /// <summary>
        /// displayes the selected category in the textbox on GUI
        /// </summary>
        public void DisplaySelectedCategory(string selectedCategory)
        {
            textSelectedCategory.Text = selectedCategory;
        }

        /// <summary>
        /// displayes the selected category in the textbox on GUI
        /// </summary>
        public void SaveScore()
        {
            if (textSelectedCategory.Text.Length == 0) // User has not selected a category
            {
                MessageBox.Show("You must select a category first!\nThen either take a 0, or your score!");
            }
            else if (checkBoxTakeZero.Checked) // User wishes to take a zero for the selected category
            {
                if (NewYahtzee.TakeZero(SelectedIndex))
                {
                    //categoryArray[SelectedIndex].Image = categoryImageArray[1];
                    categoryArray[SelectedIndex].BackColor = Color.Red;
                    DisplayScores();
                }
                else
                {
                    MessageBox.Show("Take Zero failed");
                }
            }
            else
            {
                switch (selectedIndex) // Index = the category attempting to either take a 0 or points for
                {
                    case 0:
                        if (NewYahtzee.SaveOnes())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxOnes.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 1:
                        if (NewYahtzee.SaveTwos())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxTwos.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 2:
                        if (NewYahtzee.SaveThrees())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxThrees.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 3:
                        if (NewYahtzee.SaveFours())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFours.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 4:
                        if (NewYahtzee.SaveFives())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFives.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 5:
                        if (NewYahtzee.SaveSixes())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxSixes.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 6:
                        if (NewYahtzee.SaveThreeKind())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxThreeKind.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 7:
                        if (NewYahtzee.SaveFourKind())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFourKind.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 8:
                        if (NewYahtzee.SaveFourStraight())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFourStraight.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 9:
                        if (NewYahtzee.SaveFiveStraight())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFiveStraight.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 10:
                        if (NewYahtzee.SaveFullHouse())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxFullHouse.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 11:
                        if (NewYahtzee.SaveChance())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxChance.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    case 12:
                        if (NewYahtzee.SaveYahtzee())
                        {
                            categoryArray[SelectedIndex].BackColor = Color.Green;
                            //pictureBoxYahtzee.Image = categoryImageArray[0];
                        }
                        else
                        {
                            MessageBox.Show(noScoreSaved);
                        }
                        break;

                    default:
                        break;
                }

                // If the 'Save Score' was successful, Disable the 'Save Score' and 'Roll' buttons.  Clicking 'Next Turn' will enable them
                if (NewYahtzee.SaveStatus)
                {
                    btnSaveScore.Enabled = false;
                    btnRollDice.Enabled = false;
                }

                // Update the GUI with the scores and the log window
                DisplayScores();
            }
        }

        /// <summary>
        /// Method displays scores and information to the gameboard.
        /// </summary>
        private void DisplayScores()
        {
            textRunScore.Text = Convert.ToString(NewYahtzee.RunScore);
            textRunBonus.Text = Convert.ToString(NewYahtzee.RunBonus);

            if (!NewYahtzee.TakeZeroStatus) // Regular round score saved
            {
                gameLogTextBox.AppendText("\nScored: " + NewYahtzee.RollScore + " points.  Category: " + categoryArray[selectedIndex].Tag + ". Dice: " + NewYahtzee.DiceArrayString);
            }
            else // User took a zero for a category (TakeZeroStatus == true)
            {
                gameLogTextBox.AppendText("\nZero: " + NewYahtzee.RollScore + " points.  Category: " + categoryArray[selectedIndex].Tag + ". Dice: " + NewYahtzee.DiceArrayString);
            }

            if (!NewYahtzee.GameStatus) // Check is game is over
            {
                if (NewYahtzee.BonusStatus)
                {
                    gameLogTextBox.AppendText("\nYou earned the 35 point bonus!");
                }
                MessageBox.Show("Game over!\nYour final score is " + NewYahtzee.RunScore);
                btnNextTurn.Enabled = false;
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        public void CloseApplication()
        {
            this.Close();
        }
        #endregion
    }
}
