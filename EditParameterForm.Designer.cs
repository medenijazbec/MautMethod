namespace MautMetoda
{
    partial class EditParameterForm
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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            compositRadioButton = new RadioButton();
            basicRadioButton = new RadioButton();
            weightTextBox = new TextBox();
            nameTextBox = new TextBox();
            confirmation = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 85);
            label3.Name = "label3";
            label3.Size = new Size(105, 15);
            label3.TabIndex = 15;
            label3.Text = "Parameter Weight:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 48);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 14;
            label2.Text = "Parameter Type:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 18);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 13;
            label1.Text = "Parameter Name:";
            // 
            // compositRadioButton
            // 
            compositRadioButton.AutoSize = true;
            compositRadioButton.Location = new Point(183, 48);
            compositRadioButton.Name = "compositRadioButton";
            compositRadioButton.Size = new Size(83, 19);
            compositRadioButton.TabIndex = 12;
            compositRadioButton.TabStop = true;
            compositRadioButton.Text = "Composite";
            compositRadioButton.UseVisualStyleBackColor = true;
            // 
            // basicRadioButton
            // 
            basicRadioButton.AutoSize = true;
            basicRadioButton.Location = new Point(125, 48);
            basicRadioButton.Name = "basicRadioButton";
            basicRadioButton.Size = new Size(52, 19);
            basicRadioButton.TabIndex = 11;
            basicRadioButton.TabStop = true;
            basicRadioButton.Text = "Basic";
            basicRadioButton.UseVisualStyleBackColor = true;
            // 
            // weightTextBox
            // 
            weightTextBox.Location = new Point(125, 82);
            weightTextBox.Name = "weightTextBox";
            weightTextBox.Size = new Size(222, 23);
            weightTextBox.TabIndex = 10;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(125, 15);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(222, 23);
            nameTextBox.TabIndex = 9;
            // 
            // confirmation
            // 
            confirmation.Location = new Point(272, 121);
            confirmation.Name = "confirmation";
            confirmation.Size = new Size(75, 23);
            confirmation.TabIndex = 8;
            confirmation.Text = "Ok";
            confirmation.UseVisualStyleBackColor = true;
            confirmation.Click += confirmation_Click;
            // 
            // EditParameterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 156);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(compositRadioButton);
            Controls.Add(basicRadioButton);
            Controls.Add(weightTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(confirmation);
            Name = "EditParameterForm";
            Text = "EditParameterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private RadioButton compositRadioButton;
        private RadioButton basicRadioButton;
        private TextBox weightTextBox;
        private TextBox nameTextBox;
        private Button confirmation;
    }
}