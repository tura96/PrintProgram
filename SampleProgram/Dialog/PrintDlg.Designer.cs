namespace SampleProgram
{
    partial class PrintDlg
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
            this.CopiesLabel = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.CopiesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.CopiesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // CopiesLabel
            // 
            this.CopiesLabel.AutoSize = true;
            this.CopiesLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CopiesLabel.Location = new System.Drawing.Point(28, 38);
            this.CopiesLabel.Name = "CopiesLabel";
            this.CopiesLabel.Size = new System.Drawing.Size(59, 16);
            this.CopiesLabel.TabIndex = 0;
            this.CopiesLabel.Text = "Copies";
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PrintButton.Location = new System.Drawing.Point(140, 99);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(110, 30);
            this.PrintButton.TabIndex = 1;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // CopiesNumericUpDown
            // 
            this.CopiesNumericUpDown.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CopiesNumericUpDown.Location = new System.Drawing.Point(165, 35);
            this.CopiesNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.CopiesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CopiesNumericUpDown.Name = "CopiesNumericUpDown";
            this.CopiesNumericUpDown.Size = new System.Drawing.Size(85, 23);
            this.CopiesNumericUpDown.TabIndex = 0;
            this.CopiesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PrintDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 158);
            this.Controls.Add(this.CopiesNumericUpDown);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.CopiesLabel);
            this.Name = "PrintDlg";
            this.Text = "Printing";
            this.Load += new System.EventHandler(this.PrintDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CopiesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CopiesLabel;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.NumericUpDown CopiesNumericUpDown;
    }
}