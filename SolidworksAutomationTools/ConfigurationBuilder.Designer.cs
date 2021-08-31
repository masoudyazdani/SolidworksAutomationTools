namespace SolidworksAutomationTools
{
    partial class ConfigurationBuilder
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
            this.configTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.createConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // configAsmTxt
            // 
            this.configAsmTxt.Location = new System.Drawing.Point(121, 40);
            this.configAsmTxt.Name = "configAsmTxt";
            this.configAsmTxt.ReadOnly = true;
            this.configAsmTxt.Size = new System.Drawing.Size(362, 20);
            this.configAsmTxt.TabIndex = 8;
            this.configAsmTxt.TextChanged += new System.EventHandler(this.configAsmTxt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Config Assembly";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // configAsmSelectBtn
            // 
            this.configAsmSelectBtn.Location = new System.Drawing.Point(502, 25);
            this.configAsmSelectBtn.Name = "configAsmSelectBtn";
            this.configAsmSelectBtn.Size = new System.Drawing.Size(155, 50);
            this.configAsmSelectBtn.TabIndex = 6;
            this.configAsmSelectBtn.Text = "Select Config Assembly";
            this.configAsmSelectBtn.UseVisualStyleBackColor = true;
            this.configAsmSelectBtn.Click += new System.EventHandler(this.configAsmSelectBtn_Click);
            // 
            // configTxt
            // 
            this.configTxt.Location = new System.Drawing.Point(121, 117);
            this.configTxt.Name = "configTxt";
            this.configTxt.Size = new System.Drawing.Size(536, 20);
            this.configTxt.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Config Name";
            // 
            // createConfig
            // 
            this.createConfig.Location = new System.Drawing.Point(502, 173);
            this.createConfig.Name = "createConfig";
            this.createConfig.Size = new System.Drawing.Size(155, 50);
            this.createConfig.TabIndex = 11;
            this.createConfig.Text = "Create Config";
            this.createConfig.UseVisualStyleBackColor = true;
            this.createConfig.Click += new System.EventHandler(this.createConfig_Click);
            // 
            // ConfigurationBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 254);
            this.Controls.Add(this.createConfig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.configTxt);
            this.Controls.Add(this.configAsmTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.configAsmSelectBtn);
            this.Name = "ConfigurationBuilder";
            this.Text = "Configuration Builder";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox configAsmTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button configAsmSelectBtn;
        private System.Windows.Forms.TextBox configTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button createConfig;
    }
}