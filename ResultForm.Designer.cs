namespace MautMetoda
{
    partial class ResultForm
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
            resultDataGridView = new DataGridView();
            logListView = new ListView();
            generateReportButton = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).BeginInit();
            SuspendLayout();
            // 
            // resultDataGridView
            // 
            resultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultDataGridView.Location = new Point(22, 12);
            resultDataGridView.Name = "resultDataGridView";
            resultDataGridView.RowTemplate.Height = 25;
            resultDataGridView.Size = new Size(575, 247);
            resultDataGridView.TabIndex = 0;
            // 
            // logListView
            // 
            logListView.Location = new Point(22, 287);
            logListView.Name = "logListView";
            logListView.Size = new Size(350, 486);
            logListView.TabIndex = 1;
            logListView.UseCompatibleStateImageBehavior = false;
            logListView.View = View.Details;
            // 
            // generateReportButton
            // 
            generateReportButton.Location = new Point(377, 286);
            generateReportButton.Name = "generateReportButton";
            generateReportButton.Size = new Size(220, 40);
            generateReportButton.TabIndex = 2;
            generateReportButton.Text = "Generate Report";
            generateReportButton.UseVisualStyleBackColor = true;
            generateReportButton.Click += generateReportButton_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 269);
            label1.Name = "label1";
            label1.Size = new Size(174, 15);
            label1.TabIndex = 3;
            label1.Text = "Logs of the weight calculations:";
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1191, 809);
            Controls.Add(label1);
            Controls.Add(generateReportButton);
            Controls.Add(logListView);
            Controls.Add(resultDataGridView);
            Name = "ResultForm";
            Text = "ResultForm";
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView resultDataGridView;
        private ListView logListView;
        private Button generateReportButton;
        private Label label1;
    }
}