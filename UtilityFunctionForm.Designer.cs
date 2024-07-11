namespace MautMetoda
{
    partial class UtilityFunctionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            confirmation = new Button();
            functionComboBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            minValueTextBox = new TextBox();
            maxValueTextBox = new TextBox();
            SuspendLayout();
            // 
            // confirmation
            // 
            confirmation.Location = new Point(236, 134);
            confirmation.Name = "confirmation";
            confirmation.Size = new Size(75, 23);
            confirmation.TabIndex = 0;
            confirmation.Text = "Ok";
            confirmation.UseVisualStyleBackColor = true;
            confirmation.Click += confirmation_Click;
            // 
            // functionComboBox
            // 
            functionComboBox.FormattingEnabled = true;
            functionComboBox.Location = new Point(113, 24);
            functionComboBox.Name = "functionComboBox";
            functionComboBox.Size = new Size(198, 23);
            functionComboBox.TabIndex = 1;
            functionComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 27);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 2;
            label1.Text = "Function Type:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 68);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 3;
            label2.Text = "Min Value:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 108);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 4;
            label3.Text = "Max Value:";
            // 
            // minValueTextBox
            // 
            minValueTextBox.Location = new Point(113, 65);
            minValueTextBox.Name = "minValueTextBox";
            minValueTextBox.Size = new Size(198, 23);
            minValueTextBox.TabIndex = 5;
            // 
            // maxValueTextBox
            // 
            maxValueTextBox.Location = new Point(113, 105);
            maxValueTextBox.Name = "maxValueTextBox";
            maxValueTextBox.Size = new Size(198, 23);
            maxValueTextBox.TabIndex = 6;
            // 
            // UtilityFunctionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 178);
            Controls.Add(maxValueTextBox);
            Controls.Add(minValueTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(functionComboBox);
            Controls.Add(confirmation);
            Name = "UtilityFunctionForm";
            Text = "UtilityFunctionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button confirmation;
        private ComboBox functionComboBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox minValueTextBox;
        private TextBox maxValueTextBox;
    }
}