using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YahtzeeApplication
{
    public partial class SplashScreen : Form
    {
        private GameBoard NewGUI; // Declare GameBoard object

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            NewGUI = new GameBoard(); // Instantiate a new GameBoard object (GameBoard.cs)
        }

        public void button1_Click(object sender, EventArgs e)
        {
            NewGUI.Show(); // Loads the new GUI gameboard (NewGUI)
        }
    }
}
