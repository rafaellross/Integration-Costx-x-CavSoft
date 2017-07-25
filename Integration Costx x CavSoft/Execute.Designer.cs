namespace Integration_Costx_x_CavSoft
{
    partial class Execute
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtResults = new System.Windows.Forms.RichTextBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtResults);
            this.groupBox1.Location = new System.Drawing.Point(26, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Importing Results";
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(0, 19);
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.Size = new System.Drawing.Size(454, 205);
            this.txtResults.TabIndex = 0;
            this.txtResults.Text = "";
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(394, 359);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(86, 23);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "&Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // Execute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 394);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Execute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Execute";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Execute_FormClosed);
            this.Load += new System.EventHandler(this.Execute_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtResults;
        private System.Windows.Forms.Button btnFinish;
    }
}