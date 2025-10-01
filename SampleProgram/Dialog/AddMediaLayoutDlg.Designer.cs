namespace SampleProgram
{
    partial class AddMediaLayoutDlg
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
            this.AddButton = new System.Windows.Forms.Button();
            this.MediaLayoutNameLabel = new System.Windows.Forms.Label();
            this.MediaWidthLabel = new System.Windows.Forms.Label();
            this.LabelWidthLabel = new System.Windows.Forms.Label();
            this.LabelLengthLabel = new System.Windows.Forms.Label();
            this.LabelsGapLabel = new System.Windows.Forms.Label();
            this.MediaLayoutNameTextBox = new System.Windows.Forms.TextBox();
            this.MediaWidthTextBox = new System.Windows.Forms.TextBox();
            this.LabelWidthTextBox = new System.Windows.Forms.TextBox();
            this.LabelLengthTextBox = new System.Windows.Forms.TextBox();
            this.LabelsGapTextBox = new System.Windows.Forms.TextBox();
            this.MediaWidthUnitLabel = new System.Windows.Forms.Label();
            this.LabelWidthUnitLabel = new System.Windows.Forms.Label();
            this.LabelLengthUnitLabel = new System.Windows.Forms.Label();
            this.LabelsGapUnitLabel = new System.Windows.Forms.Label();
            this.MediaWidthRangeLabel = new System.Windows.Forms.Label();
            this.LabelWidthRangeLabel = new System.Windows.Forms.Label();
            this.LabelLengthRangeLabel = new System.Windows.Forms.Label();
            this.LabelsGapRangeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InstructionLabel.Location = new System.Drawing.Point(26, 19);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(229, 26);
            this.InstructionLabel.TabIndex = 0;
            this.InstructionLabel.Text = "Input the media layout name and media \r\nsize, then add the media layout.";
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AddButton.Location = new System.Drawing.Point(338, 311);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(110, 30);
            this.AddButton.TabIndex = 18;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MediaLayoutNameLabel
            // 
            this.MediaLayoutNameLabel.AutoSize = true;
            this.MediaLayoutNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaLayoutNameLabel.Location = new System.Drawing.Point(26, 80);
            this.MediaLayoutNameLabel.Name = "MediaLayoutNameLabel";
            this.MediaLayoutNameLabel.Size = new System.Drawing.Size(151, 16);
            this.MediaLayoutNameLabel.TabIndex = 1;
            this.MediaLayoutNameLabel.Text = "Media layout name";
            // 
            // MediaWidthLabel
            // 
            this.MediaWidthLabel.AutoSize = true;
            this.MediaWidthLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaWidthLabel.Location = new System.Drawing.Point(25, 130);
            this.MediaWidthLabel.Name = "MediaWidthLabel";
            this.MediaWidthLabel.Size = new System.Drawing.Size(98, 16);
            this.MediaWidthLabel.TabIndex = 2;
            this.MediaWidthLabel.Text = "Media width";
            // 
            // LabelWidthLabel
            // 
            this.LabelWidthLabel.AutoSize = true;
            this.LabelWidthLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelWidthLabel.Location = new System.Drawing.Point(25, 180);
            this.LabelWidthLabel.Name = "LabelWidthLabel";
            this.LabelWidthLabel.Size = new System.Drawing.Size(95, 16);
            this.LabelWidthLabel.TabIndex = 3;
            this.LabelWidthLabel.Text = "Label width";
            // 
            // LabelLengthLabel
            // 
            this.LabelLengthLabel.AutoSize = true;
            this.LabelLengthLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelLengthLabel.Location = new System.Drawing.Point(26, 227);
            this.LabelLengthLabel.Name = "LabelLengthLabel";
            this.LabelLengthLabel.Size = new System.Drawing.Size(101, 16);
            this.LabelLengthLabel.TabIndex = 4;
            this.LabelLengthLabel.Text = "Label length";
            // 
            // LabelsGapLabel
            // 
            this.LabelsGapLabel.AutoSize = true;
            this.LabelsGapLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelsGapLabel.Location = new System.Drawing.Point(25, 274);
            this.LabelsGapLabel.Name = "LabelsGapLabel";
            this.LabelsGapLabel.Size = new System.Drawing.Size(89, 16);
            this.LabelsGapLabel.TabIndex = 5;
            this.LabelsGapLabel.Text = "Labels gap";
            // 
            // MediaLayoutNameTextBox
            // 
            this.MediaLayoutNameTextBox.Location = new System.Drawing.Point(186, 77);
            this.MediaLayoutNameTextBox.Name = "MediaLayoutNameTextBox";
            this.MediaLayoutNameTextBox.Size = new System.Drawing.Size(171, 19);
            this.MediaLayoutNameTextBox.TabIndex = 6;
            // 
            // MediaWidthTextBox
            // 
            this.MediaWidthTextBox.Location = new System.Drawing.Point(177, 129);
            this.MediaWidthTextBox.MaxLength = 2000;
            this.MediaWidthTextBox.Name = "MediaWidthTextBox";
            this.MediaWidthTextBox.Size = new System.Drawing.Size(86, 19);
            this.MediaWidthTextBox.TabIndex = 7;
            // 
            // LabelWidthTextBox
            // 
            this.LabelWidthTextBox.Location = new System.Drawing.Point(177, 177);
            this.LabelWidthTextBox.Name = "LabelWidthTextBox";
            this.LabelWidthTextBox.Size = new System.Drawing.Size(86, 19);
            this.LabelWidthTextBox.TabIndex = 8;
            // 
            // LabelLengthTextBox
            // 
            this.LabelLengthTextBox.Location = new System.Drawing.Point(177, 224);
            this.LabelLengthTextBox.Name = "LabelLengthTextBox";
            this.LabelLengthTextBox.Size = new System.Drawing.Size(86, 19);
            this.LabelLengthTextBox.TabIndex = 9;
            // 
            // LabelsGapTextBox
            // 
            this.LabelsGapTextBox.Location = new System.Drawing.Point(177, 271);
            this.LabelsGapTextBox.Name = "LabelsGapTextBox";
            this.LabelsGapTextBox.Size = new System.Drawing.Size(86, 19);
            this.LabelsGapTextBox.TabIndex = 10;
            // 
            // MediaWidthUnitLabel
            // 
            this.MediaWidthUnitLabel.AutoSize = true;
            this.MediaWidthUnitLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaWidthUnitLabel.Location = new System.Drawing.Point(281, 132);
            this.MediaWidthUnitLabel.Name = "MediaWidthUnitLabel";
            this.MediaWidthUnitLabel.Size = new System.Drawing.Size(34, 16);
            this.MediaWidthUnitLabel.TabIndex = 11;
            this.MediaWidthUnitLabel.Text = "mm";
            // 
            // LabelWidthUnitLabel
            // 
            this.LabelWidthUnitLabel.AutoSize = true;
            this.LabelWidthUnitLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelWidthUnitLabel.Location = new System.Drawing.Point(281, 176);
            this.LabelWidthUnitLabel.Name = "LabelWidthUnitLabel";
            this.LabelWidthUnitLabel.Size = new System.Drawing.Size(34, 16);
            this.LabelWidthUnitLabel.TabIndex = 13;
            this.LabelWidthUnitLabel.Text = "mm";
            // 
            // LabelLengthUnitLabel
            // 
            this.LabelLengthUnitLabel.AutoSize = true;
            this.LabelLengthUnitLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelLengthUnitLabel.Location = new System.Drawing.Point(281, 227);
            this.LabelLengthUnitLabel.Name = "LabelLengthUnitLabel";
            this.LabelLengthUnitLabel.Size = new System.Drawing.Size(34, 16);
            this.LabelLengthUnitLabel.TabIndex = 15;
            this.LabelLengthUnitLabel.Text = "mm";
            // 
            // LabelsGapUnitLabel
            // 
            this.LabelsGapUnitLabel.AutoSize = true;
            this.LabelsGapUnitLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelsGapUnitLabel.Location = new System.Drawing.Point(281, 270);
            this.LabelsGapUnitLabel.Name = "LabelsGapUnitLabel";
            this.LabelsGapUnitLabel.Size = new System.Drawing.Size(34, 16);
            this.LabelsGapUnitLabel.TabIndex = 17;
            this.LabelsGapUnitLabel.Text = "mm";
            // 
            // MediaWidthRangeLabel
            // 
            this.MediaWidthRangeLabel.AutoSize = true;
            this.MediaWidthRangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaWidthRangeLabel.Location = new System.Drawing.Point(335, 130);
            this.MediaWidthRangeLabel.Name = "MediaWidthRangeLabel";
            this.MediaWidthRangeLabel.Size = new System.Drawing.Size(20, 16);
            this.MediaWidthRangeLabel.TabIndex = 12;
            this.MediaWidthRangeLabel.Text = "...";
            // 
            // LabelWidthRangeLabel
            // 
            this.LabelWidthRangeLabel.AutoSize = true;
            this.LabelWidthRangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelWidthRangeLabel.Location = new System.Drawing.Point(335, 180);
            this.LabelWidthRangeLabel.Name = "LabelWidthRangeLabel";
            this.LabelWidthRangeLabel.Size = new System.Drawing.Size(20, 16);
            this.LabelWidthRangeLabel.TabIndex = 14;
            this.LabelWidthRangeLabel.Text = "...";
            // 
            // LabelLengthRangeLabel
            // 
            this.LabelLengthRangeLabel.AutoSize = true;
            this.LabelLengthRangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelLengthRangeLabel.Location = new System.Drawing.Point(335, 227);
            this.LabelLengthRangeLabel.Name = "LabelLengthRangeLabel";
            this.LabelLengthRangeLabel.Size = new System.Drawing.Size(20, 16);
            this.LabelLengthRangeLabel.TabIndex = 16;
            this.LabelLengthRangeLabel.Text = "...";
            // 
            // LabelsGapRangeLabel
            // 
            this.LabelsGapRangeLabel.AutoSize = true;
            this.LabelsGapRangeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelsGapRangeLabel.Location = new System.Drawing.Point(335, 274);
            this.LabelsGapRangeLabel.Name = "LabelsGapRangeLabel";
            this.LabelsGapRangeLabel.Size = new System.Drawing.Size(20, 16);
            this.LabelsGapRangeLabel.TabIndex = 10;
            this.LabelsGapRangeLabel.Text = "...";
            // 
            // AddMediaLayoutDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 367);
            this.Controls.Add(this.LabelsGapRangeLabel);
            this.Controls.Add(this.LabelLengthRangeLabel);
            this.Controls.Add(this.LabelWidthRangeLabel);
            this.Controls.Add(this.MediaWidthRangeLabel);
            this.Controls.Add(this.LabelsGapUnitLabel);
            this.Controls.Add(this.LabelLengthUnitLabel);
            this.Controls.Add(this.LabelWidthUnitLabel);
            this.Controls.Add(this.MediaWidthUnitLabel);
            this.Controls.Add(this.LabelsGapTextBox);
            this.Controls.Add(this.LabelLengthTextBox);
            this.Controls.Add(this.LabelWidthTextBox);
            this.Controls.Add(this.MediaWidthTextBox);
            this.Controls.Add(this.MediaLayoutNameTextBox);
            this.Controls.Add(this.LabelsGapLabel);
            this.Controls.Add(this.LabelLengthLabel);
            this.Controls.Add(this.LabelWidthLabel);
            this.Controls.Add(this.MediaWidthLabel);
            this.Controls.Add(this.MediaLayoutNameLabel);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.InstructionLabel);
            this.Name = "AddMediaLayoutDlg";
            this.Text = "Add media layout";
            this.Load += new System.EventHandler(this.AddMediaLayoutDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InstructionLabel;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label MediaLayoutNameLabel;
        private System.Windows.Forms.Label MediaWidthLabel;
        private System.Windows.Forms.Label LabelWidthLabel;
        private System.Windows.Forms.Label LabelLengthLabel;
        private System.Windows.Forms.Label LabelsGapLabel;
        private System.Windows.Forms.TextBox MediaLayoutNameTextBox;
        private System.Windows.Forms.TextBox MediaWidthTextBox;
        private System.Windows.Forms.TextBox LabelWidthTextBox;
        private System.Windows.Forms.TextBox LabelLengthTextBox;
        private System.Windows.Forms.TextBox LabelsGapTextBox;
        private System.Windows.Forms.Label MediaWidthUnitLabel;
        private System.Windows.Forms.Label LabelWidthUnitLabel;
        private System.Windows.Forms.Label LabelLengthUnitLabel;
        private System.Windows.Forms.Label LabelsGapUnitLabel;
        private System.Windows.Forms.Label MediaWidthRangeLabel;
        private System.Windows.Forms.Label LabelWidthRangeLabel;
        private System.Windows.Forms.Label LabelLengthRangeLabel;
        private System.Windows.Forms.Label LabelsGapRangeLabel;
    }
}