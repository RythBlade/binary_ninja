using System;
using System.Windows.Forms;

namespace binary_viewer.Dialogues
{
    public partial class PropertiesDialogue : Form
    {
        public static DialogResult Show(object dataSource)
        {
            DialogResult resultToReturn = DialogResult.Cancel;

            if (dataSource != null)
            {
                PropertiesDialogue dialogue = new PropertiesDialogue();
                dialogue.propertyGrid.SelectedObject = dataSource;

                resultToReturn = dialogue.ShowDialog();
            }

            return resultToReturn;
        }

        private PropertiesDialogue()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}
