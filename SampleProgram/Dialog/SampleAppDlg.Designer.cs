namespace SampleProgram
{
    partial class SampleAppDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources begin used.
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
            this.Step2Button = new System.Windows.Forms.Button();
            this.Step1Label = new System.Windows.Forms.Label();
            this.PrinterInfoComboBox = new System.Windows.Forms.ComboBox();
            this.Step3Button = new System.Windows.Forms.Button();
            this.Step4Button = new System.Windows.Forms.Button();
            this.Step5Button = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RequestsListBox = new System.Windows.Forms.ListBox();
            this.LabelPreviewPanel = new System.Windows.Forms.Panel();
            this.PrintSourceComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Step2Button
            // 
            this.Step2Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step2Button.Location = new System.Drawing.Point(33, 118);
            this.Step2Button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Step2Button.Name = "Step2Button";
            this.Step2Button.Size = new System.Drawing.Size(600, 46);
            this.Step2Button.TabIndex = 0;
            this.Step2Button.Text = "Step.2 Add the media layout";
            this.Step2Button.UseVisualStyleBackColor = true;
            this.Step2Button.Click += new System.EventHandler(this.Step2Button_Click);
            // 
            // Step1Label
            // 
            this.Step1Label.AutoSize = true;
            this.Step1Label.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step1Label.Location = new System.Drawing.Point(28, 15);
            this.Step1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Step1Label.Name = "Step1Label";
            this.Step1Label.Size = new System.Drawing.Size(200, 16);
            this.Step1Label.TabIndex = 1;
            this.Step1Label.Text = "Step.1 Select the printer";
            // 
            // PrinterInfoComboBox
            // 
            this.PrinterInfoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrinterInfoComboBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrinterInfoComboBox.FormattingEnabled = true;
            this.PrinterInfoComboBox.Location = new System.Drawing.Point(386, 46);
            this.PrinterInfoComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PrinterInfoComboBox.Name = "PrinterInfoComboBox";
            this.PrinterInfoComboBox.Size = new System.Drawing.Size(246, 21);
            this.PrinterInfoComboBox.TabIndex = 2;
            this.PrinterInfoComboBox.SelectedIndexChanged += new System.EventHandler(this.PrinterInfoComboBox_SelectedIndexChanged);
            // 
            // Step3Button
            // 
            this.Step3Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step3Button.Location = new System.Drawing.Point(33, 195);
            this.Step3Button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Step3Button.Name = "Step3Button";
            this.Step3Button.Size = new System.Drawing.Size(600, 46);
            this.Step3Button.TabIndex = 3;
            this.Step3Button.Text = "Step.3 Set the roll paper";
            this.Step3Button.UseVisualStyleBackColor = true;
            this.Step3Button.Click += new System.EventHandler(this.Step3Button_Click);
            // 
            // Step4Button
            // 
            this.Step4Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step4Button.Location = new System.Drawing.Point(33, 272);
            this.Step4Button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Step4Button.Name = "Step4Button";
            this.Step4Button.Size = new System.Drawing.Size(600, 46);
            this.Step4Button.TabIndex = 4;
            this.Step4Button.Text = "Step.4 Change the print settings";
            this.Step4Button.UseVisualStyleBackColor = true;
            this.Step4Button.Click += new System.EventHandler(this.Step4Button_Click);
            // 
            // Step5Button
            // 
            this.Step5Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step5Button.Location = new System.Drawing.Point(33, 351);
            this.Step5Button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Step5Button.Name = "Step5Button";
            this.Step5Button.Size = new System.Drawing.Size(600, 46);
            this.Step5Button.TabIndex = 5;
            this.Step5Button.Text = "Step.5 Printing...";
            this.Step5Button.UseVisualStyleBackColor = true;
            this.Step5Button.Click += new System.EventHandler(this.Step5Button_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExitButton.Location = new System.Drawing.Point(464, 425);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(168, 52);
            this.ExitButton.TabIndex = 6;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RequestsListBox
            // 
            this.RequestsListBox.FormattingEnabled = true;
            this.RequestsListBox.ItemHeight = 20;
            this.RequestsListBox.Location = new System.Drawing.Point(28, 500);
            this.RequestsListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RequestsListBox.Name = "RequestsListBox";
            this.RequestsListBox.Size = new System.Drawing.Size(298, 164);
            this.RequestsListBox.TabIndex = 7;
            this.RequestsListBox.SelectedIndexChanged += new System.EventHandler(this.RequestsListBox_SelectedIndexChanged);
            // 
            // LabelPreviewPanel
            // 
            this.LabelPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelPreviewPanel.Location = new System.Drawing.Point(360, 500);
            this.LabelPreviewPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LabelPreviewPanel.Name = "LabelPreviewPanel";
            this.LabelPreviewPanel.Size = new System.Drawing.Size(272, 165);
            this.LabelPreviewPanel.TabIndex = 8;
            // 
            // PrintSourceComboBox
            // 
            this.PrintSourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintSourceComboBox.FormattingEnabled = true;
            this.PrintSourceComboBox.Items.AddRange(new object[] {
            "Sample Image",
            "HTTP Request"});
            this.PrintSourceComboBox.Location = new System.Drawing.Point(28, 438);
            this.PrintSourceComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PrintSourceComboBox.Name = "PrintSourceComboBox";
            this.PrintSourceComboBox.Size = new System.Drawing.Size(298, 28);
            this.PrintSourceComboBox.TabIndex = 9;
            // 
            // SampleAppDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(670, 694);
            this.Controls.Add(this.PrintSourceComboBox);
            this.Controls.Add(this.LabelPreviewPanel);
            this.Controls.Add(this.RequestsListBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.Step5Button);
            this.Controls.Add(this.Step4Button);
            this.Controls.Add(this.Step3Button);
            this.Controls.Add(this.PrinterInfoComboBox);
            this.Controls.Add(this.Step1Label);
            this.Controls.Add(this.Step2Button);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SampleAppDlg";
            this.Text = "Label printer sample program";
            this.Load += new System.EventHandler(this.SampleAppDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Step2Button;
        private System.Windows.Forms.Label Step1Label;
        private System.Windows.Forms.ComboBox PrinterInfoComboBox;
        private System.Windows.Forms.Button Step3Button;
        private System.Windows.Forms.Button Step4Button;
        private System.Windows.Forms.Button Step5Button;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ListBox RequestsListBox;
        private System.Windows.Forms.Panel LabelPreviewPanel;
        private System.Windows.Forms.ComboBox PrintSourceComboBox;
    }
}

