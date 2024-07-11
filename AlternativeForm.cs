using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MautMetoda
{
    public partial class AlternativeForm : Form
    {
        public string AlternativeName { get; private set; }
        public List<string> RawParameterValues { get; private set; }
        public List<double> NumericParameterValues { get; private set; }

        private TextBox nameTextBox;
        private List<TextBox> parameterTextBoxes;

        public AlternativeForm(List<string> parameterNames)
        {
            InitializeComponent();

            this.Text = "Alternative";
            this.Width = 400;
            this.Height = 400;

            Label nameLabel = new Label() { Left = 10, Top = 20, Text = "Alternative Name:", Width = 100 };
            nameTextBox = new TextBox() { Left = 120, Top = 20, Width = 250 };
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);

            int top = 60;
            parameterTextBoxes = new List<TextBox>();

            foreach (var paramName in parameterNames)
            {
                Label paramLabel = new Label() { Left = 10, Top = top, Text = paramName + ":", Width = 100 };
                TextBox paramTextBox = new TextBox() { Left = 120, Top = top, Width = 250 };
                paramTextBox.Name = paramName;
                this.Controls.Add(paramLabel);
                this.Controls.Add(paramTextBox);
                parameterTextBoxes.Add(paramTextBox);

                top += 40;
            }

            Button confirmation = new Button() { Text = "Ok", Left = 270, Width = 100, Top = top };
            confirmation.Click += (sender, e) =>
            {
                this.AlternativeName = nameTextBox.Text.Replace(" ", "_");
                this.RawParameterValues = new List<string>();
                this.NumericParameterValues = new List<double>();

                foreach (var paramTextBox in parameterTextBoxes)
                {
                    string rawValue = paramTextBox.Text.Replace(" ", "_");
                    this.RawParameterValues.Add(rawValue);

                    string numericString = new string(rawValue.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());
                    if (double.TryParse(numericString, out double numericValue))
                    {
                        this.NumericParameterValues.Add(numericValue);
                    }
                    else
                    {
                        // Add 0 to NumericParameterValues if the parsed value is not valid
                        this.NumericParameterValues.Add(0);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.Add(confirmation);
        }

        public AlternativeForm(List<string> parameterNames, string alternativeName, List<string> rawParameterValues) : this(parameterNames)
        {
            this.AlternativeName = alternativeName;
            this.RawParameterValues = rawParameterValues;
            InitializeFields();
        }

        private void InitializeFields()
        {
            nameTextBox.Text = this.AlternativeName;
            for (int i = 0; i < this.RawParameterValues.Count; i++)
            {
                parameterTextBoxes[i].Text = this.RawParameterValues[i];
            }
        }
    }
}
