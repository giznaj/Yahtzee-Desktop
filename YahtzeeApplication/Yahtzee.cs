﻿// The famous game of Yahtzee.  I started this game back in November 2002 and used JavaScript initially.
//
// Yahtzee 1.0
// Date: July 16/2014 (Marley)
// Author: Aaron Toth
// Platform: C# .Net 4.5
//
// History:  Yahtzee began in 2003 as a simple HTML page with Javascript.  The game has now been converted to the .Net 4.5 platform.  
// Program is written in C# and will be ported to the Java platform.
//
// Aaron Toth

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YahtzeeApplication;

namespace YahtzeeApplication
{
    class Yahtzee
    {
        #region Private Members
        int[] diceArray; // Array of dice
        int categoryUsed; // When this becomes 13, the game is over.  There are 13 categories in the game of Yahtzee
        Random randomNumbers = new Random(); // random-number generator
        private int numOfYahtzee; // Keeps track of the number of Yahtzee's the user has scored
        private int rollNumber; // (rollNumber) Roll number.  This is what roll number the player is on 
        private int runBonus; // (runBonus) Running bonus (63 gets you 35 more)
        private int runScore; // (runScore) Running score.  This is your total score
        private int rollScore; // (rollScore) Roll Score.  Stores the value of the score being saved to the total
        private bool gameStatus; // Stores the games status.  If true, game is active.  If false, game is over
        private bool bonusStatus; // True if the user gets the 35 point bonus for the upper section
        private bool noviceMode; // True to display the optional game warnings (novice users)
        private string noviceModeMessage; // Stores the value of text to be displayed for optional warning messages
        private bool saveStatus = false; //status of the round scoring
        #endregion

        #region Public Properties
        /// <summary>
        /// Public property for the private field 'rollNumber'
        /// </summary>
        public int RollNumber
        {
            get { return rollNumber; }
            set { rollNumber = value; }
        }

        /// <summary>
        /// Public property for the private field 'runBonus'
        /// </summary>
        public int RunBonus
        {
            get { return runBonus; }
            set { runBonus = value; }
        }

        /// <summary>
        /// Public property for the private field 'runScore'
        /// </summary>
        public int RunScore
        {
            get { return runScore; }
            set { runScore = value; }
        }

        /// <summary>
        /// Public property for the private field 'rollScore'
        /// </summary>
        public int RollScore
        {
            get { return rollScore; }
            set { rollScore = value; }
        }

        /// <summary>
        /// Public property for categoryUsed.  When the 13th is reached the game is over
        /// </summary>
        public int CategoryUsed
        {
            get { return categoryUsed; }
            set 
            {
                if (CategoryUsed == 12)
                {
                    categoryUsed = value;
                    GameStatus = false;
                }

                else
                {
                    categoryUsed = value;
                }
            }
        }

        /// <summary>
        /// Status for the game
        /// </summary>
        public bool GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value; }
        }

        /// <summary>
        /// Gets and sets the value for the numOfYahtzee field
        /// </summary>
        public int NumOfYahtzee
        {
            get { return numOfYahtzee; }
            set { numOfYahtzee = value; }
        }

        /// <summary>
        /// Sets and gets the bonus status
        /// </summary>
        public bool BonusStatus
        {
            get { return bonusStatus; }
            set { bonusStatus = value; }
        }

        /// <summary>
        /// If true, the user will see optional warning messages when playing.  This is for novice users.
        /// </summary>
        public bool NoviceMode
        {
            get { return noviceMode; }
            set { noviceMode = value; }
        }

        /// <summary>
        /// Sets and gets the value of the string for optional game warnings
        /// </summary>
        public string NoviceModeMessage
        {
            get { return noviceModeMessage; }
            set { noviceModeMessage = value; }
        }

        /// <summary>
        /// Sets and gets the boolean value for the save status for the round score
        /// </summary>
        public bool SaveStatus
        {
            get { return saveStatus; }
            set { saveStatus = value; }
        }

        /// <summary>
        /// Gets the diceArray object for the caller
        /// </summary>
        public string DiceArray
        {
            get { return " (" + diceArray[0].ToString() + ", " + diceArray[1].ToString() + ", " + diceArray[2].ToString() + ", " + diceArray[3].ToString() + ", " + diceArray[4].ToString() + ")"; }
        }
        #endregion

        #region Initializers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Yahtzee_Load(object sender, EventArgs e)
        {
            // Todo
        } 
        #endregion

        #region Constructors
        // Default Constructor for the class.
        public Yahtzee()
        {
            diceArray = new int[5];
            for (int x = 0; x <= 4; ++x)
            {
                diceArray[x] = 0;
            }

            numOfYahtzee = 0;
        }
        #endregion

        #region Destructors
        ~Yahtzee()
        {
            try
            {
                GC.Collect();
            }
            finally
            {
                Console.WriteLine("Collection and Finalizing complete!");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method sets the new game values to 0
        /// </summary>
        /// <param name="isNovice">parameter is true is </param>
        public void NewGame(bool isNovice)
        {
            if(isNovice)
            {
                NoviceMode = true; // if box is checked in the GUI, set the mode to Novice (Optional warning messages are displayed)
            }

            RollNumber = 0;
            RunBonus = 0;
            RunScore = 0;
            RollScore = 0;
            CategoryUsed = 0;
            GameStatus = true;
        }

        /// <summary>
        /// Method rolls the dice and returns the array of rolled dice to the calling method.  Method also accepts an array as a parameter of
        /// type boolean.  This array aligns to the dice array and determines which dice are 'held' and which are 'rolled'
        /// </summary>
        /// <param name="rollOrHoldArray"></param>
        /// <returns></returns>
        public int[] RollDice(bool[] rollOrHoldArray)
        {
            if (rollOrHoldArray[0] == true)
            {
                diceArray[0] = randomNumbers.Next(1, 7); // Dice #1
            }

            if (rollOrHoldArray[1] == true)
            {
                diceArray[1] = randomNumbers.Next(1, 7); // Dice #2
            }

            if (rollOrHoldArray[2] == true)
            {
                diceArray[2] = randomNumbers.Next(1, 7); // Dice #3
            }

            if (rollOrHoldArray[3] == true)
            {
                diceArray[3] = randomNumbers.Next(1, 7); // Dice #4
            }

            if (rollOrHoldArray[4] == true)
            {
                diceArray[4] = randomNumbers.Next(1, 7); // Dice #5
            }

            ++RollNumber;
            return diceArray;
        }

        /// <summary>
        /// Method creates a nice little effect that simulates the rolling of the dice
        /// </summary>
        /// <param name="rollOrHoldArray"></param>
        /// <returns></returns>
        public int[] RollEffect(bool[] rollOrHoldArray)
        {
            if (rollOrHoldArray[0] == true)
            {
                diceArray[0] = randomNumbers.Next(1, 7); // Dice #1
            }

            if (rollOrHoldArray[1] == true)
            {
                diceArray[1] = randomNumbers.Next(1, 7); // Dice #2
            }

            if (rollOrHoldArray[2] == true)
            {
                diceArray[2] = randomNumbers.Next(1, 7); // Dice #3
            }

            if (rollOrHoldArray[3] == true)
            {
                diceArray[3] = randomNumbers.Next(1, 7); // Dice #4
            }

            if (rollOrHoldArray[4] == true)
            {
                diceArray[4] = randomNumbers.Next(1, 7); // Dice #5
            }

            return diceArray;
        }

        /// <summary>
        /// Adds the total of the 1's to the score.  It checks first to find a '1'
        /// </summary>
        public bool SaveOnes()
        {
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 1)
                {
                    RollScore += 1;
                    saveStatus = true; // We found a 1, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Adds the total of the 2's to the score.  It checks first to find a '2'
        /// </summary>
        public bool SaveTwos()
        {
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 2)
                {
                    RollScore += 2;
                    saveStatus = true; // We found a 2, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Adds the total of the 3's to the score.  It checks first to find a '3'
        /// </summary>
        /// <returns></returns>
        public bool SaveThrees()
        {
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 3)
                {
                    RollScore += 3;
                    saveStatus = true; // We found a 3, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Adds the total of the 4's to the score.  It checks first to find a '4'
        /// </summary>
        /// <returns></returns>
        public bool SaveFours()
        {
            bool saveStatus = false; // Set save status to false initially.  If we find a 4, change the bool value to true
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 4)
                {
                    RollScore += 4;
                    saveStatus = true; // We found a 4, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Adds the total of the 5's to the score.  It checks first to find a '5'
        /// </summary>
        /// <returns></returns>
        public bool SaveFives()
        {
            bool saveStatus = false; // Set save status to false initially.  If we find a 5, change the bool value to true
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 5)
                {
                    RollScore += 5;
                    saveStatus = true; // We found a 5, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Adds the total of the 6's to the score.  It checks first to find a '6'
        /// </summary>
        /// <returns></returns>
        public bool SaveSixes()
        {
            bool saveStatus = false; // Set save status to false initially.  If we find a 6, change the bool value to true
            for (int diceCounter = 0; diceCounter < diceArray.Length; ++diceCounter)
            {
                if (diceArray[diceCounter] == 6)
                {
                    RollScore += 6;
                    saveStatus = true; // We found a 6, changing bool value to true
                }
            }

            SaveScore(RollScore);
            return saveStatus;
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 3 dice the same
        /// </summary>
        /// <returns></returns>
        public bool SaveThreeKind()
        {
            bool saveStatus = false;
            int threeKindCounter;
            var duplicates = diceArray.GroupBy(g => g).Where(w => w.Count() > 2).Select(s => s.Key); // Check if 3 are the same
            if (duplicates.Count() > 0)
            {
                saveStatus = true;
                for (threeKindCounter = 0; threeKindCounter < diceArray.Length; ++threeKindCounter)
                {
                    RollScore += diceArray[threeKindCounter];
                }
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Save's total points of all dice if there are at least 4 dice the same
        /// </summary>
        /// <returns></returns>
        public bool SaveFourKind()
        {
            bool saveStatus = false;
            int fourKindCounter;
            var duplicates = diceArray.GroupBy(g => g).Where(w => w.Count() >3).Select(s => s.Key); // Check if 4 are the same
            if (duplicates.Count() > 0)
            {
                saveStatus = true;
                for (fourKindCounter = 0; fourKindCounter < diceArray.Length; ++fourKindCounter)
                {
                    RollScore += diceArray[fourKindCounter];
                }
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Save's 30 points for the the large straight.  Winning rolls are either 1-2-3-4, 2-3-4-5, 3-4-5-6 (4 in a row)
        /// </summary>
        /// <returns></returns>
        public bool SaveFourStraight()
        {
            bool saveStatus;
            int elementID;

            // Winning combo has to be either 1-2-3-4, 2-3-4-5 or 3-4-5-6.  
            // This block logic checks for 1-2-3-4
            int elementValue = 1;
            do
            {
                saveStatus = false;
                for (elementID = 0; elementID < diceArray.Length; ++elementID)
                {
                    if (diceArray[elementID] == elementValue)
                    {
                        saveStatus = true;
                    }
                }

                ++elementValue;
                elementID = 0;

            } while (saveStatus && elementValue < 5);

            // if 1-2-3-4 doesn't exist, check for 2-3-4-5
            if (!saveStatus) 
            {
                // Winning combo has to be either 1-2-3-4, 2-3-4-5 or 3-4-5-6.  
                // This block logic checks for 2-3-4-5
                elementValue = 2;
                do
                {
                    saveStatus = false;
                    for (elementID = 0; elementID < diceArray.Length; ++elementID)
                    {
                        if (diceArray[elementID] == elementValue)
                        {
                            saveStatus = true;
                        }
                    }

                    ++elementValue;
                    elementID = 0;

                } while (saveStatus && elementValue < 6);
            }

            // if 2-3-4-5 doesn't exist, check for 3-4-5-6
            if (!saveStatus)
            {
                // Winning combo has to be either 1-2-3-4, 2-3-4-5 or 3-4-5-6.  
                // This block logic checks for 3-4-5-6
                elementValue = 3;
                do
                {
                    saveStatus = false;
                    for (elementID = 0; elementID < diceArray.Length; ++elementID)
                    {
                        if (diceArray[elementID] == elementValue)
                        {
                            saveStatus = true;
                        }
                    }

                    ++elementValue;
                    elementID = 0;

                } while (saveStatus && elementValue < 7);
            }
            
            // If the 4 card straight exists, give the player 30 points
            if (saveStatus)
            {
                RollScore += 30;
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Save's 40 points for the the large straight.  Winning rolls are either 1-2-3-4-5 or 2-3-4-5-6 (5 in a row)
        /// </summary>
        /// <returns></returns>
        public bool SaveFiveStraight()
        {
            int fiveCounter;
            bool saveStatus;
            Array.Sort(diceArray); // Sort array in ascending order.  Winning combo has to be either 1-2-3-4-5 or 2-3-4-5-6
            if (diceArray[0] == 1) // If first element is a 1, we check for 1-2-3-4-5
            {
                saveStatus = true;
                for (fiveCounter = 0; fiveCounter < diceArray.Length; ++fiveCounter)
                {
                    if (diceArray[fiveCounter] != fiveCounter + 1)
                    {
                        saveStatus = false;
                    }
                }    
            }
            
            else if (diceArray[0] == 2) // If first element is a 2, we check for 2-3-4-5-6
            {
                saveStatus = true;
                for (fiveCounter = 0; fiveCounter < diceArray.Length; ++fiveCounter)
                {
                    if (diceArray[fiveCounter] != fiveCounter + 2)
                    {
                        saveStatus = false;
                    }
                }  
            }
            
            else  // Lowest card the player has is a 3, so there is no way to achieve a 5 card straight
            {
                saveStatus = false;
            }

            // If the 5 card straight exists, give the player 40 points
            if (saveStatus)
            {
                RollScore += 40;
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Save's 25 points for the full house no matter the dice.  Need a pair and 3 of a kind together.
        /// </summary>
        /// <returns></returns>
        public bool SaveFullHouse()
        {
            bool saveStatus;
            Dictionary<int, int> counts = diceArray.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            var threeOfAKind = diceArray.GroupBy(g => g).Where(w => w.Count() == 3).Select(s => s.Key); // Check if 3 are the same
            var twoOfAKind = diceArray.GroupBy(g => g).Where(w => w.Count() == 2).Select(s => s.Key); // Check if 2 are the same

            if(threeOfAKind.Count() == 1 && twoOfAKind.Count() == 1) // True if there are 2 groups of numbers

            {
                RollScore += 25;
                saveStatus = true;
            }

            else
            {
                saveStatus = false;
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Save's the sum of the current dice in each box
        /// </summary>
        /// <returns></returns>
        public bool SaveChance()
        {
            bool saveStatus;
            int chanceCounter;
            for (chanceCounter = 0; chanceCounter < diceArray.Length; ++chanceCounter)
            {
                RollScore += diceArray[chanceCounter];
            }


            SaveScore();
            saveStatus = true;
            return saveStatus;
        }

        /// <summary>
        /// Save's 50 points for the first yahtzee and 100 for each subsequent Yahtzee in the current game
        /// </summary>
        /// <returns></returns>
        public bool SaveYahtzee()
        {
            bool saveStatus;
            Array.Sort(diceArray); // Sort the array in ascending order.
            if (diceArray[0] != diceArray[4]) // If element 0 and element 4 are not the same, there is no Yahtzee
            {
                saveStatus = false;
            }

            else // Yahtzee is found!  :-)
            {
                saveStatus = true;
                if (NumOfYahtzee >= 1)
                {
                    RollScore += 100;
                }

                else
                {
                    RollScore += 50;
                }

                NumOfYahtzee += NumOfYahtzee;
            }

            SaveScore();
            return saveStatus;
        }

        /// <summary>
        /// Performs the validation and updates the scores for the upper section (i.e 1's, 2's, 3's, 4's, 5's and 6's)
        /// </summary>
        public void SaveScore()
        {
            RunScore += RollScore;
            CategoryUsed += 1;

            if (CategoryUsed == 13) // If the game is over, then check if the bonus needs to be added.
            {
                if (RunBonus >= 63) // If the user scored 63 or more on the upper section, award 25 more points at end of game
                {
                    RunScore += 35;
                    BonusStatus = true;
                }
            }
        }

        /// <summary>
        /// Performs the validation and updates the scores for the lower section (i.e. 3 of a kind, 4 of a kind, full house...etc)
        /// </summary>
        /// <param name="rollScore">The score being saved for this roll</param>
        public void SaveScore(int rollScore)
        {
            RunBonus += rollScore;
            RunScore += rollScore;
            CategoryUsed += 1;

            if (CategoryUsed == 13) // If the game is over, then check if the bonus needs to be added.
            {
                if (RunBonus >= 63) // If the user scored 63 or more on the upper section, award 25 more points at end of game
                {
                    RunScore += 35;
                    BonusStatus = true;
                }
            }
        }

        /// <summary>
        /// Returns a string based on a boolean and paramater value.
        /// if the boolean value is true, the novice mode is on (enabled)
        /// depending on what the parameter is, the message will change.
        /// </summary>
        /// <returns></returns>
        public void SetNoviceModeMessage(int noviceMessageIndex)    
        {
            if (NoviceMode)
            {
                // MessageBox.Show("Roll number: " + RollNumber + "/ MessageIndex: " + noviceMessageIndex);
                switch (noviceMessageIndex)
                {
                    case 0: // When the user saves their score before rolling the dice at all
                        NoviceModeMessage = "You have to roll the dice at least 1 time.\nNot rolling at all and selecting a category will always result in 0 points!";
                        break;
                    case 1: // When the user rolls the dice 1 time
                        NoviceModeMessage = "Rolling 1 time is allowed, but you have 2 more rolls... use them next time!";
                        break;
                    case 2: // When the user rolls the dice 2 times
                        NoviceModeMessage = "Rolling 2 times is allowed and sometimes you get what you want with only 2\nWhy not use the 3rd?  Maybe a Yahtzee is waiting?";
                        break;
                }
            }
        }
        #endregion            
    }
}

