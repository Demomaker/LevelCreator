namespace LevelCreating
{
    partial class SaveForm
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
            this.savebutton = new System.Windows.Forms.Button();
            this.seperator = new System.Windows.Forms.TextBox();
            this.lineSeperator = new System.Windows.Forms.TextBox();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.lineSeperatorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // savebutton
            // 
            this.savebutton.Location = new System.Drawing.Point(100, 58);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(75, 23);
            this.savebutton.TabIndex = 0;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // seperator
            // 
            this.seperator.Location = new System.Drawing.Point(100, 6);
            this.seperator.Name = "seperator";
            this.seperator.Size = new System.Drawing.Size(172, 20);
            this.seperator.TabIndex = 2;
            this.seperator.Text = ",";
            // 
            // lineSeperator
            // 
            this.lineSeperator.Location = new System.Drawing.Point(106, 32);
            this.lineSeperator.Name = "lineSeperator";
            this.lineSeperator.Size = new System.Drawing.Size(166, 20);
            this.lineSeperator.TabIndex = 3;
            this.lineSeperator.Text = ";";
            // 
            // seperatorLabel
            // 
            this.seperatorLabel.AutoSize = true;
            this.seperatorLabel.Location = new System.Drawing.Point(12, 9);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(79, 13);
            this.seperatorLabel.TabIndex = 4;
            this.seperatorLabel.Text = "Seperator (txt) :";
            // 
            // lineSeperatorLabel
            // 
            this.lineSeperatorLabel.AutoSize = true;
            this.lineSeperatorLabel.Location = new System.Drawing.Point(12, 32);
            this.lineSeperatorLabel.Name = "lineSeperatorLabel";
            this.lineSeperatorLabel.Size = new System.Drawing.Size(85, 13);
            this.lineSeperatorLabel.TabIndex = 5;
            this.lineSeperatorLabel.Text = "Line Seperator : ";
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 93);
            this.Controls.Add(this.lineSeperatorLabel);
            this.Controls.Add(this.seperatorLabel);
            this.Controls.Add(this.lineSeperator);
            this.Controls.Add(this.seperator);
            this.Controls.Add(this.savebutton);
            this.Name = "SaveForm";
            this.Text = "SaveForm";
            this.Load += new System.EventHandler(this.SaveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.TextBox seperator;
        private System.Windows.Forms.TextBox lineSeperator;
        private System.Windows.Forms.Label seperatorLabel;
        private System.Windows.Forms.Label lineSeperatorLabel;
    }
}