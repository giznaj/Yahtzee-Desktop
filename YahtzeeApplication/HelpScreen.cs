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
    public partial class HelpScreen : Form
    {
        #region Constructors
        public HelpScreen()
        {
            InitializeComponent();
            //HelpText = "For complete rules visit:";
            //WikiUrl = "http://en.wikipedia.org/wiki/Yahtzee";
        }
        #endregion

        #region Members
        private string helpText;
        private string wikiUrl;
        #endregion
               
        #region Public Properties
        /// <summary>
        /// Public property for the private field 'helpText'
        /// </summary>
        public string HelpText { get; }

        /// <summary>
        /// Public property for the private field 'wikiUrl'
        /// </summary>
        public string WikiUrl { get; }
        #endregion

        #region EventHandlers
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
