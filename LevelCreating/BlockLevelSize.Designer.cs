namespace LevelCreating
{
    partial class BlockLevelSize
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
            this.BlockWidth = new System.Windows.Forms.Label();
            this.BlockHeight = new System.Windows.Forms.Label();
            this.BlockX = new System.Windows.Forms.TextBox();
            this.BlockY = new System.Windows.Forms.TextBox();
            this.OKbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BlockWidth
            // 
            this.BlockWidth.AutoSize = true;
            this.BlockWidth.Location = new System.Drawing.Point(13, 13);
            this.BlockWidth.Name = "BlockWidth";
            this.BlockWidth.Size = new System.Drawing.Size(35, 13);
            this.BlockWidth.TabIndex = 0;
            this.BlockWidth.Text = "Width";
            // 
            // BlockHeight
            // 
            this.BlockHeight.AutoSize = true;
            this.BlockHeight.Location = new System.Drawing.Point(12, 40);
            this.BlockHeight.Name = "BlockHeight";
            this.BlockHeight.Size = new System.Drawing.Size(38, 13);
            this.BlockHeight.TabIndex = 1;
            this.BlockHeight.Text = "Height";
            // 
            // BlockX
            // 
            this.BlockX.Location = new System.Drawing.Point(54, 10);
            this.BlockX.Name = "BlockX";
            this.BlockX.Size = new System.Drawing.Size(100, 20);
            this.BlockX.TabIndex = 2;
            this.BlockX.Text = "px";
            this.BlockX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BlockY
            // 
            this.BlockY.Location = new System.Drawing.Point(54, 37);
            this.BlockY.Name = "BlockY";
            this.BlockY.Size = new System.Drawing.Size(100, 20);
            this.BlockY.TabIndex = 3;
            this.BlockY.Text = "px";
            this.BlockY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(160, 13);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 4;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // BlockLevelSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 68);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.BlockY);
            this.Controls.Add(this.BlockX);
            this.Controls.Add(this.BlockHeight);
            this.Controls.Add(this.BlockWidth);
            this.Name = "BlockLevelSize";
            this.Text = "BlockLevelSize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BlockWidth;
        private System.Windows.Forms.Label BlockHeight;
        private System.Windows.Forms.TextBox BlockX;
        private System.Windows.Forms.TextBox BlockY;
        private System.Windows.Forms.Button OKbutton;
    }
}