// The famous game of Yahtzee.  I started this game back in November 2002 and used JavaScript initially.  Game needs to be ported
// to Java and C#.  
//
// Aaron Toth

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YahtzeeApplication
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameBoard()); // Loads the Yahtzee GameBoard
        }
    }
}
