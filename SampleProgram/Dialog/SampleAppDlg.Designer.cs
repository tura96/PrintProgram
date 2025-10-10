namespace SampleProgram
{
    partial class SampleAppDlg
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
            this.Step2Button = new System.Windows.Forms.Button();
            this.Step1Label = new System.Windows.Forms.Label();
            this.PrinterInfoComboBox = new System.Windows.Forms.ComboBox();
            this.Step3Button = new System.Windows.Forms.Button();
            this.Step4Button = new System.Windows.Forms.Button();
            this.Step5Button = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RequestsListBox = new System.Windows.Forms.ListBox();
            this.LabelPreviewPanel = new System.Windows.Forms.Panel();
            this._printSourceComboBox = new System.Windows.Forms.ComboBox();
            this._urlTextBox = new System.Windows.Forms.TextBox();
            this._fetchJsonButton = new System.Windows.Forms.Button();
            this._clearRequestsButton = new System.Windows.Forms.Button();
            this.AutoPrintCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Step2Button
            // 
            this.Step2Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step2Button.Location = new System.Drawing.Point(22, 44);
            this.Step2Button.Name = "Step2Button";
            this.Step2Button.Size = new System.Drawing.Size(400, 30);
            this.Step2Button.TabIndex = 0;
            this.Step2Button.Text = "Step.2 Add the media layout";
            this.Step2Button.UseVisualStyleBackColor = true;
            this.Step2Button.Click += new System.EventHandler(this.Step2Button_Click);
            // 
            // Step1Label
            // 
            this.Step1Label.AutoSize = true;
            this.Step1Label.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step1Label.Location = new System.Drawing.Point(22, 17);
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
            this.PrinterInfoComboBox.Location = new System.Drawing.Point(237, 17);
            this.PrinterInfoComboBox.Name = "PrinterInfoComboBox";
            this.PrinterInfoComboBox.Size = new System.Drawing.Size(185, 21);
            this.PrinterInfoComboBox.TabIndex = 2;
            this.PrinterInfoComboBox.SelectedIndexChanged += new System.EventHandler(this.PrinterInfoComboBox_SelectedIndexChanged);
            // 
            // Step3Button
            // 
            this.Step3Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step3Button.Location = new System.Drawing.Point(22, 80);
            this.Step3Button.Name = "Step3Button";
            this.Step3Button.Size = new System.Drawing.Size(400, 30);
            this.Step3Button.TabIndex = 3;
            this.Step3Button.Text = "Step.3 Set the roll paper";
            this.Step3Button.UseVisualStyleBackColor = true;
            this.Step3Button.Click += new System.EventHandler(this.Step3Button_Click);
            // 
            // Step4Button
            // 
            this.Step4Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step4Button.Location = new System.Drawing.Point(22, 116);
            this.Step4Button.Name = "Step4Button";
            this.Step4Button.Size = new System.Drawing.Size(400, 30);
            this.Step4Button.TabIndex = 4;
            this.Step4Button.Text = "Step.4 Change the print settings";
            this.Step4Button.UseVisualStyleBackColor = true;
            this.Step4Button.Click += new System.EventHandler(this.Step4Button_Click);
            // 
            // Step5Button
            // 
            this.Step5Button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Step5Button.Location = new System.Drawing.Point(22, 152);
            this.Step5Button.Name = "Step5Button";
            this.Step5Button.Size = new System.Drawing.Size(400, 30);
            this.Step5Button.TabIndex = 5;
            this.Step5Button.Text = "Step.5 Printing...";
            this.Step5Button.UseVisualStyleBackColor = true;
            this.Step5Button.Click += new System.EventHandler(this.Step5Button_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExitButton.Location = new System.Drawing.Point(310, 478);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(112, 34);
            this.ExitButton.TabIndex = 6;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RequestsListBox
            // 
            this.RequestsListBox.FormattingEnabled = true;
            this.RequestsListBox.Location = new System.Drawing.Point(22, 188);
            this.RequestsListBox.Name = "RequestsListBox";
            this.RequestsListBox.Size = new System.Drawing.Size(400, 43);
            this.RequestsListBox.TabIndex = 7;
            this.RequestsListBox.SelectedIndexChanged += new System.EventHandler(this.RequestsListBox_SelectedIndexChanged);
            // 
            // LabelPreviewPanel
            // 
            this.LabelPreviewPanel.Location = new System.Drawing.Point(22, 250);
            this.LabelPreviewPanel.Name = "LabelPreviewPanel";
            this.LabelPreviewPanel.Size = new System.Drawing.Size(400, 136);
            this.LabelPreviewPanel.TabIndex = 100;
            this.LabelPreviewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelPreviewPanel_Paint);
            // 
            // _printSourceComboBox
            // 
            this._printSourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._printSourceComboBox.FormattingEnabled = true;
            this._printSourceComboBox.Items.AddRange(new object[] {
            "Sample Image",
            "HTTP Request",
            "JSON Data"});
            this._printSourceComboBox.Location = new System.Drawing.Point(22, 400);
            this._printSourceComboBox.Name = "_printSourceComboBox";
            this._printSourceComboBox.Size = new System.Drawing.Size(400, 21);
            this._printSourceComboBox.TabIndex = 9;
            this._printSourceComboBox.SelectedIndexChanged += new System.EventHandler(this.PrintSourceComboBox_SelectedIndexChanged);
            // 
            // _urlTextBox
            // 
            this._urlTextBox.ForeColor = System.Drawing.Color.Gray;
            this._urlTextBox.Location = new System.Drawing.Point(22, 427);
            this._urlTextBox.Name = "_urlTextBox";
            this._urlTextBox.Size = new System.Drawing.Size(400, 20);
            this._urlTextBox.TabIndex = 10;
            this._urlTextBox.Text = "Enter JSON URL";
            // 
            // _fetchJsonButton
            // 
            this._fetchJsonButton.Location = new System.Drawing.Point(22, 453);
            this._fetchJsonButton.Name = "_fetchJsonButton";
            this._fetchJsonButton.Size = new System.Drawing.Size(80, 23);
            this._fetchJsonButton.TabIndex = 11;
            this._fetchJsonButton.Text = "Fetch JSON";
            this._fetchJsonButton.UseVisualStyleBackColor = true;
            this._fetchJsonButton.Click += new System.EventHandler(this.FetchJsonButton_Click);
            // 
            // _clearRequestsButton
            // 
            this._clearRequestsButton.Location = new System.Drawing.Point(110, 453);
            this._clearRequestsButton.Name = "_clearRequestsButton";
            this._clearRequestsButton.Size = new System.Drawing.Size(80, 23);
            this._clearRequestsButton.TabIndex = 12;
            this._clearRequestsButton.Text = "Clear List";
            this._clearRequestsButton.UseVisualStyleBackColor = true;
            this._clearRequestsButton.Click += new System.EventHandler(this.ClearRequestsButton_Click);
            // 
            // AutoPrintCheckBox
            // 
            this.AutoPrintCheckBox.AutoSize = true;
            this.AutoPrintCheckBox.Checked = true;
            this.AutoPrintCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoPrintCheckBox.Location = new System.Drawing.Point(22, 489);
            this.AutoPrintCheckBox.Name = "AutoPrintCheckBox";
            this.AutoPrintCheckBox.Size = new System.Drawing.Size(162, 17);
            this.AutoPrintCheckBox.TabIndex = 99;
            this.AutoPrintCheckBox.Text = "Auto Print on HTTP Request";
            this.AutoPrintCheckBox.UseVisualStyleBackColor = true;
            this.AutoPrintCheckBox.CheckedChanged += new System.EventHandler(this.AutoPrintCheckBox_CheckedChanged);
            // 
            // SampleAppDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(453, 524);
            this.Controls.Add(this.AutoPrintCheckBox);
            this.Controls.Add(this._clearRequestsButton);
            this.Controls.Add(this._fetchJsonButton);
            this.Controls.Add(this._urlTextBox);
            this.Controls.Add(this._printSourceComboBox);
            this.Controls.Add(this.LabelPreviewPanel);
            this.Controls.Add(this.RequestsListBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.Step5Button);
            this.Controls.Add(this.Step4Button);
            this.Controls.Add(this.Step3Button);
            this.Controls.Add(this.PrinterInfoComboBox);
            this.Controls.Add(this.Step1Label);
            this.Controls.Add(this.Step2Button);
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
        private System.Windows.Forms.ListBox RequestsListBox;
        private System.Windows.Forms.Panel LabelPreviewPanel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox _printSourceComboBox;
        private System.Windows.Forms.TextBox _urlTextBox;
        private System.Windows.Forms.Button _fetchJsonButton;
        private System.Windows.Forms.Button _clearRequestsButton;
        private System.Windows.Forms.CheckBox AutoPrintCheckBox;
    }
}