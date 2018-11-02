﻿// GameBoard class (code)
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace YahtzeeApplication
{
    public partial class GameBoard : Form
    {
        #region Private Members
        bool[] rollOrHoldArray = new bool[5]; // Stores the bool value for each dice for when rolling.  (hold = false, true = roll)
        CheckBox[] checkBoxArray = new CheckBox[5]; // Array of checkboxes
        PictureBox[] pictureBoxArray = new PictureBox[5]; // Array of picture boxes for the dice
        PictureBox[] categoryArray = new PictureBox[13]; // Array of categories for the user to score with
        Image[] diceImageArray = new Image[6]; // Array of dice images
        Image[] categoryImageArray = new Image[2]; // Array of images for category (green checmkar, red x)
        Image[] rollImageArray = new Image[4]; // Array of images for the roll number
        Random randomNumbers = new Random(); // random-number generator
        Yahtzee NewYahtzee; // Declare Yahtzee.cs object (gameboard logic)
        HelpScreen newHelper; // Declare new HelpScreen.cs object (help screen) 
        bool newGameMessage; // User enables or disables the warning message when a new game starts
        bool saveStatus; // true if the user has selected a category for the current round of rolls

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

        #endregion

        #region Initializers
        // instantiate the game logic and the help form
        private void Yahtzee_Load(object sender, EventArgs e)
        {
            this.btnRollDice.Enabled = false; // Disable the roll button until a new game is started
            this.btnNextTurn.Enabled = false; // Disable the next turn button until the new game is started

            // Disable all category boxes before the game starts.
            this.DisableAllCategory();
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
            pictureBoxArray[0] = picDiceBox1;
            pictureBoxArray[1] = picDiceBox2;
            pictureBoxArray[2] = picDiceBox3;
            pictureBoxArray[3] = picDiceBox4;
            pictureBoxArray[4] = picDiceBox5;

            // Array of dice images that populate the PictureBox array
            diceImageArray[0] = Image.FromFile("1.dice.GIF");
            diceImageArray[1] = Image.FromFile("2.dice.GIF");
            diceImageArray[2] = Image.FromFile("3.dice.GIF");
            diceImageArray[3] = Image.FromFile("4.dice.GIF");
            diceImageArray[4] = Image.FromFile("5.dice.GIF");
            diceImageArray[5] = Image.FromFile("6.dice.GIF");

            // Array of status images (Green checkmark or Red X)
            categoryImageArray[0] = Image.FromFile("green_checkmark.GIF");
            categoryImageArray[1] = Image.FromFile("red_x.GIF");

            // Array of images for the roll number
            rollImageArray[0] = Image.FromFile("rollZero.PNG");
            rollImageArray[1] = Image.FromFile("rollOne.PNG");
            rollImageArray[2] = Image.FromFile("rollTwo.PNG");
            rollImageArray[3] = Image.FromFile("rollThree.PNG");

            // Display friendly name for all categories (the pictureboxes you click for points)
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

            // Enable the optional game warnings
            checkBoxWarning.Checked = true;
        }
        #endregion

        #region Destructors
        #endregion

        #region Public Methods
        /// <summary>
        /// Method displays scores and information to the gameboard.
        /// </summary>
        private void DisplayScores(object sender)
        {
            textRunScore.Text = Convert.ToString(NewYahtzee.RunScore);
            textRunBonus.Text = Convert.ToString(NewYahtzee.RunBonus);
            gameLogTextBox.AppendText("\nYou just scored: " + NewYahtzee.RollScore + " points for your " + ((PictureBox)sender).Tag);
            btnRollDice.Enabled = false;

            if (!NewYahtzee.GameStatus)
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
        /// Method displays the dice.  Images are in 'Projects\Yahtzee\YahtzeeApplication\YahtzeeApplication\bin\Debug'
        /// </summary>
        /// <param name="diceArray"></param>
        private void DisplayRoll(int[] diceArray)
        {
            picDiceBox1.Image = diceImageArray[diceArray[0] - 1];
            picDiceBox2.Image = diceImageArray[diceArray[1] - 1];
            picDiceBox3.Image = diceImageArray[diceArray[2] - 1];
            picDiceBox4.Image = diceImageArray[diceArray[3] - 1];
            picDiceBox5.Image = diceImageArray[diceArray[4] - 1];
        }

        /// <summary>
        /// Disables all category pictureboxes until the next roll.  Stops the user from clicking more than 1 category between rolls
        /// </summary>
        public void DisableAllCategory()
        {
            for (int disableCounter = 0; disableCounter < categoryArray.Length; ++disableCounter)
            {
                categoryArray[disableCounter].Enabled = false;
            }
        }
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
                    if (checkBoxWarning.Checked)
                    {
                        MessageBox.Show("Clicking a category without the right combination of dice, means you are taking a '0' for that category!" +
                            "\n\nClicking a category that has already been used ('Checkmark' or 'X') will do nothing!");
                    }

                    // Clear the form for the new game (static methods in ultility.cs)
                    Utilities.ResetAllControls(this);
                    // Calls the newGame() method in Yahtzee
                    NewYahtzee.NewGame(checkBoxWarning.Checked); // Pass the boolean value Yahtzee.  Used for optional warning messages.
                    // Show roll 0 for a new game
                    pictureBoxRolls.Image = rollImageArray[0];
                    // Show the running bonus for the game
                    textRunBonus.Text = Convert.ToString(NewYahtzee.RunBonus);
                    // Show the running score for the game
                    textRunScore.Text = Convert.ToString(NewYahtzee.RunScore);
                }
            }

            else
            {
                if (checkBoxWarning.Checked) // Enter here if there is no game in progress
                {
                    MessageBox.Show("Clicking a category without the right combination of dice, means you are taking a '0' for that category!" +
                            "\n\nClicking a category that has already been used ('Checkmark' or 'X') will do nothing!");
                }

                // Clear the form for the new game (static methods in ultility.cs)
                Utilities.ResetAllControls(this);
                // Calls the newGame() method in Yahtzee
                NewYahtzee.NewGame(checkBoxWarning.Checked); // Pass the boolean value Yahtzee.  Used for optional warning messages
                // Show roll 0 for a new game
                pictureBoxRolls.Image = rollImageArray[0];
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
            this.btnRollDice.Enabled = false;

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
            for (int effectCounter = 0; effectCounter < 7; ++effectCounter)
            {
                DisplayRoll(NewYahtzee.RollEffect(rollOrHoldArray));
                Application.DoEvents();
                Thread.Sleep(85);
            }

            // Call the displayRoll method and pass the return of the rollDice method in Yahtzee.cs
            DisplayRoll(NewYahtzee.RollDice(rollOrHoldArray));

            // Display the roll number. 
            pictureBoxRolls.Image = rollImageArray[NewYahtzee.RollNumber];

            // Check to see if user has rolled the dice 3 times.  If so, disable to the roll button
            if (NewYahtzee.RollNumber == 3)
            {
                this.btnRollDice.Enabled = false;
            }

            else
            {
                this.btnRollDice.Enabled = true;
            }

            this.btnNextTurn.Enabled = true;
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
            if (SaveStatus)
            {
                // Set rolls back to 0
                NewYahtzee.RollNumber = 0;
                // Set rollScore back to 0
                NewYahtzee.RollScore = 0;
                // Display the 0 in the roll box.  Roll number box shows the number of rolls used
                pictureBoxRolls.Image = rollImageArray[0];
                // Enable the roll button again
                this.btnRollDice.Enabled = true;

                // Uncheck all of the checkboxes for the next turn
                for (int uncheckCounter = 0; uncheckCounter < checkBoxArray.Length; ++uncheckCounter)
                {
                    checkBoxArray[uncheckCounter].Checked = false;
                }

                // Enable all of the category pictureboxes that are null (so they can be selected for next round of scoring)
                for (int enableCounter = 0; enableCounter < categoryArray.Length; ++enableCounter)
                {
                    if (categoryArray[enableCounter].Image == null)
                    {
                        categoryArray[enableCounter].Enabled = true;
                    }
                }

                SaveStatus = false; // Set back to false so validation will work for the next roll
            }

            else
            {
                MessageBox.Show("You haven't saved a category for this round yet!");
            }
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 1's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxOnes_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveOnes())
            {
                pictureBoxOnes.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxOnes.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 2's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxTwos_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveTwos())
            {
                pictureBoxTwos.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxTwos.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 3's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxThrees_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveThrees())
            {
                pictureBoxThrees.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxThrees.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender); 
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 4's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFours_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFours())
            {
                pictureBoxFours.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFours.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 5's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFives_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFives())
            {
                pictureBoxFives.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFives.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 6's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxSixes_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveSixes())
            {
                pictureBoxSixes.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxSixes.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 3 dice the same
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxThreeKind_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveThreeKind())
            {
                pictureBoxThreeKind.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxThreeKind.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 4 dice the same
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFourKind_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFourKind())
            {
                pictureBoxFourKind.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFourKind.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Save's 30 points for the the large straight.  Winning rolls are either 1-2-3-4, 2-3-4-5, 3-4-5-6 (4 in a row)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFourStraight_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFourStraight())
            {
                pictureBoxFourStraight.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFourStraight.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the 5 card straight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFiveStraight_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFiveStraight())
            {
                pictureBoxFiveStraight.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFiveStraight.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Save's 25 points for the full house no matter the dice.  Need a pair and 3 of a kind together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFullHouse_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveFullHouse())
            {
                pictureBoxFullHouse.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxFullHouse.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the Chance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxChance_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveChance())
            {
                pictureBoxChance.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxChance.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// Saves score and marks category appropriately when user clicks the picture box for the Yahtzee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxYahtzee_Click(object sender, EventArgs e)
        {
            if (NewYahtzee.NoviceMode && NewYahtzee.RollNumber < 3) // Enter here if novice mode is enabled
            {
                NewYahtzee.SetNoviceModeMessage(NewYahtzee.RollNumber);
                MessageBox.Show(NewYahtzee.NoviceModeMessage.ToString());
            }

            SaveStatus = true;
            DisableAllCategory();
            if (NewYahtzee.SaveYahtzee())
            {
                pictureBoxYahtzee.Image = categoryImageArray[0];
            }
            else
            {
                pictureBoxYahtzee.Image = categoryImageArray[1];
            }

            this.DisplayScores(sender);
        }

        /// <summary>
        /// If checked turn novice mode on and display optional game warnings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxWarning_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWarning.Checked)
            {
                NewYahtzee.NoviceMode=true;
            }

            else
            {
                NewYahtzee.NoviceMode = false;
            }
        }
        #endregion
    }
}