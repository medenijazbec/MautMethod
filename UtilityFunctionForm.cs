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
    public partial class UtilityFunctionForm : Form
    {
        public string FunctionType { get; private set; }
        public double MinValue { get; private set; }
        public double MaxValue { get; private set; }
        public UtilityFunctionForm(double minValue, double maxValue)
        {
            InitializeComponent();
            minValueTextBox.Text = minValue.ToString();
            maxValueTextBox.Text = maxValue.ToString();
            functionComboBox.Items.AddRange(new string[] { "Linear", "Exponential", "Logarithmic", "Concave", "Convex", "InverseLinear" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void confirmation_Click(object sender, EventArgs e)
        {
            this.FunctionType = functionComboBox.SelectedItem?.ToString();
            this.MinValue = double.Parse(minValueTextBox.Text);
            this.MaxValue = double.Parse(maxValueTextBox.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
