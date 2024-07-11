namespace MautMetoda
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeViewParameters = new TreeView();
            dataGridViewAlternatives = new DataGridView();
            dataGridViewNumerical = new DataGridView();
            buttonAddParameter = new Button();
            buttonEditParameter = new Button();
            buttonDeleteParameter = new Button();
            buttonSetUtilityFunction = new Button();
            buttonCalculate = new Button();
            buttonAddAlternative = new Button();
            buttonEditAlternative = new Button();
            checkBox1 = new CheckBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAlternatives).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNumerical).BeginInit();
            SuspendLayout();
            // 
            // treeViewParameters
            // 
            treeViewParameters.Location = new Point(12, 12);
            treeViewParameters.Name = "treeViewParameters";
            treeViewParameters.Size = new Size(227, 511);
            treeViewParameters.TabIndex = 0;
            // 
            // dataGridViewAlternatives
            // 
            dataGridViewAlternatives.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAlternatives.Location = new Point(245, 12);
            dataGridViewAlternatives.Name = "dataGridViewAlternatives";
            dataGridViewAlternatives.RowTemplate.Height = 25;
            dataGridViewAlternatives.Size = new Size(808, 511);
            dataGridViewAlternatives.TabIndex = 1;
            // 
            // dataGridViewNumerical
            // 
            dataGridViewNumerical.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewNumerical.Location = new Point(12, 589);
            dataGridViewNumerical.Name = "dataGridViewNumerical";
            dataGridViewNumerical.RowTemplate.Height = 25;
            dataGridViewNumerical.Size = new Size(1041, 250);
            dataGridViewNumerical.TabIndex = 2;
            // 
            // buttonAddParameter
            // 
            buttonAddParameter.Location = new Point(12, 529);
            buttonAddParameter.Name = "buttonAddParameter";
            buttonAddParameter.Size = new Size(103, 36);
            buttonAddParameter.TabIndex = 3;
            buttonAddParameter.Text = "Add parameter";
            buttonAddParameter.UseVisualStyleBackColor = true;
            buttonAddParameter.Click += buttonAddParameter_Click;
            // 
            // buttonEditParameter
            // 
            buttonEditParameter.Location = new Point(121, 529);
            buttonEditParameter.Name = "buttonEditParameter";
            buttonEditParameter.Size = new Size(107, 36);
            buttonEditParameter.TabIndex = 4;
            buttonEditParameter.Text = "Edit parameter";
            buttonEditParameter.UseVisualStyleBackColor = true;
            buttonEditParameter.Click += buttonEditParameter_Click;
            // 
            // buttonDeleteParameter
            // 
            buttonDeleteParameter.Location = new Point(234, 529);
            buttonDeleteParameter.Name = "buttonDeleteParameter";
            buttonDeleteParameter.Size = new Size(121, 36);
            buttonDeleteParameter.TabIndex = 5;
            buttonDeleteParameter.Text = "Delete parameter";
            buttonDeleteParameter.UseVisualStyleBackColor = true;
            buttonDeleteParameter.Click += buttonDeleteParameter_Click;
            // 
            // buttonSetUtilityFunction
            // 
            buttonSetUtilityFunction.Location = new Point(361, 529);
            buttonSetUtilityFunction.Name = "buttonSetUtilityFunction";
            buttonSetUtilityFunction.Size = new Size(75, 36);
            buttonSetUtilityFunction.TabIndex = 6;
            buttonSetUtilityFunction.Text = "Set Utility";
            buttonSetUtilityFunction.UseVisualStyleBackColor = true;
            buttonSetUtilityFunction.Click += buttonSetUtilityFunction_Click;
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(442, 529);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(75, 36);
            buttonCalculate.TabIndex = 7;
            buttonCalculate.Text = "Calculate";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += buttonCalculate_Click;
            // 
            // buttonAddAlternative
            // 
            buttonAddAlternative.Location = new Point(578, 529);
            buttonAddAlternative.Name = "buttonAddAlternative";
            buttonAddAlternative.Size = new Size(106, 36);
            buttonAddAlternative.TabIndex = 8;
            buttonAddAlternative.Text = "Add alternative";
            buttonAddAlternative.UseVisualStyleBackColor = true;
            buttonAddAlternative.Click += buttonAddAlternative_Click;
            // 
            // buttonEditAlternative
            // 
            buttonEditAlternative.Location = new Point(690, 529);
            buttonEditAlternative.Name = "buttonEditAlternative";
            buttonEditAlternative.Size = new Size(112, 36);
            buttonEditAlternative.TabIndex = 9;
            buttonEditAlternative.Text = "Edit alternative";
            buttonEditAlternative.UseVisualStyleBackColor = true;
            buttonEditAlternative.Click += buttonEditAlternative_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(206, 567);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(631, 19);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "This enables the user to see the actual weights calculated based on the utility functions selected for each parameter";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 568);
            label1.Name = "label1";
            label1.Size = new Size(188, 15);
            label1.TabIndex = 11;
            label1.Text = "Enable weigh calculation data grid";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 851);
            Controls.Add(label1);
            Controls.Add(checkBox1);
            Controls.Add(buttonEditAlternative);
            Controls.Add(buttonAddAlternative);
            Controls.Add(buttonCalculate);
            Controls.Add(buttonSetUtilityFunction);
            Controls.Add(buttonDeleteParameter);
            Controls.Add(buttonEditParameter);
            Controls.Add(buttonAddParameter);
            Controls.Add(dataGridViewNumerical);
            Controls.Add(dataGridViewAlternatives);
            Controls.Add(treeViewParameters);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewAlternatives).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNumerical).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeViewParameters;
        private DataGridView dataGridViewAlternatives;
        private DataGridView dataGridViewNumerical;
        private Button buttonAddParameter;
        private Button buttonEditParameter;
        private Button buttonDeleteParameter;
        private Button buttonSetUtilityFunction;
        private Button buttonCalculate;
        private Button buttonAddAlternative;
        private Button buttonEditAlternative;
        private CheckBox checkBox1;
        private Label label1;
    }
}
