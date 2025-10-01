namespace SampleProgram
{
    partial class SetRollPaperDlg
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
            this.MediaPositionLabel = new System.Windows.Forms.Label();
            this.MediaPositionComboBox = new System.Windows.Forms.ComboBox();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstructionLabel
            // 
            this.InstructionLabel.AutoSize = true;
            this.InstructionLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InstructionLabel.Location = new System.Drawing.Point(24, 23);
            this.InstructionLabel.Name = "InstructionLabel";
            this.InstructionLabel.Size = new System.Drawing.Size(260, 26);
            this.InstructionLabel.TabIndex = 0;
            this.InstructionLabel.Text = "Change the media position detection setting,\r\nthen set the roll paper.";
            // 
            // MediaPositionLabel
            // 
            this.MediaPositionLabel.AutoSize = true;
            this.MediaPositionLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaPositionLabel.Location = new System.Drawing.Point(25, 86);
            this.MediaPositionLabel.Name = "MediaPositionLabel";
            this.MediaPositionLabel.Size = new System.Drawing.Size(253, 16);
            this.MediaPositionLabel.TabIndex = 1;
            this.MediaPositionLabel.Text = "Media position detection setting";
            // 
            // MediaPositionComboBox
            // 
            this.MediaPositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaPositionComboBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MediaPositionComboBox.FormattingEnabled = true;
            this.MediaPositionComboBox.Location = new System.Drawing.Point(85, 117);
            this.MediaPositionComboBox.Name = "MediaPositionComboBox";
            this.MediaPositionComboBox.Size = new System.Drawing.Size(274, 21);
            this.MediaPositionComboBox.TabIndex = 2;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChangeButton.Location = new System.Drawing.Point(249, 171);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(110, 30);
            this.ChangeButton.TabIndex = 3;
            this.ChangeButton.Text = "Change";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // SetRollPaperDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 230);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.MediaPositionComboBox);
            this.Controls.Add(this.MediaPositionLabel);
            this.Controls.Add(this.InstructionLabel);
            this.Name = "SetRollPaperDlg";
            this.Text = "Set roll paper";
            this.Load += new System.EventHandler(this.SetRollPaperDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InstructionLabel;
        private System.Windows.Forms.Label MediaPositionLabel;
        private System.Windows.Forms.ComboBox MediaPositionComboBox;
        private System.Windows.Forms.Button ChangeButton;
    }
}