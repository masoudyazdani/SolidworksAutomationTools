namespace SolidworksAutomationTools
{
    partial class CreatePartFromList
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
            this.outputFolder = new System.Windows.Forms.TextBox();
            this.outputFolderSelectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputFolder
            // 
            this.outputFolder.Location = new System.Drawing.Point(119, 27);
            this.outputFolder.Name = "outputFolder";
            this.outputFolder.ReadOnly = true;
            this.outputFolder.Size = new System.Drawing.Size(362, 20);
            this.outputFolder.TabIndex = 16;
            // 
            // outputFolderSelectBtn
            // 
            this.outputFolderSelectBtn.Location = new System.Drawing.Point(500, 12);
            this.outputFolderSelectBtn.Name = "outputFolderSelectBtn";
            this.outputFolderSelectBtn.Size = new System.Drawing.Size(155, 50);
            this.outputFolderSelectBtn.TabIndex = 15;
            this.outputFolderSelectBtn.Text = "Select Output Folder";
            this.outputFolderSelectBtn.UseVisualStyleBackColor = true;
            this.outputFolderSelectBtn.Click += new System.EventHandler(this.outputFolderSelectBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Main Folder";
            // 
            // CreatePartFromList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 87);
            this.Controls.Add(this.outputFolder);
            this.Controls.Add(this.outputFolderSelectBtn);
            this.Controls.Add(this.label3);
            this.Name = "CreatePartFromList";
            this.Text = "CreatePartFromList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputFolder;
        private System.Windows.Forms.Button outputFolderSelectBtn;
        private System.Windows.Forms.Label label3;
    }
}