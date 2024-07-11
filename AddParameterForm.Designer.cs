namespace MautMetoda
{
    partial class AddParameterForm
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
            nameTextBox = new TextBox();
            weightTextBox = new TextBox();
            basicRadioButton = new RadioButton();
            compositRadioButton = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // confirmation
            // 
            confirmation.Location = new Point(278, 133);
            confirmation.Name = "confirmation";
            confirmation.Size = new Size(75, 23);
            confirmation.TabIndex = 0;
            confirmation.Text = "Ok";
            confirmation.UseVisualStyleBackColor = true;
            confirmation.Click += confirmation_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(131, 27);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(222, 23);
            nameTextBox.TabIndex = 1;
            // 
            // weightTextBox
            // 
            weightTextBox.Location = new Point(131, 94);
            weightTextBox.Name = "weightTextBox";
            weightTextBox.Size = new Size(222, 23);
            weightTextBox.TabIndex = 2;
            // 
            // basicRadioButton
            // 
            basicRadioButton.AutoSize = true;
            basicRadioButton.Location = new Point(131, 60);
            basicRadioButton.Name = "basicRadioButton";
            basicRadioButton.Size = new Size(52, 19);
            basicRadioButton.TabIndex = 3;
            basicRadioButton.TabStop = true;
            basicRadioButton.Text = "Basic";
            basicRadioButton.UseVisualStyleBackColor = true;
            // 
            // compositRadioButton
            // 
            compositRadioButton.AutoSize = true;
            compositRadioButton.Location = new Point(189, 60);
            compositRadioButton.Name = "compositRadioButton";
            compositRadioButton.Size = new Size(83, 19);
            compositRadioButton.TabIndex = 4;
            compositRadioButton.TabStop = true;
            compositRadioButton.Text = "Composite";
            compositRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 30);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 5;
            label1.Text = "Parameter Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 60);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 6;
            label2.Text = "Parameter Type:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 97);
            label3.Name = "label3";
            label3.Size = new Size(105, 15);
            label3.TabIndex = 7;
            label3.Text = "Parameter Weight:";
            // 
            // AddParameterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 180);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(compositRadioButton);
            Controls.Add(basicRadioButton);
            Controls.Add(weightTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(confirmation);
            Name = "AddParameterForm";
            Text = "AddParameterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button confirmation;
        private TextBox nameTextBox;
        private TextBox weightTextBox;
        private RadioButton basicRadioButton;
        private RadioButton compositRadioButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}