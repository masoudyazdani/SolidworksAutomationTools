namespace SolidworksAutomationTools
{
    partial class AutoCollapse
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
            this.configAsmTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.configAsmSelectBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.collapeTxt = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // configAsmTxt
            // 
            this.configAsmTxt.Location = new System.Drawing.Point(130, 43);
            this.configAsmTxt.Name = "configAsmTxt";
            this.configAsmTxt.ReadOnly = true;
            this.configAsmTxt.Size = new System.Drawing.Size(362, 20);
            this.configAsmTxt.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Main Drawing ";
            // 
            // configAsmSelectBtn
            // 
            this.configAsmSelectBtn.Location = new System.Drawing.Point(511, 28);
            this.configAsmSelectBtn.Name = "configAsmSelectBtn";
            this.configAsmSelectBtn.Size = new System.Drawing.Size(155, 50);
            this.configAsmSelectBtn.TabIndex = 9;
            this.configAsmSelectBtn.Text = "Select Main Drawing";
            this.configAsmSelectBtn.UseVisualStyleBackColor = true;
            this.configAsmSelectBtn.Click += new System.EventHandler(this.configAsmSelectBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(511, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 50);
            this.button1.TabIndex = 12;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Desired Collape List";
            // 
            // collapeTxt
            // 
            this.collapeTxt.Location = new System.Drawing.Point(130, 102);
            this.collapeTxt.Name = "collapeTxt";
            this.collapeTxt.ReadOnly = true;
            this.collapeTxt.Size = new System.Drawing.Size(362, 20);
            this.collapeTxt.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(511, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 50);
            this.button2.TabIndex = 15;
            this.button2.Text = "Select List File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 226);
            this.Controls.Add(this.collapeTxt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.configAsmTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.configAsmSelectBtn);
            this.Name = "Form3";
            this.Text = "Auto Collapse Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox configAsmTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button configAsmSelectBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox collapeTxt;
        private System.Windows.Forms.Button button2;
    }
}