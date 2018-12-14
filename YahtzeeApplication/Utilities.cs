using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YahtzeeApplication
{
    class Utilities
    {
        public static void ResetAllControls(Control form)
        {
            foreach (Control parentControl in form.Controls)
            {
                if (parentControl is GroupBox) // Goes inside the outter groupbox
                {
                    foreach (Control childControl in parentControl.Controls)
                    {
                        if (childControl is PictureBox) // Clears the PictureBox controls
                        {
                            PictureBox pictureBox = (PictureBox)childControl;
                            pictureBox.Image = null;
                            pictureBox.Enabled = true; // Enable the boxes to be clicked by the user again
                        }

                        if (childControl is CheckBox) // Clears the CheckBox controls
                        {
                            if (childControl.Name == "checkBoxWarning") // Leave the 'Optional Game Warning' checkbox alone
                            {
                                continue;
                            }

                            CheckBox checkBox = (CheckBox)childControl;
                            checkBox.Checked = false;
                        }

                        if (childControl is Button) // Enables the Button controls
                        {
                            Button button = (Button)childControl;
                            button.Enabled = true; // Enable the button to be clicked by the user again
                        }

                        if (childControl is RichTextBox)
                        {
                            RichTextBox richText = (RichTextBox)childControl;
                            richText.Text = null;
                        }

                        if (childControl is TextBox)
                        {
                            TextBox textBox = (TextBox)childControl;
                            textBox.Text = null;
                        }
                    }
                }

                // Called when a new game is started.  enables all groupbox controls
                if (parentControl is GroupBox)
                {
                    GroupBox groupBox = (GroupBox)parentControl;
                    groupBox.Enabled = true;
                }

                // Called when a new game is started.  Clears all values in all richtextbox controls
                if (parentControl is RichTextBox)
                {
                    RichTextBox richText = (RichTextBox)parentControl;
                    richText.Text = null;
                }

                // Called when a new game is started.  Clears all selections in all combobox controls
                if (parentControl is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)parentControl;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;
                }

                // Called when a new game is started.  Clears all selections in all listbox controls
                if (parentControl is ListBox)
                {
                    ListBox listBox = (ListBox)parentControl;
                    listBox.ClearSelected();
                }

                // Called when a new game is started.  Clears the text in all textbox controls
                if (parentControl is TextBox)
                {
                    TextBox textBox = (TextBox)parentControl;
                    textBox.Text = null;
                }
            }
        }
    }
}
