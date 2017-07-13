namespace Integration_Costx_x_CavSoft
{
    partial class Connection
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grCostx = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerCostx = new System.Windows.Forms.TextBox();
            this.txtDatabaseCostx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserCostx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPasswordCostx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPasswordCavSoft = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserCavSoft = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDatabaseCavSoft = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtServerCavSoft = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnTestCostx = new System.Windows.Forms.Button();
            this.btnTestCavSoft = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grCostx.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grCostx);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.btnNext);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(506, 394);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // grCostx
            // 
            this.grCostx.Controls.Add(this.btnTestCostx);
            this.grCostx.Controls.Add(this.txtPasswordCostx);
            this.grCostx.Controls.Add(this.label4);
            this.grCostx.Controls.Add(this.txtUserCostx);
            this.grCostx.Controls.Add(this.label3);
            this.grCostx.Controls.Add(this.txtDatabaseCostx);
            this.grCostx.Controls.Add(this.label2);
            this.grCostx.Controls.Add(this.txtServerCostx);
            this.grCostx.Controls.Add(this.label1);
            this.grCostx.Location = new System.Drawing.Point(12, 12);
            this.grCostx.Name = "grCostx";
            this.grCostx.Size = new System.Drawing.Size(468, 153);
            this.grCostx.TabIndex = 0;
            this.grCostx.TabStop = false;
            this.grCostx.Text = "Database CostX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server: ";
            // 
            // txtServerCostx
            // 
            this.txtServerCostx.Location = new System.Drawing.Point(103, 30);
            this.txtServerCostx.Name = "txtServerCostx";
            this.txtServerCostx.Size = new System.Drawing.Size(189, 20);
            this.txtServerCostx.TabIndex = 1;
            this.txtServerCostx.TextChanged += new System.EventHandler(this.txtServerCostx_TextChanged);
            // 
            // txtDatabaseCostx
            // 
            this.txtDatabaseCostx.Location = new System.Drawing.Point(103, 56);
            this.txtDatabaseCostx.Name = "txtDatabaseCostx";
            this.txtDatabaseCostx.Size = new System.Drawing.Size(189, 20);
            this.txtDatabaseCostx.TabIndex = 3;
            this.txtDatabaseCostx.TextChanged += new System.EventHandler(this.txtDatabaseCostx_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database Name: ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtUserCostx
            // 
            this.txtUserCostx.Location = new System.Drawing.Point(103, 82);
            this.txtUserCostx.Name = "txtUserCostx";
            this.txtUserCostx.Size = new System.Drawing.Size(189, 20);
            this.txtUserCostx.TabIndex = 5;
            this.txtUserCostx.TextChanged += new System.EventHandler(this.txtUserCostx_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User: ";
            // 
            // txtPasswordCostx
            // 
            this.txtPasswordCostx.Location = new System.Drawing.Point(103, 108);
            this.txtPasswordCostx.Name = "txtPasswordCostx";
            this.txtPasswordCostx.Size = new System.Drawing.Size(189, 20);
            this.txtPasswordCostx.TabIndex = 7;
            this.txtPasswordCostx.TextChanged += new System.EventHandler(this.txtPasswordCostx_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestCavSoft);
            this.groupBox1.Controls.Add(this.txtPasswordCavSoft);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUserCavSoft);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDatabaseCavSoft);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtServerCavSoft);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database CavSoft";
            // 
            // txtPasswordCavSoft
            // 
            this.txtPasswordCavSoft.Location = new System.Drawing.Point(103, 108);
            this.txtPasswordCavSoft.Name = "txtPasswordCavSoft";
            this.txtPasswordCavSoft.Size = new System.Drawing.Size(189, 20);
            this.txtPasswordCavSoft.TabIndex = 7;
            this.txtPasswordCavSoft.TextChanged += new System.EventHandler(this.txtPasswordCavSoft_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Password: ";
            // 
            // txtUserCavSoft
            // 
            this.txtUserCavSoft.Location = new System.Drawing.Point(103, 82);
            this.txtUserCavSoft.Name = "txtUserCavSoft";
            this.txtUserCavSoft.Size = new System.Drawing.Size(189, 20);
            this.txtUserCavSoft.TabIndex = 5;
            this.txtUserCavSoft.TextChanged += new System.EventHandler(this.txtUserCavSoft_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "User: ";
            // 
            // txtDatabaseCavSoft
            // 
            this.txtDatabaseCavSoft.Location = new System.Drawing.Point(103, 56);
            this.txtDatabaseCavSoft.Name = "txtDatabaseCavSoft";
            this.txtDatabaseCavSoft.Size = new System.Drawing.Size(189, 20);
            this.txtDatabaseCavSoft.TabIndex = 3;
            this.txtDatabaseCavSoft.TextChanged += new System.EventHandler(this.txtDatabaseCavSoft_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Database Name: ";
            // 
            // txtServerCavSoft
            // 
            this.txtServerCavSoft.Location = new System.Drawing.Point(103, 30);
            this.txtServerCavSoft.Name = "txtServerCavSoft";
            this.txtServerCavSoft.Size = new System.Drawing.Size(189, 20);
            this.txtServerCavSoft.TabIndex = 1;
            this.txtServerCavSoft.TextChanged += new System.EventHandler(this.txtServerCavSoft_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Server: ";
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(291, 187);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(86, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(394, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(199, 187);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Back";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnTestCostx
            // 
            this.btnTestCostx.Enabled = false;
            this.btnTestCostx.Location = new System.Drawing.Point(339, 70);
            this.btnTestCostx.Name = "btnTestCostx";
            this.btnTestCostx.Size = new System.Drawing.Size(110, 23);
            this.btnTestCostx.TabIndex = 5;
            this.btnTestCostx.Text = "Test Connection";
            this.btnTestCostx.UseVisualStyleBackColor = true;
            this.btnTestCostx.Click += new System.EventHandler(this.btnTestCostx_Click);
            // 
            // btnTestCavSoft
            // 
            this.btnTestCavSoft.Enabled = false;
            this.btnTestCavSoft.Location = new System.Drawing.Point(339, 68);
            this.btnTestCavSoft.Name = "btnTestCavSoft";
            this.btnTestCavSoft.Size = new System.Drawing.Size(110, 23);
            this.btnTestCavSoft.TabIndex = 8;
            this.btnTestCavSoft.Text = "Test Connection";
            this.btnTestCavSoft.UseVisualStyleBackColor = true;
            this.btnTestCavSoft.Click += new System.EventHandler(this.btnTestCavSoft_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 394);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integration CostX x CavSoft";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grCostx.ResumeLayout(false);
            this.grCostx.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grCostx;
        private System.Windows.Forms.TextBox txtDatabaseCostx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerCostx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPasswordCostx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserCostx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPasswordCavSoft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserCavSoft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDatabaseCavSoft;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtServerCavSoft;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnTestCostx;
        private System.Windows.Forms.Button btnTestCavSoft;
    }
}

