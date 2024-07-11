using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MautMetoda
{
    public partial class AddParameterForm : Form
    {
        public string ParameterName { get; private set; }
        public string ParameterType { get; private set; }
        public int ParameterWeight { get; private set; }
        public AddParameterForm()
        {
            InitializeComponent();
        }

        private void confirmation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(weightTextBox.Text) || (!basicRadioButton.Checked && !compositRadioButton.Checked))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            this.ParameterName = nameTextBox.Text.Replace(" ", "_");
            this.ParameterType = basicRadioButton.Checked ? "Basic" : "Composite";

            if (!int.TryParse(weightTextBox.Text, out int weight))
            {
                MessageBox.Show("Weight must be a valid number.");
                return;
            }

            this.ParameterWeight = weight;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
