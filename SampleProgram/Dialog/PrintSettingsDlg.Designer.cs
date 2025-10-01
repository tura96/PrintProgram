namespace SampleProgram
{
    partial class PrintSettingsDlg
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
            this.InstructionLabel = new System.Windows.Forms.Label();
            this.MadiaLayoutLabel = new System.Windows.Forms.Label();
            this.MediaTypeLabel = new System.Windows.Forms.Label();
            this.PrintQualityLabel = new System.Windows.Forms.Label();
            this.MediaLayoutComboBox = new System.Windows.Forms.ComboBox();
            this.PrintQualityComboBox = new System.Windows.Forms.ComboBox();
            this.MediaTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InstructionLabel.Location = new System.Drawing.Point(38, 22);
            this.InstructionLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(322, 65);
            this.InstructionLabel.TabIndex = 0;
            this.InstructionLabel.Text = "Select the print settings.\r\n\r\nWhen you select the \'Media type\', \'Print quality\' m" +
    "ay be \r\nchanged automatically.\r\n\r\n";
            // 
            // MadiaLayoutLabel
            // 
            this.MadiaLayoutLabel.AutoSize = true;
            this.MadiaLayoutLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MadiaLayoutLabel.Location = new System.Drawing.Point(38, 119);
            this.MadiaLayoutLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.MadiaLayoutLabel.Name = "MadiaLayoutLabel";
            this.MadiaLayoutLabel.Size = new System.Drawing.Size(105, 16);
            this.MadiaLayoutLabel.TabIndex = 1;
            this.MadiaLayoutLabel.Text = "Media layout";
            // 
            // MediaTypeLabel
            // 
            this.MediaTypeLabel.AutoSize = true;
            this.MediaTypeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaTypeLabel.Location = new System.Drawing.Point(38, 164);
            this.MediaTypeLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.MediaTypeLabel.Name = "MediaTypeLabel";
            this.MediaTypeLabel.Size = new System.Drawing.Size(92, 16);
            this.MediaTypeLabel.TabIndex = 1;
            this.MediaTypeLabel.Text = "Media type";
            // 
            // PrintQualityLabel
            // 
            this.PrintQualityLabel.AutoSize = true;
            this.PrintQualityLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrintQualityLabel.Location = new System.Drawing.Point(38, 208);
            this.PrintQualityLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.PrintQualityLabel.Name = "PrintQualityLabel";
            this.PrintQualityLabel.Size = new System.Drawing.Size(103, 16);
            this.PrintQualityLabel.TabIndex = 1;
            this.PrintQualityLabel.Text = "Print quality";
            // 
            // MediaLayoutComboBox
            // 
            this.MediaLayoutComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaLayoutComboBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaLayoutComboBox.FormattingEnabled = true;
            this.MediaLayoutComboBox.Location = new System.Drawing.Point(171, 111);
            this.MediaLayoutComboBox.Name = "MediaLayoutComboBox";
            this.MediaLayoutComboBox.Size = new System.Drawing.Size(243, 21);
            this.MediaLayoutComboBox.TabIndex = 0;
            // 
            // PrintQualityComboBox
            // 
            this.PrintQualityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintQualityComboBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrintQualityComboBox.FormattingEnabled = true;
            this.PrintQualityComboBox.Location = new System.Drawing.Point(171, 200);
            this.PrintQualityComboBox.Name = "PrintQualityComboBox";
            this.PrintQualityComboBox.Size = new System.Drawing.Size(243, 21);
            this.PrintQualityComboBox.TabIndex = 2;
            // 
            // MediaTypeComboBox
            // 
            this.MediaTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaTypeComboBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaTypeComboBox.FormattingEnabled = true;
            this.MediaTypeComboBox.Location = new System.Drawing.Point(171, 156);
            this.MediaTypeComboBox.Name = "MediaTypeComboBox";
            this.MediaTypeComboBox.Size = new System.Drawing.Size(243, 21);
            this.MediaTypeComboBox.TabIndex = 1;
            this.MediaTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.MediaTypeComboBox_SelectedIndexChanged);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(303, 250);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(110, 30);
            this.ApplyButton.TabIndex = 3;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // PrintSettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 314);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.MediaTypeComboBox);
            this.Controls.Add(this.PrintQualityComboBox);
            this.Controls.Add(this.MediaLayoutComboBox);
            this.Controls.Add(this.PrintQualityLabel);
            this.Controls.Add(this.MediaTypeLabel);
            this.Controls.Add(this.MadiaLayoutLabel);
            this.Controls.Add(this.InstructionLabel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "PrintSettingsDlg";
            this.Text = "Print settings";
            this.Load += new System.EventHandler(this.PrintSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InstructionLabel;
        private System.Windows.Forms.Label MadiaLayoutLabel;
        private System.Windows.Forms.Label MediaTypeLabel;
        private System.Windows.Forms.Label PrintQualityLabel;
        private System.Windows.Forms.ComboBox MediaLayoutComboBox;
        private System.Windows.Forms.ComboBox PrintQualityComboBox;
        private System.Windows.Forms.ComboBox MediaTypeComboBox;
        private System.Windows.Forms.Button ApplyButton;
    }
}