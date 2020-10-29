namespace LevelCreating
{
    partial class LevelSize
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
            this.LevelWidth = new System.Windows.Forms.TextBox();
            this.LevelHeight = new System.Windows.Forms.TextBox();
            this.Width = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LevelWidth
            // 
            this.LevelWidth.Location = new System.Drawing.Point(92, 12);
            this.LevelWidth.Name = "LevelWidth";
            this.LevelWidth.Size = new System.Drawing.Size(100, 20);
            this.LevelWidth.TabIndex = 0;
            this.LevelWidth.Text = "px";
            this.LevelWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LevelHeight
            // 
            this.LevelHeight.Location = new System.Drawing.Point(92, 38);
            this.LevelHeight.Name = "LevelHeight";
            this.LevelHeight.Size = new System.Drawing.Size(100, 20);
            this.LevelHeight.TabIndex = 1;
            this.LevelHeight.Text = "px";
            this.LevelHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Width
            // 
            this.Width.AutoSize = true;
            this.Width.Location = new System.Drawing.Point(51, 15);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(35, 13);
            this.Width.TabIndex = 2;
            this.Width.Text = "Width";
            // 
            // Height
            // 
            this.Height.AutoSize = true;
            this.Height.Location = new System.Drawing.Point(51, 38);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(38, 13);
            this.Height.TabIndex = 3;
            this.Height.Text = "Height";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(198, 28);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(60, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // LevelSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 73);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.LevelHeight);
            this.Controls.Add(this.LevelWidth);
            this.Name = "LevelSize";
            this.Text = "LevelSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LevelWidth;
        private System.Windows.Forms.TextBox LevelHeight;
        private System.Windows.Forms.Label Width;
        private System.Windows.Forms.Label Height;
        private System.Windows.Forms.Button OKButton;
    }
}